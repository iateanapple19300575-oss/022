''' <summary>
''' マスタ画面全体の状態を保持する ViewState。
''' ・一覧データ
''' ・選択 ID
''' ・モード
''' ・UI 有効／無効判定
''' を BaseViewState から継承して管理する。
''' </summary>
Public Class BasicWorkSalaryViewState
    Inherits MasterEditViewStateBase

    ''' <summary>テキストボックス項目有効判定</summary>
    Public Function IsTextFieldEnabled() As Boolean
        Return Mode = MasterEditMode.Add OrElse Mode = MasterEditMode.Edit OrElse Mode = MasterEditMode.Delete
    End Function

End Class