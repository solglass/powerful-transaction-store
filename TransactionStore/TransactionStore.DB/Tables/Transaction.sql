CREATE TABLE [dbo].[Transaction]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[Amount] [decimal](18,4)  NOT NULL,
	[Currency] [int] NOT NULL,
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
GO 
CREATE NONCLUSTERED INDEX [IX_Transaction_Timestamp] ON [dbo].[Transaction] 
(
  [Timestamp] ASC
)
INCLUDE (AccountId);
GO
CREATE NONCLUSTERED INDEX [IX_Transaction_AccountId] ON [dbo].[Transaction] 
(
  [AccountId] ASC
)
INCLUDE (Currency, Type, Amount, Timestamp);

