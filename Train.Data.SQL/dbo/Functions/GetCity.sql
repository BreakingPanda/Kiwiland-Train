create function [dbo].[GetCity] 
(
	@city	varchar(100)
)
returns int
begin
	declare @id int;
	
	select @id = Id from dbo.City where [Name] = @city; 

	return @id	
end