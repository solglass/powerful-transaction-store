create proc [dbo].[Transaction_GenerateData]
@amountStrs int
as
begin
	declare
	@mounth int = FLOOR(RAND()*(12-1+1))+1,
	@data int = FLOOR(RAND()*(28-1+1))+1,
	@year int = FLOOR(RAND()*(2021-2010+1))+2010,
	@amount float = FLOOR(RAND()*(5000000-1+1))+1 + (FLOOR(RAND()*(10000-1+1))+1) / 10000,
	@type int = FLOOR(RAND()*(3-1+1))+1,
	@currency int = FLOOR(RAND()*(4-1+1))+1,
	@leadId int = 0,
	@recId int = 0;
	DECLARE @FromDate DATETIME 
	DECLARE @ToDate   DATETIME 

	DECLARE @Seconds INT 
	DECLARE @Random INT
	DECLARE @Milliseconds INT
	declare @rdate datetime2

	DECLARE @i int = 0

	WHILE @i < 5000000
	BEGIN
		SET @i = @i + 1

		set @FromDate = '1/1/2001'
		set @ToDate ='1/1/2021'

		set @Seconds = DATEDIFF(SECOND, @FromDate, @ToDate)
		set @Random = ROUND(((@Seconds-1) * RAND()), 0)
		set @Milliseconds = ROUND((999 * RAND()), 0)
		set @rdate = DATEADD(MILLISECOND, @Milliseconds, DATEADD(SECOND, @Random, @FromDate))

		set @amount = FLOOR(RAND()*(5000000-1+1))+1 + (FLOOR(RAND()*(10000-1+1))+1) / 10000;
		set @type = FLOOR(RAND()*(3-1+1))+1;
		set @currency = FLOOR(RAND()*(4-1+1))+1;

		set @leadId = FLOOR(RAND()*(5000000-1+1))+1;
		if @leadId > 2500000
			set @recId  = FLOOR(RAND()*(2500000-1+1))+1
		else 
			set @recId  = FLOOR(RAND()*(5000000-2500000+1))+2500000
		if @type = 1
			insert into dbo.[Transaction] (LeadId, Amount, Currency, Type, Timestamp) values (@leadId, @amount, @currency,@type, @rdate);
		else if @type = 2
			insert into dbo.[Transaction] (LeadId, Amount, Currency, Type, Timestamp) values (@leadId, -@amount, @currency,@type, @rdate);
		else
			insert into dbo.[Transaction] (LeadId, Amount, Currency, Type, Timestamp) values (@leadId, -@amount, @currency,@type, @rdate), (@recId, @amount, @currency,@type, @rdate);
	END
end