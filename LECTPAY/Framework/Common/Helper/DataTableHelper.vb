Public Class DataTableHelper

    ''' <summary>
    ''' DataReader / DataRow の値を DTO プロパティ型に安全に変換する。
    ''' ・Nullable 対応
    ''' ・Enum / Guid / TimeSpan / Byte() 対応
    ''' ・その他は ChangeType に委譲
    ''' </summary>
    ''' <param name="value">元の値。</param>
    ''' <param name="targetType">変換先の型。</param>
    ''' <returns>変換後の値。</returns>
    Public Shared Function ConvertValue(value As Object, targetType As Type) As Object

        If value Is Nothing OrElse value Is DBNull.Value Then
            Return Nothing
        End If

        If targetType.IsGenericType AndAlso targetType.GetGenericTypeDefinition() Is GetType(Nullable(Of )) Then
            targetType = Nullable.GetUnderlyingType(targetType)
        End If

        If targetType Is GetType(Byte()) Then
            Return DirectCast(value, Byte())
        End If

        If targetType.IsEnum Then
            Return [Enum].Parse(targetType, value.ToString())
        End If

        If targetType Is GetType(Guid) Then
            Return New Guid(value.ToString())
        End If

        If targetType Is GetType(TimeSpan) Then
            Return TimeSpan.Parse(value.ToString())
        End If

        Return Convert.ChangeType(value, targetType)
    End Function

    ''' <summary>
    ''' DataTable を指定列でソートした後、
    ''' 1つ目は firstChunk 件、2つ目以降は otherChunk 件で分割する。
    ''' </summary>
    Public Shared Function SplitDataTableVariable(source As DataTable,
                                              sortExpression As String,
                                              firstChunk As Integer,
                                              otherChunk As Integer) As List(Of DataTable)

        Dim result As New List(Of DataTable)()

        If source Is Nothing OrElse source.Rows.Count = 0 Then
            Return result
        End If

        ' --- ソート（FW3.5 互換） ---
        Dim view As New DataView(source)
        view.Sort = sortExpression
        Dim sorted As DataTable = view.ToTable()

        Dim total As Integer = sorted.Rows.Count
        Dim index As Integer = 0
        Dim chunkSize As Integer = firstChunk   ' 最初だけ firstChunk

        While index < total

            Dim dt As DataTable = sorted.Clone()

            For i As Integer = 0 To chunkSize - 1
                If index >= total Then
                    Exit For
                End If
                dt.ImportRow(sorted.Rows(index))
                index += 1
            Next

            result.Add(dt)

            ' 2つ目以降は otherChunk 件に切り替え
            chunkSize = otherChunk
        End While

        Return result
    End Function

    ''' <summary>
    ''' DataTable をソート＋フィルタして複製する（FW3.5 互換）
    ''' </summary>
    ''' <param name="source">元の DataTable</param>
    ''' <param name="filterExpression">例. "Status = 'OK' AND Amount > 100"</param>
    ''' <param name="sortExpression">例. "ID ASC, Date DESC"</param>
    Public Shared Function CloneDataTable(source As DataTable,
                                          filterExpression As String,
                                          sortExpression As String) As DataTable

        Dim result As New DataTable()

        If source Is Nothing OrElse source.Rows.Count = 0 Then
            Return result
        End If

        ' --- DataView によるソート＋フィルタ ---
        Dim view As New DataView(source)

        If filterExpression IsNot Nothing AndAlso filterExpression <> "" Then
            view.RowFilter = filterExpression
        End If

        If sortExpression IsNot Nothing AndAlso sortExpression <> "" Then
            view.Sort = sortExpression
        End If

        ' --- 絞り込み＋ソート済みの DataTable を作成 ---
        Dim filteredSorted As DataTable = view.ToTable()

        ' --- Clone（構造コピー） ---
        Dim dt As DataTable = filteredSorted.Clone()

        ' --- 行コピー ---
        For Each row As DataRow In filteredSorted.Rows
            dt.ImportRow(row)
        Next

        Return dt
    End Function

End Class
