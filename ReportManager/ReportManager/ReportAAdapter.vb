''' <summary>
''' 帳票
''' </summary>
Public Class ReportAAdapter
    Inherits ReportListPageBase

    Public ReadOnly Property ListName As String
        Get
            Return "A帳票()"
        End Get
    End Property

    ''' <summary>
    ''' 一覧のタイトル文言を取得します。
    ''' </summary>
    Public Overrides Function GetBlockPageFormatText() As String
        Return String.Format("{0} ({1}/{2})", Me.ListName, Me.BlockCurrentPage, Me.BlockTotalPage)
    End Function
End Class
