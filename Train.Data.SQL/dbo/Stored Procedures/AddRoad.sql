create procedure [dbo].[AddRoad]
(
	@from		varchar(MAX),
	@to			varchar(MAX),
	@distance	int
)
as
begin
	
	exec dbo.AddCity @from
	declare @from_id int = dbo.GetCity (@from)

	exec dbo.AddCity @to
	declare @to_id int  = dbo.GetCity(@to)
	
	insert into dbo.Road ([From], [To], [Distance])
	values (@from_id, @to_id, @distance)
end