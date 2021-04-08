CREATE PROCEDURE [dbo].[Transaction_AddTransfer]
	@senderId int,
	@recipientId int,
	@senderAmount decimal,
	@recipientAmount decimal,
	@senderСurrency int,
	@recipientСurrency int
as
begin
	Declare 
	@timestamp datetime2 = CURRENT_TIMESTAMP
	insert into dbo.[Transaction] (LeadId, Amount, [Currency], [Type], [Timestamp])
	values (@senderId, -@senderAmount, @senderСurrency, 3, @timestamp)
	Declare @senderTransactionId int = SCOPE_IDENTITY()
	insert into dbo.[Transaction] (LeadId, Amount, [Currency], [Type], [Timestamp])
	values (@recipientId, @recipientAmount, @recipientСurrency, 3, @timestamp)
	Declare @recipientTransactionId int = SCOPE_IDENTITY()
	select @senderTransactionId, @recipientTransactionId
end