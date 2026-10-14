Imports MasterImport.MasterEditEnum
Imports Nts.Freamwork.Common.Utilities

Namespace MasterEdit.Dto

    ''' <summary>
    ''' 業務給基本設定画面 Service
    ''' </summary>
    Public Class BasicWorkSalaryService
        Inherits MasterEditServiceBase(Of BasicWorkSalaryModel, BasicWorkSalaryModel, BasicWorkSalaryEntity, BasicWorkSalaryValidator)

        ''' <summary>
        ''' 業務給基本設定マスタ Repository
        ''' </summary>
        Private _mstRepo As HourlyWageRepository

        ''' <summary>
        ''' 業務給基本設定画面 Repository
        ''' </summary>
        Private _bwsRepo As BasicWorkSalaryRepository

        ''' <summary>
        ''' バリデーション Validator
        ''' </summary>
        Protected _validation As New BasicWorkSalaryValidator


        ''' <summary>
        ''' 画面表示データ取得
        ''' </summary>
        ''' <param name="dto"></param>
        ''' <returns></returns>
        Public Function LoadInitData(dto As BasicWorkSalaryModel) As BasicWorkSalaryModel
            Dim result As New BasicWorkSalaryModel

            Try
                result.DataItemList = LoadDataList(dto)

                ' 初期表示情報
                result.Id = dto.Id
                result.EffectiveDate = dto.EffectiveDate
                result.TaskBaseUnitPrice = dto.TaskBaseUnitPrice
                result.DeemedTimeBefore = dto.DeemedTimeBefore
                result.DeemedTimeAfter = dto.DeemedTimeAfter
                result.CreateDate = dto.CreateDate
                result.CreateUser = dto.CreateUser
                result.UpdateDate = dto.UpdateDate
                result.UpdateUser = dto.UpdateUser
                result.RowVersion = dto.RowVersion

                Return result
            Catch ex As Exception
                Message = "業務給基本設定画面の初期表示データ取得に失敗しました"
                GlobalLog.Error(Message, ex)
                Throw New ViewControllerException(Message, ex)
            End Try
        End Function

        ''' <summary>
        ''' 業務給基本設定データを取得します。
        ''' </summary>
        Public Function LoadDataList(model As BasicWorkSalaryModel) As List(Of BasicWorkSalaryDto)
            Dim dt As New List(Of BasicWorkSalaryEntity)

            Try
                Using uow As New DbTransactionScope()
                    _bwsRepo = New BasicWorkSalaryRepository(uow)
                    dt = _bwsRepo.SearchAll()
                End Using

                Return BasicWorkSalaryMapper.ToViewStateList(dt)
            Catch ex As Exception
                Message = "業務給基本設定画面の表示データ取得に失敗しました"
                GlobalLog.Error(Message, ex)
                Throw New ViewControllerException(Message, ex)
            End Try
        End Function

        ''' <summary>
        ''' 業務給基本設定データを追加します。
        ''' </summary>
        ''' <param name="dto"></param>
        ''' <returns>追加件数</returns>
        Public Overrides Function Insert(dto As BasicWorkSalaryModel) As Integer
            Dim result As Integer = OptimisticLockingErrorType.DML_SUCCESS

            dto.CreateUser = Environment.UserDomainName
            Dim entity As HourlyWageEntity = DtoToEntity(dto)

            Try
                Using uow As New DbTransactionScope(True)
                    Try
                        _mstRepo = New HourlyWageRepository(uow)

                        ' 重複確認
                        Dim targetEntity As HourlyWageEntity
                        ' TODO: 後で見直し
                        entity.Id = Nothing
                        targetEntity = _mstRepo.FindByKey(entity)
                        If targetEntity.Id.HasValue Then
                            result = OptimisticLockingErrorType.INSERT_DUPLICATE
                            uow.RollbackTransaction()
                            Return result
                        End If

                        ' 追加
                        Dim count = _mstRepo.Insert(entity)
                        If count = 0 Then
                            result = OptimisticLockingErrorType.INSERT_DUPLICATE
                            uow.RollbackTransaction()
                        Else
                            uow.CommitTransaction()
                        End If

                    Catch ex As Exception
                        uow.RollbackTransaction()
                        Throw
                    End Try
                End Using

            Catch ex As Exception
                Throw
            End Try

            Return result
        End Function

        ''' <summary>
        ''' 業務給基本設定データを更新します。
        ''' </summary>
        ''' <param name="dto"></param>
        ''' <returns>更新件数</returns>
        Public Overrides Function Update(dto As BasicWorkSalaryModel) As Integer
            Dim result As Integer = OptimisticLockingErrorType.DML_SUCCESS

            dto.UpdateUser = Environment.UserDomainName
            Dim entity As HourlyWageEntity = DtoToEntity(dto)

            Try
                Using uow As New DbTransactionScope(True)
                    Try
                        _mstRepo = New HourlyWageRepository(uow)

                        ' 存在確認
                        Dim targetEntity As HourlyWageEntity
                        targetEntity = _mstRepo.FindById(entity)
                        If targetEntity.Id.HasValue Then
                            If Not targetEntity.RowVersion.SequenceEqual(entity.RowVersion) Then
                                result = OptimisticLockingErrorType.UPDATE_CHANGED
                                uow.RollbackTransaction()
                                Return result
                            End If
                        Else
                            result = OptimisticLockingErrorType.UPDATE_DELETED
                            uow.RollbackTransaction()
                            Return result
                        End If

                        ' 更新
                        Dim count = _mstRepo.Update(entity)
                        If count = 0 Then
                            result = OptimisticLockingErrorType.UPDATE_NO_MATCH
                            uow.RollbackTransaction()
                        Else
                            uow.CommitTransaction()
                        End If

                    Catch ex As Exception
                        uow.RollbackTransaction()
                        Throw
                    End Try
                End Using

            Catch ex As Exception
                Throw
            End Try

            Return result
        End Function

        ''' <summary>
        ''' 業務給基本設定データを削除します。
        ''' </summary>
        ''' <param name="dto"></param>
        ''' <returns>削除件数</returns>
        Public Overrides Function Delete(dto As BasicWorkSalaryModel) As Integer
            Dim result As Integer = OptimisticLockingErrorType.DML_SUCCESS
            Dim entity As HourlyWageEntity = DtoToEntity(dto)

            Using uow As New DbTransactionScope(True)
                Try
                    _mstRepo = New HourlyWageRepository(uow)

                    ' 削除
                    Dim count = _mstRepo.DeleteById(entity)
                    If count = 0 Then
                        result = OptimisticLockingErrorType.DELETE_NO_MATCH
                        uow.RollbackTransaction()
                    Else
                        uow.CommitTransaction()
                    End If

                Catch ex As Exception
                    uow.RollbackTransaction()
                    Throw
                End Try
            End Using

            Return result
        End Function

        ''' <summary>
        ''' 業務給基本設定画面 Entity から 業務給基本設定テーブル Entity に詰め替え
        ''' </summary>
        ''' <param name="dto"></param>
        ''' <returns></returns>
        Public Shared Function DtoToEntity(ByVal dto As BasicWorkSalaryModel) As HourlyWageEntity
            If dto Is Nothing Then
                Return Nothing
            End If

            Dim entity As New HourlyWageEntity
            entity.Id = dto.Id
            entity.EffectiveDate = If(StringUtil.IsNullOrWhiteSpace(dto.EffectiveDate), Date.MinValue, CDate(dto.EffectiveDate))
            entity.TaskBaseUnitPrice = ToIntSafeOrNothing(dto.TaskBaseUnitPrice)
            entity.DeemedTimeBefore = TimeUtil.TotalHoursToTimeSpan(dto.DeemedTimeBefore)
            entity.DeemedTimeAfter = TimeUtil.TotalHoursToTimeSpan(dto.DeemedTimeAfter)
            entity.CreateDate = ToDateSafeOrNothing(dto.CreateDate)
            entity.CreateUser = ToStringSafeOrEmpty(dto.CreateUser)
            entity.UpdateDate = ToDateSafeOrNothing(dto.UpdateDate)
            entity.UpdateUser = ToStringSafeOrEmpty(dto.UpdateUser)
            entity.RowVersion = ToByteSafeOrNothing(dto.RowVersion)
            Return entity
        End Function

    End Class

End Namespace
