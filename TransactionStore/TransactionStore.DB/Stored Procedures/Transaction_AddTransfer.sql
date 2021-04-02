CREATE PROCEDURE [dbo].[Transaction_AddTransfer]
	@senderId int,
	@recipientId int,
	@amount decimal,
	@currency int
as
begin
	Declare 
	@timestamp datetime2 = CURRENT_TIMESTAMP
	insert into dbo.[Transaction] (LeadId, Amount, [Currency], [Type], [Timestamp])
	values (@senderId, -@amount, @currency, 3, @timestamp)
	Declare @senderTransactionId int = SCOPE_IDENTITY()
	insert into dbo.[Transaction] (LeadId, Amount, [Currency], [Type], [Timestamp])
	values (@recipientId, @amount, @currency, 3, @timestamp)
	Declare @recipientTransactionId int = SCOPE_IDENTITY()
	select @senderTransactionId, @recipientTransactionId
end