CREATE TABLE [dbo].[Transaction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LeadId] [int] NOT NULL,
	[Amount] [int]  NOT NULL,
	[Type] [int] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL
	)