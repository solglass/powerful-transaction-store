if not exists (select 1 from sys.objects where name = 'Transaction') 
begin set noexec ON


CREATE TABLE [dbo].[Transaction]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LeadId] [int] NOT NULL,
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
 
CREATE NONCLUSTERED INDEX [IX_Transaction_Timestamp] ON [dbo].[Transaction] 
(
  [Timestamp] ASC
)
INCLUDE (LeadId);

CREATE NONCLUSTERED INDEX [IX_Transaction_LeadId] ON [dbo].[Transaction] 
(
  [LeadId] ASC
)
INCLUDE (Currency, Type, Amount, Timestamp);

set noexec OFF
END