CREATE TABLE ExpenseDetail(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100),
    UnitPrice MONEY,
    UnitType NVARCHAR(50),
    InvoiceId int not null,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    CONSTRAINT Fk_ExpenseDetail_Invoice
        FOREIGN KEY (InvoiceId)
        REFERENCES Invoice(Id)
        ON DELETE CASCADE
        ON UPDATE NO ACTION
)