Public Class ProgressForm

    ' スレッド間で安全にデータを受け渡すための構造体
    Private Structure ProgressPayload
        Public Current As Integer
        Public Total As Integer
        Public Message As String
    End Structure

    Private _exporter As IReportExporter
    Private WithEvents bgWorker As New System.ComponentModel.BackgroundWorker()
    ' 別クラスへ「キャンセル状態」を伝達するための変数
    Private _isCancelled As Boolean = False

    Public Property ErrorException As Exception = Nothing

    ' コンストラクタ（どの帳票クラスでもインターフェースとして受け取る）
    Public Sub New(ByVal exporter As IReportExporter)
        InitializeComponent()
        _exporter = exporter
        bgWorker.WorkerReportsProgress = True
        bgWorker.WorkerSupportsCancellation = True
    End Sub

    ' 画面が表示されたら自動で処理スタート
    Private Sub ProgressForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        bgWorker.RunWorkerAsync()
    End Sub

    ' 【別スレッド】帳票処理を実行し、イベントを画面へ中継する
    Private Sub bgWorker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgWorker.DoWork
        ' ラムダ式で各帳票のProgressChangedイベントをキャッチし、bgWorker経由で画面に投げる
        AddHandler _exporter.ProgressChanged,
            Sub(current As Integer, total As Integer, msg As String)
                Dim payload As New ProgressPayload With {.Current = current, .Total = total, .Message = msg}
                bgWorker.ReportProgress(0, payload)
            End Sub

        ' 帳票クラスのメイン処理を別スレッドで実行
        ' 画面側で管理している「_isCancelled」フラグをByRefで渡して
        ' 別クラスのループ内でいつでもこのフラグを監視できるようにする。
        _exporter.ExportReports(_isCancelled)

        ' 帳票側がループを抜けて戻ってきたとき、キャンセルボタンが押されていた場合
        If bgWorker.CancellationPending Then
            ' BackgroundWorkerに「キャンセルで終わった」と教える
            e.Cancel = True
        End If
    End Sub

    ' 【メインスレッド】中継された情報で画面コントロールを安全に更新
    Private Sub bgWorker_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bgWorker.ProgressChanged
        ' 中止処理中（CancellationPendingがTrue）なら、進捗の画面更新を無視する
        If bgWorker.CancellationPending Then
            Exit Sub
        End If

        If TypeOf e.UserState Is ProgressPayload Then
            Dim payload As ProgressPayload = DirectCast(e.UserState, ProgressPayload)

            lblMessage.Text = payload.Message
            lblCount.Text = String.Format("{0} / {1} 件", payload.Current, payload.Total)

            ProgressBar1.Maximum = payload.Total
            ProgressBar1.Value = payload.Current

            Me.Refresh()
        End If
    End Sub

    ' 【メインスレッド】処理完了時に画面を閉じる
    Private Sub bgWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgWorker.RunWorkerCompleted
        If e.Error IsNot Nothing Then
            Me.ErrorException = e.Error
            Me.DialogResult = System.Windows.Forms.DialogResult.Abort
        ElseIf e.Cancelled Then
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel ' 中止終了
        Else
            Me.DialogResult = System.Windows.Forms.DialogResult.OK     ' 正常終了
        End If
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ' ボタンを連打できないように無効化し、メッセージを変える
        btnCancel.Enabled = False
        lblMessage.Text = "処理を中止しています。しばらくお待ちください..."

        ' 別クラス側への中止シグナルをON
        _isCancelled = True

        ' BackgroundWorkerへの中止シグナルをON
        bgWorker.CancelAsync()
    End Sub
End Class
