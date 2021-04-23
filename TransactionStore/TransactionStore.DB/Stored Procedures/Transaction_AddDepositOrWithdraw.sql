CREATE PROCEDURE [dbo].[Transaction_AddDepositOrWithdraw]
	@accountId int,
	@amount decimal,
	@currency int,
	@type int
as
begin
	begin
	if @type = 1
		begin
			insert into dbo.[Transaction] (AccountId, Amount, [Currency], [Type], [Timestamp])
			values (@accountId, @amount, @currency, @type, CURRENT_TIMESTAMP)
		end
	else
		begin
			insert into dbo.[Transaction] (AccountId, Amount, [Currency], [Type], [Timestamp])
			values (@accountId, -@amount, @currency, @type, CURRENT_TIMESTAMP)
		end
	end
	select SCOPE_IDENTITY()
end