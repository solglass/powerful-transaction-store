create proc [dbo].[Transaction_SelectTransferByLeadId]
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