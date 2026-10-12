''' <summary>
''' 日別勤怠一覧をPDF出力するレポートページクラス。
''' </summary>
Public Class RptPageAttendanceList
    Inherits VBReportPage2

#Region "[定数]"

    ' ページタイトル名
    Private Const REPORT_PAGE_NAME As String = ""

    ' ページタイトルの変数名
    Private REPORT_SHEET_NAMES As String() = New String() {"AttendanceList", "AttendanceList2"}

    ' ページタイトル出力項目の変数
    Private Const REPORT_CELL_TITLE As String = "**TITLE"


    Private Const FMT_CELL_YEAR As String = "**Year"
    Private Const FMT_CELL_MONTH As String = "**Month"

    ' ヘッダ部：出力項目変数
    Private Const FMT_CELL_TEACHER_NAME As String = "**Teacher_Name"
    Private Const FMT_CELL_WORKING_MINUTES As String = "**Working_Minutes"
    Private Const FMT_CELL_WORKING_MINUTES_DEEMED As String = "**Working_Minutes_Deemed"
    Private Const FMT_CELL_MIDNIGHT_WORKING_MINUTES_TOTAL As String = "**Midnight_Working_Minutes_Total"
    Private Const FMT_CELL_OVERTIME_WORKING_MINUTES As String = "**Overtime_Working_Minutes"

    ' 明細行部：出力項目変数
    Private Const FMT_CELL_LECTURE_DATE As String = "**Lecture_Date"
    Private Const FMT_CELL_ATTENDANCE_NAME As String = "**Attendance_Name"
    Private Const FMT_CELL_PUNCH_START_TIME As String = "**Punch_Start_Time"
    Private Const FMT_CELL_PUNCH_END_TIME As String = "**Punch_End_Time"
    Private Const FMT_CELL_STAMP_WORKING_MINUTES As String = "**Stamp_Working_Minutes"
    Private Const FMT_CELL_DEEMED_START_TIME As String = "**Deemed_Start_Time"
    Private Const FMT_CELL_DEEMED_END_TIME As String = "**Deemed_End_Time"
    Private Const FMT_CELL_DEEMED_WORKING_MINUTES As String = "**Deemed_Working_Minutes"
    Private Const FMT_CELL_BREAK_TIME_MINUTES As String = "**Break_Time_Minutes"
    Private Const FMT_CELL_MIDNIGHT_WORKING_MINUTES As String = "**Midnight_Working_Minutes"
    Private Const FMT_CELL_CONTENTS As String = "**Contents"


    ' 基本セル書式参照セル：年月部
    Private Const ATTR_CELL_YEAR As String = "W1"
    Private Const ATTR_CELL_MONTH As String = "AB1"

    ' 基本セル書式参照セル：ヘッダ部
    Private Const ATTR_CELL_TEACHER_NAME As String = "B6"
    Private Const ATTR_CELL_WORKING_MINUTES As String = "H6"
    Private Const ATTR_CELL_WORKING_MINUTES_DEEMED As String = "N6"
    Private Const ATTR_CELL_MIDNIGHT_WORKING_MINUTES_TOTAL As String = "T6"
    Private Const ATTR_CELL_OVERTIME_WORKING_MINUTES As String = "Z6"

    ' 基本セル書式参照セル：明細行部
    Private Const ATTR_CELL_LECTURE_DATE As String = "B11"
    Private Const ATTR_CELL_ATTENDANCE_NAME As String = "D11"
    Private Const ATTR_CELL_PUNCH_START_TIME As String = "G11"
    Private Const ATTR_CELL_PUNCH_END_TIME As String = "I11"
    Private Const ATTR_CELL_STAMP_WORKING_MINUTES As String = "K11"
    Private Const ATTR_CELL_DEEMED_START_TIME As String = "M11"
    Private Const ATTR_CELL_DEEMED_END_TIME As String = "O11"
    Private Const ATTR_CELL_DEEMED_WORKING_MINUTES As String = "Q11"
    Private Const ATTR_CELL_BREAK_TIME_MINUTES As String = "S11"
    Private Const ATTR_CELL_MIDNIGHT_WORKING_MINUTES As String = "U11"
    Private Const ATTR_CELL_CONTENTS As String = "W11"

    ' 出力項目：年月部
    Private Const SQL_COL_YEAR As String = "Year"
    Private Const SQL_COL_MONTH As String = "Month"

    ' 出力項目：ヘッダ部
    Private Const SQL_COL_TEACHER_CODE As String = "Teacher_Code"
    Private Const SQL_COL_TEACHER_NAME As String = "Teacher_Name"
    Private Const SQL_COL_WORKING_MINUTES As String = "Working_Minutes"
    Private Const SQL_COL_WORKING_MINUTES_DEEMED As String = "Working_Minutes_Deemed"
    Private Const SQL_COL_MIDNIGHT_WORKING_MINUTES_TOTAL As String = "Midnight_Working_Minutes_Total"
    Private Const SQL_COL_OVERTIME_WORKING_MINUTES As String = "Overtime_Working_Minutes"

    ' 出力項目：明細部
    Private Const SQL_COL_LECTURE_DATE As String = "Lecture_Date"
    Private Const SQL_COL_ATTENDANCE_NAME As String = "Attendance_Name"
    Private Const SQL_COL_PUNCH_START_TIME As String = "Punch_Start_Time"
    Private Const SQL_COL_PUNCH_END_TIME As String = "Punch_End_Time"
    Private Const SQL_COL_STAMP_WORKING_MINUTES As String = "Stamp_Working_Minutes"
    Private Const SQL_COL_DEEMED_START_TIME As String = "Deemed_Start_Time"
    Private Const SQL_COL_DEEMED_END_TIME As String = "Deemed_End_Time"
    Private Const SQL_COL_DEEMED_WORKING_MINUTES As String = "Deemed_Working_Minutes"
    Private Const SQL_COL_BREAK_TIME_MINUTES As String = "Break_Time_Minutes"
    Private Const SQL_COL_MIDNIGHT_WORKING_MINUTES As String = "Midnight_Working_Minutes"
    Private Const SQL_COL_CONTENTS As String = "Contents"

    ' 一覧の先頭セル(列)
    Private Const CELL_COL_OFFSET As Integer = 17

    ' 一覧の先頭セル(行)
    Private Const CELL_ROW_OFFSET As Integer = 6

    ' 1ページ目の最大明細行数
    Private Const MAX_ROWS_PAGE1 As Integer = 27

    ' 2ページ目以降の最大明細行数
    Private Const MAX_ROWS_PAGE2 As Integer = 40

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
    Protected Overrides ReadOnly Property PageSheetName As String()
        Get
            Return REPORT_SHEET_NAMES
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
            GlobalLog.Error("日別勤怠一覧のPDF出力でエラーが発生しました", ex)
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

        ' 日別勤怠一覧(年月部)
        cellUtil.CellValue(FMT_CELL_YEAR, 0, rowNo, TargetDate.Year, ATTR_CELL_YEAR)
        cellUtil.CellValue(FMT_CELL_MONTH, 0, rowNo, TargetDate.Month, ATTR_CELL_MONTH)

        ' 日別勤怠一覧(ヘッダ部)
        RenderHeader(cellUtil, dtTable.Rows(0))

        ' 日別勤怠一覧(明細部)
        Dim pageNo As Integer = 1
        Dim isFirstPage As Boolean = True
        Dim MaxRowsPerPage As Integer = MAX_ROWS_PAGE1

        For i As Integer = 0 To dtTable.Rows.Count - 1
            ' 明細行出力
            RenderDetail(cellUtil, rowNo, dtTable.Rows(i))

            ' 対象行：次の行
            rowNo += 1

            ' ページ切替
            If rowNo > MaxRowsPerPage + 2 Then
                ' ページ終了
                PageEnd()
                ' 次頁
                pageNo += 1
                ' １頁目終了
                isFirstPage = False
                ' 最大行数設定
                MaxRowsPerPage = MAX_ROWS_PAGE2
                ' 次頁目の設定
                Page2Setup()
                ' 次頁目の開始
                PageStart()
            End If
        Next
    End Sub

#End Region

    ''' <summary>
    ''' ヘッダ行出力
    ''' </summary>
    ''' <param name="cellUtil"></param>
    ''' <param name="header"></param>
    Protected Sub RenderHeader(cellUtil As CellReportUtil, header As DataRow)
        Dim teacherName As String = GetValue(header(SQL_COL_TEACHER_CODE)) & ":" & GetValue(header(SQL_COL_TEACHER_NAME))
        cellUtil.CellValue(FMT_CELL_TEACHER_NAME, 0, 0, teacherName, ATTR_CELL_TEACHER_NAME)
        cellUtil.CellValueTimeFormat(FMT_CELL_WORKING_MINUTES, 0, 0, GetValue(header(SQL_COL_WORKING_MINUTES)), ATTR_CELL_WORKING_MINUTES)
        cellUtil.CellValueTimeFormat(FMT_CELL_WORKING_MINUTES_DEEMED, 0, 0, GetValue(header(SQL_COL_WORKING_MINUTES_DEEMED)), ATTR_CELL_WORKING_MINUTES_DEEMED)
        cellUtil.CellValueTimeFormat(FMT_CELL_MIDNIGHT_WORKING_MINUTES_TOTAL, 0, 0, GetValue(header(SQL_COL_MIDNIGHT_WORKING_MINUTES_TOTAL)), ATTR_CELL_MIDNIGHT_WORKING_MINUTES_TOTAL)
        cellUtil.CellValueTimeFormat(FMT_CELL_OVERTIME_WORKING_MINUTES, 0, 0, GetValue(header(SQL_COL_OVERTIME_WORKING_MINUTES)), ATTR_CELL_OVERTIME_WORKING_MINUTES)
    End Sub

    ''' <summary>
    ''' 明細行出力
    ''' </summary>
    ''' <param name="cellUtil"></param>
    ''' <param name="rowNo"></param>
    ''' <param name="detail"></param>
    Protected Sub RenderDetail(cellUtil As CellReportUtil, rowNo As Integer, detail As DataRow)
        With m_CellReport
            cellUtil.CellValue(FMT_CELL_LECTURE_DATE, 0, rowNo, GetValue(detail(SQL_COL_LECTURE_DATE)), ATTR_CELL_LECTURE_DATE)
            cellUtil.CellValue(FMT_CELL_ATTENDANCE_NAME, 0, rowNo, GetValue(detail(SQL_COL_ATTENDANCE_NAME)), ATTR_CELL_ATTENDANCE_NAME)
            cellUtil.CellValueTimeFormat(FMT_CELL_PUNCH_START_TIME, 0, rowNo, GetValue(detail(SQL_COL_PUNCH_START_TIME)), ATTR_CELL_PUNCH_START_TIME)
            cellUtil.CellValueTimeFormat(FMT_CELL_PUNCH_END_TIME, 0, rowNo, GetValue(detail(SQL_COL_PUNCH_END_TIME)), ATTR_CELL_PUNCH_END_TIME)
            cellUtil.CellValueTimeFormat(FMT_CELL_STAMP_WORKING_MINUTES, 0, rowNo, GetValue(detail(SQL_COL_STAMP_WORKING_MINUTES)), ATTR_CELL_STAMP_WORKING_MINUTES)
            cellUtil.CellValueTimeFormat(FMT_CELL_DEEMED_START_TIME, 0, rowNo, GetValue(detail(SQL_COL_DEEMED_START_TIME)), ATTR_CELL_DEEMED_START_TIME)
            cellUtil.CellValueTimeFormat(FMT_CELL_DEEMED_END_TIME, 0, rowNo, GetValue(detail(SQL_COL_DEEMED_END_TIME)), ATTR_CELL_DEEMED_END_TIME)
            cellUtil.CellValueTimeFormat(FMT_CELL_DEEMED_WORKING_MINUTES, 0, rowNo, GetValue(detail(SQL_COL_DEEMED_WORKING_MINUTES)), ATTR_CELL_DEEMED_WORKING_MINUTES)
            cellUtil.CellValueTimeFormat(FMT_CELL_BREAK_TIME_MINUTES, 0, rowNo, GetValue(detail(SQL_COL_BREAK_TIME_MINUTES)), ATTR_CELL_BREAK_TIME_MINUTES)
            cellUtil.CellValueTimeFormat(FMT_CELL_MIDNIGHT_WORKING_MINUTES, 0, rowNo, GetValue(detail(SQL_COL_MIDNIGHT_WORKING_MINUTES)), ATTR_CELL_MIDNIGHT_WORKING_MINUTES)
            cellUtil.CellValue(FMT_CELL_CONTENTS, 0, rowNo, GetValue(detail(SQL_COL_CONTENTS)), ATTR_CELL_CONTENTS)
            rowNo = rowNo + 1
        End With
    End Sub

End Class
