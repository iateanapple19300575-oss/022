''' <summary>
''' 出講台帳シートをEXCEL出力するレポートページクラス。
''' </summary>
Public Class RptPageLedgerList
    Inherits VBReportPage

#Region "[定数]"

    ' ページタイトル名
    Private Const REPORT_PAGE_NAME As String = "出講台帳"

    ' ページタイトルの変数名
    Private Const REPORT_SHEET_NAME As String = "出講台帳"

    ' ページタイトル出力項目の変数
    Private Const REPORT_CELL_TITLE As String = "**TITLE"


    ' 明細行部：出力項目変数
    Private Const FMT_CELL_TEACHER_CODE As String = "**Teacher_Code"                              ' 講師コード
    Private Const FMT_CELL_TEACHER_NAME As String = "**Teacher_Name"                              ' 講師名
    Private Const FMT_CELL_LECTURE_UNIT_PRICE As String = "**Lecture_Unit_Price"                  ' 授業給単価
    Private Const FMT_CELL_TASK_UNIT_PRICE As String = "**Task_Unit_Price"                        ' 業務給単価
    Private Const FMT_CELL_WORKING_DAYS As String = "**Working_Days"                              ' 勤務日数
    Private Const FMT_CELL_LECTURE_KOMA_NUMBER As String = "**Lecture_Koma_Number"                ' 出講コマ数
    Private Const FMT_CELL_WORKING_MINUTES As String = "**Working_Minutes"                        ' 総勤務時間
    Private Const FMT_CELL_WORKING_MINUTES_DEEMED As String = "**Working_Minutes_Deemed"          ' 総勤務時間（みなし含む）
    Private Const FMT_CELL_MIDNIGHT_WORKING_MINUTES As String = "**Midnight_Working_Minutes"      ' 深夜・早朝勤務
    Private Const FMT_CELL_OVERTIME_WORKING_MINUTES As String = "**Overtime_Working_Minutes"      ' 残業時間
    Private Const FMT_CELL_LECTURE_WAGE_TOTAL As String = "**Lecture_Wage_Total"                  ' 授業給
    Private Const FMT_CELL_HOURLY_WAGE_TOTAL As String = "**Hourly_Wage_Total"                    ' 業務給
    Private Const FMT_CELL_ALLOWANCE_TOTAL As String = "**Allowance_Total"                        ' 手当
    Private Const FMT_CELL_ADJUSTMENT_SALARY_TOTAL As String = "**Adjustment_Salary_Total"        ' 調整給与
    Private Const FMT_CELL_SETTLEMENT_MONEY_TOTAL As String = "**Settlement_Money_Total"          ' 経費精算
    Private Const FMT_CELL_OVERTIME_WAGE_TOTAL As String = "**Overtime_Wage_Total"                ' 残業手当
    Private Const FMT_CELL_TRANSPO_TOTAL As String = "**Transpo_Total"                            ' 交通費
    Private Const FMT_CELL_ADJUSTMENT_TRANSP_TOTAL As String = "**Adjustment_Transp_Total"        ' 調整交通費
    Private Const FMT_CELL_PAYMENTS_TOTAL As String = "**Payments_Total"                          ' 総支給額
    Private Const FMT_CELL_DEDUCTION_TOTAL As String = "**Deduction_Total"                        ' 控除
    Private Const FMT_CELL_ADVANCES_PAID_TOTAL As String = "**Advances_Paid_Total"                ' 前渡金
    Private Const FMT_CELL_HEALTH_CHECK_TOTAL As String = "**Health_Check_Total"                  ' 健診・予防接種
    Private Const FMT_CELL_DEDUCTION_AMOUNT_TOTAL As String = "**Deduction_Amount_Total"            ' 総控除額
    Private Const FMT_CELL_PAYMENTS_DEDUCTION_TOTAL As String = "**Payments_Deduction_Total"      ' 差引支給額

    ' 明細行部：基本セル書式参照セル
    Private Const ATTR_CELL_TEACHER_CODE As String = "A2"                                         ' 講師コード
    Private Const ATTR_CELL_TEACHER_NAME As String = "B2"                                         ' 講師名
    Private Const ATTR_CELL_LECTURE_UNIT_PRICE As String = "C2"                                   ' 授業給単価
    Private Const ATTR_CELL_TASK_UNIT_PRICE As String = "D2"                                      ' 業務給単価
    Private Const ATTR_CELL_WORKING_DAYS As String = "E2"                                         ' 勤務日数
    Private Const ATTR_CELL_LECTURE_KOMA_NUMBER As String = "F2"                                  ' 出講コマ数
    Private Const ATTR_CELL_WORKING_MINUTES As String = "G2"                                      ' 総勤務時間
    Private Const ATTR_CELL_WORKING_MINUTES_DEEMED As String = "H2"                               ' 総勤務時間（みなし含む）
    Private Const ATTR_CELL_MIDNIGHT_WORKING_MINUTES As String = "I2"                             ' 深夜・早朝勤務
    Private Const ATTR_CELL_OVERTIME_WORKING_MINUTES As String = "J2"                             ' 残業時間
    Private Const ATTR_CELL_LECTURE_WAGE_TOTAL As String = "K2"                                   ' 授業給
    Private Const ATTR_CELL_HOURLY_WAGE_TOTAL As String = "L2"                                    ' 業務給
    Private Const ATTR_CELL_ALLOWANCE_TOTAL As String = "M2"                                      ' 手当
    Private Const ATTR_CELL_ADJUSTMENT_SALARY_TOTAL As String = "N2"                              ' 調整給与
    Private Const ATTR_CELL_SETTLEMENT_MONEY_TOTAL As String = "O2"                               ' 経費精算
    Private Const ATTR_CELL_OVERTIME_WAGE_TOTAL As String = "P2"                                  ' 残業手当
    Private Const ATTR_CELL_TRANSPO_TOTAL As String = "Q2"                                        ' 交通費
    Private Const ATTR_CELL_ADJUSTMENT_TRANSP_TOTAL As String = "R2"                              ' 調整交通費
    Private Const ATTR_CELL_PAYMENTS_TOTAL As String = "S2"                                       ' 総支給額
    Private Const ATTR_CELL_DEDUCTION_TOTAL As String = "T2"                                      ' 控除
    Private Const ATTR_CELL_ADVANCES_PAID_TOTAL As String = "U2"                                  ' 前渡金
    Private Const ATTR_CELL_HEALTH_CHECK_TOTAL As String = "V2"                                   ' 健診・予防接種
    Private Const ATTR_CELL_DEDUCTION_AMOUNT_TOTAL As String = "W2"                               ' 総控除額
    Private Const ATTR_CELL_PAYMENTS_DEDUCTION_TOTAL As String = "X2"                             ' 差引支給額

    ' SQLカラム
    Private Const SQL_COL_TEACHER_CODE As String = "Teacher_Code"                                 ' 講師コード
    Private Const SQL_COL_TEACHER_NAME As String = "Teacher_Name"                                 ' 講師名
    Private Const SQL_COL_LECTURE_UNIT_PRICE As String = "Lecture_Unit_Price"                     ' 授業給単価
    Private Const SQL_COL_TASK_UNIT_PRICE As String = "Task_Unit_Price"                           ' 業務給単価
    Private Const SQL_COL_WORKING_DAYS As String = "Working_Days"                                 ' 勤務日数
    Private Const SQL_COL_LECTURE_KOMA_NUMBER As String = "Lecture_Koma_Number"                   ' 出講コマ数
    Private Const SQL_COL_WORKING_MINUTES As String = "Working_Minutes_Hour"                      ' 総勤務時間
    Private Const SQL_COL_WORKING_MINUTES_DEEMED As String = "Working_Minutes_Deemed_Hour"        ' 総勤務時間（みなし含む）
    Private Const SQL_COL_MIDNIGHT_WORKING_MINUTES As String = "Midnight_Working_Minutes_Hour"    ' 深夜・早朝勤務
    Private Const SQL_COL_OVERTIME_WORKING_MINUTES As String = "Overtime_Working_Minutes_Hour"    ' 残業時間
    Private Const SQL_COL_LECTURE_WAGE_TOTAL As String = "Lecture_Wage_Total"                     ' 授業給
    Private Const SQL_COL_HOURLY_WAGE_TOTAL As String = "Hourly_Wage_Total"                       ' 業務給
    Private Const SQL_COL_ALLOWANCE_TOTAL As String = "Allowance_Total"                           ' 手当
    Private Const SQL_COL_ADJUSTMENT_SALARY_TOTAL As String = "Adjustment_Salary_Total"           ' 調整給与
    Private Const SQL_COL_SETTLEMENT_MONEY_TOTAL As String = "Settlement_Money_Total"             ' 経費精算
    Private Const SQL_COL_OVERTIME_WAGE_TOTAL As String = "Overtime_Wage_Total"                   ' 残業手当
    Private Const SQL_COL_TRANSPO_TOTAL As String = "Transpo_Total"                               ' 交通費
    Private Const SQL_COL_ADJUSTMENT_TRANSP_TOTAL As String = "Adjustment_Transp_Total"           ' 調整交通費
    Private Const SQL_COL_PAYMENTS_TOTAL As String = "Payments_Total"                             ' 総支給額
    Private Const SQL_COL_DEDUCTION_TOTAL As String = "Deduction_Total"                           ' 控除
    Private Const SQL_COL_ADVANCES_PAID_TOTAL As String = "Advances_Paid_Total"                   ' 前渡金
    Private Const SQL_COL_HEALTH_CHECK_TOTAL As String = "Health_Check_Total"                     ' 健診・予防接種
    Private Const SQL_COL_DEDUCTION_AMOUNT_TOTAL As String = "Deduction_Amount_Total"             ' 総控除額
    Private Const SQL_COL_PAYMENTS_DEDUCTION_TOTAL As String = "Payments_Deduction_Total"         ' 差引支給額

    ' 締め状態
    Private _closingState As MonthlyClosingMode

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
                cellUtil.CellValue(FMT_CELL_LECTURE_UNIT_PRICE, 0, rowNo, GetValue(row(SQL_COL_LECTURE_UNIT_PRICE)), ATTR_CELL_LECTURE_UNIT_PRICE)
                cellUtil.CellValue(FMT_CELL_TASK_UNIT_PRICE, 0, rowNo, GetValue(row(SQL_COL_TASK_UNIT_PRICE)), ATTR_CELL_TASK_UNIT_PRICE)
                cellUtil.CellValue(FMT_CELL_WORKING_DAYS, 0, rowNo, GetValue(row(SQL_COL_WORKING_DAYS)), ATTR_CELL_WORKING_DAYS)
                cellUtil.CellValue(FMT_CELL_LECTURE_KOMA_NUMBER, 0, rowNo, GetValue(row(SQL_COL_LECTURE_KOMA_NUMBER)), ATTR_CELL_LECTURE_KOMA_NUMBER)
                cellUtil.CellValue(FMT_CELL_WORKING_MINUTES, 0, rowNo, GetValue(row(SQL_COL_WORKING_MINUTES)), ATTR_CELL_WORKING_MINUTES)
                cellUtil.CellValue(FMT_CELL_WORKING_MINUTES_DEEMED, 0, rowNo, GetValue(row(SQL_COL_WORKING_MINUTES_DEEMED)), ATTR_CELL_WORKING_MINUTES_DEEMED)
                cellUtil.CellValue(FMT_CELL_MIDNIGHT_WORKING_MINUTES, 0, rowNo, GetValue(row(SQL_COL_MIDNIGHT_WORKING_MINUTES)), ATTR_CELL_MIDNIGHT_WORKING_MINUTES)
                cellUtil.CellValue(FMT_CELL_OVERTIME_WORKING_MINUTES, 0, rowNo, GetValue(row(SQL_COL_OVERTIME_WORKING_MINUTES)), ATTR_CELL_OVERTIME_WORKING_MINUTES)
                cellUtil.CellValue(FMT_CELL_LECTURE_WAGE_TOTAL, 0, rowNo, GetValue(row(SQL_COL_LECTURE_WAGE_TOTAL)), ATTR_CELL_LECTURE_WAGE_TOTAL)
                cellUtil.CellValue(FMT_CELL_HOURLY_WAGE_TOTAL, 0, rowNo, GetValue(row(SQL_COL_HOURLY_WAGE_TOTAL)), ATTR_CELL_HOURLY_WAGE_TOTAL)
                cellUtil.CellValue(FMT_CELL_ALLOWANCE_TOTAL, 0, rowNo, GetValue(row(SQL_COL_ALLOWANCE_TOTAL)), ATTR_CELL_ALLOWANCE_TOTAL)
                cellUtil.CellValue(FMT_CELL_ADJUSTMENT_SALARY_TOTAL, 0, rowNo, GetValue(row(SQL_COL_ADJUSTMENT_SALARY_TOTAL)), ATTR_CELL_ADJUSTMENT_SALARY_TOTAL)
                cellUtil.CellValue(FMT_CELL_SETTLEMENT_MONEY_TOTAL, 0, rowNo, GetValue(row(SQL_COL_SETTLEMENT_MONEY_TOTAL)), ATTR_CELL_SETTLEMENT_MONEY_TOTAL)
                cellUtil.CellValue(FMT_CELL_OVERTIME_WAGE_TOTAL, 0, rowNo, GetValue(row(SQL_COL_OVERTIME_WAGE_TOTAL)), ATTR_CELL_OVERTIME_WAGE_TOTAL)
                cellUtil.CellValue(FMT_CELL_TRANSPO_TOTAL, 0, rowNo, GetValue(row(SQL_COL_TRANSPO_TOTAL)), ATTR_CELL_TRANSPO_TOTAL)
                cellUtil.CellValue(FMT_CELL_ADJUSTMENT_TRANSP_TOTAL, 0, rowNo, GetValue(row(SQL_COL_ADJUSTMENT_TRANSP_TOTAL)), ATTR_CELL_ADJUSTMENT_TRANSP_TOTAL)
                cellUtil.CellValue(FMT_CELL_PAYMENTS_TOTAL, 0, rowNo, GetValue(row(SQL_COL_PAYMENTS_TOTAL)), ATTR_CELL_PAYMENTS_TOTAL)
                cellUtil.CellValue(FMT_CELL_DEDUCTION_TOTAL, 0, rowNo, GetValue(row(SQL_COL_DEDUCTION_TOTAL)), ATTR_CELL_DEDUCTION_TOTAL)
                cellUtil.CellValue(FMT_CELL_ADVANCES_PAID_TOTAL, 0, rowNo, GetValue(row(SQL_COL_ADVANCES_PAID_TOTAL)), ATTR_CELL_ADVANCES_PAID_TOTAL)
                cellUtil.CellValue(FMT_CELL_HEALTH_CHECK_TOTAL, 0, rowNo, GetValue(row(SQL_COL_HEALTH_CHECK_TOTAL)), ATTR_CELL_HEALTH_CHECK_TOTAL)
                cellUtil.CellValue(FMT_CELL_DEDUCTION_AMOUNT_TOTAL, 0, rowNo, GetValue(row(SQL_COL_DEDUCTION_AMOUNT_TOTAL)), ATTR_CELL_DEDUCTION_AMOUNT_TOTAL)
                cellUtil.CellValue(FMT_CELL_PAYMENTS_DEDUCTION_TOTAL, 0, rowNo, GetValue(row(SQL_COL_PAYMENTS_DEDUCTION_TOTAL)), ATTR_CELL_PAYMENTS_DEDUCTION_TOTAL)
                rowNo = rowNo + 1
            End With
        Next

    End Sub

#End Region

End Class
