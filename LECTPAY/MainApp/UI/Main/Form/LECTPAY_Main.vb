Imports System.ComponentModel
Imports App.Common.Data.Model
Imports Microsoft.Win32

''' <summary>
''' 出講料計算メイン画面
''' </summary>
Public Class LECTPAY_Main
    Inherits BaseForm
    Implements IMDIParent, IMdiParentMethods

#Region "フィールド・プロパティ関連"

    ''' <summary>WIN32API：SendMessage(user32.dll)</summary>
    Public Declare Auto Function SendMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr

    ''' <summary>WIN32API：ReleaseCapture(user32.dll)</summary>
    Public Declare Auto Function ReleaseCapture Lib "user32.dll" () As Boolean

    ''' <summary>出講料計算 ViewController</summary>
    Private _controller As New LectPayMainViewController

    ''' <summary>自前ドラッグ・リサイズ Controller</summary>
    Private _dragController As DragResizeController

    ''' <summary>タイトルパネルコントローラー</summary>
    Private _titleBarPanel As TitleBarPanel

    ''' <summary>サイドメニューコントローラー</summary>
    Private _sideMenuPanel As SideMenuPanel

    ''' <summary>MDIコントローラー</summary>
    Private _mdiController As MdiController

    ''' <summary>本番環境か否かの判定フラグ</summary>
    Public ReadOnly Property IsProdEnv As Boolean = SqlDataBaseUtil.IsProduction() = True

    '''' <summary>初期化処理完了フラグ</summary>
    'Private Shared _initialized As Boolean = False
    Private isExpanding As Boolean = False
    Private isCollapsing As Boolean = False
    Private minWidth As Integer = 0


    ''' <summary>
    ''' タイトルバーパネル
    ''' </summary>
    Private _titolPanel As Panel
    Public ReadOnly Property TitolBarPanel As TitleBarPanel
        Get
            Return _titleBarPanel
        End Get
    End Property

    ''' <summary>
    ''' サイドメニューパネル
    ''' </summary>
    Private _menuPanel As Panel
    Public ReadOnly Property SideMenuPanel As Panel Implements IMDIParent.SideMenuPanel
        Get
            Return _sideMenuPanel.pnlMenu
        End Get
    End Property

    ''' <summary>
    ''' 初期化処理完了フラグ
    ''' </summary>
    ''' <returns></returns>
    Public Property IsInitComplete As Boolean
        Get
            Return IsImportComplete
        End Get
        Set(value As Boolean)
            IsImportComplete = value
        End Set
    End Property

#End Region


#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
        InitializeComponent()

        ' メイン画面初期化(属性設定)
        Me.IsMdiContainer = True
        Me.FormBorderStyle = FormBorderStyle.None
        Me.BackColor = Color.Black
        Me.MinimumSize = New Size(960, 540)
        Me.Size = ScreenUtil.ScreenWorkAreaSize(Me)

        ' 画面部品初期化(部品生成)
        InitializeWindow()

        ' SynchronizationContext 初期化
        StateDispatcher.InitializeForUIThread()
        StateDispatcher.Attach()

        ' --------------------------------------------
        ' ハンドラ登録
        ' --------------------------------------------
        ' Windowsの「表示設定」を変更したときに発生するシステムイベント
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf OnDisplaySettingsChanged

        ' Windowsの「ユーザー設定」が変更されたときに発生するシステムイベント
        AddHandler SystemEvents.UserPreferenceChanged, AddressOf OnUserPreferenceChanged

        ' MDI 親フォームで、子フォームのアクティブ状態が変わったときに発生するイベント
        AddHandler Me.MdiChildActivate, AddressOf OnMdiChildActivated
    End Sub

    ''' <summary>
    ''' 画面部品の初期化
    ''' 
    ''' タイトルバーパネル、サイドメニューパネルの生成及び初期化処理を行います。
    ''' </summary>
    Private Sub InitializeWindow()

        ' タイトルバーパネル
        Me._titleBarPanel = New TitleBarPanel(Me)
        Me._titolPanel = _titleBarPanel.InitializeTitleBar()

        ' サイドメニューパネル
        Me._sideMenuPanel = New SideMenuPanel(Me)
        Me._menuPanel = _sideMenuPanel.InitializeMenu()

        ' パネル統合
        Me.Controls.Add(_menuPanel)
        Me.Controls.Add(_titolPanel)

        ' コントローラのインスタンス生成
        Me._mdiController = New MdiController(Me)
        Me._dragController = New DragResizeController(Me)
    End Sub

#End Region


#Region "初期表示処理"

    ''' <summary>
    ''' Load イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub LectPay_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' MDIWindowの部品管理設定
        AppState.MDIMainForm = Me
        AppState.MDIMainHeaderPanel = Me._titleBarPanel.pnlTitle
        AppState.MDIMainSizePanel = Me._sideMenuPanel.pnlMenu

        ' MDI画面初期表示
        MDIWindowRefresh()

        ' 初期化完了
        IsImportComplete = True
    End Sub

    ''' <summary>
    ''' MDI画面初期表示処理
    ''' </summary>
    Private Sub MDIWindowRefresh()
        AppState.DebugSystemDate = SystemDateForDebugging()

        ' 左上(0, 0)座標に画面移動
        Me.Location = New Point(0, 0)

        ' タイトルバーレイアウト調整
        _titleBarPanel.TitleLayoutRefresh()

        ' サイドメニューレイアウト調整
        _sideMenuPanel.LayoutMenu()

        ' サイドメニューボタン活性／非活性制御
        SideMenuEnable()

        ' ロック状況の更新タイマーリセット
        LockMonitoringTimer.Stop()
        LockMonitoringTimer.Start()
    End Sub

    ''' <summary>
    ''' メインボタン活性／非活性制御
    ''' </summary>
    Public Sub SideMenuEnable()
        Dim buttons = _sideMenuPanel.menuButtons

        If DateUtil.IsNullOrEmpty(AppState.TargetYearMonth) Then
            buttons(0).Enabled = True
            buttons(1).Enabled = False
            buttons(2).Enabled = False
            buttons(3).Enabled = False
            buttons(4).Enabled = False
        Else
            buttons(0).Enabled = True
            buttons(1).Enabled = True
            buttons(2).Enabled = True
            buttons(3).Enabled = True
            buttons(4).Enabled = True
        End If
    End Sub

#End Region


#Region "アプリ終了時処理"

    ''' <summary>
    ''' Closing イベント
    ''' 
    ''' フォームが閉じる「直前」に実行される処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GHRUN_Main_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        LockMonitoringTimer.Stop()

        Dim result As New YearMonthLockModel
        Dim model As New YearMonthLockModel
        result = _controller.UnLockedTargetYearMonth(model)
        If Not result.Success Then
            LectPayAppMsgBox.ShowMessage(result.BusinessMessage)
        End If
    End Sub

    ''' <summary>
    ''' FormClosed イベント
    ''' 
    ''' フォームが完全に閉じられた後（FormClosed）にお掃除処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GHRUN_Main_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf OnDisplaySettingsChanged
        RemoveHandler SystemEvents.UserPreferenceChanged, AddressOf OnUserPreferenceChanged
        RemoveHandler Me.MdiChildActivate, AddressOf OnMdiChildActivated
    End Sub

#End Region


#Region "リサイズ処理"

    ''' <summary>
    ''' Resize イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GHRUN_Main_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        WindowResize()
    End Sub

    ''' <summary>
    ''' ディスプレイの設定（解像度など）が変更されたときに実行される処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OnDisplaySettingsChanged(sender As Object, e As EventArgs)
        WindowResize()
    End Sub

    ''' <summary>
    ''' Windowsのシステム設定やテーマが変更されたときに実行される処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OnUserPreferenceChanged(sender As Object, e As UserPreferenceChangedEventArgs)
        If e.Category = UserPreferenceCategory.Window Then
            WindowResize()
        End If
    End Sub

    ''' <summary>
    ''' 画面のリサイズ時の処理
    ''' </summary>
    Private Sub WindowResize()
        If Not IsImportComplete Then
            Return
        End If
        _titleBarPanel.TitleLayoutRefresh()
    End Sub

    ''' <summary>
    ''' フォームの作成パラメータを拡張し、WS_EX_COMPOSITED を付与する。
    ''' WS_EX_COMPOSITED により、フォーム全体がダブルバッファリングされ、
    ''' リサイズ時や再描画時のチラつきを軽減する。
    ''' </summary>
    ''' <returns>拡張済みの CreateParams オブジェクト。</returns>
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams

            ' WS_EX_COMPOSITED (&H2000000) を付与してフォーム全体を合成描画にする
            cp.ExStyle = cp.ExStyle Or &H2000000

            Return cp
        End Get
    End Property

    ''' <summary>
    ''' フォームの描画処理。
    ''' リサイズ中は大量の再描画が発生するため、描画を抑制して高速化する。
    ''' リサイズ終了後に通常描画を行うことで、チラつきを防止する。
    ''' </summary>
    ''' <param name="e">PaintEventArgs（描画用 Graphics を含む）。</param>
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        ' リサイズ中は描画を行わず、負荷とチラつきを抑える
        If _dragController.resizing Then
            Return
        End If

        ' 通常描画
        MyBase.OnPaint(e)
    End Sub

#End Region


#Region "システムイベント通知"

    ''' <summary>
    ''' ロック状態の変更通知処理
    ''' </summary>
    ''' <param name="key"></param>
    ''' <param name="value"></param>
    Public Overrides Sub OnUiStateChanged(key As String, value As Object)
        StateUpdateHelper.UpdateState(Me, key, value)
    End Sub

    ''' <summary>
    ''' 画面をアクティブにします。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Overridable Sub OnMdiChildActivated(sender As Object, e As EventArgs)
        _mdiController.OnMdiChildActivated()
    End Sub

#End Region


#Region "その他処理"

    ''' <summary>
    ''' 強制ロック解除監視処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub LockMonitoringTimer_Tick(sender As Object, e As EventArgs) Handles LockMonitoringTimer.Tick
        Dim lockInfo As YearMonthLockModel = _controller.TargetYearMonthLockCheck()
        If Not lockInfo.Success Then
            Return
        End If

        If lockInfo.SubStatus = 0 Then
            Return
        End If

        MessageBox.Show("他ユーザーに強制解除されました。年月度設定からやり直してください。")

        AppState.TargetYearMonth = Date.MinValue
        AppState.LockStatus = False
        _mdiController.CloseMDIChildAll()
        MDIWindowRefresh()
        _sideMenuPanel.LayoutMenu()
    End Sub

    ''' <summary>
    ''' 画面起動共通処理
    ''' </summary>
    ''' <param name="formType"></param>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Sub OpenChildFormDynamic(formType As Type, sender As Object, e As EventArgs)

        For Each child As Form In Me.MdiChildren
            If child.GetType() Is formType Then
                child.Activate()
                Return
            End If
        Next

        Dim newForm As Form = CType(Activator.CreateInstance(formType), Form)
        _mdiController.CloseMDIChildScreen(newForm)
    End Sub

    ''' <summary>
    ''' 現在日付取得
    ''' 
    ''' リリース版はマシン日付。デバッグは起動パラメタの日付を取得します。
    ''' </summary>
    ''' <returns></returns>
    Private Function SystemDateForDebugging() As Date
        Dim result As Date = Now()

#If DEBUG Then
        If My.Application.CommandLineArgs.Count > 0 Then
            For Each arg As String In My.Application.CommandLineArgs
                Dim param() As String = arg.ToUpper().Split("=")
                If param(0) = "SYSTEM_DATE" Then
                    If Not Date.TryParse(param(1), result) Then
                        result = Now()
                    End If
                    Exit For
                End If
            Next
        End If
#End If

        Return result
    End Function

    ''' <summary>
    ''' MDI子画面からサイドメニューパネルを自動開閉するメソッド
    ''' 
    ''' ※いまは、タイトルバーの[Ξ]の押下イベントを発生させる。
    ''' </summary>
    Public Sub OpenSideMenuPanel() Implements IMdiParentMethods.OpenSideMenuPanel
        TitolBarPanel.ToggleSideMenu()
    End Sub

    ''' <summary>
    ''' MDI子画面からサイドメニューパネルを閉じるために使用するメソッド
    ''' 
    ''' ※いまは、タイトルバーの[Ξ]の押下イベントを発生させる。
    ''' </summary>
    Public Sub CloseSideMenuPanel() Implements IMdiParentMethods.CloseSideMenuPanel
        TitolBarPanel.ToggleSideMenu()
    End Sub

#End Region

End Class
