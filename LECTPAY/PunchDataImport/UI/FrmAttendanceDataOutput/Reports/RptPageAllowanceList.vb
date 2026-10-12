''' <summary>
''' 給与明細をPDF出力するレポートページクラス。
''' </summary>
Public Class RptPageAllowanceList
    Inherits VBReportPage

#Region "[定数]"

    ' ページタイトル名
    Private Const REPORT_PAGE_NAME As String = ""

    ' ページタイトルの変数名
    Private Const REPORT_SHEET_NAME As String = "AllowanceList"

    ' ページタイトル出力項目の変数
    Private Const REPORT_CELL_TITLE As String = "**TITLE"

    ' 日付部：出力項目変数
    Private Const FMT_CELL_YEAR As String = "**Year"                                                    ' 年
    Private Const FMT_CELL_MONTH As String = "**Month"                                                  ' 月

    ' ヘッダ部：出力項目変数
    Private Const FMT_CELL_TEACHER_NAME As String = "**Teacher_Name"                                    ' 講師名
    Private Const FMT_CELL_LECTURE_UNIT_PRICE As String = "**Lecture_Unit_Price"                        ' 授業給単価
    Private Const FMT_CELL_TASK_UNIT_PRICE As String = "**Task_Unit_Price"                              ' 業務給単価
    Private Const FMT_CELL_WORKING_DAYS As String = "**Working_Days"                                    ' 勤務日数
    Private Const FMT_CELL_LECTURE_KOMA_NUMBER As String = "**Lecture_Koma_Number"                      ' 出講コマ数
    Private Const FMT_CELL_WORKING_MINUTES As String = "**Working_Minutes"                              ' 総勤務時間
    Private Const FMT_CELL_WORKING_MINUTES_DEEMED As String = "**Working_Minutes_Deemed"                ' 総勤務時間(みなし含)
    Private Const FMT_CELL_MIDNIGHT_WORKING_MINUTES As String = "**Midnight_Working_Minutes"            ' 深夜・早朝勤務
    Private Const FMT_CELL_OVERTIME_WORKING_MINUTES As String = "**Overtime_Working_Minutes"            ' 残業時間

    ' 明細行部：出力項目変数
    Private Const FMT_CELL_ALLOWANCE_DEDUCTION_NAME As String = "**Allowance_Deduction_Name"            ' 支給/控除
    Private Const FMT_CELL_ALLOWANCE_DEDUCTION_TYPE_NAME As String = "**Allowance_Deduction_Type_Name"  ' 種別
    Private Const FMT_CELL_ACCOUNT_NAME As String = "**Account_Name"                                    ' 費目
    Private Const FMT_CELL_AMOUNT As String = "**Amount"                                                ' 金額

    ' 日付部：基本セル書式参照セル
    Private Const ATTR_CELL_YEAR As String = "W1"                                                       ' 年
    Private Const ATTR_CELL_MONTH As String = "AB1"                                                     ' 月

    ' ヘッダ部：基本セル書式参照セル
    Private Const ATTR_CELL_TEACHER_NAME As String = "B6"                                               ' 講師名
    Private Const ATTR_CELL_LECTURE_UNIT_PRICE As String = "H6"                                         ' 授業給単価
    Private Const ATTR_CELL_TASK_UNIT_PRICE As String = "N6"                                            ' 業務給単価
    Private Const ATTR_CELL_WORKING_DAYS As String = "T6"                                               ' 勤務日数
    Private Const ATTR_CELL_LECTURE_KOMA_NUMBER As String = "Z6"                                        ' 出講コマ数
    Private Const ATTR_CELL_WORKING_MINUTES As String = "B9"                                            ' 総勤務時間
    Private Const ATTR_CELL_WORKING_MINUTES_DEEMED As String = "H9"                                     ' 総勤務時間(みなし含)
    Private Const ATTR_CELL_MIDNIGHT_WORKING_MINUTES As String = "N9"                                   ' 深夜・早朝勤務
    Private Const ATTR_CELL_OVERTIME_WORKING_MINUTES As String = "T9"                                   ' 残業時間

    ' 明細行部(手当・控除額等明細行)
    Private Const ATTR_CELL_ALLOWANCE_DEDUCTION_NAME As String = "B13"                                  ' 支給/控除
    Private Const ATTR_CELL_ALLOWANCE_DEDUCTION_TYPE_NAME As String = "F13"                             ' 種別
    Private Const ATTR_CELL_ACCOUNT_NAME As String = "J13"                                              ' 費目
    Private Const ATTR_CELL_AMOUNT As String = "X13"                                                    ' 金額

    ' 日付部：項目
    Private Const SQL_COL_YEAR As String = "Year"                                                       ' 年
    Private Const SQL_COL_MONTH As String = "Month"                                                     ' 月

    ' ヘッダ部：項目
    Private Const SQL_COL_TEACHER_CODE As String = "Teacher_Code"                                       ' 講師コード
    Private Const SQL_COL_TEACHER_NAME As String = "Teacher_Name"                                       ' 講師名
    Private Const SQL_COL_LECTURE_UNIT_PRICE As String = "Lecture_Unit_Price"                           ' 授業給単価
    Private Const SQL_COL_TASK_UNIT_PRICE As String = "Task_Unit_Price"                                 ' 業務給単価
    Private Const SQL_COL_WORKING_DAYS As String = "Working_Days"                                       ' 勤務日数
    Private Const SQL_COL_LECTURE_KOMA_NUMBER As String = "Lecture_Koma_Number"                         ' 出講コマ数
    Private Const SQL_COL_WORKING_MINUTES As String = "Working_Minutes"                                 ' 総勤務時間
    Private Const SQL_COL_WORKING_MINUTES_DEEMED As String = "Working_Minutes_Deemed"                   ' 総勤務時間(みなし含)
    Private Const SQL_COL_MIDNIGHT_WORKING_MINUTES As String = "Midnight_Working_Minutes"               ' 深夜・早朝勤務
    Private Const SQL_COL_OVERTIME_WORKING_MINUTES As String = "Overtime_Working_Minutes"               ' 残業時間

    ' 明細行部：項目
    Private Const SQL_COL_ALLOWANCE_DEDUCTION_NAME As String = "Allowance_Deduction_Name"               ' 支給/控除
    Private Const SQL_COL_ALLOWANCE_DEDUCTION_TYPE_NAME As String = "Allowance_Deduction_Type_Name"     ' 種別
    Private Const SQL_COL_ACCOUNT_NAME As String = "Account_Name"                                       ' 費目
    Private Const SQL_COL_AMOUNT As String = "Amount"                                                   ' 金額

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
        m_PageName = String.Format(REPORT_PAGE_NAME, Year(MyBase.TargetDate), Month(MyBase.TargetDate))
        m_SheetName = REPORT_SHEET_NAME
    End Sub

    ''' <summary>
    ''' ページデータ出力
    ''' </summary>
    Public Overrides Sub PageOutput()

        ' 仮締めのときは、仮締めマーク配置
        If _closingState = MonthlyClosingMode.TempClose Then
            m_CellReport.ScaleMode = AdvanceSoftware.VBReport8.ScaleMode.Pixel
            m_CellReport.PDF.ImageType = AdvanceSoftware.VBReport8.ImageType.AUTO
            m_CellReport.Cell("A1").Drawing.AddImage("resources\仮締め.png", 195, 63)
        End If

        Try
            OutputDetail(ReportSourceDataTable)
        Catch ex As Exception
            GlobalLog.Error("給与明細のPDF出力でエラーが発生しました", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' 明細出力
    ''' </summary>
    ''' <param name="dtTable"></param>
    Public Overrides Sub OutputDetail(ByVal dtTable As DataTable)
        Dim cellUtil As New CellReportUtil(m_CellReport)
        Dim rowNo As Integer = 0
        Dim headerCompleted As Boolean = False

        ' 年月
        cellUtil.CellValue(FMT_CELL_YEAR, 0, rowNo, TargetDate.Year, ATTR_CELL_YEAR)
        cellUtil.CellValue(FMT_CELL_MONTH, 0, rowNo, TargetDate.Month, ATTR_CELL_MONTH)

        ' 日別勤怠一覧
        For Each row As DataRow In dtTable.Rows

            ' ヘッダ部
            If Not headerCompleted Then
                Dim teacherName As String = GetValue(row(SQL_COL_TEACHER_CODE)) & ":" & GetValue(row(SQL_COL_TEACHER_NAME))
                cellUtil.CellValue(FMT_CELL_TEACHER_NAME, 0, rowNo, teacherName, ATTR_CELL_TEACHER_NAME)
                cellUtil.CellValue(FMT_CELL_LECTURE_UNIT_PRICE, 0, rowNo, GetValue(row(SQL_COL_LECTURE_UNIT_PRICE)), ATTR_CELL_LECTURE_UNIT_PRICE)
                cellUtil.CellValue(FMT_CELL_TASK_UNIT_PRICE, 0, rowNo, GetValue(row(SQL_COL_TASK_UNIT_PRICE)), ATTR_CELL_TASK_UNIT_PRICE)
                cellUtil.CellValue(FMT_CELL_WORKING_DAYS, 0, rowNo, GetValue(row(SQL_COL_WORKING_DAYS)), ATTR_CELL_WORKING_DAYS)
                cellUtil.CellValue(FMT_CELL_LECTURE_KOMA_NUMBER, 0, rowNo, GetValue(row(SQL_COL_LECTURE_KOMA_NUMBER)), ATTR_CELL_LECTURE_KOMA_NUMBER)
                cellUtil.CellValue(FMT_CELL_WORKING_MINUTES, 0, rowNo, TimeUtil.MinutesToHoursStr(GetValue(row(SQL_COL_WORKING_MINUTES))), ATTR_CELL_WORKING_MINUTES)
                cellUtil.CellValue(FMT_CELL_WORKING_MINUTES_DEEMED, 0, rowNo, TimeUtil.MinutesToHoursStr(GetValue(row(SQL_COL_WORKING_MINUTES_DEEMED))), ATTR_CELL_WORKING_MINUTES_DEEMED)
                cellUtil.CellValue(FMT_CELL_MIDNIGHT_WORKING_MINUTES, 0, rowNo, TimeUtil.MinutesToHoursStr(GetValue(row(SQL_COL_MIDNIGHT_WORKING_MINUTES))), ATTR_CELL_MIDNIGHT_WORKING_MINUTES)
                cellUtil.CellValue(FMT_CELL_OVERTIME_WORKING_MINUTES, 0, rowNo, TimeUtil.MinutesToHoursStr(GetValue(row(SQL_COL_OVERTIME_WORKING_MINUTES))), ATTR_CELL_OVERTIME_WORKING_MINUTES)
                headerCompleted = True
            End If

            ' 明細部
            With m_CellReport
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
