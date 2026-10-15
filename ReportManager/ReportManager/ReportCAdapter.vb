''' <summary>
''' 帳票
''' </summary>
Public Class ReportCAdapter
    Inherits ReportListPageBase

    Public ReadOnly Property ListName As String
        Get
            Return "C帳票(送付状)"
        End Get
    End Property

    ''' <summary>
    ''' 帳票Cはフォーマットを変えたいので、親のメソッドをオーバーライド（上書き）します
    ''' </summary>
    Public Overrides Function GetBlockPageFormatText() As String
        Return String.Format("[{0}] Page.{1} (通しNo.{2})", Me.ListName, Me.BlockCurrentPage, Me.BlockTotalPage)
    End Function
End Class
