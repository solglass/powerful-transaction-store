create proc [dbo].[Transaction_SelectDepositOrWithdrawByLeadId]
@leadId int,
@type int
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
	where t.LeadId = @leadId  and t.[Type] = @type
end