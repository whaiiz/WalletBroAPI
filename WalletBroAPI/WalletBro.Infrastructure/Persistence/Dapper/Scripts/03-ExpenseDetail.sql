CREATE TABLE ExpenseDetail(
    Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    Name NVARCHAR(100),
    UnitPrice MONEY,
    UnitType NVARCHAR(50),
    InvoiceId UNIQUEIDENTIFIER not null,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    CONSTRAINT Fk_ExpenseDetail_Invoice
        FOREIGN KEY (InvoiceId)
        REFERENCES Invoices(Id)
        ON DELETE CASCADE
        ON UPDATE NO ACTION
)