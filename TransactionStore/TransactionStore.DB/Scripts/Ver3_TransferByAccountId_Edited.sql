declare @DbVersion int

select top 1 @DbVersion = version
from dbo.DbVersion order by id desc

if @DbVersion > 2 set noexec on

BEGIN TRANSACTION
GO
ALTER proc [dbo].[Transaction_SelectTransferByAccountId]
@accountId int
as
begin
select 
  t.Id,
    (select tr.AccountId 
    from [dbo].[Transaction] tr 
    where tr.Timestamp = t.Timestamp  
    and tr.Amount > 0) as RecipientAccountId,
	(select tr.AccountId 
	from [dbo].[Transaction] tr 
    where tr.Timestamp = t.Timestamp 
    and tr.Amount < 0) as SenderAccountId,
  t.Timestamp,
  t.Amount,
	 (select tr.Currency from [dbo].[Transaction] tr 
     where tr.Timestamp = t.Timestamp  
    and tr.Amount < 0) as SenderCurrency,
	  (select tr.Currency from [dbo].[Transaction] tr 
     where tr.Timestamp = t.Timestamp  
    and tr.Amount > 0) as RecipientCurrency
  from [dbo].[Transaction] t
  where t.AccountId = @accountId and t.[Type] = 3
end
GO
update dbo.DbVersion set Version=3
GO
COMMIT
set noexec off
