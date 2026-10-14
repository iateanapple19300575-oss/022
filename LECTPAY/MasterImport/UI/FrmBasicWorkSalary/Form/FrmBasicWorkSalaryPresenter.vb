Imports MasterImport.MasterEdit.Dto
Imports MasterImport.MasterEditEnum

''' <summary>
''' 業務給基本設定画面 Presenter
''' </summary>
Public Class FrmBasicWorkSalaryPresenter
    Inherits PresenterBase

    Protected Message As String = ""

#Region "プロパティ関連"

    ''' <summary>
    ''' 業務給基本設定画面 Interface
    ''' </summary>
    Private ReadOnly _view As IFrmBasicWorkSalary

    ''' <summary>
    ''' 業務給基本設定画面 Model
    ''' </summary>
    Private _viewModel As BasicWorkSalaryModel

    ''' <summary>
    ''' 業務給基本設定画面 Service
    ''' </summary>
    Private Property _service As New BasicWorkSalaryService

    ''' <summary>
    ''' Validator
    ''' </summary>
    Private ReadOnly _validation As New BasicWorkSalaryValidator

    ''' <summary>
    ''' 状態管理プロパティ
    ''' </summary>
    Private Property _formViewState As New BasicWorkSalaryViewState

    ''' <summary>
    ''' 業務給基本設定情報用 BindingSource
    ''' </summary>
    Private ReadOnly _bindingSourceData As BindingSource

    ''' <summary>
    ''' 業務給基本設定情報取得データ
    ''' </summary>
    Private _DataList As IEnumerable(Of BasicWorkSalaryDto)

    ''' <summary>
    ''' 編集前データ
    ''' </summary>
    Private _beforeData As New BasicWorkSalaryModel

#End Region


#Region "ログ出力用汎用変数"

    ''' <summary>
    ''' 操作内容
    ''' </summary>
    Dim windowName As String = ""

    ''' <summary>
    ''' 操作内容
    ''' </summary>
    Dim actionName As String = ""

    ''' <summary>
    ''' 詳細内容
    ''' </summary>
    Dim detailes As String = ""

#End Region


#Region "初期処理"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New(ByRef view As FrmBasicWorkSalary)
        Me._bindingSourceData = New BindingSource()
        Me._viewModel = New BasicWorkSalaryModel
        Me._view = view
        Me.windowName = view.Text

        ' 初期化処理
        Initialize()

        ' 初期データ表示
        LoadInitData()
    End Sub

    ''' <summary>
    ''' 初期化処理
    ''' ・イベントハンドラの紐付け
    ''' ・クラスのインスタンス化
    ''' </summary>
    Private Sub Initialize()
        ' 一覧用 BindingSource設定
        Me._view.SetDataListBindingSource(_bindingSourceData)
        _DataList = New BasicWorkSalaryDto(-1) {}
    End Sub

#End Region


#Region "授業給係数設定（本科・日特・特選）情報の取得と表示"

    ''' <summary>
    ''' 初期データ表示
    ''' </summary>
    Private Sub LoadInitData()

        ' 画面の初期値を取得
        _viewModel.CurrentMode = _formViewState.Mode

        ' 画面表示データ取得
        _viewModel = _service.LoadInitData(_viewModel)
        _DataList = _viewModel.DataItemList

        ' DataGridiewデータバインド
        BindLoadData(_DataList)

        'BindLoadData()
        SetSelectedCursor(0)

        ' 画面項目データ表示
        ModelToUi(_viewModel)
    End Sub

    ''' <summary>
    ''' 検索データの表示
    ''' </summary>
    Private Sub ReLoadData()
        Dim m = _viewModel.Clone()

        ' 条件検索で一覧データ読み込み
        _DataList = _service.LoadDataList(_viewModel)

        ' DataGridiewデータバインド
        BindLoadData(_DataList)

        ' カーソル位置復旧
        _view.RefreshAndFocusRow(m.EffectiveDate)
    End Sub

    ''' <summary>
    ''' 取得した授業給係数設定データを一覧に反映します。
    ''' </summary>
    Private Sub BindLoadData(list As IEnumerable(Of BasicWorkSalaryDto))
        _bindingSourceData.DataSource = Nothing
        _bindingSourceData.DataSource = list
        _view.SetDataListBindingSource(_bindingSourceData)
        _view.ApplyDataGridViewDefinition()
    End Sub

#End Region


#Region "イベント処理"

    ''' <summary>
    ''' [追加] ボタン押下イベント処理
    ''' </summary>
    Public Sub AddNewEvent(ctrlName As String)
#Region "--- ログ出力 ---"
        actionName = "[" & ctrlName & "] ボタン押下"
        detailes = ""
        LogService.ManualLogWriting(windowName, actionName, detailes)
#End Region

        Try
            ' 追加モード設定
            _formViewState.EnterAddMode()

            SetDefaultValues()

            ' 活性/非活性制御
            _view.CurrentViewState = CurrentViewState()
            _view.ApplyUIState()
            _view.LisLocker.StartLock()
        Catch ex As Exception
            GlobalLog.Error(MessageIdConst.MSGID_I9002, ex)
            _view.ShowMessageError(MessageIdConst.MSGID_I9002)
        End Try
    End Sub

    ''' <summary>
    ''' [編集] ボタン押下イベント処理
    ''' </summary>
    Public Sub EditEvent(ctrlName As String)
#Region "--- ログ出力 ---"
        actionName = "[" & ctrlName & "] ボタン押下"
        detailes = ""
        LogService.ManualLogWriting(windowName, actionName, detailes)
#End Region

        Try
            ' 編集モード設定
            _formViewState.EnterEditMode(_formViewState.SelectedId, _formViewState.SelectedRowVersion)

            ' 編集前データを保持
            SetOriginalData()

            ' 活性/非活性制御
            _view.CurrentViewState = CurrentViewState()
            _view.ApplyUIState()
            _view.LisLocker.StartLock()
        Catch ex As Exception
            GlobalLog.Error(MessageIdConst.MSGID_I9002, ex)
            _view.ShowMessageError(MessageIdConst.MSGID_I9002)
        End Try
    End Sub

    ''' <summary>
    ''' [削除] ボタン押下イベント処理
    ''' </summary>
    Public Sub DeleteEvent(ctrlName As String)
#Region "--- ログ出力 ---"
        actionName = "[" & ctrlName & "] ボタン押下"
        detailes = ""
        LogService.ManualLogWriting(windowName, actionName, detailes)
#End Region

        Try
#Region "--- ログ出力 ---"
            actionName = "[" & ctrlName & "] ボタン押下"
            detailes = "削除確認：" & "本当に削除しますか？"
            LogService.ManualLogWriting(windowName, actionName, detailes)
#End Region

            ' 削除確認
            If MessageBox.Show("本当に削除しますか？", "削除確認",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then

#Region "--- ログ出力 ---"
                actionName = "削除確認：[いいえ] ボタン押下"
                detailes = "削除をキャンセルしました。"
                LogService.ManualLogWriting(windowName, actionName, detailes)
#End Region

                Return
            End If

            ' 削除モード設定
            _formViewState.EnterDeleteMode(_formViewState.SelectedId, _formViewState.SelectedRowVersion)

            ' データ削除処理
            GetListDataRowForModel()
            Dim result As New MasterEdit.Dto.ValidationResult
            result = _service.Save(_viewModel)
            ShowDeleteResultMessage(result)

            ' 一覧データの再読込表示
            ReLoadData()
            If Not result.ResultType = OptimisticLockingErrorType.DML_SUCCESS Then
                Return
            End If

            ' 通常モード設定
            _formViewState.EnterDefaultMode()

            ' 入力欄初期値設定
            SetDefaultValues()

            ' 活性/非活性制御
            _view.CurrentViewState = CurrentViewState()
            _view.ApplyUIState()

        Catch ex As Exception
            GlobalLog.Error(MessageIdConst.MSGID_E9013, ex)
            _view.ShowMessageError(MessageIdConst.MSGID_E9013)
        End Try
    End Sub

    ''' <summary>
    ''' 削除処理の結果メッセージ表示処理
    ''' </summary>
    ''' <param name="result"></param>
    Public Sub ShowDeleteResultMessage(result As MasterEdit.Dto.ValidationResult)

        actionName = "削除確認：[はい] ボタン押下"

        ' 正常終了
        If result.ResultType = OptimisticLockingErrorType.DML_SUCCESS Then
            detailes = "授業給係数設定を削除しました"
            _view.ShowMessageResult(detailes)
        ElseIf result.ResultType = OptimisticLockingErrorType.DELETE_NO_MATCH Then
            detailes = "他ユーザに削除された可能性があります。" & vbCrLf & "再度実行してください。"
            _view.ShowMessageResult(detailes)
        Else
            detailes = MessageIdConst.MSGID_E9001
            _view.ShowMessageResult(detailes)
        End If

#Region "--- ログ出力 ---"
        LogService.ManualLogWriting(windowName, actionName, detailes)
#End Region

    End Sub

    ''' <summary>
    ''' [保存] ボタン押下イベント処理
    ''' </summary>
    Public Sub SaveEvent(ctrlName As String)
#Region "--- ログ出力 ---"
        actionName = "[" & ctrlName & "] ボタン押下"
        detailes = ""
        LogService.ManualLogWriting(windowName, actionName, detailes)
#End Region

        Try
            If Not _formViewState.CanSave() Then
                Return
            End If

            ' データ保存処理
            UiToModel()
            Dim result As New MasterEdit.Dto.ValidationResult
            result = _service.Save(_viewModel)
            ShowSaveResultMessage(result)

            ' 一覧データの再読込表示
            ReLoadData()
            If Not result.ResultType = OptimisticLockingErrorType.DML_SUCCESS Then
                Return
            End If

            ' 通常モード設定
            _formViewState.EnterDefaultMode()

            ' 入力欄初期値設定
            SetDefaultValues()

            ' 活性/非活性制御
            _view.CurrentViewState = CurrentViewState()
            _view.ApplyUIState()
            _view.LisLocker.EndLock()
            _view.LisLocker.EndUpdate()

        Catch ex As Exception
            GlobalLog.Error(MessageIdConst.MSGID_E9014, ex)
            _view.ShowMessageError(MessageIdConst.MSGID_E9014)
        End Try
    End Sub

    ''' <summary>
    ''' 保存処理の結果メッセージ表示処理
    ''' </summary>
    ''' <param name="result"></param>
    Private Sub ShowSaveResultMessage(result As MasterEdit.Dto.ValidationResult)

        actionName = If(_formViewState.Mode = MasterEditMode.Add, "[保存] ボタン押下（追加）", "[保存] ボタン押下（編集）")

        ' 正常終了
        If result.ResultType = OptimisticLockingErrorType.DML_SUCCESS Then
            LogWriteForSuccess()
            _view.ShowMessageResult("保存が完了しました")
        End If

        ' バリデーションエラー
        If result.ResultType > 0 Then
            ' エラーデータ表示
            _view.ShowMessageResult(String.Join(Environment.NewLine, result.ValidationErrors.ToArray))

#Region "--- ログ出力 ---"
            detailes = "" & String.Join(", ", result.ValidationErrors.ToArray)
            LogService.ManualLogWriting(windowName, actionName, detailes)
#End Region

            Return
        End If

        ' 処理エラー
        If result.ResultType < 0 Then
            If result.ResultType = OptimisticLockingErrorType.INSERT_DUPLICATE Then
                result.StatusMessage = "このデータは既に登録されています。"
            End If

            If result.ResultType = OptimisticLockingErrorType.UPDATE_CHANGED Then
                result.StatusMessage = "他ユーザに更新された可能性があります。" & vbCrLf & "再度実行してください。"
            End If

            If result.ResultType = OptimisticLockingErrorType.UPDATE_DELETED Then
                result.StatusMessage = "他ユーザに削除された可能性があります。" & vbCrLf & "再度実行してください。"
            End If

            If result.ResultType = OptimisticLockingErrorType.UPDATE_NO_MATCH Then
                result.StatusMessage = "他ユーザに更新された可能性があります。" & vbCrLf & "再度実行してください。"
            End If

            ' エラーメッセージ表示
            _view.ShowMessageWarning(result.StatusMessage)

#Region "--- ログ出力 ---"
            detailes = result.StatusMessage
            LogService.ManualLogWriting(windowName, actionName, detailes)
#End Region
            Return
        End If

    End Sub

    ''' <summary>
    ''' [キャンセル] ボタン押下イベント処理
    ''' </summary>
    Public Sub CancelEvent(ctrlName As String)

#Region "--- ログ出力 ---"
        actionName = "[" & ctrlName & "] ボタン押下"
        detailes = ""
        LogService.ManualLogWriting(windowName, actionName, detailes)
#End Region

        Try
            ' モードリセット
            _formViewState.EnterDefaultMode()

            ' 初期値(クリア)表示
            SetDefaultValues()

            ' 活性/非活性制御
            _view.CurrentViewState = CurrentViewState()
            _view.ApplyUIState()
            _view.LisLocker.EndLock()
            _view.LisLocker.EndUpdate()
        Catch ex As Exception
            GlobalLog.Error(MessageIdConst.MSGID_I9002, ex)
            _view.ShowMessageError(MessageIdConst.MSGID_I9002)
        End Try
    End Sub

    ''' <summary>
    ''' [一覧]ダブルクリックイベント処理
    ''' </summary>
    Public Sub DataListEvent(ctrlName As String)
#Region "--- ログ出力 ---"
        actionName = "[" & "授業給係数設定（本科・日特・特選）一覧" & "] ダブルクリック"
        detailes = ""
        LogService.ManualLogWriting(windowName, actionName, detailes)
#End Region

        Try
            ' [参照]モードの場合スキップ
            If _formViewState.Mode = MasterEditMode.ReadOnly Then
                Return
            End If

            ' 選択行のデータを取得
            If _bindingSourceData.Current Is Nothing OrElse Not (TypeOf _bindingSourceData.Current Is BasicWorkSalaryDto) Then
                Return
            End If

            ' モード：[編集]モードへ
            _formViewState.EnterEditMode(_formViewState.SelectedId, _formViewState.SelectedRowVersion)

            ' 編集前データを保持
            SetOriginalData()

            ' 活性/非活性制御
            _view.CurrentViewState = CurrentViewState()
            _view.ApplyUIState()
            _view.LisLocker.StartLock()

        Catch ex As Exception
            GlobalLog.Error(MessageIdConst.MSGID_I9002, ex)
            _view.ShowMessageError(MessageIdConst.MSGID_I9002)
        End Try
    End Sub

    ''' <summary>
    ''' 画面遷移（Shown）時のチェック要求イベントの処理
    ''' </summary>
    Public Sub TransitionEvent(ctrlName As String)
        Try
            _view.LockUsersCount = _service.GetLockedAccounts()

            ' ロックユーザ数取得エラー（負の値:-1）
            If _view.LockUsersCount < 0 Then
                _view.ShowMessageError(MessageIdConst.MSGID_E9004)
            End If

            If Not _view.IsStartupSuccess Then
                _view.ShowMessageError(MessageIdConst.MSGID_E9004)
            End If
        Catch ex As Exception
            GlobalLog.Error(MessageIdConst.MSGID_I9002, ex)
            _view.ShowMessageError(MessageIdConst.MSGID_I9002)
        End Try
    End Sub

    ''' <summary>
    ''' Shown イベント
    ''' 現在の編集項目データとオリジナルデータを比較して編集中判定を行います。
    ''' </summary>
    Public Sub ShownEvent(ctrlName As String)
        Try
            ' 他ユーザー使用中
            If _view.LockUsersCount > 1 Then
                ' 警告メッセージを表示し、[参照]モードに強制する。
                _formViewState.EnterInitMode(MasterEditMode.[ReadOnly])
                _view.CurrentViewState = CurrentViewState()
                _view.ApplyUIState()
                _view.ShowMessageWarning(MessageIdConst.MSGID_E9010)
                Return
            End If

            ' 他ユーザー未使用
            If _view.LockUsersCount >= 0 AndAlso _view.LockUsersCount <= 1 Then
                ' 通常モード設定
                _formViewState.EnterDefaultMode()
            End If

            ' 活性/非活性制御
            _view.CurrentViewState = CurrentViewState()
            _view.ApplyUIState()
        Catch ex As Exception
            GlobalLog.Error(MessageIdConst.MSGID_I9002, ex)
            _view.ShowMessageError(MessageIdConst.MSGID_I9002)
        End Try
    End Sub

    ''' <summary>
    ''' Closing イベント
    ''' 現在の編集項目データとオリジナルデータを比較して編集中判定を行います。
    ''' </summary>
    Public Sub ClosingEvent(ctrlName As String)
        Try
            UiToModel()
            _view.IsEditChanged = _viewModel.IsEditChange(_beforeData)
        Catch ex As Exception
            GlobalLog.Error(MessageIdConst.MSGID_I9002, ex)
            _view.ShowMessageError(MessageIdConst.MSGID_I9002)
        End Try
    End Sub

#End Region


#Region "画面 <-> Model インターフェース処理"

    ''' <summary>
    ''' UI → Model の変換。
    ''' </summary>
    Private Sub UiToModel()
        _viewModel.CurrentMode = _formViewState.Mode
        _viewModel.Id = _formViewState.SelectedId
        _viewModel.RowVersion = ToByteSafe(_formViewState.SelectedRowVersion)

        _viewModel.EffectiveDate = If(_view.EffectiveDate.Replace("/", "").Trim = "", "", _view.EffectiveDate)
        _viewModel.TaskBaseUnitPrice = _view.TaskBaseUnitPrice.Replace(",", "")
        _viewModel.DeemedTimeBefore = _view.DeemedTimeBefore
        _viewModel.DeemedTimeAfter = _view.DeemedTimeAfter
    End Sub

    ''' <summary>
    ''' Model データを UI に反映します。
    ''' </summary>
    Private Sub ModelToUi(ByVal model As BasicWorkSalaryModel)
        _view.EffectiveDate = model.EffectiveDate
        _view.TaskBaseUnitPrice = NumberUtil.DigitComma(model.TaskBaseUnitPrice)
        _view.DeemedTimeBefore = model.DeemedTimeBefore
        _view.DeemedTimeAfter = model.DeemedTimeAfter
    End Sub

    ''' <summary>
    ''' Model データを 入力項目に反映します。
    ''' </summary>
    Private Sub ModelToEditField(ByVal _viewModel As BasicWorkSalaryModel)
        _view.EffectiveDate = _viewModel.EffectiveDate
        _view.TaskBaseUnitPrice = _viewModel.TaskBaseUnitPrice
        _view.DeemedTimeBefore = _viewModel.DeemedTimeBefore
        _view.DeemedTimeAfter = _viewModel.DeemedTimeAfter
        _view.Id = _viewModel.Id
        _view.Rowversion = _viewModel.RowVersion
    End Sub

#End Region


#Region "内部共通処理"

    ''' <summary>
    ''' 項目クリア処理
    ''' </summary>
    Private Sub CleanViewFields()
        _viewModel.EffectiveDate = String.Empty
        _viewModel.TaskBaseUnitPrice = String.Empty
        _viewModel.DeemedTimeBefore = String.Empty
        _viewModel.DeemedTimeAfter = String.Empty

        _viewModel.Id = Nothing
        _viewModel.RowVersion = Nothing
        _viewModel.CurrentMode = _formViewState.Mode
    End Sub

    ''' <summary>
    ''' 初期値(クリア)データを入力項目に設定します。
    ''' </summary>
    Protected Sub SetDefaultValues()
        UiToModel()
        CleanViewFields()
        ModelToUi(_viewModel)
        _beforeData = _viewModel.Clone()
    End Sub

    ''' <summary>
    ''' オリジナル(編集元データ)を入力項目に設定します。
    ''' </summary>
    Protected Sub SetOriginalData()
        UiToModel()
        GetListDataRowForModel()
        ModelToUi(_viewModel)
        _beforeData = _viewModel.Clone()
    End Sub

    ''' <summary>
    ''' DataGridViewの選択行のデータをModelで取得します。
    ''' </summary>
    Public Sub GetListDataRowForModel()

        If _bindingSourceData.Current Is Nothing OrElse Not (TypeOf _bindingSourceData.Current Is BasicWorkSalaryDto) Then
            Return
        End If

        Dim current As BasicWorkSalaryDto = CType(_bindingSourceData.Current, BasicWorkSalaryDto)
        _viewModel.EffectiveDate = current.EffectiveDate
        _viewModel.TaskBaseUnitPrice = current.TaskBaseUnitPrice
        _viewModel.DeemedTimeBefore = current.DeemedTimeBefore
        _viewModel.DeemedTimeAfter = current.DeemedTimeAfter
        _viewModel.Id = current.Id
        _viewModel.RowVersion = current.RowVersion
        _viewModel.CurrentMode = _formViewState.Mode

        _formViewState.SelectedId = _viewModel.Id
        _formViewState.SelectedRowVersion = ToByteSafe(_viewModel.RowVersion)
    End Sub

    ''' <summary>
    ''' 選択カーソルを設定する。
    ''' </summary>
    Public Sub SetSelectedCursor(Optional ByVal rowIndex As Integer = -1)
        _formViewState.SelectedId = -1
        _formViewState.SelectedRowVersion = New Byte() {0, 0, 0, 0, 0, 0, 0, 0}

        If _bindingSourceData.Current Is Nothing OrElse Not (TypeOf _bindingSourceData.Current Is BasicWorkSalaryDto) Then
            Return
        End If

        Dim current As BasicWorkSalaryDto = CType(_bindingSourceData.Current, BasicWorkSalaryDto)
        If current IsNot Nothing AndAlso current.Id.HasValue Then
            _formViewState.SelectedId = current.Id
            _formViewState.SelectedRowVersion = current.RowVersion

            If rowIndex >= 0 Then
                _view.SetSelectedCursor(rowIndex)
            End If
        End If
    End Sub

    ''' <summary>
    ''' [保存] 正常終了時のログ出力
    ''' </summary>
    Private Sub LogWriteForSuccess()
        Dim action As String =
            If(_viewModel.CurrentMode = MasterEditMode.Add, "[保存] ボタン押下（追加）",
                If(_viewModel.CurrentMode = MasterEditMode.Edit, "[保存] ボタン押下（編集）", "[削除] ボタン押下"))

        ' 編集の保存ならば、編集前後のデータを出力
        If _viewModel.CurrentMode = MasterEditMode.Edit Then
            LogService.ManualLogWriting(windowName, action, _beforeData, "編集前")
            LogService.ManualLogWriting(windowName, action, _viewModel, "編集後")
        Else
            LogService.ManualLogWriting(windowName, action, _viewModel)
        End If
    End Sub

#End Region


    ''' <summary>
    ''' 授業給係数コピー時の共通エラー処理
    ''' </summary>
    Private Sub HandleDataCopyException(ex As Exception, currentYear As Integer, nextYear As Integer, logFileWrite As Boolean)
        Dim message As String = String.Format("{0}年度から{1}年度への授業給係数のコピーに失敗しました", currentYear, nextYear)
        If logFileWrite Then
            GlobalLog.Error(message, ex)
        End If
        _view.ShowMessageError(message)
    End Sub

    ''' <summary>
    ''' 係数値（Decimal型）を文字列に変換します。
    ''' DBの型が Decimal(6,3) であるが画面上 整数部1桁、小数点以下2桁のため
    ''' 文字列に変換するときは、Decimal(4,2) であるかのように振る舞う。
    ''' </summary>
    ''' <param name="decValue"></param>
    ''' <returns></returns>
    Private Function LectureCoefficientString(ByVal decValue As Decimal) As String
        Dim truncated As Decimal = Math.Floor(decValue * 100D) / 100D
        Return truncated.ToString("0.00")
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Private Function CurrentViewState() As BasicWorkSalaryControlsDto

        Dim EditModeBackColor As Color = SystemColors.Control
        Dim EditModeForeColor As Color = Color.Black

        If Not _formViewState.Mode = MasterEditMode.None Then
            EditModeBackColor = Color.ForestGreen
            EditModeForeColor = Color.Yellow
        End If

        Return New BasicWorkSalaryControlsDto With {
            .btnAdd = _formViewState.IsAddEnabled(),
            .btnEdit = _formViewState.IsEditEnabled(),
            .btnDelete = _formViewState.IsDeleteEnabled(),
            .btnSave = _formViewState.IsSaveEnabled(),
            .btnCancel = _formViewState.IsCancelEnabled(),
            .txtEffectiveDate = _formViewState.IsTextFieldEnabled(),
            .txtTaskBaseUnitPrice = _formViewState.IsTextFieldEnabled(),
            .txtDeemedTimeBefore = _formViewState.IsTextFieldEnabled(),
            .txtDeemedTimeAfter = _formViewState.IsTextFieldEnabled(),
            .lblEditModeText = _formViewState.CurrentModeName(),
            .lblEditModeBackColor = EditModeBackColor,
            .lblEditModeForeColor = EditModeForeColor
        }
    End Function

End Class
