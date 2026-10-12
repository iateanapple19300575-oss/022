''' <summary>
''' 給与明細情報を講師毎にPDF出力するレポートクラス
''' </summary>
Public Class RptAttendanceReport
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
    ''' 給与明細出力
    ''' </summary>
    Public Overloads Sub Output(ByRef cancelRequested As Boolean)
        Dim frmVBReportsViewer As New frmVBReportsViewer
        Dim yearMonthStr As String = DateUtil.DateToFormatString(TargetDate, "yyyyMM")
        Dim targetTeacherCode As String = ""

        Try
            VbReportsEnvSetting(frmVBReportsViewer.CellReport1, frmVBReportsViewer.ViewerControl1)
            frmVBReportsViewer.ViewerControl1.ShowProgressDialog = False

            Using uow As New DbTransactionScope()
                Dim repo As New AttendanceReportRepository(uow, _closingState)

                ' 給与明細講師ヘッダ情報
                Dim headsDt As DataTable = repo.GetPaySlipHeader(yearMonthStr)
                ' 給与明細情報（全体）
                Dim allowancesALLDt As DataTable = repo.GetAllowanceList(yearMonthStr)
                ' 授業給明細情報（全体）
                Dim lecturesALLDt As DataTable = repo.GetLectureList(yearMonthStr)
                ' 日別勤怠一覧情報（全体）
                Dim attendancesALLDt As DataTable = repo.GetAttendanceList(yearMonthStr)

                Dim allowanceDt As DataTable
                Dim lectureDt As DataTable
                Dim attendanceDt As DataTable
                Dim totalCount As Integer = headsDt.Rows.Count
                Dim counter As Integer = 0

                For Each row As DataRow In headsDt.Rows
                    counter += 1
                    If cancelRequested Then
                        Return ' 中断
                    End If

                    ' 対象となる講師コードを取得
                    targetTeacherCode = row("Teacher_Code")

                    ' 講師コードから出力ファイル名を生成
                    OutputFilePath = MakeFileName(targetTeacherCode)

                    ' 講師１名の各種情報（取得給与明細、授業給明細、日別勤怠一覧）取得
                    ' DataTable に前回のゴミが残ることが可能性があるので、毎回 new するようにしている。 
                    allowanceDt = New DataTable
                    lectureDt = New DataTable
                    attendanceDt = New DataTable
                    Dim where As String = WhereSql(targetTeacherCode)
                    Dim order As String = OrderBySql()

                    ' １講師の給与明細情報取得
                    allowanceDt = allowancesALLDt.Select(where, order).CopyToDataTable()

                    ' １講師の授業給明細情報取得
                    lectureDt = lecturesALLDt.Select(where, order).CopyToDataTable()

                    ' １講師の日別勤怠一覧情報取得
                    attendanceDt = attendancesALLDt.Select(where, order).CopyToDataTable()

                    ' 給与明細ページ
                    AddPage(New RptPageAllowanceList(TargetDate, allowanceDt, _closingState))

                    ' 授業給明細ページ
                    AddPage(New RptPageLectureList(TargetDate, lectureDt, _closingState))

                    ' 日別勤怠一覧ページ
                    AddPage(New RptPageAttendanceList(TargetDate, attendanceDt, _closingState))

                    RaiseEvent ProgressChanged(counter, totalCount, "給与明細データをPDFに出力しています...")

                    ' 1講師分のレポート出力
                    Report()

                    RaiseEvent ProgressChanged(counter, totalCount, "給与明細データをPDFに出力しています...")
                    Application.DoEvents()
                    System.Threading.Thread.Sleep(10)
                Next
            End Using

            '' レポート出力
            'Report()

            ' Viewerは表示しない
            'frmVBReportsViewer.ShowDialog()

            ' リソースの解放
            frmVBReportsViewer.Dispose()

        Catch ex As Exception
            GlobalLog.Error("給与明細(PDF)出力に失敗しました" & " [講師コード:" & targetTeacherCode & "]", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' 絞り込み条件文字列を取得します。
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Private Function WhereSql(ByVal code As String) As String
        Return "Teacher_Code = '" & code & "'"
    End Function

    ''' <summary>
    ''' 並び替え条件文字列を取得します。
    ''' </summary>
    ''' <returns></returns>
    Private Function OrderBySql() As String
        Return "Teacher_Code ASC"
    End Function

    ''' <summary>
    ''' 講師コード毎のファイル名を作成する。
    ''' ※ファイル名に格納先フォルダを付加する。
    ''' 　
    ''' 例）101001_202604.pdf
    ''' [給与明細のPDFのファイル名ルール]
    '''   1-6桁目: 講師コード変換後の6桁コード
    '''     7桁目: "_"固定
    '''  8-13桁目: YYYYMM（年月）
    ''' </summary>
    ''' <param name="teacherCode"></param>
    ''' <returns></returns>
    Private Function MakeFileName(ByVal teacherCode As String) As String
        Dim sb As New System.Text.StringBuilder
        sb.Length = 0
        sb.Append(FolderPath).Append("\")

        If _closingState = MonthlyClosingMode.TempClose Then
            sb.Append("仮_")
        End If

        sb.Append(convertTeacherCodeToEmployeeNo(teacherCode)).Append("_")
        sb.Append(DateUtil.DateToFormatString(TargetDate)).Append(".")
        sb.Append("pdf")

        Return sb.ToString()
    End Function

    ''' <summary>
    ''' 講師コードを変換(4桁→6桁)する
    ''' ※講師コードは、英字1桁＋数字3桁を前提。
    ''' 　
    ''' [給与明細のPDFのファイル名ルール]
    ''' 　1-2桁目: "10"
    ''' 　3桁目　: 講師コードの先頭文字（英字を K→1, M→2, S→3, R→4, G→5）に変換
    ''' 　4-6桁目: 講師コードの下3桁
    ''' </summary>
    ''' <param name="teacherCode">元の講師コード</param>
    ''' <returns>変換後コード</returns>
    Private Function convertTeacherCodeToEmployeeNo(ByVal teacherCode As String) As String
        Dim strEmployeeNo As String = ""

        strEmployeeNo = "10"

        Select Case Strings.Left(teacherCode, 1)
            Case "K"
                strEmployeeNo = strEmployeeNo & "1"
            Case "M"
                strEmployeeNo = strEmployeeNo & "2"
            Case "S"
                strEmployeeNo = strEmployeeNo & "3"
            Case "R"
                strEmployeeNo = strEmployeeNo & "4"
            Case "G"
                strEmployeeNo = strEmployeeNo & "5"
            Case Else
                strEmployeeNo = strEmployeeNo & " "
        End Select

        strEmployeeNo = strEmployeeNo & Strings.Right(teacherCode, 3)

        Return strEmployeeNo
    End Function

    ''' <summary>
    ''' 処理中画面から呼び出される処理です。
    ''' </summary>
    ''' <param name="cancelRequested"></param>
    Public Sub ExportReports(ByRef cancelRequested As Boolean) Implements IReportExporter.ExportReports
        Output(cancelRequested)
    End Sub

End Class
