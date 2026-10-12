Public Class FrmUnitPriceEdit
    Private Sub btnSetting1_Click(sender As Object, e As EventArgs) Handles btnSetting1.Click
        '' MDI親画面を取得（自身がMDI子画面、または所有されている前提）
        'Dim mdiParentForm As Form = Me.MdiParent

        '' もし自身がMDI子画面ではない独立したウインドウの場合は、My.Application.OpenForms などから親を探します
        'If mdiParentForm Is Nothing AndAlso Me.Owner IsNot Nothing Then
        '    mdiParentForm = Me.Owner.MdiParent
        'End If

        'If mdiParentForm Is Nothing Then Exit Sub

        '' 現在アクティブになっているMDI子画面を取得
        'Dim activeChild As Form = mdiParentForm.ActiveMdiChild

        '' アクティブな画面がに対象の子画面（FormChild）かチェック
        'If activeChild IsNot Nothing AndAlso TypeOf activeChild Is FrmFixedPricePerInstructor Then
        '    Dim targetForm As FrmFixedPricePerInstructor = DirectCast(activeChild, FrmFixedPricePerInstructor)

        '    ' ツール画面の入力項目（例：TextBox1）の値を渡して一括設定
        '    Dim inputValue As String = dtpApplyDate.Value.ToString("yyyy/MM/dd")
        '    targetForm.SetValueToSelectedCells(inputValue)
        'End If

    End Sub

    Private Sub btnSetting2_Click(sender As Object, e As EventArgs) Handles btnSetting2.Click
        Dim targetForm As Form = Nothing

        ' 開いている画面の中から、画面の「クラス名」を文字列で指定して MDI子画面 を探す
        ' （これなら別プロジェクトのクラスでも参照エラーになりません）
        For Each f As Form In Application.OpenForms
            ' 実際のMDI子画面のクラス名（大文字小文字を区別）
            If f.GetType().Name = "FrmBasicWorkSalary" Or f.GetType().Name = "あなたのMDI子画面クラス名" Then
                targetForm = f
                Exit For
            End If
        Next

        ' 見つかったら一括設定を実行
        If targetForm IsNot Nothing Then
            ' リフレクションで、MDI子画面側に作った「SetValueToSelectedCells」メソッドを呼び出す
            Dim method As System.Reflection.MethodInfo = targetForm.GetType().GetMethod("SetValueToSelectedCells")

            If method IsNot Nothing Then
                Dim inputValue As String = txtLectureUnitPrice.Value.ToString()
                ' 引数としてテキストボックスの値を配列で渡す
                method.Invoke(targetForm, New Object() {inputValue})
            Else
                MessageBox.Show("子画面に 'SetValueToSelectedCells' メソッドが見つかりません。")
            End If
        Else
            MessageBox.Show("対象のMDI子画面が開かれていません。")
        End If
    End Sub

    Private Sub btnSetting3_Click(sender As Object, e As EventArgs) Handles btnSetting3.Click
        '' MDI親画面を取得（自身がMDI子画面、または所有されている前提）
        'Dim mdiParentForm As Form = Me.MdiParent

        '' もし自身がMDI子画面ではない独立したウインドウの場合は、My.Application.OpenForms などから親を探します
        'If mdiParentForm Is Nothing AndAlso Me.Owner IsNot Nothing Then
        '    mdiParentForm = Me.Owner.MdiParent
        'End If

        'If mdiParentForm Is Nothing Then Exit Sub

        '' 現在アクティブになっているMDI子画面を取得
        'Dim activeChild As Form = mdiParentForm.ActiveMdiChild

        '' アクティブな画面がに対象の子画面（FormChild）かチェック
        'If activeChild IsNot Nothing AndAlso TypeOf activeChild Is FrmFixedPricePerInstructor Then
        '    Dim targetForm As FrmFixedPricePerInstructor = DirectCast(activeChild, FrmFixedPricePerInstructor)

        '    ' ツール画面の入力項目（例：TextBox1）の値を渡して一括設定
        '    Dim inputValue As String = txtTaskUnitPrice.Value.ToString()
        '    targetForm.SetValueToSelectedCells(inputValue)
        'End If

    End Sub
End Class