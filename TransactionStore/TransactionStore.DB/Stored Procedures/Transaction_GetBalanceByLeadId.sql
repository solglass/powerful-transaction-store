create proc dbo.[Transaction_GetBalanceByLeadId]
@leadId int
as
begin
select
	SUM (t.Amount)
	from [dbo].[Transaction] t
	where t.LeadId = @leadId
	group by t.LeadId
end