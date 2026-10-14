<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class NumericTextBox
    Inherits System.Windows.Forms.UserControl

    'UserControl はコンポーネント一覧をクリーンアップするために dispose をオーバーライドします。
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
        Me.TextBox = New System.Windows.Forms.TextBox()
        Me.pnl = New System.Windows.Forms.Panel()
        Me.pnl.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBox
        '
        Me.TextBox.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox.Font = New System.Drawing.Font("游ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TextBox.Location = New System.Drawing.Point(5, 6)
        Me.TextBox.Margin = New System.Windows.Forms.Padding(5, 5, 5, 0)
        Me.TextBox.Name = "TextBox"
        Me.TextBox.Size = New System.Drawing.Size(45, 21)
        Me.TextBox.TabIndex = 1
        Me.TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'pnl
        '
        Me.pnl.BackColor = System.Drawing.Color.White
        Me.pnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnl.Controls.Add(Me.TextBox)
        Me.pnl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl.Font = New System.Drawing.Font("游ゴシック", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnl.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.pnl.Location = New System.Drawing.Point(0, 0)
        Me.pnl.Margin = New System.Windows.Forms.Padding(4)
        Me.pnl.Name = "pnl"
        Me.pnl.Padding = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.pnl.Size = New System.Drawing.Size(57, 28)
        Me.pnl.TabIndex = 0
        '
        'NumericTextBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnl)
        Me.Font = New System.Drawing.Font("游ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "NumericTextBox"
        Me.Size = New System.Drawing.Size(57, 28)
        Me.pnl.ResumeLayout(False)
        Me.pnl.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents TextBox As TextBox
    Friend WithEvents pnl As Panel
End Class
