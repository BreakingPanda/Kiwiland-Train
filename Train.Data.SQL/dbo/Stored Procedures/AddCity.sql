create procedure [dbo].[AddCity] 
(
	@city	varchar(100)
)
as
begin
	if not exists (select 1 from dbo.City where [Name] = @city)
		insert into dbo.City ([Name]) VALUES (@city)
end