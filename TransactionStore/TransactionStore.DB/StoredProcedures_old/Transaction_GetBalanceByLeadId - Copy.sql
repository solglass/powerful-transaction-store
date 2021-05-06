if not exists (select 1 from sys.objects where name = 'Transaction_GetBalanceByLeadId') 
begin set noexec ON
END
GO
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
GO
set noexec OFF