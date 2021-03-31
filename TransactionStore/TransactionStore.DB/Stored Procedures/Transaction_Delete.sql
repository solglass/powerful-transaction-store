CREATE PROCEDURE [dbo].[Transaction_Delete]
@id int
as
begin
Delete from [dbo].[Transaction] where Id = @id
end