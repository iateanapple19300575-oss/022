Imports Nts.Freamwork.Common.Utilities

Namespace MasterEdit.Dto

    ''' <summary>
    ''' 業務給基本設定 Validator クラス
    ''' </summary>
    Public Class BasicWorkSalaryValidator
        Implements IMasterEditValidator(Of BasicWorkSalaryModel)

    '' <summary>
    '' 保存要求のバリデーションを行う。
    '' </summary>
        Public Function Validate(model As BasicWorkSalaryModel) As ValidationResult _
            Implements IMasterEditValidator(Of BasicWorkSalaryModel).Validate

            Dim result As New ValidationResult
        Dim errors As New List(Of String)

        ' --- 必須チェック ---

            ' 適用開始日
            If StringUtil.IsNullOrWhiteSpace(model.EffectiveDate) Then
                result.ValidationErrors.Add("適用開始日は必須です。")
                Return result
        End If

            ' 標準単価
            If StringUtil.IsNullOrWhiteSpace(model.TaskBaseUnitPrice) Then
                result.ValidationErrors.Add("標準単価は必須です。")
                Return result
        End If

            ' みなし勤務時間（授業前）
            If StringUtil.IsNullOrWhiteSpace(model.DeemedTimeBefore) Then
                result.ValidationErrors.Add("みなし勤務時間（授業前）は必須です。")
                Return result
        End If

            ' みなし時間（授業後）
            If StringUtil.IsNullOrWhiteSpace(model.DeemedTimeAfter) Then
                result.ValidationErrors.Add("みなし勤務時間（授業後）は必須です。")
                Return result
        End If

        ' 必須エラーがある場合はここで終了
            If result.ValidationErrors.Count > 0 Then
                Return result
        End If

        ' --- 形式チェック（TryParse） ---
        Dim chkDate As Date
        Dim chkNumeric As Integer

            ' 適用開始日
        If Not Date.TryParse(model.EffectiveDate, chkDate) Then
                result.ValidationErrors.Add("適用開始日の形式が不正です。（例：YYYY/MM/DD）")
        End If

            ' 標準単価
        If Not Integer.TryParse(model.TaskBaseUnitPrice, chkNumeric) Then
                result.ValidationErrors.Add("標準単価の入力形式が不正です。（例：2000）")
        End If

            ' みなし勤務時間（授業前）
        If Not Integer.TryParse(model.DeemedTimeBefore, chkNumeric) Then
                result.ValidationErrors.Add("みなし勤務時間（授業前）の入力形式が不正です。（例：20）")
        End If

            ' みなし勤務時間（授業後）
        If Not Integer.TryParse(model.DeemedTimeAfter, chkNumeric) Then
                result.ValidationErrors.Add("みなし勤務時間（授業後）の入力形式が不正です。（例：20）")
        End If

        ' 形式エラーがある場合はここで終了
        If errors.Count > 0 Then
                Return result
        End If

            Return result
    End Function

    End Class

End Namespace
