create proc [dbo].[Transaction_SelectByLeadId]
@leadId int
as
begin
select 
	t.Id,
	t.Timestamp,
	t.LeadId,
	t.Amount,
	t.Currency,
	t.[Type]
	from [dbo].[Transaction] t
	where t.LeadId = @leadId
end