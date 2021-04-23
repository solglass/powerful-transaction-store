create proc [dbo].[Transaction_SelectByAccountId]
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