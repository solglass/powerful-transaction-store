CREATE PROCEDURE [dbo].[Transaction_AddTransfer]
	@senderId int,
	@recipientId int,
	@amount int
as
begin
	Declare 
	@timestamp datetime2 = CURRENT_TIMESTAMP
	insert into dbo.[Transaction] (LeadId, Amount, [Type], [Timestamp])
	values (@senderId, -@amount, 3, @timestamp)
	Declare @senderTransactionId int = SCOPE_IDENTITY()
	insert into dbo.[Transaction] (LeadId, Amount, [Type], [Timestamp])
	values (@recipientId, @amount, 3, @timestamp)
	Declare @recipientTransactionId int = SCOPE_IDENTITY()
	select @senderTransactionId, @recipientTransactionId
end