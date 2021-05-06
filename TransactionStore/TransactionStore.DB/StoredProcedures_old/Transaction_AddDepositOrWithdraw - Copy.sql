if not exists (select 1 from sys.objects where name = 'Transaction_AddDepositOrWithdraw') 
begin set noexec ON
END
GO
CREATE PROCEDURE [dbo].[Transaction_AddDepositOrWithdraw]
	@leadId int,
	@amount decimal,
	@currency int,
	@type int
as
begin
	begin
	if @type = 1
		begin
			insert into dbo.[Transaction] (LeadId, Amount, [Currency], [Type], [Timestamp])
			values (@leadId, @amount, @currency, @type, CURRENT_TIMESTAMP)
		end
	else
		begin
			insert into dbo.[Transaction] (LeadId, Amount, [Currency], [Type], [Timestamp])
			values (@leadId, -@amount, @currency, @type, CURRENT_TIMESTAMP)
		end
	end
	select SCOPE_IDENTITY()
end
GO
set noexec OFF