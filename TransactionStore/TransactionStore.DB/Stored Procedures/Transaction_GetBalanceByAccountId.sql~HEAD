create proc dbo.[Transaction_GetBalanceByAccountId]
@accountId int
as
begin
select
	SUM (t.Amount) as Amount, t.Currency
	from [dbo].[Transaction] t
	where t.AccountId = @accountId
	group by t.Currency
end