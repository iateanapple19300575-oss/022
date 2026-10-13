Public Class PageUtil
    Public Shared Function GetPageCount(allCnt As Integer, p1Cnt As Integer, p2Cnt As Integer) As Integer
        If allCnt <= 0 Then
            Return 0
        End If

        If 0 < allCnt AndAlso allCnt <= p1Cnt Then
            Return 1
        End If

        Dim restCnt As Integer = allCnt - p1Cnt
        Dim totalPages As Integer = CInt(Math.Ceiling(CDbl(restCnt) / CDbl(p2Cnt)))
        Return totalPages + 1
    End Function

    Public Shared Function IsMultiPages(allCnt As Integer, p1Cnt As Integer, p2Cnt As Integer) As Boolean
        Return GetPageCount(allCnt, p1Cnt, p2Cnt) > 1
    End Function

End Class
