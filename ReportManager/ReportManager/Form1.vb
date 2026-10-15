Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        MainMain()
    End Sub

    Sub MainMain()
        Console.WriteLine("=== 帳票システム（Aクラス・Bクラス連携） ===")

        ' 1. 全体を管理する「Bクラス」を生成
        Dim pdfManager As New PdfDocumentManager()

        ' 2. 各帳票のページ管理を行う
        pdfManager.AddReport(New ReportAAdapter(), 1, 2)
        pdfManager.AddReport(New ReportBAdapter(), 10, 20)
        pdfManager.AddReport(New ReportCAdapter(), 15, 30)

        ' 3. PDF出力実行
        'pdfManager.GeneratePdf()

        Console.WriteLine("--- PDF出力開始 (1つのPDFファイルに結合) ---")
        '_globalPageCount = 0 ' 通しページ初期化

        ' 登録された帳票（A, B, C）を順番に出力
        For Each report As ReportListPageBase In pdfManager._reports

            Console.WriteLine(String.Format("[{0} のデータ出力]", report.GetReportPageFormatText))

            For i As Integer = 1 To 3
                report.NextPage()

                'Dim pageLine As String = report.GetBlockPageFormatText()

                Console.WriteLine(String.Format("  ページ印字 -> {0}", report.GetBlockPageFormatText()))
                Console.WriteLine(String.Format("[{0} のデータ出力]", report.GetReportPageFormatText))
            Next
            Console.WriteLine() ' 帳票の区切り
        Next
        Console.WriteLine("--- PDF出力完了 ---")


        ' 待機用
        Console.WriteLine(Environment.NewLine & "Enterキーを押すと終了します...")
        Console.ReadLine()
    End Sub

End Class
