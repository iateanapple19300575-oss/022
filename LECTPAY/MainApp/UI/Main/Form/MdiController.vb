''' <summary>
''' MDI Controller
''' </summary>
Public Class MdiController

    ''' <summary>出講料計算メイン画面 Form</summary>
    Private ReadOnly mainForm As LECTPAY_Main

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="form"></param>
    Public Sub New(form As LECTPAY_Main)
        Me.mainForm = form
    End Sub

    ''' <summary>
    ''' MDI子画面をアクティブにします。
    ''' </summary>
    Public Sub OnMdiChildActivated()
        Dim child = mainForm.ActiveMdiChild
        mainForm.SideMenuEnable()

        If child Is Nothing Then
            Return
        End If
    End Sub

    ''' <summary>
    ''' MDI子画面が存在するときにアクティブにします。
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <returns></returns>
    Public Function ShowChildForm(Of T As Form)() As Boolean
        For Each child As Form In mainForm.MdiChildren
            If TypeOf child Is T Then
                child.Activate()
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' MDI子画面を表示します。
    ''' </summary>
    ''' <param name="mdiChild"></param>
    ''' <param name="startPosition"></param>
    Public Sub ShowMdiChildForm(ByRef mdiChild As Form, Optional ByVal startPosition As FormStartPosition = FormStartPosition.Manual)
        Dim mdiChildren() As Form = mainForm.MdiChildren
        Mdihelper.ShowMdiChildFormStart(mainForm, mdiChild, mdiChildren)
    End Sub

    ''' <summary>
    ''' MDI子画面を閉じます。
    ''' </summary>
    ''' <param name="newScreen"></param>
    Public Sub CloseMDIChildScreen(ByVal newScreen As Form)
        ShowMdiChildForm(newScreen)
        For Each frm As Form In mainForm.MdiChildren
            If Not frm.Name.Equals(newScreen.Name) Then
                frm.Close()
            End If
        Next
    End Sub

    ''' <summary>
    ''' すべてのMDI子画面を閉じます。
    ''' </summary>
    Public Sub CloseMDIChildAll()
        For Each frm As Form In mainForm.MdiChildren
            frm.Close()
        Next
    End Sub

End Class