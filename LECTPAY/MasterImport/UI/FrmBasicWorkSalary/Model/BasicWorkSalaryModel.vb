Imports MasterImport
''' <summary>
''' 業務給基本設定画面(BasicWorkSalary)の Model
''' </summary>
Public Class BasicWorkSalaryModel
    Inherits BasicEditModelBase(Of BasicWorkSalaryModel)
    Implements IMasterEditModel

#Region "コンポーネント必要データ"

    ''' <summary>
    ''' 授業給係数設定データ
    ''' </summary>
    ''' <returns></returns>
    Public Property DataItemList As List(Of BasicWorkSalaryDto)

#End Region

#Region "画面項目データ"

    ''' <summary>
    ''' ID
    ''' </summary>
    <LogField("Id", Enabled:=True)>
    Public Property Id As Long?

    ''' <summary>
    ''' 適用開始日
    ''' </summary>
    <LogField("適用開始日", Enabled:=True)>
    Public Property EffectiveDate As String = ""

    ''' <summary>
    ''' 業務給標準単価
    ''' </summary>
    <LogField("業務給標準単価", Enabled:=True)>
    Public Property TaskBaseUnitPrice As String = ""

    ''' <summary>
    ''' みなし勤務時間(事前分)
    ''' </summary>
    <LogField("みなし勤務時間(事前分)", Enabled:=True)>
    Public Property DeemedTimeBefore As String = ""

    ''' <summary>
    ''' みなし勤務時間(事後分)
    ''' </summary>
    <LogField("みなし勤務時間(事後分)", Enabled:=True)>
    Public Property DeemedTimeAfter As String = ""

    ''' <summary>
    ''' 作成日
    ''' </summary>
    <LogField("作成日", Enabled:=False)>
    Public Property CreateDate As DateTime?

    ''' <summary>
    ''' 作成者
    ''' </summary>
    <LogField("作成者", Enabled:=False)>
    Public Property CreateUser As String

    ''' <summary>
    ''' 更新日
    ''' </summary>
    <LogField("更新日", Enabled:=False)>
    Public Property UpdateDate As DateTime?

    ''' <summary>
    ''' 更新者
    ''' </summary>
    <LogField("更新者", Enabled:=False)>
    Public Property UpdateUser As String

    ''' <summary>
    ''' RowVersion
    ''' </summary>
    <LogField("RowVersion", Enabled:=True)>
    Public Property RowVersion As Byte()

#End Region

#Region "レスポンス関連"

    Public ReadOnly Property Result As MasterEditResult
        Get
            Return New MasterEditResult With {
                .ResultType = ResultType,
                .ValidationErrors = ValidationErrors,
                .StatusMessage = StatusMessage
            }
        End Get
    End Property

    ''' <summary>
    ''' 処理結果タイプ
    ''' </summary>
    Public Property ResultType As Integer

    ''' <summary>
    ''' バリデーションエラー
    ''' </summary>
    Public Property ValidationErrors As List(Of String)

    ''' <summary>
    ''' 処理結果メッセージ
    ''' </summary>
    Public Property StatusMessage As String

#End Region

#Region "状態管理"

    ''' <summary>
    ''' 現在の状態
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrentMode As MasterEditMode Implements IMasterEditModel.CurrentMode

#End Region


#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
        Clear()
    End Sub


    ''' <summary>
    ''' BasicWorkSalaryModel の初期化
    ''' </summary>
    Public Overrides Sub Clear()
        Id = Nothing
        EffectiveDate = ""
        TaskBaseUnitPrice = ""
        DeemedTimeBefore = ""
        DeemedTimeAfter = ""
        CreateDate = Nothing
        CreateUser = ""
        UpdateDate = Nothing
        UpdateUser = ""
        RowVersion = New Byte() {0, 0, 0, 0, 0, 0, 0, 0}

        ResultType = 0
        ValidationErrors = New List(Of String)
        StatusMessage = ""
        CurrentMode = MasterEditMode.None
    End Sub

#End Region

    ''' <summary>
    ''' クローンメソッド
    ''' </summary>
    ''' <returns></returns>
    Public Overrides Function Clone() As BasicWorkSalaryModel
        Return DirectCast(Me.MemberwiseClone(), BasicWorkSalaryModel)
    End Function

    ''' <summary>
    ''' BasicWorkSalaryModel → LectureFeeCoefficientEntity 変換
    ''' </summary>
    Public Function ToEntity() As HourlyWageEntity
        Dim entity As New HourlyWageEntity()
        entity.Id = Id
        entity.EffectiveDate = DateUtil.DateStringToTryDate(EffectiveDate)
        entity.TaskBaseUnitPrice = NumberUtil.StringNumberToInteger(TaskBaseUnitPrice)
        entity.DeemedTimeBefore = TimeUtil.TotalHoursToTimeSpan(DeemedTimeBefore)
        entity.DeemedTimeAfter = TimeUtil.TotalHoursToTimeSpan(DeemedTimeAfter)
        entity.CreateDate = CreateDate
        entity.CreateUser = CreateUser
        entity.UpdateDate = UpdateDate
        entity.UpdateUser = UpdateUser
        entity.RowVersion = RowVersion
        Return entity
    End Function

    ''' <summary>
    ''' HourlyWageEntity → BasicWorkSalaryModel の反映
    ''' </summary>
    Public Sub FromEntity(ByVal entity As HourlyWageEntity)
        Id = entity.Id
        EffectiveDate = entity.EffectiveDate
        TaskBaseUnitPrice = entity.TaskBaseUnitPrice.ToString()
        'DeemedTimeBefore = entity.DeemedTimeBefore.ToString()
        'DeemedTimeAfter = entity.DeemedTimeAfter.ToString()
        DeemedTimeBefore = TimeUtil.TimeSpanToFormatString(entity.DeemedTimeBefore)
        DeemedTimeAfter = TimeUtil.TimeSpanToFormatString(entity.DeemedTimeAfter)
        CreateDate = entity.CreateDate
        CreateUser = entity.CreateUser
        UpdateDate = entity.UpdateDate
        UpdateUser = entity.UpdateUser
        RowVersion = entity.RowVersion
        ResultType = 0
        ValidationErrors = New List(Of String)
        StatusMessage = ""
        CurrentMode = MasterEditMode.None
    End Sub

    ''' <summary>
    ''' データ変更チェック
    ''' 
    ''' 項目単位に値を比較し変更があったか確認します。
    ''' </summary>
    ''' <param name="model"></param>
    ''' <returns></returns>
    Public Overrides Function IsEditChange(ByVal model As BasicWorkSalaryModel) As Boolean

        If Not EffectiveDate = model.EffectiveDate Then
            Return True
        End If

        If Not TaskBaseUnitPrice = model.TaskBaseUnitPrice Then
            Return True
        End If

        If Not DeemedTimeBefore = model.DeemedTimeBefore Then
            Return True
        End If

        If Not DeemedTimeAfter = model.DeemedTimeAfter Then
            Return True
        End If

        Return False
    End Function

End Class
