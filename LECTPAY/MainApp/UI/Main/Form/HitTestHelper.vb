''' <summary>
''' フォーム内座標から、どの領域（リサイズ／移動／クライアント）に
''' ヒットしているかを判定する共通ヘルパークラス。
''' FormBorderStyle=None の自前ドラッグ・リサイズ方式で使用する。
''' </summary>
Public NotInheritable Class HitTestHelper

    ''' <summary>
    ''' 指定されたフォームと座標に基づき、ヒットテスト結果を返す。
    ''' リサイズ可能な四辺・四隅・タイトルバー・クライアント領域を判定する。
    ''' </summary>
    ''' <param name="frm">判定対象のフォーム</param>
    ''' <param name="p">フォーム内座標（Point）</param>
    ''' <returns>
    ''' HT_* に対応するヒットテスト結果。
    ''' ・四隅 → リサイズ（斜めカーソル）
    ''' ・四辺 → リサイズ（水平／垂直カーソル）
    ''' ・タイトルバー → 移動（HT_CAPTION）
    ''' ・クライアント領域 → HT_CLIENT
    ''' </returns>
    Public Shared Function GetHitTestResult(frm As Form, p As Point) As UInteger

        ' リサイズ判定幅（辺・角）
        Const BORDER As Integer = 8
        Const CORNER As Integer = 16

        ' タイトルバー高さ
        Const TITLEBAR_HEIGHT As Integer = 40

        ' 辺の判定
        Dim left As Boolean = p.X <= BORDER
        Dim right As Boolean = p.X >= frm.Width - BORDER
        Dim top As Boolean = p.Y <= BORDER
        Dim bottom As Boolean = p.Y >= frm.Height - BORDER

        ' 角の判定（最優先）
        If p.X <= CORNER AndAlso p.Y <= CORNER Then
            Return HitTestConstants.HT_TOPLEFT
        End If
        If p.X >= frm.Width - CORNER AndAlso p.Y <= CORNER Then
            Return HitTestConstants.HT_TOPRIGHT
        End If
        If p.X <= CORNER AndAlso p.Y >= frm.Height - CORNER Then
            Return HitTestConstants.HT_BOTTOMLEFT
        End If
        If p.X >= frm.Width - CORNER AndAlso p.Y >= frm.Height - CORNER Then
            Return HitTestConstants.HT_BOTTOMRIGHT
        End If

        ' 辺の判定
        If left Then
            Return HitTestConstants.HT_LEFT
        End If
        If right Then
            Return HitTestConstants.HT_RIGHT
        End If
        If top Then
            Return HitTestConstants.HT_TOP
        End If
        If bottom Then
            Return HitTestConstants.HT_BOTTOM
        End If

        ' サイドメニューが非表示時でも左端をリサイズ扱いしたいため
        ' 左端 8px を特別扱いする。（調整必要かも）
        If p.X <= 8 Then
            Return HitTestConstants.HT_LEFT
        End If

        ' タイトルバー（移動領域）
        If p.Y <= TITLEBAR_HEIGHT Then
            Return HitTestConstants.HT_CAPTION
        End If

        ' クライアント領域
        Return HitTestConstants.HT_CLIENT
    End Function

    ''' <summary>
    ''' インスタンス化禁止（静的クラス）
    ''' </summary>
    Private Sub New()
    End Sub

End Class
