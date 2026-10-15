Public MustInherit Class ReportListPageBase

    ' 帳票管理マネージャーへの参照
    Private _manager As PdfDocumentManager

    ' 帳票ブロック内ページ番号
    Public Property BlockCurrentPage As Integer = 1

    ' 帳票ブロック内全体ページ数
    Public Property BlockTotalPage As Integer = 0

    '''' <summary>一覧タイトル名</summary>
    'Public MustOverride ReadOnly Property ListName() As String

    ''' <summary>帳票管理マネージャーをセットする</summary>
    Public Sub SetManager(ByVal manager As PdfDocumentManager)
        Me._manager = manager
    End Sub

    ''' <summary>
    ''' 帳票内のページを進める
    ''' 
    ''' 同時に帳票管理マネージャーを通しページのカウントアップを依頼します。
    ''' </summary>
    Public Overridable Sub NextPage()
        Me.BlockCurrentPage += 1

        ' 帳票管理マネージャーのメソッドを呼び出し、最新の通しページ番号を同期
        If Me._manager IsNot MyBase.GetType() Then
            Me._manager.NextPage()
        End If
    End Sub

    ''' <summary>
    ''' レポート全体の現在ページ番号を取得します。
    ''' </summary>
    Public Function GetReportCurrentPage() As Integer
        Return Me._manager.CurrentPage
    End Function

    ''' <summary>
    ''' レポート全体の総ページ数を取得します。
    ''' </summary>
    Public Function GetReportTotalPages() As Integer
        Return Me._manager.TotalPages
    End Function

    ''' <summary>
    ''' ブロック内ページ番号のフォーマット文字列を作成します。（基本形）
    ''' </summary>
    Public Overridable Function GetBlockPageFormatText() As String
        ' デフォルト形式: タイトル名 (n/ブロック内通し番号)
        Return String.Format("{0}/{1}", Me.BlockCurrentPage, Me.BlockTotalPage)
    End Function

    ''' <summary>
    ''' レポート全体ページ番号のフォーマット文字列を作成します。（基本形）
    ''' </summary>
    Public Overridable Function GetReportPageFormatText() As String
        ' デフォルト形式: タイトル名 (n/PDF内通し番号)
        Return String.Format("{0}/{1}", Me._manager.CurrentPage, Me._manager.TotalPages)
    End Function
End Class