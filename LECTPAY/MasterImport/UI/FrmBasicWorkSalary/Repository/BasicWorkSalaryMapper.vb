''' <summary>
''' DataTable と ViewState モデルの相互変換を行うマッパー。
''' DataGridView と内部モデルの橋渡しを担当する。
''' </summary>
Public Class BasicWorkSalaryMapper

    ''' <summary>
    ''' DataTable → List(Of MasterItemViewState)
    ''' </summary>
    Public Shared Function ToViewStateList(entitys As List(Of BasicWorkSalaryEntity)) As List(Of BasicWorkSalaryDto)
        Dim list As New List(Of BasicWorkSalaryDto)
        Dim ts As TimeSpan = TimeSpan.Zero
        For Each item As BasicWorkSalaryEntity In entitys
            Dim rec As New BasicWorkSalaryDto
            rec.Id = item.Id
            rec.EffectiveDate = item.EffectiveDate
            rec.TaskBaseUnitPrice = item.TaskBaseUnitPrice
            rec.DeemedTimeBefore = item.DeemedTimeBefore
            rec.DeemedTimeAfter = item.DeemedTimeAfter
            rec.CreateDate = item.CreateDate
            rec.CreateUser = item.CreateUser
            rec.UpdateDate = item.UpdateDate
            rec.UpdateUser = item.UpdateUser
            rec.RowVersion = item.RowVersion

            list.Add(rec)
        Next

        Return list
    End Function

    ''' <summary>
    ''' List(Of MasterItemViewState) → DataTable（DataGridView 用）
    ''' </summary>
    Public Shared Function ToDataTable(items As List(Of BasicWorkSalaryEntity)) As DataTable
        Dim dt As New DataTable()

        dt.Columns.Add("Id", GetType(Integer))
        dt.Columns.Add("Effective_Date", GetType(Date))
        dt.Columns.Add("Task_Base_Unit_Price", GetType(Integer))
        dt.Columns.Add("Deemed_Time_Before", GetType(Integer))
        dt.Columns.Add("Deemed_Time_After", GetType(Integer))
        dt.Columns.Add("Create_Date", GetType(DateTime))
        dt.Columns.Add("Create_User", GetType(String))
        dt.Columns.Add("Update_Date", GetType(DateTime))
        dt.Columns.Add("Update_User", GetType(String))
        dt.Columns.Add("RowVersion", GetType(Byte()))

        For Each item In items
            Dim row As DataRow = dt.NewRow()

            row("Id") = item.Id
            row("Effective_Date") = item.EffectiveDate
            row("Task_Base_Unit_Price") = item.TaskBaseUnitPrice
            row("Deemed_Time_Before") = item.DeemedTimeBefore
            row("Deemed_Time_After") = item.DeemedTimeAfter
            row("Create_Date") = If(item.CreateDate Is Nothing, DBNull.Value, item.CreateDate)
            row("Create_User") = item.CreateUser
            row("Update_Date") = If(item.UpdateDate Is Nothing, DBNull.Value, item.UpdateDate)
            row("Update_User") = item.UpdateUser
            row("RowVersion") = item.RowVersion

            dt.Rows.Add(row)
        Next

        Return dt
    End Function

End Class