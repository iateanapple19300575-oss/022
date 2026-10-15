''' <summary>
''' 帳票ブロックを管理し、PDF全体の通しページを統括するクラス
''' </summary>
Public Class PdfDocumentManager

    ' PDF全体の各帳票ブロックのリスト
    Public ReadOnly _reports As New List(Of ReportListPageBase)()

    ' PDF全体の通しページ番号
    Private _currentPage As Integer = 1

    ' PDF全体ページ番号
    Private _totalPages As Integer = 0

    ''' <summary>
    ''' 管理する ReportListPage クラスを追加する。
    ''' </summary>
    Public Sub AddReport(ByVal report As ReportListPageBase, p1 As Integer, p2 As Integer)
        _totalPages += p1 + p2


        report.SetManager(Me)
        _reports.Add(report)
    End Sub

    ''' <summary>
    ''' 現在のページ番号を初期化する。
    ''' </summary>
    Public Sub Reset()
        _currentPage = 1
    End Sub

    ''' <summary>
    ''' 現在のページ番号を 1 進める。
    ''' </summary>
    Public Sub NextPage()
        _currentPage += 1
    End Sub

    ''' <summary>
    ''' 現在のページ番号を取得する。
    ''' </summary>
    ''' <returns></returns>
    Public Function CurrentPage() As Integer
        Return _currentPage
    End Function

    ''' <summary>
    ''' 総ページ数を取得する。
    ''' </summary>
    ''' <returns></returns>
    Public Function TotalPages() As Integer
        Return _totalPages
    End Function

End Class