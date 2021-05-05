declare @DbVersion int

select top 1 @DbVersion = version
from dbo.DbVersion order by id desc

if @DbVersion > 3 set noexec on

BEGIN TRANSACTION
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
update dbo.DbVersion set Version=2
GO
COMMIT
set noexec off
