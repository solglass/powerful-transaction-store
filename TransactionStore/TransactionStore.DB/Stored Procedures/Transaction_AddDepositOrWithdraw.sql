CREATE PROCEDURE [dbo].[Transaction_AddDepositOrWithdraw]
	@leadId int,
	@amount decimal,
	@currency int,
	@type int
as
begin
	insert into dbo.[Transaction] (LeadId, Amount, [Currency], [Type], [Timestamp])
	values (@leadId, @amount, @currency, @type, CURRENT_TIMESTAMP)
	select SCOPE_IDENTITY()
end