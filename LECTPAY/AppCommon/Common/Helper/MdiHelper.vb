Imports System.Drawing
Imports System.Windows.Forms


Public Class MdiHelper

    ''' <summary>
    ''' MDIクライアント背景色を設定する
    ''' </summary>
    ''' <param name="frm">背景色を設定する対象のフォーム</param>
    ''' <param name="backColor">設定する背景色</param>
    ''' <remarks></remarks>
    Public Shared Sub ChangeMdiClientBackColor(ByVal frm As System.Windows.Forms.Form, ByVal backColor As System.Drawing.Color)
        Dim mdiClient As System.Windows.Forms.MdiClient = GetMdiClient(frm)
        mdiClient.BackColor = backColor
    End Sub

    ''' <summary>
    ''' フォームのMdiClientコントロールを取得する
    ''' </summary>
    ''' <param name="frm">MdiClientコントロール取得対象のフォーム</param>
    ''' <returns>MdiClientコントロール</returns>
    ''' <remarks>
    ''' MDIフォームのMDIクライアント領域の背景色を変更する際に使用するために用意
    ''' </remarks>
    Public Shared Function GetMdiClient(ByVal frm As System.Windows.Forms.Form) As System.Windows.Forms.MdiClient
        Dim control As System.Windows.Forms.Control
        For Each control In frm.Controls
            If TypeOf control Is System.Windows.Forms.MdiClient Then
                Return CType(control, System.Windows.Forms.MdiClient)
            End If
        Next control

        Return Nothing
    End Function

    ''' <summary>
    ''' MDI子フォーム群に指定MDI子フォームが存在するか検索する。
    ''' </summary>
    ''' <param name="mdiChildren">MDI子フォーム群</param>
    ''' <param name="mdiChild">検索対象のMDI子フォーム</param>
    ''' <returns></returns>
    Public Shared Function FindChildForm(ByRef mdiChildren() As Form, ByRef mdiChild As Form) As Boolean
        For Each hMdiChild As Form In mdiChildren
            If (mdiChild Is hMdiChild) Then
                Return True
            End If
        Next hMdiChild

        Return False
    End Function

    ''' <summary>
    ''' MDIClient領域を基準に、MIDチャイルドフォームを左上に初期表示します。
    ''' </summary>
    ''' <param name="mdiParent">MDI親フォーム</param>
    ''' <param name="mdiChild">MDI子フォーム</param>
    ''' <param name="startPosition">表示位置</param>
    Public Shared Sub ShowMdiChildFormStart(ByRef mdiParent As Form, ByRef mdiChild As Form, ByRef mdiChildren() As Form, Optional ByVal startPosition As FormStartPosition = FormStartPosition.Manual)
        If FindChildForm(mdiChildren, mdiChild) Then
            mdiChild.Activate()
            mdiChild.Focus()
        Else
            mdiChild.MdiParent = mdiParent
            If (startPosition = FormStartPosition.Manual) Then
                mdiChild.StartPosition = FormStartPosition.Manual
                mdiChild.Left = 0
                mdiChild.Top = 0
            Else
                mdiChild.StartPosition = startPosition
            End If
            mdiChild.Show()
        End If
    End Sub

    ''' <summary>
    ''' MDIClient領域サイズを基準に表示開始位置（縦位置）を移動します。
    ''' 高さサイズ変更時に画面下が見切れないようにするために使用。
    ''' viewTopStart：クライアント領域の TOP = 0 から表示するかの指定が可能。
    ''' </summary>
    ''' <param name="mdiChaild"></param>
    ''' <param name="viewTopStart">表示位置TOP=0とするかの指定</param>
    Public Shared Sub MDIClientAreaVerticalPosition(ByRef mdiChaild As Form, Optional ByVal viewTopStart As Boolean = False)
        Dim s As Size = ScreenUtil.FormFrameSize(AppState.MDIMainForm)
        'Dim parentSize As Size = New Size(AppState.MDIMainForm.ClientSize.Width, AppState.MDIMainForm.ClientSize.Height - 40 - 4)
        Dim parentSize As Size = MDIUtil.ClientAreaSize()
        Dim location As Point = VerticalPosition(parentSize, mdiChaild)
        If viewTopStart Then
            location.Y = 0
        End If
        mdiChaild.Location = location
    End Sub

    ''' <summary>
    ''' MDIClient領域サイズを基準に中央位置に画面を移動します。
    ''' サイズ変更時に中央表示しなおしたい場合に使用。
    ''' </summary>
    ''' <param name="mdiChaild"></param>
    Public Shared Sub MDIClientAreaCenterPosition(ByRef mdiChaild As Form)
        Dim parentSize As Size = New Size(AppState.MDIMainForm.ClientSize.Width, AppState.MDIMainForm.ClientSize.Height)
        Dim location As Point = CenteredPosition(parentSize, mdiChaild)
        mdiChaild.Location = location
    End Sub

    ''' <summary>
    ''' モニタ表示領域サイズ（タスクバーを除く）を基準に中央位置に画面を移動します。
    ''' </summary>
    ''' <param name="parentFrm"></param>
    Public Shared Sub DesktopWorkAreaCenterPosition(ByRef parentFrm As Form, ByRef mdiChaild As Form)
        Dim screenSize As Size = ScreenUtil.ScreenWorkAreaSize(parentFrm)
        Dim location As Point = CenteredPosition(screenSize, mdiChaild)
        mdiChaild.Location = location
    End Sub

    ''' <summary>
    ''' 場所と配置するもののサイズから中央配置場所の位置を取得します。
    ''' </summary>
    ''' <param name="target"></param>
    ''' <param name="content"></param>
    Public Shared Function CenteredPosition(ByVal target As Size, ByVal content As Form) As Point
        Dim HalfTargetWidth As Integer = target.Width / 2
        Dim HalfTargetHeight As Integer = target.Height / 2
        Dim HalfContentWidth As Integer = content.Width / 2
        Dim HalfContentHeight As Integer = content.Height / 2
        Dim locationLeft As Integer = (HalfTargetWidth - HalfContentWidth)
        Dim locationTop As Integer = (HalfTargetHeight - HalfContentHeight)
        Return New Point(locationLeft, locationTop)
    End Function

    ''' <summary>
    ''' 場所と配置するもののサイズから中央配置場所の位置を取得します。
    ''' </summary>
    ''' <param name="target"></param>
    ''' <param name="content"></param>
    Public Shared Function VerticalPosition(ByVal target As Size, ByVal content As Form) As Point
        Dim HalfTargetWidth As Integer = target.Width / 2
        Dim HalfTargetHeight As Integer = target.Height / 2
        Dim HalfContentHeight As Integer = content.Height / 2
        Dim locationLeft As Integer = content.Left
        Dim locationTop As Integer = (HalfTargetHeight - HalfContentHeight) / 2
        Return New Point(locationLeft, locationTop)
    End Function

    ''' <summary>
    ''' サイドメニューパネルを閉じる
    ''' </summary>
    ''' <param name="frm"></param>
    Public Shared Sub CloseSideMenuPanel(ByVal frm As Form)
        If frm.MdiParent IsNot Nothing Then
            Dim parentMethods As IMdiParentMethods = TryCast(frm.MdiParent, IMdiParentMethods)
            If parentMethods IsNot Nothing Then
                parentMethods.CloseSideMenuPanel()
            End If
        End If
    End Sub

    ''' <summary>
    ''' サイドメニューパネルを開く
    ''' </summary>
    ''' <param name="frm"></param>
    Public Shared Sub OpenSideMenuPanel(ByVal frm As Form)
        If frm.MdiParent IsNot Nothing Then
            Dim parentMethods As IMdiParentMethods = TryCast(frm.MdiParent, IMdiParentMethods)
            If parentMethods IsNot Nothing Then
                parentMethods.OpenSideMenuPanel()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 画面がクライアント領域に収まらないときは、サイドメニューパネルを閉じて画面を表示する。
    ''' </summary>
    ''' <param name="mdiParent"></param>
    ''' <param name="frm"></param>
    Public Shared Sub CloseSideMenuPanelWithFitToSize(ByVal mdiParent As Form, ByRef frm As Form)
        If frm Is Nothing Then
            Return
        End If

        ' 画面の幅
        Dim fromWidth As Integer = frm.Width

        ' MDIクライアント領域サイズ
        Dim workSize As Size = ScreenUtil.ScreenWorkAreaSize(mdiParent)

        ' MDIクライアント領域内に画面が収まらないとき
        If fromWidth > workSize.Width Then
            ' MDIクライアント領域内の左端に寄せる
            frm.Left = 0
            ' サイドメニューパネルを閉じる
            CloseSideMenuPanel(frm)
        Else
            ' MDIクライアント領域内の左端に寄せる
            frm.Left = 0

            Dim parent As IMDIParent = DirectCast(AppState.MDIMainForm, IMDIParent)
            Dim sidePanelWidth As Integer = parent.SideMenuPanel().Width

            ' サイドメニューパネルがあると収まらないとき
            If sidePanelWidth > 0 AndAlso fromWidth > (workSize.Width - sidePanelWidth) Then
                ' サイドメニューパネルを閉じる
                CloseSideMenuPanel(frm)
            End If
        End If
    End Sub

    ''' <summary>
    ''' サイドメニューパネルが閉じている状態ならば、サイドメニューパネルを開いて
    ''' 通常の表示に戻します。
    ''' </summary>
    ''' <param name="frm"></param>
    Public Shared Sub OpenSideMenuPanelWithFitToSize(ByVal frm As Form)
        Dim parent As IMDIParent = TryCast(AppState.MDIMainForm, IMDIParent)
        If parent Is Nothing Then
            Exit Sub
        End If

        Dim sidePanelWidth As Integer = parent.SideMenuPanel().Width
        If sidePanelWidth <= 0 Then
            OpenSideMenuPanel(frm)
        End If
    End Sub

End Class
