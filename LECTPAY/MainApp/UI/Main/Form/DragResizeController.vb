''' <summary>
''' フォームのドラッグ／リサイズ処理を共通化するコントローラクラス。
''' 任意のコントロールに対して MouseDown / MouseMove / MouseUp を割り当てることで、
''' FormBorderStyle=None のフォームを完全自前で移動・リサイズできるようにする。
''' </summary>
Public Class DragResizeController

    Private ReadOnly mainForm As LECTPAY_Main
    Public Property resizing As Boolean = False
    Private resizeDir As UInteger = HitTestConstants.HT_NONE
    Private startPoint As Point
    Private startBounds As Rectangle

    ''' <summary>
    ''' コンストラクタ。対象フォームを受け取り、
    ''' フォーム内の全コントロールにドラッグ／リサイズイベントを割り当てる。
    ''' </summary>
    ''' <param name="frm">対象フォーム。</param>
    Public Sub New(frm As Form)
        mainForm = frm

        ' フォーム内の全コントロールにイベントを付与
        For Each c As Control In frm.Controls
            AddHandler c.MouseDown, AddressOf AllControls_MouseDown
            AddHandler c.MouseMove, AddressOf AllControls_MouseMove
            AddHandler c.MouseUp, AddressOf AllControls_MouseUp
        Next

        ' フォーム自身にも付与
        AddHandler frm.MouseDown, AddressOf AllControls_MouseDown
        AddHandler frm.MouseMove, AddressOf AllControls_MouseMove
        AddHandler frm.MouseUp, AddressOf AllControls_MouseUp
    End Sub


    ''' <summary>
    ''' MouseDown イベント
    ''' 
    ''' ※ドラッグ／リサイズ開始
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AllControls_MouseDown(sender As Object, e As MouseEventArgs)
        ' 左マウス押下イベント以外はスキップ
        If e.Button <> MouseButtons.Left Then
            Return
        End If

        ' ドラッグ開始時の情報出得
        Dim screenPoint As Point = CType(sender, Control).PointToScreen(e.Location)
        Dim formPoint As Point = mainForm.PointToClient(screenPoint)
        resizeDir = HitTestHelper.GetHitTestResult(mainForm, formPoint)
        resizing = True
        startPoint = screenPoint
        startBounds = mainForm.Bounds
    End Sub

    ''' <summary>
    ''' MouseMove イベント
    ''' 
    ''' ※ドラッグ／リサイズ中
    ''' 全コントロール共通の MouseMove 処理。
    ''' リサイズ中はフォームの Bounds をリアルタイムに更新し、
    ''' リサイズしていない場合はカーソル形状を変更する。
    ''' 自前ドラッグ／リサイズ方式の中心となる処理。
    ''' </summary>
    ''' <param name="sender">イベントを発生させたコントロール。</param>
    ''' <param name="e">マウス座標・ボタン情報を含む MouseEventArgs。</param>
    Private Sub AllControls_MouseMove(sender As Object, e As MouseEventArgs)
        ' コントロール座標 → 画面座標へ変換
        Dim screenPoint As Point = CType(sender, Control).PointToScreen(e.Location)
        ' ドラッグ開始位置との差分（移動量）
        Dim dx As Integer = screenPoint.X - startPoint.X
        Dim dy As Integer = screenPoint.Y - startPoint.Y

        '----------------------------------------
        '  リサイズ中の処理
        '----------------------------------------
        If resizing Then
            Dim r As Rectangle = startBounds

            ' HitTest の結果（resizeDir）に応じて、
            ' 左右・上下・四隅のいずれかの方向にリサイズを行う。
            ' Bounds を直接書き換えることで、OS に依存せず完全に自前でリサイズ処理。
            Select Case resizeDir
                Case HitTestConstants.HT_LEFT
                    r.X += dx
                    r.Width -= dx
                Case HitTestConstants.HT_RIGHT
                    r.Width += dx
                Case HitTestConstants.HT_TOP
                    r.Y += dy
                    r.Height -= dy
                Case HitTestConstants.HT_BOTTOM
                    r.Height += dy
                Case HitTestConstants.HT_TOPLEFT
                    r.X += dx
                    r.Width -= dx
                    r.Y += dy
                    r.Height -= dy
                Case HitTestConstants.HT_TOPRIGHT
                    r.Width += dx
                    r.Y += dy
                    r.Height -= dy
                Case HitTestConstants.HT_BOTTOMLEFT
                    r.X += dx
                    r.Width -= dx
                    r.Height += dy
                Case HitTestConstants.HT_BOTTOMRIGHT
                    r.Width += dx
                    r.Height += dy
                Case HitTestConstants.HT_CAPTION
                    ' タイトルバー領域は移動扱い
                    r.X += dx
                    r.Y += dy
            End Select

            ' フォームの位置・サイズを更新
            mainForm.Bounds = r
            mainForm.TitolBarPanel.TitleLayoutRefresh()

            Return
        End If

        '----------------------------------------
        '  リサイズ中でない場合：カーソル変更
        '----------------------------------------
        ' マウス位置から HitTest を再判定し、
        ' リサイズ可能な方向に応じたカーソル形状を設定する。
        ' OS の WM_NCHITTEST を使わず、完全自前で制御する。
        Dim formPoint As Point = mainForm.PointToClient(screenPoint)
        Dim dir As UInteger = HitTestHelper.GetHitTestResult(mainForm, formPoint)

        Select Case dir
            Case HitTestConstants.HT_LEFT, HitTestConstants.HT_RIGHT
                mainForm.Cursor = Cursors.SizeWE
            Case HitTestConstants.HT_TOP, HitTestConstants.HT_BOTTOM
                mainForm.Cursor = Cursors.SizeNS
            Case HitTestConstants.HT_TOPLEFT, HitTestConstants.HT_BOTTOMRIGHT
                mainForm.Cursor = Cursors.SizeNWSE
            Case HitTestConstants.HT_TOPRIGHT, HitTestConstants.HT_BOTTOMLEFT
                mainForm.Cursor = Cursors.SizeNESW
            Case HitTestConstants.HT_CAPTION
                mainForm.Cursor = Cursors.Default
            Case Else
                mainForm.Cursor = Cursors.Default
        End Select
    End Sub

    ''' <summary>
    ''' MouseUp イベント
    ''' 
    ''' ※ドラッグ／リサイズ終了
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AllControls_MouseUp(sender As Object, e As MouseEventArgs)
        ' ドラッグ／リサイズ終了
        resizing = False
        mainForm.Cursor = Cursors.Default
    End Sub

End Class
