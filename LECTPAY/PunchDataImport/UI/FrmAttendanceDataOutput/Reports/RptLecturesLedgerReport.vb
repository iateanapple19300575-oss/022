''' <summary>
''' 出講台帳をEXCEL出力するレポートクラス
''' </summary>
Public Class RptLecturesLedgerReport
    Inherits VBReport
    Implements IReportExporter

    ''' <summary>出力先フォルダ</summary>
    Private FolderPath As String = ""

    ''' <summary>締め状態</summary>
    Private _closingState As MonthlyClosingMode

    ' 進捗を通知するためのカスタムイベント（現在の件数, 総件数）
    Public Event ProgressChanged(ByVal current As Integer, ByVal total As Integer, ByVal statusMessage As String) Implements IReportExporter.ProgressChanged

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="targetDate"></param>
    Public Sub New(ByVal targetDate As Date, ByVal filePath As String, ByVal fileType As String, ByVal closingState As MonthlyClosingMode)
        MyBase.New(targetDate, filePath, fileType)
        Me.OutputFilePath = filePath
        Me.FolderPath = filePath
        Me._closingState = closingState
    End Sub

    ''' <summary>
    ''' 処理中画面から呼び出される処理です。
    ''' </summary>
    ''' <param name="cancelRequested"></param>
    Public Sub ExportReports(ByRef cancelRequested As Boolean) Implements IReportExporter.ExportReports
        Output(cancelRequested)
    End Sub

    ''' <summary>
    ''' 出講台帳出力
    ''' </summary>
    Public Sub Output(ByRef cancelRequested As Boolean)
        Dim frmVBReportsViewer As New frmVBReportsViewer

        Try
            VbReportsEnvSetting(frmVBReportsViewer.CellReport1, frmVBReportsViewer.ViewerControl1)
            frmVBReportsViewer.ViewerControl1.ShowProgressDialog = False
            Dim totalCount As Integer = 1
            Dim counter As Integer = 0

            Using uow As New DbTransactionScope()
                Dim repo As New PaySlipHeaderRepository(uow, _closingState)
                Dim yearMonthStr As String = DateUtil.DateToFormatString(TargetDate, "yyyyMM")
                counter += 1
                RaiseEvent ProgressChanged(counter, totalCount, "出講台帳データをEXCELに出力しています...")

                If cancelRequested Then
                    Return  ' 中断
                End If

                ' 出講台帳情報取得
                Dim ledgerDt As DataTable = repo.LectureAttendanceLedger(yearMonthStr)

                ' 手当控除明細情報
                Dim lecturesALLDt As DataTable = repo.GetAllowanceDeductionDetails(yearMonthStr)

                ' 出講台帳ページ
                AddPage(New RptPageLedgerList(TargetDate, ledgerDt, _closingState))

                ' 手当控除明細ページ
                AddPage(New RptPageDeductionsList(TargetDate, lecturesALLDt, _closingState))

                ' レポート出力
                Report()

                RaiseEvent ProgressChanged(counter, totalCount, "出講台帳データをEXCELに出力しています...")
                Application.DoEvents()
                System.Threading.Thread.Sleep(10)
            End Using

            '' レポート出力
            'Report()

            ' Viewerは表示しない
            'frmVBReportsViewer.ShowDialog()

            ' リソースの解放
            frmVBReportsViewer.Dispose()

        Catch ex As Exception
            GlobalLog.Error(MessageIdConst.MSGID_E2014, ex)
            Throw
        End Try
    End Sub
End Class
