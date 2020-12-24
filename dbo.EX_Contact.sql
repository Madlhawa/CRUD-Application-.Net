CREATE TABLE [dbo].[EX_Contact]
(
	[ContactID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NULL, 
    [MobileNumber] NVARCHAR(50) NULL, 
    [Address] NVARCHAR(250) NULL
)
