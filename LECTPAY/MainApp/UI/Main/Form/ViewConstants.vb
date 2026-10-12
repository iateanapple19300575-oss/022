Namespace LecturePay.Main

    ''' <summary>
    ''' 出講料計算メイン画面で使用する定数定義クラス
    ''' </summary>
    Public NotInheritable Class ViewConstants

        '''<summary>初期値</summary> 
        Public Const BORDER_WIDTH As Integer = 10

        '''<summary>ウィンドウの “非クライアント領域” で左ボタンが押された</summary> 
        Public Const WM_NCLBUTTONDOWN As UInteger = &HA1

        '''<summary>サイドメニューパネル幅</summary> 
        Public Const SIDE_PANEL_WIDTH As Integer = 240

        '''<summary>タイトル：マージン</summary> 
        Public Const TITLE_MARGIN As Integer = 10

        '''<summary>タイトルバー：[Ξ]ボタン幅</summary> 
        Public Const TITLE_MENU_CTRL_WIDTH As Integer = 30

        '''<summary>タイトルバー：[アプリ名]表示幅</summary> 
        Public Const TITLE_APPL_NAME_WIDTH As Integer = 120

        '''<summary>タイトルバー：[対象年月度]表示幅</summary> 
        Public Const TITLE_YEAR_MONTH_WIDTH As Integer = 260

        '''<summary>タイトルバー：[ロック状態]表示幅</summary> 
        Public Const TITLE_LOCK_STATUS_WIDTH As Integer = 150

        '''<summary>タイトルバー：[ユーザID]表示幅</summary> 
        Public Const TITLE_USER_NAME_WIDTH As Integer = 200

        '''<summary>タイトルバー：[最小化]ボタン幅</summary> 
        Public Const TITLE_MIN_WIDTH As Integer = 30

        '''<summary>タイトルバー：[最大化]ボタン幅</summary> 
        Public Const TITLE_MAX_WIDTH As Integer = 30

        '''<summary>タイトルバー：[閉じる]ボタン幅</summary> 
        Public Const TITLE_CLOSE_WIDTH As Integer = 80

        '''<summary>タイトルバー：[Ξ]ボタン左位置</summary> 
        Public Const TITLE_PANEL_MENU_CTRL_LEFT As Integer = TITLE_MARGIN

        '''<summary>タイトルバー：[アプリ名]ラベル左位置</summary> 
        Public Const TITLE_PANEL_APPL_NAME_LEFT As Integer = TITLE_MARGIN + TITLE_MENU_CTRL_WIDTH

        '''<summary>タイトルバー：[対象年月度]ラベル左位置</summary> 
        Public Const TITLE_PANEL_YEAR_MONTH_LEFT As Integer = TITLE_PANEL_APPL_NAME_LEFT + TITLE_MARGIN + TITLE_APPL_NAME_WIDTH

        '''<summary>タイトルバー：[ロック状態]ラベル左位置</summary> 
        Public Const TITLE_PANEL_LOCK_STATUS_LEFT As Integer = TITLE_PANEL_YEAR_MONTH_LEFT + TITLE_MARGIN + TITLE_YEAR_MONTH_WIDTH

        '''<summary>タイトルバー：[閉じる]ボタン右端からの位置</summary> 
        Public Const TITLE_PANEL_CLOSE_RLEFT As Integer = TITLE_MARGIN + TITLE_CLOSE_WIDTH

        '''<summary>タイトルバー：[最大化]ボタン右端からの位置</summary> 
        Public Const TITLE_PANEL_MAX_RLEFT As Integer = TITLE_MAX_WIDTH + TITLE_PANEL_CLOSE_RLEFT

        '''<summary>タイトルバー：[最小化]ボタン右からの位置</summary> 
        Public Const TITLE_PANEL_MIN_RLEFT As Integer = TITLE_PANEL_MAX_RLEFT + TITLE_MIN_WIDTH

        '''<summary>タイトルバー：[ユーザID]ラベル右からの位置</summary> 
        Public Const TITLE_PANEL_USER_NAME_RLEFT As Integer = TITLE_USER_NAME_WIDTH + TITLE_MARGIN + TITLE_PANEL_MIN_RLEFT

        '''<summary>タイトルバー：[タイトルバーの背景色]</summary> 
        Public Shared COLOR_TITLE_BAR As Color = Color.DarkSlateGray

        '''<summary>サイドメニューボタンの背景色：デフォルト色</summary> 
        Public Shared COLOR_BUTTON_DEFAULT As Color = Color.LightGray

        '''<summary>サイドメニューボタンの背景色：活性ボタン色</summary> 
        Public Shared COLOR_BUTTON_ENABLE As Color = Color.LightGoldenrodYellow

        ' インスタンス化禁止
        Private Sub New()
        End Sub

    End Class

End Namespace
