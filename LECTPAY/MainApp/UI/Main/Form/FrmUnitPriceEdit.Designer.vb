<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUnitPriceEdit
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtTaskUnitPrice = New UserControl.NumericTextBox()
        Me.txtLectureUnitPrice = New UserControl.NumericTextBox()
        Me.dtpApplyDate = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnSetting1 = New System.Windows.Forms.Button()
        Me.btnSetting2 = New System.Windows.Forms.Button()
        Me.btnSetting3 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(219, 126)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(24, 20)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "円"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(219, 76)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(24, 20)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "円"
        '
        'txtTaskUnitPrice
        '
        Me.txtTaskUnitPrice.Font = New System.Drawing.Font("游ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTaskUnitPrice.Location = New System.Drawing.Point(131, 120)
        Me.txtTaskUnitPrice.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtTaskUnitPrice.MaximumValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtTaskUnitPrice.MinimumValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtTaskUnitPrice.Name = "txtTaskUnitPrice"
        Me.txtTaskUnitPrice.Size = New System.Drawing.Size(78, 32)
        Me.txtTaskUnitPrice.TabIndex = 14
        Me.txtTaskUnitPrice.Value = Nothing
        '
        'txtLectureUnitPrice
        '
        Me.txtLectureUnitPrice.Font = New System.Drawing.Font("游ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtLectureUnitPrice.Location = New System.Drawing.Point(131, 70)
        Me.txtLectureUnitPrice.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.txtLectureUnitPrice.MaxDecimalDigits = 0
        Me.txtLectureUnitPrice.MaximumValue = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.txtLectureUnitPrice.MinimumValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtLectureUnitPrice.Name = "txtLectureUnitPrice"
        Me.txtLectureUnitPrice.Size = New System.Drawing.Size(78, 32)
        Me.txtLectureUnitPrice.TabIndex = 11
        Me.txtLectureUnitPrice.Value = Nothing
        '
        'dtpApplyDate
        '
        Me.dtpApplyDate.CustomFormat = "yyyy/MM/dd"
        Me.dtpApplyDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpApplyDate.Location = New System.Drawing.Point(131, 20)
        Me.dtpApplyDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpApplyDate.MaxDate = New Date(2999, 12, 31, 0, 0, 0, 0)
        Me.dtpApplyDate.Name = "dtpApplyDate"
        Me.dtpApplyDate.Size = New System.Drawing.Size(120, 32)
        Me.dtpApplyDate.TabIndex = 9
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(30, 126)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(84, 20)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "業務給単価"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(30, 76)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 20)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "授業単価"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(30, 26)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 20)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "適用年月日"
        '
        'btnSetting1
        '
        Me.btnSetting1.Location = New System.Drawing.Point(335, 21)
        Me.btnSetting1.Name = "btnSetting1"
        Me.btnSetting1.Size = New System.Drawing.Size(113, 30)
        Me.btnSetting1.TabIndex = 16
        Me.btnSetting1.Text = "設定"
        Me.btnSetting1.UseVisualStyleBackColor = True
        '
        'btnSetting2
        '
        Me.btnSetting2.Location = New System.Drawing.Point(335, 70)
        Me.btnSetting2.Name = "btnSetting2"
        Me.btnSetting2.Size = New System.Drawing.Size(113, 30)
        Me.btnSetting2.TabIndex = 17
        Me.btnSetting2.Text = "設定"
        Me.btnSetting2.UseVisualStyleBackColor = True
        '
        'btnSetting3
        '
        Me.btnSetting3.Location = New System.Drawing.Point(335, 122)
        Me.btnSetting3.Name = "btnSetting3"
        Me.btnSetting3.Size = New System.Drawing.Size(113, 30)
        Me.btnSetting3.TabIndex = 18
        Me.btnSetting3.Text = "設定"
        Me.btnSetting3.UseVisualStyleBackColor = True
        '
        'FrmUnitPriceEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 181)
        Me.Controls.Add(Me.btnSetting3)
        Me.Controls.Add(Me.btnSetting2)
        Me.Controls.Add(Me.btnSetting1)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtTaskUnitPrice)
        Me.Controls.Add(Me.txtLectureUnitPrice)
        Me.Controls.Add(Me.dtpApplyDate)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Font = New System.Drawing.Font("游ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "FrmUnitPriceEdit"
        Me.Text = "FrmUnitPriceEdit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents txtTaskUnitPrice As UserControl.NumericTextBox
    Friend WithEvents txtLectureUnitPrice As UserControl.NumericTextBox
    Friend WithEvents dtpApplyDate As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents btnSetting1 As Button
    Friend WithEvents btnSetting2 As Button
    Friend WithEvents btnSetting3 As Button
End Class
