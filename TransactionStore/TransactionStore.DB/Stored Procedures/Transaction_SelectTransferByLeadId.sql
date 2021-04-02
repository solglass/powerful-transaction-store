create proc [dbo].[Transaction_SelectTransferByLeadId]
@leadId int
as
begin
select 
	t.LeadId as SenderId,
		(select tr.LeadId 
		from [dbo].[Transaction] tr 
		where tr.Timestamp = t.Timestamp 
		and tr.Amount = -t.Amount 
		and tr.LeadId <> t.LeadId) as RecipientId,
	t.Timestamp,
	t.Amount,
	t.Currency,
	t.[Type]
	from [dbo].[Transaction] t
	where t.LeadId = @leadId and t.Amount < 0
union all
	select 
	(select tr.LeadId 
		from [dbo].[Transaction] tr 
		where tr.Timestamp = t.Timestamp 
		and tr.Amount = -t.Amount 
		and tr.LeadId <> t.LeadId) as SenderId,
	t.LeadId as RecipientId,
	t.Timestamp,
	t.Amount,
	t.Currency,
	t.[Type]
	from [dbo].[Transaction] t
	where t.LeadId = @leadId and t.Amount > 0
end