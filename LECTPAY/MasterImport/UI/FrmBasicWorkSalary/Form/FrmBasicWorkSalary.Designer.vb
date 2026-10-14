<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBasicWorkSalary
    'Inherits Form
    Inherits BaseEditFormBase
    Implements IFrmBasicWorkSalary

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dgvDataList = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.tlpMainPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlRightPanel = New System.Windows.Forms.Panel()
        Me.tlpRightInPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.lblEditMode = New System.Windows.Forms.Label()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.pnlLeftPanel = New System.Windows.Forms.Panel()
        Me.tlpLeftInPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlLeftRow2Panel = New System.Windows.Forms.Panel()
        Me.txtDeemedTimeAfter = New UserControl.NumericTextBox()
        Me.txtDeemedTimeBefore = New UserControl.NumericTextBox()
        Me.txtTaskBaseUnitPrice = New UserControl.NumericTextBox()
        Me.txtEffectiveDate = New UserControl.DateMaskedTextBox()
        Me.pnlLeftRow1Panel = New System.Windows.Forms.Panel()
        CType(Me.dgvDataList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpMainPanel.SuspendLayout()
        Me.pnlRightPanel.SuspendLayout()
        Me.tlpRightInPanel.SuspendLayout()
        Me.pnlLeftPanel.SuspendLayout()
        Me.tlpLeftInPanel.SuspendLayout()
        Me.pnlLeftRow2Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(159, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(114, 20)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "標準単価(1時間)"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dgvDataList
        '
        Me.dgvDataList.AllowUserToAddRows = False
        Me.dgvDataList.AllowUserToDeleteRows = False
        Me.dgvDataList.AllowUserToResizeColumns = False
        Me.dgvDataList.AllowUserToResizeRows = False
        Me.dgvDataList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDataList.Location = New System.Drawing.Point(12, 118)
        Me.dgvDataList.Margin = New System.Windows.Forms.Padding(2, 10, 2, 2)
        Me.dgvDataList.MultiSelect = False
        Me.dgvDataList.Name = "dgvDataList"
        Me.dgvDataList.ReadOnly = True
        Me.dgvDataList.RowHeadersWidth = 20
        Me.dgvDataList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvDataList.RowTemplate.Height = 21
        Me.dgvDataList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDataList.Size = New System.Drawing.Size(520, 332)
        Me.dgvDataList.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(10, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "適用開始日"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(317, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(150, 20)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "みなし勤務時間"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(322, 28)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 20)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "授業前"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(3, 238)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(100, 30)
        Me.btnAdd.TabIndex = 3
        Me.btnAdd.Text = "追加"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(416, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 20)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "授業後"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Location = New System.Drawing.Point(377, 49)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(30, 28)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "分"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Location = New System.Drawing.Point(233, 49)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(31, 28)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "円"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Location = New System.Drawing.Point(469, 49)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(30, 28)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "分"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(3, 158)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 30)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "保存"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(3, 278)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(100, 30)
        Me.btnEdit.TabIndex = 4
        Me.btnEdit.Text = "編集"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(3, 118)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 30)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "キャンセル"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'tlpMainPanel
        '
        Me.tlpMainPanel.ColumnCount = 2
        Me.tlpMainPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMainPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpMainPanel.Controls.Add(Me.pnlRightPanel, 1, 0)
        Me.tlpMainPanel.Controls.Add(Me.pnlLeftPanel, 0, 0)
        Me.tlpMainPanel.Location = New System.Drawing.Point(0, 0)
        Me.tlpMainPanel.Name = "tlpMainPanel"
        Me.tlpMainPanel.RowCount = 1
        Me.tlpMainPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMainPanel.Size = New System.Drawing.Size(672, 466)
        Me.tlpMainPanel.TabIndex = 0
        '
        'pnlRightPanel
        '
        Me.pnlRightPanel.Controls.Add(Me.tlpRightInPanel)
        Me.pnlRightPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRightPanel.Location = New System.Drawing.Point(559, 3)
        Me.pnlRightPanel.Name = "pnlRightPanel"
        Me.pnlRightPanel.Size = New System.Drawing.Size(110, 460)
        Me.pnlRightPanel.TabIndex = 1
        '
        'tlpRightInPanel
        '
        Me.tlpRightInPanel.ColumnCount = 1
        Me.tlpRightInPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpRightInPanel.Controls.Add(Me.lblEditMode, 0, 0)
        Me.tlpRightInPanel.Controls.Add(Me.btnEdit, 0, 5)
        Me.tlpRightInPanel.Controls.Add(Me.btnAdd, 0, 4)
        Me.tlpRightInPanel.Controls.Add(Me.btnDelete, 0, 6)
        Me.tlpRightInPanel.Controls.Add(Me.btnSave, 0, 2)
        Me.tlpRightInPanel.Controls.Add(Me.btnCancel, 0, 1)
        Me.tlpRightInPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpRightInPanel.Location = New System.Drawing.Point(0, 0)
        Me.tlpRightInPanel.Name = "tlpRightInPanel"
        Me.tlpRightInPanel.RowCount = 7
        Me.tlpRightInPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
        Me.tlpRightInPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.tlpRightInPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.tlpRightInPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.tlpRightInPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.tlpRightInPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.tlpRightInPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.tlpRightInPanel.Size = New System.Drawing.Size(110, 460)
        Me.tlpRightInPanel.TabIndex = 0
        '
        'lblEditMode
        '
        Me.lblEditMode.BackColor = System.Drawing.SystemColors.Control
        Me.lblEditMode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEditMode.Font = New System.Drawing.Font("游ゴシック", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblEditMode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEditMode.Location = New System.Drawing.Point(3, 10)
        Me.lblEditMode.Margin = New System.Windows.Forms.Padding(3, 10, 3, 0)
        Me.lblEditMode.Name = "lblEditMode"
        Me.lblEditMode.Size = New System.Drawing.Size(100, 40)
        Me.lblEditMode.TabIndex = 0
        Me.lblEditMode.Text = "通常モード"
        Me.lblEditMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(3, 318)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 30)
        Me.btnDelete.TabIndex = 5
        Me.btnDelete.Text = "削除"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'pnlLeftPanel
        '
        Me.pnlLeftPanel.Controls.Add(Me.tlpLeftInPanel)
        Me.pnlLeftPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftPanel.Location = New System.Drawing.Point(3, 3)
        Me.pnlLeftPanel.Name = "pnlLeftPanel"
        Me.pnlLeftPanel.Size = New System.Drawing.Size(550, 460)
        Me.pnlLeftPanel.TabIndex = 0
        '
        'tlpLeftInPanel
        '
        Me.tlpLeftInPanel.ColumnCount = 2
        Me.tlpLeftInPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.tlpLeftInPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpLeftInPanel.Controls.Add(Me.pnlLeftRow2Panel, 1, 1)
        Me.tlpLeftInPanel.Controls.Add(Me.pnlLeftRow1Panel, 1, 0)
        Me.tlpLeftInPanel.Controls.Add(Me.dgvDataList, 1, 2)
        Me.tlpLeftInPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpLeftInPanel.Location = New System.Drawing.Point(0, 0)
        Me.tlpLeftInPanel.Name = "tlpLeftInPanel"
        Me.tlpLeftInPanel.RowCount = 3
        Me.tlpLeftInPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpLeftInPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpLeftInPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpLeftInPanel.Size = New System.Drawing.Size(550, 460)
        Me.tlpLeftInPanel.TabIndex = 0
        '
        'pnlLeftRow2Panel
        '
        Me.pnlLeftRow2Panel.Controls.Add(Me.txtDeemedTimeAfter)
        Me.pnlLeftRow2Panel.Controls.Add(Me.txtDeemedTimeBefore)
        Me.pnlLeftRow2Panel.Controls.Add(Me.txtTaskBaseUnitPrice)
        Me.pnlLeftRow2Panel.Controls.Add(Me.txtEffectiveDate)
        Me.pnlLeftRow2Panel.Controls.Add(Me.Label1)
        Me.pnlLeftRow2Panel.Controls.Add(Me.Label6)
        Me.pnlLeftRow2Panel.Controls.Add(Me.Label3)
        Me.pnlLeftRow2Panel.Controls.Add(Me.Label8)
        Me.pnlLeftRow2Panel.Controls.Add(Me.Label4)
        Me.pnlLeftRow2Panel.Controls.Add(Me.Label7)
        Me.pnlLeftRow2Panel.Controls.Add(Me.Label5)
        Me.pnlLeftRow2Panel.Controls.Add(Me.Label2)
        Me.pnlLeftRow2Panel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftRow2Panel.Location = New System.Drawing.Point(12, 16)
        Me.pnlLeftRow2Panel.Margin = New System.Windows.Forms.Padding(2)
        Me.pnlLeftRow2Panel.Name = "pnlLeftRow2Panel"
        Me.pnlLeftRow2Panel.Size = New System.Drawing.Size(536, 90)
        Me.pnlLeftRow2Panel.TabIndex = 1
        '
        'txtDeemedTimeAfter
        '
        Me.txtDeemedTimeAfter.Font = New System.Drawing.Font("游ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDeemedTimeAfter.Location = New System.Drawing.Point(411, 49)
        Me.txtDeemedTimeAfter.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDeemedTimeAfter.MaximumValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtDeemedTimeAfter.MinimumValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtDeemedTimeAfter.Name = "txtDeemedTimeAfter"
        Me.txtDeemedTimeAfter.Size = New System.Drawing.Size(57, 28)
        Me.txtDeemedTimeAfter.TabIndex = 10
        Me.txtDeemedTimeAfter.Value = Nothing
        '
        'txtDeemedTimeBefore
        '
        Me.txtDeemedTimeBefore.Font = New System.Drawing.Font("游ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDeemedTimeBefore.Location = New System.Drawing.Point(317, 49)
        Me.txtDeemedTimeBefore.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDeemedTimeBefore.MaximumValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtDeemedTimeBefore.MinimumValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtDeemedTimeBefore.Name = "txtDeemedTimeBefore"
        Me.txtDeemedTimeBefore.Size = New System.Drawing.Size(57, 28)
        Me.txtDeemedTimeBefore.TabIndex = 7
        Me.txtDeemedTimeBefore.Value = Nothing
        '
        'txtTaskBaseUnitPrice
        '
        Me.txtTaskBaseUnitPrice.Font = New System.Drawing.Font("游ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTaskBaseUnitPrice.Location = New System.Drawing.Point(175, 49)
        Me.txtTaskBaseUnitPrice.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTaskBaseUnitPrice.MaximumValue = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtTaskBaseUnitPrice.MinimumValue = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtTaskBaseUnitPrice.Name = "txtTaskBaseUnitPrice"
        Me.txtTaskBaseUnitPrice.Size = New System.Drawing.Size(57, 28)
        Me.txtTaskBaseUnitPrice.TabIndex = 3
        Me.txtTaskBaseUnitPrice.Value = Nothing
        '
        'txtEffectiveDate
        '
        Me.txtEffectiveDate.DateValue = Nothing
        Me.txtEffectiveDate.Font = New System.Drawing.Font("游ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEffectiveDate.Location = New System.Drawing.Point(10, 49)
        Me.txtEffectiveDate.Margin = New System.Windows.Forms.Padding(4)
        Me.txtEffectiveDate.MaskText = "0000/00/00"
        Me.txtEffectiveDate.Name = "txtEffectiveDate"
        Me.txtEffectiveDate.Size = New System.Drawing.Size(100, 28)
        Me.txtEffectiveDate.TabIndex = 1
        '
        'pnlLeftRow1Panel
        '
        Me.pnlLeftRow1Panel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftRow1Panel.Location = New System.Drawing.Point(12, 2)
        Me.pnlLeftRow1Panel.Margin = New System.Windows.Forms.Padding(2)
        Me.pnlLeftRow1Panel.Name = "pnlLeftRow1Panel"
        Me.pnlLeftRow1Panel.Size = New System.Drawing.Size(536, 10)
        Me.pnlLeftRow1Panel.TabIndex = 0
        '
        'FrmBasicWorkSalary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(672, 471)
        Me.Controls.Add(Me.tlpMainPanel)
        Me.Font = New System.Drawing.Font("游ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmBasicWorkSalary"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "業務給基本設定"
        CType(Me.dgvDataList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpMainPanel.ResumeLayout(False)
        Me.pnlRightPanel.ResumeLayout(False)
        Me.tlpRightInPanel.ResumeLayout(False)
        Me.pnlLeftPanel.ResumeLayout(False)
        Me.tlpLeftInPanel.ResumeLayout(False)
        Me.pnlLeftRow2Panel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label3 As Label
    Friend WithEvents dgvDataList As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents btnAdd As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents btnSave As Button
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents tlpMainPanel As TableLayoutPanel
    Friend WithEvents pnlRightPanel As Panel
    Friend WithEvents pnlLeftPanel As Panel
    Friend WithEvents tlpRightInPanel As TableLayoutPanel
    Friend WithEvents tlpLeftInPanel As TableLayoutPanel
    Friend WithEvents pnlLeftRow2Panel As Panel
    Friend WithEvents pnlLeftRow1Panel As Panel
    Friend WithEvents txtEffectiveDate As UserControl.DateMaskedTextBox
    Friend WithEvents txtTaskBaseUnitPrice As UserControl.NumericTextBox
    Friend WithEvents txtDeemedTimeBefore As UserControl.NumericTextBox
    Friend WithEvents txtDeemedTimeAfter As UserControl.NumericTextBox
    Friend WithEvents btnDelete As Button
    Friend WithEvents lblEditMode As Label
End Class
