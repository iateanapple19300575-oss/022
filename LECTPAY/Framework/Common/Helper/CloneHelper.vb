Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Public Class CloneHelper

    ' 汎用的なクローン用メソッド
    Public Shared Function CloneObject(Of T)(ByVal source As T) As T
        If source Is Nothing Then Return Nothing

        Dim formatter As New BinaryFormatter()
        Using stream As New MemoryStream()
            formatter.Serialize(stream, source)
            stream.Position = 0
            Return DirectCast(formatter.Deserialize(stream), T)
        End Using
    End Function

End Class
