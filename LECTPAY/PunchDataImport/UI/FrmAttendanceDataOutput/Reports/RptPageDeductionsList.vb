''' <summary>
''' 手当控除明細シートをEXCEL出力するレポートページクラス。
''' </summary>
Public Class RptPageDeductionsList
    Inherits VBReportPage

#Region "[定数]"

    ' ページタイトル名
    Private Const REPORT_PAGE_NAME As String = "手当控除明細"

    ' ページタイトルの変数名
    Private Const REPORT_SHEET_NAME As String = "手当控除明細"

    ' ページタイトル出力項目の変数
    Private Const REPORT_CELL_TITLE As String = "**TITLE"


    ' 明細行部：出力項目変数
    Private Const FMT_CELL_TEACHER_CODE As String = "**Teacher_Code"
    Private Const FMT_CELL_TEACHER_NAME As String = "**Teacher_Name"
    Private Const FMT_CELL_ALLOWANCE_DEDUCTION_NAME As String = "**Allowance_Deduction_Name"
    Private Const FMT_CELL_ALLOWANCE_DEDUCTION_TYPE_NAME As String = "**Allowance_Deduction_Type_Name"
    Private Const FMT_CELL_ACCOUNT_NAME As String = "**Account_Name"
    Private Const FMT_CELL_AMOUNT As String = "**Amount"

    ' 明細行部：基本セル書式参照セル
    Private Const ATTR_CELL_TEACHER_CODE As String = "A2"
    Private Const ATTR_CELL_TEACHER_NAME As String = "B2"
    Private Const ATTR_CELL_ALLOWANCE_DEDUCTION_NAME As String = "C2"
    Private Const ATTR_CELL_ALLOWANCE_DEDUCTION_TYPE_NAME As String = "D2"
    Private Const ATTR_CELL_ACCOUNT_NAME As String = "E2"
    Private Const ATTR_CELL_AMOUNT As String = "F2"

    Private Const SQL_COL_TEACHER_CODE As String = "Teacher_Code"
    Private Const SQL_COL_TEACHER_NAME As String = "Teacher_Name"
    Private Const SQL_COL_ALLOWANCE_DEDUCTION_NAME As String = "Allowance_Deduction_Name"
    Private Const SQL_COL_ALLOWANCE_DEDUCTION_TYPE_NAME As String = "Allowance_Deduction_Type_Name"
    Private Const SQL_COL_ACCOUNT_NAME As String = "Account_Name"
    Private Const SQL_COL_AMOUNT As String = "Amount"

    ' 一覧の先頭セル(列)
    Private Const CELL_COL_OFFSET As Integer = 17

    ' 一覧の先頭セル(行)
    Private Const CELL_ROW_OFFSET As Integer = 6

#End Region

#Region "[プロパティ]"

    ''' <summary>
    ''' ページタイトルセル
    ''' </summary>
    ''' <returns></returns>
    Protected Overrides ReadOnly Property PageTitleCell As String
        Get
            Return REPORT_CELL_TITLE
        End Get
    End Property

    ''' <summary>
    ''' ページタイトル名
    ''' </summary>
    ''' <returns></returns>
    Protected Overrides ReadOnly Property PageTitleName As String
        Get
            Return REPORT_PAGE_NAME
        End Get
    End Property

    ''' <summary>
    ''' シート名
    ''' </summary>
    ''' <returns></returns>
    Protected Overrides ReadOnly Property PageSheetName As String
        Get
            Return REPORT_SHEET_NAME
        End Get
    End Property

#End Region

    ' 締め状態
    Private _closingState As MonthlyClosingMode

#Region "[メソッド]"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="param"></param>
    Public Sub New(ByVal param As DaoParameter)
        MyBase.New(param.TargetDate, Nothing)
    End Sub

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="targetDate"></param>
    ''' <param name="reportSourceDataTable"></param>
    Public Sub New(ByVal targetDate As Date, ByVal reportSourceDataTable As DataTable, ByVal closingState As MonthlyClosingMode)
        MyBase.New(targetDate, reportSourceDataTable)
        Me._closingState = closingState
    End Sub

    ''' <summary>
    ''' 初期設定
    ''' </summary>
    Protected Overrides Sub InitialSetup()
        m_PageName = String.Format("{0}_{1}", DateUtil.DateToFormatString(TargetDate), REPORT_PAGE_NAME)
        m_SheetName = REPORT_SHEET_NAME
    End Sub

    ''' <summary>
    ''' ページタイトル出力
    ''' </summary>
    Protected Overrides Sub PageTitle()
        ' ページタイトルは無いので何もしない。
    End Sub

    ''' <summary>
    ''' ページデータ出力
    ''' </summary>
    Public Overrides Sub PageOutput()
        OutputDetail(ReportSourceDataTable)
    End Sub

    ''' <summary>
    ''' 明細出力
    ''' </summary>
    ''' <param name="dtTable"></param>
    Public Overrides Sub OutputDetail(ByVal dtTable As DataTable)
        Dim cellUtil As New CellReportUtil(m_CellReport)
        Dim rowNo As Integer = 0
        Dim headerCompleted As Boolean = False

        ' 日別勤怠一覧
        For Each row As DataRow In dtTable.Rows

            ' 明細部
            With m_CellReport
                cellUtil.CellValue(FMT_CELL_TEACHER_CODE, 0, rowNo, GetValue(row(SQL_COL_TEACHER_CODE)), ATTR_CELL_TEACHER_CODE)
                cellUtil.CellValue(FMT_CELL_TEACHER_NAME, 0, rowNo, GetValue(row(SQL_COL_TEACHER_NAME)), ATTR_CELL_TEACHER_NAME)
                cellUtil.CellValue(FMT_CELL_ALLOWANCE_DEDUCTION_NAME, 0, rowNo, GetValue(row(SQL_COL_ALLOWANCE_DEDUCTION_NAME)), ATTR_CELL_ALLOWANCE_DEDUCTION_NAME)
                cellUtil.CellValue(FMT_CELL_ALLOWANCE_DEDUCTION_TYPE_NAME, 0, rowNo, GetValue(row(SQL_COL_ALLOWANCE_DEDUCTION_TYPE_NAME)), ATTR_CELL_ALLOWANCE_DEDUCTION_TYPE_NAME)
                cellUtil.CellValue(FMT_CELL_ACCOUNT_NAME, 0, rowNo, GetValue(row(SQL_COL_ACCOUNT_NAME)), ATTR_CELL_ACCOUNT_NAME)
                cellUtil.CellValue(FMT_CELL_AMOUNT, 0, rowNo, GetValue(row(SQL_COL_AMOUNT)), ATTR_CELL_AMOUNT)
                rowNo = rowNo + 1
            End With
        Next

    End Sub

#End Region

End Class
