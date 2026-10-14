'''' <summary>
'''' 業務給基本設定画面 ViewController
'''' </summary>
'Public Class BasicWorkSalaryViewController
'    Inherits BaseEditViewControllerBase(Of BasicWorkSalaryModel)

'#Region "プロパティ関連"

'    ''' <summary>
'    ''' 業務給基本設定 Service
'    ''' </summary>
'    Private ReadOnly _service As New BasicWorkSalaryService

'    ''' <summary>
'    ''' 業務給基本設定Model Validator
'    ''' </summary>
'    Private ReadOnly _validation As New BasicWorkSalaryValidator

'#End Region


'#Region "コンストラクタ"

'    ''' <summary>
'    ''' コンストラクタ
'    ''' </summary>
'    Public Sub New()
'    End Sub

'#End Region


'#Region "初期表示"

'    ''' <summary>
'    ''' 画面表示データ取得
'    ''' </summary>
'    Public Function LoadInitData(model As BasicWorkSalaryModel) As BasicWorkSalaryModel
'        Dim resultModel As New BasicWorkSalaryModel

'        resultModel.DataItemList = LoadDataList(model)

'        ' 初期表示情報
'        resultModel.Id = model.Id
'        resultModel.EffectiveDate = model.EffectiveDate
'        resultModel.TaskBaseUnitPrice = model.TaskBaseUnitPrice
'        resultModel.DeemedTimeBefore = model.DeemedTimeBefore
'        resultModel.DeemedTimeAfter = model.DeemedTimeAfter
'        resultModel.CreateDate = model.CreateDate
'        resultModel.CreateUser = model.CreateUser
'        resultModel.UpdateDate = model.UpdateDate
'        resultModel.UpdateUser = model.UpdateUser
'        resultModel.RowVersion = model.RowVersion

'        Return resultModel
'    End Function


'#Region "コンポーネント取得"
'#End Region

'#Region "イベント対応メソッド"

'    ''' <summary>
'    ''' [検索]ボタン押下時及び画面初期表示時の内部処理
'    ''' 
'    ''' 授業給係数設定データ取得
'    ''' </summary>
'    'Public Function LoadDataList(ByVal model As BasicWorkSalaryModel) As List(Of BasicWorkSalaryEntity)
'    '    Return _service.LoadDataList(model)
'    'End Function

'#End Region
'#End Region


'#Region "イベント対応メソッド"

'    ''' <summary>
'    ''' [削除]ボタン押下処理
'    ''' </summary>
'    Public Overrides Function DeleteButtonClick(ByVal model As BasicWorkSalaryModel) As BasicWorkSalaryModel
'        Dim result As New BasicWorkSalaryModel

'        ' 保存処理
'        result = PreservationTreatment(model)

'        Return result
'    End Function

'    ''' <summary>
'    ''' [保存]ボタン押下処理
'    ''' </summary>
'    Public Overrides Function SaveButtonClick(ByVal model As BasicWorkSalaryModel) As BasicWorkSalaryModel
'        Dim result As New BasicWorkSalaryModel

'        ' バリデーション
'        Dim errors = _validation.Validate(model)
'        If errors.Count > 0 Then
'            result.ResultType = BasicEditEnum.ValidationErrorType.NOT_ENTERED
'            result.ValidationErrors = errors
'            Return result
'        End If

'        ' 保存処理
'        result = PreservationTreatment(model)
'        Return result
'    End Function

'    ''' <summary>
'    ''' 保存処理
'    ''' </summary>
'    ''' <param name="model"></param>
'    ''' <returns></returns>
'    Private Function PreservationTreatment(ByVal model As BasicWorkSalaryModel) As BasicWorkSalaryModel
'        Dim result As New BasicWorkSalaryModel
'        Dim status As Integer = 0

'        status = _service.Save(model)

'        If status = -1 Then
'            result.StatusMessage = "既に登録済みの業務給基本設定です"
'            Return result
'        End If

'        If status = -2 Then
'            result.StatusMessage = "他ユーザに更新された可能性があります。" & vbCrLf & "[検索]ボタン押下により最新データを表示してデータを確認してください。"
'            Return result
'        End If

'        If status = -3 Then
'            result.StatusMessage = "他ユーザに削除された可能性があります。" & vbCrLf & "[検索]ボタン押下により最新データを表示してデータを確認してください。"
'            Return result
'        End If

'        result.StatusMessage = "保存が完了しました。"

'        Return result
'    End Function

'#End Region

'    '''' <summary>
'    '''' 入力欄データをクリアする。
'    '''' </summary>
'    '''' <param name="model"></param>
'    'Private Sub SetBlankInTextBox(ByRef model As BasicWorkSalaryModel)
'    '    model.EffectiveDate = DateTime.MinValue
'    '    model.TaskBaseUnitPrice = ""
'    '    model.DeemedTimeBefore = ""
'    '    model.DeemedTimeAfter = ""
'    'End Sub

'    '''' <summary>
'    '''' MODEL→DTO変換
'    '''' </summary>
'    '''' <param name="model"></param>
'    '''' <returns></returns>
'    'Public Shared Function ToDto(model As BasicWorkSalaryModel) As BasicWorkSalaryEntity
'    '    If model Is Nothing Then
'    '        Return Nothing
'    '    End If

'    '    Return New BasicWorkSalaryEntity With {
'    '            .EffectiveDate = model.EffectiveDate,
'    '            .TaskBaseUnitPrice = CInt(model.TaskBaseUnitPrice),
'    '            .DeemedTimeBefore = CInt(model.DeemedTimeBefore),
'    '            .DeemedTimeAfter = CInt(model.DeemedTimeAfter)
'    '        }
'    'End Function

'    '''' <summary>
'    '''' DTO→MODEL変換
'    '''' </summary>
'    '''' <param name="entity"></param>
'    '''' <returns></returns>
'    'Public Shared Function ToModel(entity As BasicWorkSalaryEntity) As BasicWorkSalaryModel
'    '    If entity Is Nothing Then
'    '        Return Nothing
'    '    End If

'    '    Return New BasicWorkSalaryModel With {
'    '            .EffectiveDate = entity.EffectiveDate,
'    '            .TaskBaseUnitPrice = entity.TaskBaseUnitPrice.ToString(),
'    '            .DeemedTimeBefore = entity.DeemedTimeBefore.ToString(),
'    '            .DeemedTimeAfter = entity.DeemedTimeAfter.ToString()
'    '        }
'    'End Function
'#Region "バリデーション"

'    ''' <summary>
'    ''' 現在の利用者(ロック)数のチェックを行います。
'    ''' </summary>
'    ''' <returns></returns>
'    'Public Function GetLockedAccounts() As Integer
'    '    Return _service.GetLockedAccounts()
'    'End Function

'#End Region

'End Class
