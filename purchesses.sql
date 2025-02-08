CREATE TABLE [dbo].[Purchases_Zeljko]
(
    [PurchaseId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  
    [CustomerName] NVARCHAR(100) NOT NULL,                
    [Address] NVARCHAR(255) NOT NULL,                     
    [Email] NVARCHAR(100) NOT NULL,                       
    [PurchaseDate] DATETIME NOT NULL                      
);