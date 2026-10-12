''' <summary>
''' ヒットテスト用定数
''' </summary>
Public NotInheritable Class HitTestConstants

    '---------------------------------------------
    ' マウスカーソルUI領域位置判定
    '---------------------------------------------

    '''<summary>初期値</summary> 
    Public Const HT_NONE As Integer = 0

    '''<summary>クライアント領域</summary> 
    Public Const HT_CLIENT As UInteger = 1

    '''<summary>タイトルバー領域</summary> 
    Public Const HT_CAPTION As UInteger = 2

    '''<summary>左端：⇔</summary> 
    Public Const HT_LEFT As Integer = 10

    '''<summary>右端：⇔</summary> 
    Public Const HT_RIGHT As Integer = 11

    '''<summary>上端：⇕</summary> 
    Public Const HT_TOP As Integer = 12

    '''<summary>左上隅：⇖</summary> 
    Public Const HT_TOPLEFT As Integer = 13

    '''<summary>右上隅：⇗</summary> 
    Public Const HT_TOPRIGHT As Integer = 14

    '''<summary>下端：↕</summary> 
    Public Const HT_BOTTOM As Integer = 15

    '''<summary>左下隅：⇙</summary> 
    Public Const HT_BOTTOMLEFT As Integer = 16

    '''<summary>右下隅：⇘</summary> 
    Public Const HT_BOTTOMRIGHT As Integer = 17

    ''' <summary>
    ''' インスタンス化禁止（静的クラス）
    ''' </summary>
    Private Sub New()
    End Sub

End Class