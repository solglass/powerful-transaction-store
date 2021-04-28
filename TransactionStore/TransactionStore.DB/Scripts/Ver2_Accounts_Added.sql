declare @DbVersion int

select top 1 @DbVersion = version
from dbo.DbVersion order by id desc

if @DbVersion > 1 set noexec on

BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
EXECUTE sp_rename N'dbo.[Transaction].LeadId', N'Tmp_AccountId_1', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.[Transaction].Tmp_AccountId_1', N'AccountId', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.[Transaction].IX_Transaction_LeadId', N'IX_Transaction_AccountId', 'INDEX';   
GO
ALTER TABLE dbo.[Transaction] SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
GO
BEGIN TRANSACTION
GO
ALTER PROCEDURE [dbo].[Transaction_AddDepositOrWithdraw]
  @accountId int,
  @amount decimal,
  @currency int,
  @type int
as
begin
  begin
  if @type = 1
    begin
      insert into dbo.[Transaction] (AccountId, Amount, [Currency], [Type], [Timestamp])
      values (@accountId, @amount, @currency, @type, CURRENT_TIMESTAMP)
    end
  else
    begin
      insert into dbo.[Transaction] (AccountId, Amount, [Currency], [Type], [Timestamp])
      values (@accountId, -@amount, @currency, @type, CURRENT_TIMESTAMP)
    end
  end
  select SCOPE_IDENTITY()
end
GO
ALTER PROCEDURE [dbo].[Transaction_AddTransfer]
  @senderAccountId int,
  @recipientAccountId int,
  @senderAmount decimal,
  @recipientAmount decimal,
  @senderСurrency int,
  @recipientСurrency int
as
begin
  Declare 
  @timestamp datetime2 = CURRENT_TIMESTAMP
  insert into dbo.[Transaction] (AccountId, Amount, [Currency], [Type], [Timestamp])
  values (@senderAccountId, -@senderAmount, @senderСurrency, 3, @timestamp)
  Declare @senderTransactionId int = SCOPE_IDENTITY()
  insert into dbo.[Transaction] (AccountId, Amount, [Currency], [Type], [Timestamp])
  values (@recipientAccountId, @recipientAmount, @recipientСurrency, 3, @timestamp)
  Declare @recipientTransactionId int = SCOPE_IDENTITY()
  select @senderTransactionId, @recipientTransactionId
end
GO
ALTER proc [dbo].[Transaction_GenerateData]
@amountStrs int
as
begin
  declare
    @mounth int,
    @data int,
    @year int,
    @amount float,
    @type int,
    @currency int,
    @accountId int,
    @recId int;
  DECLARE @FromDate DATETIME 
  DECLARE @ToDate   DATETIME 

  DECLARE @Seconds INT 
  DECLARE @Random INT
  DECLARE @Milliseconds INT
  declare @rdate datetime2

  DECLARE @i int = 0

  WHILE @i < @amountStrs
  BEGIN
    SET @i = @i + 1

    set @FromDate = '1/1/2001'
    set @ToDate ='1/1/2021'

    set @Seconds = DATEDIFF(SECOND, @FromDate, @ToDate)
    set @Random = ROUND(((@Seconds-1) * RAND()), 0)
    set @Milliseconds = ROUND((999 * RAND()), 0)
    set @rdate = DATEADD(MILLISECOND, @Milliseconds, DATEADD(SECOND, @Random, @FromDate))

    set @amount = FLOOR(RAND()*(5000000-1+1))+1 + (FLOOR(RAND()*(10000-1+1))+1) / 10000;
    set @type = FLOOR(RAND()*(3-1+1))+1;
    set @currency = FLOOR(RAND()*(4-1+1))+1;

set @accountId = FLOOR(RAND()*(5000000-1+1))+1;
    if @accountId > 2500000
      set @recId  = FLOOR(RAND()*(2500000-1+1))+1
    else 
      set @recId  = FLOOR(RAND()*(5000000-2500000+1))+2500000
    if @type = 1
      insert into dbo.[Transaction] (AccountId, Amount, Currency, Type, Timestamp) values (@accountId, @amount, @currency,@type, @rdate);
    else if @type = 2
      insert into dbo.[Transaction] (AccountId, Amount, Currency, Type, Timestamp) values (@accountId, -@amount, @currency,@type, @rdate);
    else
      insert into dbo.[Transaction] (AccountId, Amount, Currency, Type, Timestamp) values (@accountId, -@amount, @currency,@type, @rdate), (@recId, @amount, @currency,@type, @rdate);
  END
end
GO
EXEC sp_rename 'Transaction_SelectByLeadId','Transaction_SelectByAccountId'
GO
ALTER proc [dbo].[Transaction_SelectByAccountId]
@accountId int
as
begin
select 
  t.Id,
  t.AccountId,
  t.Amount,
  t.Currency,
  t.[Type],
  t.Timestamp
  from [dbo].[Transaction] t
  where t.AccountId = @accountId and (t.Type = 1 or t.Type = 2)
end
GO
EXEC sp_rename 'Transaction_SelectTransferByLeadId','Transaction_SelectTransferByAccountId'
GO
ALTER proc [dbo].[Transaction_SelectTransferByAccountId]
@accountId int
as
begin
select 
  t.Id,
  t.AccountId as SenderAccountId,
    (select tr.AccountId 
    from [dbo].[Transaction] tr 
    where tr.Timestamp = t.Timestamp  
    and tr.AccountId <> t.AccountId) as RecipientAccountId,
  t.Timestamp,
  t.Amount,
  t.Currency
  from [dbo].[Transaction] t
  where t.AccountId = @accountId and t.Amount < 0 and t.[Type] = 3
union all
  select 
  t.Id,
  (select tr.AccountId 
    from [dbo].[Transaction] tr 
    where tr.Timestamp = t.Timestamp 
    and tr.AccountId <> t.AccountId) as SenderAccountId,
  t.AccountId as RecipientAccountId,
  t.Timestamp,
  t.Amount,
  t.Currency
  from [dbo].[Transaction] t
  where t.AccountId = @accountId and t.Amount > 0 and t.[Type] = 3
end
GO
EXEC sp_rename 'Transaction_GetBalanceByLeadId','Transaction_GetBalanceByAccountId'
GO
ALTER proc [dbo].[Transaction_GetBalanceByAccountId]
@accountId int
as
begin
select
  SUM (t.Amount) as Amount, t.Currency
  from [dbo].[Transaction] t
  where t.AccountId = @accountId
  group by t.Currency
end
GO

update dbo.DbVersion set Version=2
GO
COMMIT
set noexec off
