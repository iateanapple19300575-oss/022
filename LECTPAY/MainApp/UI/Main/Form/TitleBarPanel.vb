Imports LecturePay.Common.Infrastructure
Imports LecturePay.Main

''' <summary>
''' タイトルバーパネル
''' </summary>
Public Class TitleBarPanel

    ''' <summary>出講料計算メイン画面 Form</summary>
    Private ReadOnly mainForm As LECTPAY_Main

    ''' <summary>タイトルバーパネル</summary>
    Public pnlTitle As Panel

    ''' <summary>[Ξ]トグルボタン</summary>
    Private btnToggle As Button

    ''' <summary>アプリ名ラベル</summary>
    Private lblTitle As Label

    ''' <summary>対象年月度ラベル</summary>
    Private lblYM As Label

    ''' <summary>対象年月ロック状態</summary>
    Private lblLock As Label

    ''' <summary>ユーザーID</summary>
    Private lblUser As Label

    ''' <summary>システム日付(デバッグ用)</summary>
    Private lblSystemDate As Label

    ''' <summary>[最小化]ボタン</summary>
    Private btnMinimize As Button

    ''' <summary>[最大化]ボタン</summary>
    Private btnMaximize As Button

    ''' <summary>[閉じる]ボタン</summary>
    Private btnAppClose As Button

    ''' <summary>サイドメニューパネル開閉タイマー</summary>
    Private animationTimer As New Timer()

    ' サイドメニューパネルの開閉状態
    Private isExpanding As Boolean = False

    ' クロージングフラグ
    Private isCollapsing As Boolean = False

    ' サイドパネル幅サイズ
    Private sidePanelWidth As Integer = ViewConstants.SIDE_PANEL_WIDTH

    ' サイドメニューパネル最小サイズ
    Private minWidth As Integer = 0

    ' サイドメニュー開閉アニメーション間隔
    Private animationStep As Integer = 10

    ' 最大化表示状態管理
    Private isPseudoMaximized As Boolean = False

    ' フォームの位置・サイズ情報
    Private normalBounds As Rectangle


    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="form"></param>
    Public Sub New(form As LECTPAY_Main)
        Me.mainForm = form

        ' タイマーの初期設定
        animationTimer.Interval = 10
    End Sub

    ''' <summary>
    ''' パネルの初期化
    ''' </summary>
    Public Function InitializeTitleBar() As Panel
        ' タイトルバー部品生成
        AddTitleButton()
        ' タイトルバー部品イベント定義
        AddMouseEventHandler()
        ' タイトルバー部品レイアウト調整
        TitleLayoutRefresh()

        ' タイトルパネル
        Return pnlTitle
    End Function

    ''' <summary>
    ''' タイトルバー部品の生成
    ''' </summary>
    Private Sub AddTitleButton()
        ' タイトルパネル
        pnlTitle = New Panel With {
            .Name = "HeaderTitlePanel",
            .Dock = DockStyle.Top,
            .Height = 40,
            .BackColor = BackcolorWithTitleBar()
        }

        ' [Ξ]ボタン
        btnToggle = New Button With {
            .Text = "Ξ",
            .Font = New Font("游ゴシック", 12, FontStyle.Regular),
            .Size = New Size(30, 30),
            .ForeColor = Color.Black,
            .BackColor = ViewConstants.COLOR_BUTTON_DEFAULT,
            .Location = New Point(10, 5),
            .Anchor = AnchorStyles.Top Or AnchorStyles.Left
        }
        AddHandler btnToggle.Click, AddressOf toggleButton_Click
        pnlTitle.Controls.Add(btnToggle)

        ' アプリ名ラベル
        lblTitle = New Label With {
            .Name = "lblAppliName",
            .Text = AppCommon.ApplicationName,
            .Font = New Font("游ゴシック", 12, FontStyle.Regular),
            .Location = New Point(10, 10),
            .Height = 30,
            .ForeColor = Color.White,
            .BackColor = BackcolorWithTitleBar(),
            .AutoSize = True
        }
        pnlTitle.Controls.Add(lblTitle)

        ' 対象年度ラベル
        lblYM = New Label With {
            .Name = "lblYearMonth",
            .Text = AppCommon.GetTargetYearMonth(),
            .Font = New Font("游ゴシック", 12, FontStyle.Regular),
            .Location = New Point(10, 10),
            .ForeColor = Color.White,
            .BackColor = BackcolorWithTitleBar(),
            .AutoSize = True
        }
        pnlTitle.Controls.Add(lblYM)

        ' ロック状態ラベル
        lblLock = New Label With {
            .Name = "lblLockState",
            .Text = AppCommon.GetLockStatus(),
            .Font = New Font("游ゴシック", 12, FontStyle.Regular),
            .Location = New Point(10, 10),
            .ForeColor = Color.White,
            .BackColor = BackcolorWithTitleBar(),
            .AutoSize = True
        }
        pnlTitle.Controls.Add(lblLock)

        ' ユーザーラベル
        lblUser = New Label With {
            .Name = "lblUserName",
            .Text = AppCommon.GetUser(),
            .Font = New Font("游ゴシック", 12, FontStyle.Regular),
            .Location = New Point(10, 10),
            .ForeColor = Color.White,
            .BackColor = BackcolorWithTitleBar(),
            .AutoSize = True
        }
        pnlTitle.Controls.Add(lblUser)

        ' [最小化]ボタン
        btnMinimize = New Button With {
            .Text = "＿",
            .Font = New Font("游ゴシック", 12, FontStyle.Regular),
            .Size = New Size(30, 30),
            .ForeColor = Color.Black,
            .BackColor = ViewConstants.COLOR_BUTTON_DEFAULT,
            .Location = New Point(10, 5),
            .Anchor = AnchorStyles.Top Or AnchorStyles.Left
        }
        AddHandler btnMinimize.Click,
            Sub(sender, e) mainForm.WindowState = FormWindowState.Minimized
        pnlTitle.Controls.Add(btnMinimize)

        ' [最大化]ボタン
        btnMaximize = New Button With {
            .Text = "□",
            .Font = New Font("游ゴシック", 12, FontStyle.Regular),
            .Size = New Size(30, 30),
            .ForeColor = Color.Black,
            .BackColor = ViewConstants.COLOR_BUTTON_DEFAULT,
            .Location = New Point(10, 5),
            .Anchor = AnchorStyles.Top Or AnchorStyles.Left
        }
        AddHandler btnMaximize.Click, AddressOf MaximizeWindow
        pnlTitle.Controls.Add(btnMaximize)

        ' [閉じる]ボタン
        btnAppClose = New Button With {
            .Text = "閉じる",
            .Font = New Font("游ゴシック", 12, FontStyle.Regular),
            .Size = New Size(80, 30),
            .ForeColor = Color.Black,
            .BackColor = ViewConstants.COLOR_BUTTON_DEFAULT,
            .Location = New Point(10, 5),
            .Anchor = AnchorStyles.Top Or AnchorStyles.Left,
            .Name = "btnAppClose"
        }
        AddHandler btnAppClose.Click, Sub(sender, e) mainForm.Close()
        pnlTitle.Controls.Add(btnAppClose)
    End Sub

    ''' <summary>
    ''' イベントハンドラ登録
    ''' </summary>
    Private Sub AddMouseEventHandler()
        ' タイトルバー：ダブルクリックイベント定義
        AddHandler pnlTitle.MouseDoubleClick, AddressOf TitleBar_DoubleClick
        AddHandler lblTitle.MouseDoubleClick, AddressOf TitleBar_DoubleClick
        AddHandler lblYM.MouseDoubleClick, AddressOf TitleBar_DoubleClick
        AddHandler lblLock.MouseDoubleClick, AddressOf TitleBar_DoubleClick
        AddHandler lblUser.MouseDoubleClick, AddressOf TitleBar_DoubleClick

        ' サイドパネル開閉：タイマーイベント定義
        AddHandler animationTimer.Tick, AddressOf AnimationTimer_Tick
    End Sub

    ''' <summary>
    ''' タイトルバー部品のレイアウト調整
    ''' </summary>
    Public Sub TitleLayoutRefresh()
        ' 初期化処理中はスキップ
        'If Not Me.mainForm.IsInitComplete Then
        '    Return
        'End If

        ' [Ξ]トグルボタン
        btnToggle.Left = ViewConstants.TITLE_PANEL_MENU_CTRL_LEFT
        ' [アプリ名]ラベル 
        lblTitle.Left = ViewConstants.TITLE_PANEL_APPL_NAME_LEFT
        ' [対象年月度]ラベル
        lblYM.Left = ViewConstants.TITLE_PANEL_YEAR_MONTH_LEFT
        ' [ロック状態]ラベル
        lblLock.Left = ViewConstants.TITLE_PANEL_LOCK_STATUS_LEFT

        ' [ユーザID]ラベル　
        lblUser.Left = mainForm.Width - ViewConstants.TITLE_PANEL_USER_NAME_RLEFT
        ' [最小化]ボタン　
        btnMinimize.Left = mainForm.Width - ViewConstants.TITLE_PANEL_MIN_RLEFT
        ' [最大化]ボタン
        btnMaximize.Left = mainForm.Width - ViewConstants.TITLE_PANEL_MAX_RLEFT
        ' [閉じる]ボタン　
        btnAppClose.Left = mainForm.Width - ViewConstants.TITLE_PANEL_CLOSE_RLEFT
    End Sub

    '''' <summary>
    '''' タイトルバー部品のレイアウト調整
    '''' 
    '''' リサイズイベントで右側配置ボタンの位置調整を行います。
    '''' </summary>
    'Public Sub RefreshLayout()
    '    Dim margin As Integer = 10
    '    Dim rightEdge As Integer = mainForm.Width

    '    ' [閉じる]ボタン　
    '    btnAppClose.Left = rightEdge - btnAppClose.Width - margin
    '    ' [最大化]ボタン
    '    btnMaximize.Left = btnAppClose.Left - btnMaximize.Width - margin
    '    ' [最小化]ボタン　
    '    btnMinimize.Left = btnMaximize.Left - btnMinimize.Width - margin
    '    ' [ユーザID]ラベル　
    '    lblUser.Left = btnMinimize.Left - lblUser.Width - margin
    'End Sub

    ''' <summary>
    ''' [Ξ]ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub toggleButton_Click(sender As Object, e As EventArgs)
        Dim pnlMenu = mainForm.SideMenuPanel
        If pnlMenu.Width > minWidth Then
            isCollapsing = False
            isExpanding = True
        Else
            isCollapsing = True
            isExpanding = False
        End If
        animationTimer.Start()
    End Sub

    ''' <summary>
    ''' サイドパネルの開閉処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AnimationTimer_Tick(sender As Object, e As EventArgs)
        Dim pnlMenu = mainForm.SideMenuPanel
        If isCollapsing Then
            pnlMenu.Width += animationStep
            If pnlMenu.Width >= sidePanelWidth Then
                pnlMenu.Width = sidePanelWidth
                animationTimer.Stop()
                isExpanding = False
                btnToggle.Text = "Ξ"
            End If
        ElseIf isExpanding Then
            pnlMenu.Width -= animationStep
            If pnlMenu.Width <= minWidth Then
                pnlMenu.Width = minWidth
                animationTimer.Stop()
                isCollapsing = False
                btnToggle.Text = "Ξ"
            End If
        End If
    End Sub

    ''' <summary>
    ''' [最大化]ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MaximizeWindow(sender As Object, e As EventArgs)
        ' 最大化中状態
        If isPseudoMaximized Then
            ' 元のサイズに戻す
            mainForm.Bounds = normalBounds
            ' 非最大化中
            isPseudoMaximized = False
            Return
        End If

        ' 最大化前の位置とサイズを保存
        normalBounds = mainForm.Bounds

        ' 擬似最大化（画面いっぱいにする）
        Dim wa = Screen.GetWorkingArea(mainForm)
        mainForm.Bounds = wa

        ' 最大化中状態
        isPseudoMaximized = True
    End Sub

    ''' <summary>
    ''' タイトルバーダブルクリックイベント
    ''' 
    ''' ※[最大化]ボタン押下と同様の動作
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TitleBar_DoubleClick(sender As Object, e As EventArgs)
        MaximizeWindow(sender, e)
    End Sub

    ''' <summary>
    ''' タイトルバーパネルの背景色
    ''' </summary>
    ''' <returns></returns>
    Private Function BackcolorWithTitleBar()
        If mainForm.IsProdEnv Then
            Return Color.DarkSlateGray
        End If
        Return Color.DarkBlue
    End Function


    Public Sub ToggleSideMenu()
        btnToggle.PerformClick()
    End Sub
End Class
