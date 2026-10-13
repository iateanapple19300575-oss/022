Imports System.Data.SqlClient
Imports AdvanceSoftware.VBReport8
Imports Nts.Freamwork.Common.Utilities

''' <summary>
''' VB-Reportsユーティリティクラス
''' </summary>
Public Class CellReportUtil
    Inherits CellReport

    Private m_CellReport As CellReport


    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="report"></param>
    Public Sub New(ByVal report As CellReport)
        Me.m_CellReport = report
    End Sub

    ''' <summary>
    ''' セル参照形式変換（R1C1形式の列→A1形式の列）番号変換
    ''' 列:2 -> 列:B
    ''' </summary>
    ''' <param name="columnNumber">カラム番号(R1C1形式)</param>
    ''' <returns>カラム番号(A1)</returns>
    Public Function NumberToA1(ByVal columnNumber As Integer) As String
        Dim result As String = ""
        Dim dividend As Integer = columnNumber
        While dividend > 0
            Dim modulo As Integer = (dividend - 1) Mod 26
            result = Chr(65 + modulo) & result
            dividend = (dividend - 1) \ 26
        End While
        Return result
    End Function

    ''' <summary>
    ''' セル参照形式変換（R1C1参照形式のセル→A1参照形式のセル）
    ''' R1C2 -> B1
    ''' </summary>
    ''' <param name="colomunNunber">列番号</param>
    ''' <param name="rowNumber">行番号</param>
    ''' <returns></returns>
    Public Function NumberToCellString(ByVal colomunNunber As Integer, ByVal rowNumber As Integer) As String
        Dim a1Cell As String = NumberToA1(colomunNunber)
        Return a1Cell & rowNumber.ToString()
    End Function

    ''' <summary>
    ''' 値設定
    ''' </summary>
    ''' <param name="_cell">雛形セル名(**セル名)</param>
    ''' <param name="cx">雛形セル名(_cell)からのX軸オフセット</param>
    ''' <param name="cy">雛形セル名(_cell)からのY軸オフセット</param>
    ''' <param name="attrCell">属性番号</param>
    ''' <param name="value">設定値</param>
    Public Overloads Sub Cell(ByVal _cell As String, ByVal cx As Integer, ByVal cy As Integer, ByVal attrCell As String, ByVal value As Object)
        Dim attrNo As Integer = m_CellReport.Cell(attrCell).AttrNo

        With m_CellReport
            .Cell(_cell, cx, cy).Value = value
            .Cell(_cell, cx, cy).AttrNo = attrNo
        End With
    End Sub

    ''' <summary>
    ''' 数式設定
    ''' </summary>
    ''' <param name="_cell">雛形セル名(**セル名)</param>
    ''' <param name="cx">雛形セル名(_cell)からのX軸オフセット</param>
    ''' <param name="cy">雛形セル名(_cell)からのY軸オフセット</param>
    ''' <param name="attrCell">属性番号</param>
    ''' <param name="calcFormula">数式</param>
    Public Overloads Sub Formula(ByVal _cell As String, ByVal cx As Integer, ByVal cy As Integer, ByVal attrCell As String, ByVal calcFormula As String)
        ' 基準セルの属性番号取得
        Dim attrNo As Integer = m_CellReport.Cell(attrCell).AttrNo

        ' 取得した属性番号の書式情報と同じ設定＋数式を設定
        With m_CellReport
            .Cell(_cell, cx, cy).AttrNo = attrNo
            .Cell(_cell, cx, cy).Func(calcFormula, Nothing)
        End With
    End Sub

    ''' <summary>
    ''' 日付データ設定(書式付き)
    ''' </summary>
    ''' <param name="_cell">雛形セル名(**セル名)</param>
    ''' <param name="cx">雛形セル名(_cell)からのX軸オフセット</param>
    ''' <param name="cy">雛形セル名(_cell)からのY軸オフセット</param>
    ''' <param name="attrCell">属性番号</param>
    ''' TODO: 別クラスに移動では？
    Public Overloads Sub CellDateFormat(ByVal _cell As String,
                                        ByVal cx As Integer,
                                        ByVal cy As Integer,
                                        ByVal attrCell As String,
                                        ByVal value As Object)

        Dim attrNo As Integer = m_CellReport.Cell(attrCell).AttrNo

        With m_CellReport
            .Cell(_cell, cx, cy).AttrNo = attrNo
            .Cell(_cell, cx, cy).Attr.Format = "yyyy/mm/dd"
            .Cell(_cell, cx, cy).Value = CType(value, Date).ToString("yyyy/MM/dd")
        End With
    End Sub


    ''' <summary>
    ''' 値設定
    ''' </summary>
    ''' <param name="_cell">雛形セル名(**セル名)</param>
    ''' <param name="cx">雛形セル名(_cell)からのX軸オフセット</param>
    ''' <param name="cy">雛形セル名(_cell)からのY軸オフセット</param>
    ''' <param name="reader">SqlDataReaderオブジェクト</param>
    ''' <param name="colName">出力対象項目(SqlDataReader内カラム名)</param>
    ''' <param name="attrCell">属性番号</param>
    ''' TODO: 別クラスに移動では？
    Public Overloads Sub Cell(ByVal _cell As String, ByVal cx As Integer, ByVal cy As Integer, ByVal reader As SqlDataReader, ByVal colName As String, ByVal attrCell As String)
        Dim attrNo As Integer = m_CellReport.Cell(attrCell).AttrNo

        With m_CellReport
            .Cell(_cell, cx, cy).Value = SqlDataReaderHelper.GetValue(reader, colName)
            .Cell(_cell, cx, cy).AttrNo = attrNo
        End With
    End Sub

    ''' <summary>
    ''' 値設定
    ''' </summary>
    ''' <param name="reader">SqlDataReaderオブジェクト</param>
    ''' <param name="colName">出力対象項目(SqlDataReader内カラム名)</param>
    ''' <param name="cx">雛形セル名(_cell)からのX軸オフセット</param>
    ''' <param name="cy">雛形セル名(_cell)からのY軸オフセット</param>
    ''' <param name="ox">雛形セル名(cx)からのX軸オフセット</param>
    ''' <param name="oy">雛形セル名(cy)からのY軸オフセット</param>
    Public Overloads Sub Cell(ByVal reader As SqlDataReader, ByVal colName As String, ByVal cx As Integer, ByVal cy As Integer, ByVal ox As Integer, ByVal oy As Integer)
        ' 基準セルの属性番号取得
        Dim attrCell As String = NumberToCellString(ox, oy)
        Dim attrNo As Integer = m_CellReport.Cell(attrCell).AttrNo

        ' 設定先のセル位置(A1セル名)取得
        Dim cell As String = NumberToCellString(cx + ox, cy + oy)

        ' SqlDataReaderの指定カラムの値を設定
        With m_CellReport
            .Cell(cell, 0, 0).Value = SqlDataReaderHelper.GetValue(reader, colName)
            .Cell(cell, 0, 0).AttrNo = attrNo
        End With
    End Sub

    ''' <summary>
    ''' 値設定
    ''' </summary>
    ''' <param name="_cell">雛形セル名(**セル名)</param>
    ''' <param name="cx">雛形セル名(_cell)からのX軸オフセット</param>
    ''' <param name="cy">雛形セル名(_cell)からのY軸オフセット</param>
    ''' <param name="value">出力内容</param>
    ''' <param name="attrCell">属性番号</param>
    ''' TODO: 別クラスに移動では？
    Public Overloads Sub CellValue(ByVal _cell As String, ByVal cx As Integer, ByVal cy As Integer, ByVal value As String, ByVal attrCell As String)
        Dim attrNo As Integer = m_CellReport.Cell(attrCell).AttrNo

        With m_CellReport
            .Cell(_cell, cx, cy).Value = value
            .Cell(_cell, cx, cy).AttrNo = attrNo
        End With
    End Sub

    ''' <summary>
    ''' 値設定
    ''' </summary>
    ''' <param name="_cell">雛形セル名(**セル名)</param>
    ''' <param name="cx">雛形セル名(_cell)からのX軸オフセット</param>
    ''' <param name="cy">雛形セル名(_cell)からのY軸オフセット</param>
    ''' <param name="value">出力内容</param>
    ''' <param name="attrCell">属性番号</param>
    ''' TODO: 別クラスに移動では？
    Public Overloads Sub CellValue(ByVal _cell As String, ByVal cx As Integer, ByVal cy As Integer, ByVal value As Decimal, ByVal attrCell As String)
        Dim attrNo As Integer = m_CellReport.Cell(attrCell).AttrNo

        With m_CellReport
            .Cell(_cell, cx, cy).Value = value
            .Cell(_cell, cx, cy).AttrNo = attrNo
        End With
    End Sub

    Public Overloads Sub CellValue(ByVal _cell As String, ByVal cx As Integer, ByVal cy As Integer, ByVal value As Object, ByVal attrCell As String)
        Dim attrNo As Integer = m_CellReport.Cell(attrCell).AttrNo

        With m_CellReport
            .Cell(_cell, cx, cy).Value = If(IsDBNull(value), "", value)
            .Cell(_cell, cx, cy).AttrNo = attrNo
        End With
    End Sub

    Public Overloads Sub CellValue(ByVal _cell As String, ByVal value As Object, ByVal attrCell As String)
        With m_CellReport
            .Cell(_cell).Value = If(IsDBNull(value), "", value)
        End With
    End Sub

    Public Overloads Sub CellValueTimeJ(ByVal _cell As String, ByVal cx As Integer, ByVal cy As Integer, ByVal value As Object, ByVal attrCell As String)
        Dim attrNo As Integer = m_CellReport.Cell(attrCell).AttrNo

        Dim ts As TimeSpan
        Dim outValue As String = ""

        If TypeOf value Is TimeSpan Then
            ts = value
            If ts.TotalMinutes > 0 Then
                outValue = String.Format("{0}時間{1:00}分", CInt(Math.Floor(ts.TotalHours)), ts.Minutes)
            End If
        ElseIf TypeOf value Is Integer Then
        If Not IsDBNull(value) AndAlso value > 0 Then
                ts = TimeSpan.FromMinutes(CDbl(value))
                outValue = String.Format("{0}時間{1:00}分", CInt(Math.Floor(ts.TotalHours)), ts.Minutes)
            End If
        Else
            outValue = ""
        End If

        With m_CellReport
            .Cell(_cell, cx, cy).Value = outValue
            .Cell(_cell, cx, cy).AttrNo = attrNo
        End With
    End Sub

    ''' <summary>
    ''' 位置指定データ設定
    ''' </summary>
    ''' <param name="cx">基準X座標</param>
    ''' <param name="cy">基準Y座標</param>
    ''' <param name="ox">Xオフセット</param>
    ''' <param name="oy">Yオフセット</param>
    ''' <param name="value"></param>
    Public Overloads Sub CellValue(ByVal cx As Integer, ByVal cy As Integer, ByVal ox As Integer, ByVal oy As Integer, ByVal value As Object)
        ' 基準セルの属性番号取得
        Dim attrCell As String = NumberToCellString(ox, oy)
        Dim attrNo As Integer = m_CellReport.Cell(attrCell).AttrNo

        ' 設定先のセル位置(A1セル名)取得
        Dim cell As String = NumberToCellString(cx + ox, cy + oy)

        ' SqlDataReaderの指定カラムの値を設定
        With m_CellReport
            .Cell(cell, 0, 0).Value = IIf(IsDBNull(value), "", value)
            .Cell(cell, 0, 0).AttrNo = attrNo
        End With
    End Sub

    ''' <summary>
    ''' 位置指定データ設定
    ''' 値がDBNullまたは 1 の場合は、空として扱う
    ''' </summary>
    ''' <param name="cx">基準X座標</param>
    ''' <param name="cy">基準Y座標</param>
    ''' <param name="ox">Xオフセット</param>
    ''' <param name="oy">Yオフセット</param>
    ''' <param name="value"></param>
    Public Overloads Sub CellValueZeroIsBlank(ByVal cx As Integer, ByVal cy As Integer, ByVal ox As Integer, ByVal oy As Integer, ByVal value As Object)
        CellValue(cx, cy, ox, oy, IIf(IsDBNull(value) OrElse value.ToString() = "0", "", value))
    End Sub

    ''' <summary>
    ''' 日付データ設定(書式付き)
    ''' </summary>
    ''' <param name="_cell">雛形セル名(**セル名)</param>
    ''' <param name="cx">雛形セル名(_cell)からのX軸オフセット</param>
    ''' <param name="cy">雛形セル名(_cell)からのY軸オフセット</param>
    ''' <param name="reader">SqlDataReaderオブジェクト</param>
    ''' <param name="colName">出力対象項目(SqlDataReader内カラム名)</param>
    ''' <param name="attrCell">属性番号</param>
    ''' TODO: 別クラスに移動では？
    Public Overloads Sub CellDateFormat(ByVal _cell As String,
                                          ByVal cx As Integer,
                                          ByVal cy As Integer,
                                          ByVal reader As SqlDataReader,
                                          ByVal colName As String,
                                          ByVal attrCell As String)

        Dim attrNo As Integer = m_CellReport.Cell(attrCell).AttrNo

        With m_CellReport
            .Cell(_cell, cx, cy).AttrNo = attrNo
            .Cell(_cell, cx, cy).Attr.Format = "yyyy/mm/dd"
            .Cell(_cell, cx, cy).Value = CType(SqlDataReaderHelper.GetValue(reader, colName), Date).ToString("yyyy/MM/dd")
        End With
    End Sub

    ''' <summary>
    ''' 日付データ設定(書式付き)
    ''' </summary>
    ''' <param name="_cell">雛形セル名(**セル名)</param>
    ''' <param name="cx">雛形セル名(_cell)からのX軸オフセット</param>
    ''' <param name="cy">雛形セル名(_cell)からのY軸オフセット</param>
    ''' <param name="attrCell">属性番号</param>
    ''' TODO: 別クラスに移動では？
    Public Overloads Sub CellValueDateFormat(ByVal _cell As String,
                                          ByVal cx As Integer,
                                          ByVal cy As Integer,
                                          ByVal value As String,
                                          ByVal attrCell As Object)

        Dim attrNo As Integer = m_CellReport.Cell(attrCell).AttrNo
        Dim parseDate As Date
        Dim dateStr As String = ""
        If Date.TryParse(value, parseDate) Then
            dateStr = parseDate.ToString("yyyy/MM/dd")
        End If

        With m_CellReport
            .Cell(_cell, cx, cy).AttrNo = attrNo
            .Cell(_cell, cx, cy).Attr.Format = "yyyy/mm/dd"
            .Cell(_cell, cx, cy).Value = dateStr
        End With
    End Sub

    ''' <summary>
    ''' 日付データ設定(書式付き)
    ''' </summary>
    ''' <param name="_cell">雛形セル名(**セル名)</param>
    ''' <param name="cx">雛形セル名(_cell)からのX軸オフセット</param>
    ''' <param name="cy">雛形セル名(_cell)からのY軸オフセット</param>
    ''' <param name="attrCell">属性番号</param>
    ''' TODO: 別クラスに移動では？
    Public Overloads Sub CellValueTimeFormat(ByVal _cell As String,
                                          ByVal cx As Integer,
                                          ByVal cy As Integer,
                                          ByVal value As Object,
                                          ByVal attrCell As Object)

        Dim attrNo As Integer = m_CellReport.Cell(attrCell).AttrNo
        Dim timeStr As String = Nothing
        If value IsNot Nothing AndAlso StringUtil.IsNotNullOrWhiteSpace(value.ToString()) Then
            If TypeOf value Is TimeSpan Then
                Dim ts As TimeSpan = DirectCast(value, TimeSpan)
                timeStr = String.Format("{0:D2}:{1:D2}", ts.Hours, ts.Minutes)
            ElseIf TypeOf value Is Integer Then
                timeStr = TimeUtil.MinutesToHoursStr(value)
            End If
        End If

        With m_CellReport
            .Cell(_cell, cx, cy).AttrNo = attrNo
            .Cell(_cell, cx, cy).Attr.Format = "hh:mm"
            .Cell(_cell, cx, cy).Value = timeStr
        End With
    End Sub

    ''' <summary>
    ''' 位置指定時間データ設定(書式付き)
    ''' </summary>
    ''' <param name="cx">基準X座標</param>
    ''' <param name="cy">基準Y座標</param>
    ''' <param name="ox">Xオフセット</param>
    ''' <param name="oy">Yオフセット</param>
    ''' <param name="value"></param>
    Public Overloads Sub CellValueTimeFormat(ByVal cx As Integer, ByVal cy As Integer, ByVal ox As Integer, ByVal oy As Integer, ByVal value As Object)
        ' 基準セルの属性番号取得
        Dim attrCell As String = NumberToCellString(ox, oy)
        Dim attrNo As Integer = m_CellReport.Cell(attrCell).AttrNo

        ' 設定先のセル位置(A1セル名)取得
        Dim cell As String = NumberToCellString(cx + ox, cy + oy)
        Dim timeStr As String = Nothing
        If value IsNot Nothing AndAlso StringUtil.IsNotNullOrWhiteSpace(value.ToString()) Then
            Dim ts As TimeSpan = DirectCast(value, TimeSpan)
            timeStr = String.Format("{0:D2}:{1:D2}", ts.Hours, ts.Minutes)
        End If

        ' SqlDataReaderの指定カラムの値を設定
        With m_CellReport
            .Cell(cell, 0, 0).Value = value
            .Cell(cell, 0, 0).AttrNo = attrNo
            .Cell(cell, 0, 0).Attr.Format = "hh:mm"
            .Cell(cell, 0, 0).Value = timeStr
        End With
    End Sub



    ''-------------------------------------------------------------------------------
    '' 
    ''-------------------------------------------------------------------------------


    ''' <summary>
    ''' 値設定
    ''' </summary>
    ''' <param name="excelName">Excel Name(Excel名前)</param>
    ''' <param name="row">Y軸オフセット</param>
    ''' <param name="value">設定値</param>
    Public Overloads Sub CellValue(ByVal excelName As String, ByVal row As Integer, ByVal value As Object, Optional ByVal defValue As Object = "")
        Dim setValue As Object = value
        If IsDBNull(value) Then
            If Not defValue Is Nothing Then
                setValue = defValue
            End If
        End If
        m_CellReport.Cell(excelName, 0, row).Value = setValue
    End Sub

End Class
