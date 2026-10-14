''' <summary>
''' BasicWorkSalaryDto データモデル
''' </summary>
Public Class BasicWorkSalaryDto
    Inherits MasterEditDtoBase

    ''' <summary>
    ''' Effective_Date
    ''' </summary>
    Public Property EffectiveDate As Date?

    ''' <summary>
    ''' Task_Base_Unit_Price
    ''' </summary>
    Public Property TaskBaseUnitPrice As Integer?

    ''' <summary>
    ''' Deemed_Time_Before
    ''' </summary>
    Public Property DeemedTimeBefore As Integer?

    ''' <summary>
    ''' Deemed_Time_After
    ''' </summary>
    Public Property DeemedTimeAfter As Integer?


    ''' <summary>
    ''' BasicWorkSalaryDto の初期化
    ''' </summary>
    Public Overrides Sub Clear()
        EffectiveDate = Nothing
        TaskBaseUnitPrice = Nothing
        DeemedTimeBefore = Nothing
        DeemedTimeAfter = Nothing
    End Sub

End Class