Imports AdvanceSoftware.VBReport8

Public MustInherit Class VBReport
    Implements IVBReport

    ''' <summary>
    ''' DataBse接続文字列
    ''' </summary>
    Protected m_ConnectString As String = SqlDataBaseUtil.GetConnectionString()

    ''' <summary>
    ''' VB-Reports CellReportオブジェクト
    ''' </summary>
    Protected m_CellReport As CellReport

    ''' <summary>
    ''' VB-Reports CellReportオブジェクト
    ''' </summary>
    Protected m_ViewerControl As ViewerControl

    ''' <summary>
    ''' 出力対象レポートページオブジェクト
    ''' </summary>
    Private pageList As New List(Of IVBReportPage)

    ''' <summary>
    ''' 出力対象レポートページオブジェクト
    ''' </summary>
    Protected m_templateBook As String = "LECTUREFEE_TEMPLATE.xlsx"

    ''' <summary>
    ''' 対象年月度
    ''' </summary>
    Protected Property TargetDate As Date

    ''' <summary>
    ''' 出力ファイルパス名
    ''' </summary>
    Protected Property OutputFilePath As String

    ''' <summary>
    ''' 出力ファイルタイプ
    ''' </summary>
    Protected Property FileType As String


    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="targetDate">対象年月度</param>
    Public Sub New(ByVal targetDate As Date, ByVal filePath As String, Optional ByVal fileType As String = "EXCEL")
        Me.TargetDate = targetDate
        Me.OutputFilePath = filePath
        Me.FileType = fileType
    End Sub

    ''' <summary>
    ''' レポート出力
    ''' </summary>
    Public Sub Report() Implements IVBReport.Report
        ' 初期設定
        InitialSetup()

        ' レポート出力開始
        ReportStart()

        ' レポートページ出力
        OutputPage()

        ' レポート出力終了
        ReportEnd()

        ' ファイル保存
        SaveAs()
    End Sub

    ''' <summary>
    ''' 環境設定
    ''' </summary>
    ''' <param name="report"></param>
    ''' <param name="viewer"></param>
    Protected Overridable Sub VbReportsEnvSetting(ByVal report As CellReport, ByVal viewer As ViewerControl) Implements IVBReport.VbReportsEnvSetting
        m_CellReport = report
        m_ViewerControl = viewer
    End Sub

    ''' <summary>
    ''' 初期設定
    ''' </summary>
    Private Sub InitialSetup() Implements IVBReport.InitialSetup
        m_CellReport.FileName = m_templateBook
        m_CellReport.ExcelMode = False
    End Sub

    ''' <summary>
    ''' レポート出力開始
    ''' </summary>
    Private Sub ReportStart() Implements IVBReport.ReportStart
        Try
            m_ViewerControl.Clear()
            m_CellReport.Report.Start()
            m_CellReport.Report.File()
        Catch ex As Exception
            GlobalLog.Error("VB-Reportsレポート開始処理でエラーが発生しました", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' レポート出力終了
    ''' </summary>
    Private Sub ReportEnd() Implements IVBReport.ReportEnd
        Try
            m_CellReport.Report.End()
        Catch ex As Exception
            GlobalLog.Error("VB-Reportsレポート終了処理でエラーが発生しました", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' レポートページオブジェクト追加
    ''' </summary>
    ''' <param name="reportPage"></param>
    Protected Sub AddPage(ByVal reportPage As IVBReportPage) Implements IVBReport.AddPage
        pageList.Add(reportPage)
    End Sub

    ''' <summary>
    ''' レポートページオブジェクト格納件数
    ''' </summary>
    ''' <returns></returns>
    Private Function PageCount() As Integer
        Return pageList.Count
    End Function

    ''' <summary>
    ''' ファイル保存
    ''' </summary>
    Private Sub SaveAs()
        Try
            m_ViewerControl.Document = m_CellReport.Document

            If FileType = "PDF" Then
                ' PDF出力
                m_ViewerControl.SavePDF(OutputFilePath)
            Else
                ' EXCEL出力
                m_ViewerControl.SaveAs(OutputFilePath, AdvanceSoftware.VBReport8.ExcelVersion.ver2016)
            End If

            pageList.Clear()
        Catch ex As Exception
            GlobalLog.Error("VB-Reports ファイル保存処理でエラーが発生しました" & " => [" & OutputFilePath & "]", ex)
            Throw
        End Try

    End Sub

    ''' <summary>
    ''' ページ出力
    ''' </summary>
    Private Sub OutputPage()
        Try
            For pageIndex As Integer = 0 To pageList.Count - 1
                pageList(pageIndex).VbReportsEnvSetting(m_CellReport, TargetDate)
                pageList(pageIndex).PageReport()
            Next
        Catch ex As Exception
            GlobalLog.Error("VB-Reports ページ出力処理でエラーが発生しました", ex)
            Throw
        End Try
    End Sub
End Class
