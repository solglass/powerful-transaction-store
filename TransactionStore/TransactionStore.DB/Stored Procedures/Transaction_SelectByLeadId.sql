create proc [dbo].[Transaction_SelectByLeadId]
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