''' <summary>
''' 授業給明細をPDF出力するレポートページクラス。
''' </summary>
Public Class RptPageLectureList
    Inherits VBReportPage

#Region "[定数]"

    ' ページタイトル名
    Private Const REPORT_PAGE_NAME As String = ""

    ' ページタイトルの変数名
    Private Const REPORT_SHEET_NAME As String = "LectureList"

    ' ページタイトル出力項目の変数
    Private Const REPORT_CELL_TITLE As String = "**TITLE"


    Private Const FMT_CELL_YEAR As String = "**Year"
    Private Const FMT_CELL_MONTH As String = "**Month"

    ' ヘッダ部：出力項目変数
    Private Const FMT_CELL_TEACHER_NAME As String = "**Teacher_Name"
    Private Const FMT_CELL_LECTURE_UNIT_PRICE As String = "**Lecture_Unit_Price"
    Private Const FMT_CELL_LECTURE_KOMA_NUMBER As String = "**Lecture_Koma_Number"
    Private Const FMT_CELL_LECTURE_COEFFICIENT_CNT As String = "**Lecture_Coefficient_Cnt"
    Private Const FMT_CELL_GRADE_1_CNT As String = "**Grade_1_Cnt"
    Private Const FMT_CELL_GRADE_2_CNT As String = "**Grade_2_Cnt"
    Private Const FMT_CELL_GRADE_3_CNT As String = "**Grade_3_Cnt"
    Private Const FMT_CELL_GRADE_4_CNT As String = "**Grade_4_Cnt"
    Private Const FMT_CELL_GRADE_5_CNT As String = "**Grade_5_Cnt"
    Private Const FMT_CELL_GRADE_6_CNT As String = "**Grade_6_Cnt"

    ' 明細行部：出力項目変数
    Private Const FMT_CELL_LECTURE_DATE As String = "**Lecture_Date"
    Private Const FMT_CELL_SITE_NAME As String = "**Site_Name"
    Private Const FMT_CELL_GRADE As String = "**Grade"
    Private Const FMT_CELL_LD_KOMA_CNT As String = "**LD_Koma_Cnt"
    Private Const FMT_CELL_LECTURE_COEFFICIENT As String = "**Lecture_Coefficient"
    Private Const FMT_CELL_REMARKS As String = "**Remarks"


    Private Const ATTR_CELL_YEAR As String = "W1"
    Private Const ATTR_CELL_MONTH As String = "AB1"

    ' ヘッダ部：基本セル書式参照セル
    Private Const ATTR_CELL_TEACHER_NAME As String = "B6"
    Private Const ATTR_CELL_LECTURE_UNIT_PRICE As String = "J6"
    Private Const ATTR_CELL_LECTURE_KOMA_NUMBER As String = "R6"
    Private Const ATTR_CELL_LECTURE_COEFFICIENT_CNT As String = "Y6"
    Private Const ATTR_CELL_GRADE_1_CNT As String = "B9"
    Private Const ATTR_CELL_GRADE_2_CNT As String = "G9"
    Private Const ATTR_CELL_GRADE_3_CNT As String = "L9"
    Private Const ATTR_CELL_GRADE_4_CNT As String = "Q9"
    Private Const ATTR_CELL_GRADE_5_CNT As String = "V9"
    Private Const ATTR_CELL_GRADE_6_CNT As String = "AA9"

    ' 明細行部：基本セル書式参照セル

    Private Const ATTR_CELL_LECTURE_DATE As String = "B13"
    Private Const ATTR_CELL_SITE_NAME As String = "F13"
    Private Const ATTR_CELL_GRADE As String = "K13"
    Private Const ATTR_CELL_LD_KOMA_CNT As String = "N13"
    Private Const ATTR_CELL_LECTURE_COEFFICIENT As String = "Q13"
    Private Const ATTR_CELL_REMARKS As String = "T13"


    Private Const SQL_COL_YEAR As String = "Year"
    Private Const SQL_COL_MONTH As String = "Month"

    Private Const SQL_COL_TEACHER_CODE As String = "Teacher_Code"
    Private Const SQL_COL_TEACHER_NAME As String = "Teacher_Name"
    Private Const SQL_COL_LECTURE_UNIT_PRICE As String = "Lecture_Unit_Price"
    Private Const SQL_COL_LECTURE_KOMA_NUMBER As String = "Lecture_Koma_Number"
    Private Const SQL_COL_LECTURE_COEFFICIENT_CNT As String = "Lecture_Coefficient_Cnt"
    Private Const SQL_COL_GRADE_1_CNT As String = "Grade_1_Cnt"
    Private Const SQL_COL_GRADE_2_CNT As String = "Grade_2_Cnt"
    Private Const SQL_COL_GRADE_3_CNT As String = "Grade_3_Cnt"
    Private Const SQL_COL_GRADE_4_CNT As String = "Grade_4_Cnt"
    Private Const SQL_COL_GRADE_5_CNT As String = "Grade_5_Cnt"
    Private Const SQL_COL_GRADE_6_CNT As String = "Grade_6_Cnt"

    ' 出力項目対応テーブル項目名

    Private Const SQL_COL_LECTURE_DATE As String = "Lecture_Date"
    Private Const SQL_COL_SITE_NAME As String = "Site_Name"
    Private Const SQL_COL_GRADE As String = "Grade"
    Private Const SQL_COL_LD_KOMA_CNT As String = "LD_Koma_Cnt"
    Private Const SQL_COL_LECTURE_COEFFICIENT As String = "Lecture_Coefficient"
    Private Const SQL_COL_REMARKS As String = "Remarks"

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
            GlobalLog.Error("授業給明細のPDF出力でエラーが発生しました", ex)
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
                cellUtil.CellValue(FMT_CELL_LECTURE_KOMA_NUMBER, 0, rowNo, GetValue(row(SQL_COL_LECTURE_KOMA_NUMBER)), ATTR_CELL_LECTURE_KOMA_NUMBER)
                cellUtil.CellValue(FMT_CELL_LECTURE_COEFFICIENT_CNT, 0, rowNo, GetValue(row(SQL_COL_LECTURE_COEFFICIENT_CNT)), ATTR_CELL_LECTURE_COEFFICIENT_CNT)
                cellUtil.CellValue(FMT_CELL_GRADE_1_CNT, 0, rowNo, GetValue(row(SQL_COL_GRADE_1_CNT)), ATTR_CELL_GRADE_1_CNT)
                cellUtil.CellValue(FMT_CELL_GRADE_2_CNT, 0, rowNo, GetValue(row(SQL_COL_GRADE_2_CNT)), ATTR_CELL_GRADE_2_CNT)
                cellUtil.CellValue(FMT_CELL_GRADE_3_CNT, 0, rowNo, GetValue(row(SQL_COL_GRADE_3_CNT)), ATTR_CELL_GRADE_3_CNT)
                cellUtil.CellValue(FMT_CELL_GRADE_4_CNT, 0, rowNo, GetValue(row(SQL_COL_GRADE_4_CNT)), ATTR_CELL_GRADE_4_CNT)
                cellUtil.CellValue(FMT_CELL_GRADE_5_CNT, 0, rowNo, GetValue(row(SQL_COL_GRADE_5_CNT)), ATTR_CELL_GRADE_5_CNT)
                cellUtil.CellValue(FMT_CELL_GRADE_6_CNT, 0, rowNo, GetValue(row(SQL_COL_GRADE_6_CNT)), ATTR_CELL_GRADE_6_CNT)
                headerCompleted = True
            End If

            ' 明細部
            With m_CellReport
                cellUtil.CellValue(FMT_CELL_LECTURE_DATE, 0, rowNo, GetValue(row(SQL_COL_LECTURE_DATE)), ATTR_CELL_LECTURE_DATE)
                cellUtil.CellValue(FMT_CELL_SITE_NAME, 0, rowNo, GetValue(row(SQL_COL_SITE_NAME)), ATTR_CELL_SITE_NAME)
                cellUtil.CellValue(FMT_CELL_GRADE, 0, rowNo, GetValue(row(SQL_COL_GRADE)), ATTR_CELL_GRADE)
                cellUtil.CellValue(FMT_CELL_LD_KOMA_CNT, 0, rowNo, GetValue(row(SQL_COL_LD_KOMA_CNT)), ATTR_CELL_LD_KOMA_CNT)
                cellUtil.CellValue(FMT_CELL_LECTURE_COEFFICIENT, 0, rowNo, GetDecimalValue(row(SQL_COL_LECTURE_COEFFICIENT), "0.00"), ATTR_CELL_LECTURE_COEFFICIENT)
                cellUtil.CellValue(FMT_CELL_REMARKS, 0, rowNo, GetValue(row(SQL_COL_REMARKS)), ATTR_CELL_REMARKS)
                rowNo = rowNo + 1
            End With
        Next
    End Sub

#End Region

End Class
