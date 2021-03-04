create procedure [deployment].[Import_Data]
as
begin
	INSERT INTO dbo.City ([Name])
	VALUES 
	('Brisbane'),
	('Sydney'),
	('Melbourne'),
	('Perth'),
	('Canberra'),
	('Cairns'),
	('Adelaide'),
	('Hobart'),
	('Coffs Hourbor'),
	('Gold Coast'),
	('Alice Spring')

	declare @brisbane int = dbo.GetCity('Brisbane')
	declare @sydney int = dbo.GetCity('Sydney')
	declare @melbourne int = dbo.GetCity('Melbourne')
	declare @perth int = dbo.GetCity('Perth')
	declare @canberra int = dbo.GetCity('Canberra')
	declare @cairns int = dbo.GetCity('Cairns')
	declare @adelaide int = dbo.GetCity('Adelaide')
	declare @hobart int = dbo.GetCity('Hobart')
	declare @coffs_harbour int = dbo.GetCity('Coffs Hourbor')
	declare @gold_coast int = dbo.GetCity('Gold Coast')
	declare @alice_spring int = dbo.GetCity('Alice Spring')

	INSERT INTO dbo.Road ([From],[To],[Distance])
	VALUES 
	(@sydney,@melbourne,877),		
	(@melbourne,@sydney,877),
	(@sydney,@canberra,286),		
	(@canberra,@sydney,286),
	(@sydney,@coffs_harbour,526),	
	(@coffs_harbour,@sydney,526),
	(@brisbane,@sydney,915),		
	(@sydney,@brisbane,915),
	(@brisbane,@gold_coast,59),		
	(@gold_coast,@brisbane,59),
	(@brisbane,@adelaide,2022),		
	(@adelaide,@brisbane,2022),		
	(@melbourne,@adelaide,726),		
	(@adelaide,@melbourne,726),		
	(@canberra,@melbourne,661),		
	(@melbourne,@canberra,661),		
	(@hobart,@melbourne,696),		
	(@melbourne,@hobart,696),
	(@cairns,@brisbane,1684),		
	(@brisbane,@cairns,1684),
	(@brisbane,@perth,4309),		
	(@perth,@brisbane,4309),
	(@sydney,@perth,3934),			
	(@perth,@sydney,3934),
	(@adelaide,@perth,2696),		
	(@perth,@adelaide,2696),
	(@brisbane,@coffs_harbour,390),
	(@coffs_harbour,@brisbane,390),
	(@coffs_harbour,@gold_coast,317),
	(@gold_coast,@coffs_harbour,317),
	(@alice_spring,@brisbane,2529),
	(@brisbane,@alice_spring,2529),
	(@alice_spring,@adelaide,1535),
	(@adelaide,@alice_spring,1535),
	(@alice_spring,@sydney,2773),
	(@sydney,@alice_spring,2773)
end