Imports AdvanceSoftware.VBReport8

''' <summary>
''' VB-Reports インターフェース
''' </summary>
Public Interface IVBReport

    ''' <summary>
    ''' ページ出力
    ''' </summary>
    Sub Report()

	''' <summary>
	''' 環境設定
	''' </summary>
	Sub VbReportsEnvSetting(ByVal report As CellReport, ByVal viewer As ViewerControl)

    ''' <summary>
    ''' 初期処理
    ''' </summary>
    Sub InitialSetup()

    ''' <summary>
    ''' レポート開始処理
    ''' </summary>
    Sub ReportStart()

    ''' <summary>
    ''' レポート終了処理
    ''' </summary>
    Sub ReportEnd()

    ''' <summary>
    ''' ページ追加
    ''' </summary>
    ''' <param name="reportPage"></param>
    Sub AddPage(ByVal reportPage As IVBReportPage)

End Interface
