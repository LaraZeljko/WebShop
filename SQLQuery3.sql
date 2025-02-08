CREATE TABLE [dbo].[PurchaseItems_Zeljko]
(
    [PurchaseItemId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [PurchaseId] INT NOT NULL,
    [ProductId] INT NOT NULL,
    [Quantity] INT NOT NULL,
    FOREIGN KEY (PurchaseId) REFERENCES Purchases_Zeljko(PurchaseId),
    FOREIGN KEY (ProductId) REFERENCES WebShop_Zeljko(id),
    [Category] NVARCHAR(100) NOT NULL -- Kategorija proizvoda
);