Imports LecturePay.Main
Imports MasterImport
Imports PunchDataImport

''' <summary>
''' サイドメニューパネル
''' </summary>
Public Class SideMenuPanel

    ''' <summary>出講料計算メイン画面 Form</summary>
    Private ReadOnly mainForm As LECTPAY_Main

    ''' <summary>サイドメニューパネル</summary>
    Public pnlMenu As Panel

    ''' <summary>メインメニューボタンリスト</summary>
    Public menuButtons As New List(Of Button)

    ''' <summary>サブメニューボタンリスト</summary>
    Private subMenuButtons As New List(Of Button)

    ''' <summary>File Version (Label)</summary>
    Private lblFileVersion As Label

    ''' <summary>現在選択状態のメイン機能ボタンINDEDX</summary>
    Private currentExpandedIndex As Integer = -1


    ''' <summary>
    ''' イベントクラス
    ''' </summary>
    Private Class EventInfo
        Property Name As String
        Property Text As String
        Property FormType As Type
    End Class

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="form"></param>
    Public Sub New(form As LECTPAY_Main)
        Me.mainForm = form
    End Sub

    ''' <summary>
    ''' パネルの初期化
    ''' </summary>
    Public Function InitializeMenu() As Panel
        ' サイドメニューパネル
        pnlMenu = New Panel With {
            .Dock = DockStyle.Left,
            .Width = ViewConstants.SIDE_PANEL_WIDTH,
            .AutoScroll = True,
            .BackColor = BackcolorWithSideMenu()
        }
        mainForm.Controls.Add(pnlMenu)

        AddMainButton("年月度設定", AddressOf ShowMonthSettingSubmenu)
        AddMainButton("勤怠データ管理", AddressOf ShowAttendanceDataManagementSubMenu)
        AddMainButton("勤怠マスタ管理", AddressOf ShowAttendanceMasterManagementSubMenu)
        AddMainButton("出講管理情報出力", AddressOf ShowAttendanceDataOutputSubMenu)
        AddMainButton("設定", AddressOf ShowSettingSubMenu)
        AddFileVer()
        LayoutMenu()

        AddMouseEventHandler()

        Return pnlMenu
    End Function

    ''' <summary>
    ''' イベントハンドラ登録
    ''' </summary>
    Private Sub AddMouseEventHandler()
    End Sub

    ''' <summary>
    ''' メインメニューボタン生成
    ''' </summary>
    ''' <param name="text"></param>
    ''' <param name="clickHandler"></param>
    Private Sub AddMainButton(ByVal text As String, clickHandler As EventHandler)
        ' ボタン生成
        Dim btn As New Button With {
            .Text = text,
            .Width = MainMenuButtonWidth(),
            .Left = 20,
            .Height = 35,
            .BackColor = ViewConstants.COLOR_BUTTON_DEFAULT,
            .Font = New Font("游ゴシック", 12, FontStyle.Regular),
            .Tag = "NoLog"
        }

        AddHandler btn.Click, clickHandler
        menuButtons.Add(btn)
        pnlMenu.Controls.Add(btn)
    End Sub

    ''' <summary>
    ''' File Version表示
    ''' </summary>
    Private Sub AddFileVer()
        Dim exeVersion As String = "File Ver. " &
        VersionHelper.GetExeName & " " & "(" & VersionHelper.GetFileVersion & ")"
        lblFileVersion = New Label With {
                .Text = exeVersion,
                .Width = pnlMenu.Width - 30,
                .Top = pnlMenu.Height - 40 - 20,
                .Left = 10,
                .Height = 20,
                .BackColor = BackcolorWithSideMenu(),
                .BorderStyle = BorderStyle.None,
                .Font = New Font("游ゴシック", 8, FontStyle.Regular)
            }
        pnlMenu.Controls.Add(lblFileVersion)
    End Sub

    ''' <summary>
    ''' メニューボタン部品のレイアウト調整
    ''' </summary>
    Public Sub LayoutMenu()
        Dim y As Integer = 10
        For i As Integer = 0 To menuButtons.Count - 1
            menuButtons(i).Top = y
            y += menuButtons(i).Height + 15
            If i = currentExpandedIndex Then
                y -= 10
                For Each subBtn In subMenuButtons
                    subBtn.Top = y
                    pnlMenu.Controls.Add(subBtn)
                    y += subBtn.Height + 3
                Next
                y += 20
            End If
        Next
    End Sub

    ''' <summary>
    ''' サブメニューボタン部品のレイアウト調整
    ''' </summary>
    ''' <param name="index"></param>
    ''' <param name="eventInfo"></param>
    Private Sub ShowSubMenu(index As Integer, eventInfo As List(Of EventInfo))

        If index < 0 Then
            ClearSubMenu()
            LayoutMenu()
            For Each ctrl As Button In menuButtons
                ctrl.BackColor = ViewConstants.COLOR_BUTTON_DEFAULT
            Next
            Return
        End If

        If currentExpandedIndex >= 0 Then
            menuButtons(currentExpandedIndex).BackColor = ViewConstants.COLOR_BUTTON_DEFAULT
        End If

        If index = currentExpandedIndex Then
            ClearSubMenu()
            LayoutMenu()
            Return
        End If

        ClearSubMenu()
        currentExpandedIndex = index
        menuButtons(currentExpandedIndex).BackColor = ViewConstants.COLOR_BUTTON_ENABLE

        For Each info In eventInfo
            Dim btn As New Button With {
                .Name = info.Name,
                .Text = info.Text,
                .TextAlign = ContentAlignment.MiddleLeft,
                .Width = SubMenuButtonWidth(),
                .Left = 40,
                .Height = 35,
                .BackColor = ViewConstants.COLOR_BUTTON_ENABLE,
                .Font = New Font("游ゴシック", 12, FontStyle.Regular),
                .Tag = "NoLog"
            }

            AddHandler btn.Click,
                Sub(sender, e)
                    mainForm.OpenChildFormDynamic(info.FormType, sender, e)
                End Sub

            subMenuButtons.Add(btn)
        Next

        LayoutMenu()
    End Sub

    ''' <summary>
    ''' 年月度設定：サブメニューボタンの生成
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowMonthSettingSubmenu(sender As Object, e As EventArgs)
        Dim eventInfo As New List(Of EventInfo) From {
            New EventInfo With {
                .Name = "btnMenuItemSelectMonth",
                .Text = "年月度設定",
                .FormType = GetType(frmTargetYearMonth)
            }
        }
        ShowSubMenu(0, eventInfo)
    End Sub

    ''' <summary>
    ''' 勤怠データ管理：サブメニューボタンの生成
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowAttendanceDataManagementSubMenu(sender As Object, e As EventArgs)
        Dim eventInfo As New List(Of EventInfo) From {
            New EventInfo With {.Name = "btnAttendanceDataImport", .Text = "勤怠データ取込", .FormType = GetType(FrmLectureImport)},
            New EventInfo With {.Name = "btnAllowanceDeductionDataImport", .Text = "手当・控除データ取込", .FormType = GetType(FrmAllowanceDeductionImport)},
            New EventInfo With {.Name = "btnTravelExpensesMgmt", .Text = "交通費管理", .FormType = GetType(FrmTransportExpenses)},
            New EventInfo With {.Name = "btnCheckAttendanceData", .Text = "勤怠データ確認", .FormType = GetType(FrmCheckAttendanceData)},
            New EventInfo With {.Name = "btnClosingProcess", .Text = "締め処理", .FormType = GetType(FormMonthlyClosing)},
            New EventInfo With {.Name = "btnAttendanceDataOutput", .Text = "勤怠データ出力", .FormType = GetType(FrmAttendanceDataOutput)}
        }

        ShowSubMenu(1, eventInfo)
    End Sub

    ''' <summary>
    ''' 勤怠マスタ管理：サブメニューボタンの生成
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowAttendanceMasterManagementSubMenu(sender As Object, e As EventArgs)
        Dim eventInfo As New List(Of EventInfo) From {
            New EventInfo With {
                .Name = "btnTimeTableDataImport",
                .Text = "時間割設定データ取込",
                .FormType = GetType(FrmImportTimetableData)
            },
            New EventInfo With {
                .Name = "btnLectureFeeDataImport",
                .Text = "出講料設定データ取込",
                .FormType = GetType(FrmImportLectureFeeData)
            }
        }

        ShowSubMenu(2, eventInfo)
    End Sub

    ''' <summary>
    ''' 出講管理情報出力：サブメニューボタンの生成
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowAttendanceDataOutputSubMenu(sender As Object, e As EventArgs)
        Dim eventInfo As New List(Of EventInfo) From {
            New EventInfo With {
                .Name = "btnLectureInformationOutput",
                .Text = "出講管理情報出力",
                .FormType = GetType(FrmImportLectureFeeData)
            }
        }

        ShowSubMenu(3, eventInfo)
    End Sub

    ''' <summary>
    ''' 設定：サブメニューボタンの生成
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ShowSettingSubMenu(sender As Object, e As EventArgs)
        Dim eventInfo As New List(Of EventInfo) From {
            New EventInfo With {.Name = "btnLockHistory", .Text = "年月ロック状況確認", .FormType = GetType(FrmLockHistory)},
            New EventInfo With {.Name = "btnImportHistory", .Text = "取込履歴", .FormType = GetType(FrmImportHistory)},
            New EventInfo With {.Name = "btnOperationHistory", .Text = "操作履歴", .FormType = GetType(FrmOperationHistory)}
        }

        ShowSubMenu(4, eventInfo)
    End Sub

    ''' <summary>
    ''' サブメニューボタン配置クリア
    ''' </summary>
    Private Sub ClearSubMenu()
        For Each btn In subMenuButtons
            pnlMenu.Controls.Remove(btn)
        Next

        subMenuButtons.Clear()
        currentExpandedIndex = -1
    End Sub

    ''' <summary>
    ''' メイン機能ボタン幅取得
    ''' </summary>
    ''' <returns></returns>
    Private Function MainMenuButtonWidth() As Integer
        Return pnlMenu.Width - 40
    End Function

    ''' <summary>
    ''' サブ機能ボタン幅取得
    ''' </summary>
    ''' <returns></returns>
    Private Function SubMenuButtonWidth() As Integer
        Return MainMenuButtonWidth() - 20
    End Function

    ''' <summary>
    ''' サイドメニューパネルの背景色
    ''' </summary>
    ''' <returns></returns>
    Private Function BackcolorWithSideMenu()
        If mainForm.IsProdEnv Then
            Return Color.FromArgb(&HFFCAE6CA)
        End If
        Return Color.PowderBlue
    End Function

End Class
