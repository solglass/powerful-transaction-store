/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


:r ..\Tables\Transaction.sql
:r ..\StoredProcedures\Transaction_AddDepositOrWithdraw.sql
:r ..\StoredProcedures\Transaction_AddTransfer.sql
:r ..\StoredProcedures\Transaction_GenerateData.sql
:r ..\StoredProcedures\Transaction_GetBalanceByLeadId.sql
:r ..\StoredProcedures\Transaction_SelectByLeadId.sql
:r ..\StoredProcedures\Transaction_SelectTransferByLeadId.sql
:r .\Ver2_Accounts_Added.sql
:r .\Ver3_TransferByAccountId_Edited.sql

