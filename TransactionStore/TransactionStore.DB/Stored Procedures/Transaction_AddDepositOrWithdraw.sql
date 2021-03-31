CREATE PROCEDURE [dbo].[Transaction_AddDepositOrWithdraw]
	@leadId int,
	@amount int,
	@type int
as
begin
	insert into dbo.[Transaction] (LeadId, Amount, [Type], [Timestamp])
	values (@leadId, @amount, @type, CURRENT_TIMESTAMP)
	select SCOPE_IDENTITY()
end