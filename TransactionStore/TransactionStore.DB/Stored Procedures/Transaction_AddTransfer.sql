CREATE PROCEDURE [dbo].[Transaction_AddTransfer]
	@senderAccountId int,
	@recipientAccountId int,
	@senderAccountAmount decimal,
	@recipientAccountAmount decimal,
	@senderAccountСurrency int,
	@recipientAccountСurrency int
as
begin
	Declare 
	@timestamp datetime2 = CURRENT_TIMESTAMP
	insert into dbo.[Transaction] (AccountId, Amount, [Currency], [Type], [Timestamp])
	values (@senderAccountId, -@senderAccountAmount, @senderAccountСurrency, 3, @timestamp)
	Declare @senderAccountTransactionId int = SCOPE_IDENTITY()
	insert into dbo.[Transaction] (AccountId, Amount, [Currency], [Type], [Timestamp])
	values (@recipientAccountId, @recipientAccountAmount, @recipientAccountСurrency, 3, @timestamp)
	Declare @recipientAccountTransactionId int = SCOPE_IDENTITY()
	select @senderAccountTransactionId, @recipientAccountTransactionId
end