create proc [dbo].[Transaction_SelectById]
@id int
as
begin
select 
	t.LeadId,
	t.Timestamp,
	t.Amount,
	t.[Type]
	from [dbo].[Transaction] t
	where t.Id = @id
end