Imports AdvanceSoftware.VBReport8

Public MustInherit Class VBReportPage2
    Implements IVBReportPage2

    ''' <summary>
    ''' ページタイトルセル変数
    ''' </summary>
    ''' <returns></returns>
    Protected MustOverride ReadOnly Property PageTitleCell As String

    ''' <summary>
    ''' ページタイトル
    ''' </summary>
    ''' <returns></returns>
    Protected MustOverride ReadOnly Property PageTitleName As String

    ''' <summary>
    ''' シート名
    ''' </summary>
    ''' <returns></returns>
    Protected MustOverride ReadOnly Property PageSheetName As String()

    ''' <summary>
    ''' ページ名
    ''' </summary>
    Protected m_PageName As String = ""

    ''' <summary>
    ''' シート名
    ''' </summary>
    Protected m_SheetName As String = ""

    ''' <summary>
    ''' DataBse接続文字列
    ''' </summary>
    Private m_ConnectString As String = SqlDataBaseUtil.GetConnectionString()

    ''' <summary>
    ''' VB-Reports CellReportオブジェクト
    ''' </summary>
    Protected m_CellReport As CellReport

    ''' <summary>
    ''' 対象年月度
    ''' </summary>
    Protected Property TargetDate As Date

    ''' <summary>
    ''' 出力データ
    ''' </summary>
    Protected Property ReportSourceDataTable As DataTable

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="targetDate"></param>
    Public Sub New(ByVal targetDate As Date)
        Me.TargetDate = targetDate
    End Sub

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="targetDate"></param>
    Public Sub New(ByVal targetDate As Date, ByVal reportDataSource As DataTable)
        Me.TargetDate = targetDate
        Me.ReportSourceDataTable = reportDataSource
    End Sub

    ''' <summary>
    ''' 環境設定
    ''' </summary>
    ''' <param name="report"></param>
    ''' <param name="tagetDate"></param>
    Private Sub VbReportsEnvSetting(ByVal report As CellReport, ByVal tagetDate As Date) Implements IVBReportPage2.VbReportsEnvSetting
        m_CellReport = report
        Me.TargetDate = tagetDate
    End Sub

    ''' <summary>
    ''' 初期設定（１頁目）
    ''' </summary>
    Protected Overridable Sub InitialSetup() Implements IVBReportPage2.InitialSetup
        m_PageName = String.Format(PageTitleName, Year(Me.TargetDate), Month(Me.TargetDate))
        m_SheetName = PageSheetName(0)
    End Sub

    ''' <summary>
    ''' 初期設定（２頁目）
    ''' </summary>
    Protected Overridable Sub Page2Setup() Implements IVBReportPage2.Page2Setup
        'm_PageName = String.Format(PageTitleName, Year(Me.TargetDate), Month(Me.TargetDate))
        m_SheetName = PageSheetName(1)
    End Sub

    ''' <summary>
    ''' ページ処出力理開始
    ''' </summary>
    Protected Overridable Sub PageStart() Implements IVBReportPage2.PageStart
        Dim outPageCount As String = "1-999999"
        m_CellReport.Page.Start(m_SheetName, outPageCount)
        If Not String.IsNullOrEmpty(m_PageName) Then
            m_CellReport.Page.Name = m_PageName
        End If
    End Sub

    ''' <summary>
    ''' ページ出力処理終了
    ''' </summary>
    Protected Overridable Sub PageEnd() Implements IVBReportPage2.PageEnd
        m_CellReport.Page.End()
    End Sub

    Protected Overridable Sub PageTitle() Implements IVBReportPage2.PageTitle
        If Not String.IsNullOrEmpty(m_PageName) Then
            m_CellReport.Cell(PageTitleCell, 0, 0).Value = m_PageName
        End If
    End Sub

    Protected Overridable Sub PageTitle(ByVal cell As String) Implements IVBReportPage2.PageTitle
        If Not String.IsNullOrEmpty(m_PageName) Then
            m_CellReport.Cell(cell, 0, 0).Value = m_PageName
        End If
    End Sub

    Protected Overridable Sub PageTitle(ByVal x As Integer, ByVal y As Integer, Optional ByVal offset As Integer = 0) Implements IVBReportPage2.PageTitle
        If Not String.IsNullOrEmpty(m_PageName) Then
            m_CellReport.Cell(x, y, offset).Value = m_PageName
        End If
    End Sub

    ''' <summary>
    ''' ページ出力
    ''' </summary>
    Protected Overridable Sub PageReport() Implements IVBReportPage2.PageReport

        ' 初期設定
        InitialSetup()

        ' ページ出力開始
        PageStart()

        ' ページタイトル出力
        PageTitle()

        ' ページデータ出力
        PageOutput()

        ' ページ出力終了
        PageEnd()

    End Sub

    ''' <summary>
    ''' RowData値(value)値を取得します。
    ''' DBNullの時は、空白("")に変換します。
    ''' ※指定値は、RowData値を想定。
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Protected Shared Function GetValue(ByVal value As Object) As Object
        If Not IsDBNull(value) Then
            Return value
        Else
            Return ""
        End If
    End Function

    ''' <summary>
    ''' Decimal値を指定フォーマットの文字列値に変換します。
    ''' DBNullの時は、空白("")に変換します。
    ''' ※指定値は、RowData値を想定。
    ''' 
    ''' fmtで、小数値のフォーマットを指定します。
    ''' 例:"0.0"、"0.00"、"0.000"
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="fmt">小数桁のフォーマット</param>
    ''' <returns></returns>
    Public Shared Function GetDecimalValue(ByVal value As Object, ByVal fmt As String) As String
        If Not IsDBNull(value) Then
            Return CDec(value).ToString(fmt)
        End If
        Return ""
    End Function

    ''' <summary>
    ''' レポートページデータ出力
    ''' </summary>
    Public MustOverride Sub PageOutput() Implements IVBReportPage2.PageOutput

    ''' <summary>
    ''' 明細出力
    ''' </summary>
    ''' <param name="reader"></param>
    Public MustOverride Sub OutputDetail(ByVal reader As DataTable) Implements IVBReportPage2.OutputDetail

End Class
