declare @DbVersion int

select top 1 @DbVersion = version
from dbo.DbVersion order by id desc

if @DbVersion > 2 set noexec on

BEGIN TRANSACTION
GO
EXECUTE sp_rename N'dbo.[Transaction].AccountId', N'Tmp_LeadId_1', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.[Transaction].Tmp_LeadId_1', N'LeadId', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.[Transaction].IX_Transaction_AccountId', N'IX_Transaction_LeadId', 'INDEX'    
GO
ALTER TABLE dbo.[Transaction] SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
GO
BEGIN TRANSACTION
GO
ALTER PROCEDURE [dbo].[Transaction_AddDepositOrWithdraw]
	@leadId int,
	@amount decimal,
	@currency int,
	@type int
as
begin
	begin
	if @type = 1
		begin
			insert into dbo.[Transaction] (LeadId, Amount, [Currency], [Type], [Timestamp])
			values (@leadId, @amount, @currency, @type, CURRENT_TIMESTAMP)
		end
	else
		begin
			insert into dbo.[Transaction] (LeadId, Amount, [Currency], [Type], [Timestamp])
			values (@leadId, -@amount, @currency, @type, CURRENT_TIMESTAMP)
		end
	end
	select SCOPE_IDENTITY()
end
GO
ALTER PROCEDURE [dbo].[Transaction_AddTransfer]
	@senderId int,
	@recipientId int,
	@senderAmount decimal,
	@recipientAmount decimal,
	@senderСurrency int,
	@recipientСurrency int
as
begin
	Declare 
	@timestamp datetime2 = CURRENT_TIMESTAMP
	insert into dbo.[Transaction] (LeadId, Amount, [Currency], [Type], [Timestamp])
	values (@senderId, -@senderAmount, @senderСurrency, 3, @timestamp)
	Declare @senderTransactionId int = SCOPE_IDENTITY()
	insert into dbo.[Transaction] (LeadId, Amount, [Currency], [Type], [Timestamp])
	values (@recipientId, @recipientAmount, @recipientСurrency, 3, @timestamp)
	Declare @recipientTransactionId int = SCOPE_IDENTITY()
	select @senderTransactionId, @recipientTransactionId
end
GO
ALTER proc [dbo].[Transaction_GenerateData]
@amountStrs int
as
begin
	declare
	@mounth int = FLOOR(RAND()*(12-1+1))+1,
	@data int = FLOOR(RAND()*(28-1+1))+1,
	@year int = FLOOR(RAND()*(2021-2010+1))+2010,
	@amount float = FLOOR(RAND()*(5000000-1+1))+1 + (FLOOR(RAND()*(10000-1+1))+1) / 10000,
	@type int = FLOOR(RAND()*(3-1+1))+1,
	@currency int = FLOOR(RAND()*(4-1+1))+1,
	@leadId int = 0,
	@recId int = 0;
	DECLARE @FromDate DATETIME 
	DECLARE @ToDate   DATETIME 

	DECLARE @Seconds INT 
	DECLARE @Random INT
	DECLARE @Milliseconds INT
	declare @rdate datetime2

	DECLARE @i int = 0

	WHILE @i < 5000000
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

		set @leadId = FLOOR(RAND()*(5000000-1+1))+1;
		if @leadId > 2500000
			set @recId  = FLOOR(RAND()*(2500000-1+1))+1
		else 
			set @recId  = FLOOR(RAND()*(5000000-2500000+1))+2500000
		if @type = 1
			insert into dbo.[Transaction] (LeadId, Amount, Currency, Type, Timestamp) values (@leadId, @amount, @currency,@type, @rdate);
		else if @type = 2
			insert into dbo.[Transaction] (LeadId, Amount, Currency, Type, Timestamp) values (@leadId, -@amount, @currency,@type, @rdate);
		else
			insert into dbo.[Transaction] (LeadId, Amount, Currency, Type, Timestamp) values (@leadId, -@amount, @currency,@type, @rdate), (@recId, @amount, @currency,@type, @rdate);
	END
end
GO
EXEC sp_rename 'Transaction_SelectByAccountId','Transaction_SelectByLeadId'
GO
ALTER proc [dbo].[Transaction_SelectByLeadId]
@leadId int
as
begin
select 
	t.Id,
	t.LeadId,
	t.Amount,
	t.Currency,
	t.[Type],
	t.Timestamp
	from [dbo].[Transaction] t
	where t.LeadId = @leadId and (t.Type = 1 or t.Type = 2)
end
GO
EXEC sp_rename 'Transaction_SelectTransferByAccountId','Transaction_SelectTransferByLeadId'
GO
ALTER proc [dbo].[Transaction_SelectTransferByLeadId]
@leadId int
as
begin
select 
	t.Id,
	t.LeadId as SenderId,
		(select tr.LeadId 
		from [dbo].[Transaction] tr 
		where tr.Timestamp = t.Timestamp 
		and tr.LeadId <> t.LeadId) as RecipientId,
	t.Timestamp,
	t.Amount,
	t.Currency
	from [dbo].[Transaction] t
	where t.LeadId = @leadId and t.Amount < 0 and t.Type = 3
union all
	select 
	t.Id,
	(select tr.LeadId 
		from [dbo].[Transaction] tr 
		where tr.Timestamp = t.Timestamp 
		and tr.LeadId <> t.LeadId) as SenderId,
	t.LeadId as RecipientId,
	t.Timestamp,
	t.Amount,
	t.Currency
	from [dbo].[Transaction] t
	where t.LeadId = @leadId and t.Amount > 0 and t.Type = 3
end
GO
EXEC sp_rename 'Transaction_GetBalanceByAccountId','Transaction_GetBalanceByLeadId'
GO
ALTER proc [dbo].[Transaction_GetBalanceByLeadId]
@leadId int
as
begin
select
	SUM (t.Amount) as Amount, t.Currency
	from [dbo].[Transaction] t
	where t.LeadId = @leadId
	group by t.Currency
end

update dbo.DbVersion set Version=1
GO
COMMIT

set noexec off

