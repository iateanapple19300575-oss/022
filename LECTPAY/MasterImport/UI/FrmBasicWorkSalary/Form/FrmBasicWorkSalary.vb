Imports Nts.Freamwork.Common.Utilities

''' <summary>
''' 業務給基本設定画面
''' </summary>
Public Class FrmBasicWorkSalary
    'Inherits Form
    Inherits BaseEditFormBase
    Implements IFrmBasicWorkSalary

#Region "フィールド・プロパティ関連"

    ''' <summary>
    ''' 業務給基本設定画面 Presenter
    ''' </summary>
    Private _presenter As FrmBasicWorkSalaryPresenter

    ''' <summary>
    ''' DataGridView行選択ロック制御クラス
    ''' </summary>
    Public Property LisLocker As DataGridViewSelectionLocker Implements IFrmBasicWorkSalary.LisLocker

    ''' <summary>
    ''' 編集前のオリジナルデータ
    ''' </summary>
    Private _beforeData As New LectureFeeCoefficientModel

    ''' <summary>
    ''' 対象年度ロックユーザー数
    ''' </summary>
    ''' <returns></returns>
    Public Property LockUsersCount As Integer = 0 Implements IFrmBasicWorkSalary.LockUsersCount

    ''' <summary>
    ''' 編集有無判定
    ''' </summary>
    ''' <returns></returns>
    Public Property IsEditChanged As Boolean = False Implements IFrmBasicWorkSalary.IsEditChanged

    ''' <summary>
    ''' 画面起動処理結果
    ''' </summary>
    ''' <returns></returns>
    Public Property IsStartupSuccess As Boolean = True Implements IFrmBasicWorkSalary.IsStartupSuccess

#End Region


#Region "画面項目プロパティ"

    ''' <summary>
    ''' 適用開始日
    ''' </summary>
    ''' <returns></returns>
    Public Property EffectiveDate As String Implements IFrmBasicWorkSalary.EffectiveDate
        Get
            Return txtEffectiveDate.TextBox.Text
        End Get
        Set(value As String)
            txtEffectiveDate.TextBox.Text = value
        End Set
    End Property

    ''' <summary>
    ''' 業務給標準単価
    ''' </summary>
    ''' <returns></returns>
    Public Property TaskBaseUnitPrice As String Implements IFrmBasicWorkSalary.TaskBaseUnitPrice
        Get
            Return txtTaskBaseUnitPrice.TextValue
        End Get
        Set(value As String)
            txtTaskBaseUnitPrice.TextValue = If(StringUtil.IsNullOrWhiteSpace(value), Nothing, value)
        End Set
    End Property

    ''' <summary>
    ''' みなし勤務時間(事前分)
    ''' </summary>
    ''' <returns></returns>
    Public Property DeemedTimeBefore As String Implements IFrmBasicWorkSalary.DeemedTimeBefore
        Get
            Return txtDeemedTimeBefore.TextValue
        End Get
        Set(value As String)
            txtDeemedTimeBefore.TextValue = value
        End Set
    End Property

    ''' <summary>
    ''' みなし勤務時間(事後分)
    ''' </summary>
    ''' <returns></returns>
    Public Property DeemedTimeAfter As String Implements IFrmBasicWorkSalary.DeemedTimeAfter
        Get
            Return txtDeemedTimeAfter.TextValue
        End Get
        Set(value As String)
            txtDeemedTimeAfter.TextValue = value
        End Set
    End Property

    ''' <summary>
    ''' ID（隠し：内部情報）
    ''' </summary>
    Private _id As Long?
    Public Property Id As Long? Implements IFrmBasicWorkSalary.Id
        Get
            Return _id
        End Get
        Set(value As Long?)
            _id = value
        End Set
    End Property

    ''' <summary>
    ''' RowVersion（隠し：内部情報）
    ''' </summary>
    Private _rowversion As Byte()
    Public Property Rowversion As Byte() Implements IFrmBasicWorkSalary.Rowversion
        Get
            Return _rowversion
        End Get
        Set(value As Byte())
            _rowversion = value
        End Set
    End Property

#End Region


#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
        InitializeComponent()

        ' Presenterに自分(Me)とServiceを渡して初期化
        _presenter = New FrmBasicWorkSalaryPresenter(Me)
    End Sub

#End Region


#Region "状態管理メソッド"

    ''' <summary>
    ''' 現在の画面状態
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrentViewState As BasicWorkSalaryControlsDto Implements IFrmBasicWorkSalary.CurrentViewState

    ''' <summary>
    ''' ViewState の状態に応じて UI の Enabled を制御する。
    ''' 活性・非活性の表示制御を行います。
    ''' </summary>
    Public Sub ApplyUIState() Implements IFrmBasicWorkSalary.ApplyUIState

        ' [追加][編集][削除][保存][キャンセル]ボタン
        btnAdd.Enabled = CurrentViewState.btnAdd
        btnEdit.Enabled = CurrentViewState.btnEdit
        btnDelete.Enabled = CurrentViewState.btnDelete
        btnSave.Enabled = CurrentViewState.btnSave
        btnCancel.Enabled = CurrentViewState.btnCancel

        ' データ編集用項目
        txtEffectiveDate.Enabled = CurrentViewState.txtEffectiveDate
        txtTaskBaseUnitPrice.Enabled = CurrentViewState.txtTaskBaseUnitPrice
        txtDeemedTimeBefore.Enabled = CurrentViewState.txtDeemedTimeBefore
        txtDeemedTimeAfter.Enabled = CurrentViewState.txtDeemedTimeAfter

        ' 現在のモード名称
        lblEditMode.Text = CurrentViewState.lblEditModeText
        lblEditMode.BackColor = CurrentViewState.lblEditModeBackColor
        lblEditMode.ForeColor = CurrentViewState.lblEditModeForeColor
    End Sub

#End Region


#Region "画面初期表示"

    ''' <summary>
    ''' Load イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmBasicWorkSalary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            LisLocker = New DataGridViewSelectionLocker(dgvDataList)
        Catch ex As Exception
            GlobalLog.Error(MessageIdConst.MSGID_E9004, ex)
            IsStartupSuccess = False
        End Try
    End Sub

    ''' <summary>
    ''' 現在の利用者数を取得し複数人利用中は更新できない旨をメッセージ表示します。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmBasicWorkSalary_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _presenter.ShownEvent("")
    End Sub

    ''' <summary>
    ''' 対象一覧データのバインディング
    ''' </summary>
    ''' <param name="list"></param>
    Public Sub SetDataListBindingSource(ByVal list As BindingSource) Implements IFrmBasicWorkSalary.SetDataListBindingSource
        'ApplyDataGridViewDefinition()
        Me.dgvDataList.DataSource = list
    End Sub

    ''' <summary>
    ''' DataGridView に一覧データをバインドする。
    ''' </summary>
    Public Sub ApplyDataGridViewDefinition() Implements IFrmBasicWorkSalary.ApplyDataGridViewDefinition
        Dim def As New GridBasicWorkSalaryStyle()
        def.Apply(dgvDataList)
        Dim style As New GridStyleDefinitionDefault()
        style.Apply(dgvDataList)
        DataGridViewHelper.SetupFastGrid(dgvDataList)
    End Sub

    ''' <summary>
    ''' 現在の利用者数を確認します。
    ''' DBエラー等ので取得できなかった場合が発生した場合、-1（マイナス値）を返す
    ''' ここで-1を返却すると基底クラスで起動元画面に戻す。
    ''' </summary>
    ''' <returns></returns>
    Protected Overrides Function CheckBeforeShow() As Boolean
        _presenter.TransitionEvent("")

        ' 画面起動中のエラーあり
        If Not IsStartupSuccess Then
            Return False
        End If

        ' 複数ユーザー利用中
        Return LockUsersCount >= 0
    End Function

#End Region


#Region "ボタン押下イベント処理"

    ''' <summary>
    ''' [追加]ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        _presenter.AddNewEvent("追加")
    End Sub

    ''' <summary>
    ''' [編集]ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        _presenter.EditEvent("編集")
    End Sub

    ''' <summary>
    ''' [削除]ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        _presenter.DeleteEvent("削除")
    End Sub

    ''' <summary>
    ''' [保存]ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        _presenter.SaveEvent("保存")
    End Sub

    ''' <summary>
    ''' [キャンセル]ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        _presenter.CancelEvent("キャンセル")
    End Sub

    ''' <summary>
    ''' [一覧]ダブルクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub dgvDataList_CellDoubleClick(sender As Object, e As EventArgs) Handles dgvDataList.CellDoubleClick
        _presenter.DataListEvent("一覧")
    End Sub

#End Region


#Region "画面共通部品"

    ''' <summary>
    ''' 選択カーソルを設定する。
    ''' </summary>
    Public Sub SetSelectedCursor(ByVal rowIndex As Integer) Implements IFrmBasicWorkSalary.SetSelectedCursor
        If dgvDataList.Rows.Count > 0 Then
            dgvDataList.CurrentCell = dgvDataList.Rows(rowIndex).Cells(0)
            dgvDataList.Rows(rowIndex).Selected = True
        End If
    End Sub

    ''' <summary>
    ''' 画面名（タイトル文言）取得
    ''' </summary>
    ''' <returns></returns>
    Protected Function WindowCaption() As String Implements IFrmBasicWorkSalary.WindowCaption
        Return Me.Text
    End Function

#End Region


#Region "基底クラスから呼び出されるメソッド"

    ''' <summary>
    ''' データ編集有無判定
    ''' </summary>
    ''' <returns></returns>
    Public Overrides Function IsEditChange() As Boolean
        _presenter.ClosingEvent("")
        Return IsEditChanged
    End Function

#End Region

    Private Sub RefreshAndFocusRow(effectiveDate As String) Implements IFrmBasicWorkSalary.RefreshAndFocusRow
        Dim conditions As New Dictionary(Of String, Object)()
        conditions.Add("effectiveDate", effectiveDate)

        DataGridViewHelper.RefreshAndFocusRow(Me, dgvDataList, conditions)
    End Sub

End Class