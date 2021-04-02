CREATE PROCEDURE [dbo].[Transaction_AddDepositOrWithdraw]
	@leadId int,
	@amount decimal,
	@currency int,
	@type int
as
begin
	insert into dbo.[Transaction] (LeadId, Amount, [Type], [Currency], [Timestamp])
	values (@leadId, @amount, @type, @currency, CURRENT_TIMESTAMP)
	select SCOPE_IDENTITY()
end