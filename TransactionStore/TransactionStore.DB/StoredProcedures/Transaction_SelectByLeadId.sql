if not exists (select 1 from sys.objects where name = 'Transaction_SelectByLeadId') 
begin set noexec ON
END
GO
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
GO
set noexec OFF