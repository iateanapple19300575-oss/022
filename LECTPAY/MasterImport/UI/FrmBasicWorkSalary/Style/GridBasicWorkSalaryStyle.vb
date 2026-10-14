''' <summary>
''' 業務給基本設定（本科・日特・特選）画面用 DataTable 定義
''' </summary>
Public Class GridBasicWorkSalaryStyle
    Implements IGridColumnDefinition

    ''' <summary>
    ''' 業務給基本設定の一覧に必要な列定義を作成し、指定された DataGridView に適用する。
    ''' </summary>
    ''' <param name="grid"></param>
    Public Sub Apply(ByRef grid As DataGridView) Implements IGridColumnDefinition.Apply

        ' 一覧の項目定義
        Dim defs As New List(Of GridColumnDefinition) From {
            New GridColumnBuilder("EffectiveDate", "適用開始日", 100).
                Align(DataGridViewContentAlignment.MiddleCenter).
                Sort(False).
                CellReadOnly().
                Build(),
            New GridColumnBuilder("TaskBaseUnitPrice", "標準単価", 100).
                Align(DataGridViewContentAlignment.MiddleRight).
                Sort(False).
                CellReadOnly().
                Build(),
            New GridColumnBuilder("DeemedTimeBefore", "みなし勤務時間(前)", 150).
                Align(DataGridViewContentAlignment.MiddleRight).
                Sort(False).
                CellReadOnly().
                Build(),
            New GridColumnBuilder("DeemedTimeAfter", "みなし勤務時間(前)", 150).
                Align(DataGridViewContentAlignment.MiddleRight).
                Sort(False).
                CellReadOnly().
                Build(),
            New GridColumnBuilder("Id", "ID", 10).
                Align(DataGridViewContentAlignment.MiddleCenter).
                CellReadOnly().
                Hide().
                Build(),
            New GridColumnBuilder("RowVersion", "RowVersion", 10).
                Align(DataGridViewContentAlignment.MiddleCenter).
                CellReadOnly().
                Hide().
                Build()
        }

        ' 既存列をクリアし、自動生成を無効化
        grid.Columns.Clear()
        grid.AutoGenerateColumns = False

        ' 列定義を DataGridView に追加
        For Each def In defs
            grid.Columns.Add(GridColumnFactory.Create(def))
        Next

    End Sub

    Public Sub Update(ByRef grid As DataGridView) Implements IGridColumnDefinition.Update
        Throw New NotImplementedException()
    End Sub
End Class
