Imports System.Data.SqlClient

''' <summary>
''' 業務給基本設定データテーブル Repository
''' </summary>
Public Class HourlyWageRepository
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
    Public Function SearchAll() As List(Of HourlyWageEntity)

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

        Return ExecuteQuery(Of HourlyWageEntity)(sqlString.ToString(), Nothing)
    End Function

    ''' <summary>
    ''' 適用開始日データの1件取得
    ''' </summary>
    ''' <returns></returns>
    Public Function FindByKey(ByVal dto As HourlyWageEntity) As HourlyWageEntity

        ' SQL文の生成
        Dim sqlString As New System.Text.StringBuilder
        sqlString.Length = 0
        sqlString.Append("SELECT").Append(" ")
        sqlString.Append("  Id").Append(" ")
        sqlString.Append("  , Effective_Date").Append(" ")
        sqlString.Append("  , Task_Base_Unit_Price").Append(" ")
        sqlString.Append("  , Deemed_Time_Before").Append(" ")
        sqlString.Append("  , Deemed_Time_After").Append(" ")
        sqlString.Append("  , Create_Date").Append(" ")
        sqlString.Append("  , Create_User").Append(" ")
        sqlString.Append("  , Update_Date").Append(" ")
        sqlString.Append("  , Update_User").Append(" ")
        sqlString.Append("  , RowVersion").Append(" ")
        sqlString.Append("FROM M_Hourly_Wage").Append(" ")
        sqlString.Append("WHERE 1=1").Append(" ")
        sqlString.Append("  AND Effective_Date = @Effective_Date").Append(" ")

        Return ExecuteQuerySingle(Of HourlyWageEntity)(sqlString.ToString(),
            New SqlParameter() With {.ParameterName = "@Effective_Date", .Value = dto.EffectiveDate}
          )
    End Function

    ''' <summary>
    ''' 適用開始日データの1件取得
    ''' </summary>
    ''' <returns></returns>
    Public Function FindById(ByVal dto As HourlyWageEntity) As HourlyWageEntity

        ' SQL文の生成
        Dim sqlString As New System.Text.StringBuilder
        sqlString.Length = 0
        sqlString.Append("SELECT").Append(" ")
        sqlString.Append("  Id").Append(" ")
        sqlString.Append("  , Effective_Date").Append(" ")
        sqlString.Append("  , Task_Base_Unit_Price").Append(" ")
        sqlString.Append("  , Deemed_Time_Before").Append(" ")
        sqlString.Append("  , Deemed_Time_After").Append(" ")
        sqlString.Append("  , Create_Date").Append(" ")
        sqlString.Append("  , Create_User").Append(" ")
        sqlString.Append("  , Update_Date").Append(" ")
        sqlString.Append("  , Update_User").Append(" ")
        sqlString.Append("  , RowVersion").Append(" ")
        sqlString.Append("FROM M_Hourly_Wage").Append(" ")
        sqlString.Append("WHERE 1=1").Append(" ")
        sqlString.Append("  AND Id = @Id").Append(" ")

        Return ExecuteQuerySingle(Of HourlyWageEntity)(sqlString.ToString(),
            New SqlParameter() With {.ParameterName = "@Id", .Value = dto.Id}
          )
    End Function

#End Region


#Region "更新系"

    ''' <summary>
    ''' 1件追加
    ''' </summary>
    ''' <param name="dto">追加情報</param>
    ''' <returns></returns>
    Public Function Insert(ByVal dto As HourlyWageEntity) As Integer

        ' SQL文の生成
        Dim sqlString As New System.Text.StringBuilder
        sqlString.Length = 0
        sqlString.Append("INSERT INTO").Append(" ")
        sqlString.Append("  M_Hourly_Wage").Append(" ")
        sqlString.Append("  (").Append(" ")
        sqlString.Append("    Effective_Date").Append(" ")
        sqlString.Append("    , Task_Base_Unit_Price").Append(" ")
        sqlString.Append("    , Deemed_Time_Before").Append(" ")
        sqlString.Append("    , Deemed_Time_After").Append(" ")
        sqlString.Append("    , Create_Date").Append(" ")
        sqlString.Append("    , Create_User").Append(" ")
        sqlString.Append("    , Update_Date").Append(" ")
        sqlString.Append("    , Update_User").Append(" ")
        sqlString.Append("  ) VALUES (").Append(" ")
        sqlString.Append("    @Effective_Date").Append(" ")
        sqlString.Append("    , @Task_Base_Unit_Price").Append(" ")
        sqlString.Append("    , @Deemed_Time_Before").Append(" ")
        sqlString.Append("    , @Deemed_Time_After").Append(" ")
        sqlString.Append("    , GETDATE()").Append(" ")
        sqlString.Append("    , @Create_User").Append(" ")
        sqlString.Append("    , NULL").Append(" ")
        sqlString.Append("    , NULL").Append(" ")
        sqlString.Append("  )").Append(" ")

        ' パラメタ生成
        Dim params As New List(Of SqlParameter)
        params.Add(New SqlParameter() With {.ParameterName = "@Effective_Date", .Value = dto.EffectiveDate})
        params.Add(New SqlParameter() With {.ParameterName = "@Task_Base_Unit_Price", .Value = dto.TaskBaseUnitPrice})
        params.Add(New SqlParameter() With {.ParameterName = "@Deemed_Time_Before", .Value = dto.DeemedTimeBefore})
        params.Add(New SqlParameter() With {.ParameterName = "@Deemed_Time_After", .Value = dto.DeemedTimeAfter})
        params.Add(New SqlParameter() With {.ParameterName = "@Create_User", .Value = dto.CreateUser})

        Return ExecuteNonQuery(sqlString.ToString(), params.ToArray)
    End Function

    ''' <summary>
    ''' 1件更新
    ''' </summary>
    ''' <param name="dto">更新情報</param>
    ''' <returns></returns>
    Public Function Update(ByVal dto As HourlyWageEntity) As Integer

        ' SQL文の生成
        Dim sqlString As New System.Text.StringBuilder
        sqlString.Length = 0
        sqlString.Append("UPDATE").Append(" ")
        sqlString.Append("  M_Hourly_Wage").Append(" ")
        sqlString.Append("  SET").Append(" ")
        sqlString.Append("    Effective_Date = @Effective_Date").Append(" ")
        sqlString.Append("    , Task_Base_Unit_Price = @Task_Base_Unit_Price").Append(" ")
        sqlString.Append("    , Deemed_Time_Before = @Deemed_Time_Before").Append(" ")
        sqlString.Append("    , Deemed_Time_After = @Deemed_Time_After").Append(" ")
        sqlString.Append("    , Update_Date = GETDATE()").Append(" ")
        sqlString.Append("    , Update_User = @Update_User").Append(" ")
        sqlString.Append("  WHERE").Append(" ")
        sqlString.Append("    1=1").Append(" ")
        sqlString.Append("    AND Id = @Id").Append(" ")
        sqlString.Append("    AND RowVersion = @RowVersion").Append(" ")

        ' パラメタ生成
        Dim params As New List(Of SqlParameter)
        params.Add(New SqlParameter() With {.ParameterName = "@Id", .Value = dto.Id})
        params.Add(New SqlParameter() With {.ParameterName = "@Effective_Date", .Value = dto.EffectiveDate})
        params.Add(New SqlParameter() With {.ParameterName = "@Task_Base_Unit_Price", .Value = dto.TaskBaseUnitPrice})
        params.Add(New SqlParameter() With {.ParameterName = "@Deemed_Time_Before", .Value = dto.DeemedTimeBefore})
        params.Add(New SqlParameter() With {.ParameterName = "@Deemed_Time_After", .Value = dto.DeemedTimeAfter})
        params.Add(New SqlParameter() With {.ParameterName = "@Update_User", .Value = dto.UpdateUser})
        params.Add(New SqlParameter() With {.ParameterName = "@RowVersion", .Value = dto.RowVersion})

        Return ExecuteNonQuery(sqlString.ToString(), params.ToArray)
    End Function

    ''' <summary>
    ''' 条件指定一括取得
    ''' </summary>
    ''' <param name="dto">条件情報</param>
    ''' <returns></returns>
    Public Function DeleteById(ByVal dto As HourlyWageEntity) As Integer

        ' SQL文の生成
        Dim sqlString As New System.Text.StringBuilder
        sqlString.Length = 0
        sqlString.Append("DELETE").Append(" ")
        sqlString.Append("  FROM").Append(" ")
        sqlString.Append("    M_Hourly_Wage").Append(" ")
        sqlString.Append("  WHERE").Append(" ")
        sqlString.Append("        1=1").Append(" ")
        sqlString.Append("    AND Id = @Id").Append(" ")
        'sqlString.Append("    AND RowVersion = @RowVersion").Append(" ")

        ' パラメタ生成
        Dim params As New List(Of SqlParameter)
        params.Add(New SqlParameter() With {.ParameterName = "@Id", .Value = dto.Id})
        'params.Add(New SqlParameter() With {.ParameterName = "@RowVersion", .Value = dto.RowVersion})

        Return ExecuteNonQuery(sqlString.ToString(), params.ToArray)
    End Function

#End Region

End Class