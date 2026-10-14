''' <summary>
''' 業務給基本設定画面用 Repository
''' </summary>
Public Class BasicWorkSalaryRepository
    Inherits TransactionBase

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="sqDblConn"></param>
    Public Sub New(ByVal sqDblConn As DbTransactionScope)
        MyBase.New(sqDblConn)
    End Sub


#Region "参照系"

    ''' <summary>
    ''' 条件指定一括取得
    ''' </summary>
    ''' <returns></returns>
    Public Function SearchAll() As List(Of BasicWorkSalaryEntity)
        ' SQL文の生成
        Dim sqlString As New System.Text.StringBuilder
        sqlString.Length = 0
        sqlString.Append("SELECT").Append(" ")
        sqlString.Append("  Id").Append(" ")
        sqlString.Append("  , Effective_Date").Append(" ")
        sqlString.Append("  , Task_Base_Unit_Price").Append(" ")
        sqlString.Append("  , DATEDIFF(MINUTE, 0, Deemed_Time_Before) AS Deemed_Time_Before").Append(" ")
        sqlString.Append("  , DATEDIFF(MINUTE, 0, Deemed_Time_After) AS Deemed_Time_After").Append(" ")
        sqlString.Append("  , Create_Date").Append(" ")
        sqlString.Append("  , Create_User").Append(" ")
        sqlString.Append("  , Update_Date").Append(" ")
        sqlString.Append("  , Update_User").Append(" ")
        sqlString.Append("  , RowVersion").Append(" ")
        sqlString.Append("FROM M_Hourly_Wage").Append(" ")
        sqlString.Append("ORDER BY").Append(" ")
        sqlString.Append("  Effective_Date").Append(" ")

        Return ExecuteQuery(Of BasicWorkSalaryEntity)(sqlString.ToString(), Nothing)
    End Function

#End Region

End Class