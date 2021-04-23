create proc [dbo].[Transaction_SelectTransferByAccountId]
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
	where t.AccountId = @accountId and t.Amount < 0 and t.Type = 3
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
	where t.AccountId = @accountId and t.Amount > 0 and t.Type = 3
end