''' <summary>
''' BasicWorkSalaryEntity データモデル
''' </summary>
Public Class BasicWorkSalaryEntity
    Inherits MasterEditEntityBase

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
    ''' BasicWorkSalaryEntity の初期化
    ''' </summary>
    Public Overrides Sub Clear()
        EffectiveDate = Nothing
        TaskBaseUnitPrice = Nothing
        DeemedTimeBefore = Nothing
        DeemedTimeAfter = Nothing
    End Sub

End Class