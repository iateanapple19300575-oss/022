Imports System.Data.SqlClient
Imports AdvanceSoftware.VBReport8

Public Interface IVBReportPage

    ''' <summary>
    ''' 環境設定
    ''' </summary>
    Sub VbReportsEnvSetting(ByVal report As CellReport, ByVal tagetDate As Date)

    ''' <summary>
    ''' 初期設定
    ''' </summary>
    Sub InitialSetup()

    ''' <summary>
    ''' ページ出力開始
    ''' </summary>
    Sub PageStart()

    ''' <summary>
    ''' ページ出力終了
    ''' </summary>
    Sub PageEnd()

    ''' <summary>
    ''' ページ出力
    ''' </summary>
    Sub PageReport()

    ''' <summary>
    ''' ページタイトル出力
    ''' </summary>
    Sub PageTitle()

    ''' <summary>
    ''' ページタイトル出力
    ''' </summary>
    ''' <param name="cell">指定セル</param>
    Sub PageTitle(ByVal cell As String)

    ''' <summary>
    ''' レポートタイトル出力
    ''' </summary>
    ''' <param name="x">指定セルX軸</param>
    ''' <param name="y">指定セルY軸</param>
    ''' <param name="offset">指定セルオフセット</param>
    Sub PageTitle(ByVal x As Integer, ByVal y As Integer, Optional ByVal offset As Integer = 0)

    ''' <summary>
    ''' ページデータ出力
    ''' </summary>
    Sub PageOutput()

	''' <summary>
	''' 明細出力
	''' </summary>
	''' <param name="reader"></param>
	Sub OutputDetail(ByVal reader As DataTable)

End Interface
