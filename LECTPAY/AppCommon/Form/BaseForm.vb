Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

''' <summary>
''' 通常画面共通の基底フォーム。
''' </summary>
Public Class BaseForm
    Inherits System.Windows.Forms.Form


    Protected Message As String = ""

    ' 初期表示処理中判定フラグ
    Protected Property IsImportComplete = False

#Region "プロパティ関連"

    ' API宣言：キーの物理的な押下状態を取得
    <DllImport("user32.dll", CharSet:=CharSet.Auto, ExactSpelling:=True)>
    Private Shared Function GetAsyncKeyState(ByVal vKey As Integer) As Short
    End Function

    ' 仮想キーコード (Virtual Key Codes)
    Private Const VK_D As Integer = &H44 ' D キー
    Private Const VK_Q As Integer = &H51 ' Q キー

    ' サブクラスに通知するための独自イベントを定義
    Protected Event StartDebugMode(ByVal sender As Object, ByVal e As EventArgs) ' Shift + Ctrl + Alt + D
    Protected Event EndDebugMode(ByVal sender As Object, ByVal e As EventArgs)   ' Shift + Ctrl + Alt + Q

    ' サブクラスに通知するための独自イベント
    Protected Event FormCaptionDoubleClick(ByVal sender As Object, ByVal e As EventArgs)

    ' Windowsメッセージ定数
    Private Const WM_NCLBUTTONDBLCLK As Integer = &HA3 ' 非クライアント領域（タイトルバー等）の左ダブルクリック
    Private Const HTCAPTION As Integer = 2             ' タイトルバー（キャプション）を示す位置コード

    ''' <summary>
    ''' Form 表示時ログメッセージ
    ''' </summary>
    Protected LOG_SHOW_FORM_MESSAGE As String = "画面表示"

    ''' <summary>
    ''' Form クローズ時ログメッセージ
    ''' </summary>
    Protected LOG_CLOSE_FORM_MESSAGE As String = "画面終了"

    ''' <summary>
    ''' 画面名
    ''' </summary>
    ''' <returns></returns>
    Protected Property WindowTitle As String = ""

    ''' <summary>
    ''' 起動処理完了フラグ
    ''' </summary>
    ''' <returns></returns>
    Protected Property IsStartUpComplete As Boolean = False

#End Region


#Region "イベントフック"

    ''' <summary>
    ''' フォーム全体のメッセージループを監視し、タイトルバーのダブルクリックをフックする
    ''' </summary>
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)

        ' タイトルバー（非クライアント領域）が左ダブルクリックされたか判定
        If m.Msg = WM_NCLBUTTONDBLCLK AndAlso m.WParam.ToInt32() = HTCAPTION Then
            RaiseEvent FormCaptionDoubleClick(Me, EventArgs.Empty)
        End If

        ' 条件に一致しない場合は、通常通りWindowsの標準処理に流す
        MyBase.WndProc(m)
    End Sub

#End Region


#Region "オペレーションサポート関連"

    ''' <summary>
    ''' 各キーの同時押し状態、および左ダブルクリックであることを厳密にチェックしてイベントを発生させる
    ''' </summary>
    ''' <param name="e">ダブルクリックイベントから渡されるマウス情報</param>
    Protected Sub CheckSpecialKeysAndRaiseEvent(ByVal e As System.Windows.Forms.MouseEventArgs)

        ' 左ダブルクリック以外は処理しない
        If e.Button <> System.Windows.Forms.MouseButtons.Left Then
            Return
        End If

        ' Shift + Ctrl + Alt の同時押下以外は処理しない
        Dim targetModifiers As Keys = Keys.Shift Or Keys.Control Or Keys.Alt
        Dim isModifiersPressed As Boolean = ((Control.ModifierKeys And targetModifiers) = targetModifiers)
        If Not isModifiersPressed Then
            Return
        End If

        ' D キー と C キー の押下状態を判定 (最上位ビットが1なら押下中)
        Dim isDPressed As Boolean = ((GetAsyncKeyState(VK_D) And &H8001) <> 0)
        Dim isQPressed As Boolean = ((GetAsyncKeyState(VK_Q) And &H8001) <> 0)

        ' 条件が完全に一致した時だけイベントを発生させる
        If isDPressed Then
            ' Shift + Ctrl + Alt + D + 左ダブルクリック で起動
            RaiseEvent StartDebugMode(Me, EventArgs.Empty)
        ElseIf isQPressed Then
            ' Shift + Ctrl + Alt + Q + 左ダブルクリック で終了
            RaiseEvent EndDebugMode(Me, EventArgs.Empty)
        End If

    End Sub

    ''' <summary>
    ''' ENTERキー押下：次の項目へ移動
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub BaseForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' 次のコントロールへフォーカス移動
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
            ' イベントを処理済みにする
            e.Handled = True
        End If
    End Sub

#End Region


#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
        MyBase.New()

        IsStartUpComplete = False
    End Sub

    ''' <summary>
    ''' デザイナー実行中かを判定します。
    ''' </summary>
    Protected Function IsInDesignMode() As Boolean
        Return (Me.DesignMode) OrElse
               (Me.Site IsNot Nothing AndAlso Me.Site.DesignMode)
    End Function

#End Region


#Region "画面表示関連：表示／終了出力"

    ''' <summary>
    ''' OnLoadイベントログ
    ''' </summary>
    ''' <param name="e"></param>
    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        AddHandler StateDispatcher.UiStateChanged, AddressOf OnUiStateChanged

        ' 画面初期表示位置座標（0,0）設定
        Dim currentX As Integer = IIf(Me.Location.X < 0, 0, Me.Location.X)
        Dim currentY As Integer = IIf(Me.Location.Y < 0, 0, Me.Location.Y)
        Me.Location = New Point(currentX, currentY)
    End Sub

    ''' <summary>
    ''' フォームクローズ時に購読解除（メモリリーク防止）。
    ''' </summary>
    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        RemoveHandler StateDispatcher.UiStateChanged, AddressOf OnUiStateChanged
        MyBase.OnFormClosed(e)
    End Sub

    ''' <summary>
    ''' GlobalState の変更を UI スレッドで受け取る。
    ''' 派生フォームで UI 更新処理を実装する。
    ''' </summary>
    Public Overridable Sub OnUiStateChanged(key As String, value As Object)
        ' 派生フォームで実装
    End Sub

    ''' <summary>
    ''' Shownイベント
    ''' 
    ''' 画面表示ログ出力します。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Overridable Sub BaseForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim formName As String = Me.GetType().Name
        Dim formTitle As String = Me.Text

        ' ファイルログ出力
        GlobalLog.Info(String.Format("[{0}]画面({1}):表示", formTitle, formName))

        ' 画面表示開始のログ出力
        LogService.ManualLogWriting(Me.Text, LOG_SHOW_FORM_MESSAGE, "")

        ' 起動完了フラグ = ON
        IsStartUpComplete = True
    End Sub

    ''' <summary>
    ''' Closed イベント
    ''' 
    ''' 画面終了ログ出力します。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Overridable Sub BaseForm_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Dim formName As String = Me.GetType().Name
        Dim formTitle As String = Me.Text

        ' ファイルログ出力
        GlobalLog.Info(String.Format("[{0}]画面({1}):終了", formTitle, formName))

        ' 画面表示終了のログ出力
        LogService.ManualLogWriting(Me.Text, LOG_CLOSE_FORM_MESSAGE, "")
    End Sub

    ''' <summary>
    ''' 画面が閉じられる直前の共通処理
    ''' ALT + F4 で画面、アプリが終了しないようにします。
    ''' </summary>
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)

        ' 右上の×ボタン、または Alt + F4 で閉じられようとしている場合
        If e.CloseReason = CloseReason.UserClosing Then

            ' Alt + F4 が押されたかどうかを判定してブロックする
            ' (ModifierKeys が Alt かつ、F4 キーが押されている場合)
            If (Control.ModifierKeys And Keys.Alt) = Keys.Alt AndAlso
               (Control.MouseButtons = MouseButtons.None) Then

                ' 閉じる処理をキャンセルする
                e.Cancel = True
                Exit Sub
            End If
        End If

        ' 本来の閉じる処理を実行
        MyBase.OnFormClosing(e)
    End Sub

    ''' <summary>
    ''' メインメニューボタンイベントログ出力
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub MainMenuButtonClick(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim message As String = String.Format("Main Menu：[{0}] ボタン押下", btn.Text)
        LogService.ManualLogWriting(Me.Text, message, "")
    End Sub

    ''' <summary>
    ''' サブメニューボタンイベントログ出力
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub SubMenuButtonClick(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim message As String = String.Format("Sub Menu：[{0}] ボタン押下", btn.Text)
        Dim detail As String = "[" & btn.Text & "] 画面起動"

        LogService.ManualLogWriting(Me.Text, message, detail)
    End Sub

#End Region


    ''' <summary>
    ''' バリデーションエラーメッセージ表示
    ''' </summary>
    Public Overridable Sub ShowMessageErrors(ByVal result As ResultBase)
        MessageBox.Show(String.Join(Environment.NewLine, result.ValidationErrors.ToArray))
    End Sub

    ''' <summary>
    ''' 処理エラーメッセージ表示
    ''' </summary>
    ''' <param name="result"></param>
    Public Overridable Sub ShowMessageError(ByVal result As ResultBase)
        MessageBox.Show(result.StatusMessage)
    End Sub

    ''' <summary>
    ''' 処理エラーメッセージ表示
    ''' </summary>
    ''' <param name="result"></param>
    Public Overridable Sub ShowMessageResult(ByVal result As ResultBase)
        MessageBox.Show(result.StatusMessage)
    End Sub

    ''' <summary>
    ''' 処理エラーメッセージ表示
    ''' </summary>
    ''' <param name="message"></param>
    Public Overridable Sub ShowMessageError(ByVal message As String, Optional ByVal caption As String = "エラー")
        MessageBox.Show(Me, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    ''' <summary>
    ''' 警告メッセージ表示
    ''' </summary>
    ''' <param name="message"></param>
    Public Overridable Sub ShowMessageWarning(ByVal message As String, Optional ByVal caption As String = "警告")
        MessageBox.Show(Me, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    ''' <summary>
    ''' 処理エラーメッセージ表示
    ''' </summary>
    ''' <param name="message"></param>
    Public Overridable Sub ShowMessageResult(ByVal message As String, Optional ByVal caption As String = "情報")
        MessageBox.Show(Me, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

End Class
