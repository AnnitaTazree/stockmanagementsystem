use StockManagement_DB
go
CREATE TABLE signlog (
    ID INT NOT NULL PRIMARY KEY IDENTITY,
    Email   NVARCHAR (105) NOT NULL ,
    Password NVARCHAR (10) NOT NULL,
   
); 

INSERT INTO signlog (Email,Password)
VALUES
('annitatazree38@gmail.com','admin3815'),
('meherunnesatisa@gmail.com','admin123'),
('shamimhossain@gmail.com','admin');