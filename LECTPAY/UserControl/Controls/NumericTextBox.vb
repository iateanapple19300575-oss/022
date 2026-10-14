Imports System.ComponentModel
Imports System.Text

''' <summary>
''' 数字用テキストボックスコントロール
''' </summary>
<DefaultEvent("ValueChanged")>
Public Class NumericTextBox

#Region "フィールド変数関連"

    Private _isInternal As Boolean = False


    Public Event ValueChanged As EventHandler

#End Region

#Region "デザイナー対応プロパティ"

    <Browsable(True)>
    <Category("Behavior")>
    <Description("負数を許可するかどうか")>
    <DefaultValue(False)>
    Public Property AllowNegative As Boolean = False

    <Browsable(True)>
    <Category("Behavior")>
    <Description("小数点を許可するかどうか")>
    <DefaultValue(False)>
    Public Property AllowDecimal As Boolean = False

    <Browsable(True)>
    <Category("Behavior")>
    <Description("整数部の最大桁数")>
    <DefaultValue(10)>
    Public Property MaxIntegerDigits As Integer = 10

    <Browsable(True)>
    <Category("Behavior")>
    <Description("小数部の最大桁数")>
    <DefaultValue(2)>
    Public Property MaxDecimalDigits As Integer = 2

    <Browsable(True)>
    <Category("Behavior")>
    <Description("空欄を Nothing として扱うかどうか")>
    <DefaultValue(True)>
    Public Property AllowNull As Boolean = True

    <Browsable(True)>
    <Category("Behavior")>
    <Description("カンマ区切りで表示するかどうか")>
    <DefaultValue(True)>
    Public Property UseCommaFormat As Boolean = True

    <Browsable(True)>
    <Category("Behavior")>
    <Description("最小値")>
    Public Property MinimumValue As Decimal = Decimal.MinValue

    <Browsable(True)>
    <Category("Behavior")>
    <Description("最大値")>
    Public Property MaximumValue As Decimal = Decimal.MaxValue

    <Browsable(True)>
    <Category("Data")>
    <Description("数値")>
    Private _decimalVvalue As Decimal? = Nothing
    Public Property Value As Decimal?
        Get
            CommitValue()
            Return _decimalVvalue
        End Get
        Set(value As Decimal?)
            _decimalVvalue = value
            UpdateFormattedText()
            RaiseEvent ValueChanged(Me, EventArgs.Empty)
        End Set
    End Property

    ''' <summary>
    ''' 文字列としての値
    ''' </summary>
    ''' <returns></returns>
    Public Property TextValue As String
        Get
            CommitValue()
            Return If(_decimalVvalue.HasValue, TextBox.Text, "")
        End Get
        Set(value As String)
            If String.IsNullOrEmpty(value) Then
                _decimalVvalue = Nothing
            Else
                Dim decValue As Decimal
                If Decimal.TryParse(value, decValue) Then
                    _decimalVvalue = decValue
                Else
                    _decimalVvalue = Nothing
                End If
            End If
            UpdateFormattedText()
        End Set
    End Property

    ''' <summary>
    ''' Enabledプロパティ
    ''' </summary>
    Public Overloads WriteOnly Property Enabled As Boolean
        Set(value As Boolean)
            TextBox.Enabled = value
            If value Then
                pnl.BackColor = Color.White
            Else
                pnl.BackColor = SystemColors.Control
            End If
        End Set
    End Property

    ''' <summary>
    ''' 未確定入力を確定します。
    ''' </summary>
    Public Sub CommitValue()
        Dim txt As String = TextBox.Text

        If String.IsNullOrEmpty(txt) Then
            _decimalVvalue = If(AllowNull, CType(Nothing, Decimal?), 0D)
        Else
            Dim decValue As Decimal
            If Decimal.TryParse(txt, decValue) Then
                If decValue < MinimumValue Then decValue = MinimumValue
                If decValue > MaximumValue Then decValue = MaximumValue
                _decimalVvalue = decValue
            Else
                _decimalVvalue = 0D
            End If
        End If

        UpdateFormattedText()
    End Sub

    ''' <summary>
    ''' フォーカス移動時の表示データを編集します。
    ''' </summary>
    Private Sub UpdateFormattedText()
        _isInternal = True

        If Not _decimalVvalue.HasValue Then
            TextBox.Text = ""
        Else
            If UseCommaFormat Then
                Dim fmt As String = If(AllowDecimal, "N" & MaxDecimalDigits, "N0")
                TextBox.Text = _decimalVvalue.Value.ToString(fmt)
            Else
                TextBox.Text = RawNumberString(_decimalVvalue.Value)
            End If
        End If

        _isInternal = False
    End Sub

    ''' <summary>
    ''' 小数値、整数値を文字列で返します。
    ''' </summary>
    ''' <param name="v"></param>
    ''' <returns></returns>
    Private Function RawNumberString(v As Decimal) As String
        If AllowDecimal Then
            Return v.ToString("0." & New String("0"c, MaxDecimalDigits))
        Else
            Return v.ToString("0")
        End If
    End Function

#End Region

#Region "入力制御"

    ''' <summary>
    ''' KeyPressイベント（文字として入力されるキーが押下されたとき）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox.KeyPress
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        If Char.IsDigit(e.KeyChar) Then
            Return
        End If

        If AllowNegative AndAlso e.KeyChar = "-"c Then
            If TextBox.SelectionStart = 0 AndAlso Not TextBox.Text.StartsWith("-") Then
                Return
            End If
        End If

        If AllowDecimal AndAlso e.KeyChar = "."c Then
            If Not TextBox.Text.Contains(".") Then
                Return
            End If
        End If

        e.Handled = True
    End Sub

    ''' <summary>
    ''' TextChangedイベント（テキスト内容が変化したとき）
    ''' 入力文字数制限の処理を行います。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles TextBox.TextChanged
        If _isInternal Then
            Return
        End If

        Dim raw As String = TextBox.Text
        Dim sb As New StringBuilder()

        Dim hasMinus As Boolean = False
        Dim hasDot As Boolean = False
        Dim intCount As Integer = 0
        Dim decCount As Integer = 0

        For i As Integer = 0 To raw.Length - 1
            Dim ch = raw(i)

            If ch = "-"c AndAlso AllowNegative AndAlso i = 0 Then
                hasMinus = True
                sb.Append(ch)
                Continue For
            End If

            If ch = "."c AndAlso AllowDecimal AndAlso Not hasDot Then
                hasDot = True
                sb.Append(ch)
                Continue For
            End If

            If Char.IsDigit(ch) Then
                If Not hasDot Then
                    If intCount < MaxIntegerDigits Then
                        sb.Append(ch)
                        intCount += 1
                    End If
                Else
                    If decCount < MaxDecimalDigits Then
                        sb.Append(ch)
                        decCount += 1
                    End If
                End If
            End If
        Next

        ' TODO: DEL Or BS キー押下時の動作要検討のため保留
        ' 小数点自動付加
        'If AllowDecimal AndAlso Not hasDot AndAlso intCount = MaxIntegerDigits Then
        '    sb.Append("."c)
        '    hasDot = True
        'End If

        _isInternal = True
        TextBox.Text = sb.ToString()
        TextBox.SelectionStart = TextBox.Text.Length
        _isInternal = False
    End Sub

    ''' <summary>
    ''' Enter イベント（フォーカスが入ったとき）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox_Enter(sender As Object, e As EventArgs) Handles TextBox.Enter
        If _decimalVvalue.HasValue Then
            _isInternal = True
            TextBox.Text = RawNumberString(_decimalVvalue.Value)
            TextBox.SelectionStart = TextBox.Text.Length
            _isInternal = False
        End If
    End Sub

    ''' <summary>
    ''' Leaveイベント（フォーカスが抜けたとき）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox_Leave(sender As Object, e As EventArgs) Handles TextBox.Leave
        CommitValue()
        RaiseEvent ValueChanged(Me, EventArgs.Empty)
    End Sub

    ''' <summary>
    ''' キー押下(KeyDown)イベント（キーが押下された瞬間）
    ''' ENTERキー押下で次の項目へ遷移する。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' ピッ音を消す
            e.SuppressKeyPress = True
            Me.Parent.SelectNextControl(Me, True, True, True, True)
            e.Handled = True
        End If
    End Sub

#End Region

End Class
