''' <summary>
''' 業務給基本設定画面 インターフェース
''' </summary>
Public Interface IFrmBasicWorkSalary
    Inherits IMasterEditFormBase(Of BasicWorkSalaryControlsDto)

    '--- 編集用項目 ---

    ''' <summary>適用開始日</summary>
    Property EffectiveDate As String

    ''' <summary>業務給標準単価</summary>
    Property TaskBaseUnitPrice As String

    ''' <summary>みなし勤務時間(事前分)</summary>
    Property DeemedTimeBefore As String

    ''' <summary>みなし勤務時間(事後分)</summary>
    Property DeemedTimeAfter As String


    '--- イベント定義 ---

    '--- メソッド ---
    Sub RefreshAndFocusRow(EffectiveDate As String)

End Interface
