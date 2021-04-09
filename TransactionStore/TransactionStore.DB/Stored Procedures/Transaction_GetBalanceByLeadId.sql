create proc dbo.[Transaction_GetBalanceByLeadId]
@leadId int
as
begin
select
	SUM (t.Amount) as Amount, t.Currency
	from [dbo].[Transaction] t
	where t.LeadId = @leadId
	group by t.Currency
end