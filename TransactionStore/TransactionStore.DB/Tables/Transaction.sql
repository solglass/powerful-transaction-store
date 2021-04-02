CREATE TABLE [dbo].[Transaction]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LeadId] [int] NOT NULL,
	[Amount] [decimal](9,2)  NOT NULL,
	[Type] [int] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,

	CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([Id] ASC) 
	WITH 
		( PAD_INDEX = OFF, 
		STATISTICS_NORECOMPUTE = OFF, 
		IGNORE_DUP_KEY = OFF, 
		ALLOW_ROW_LOCKS = ON,
		ALLOW_PAGE_LOCKS = ON)
) 
