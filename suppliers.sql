CREATE TABLE suppliers (
    id INT NOT NULL PRIMARY KEY IDENTITY,
    suppliersname NVARCHAR (100) NOT NULL,
    phone NVARCHAR (150) NOT NULL ,
    [address] NVARCHAR(200)NOT NULL,
    category NVARCHAR(100) NOT NULL,
    
);

INSERT INTO suppliers (suppliersname,phone, [address],category)
VALUES
('Makkah-Madina sewing', '+01323456789','Majar road,Dhaka','sewing'),
('Tahmid Sewing',  '+01811122233','Majar road,Dhaka','Cutting'),
('Mim Embroidary and Accessories',  '+01611133355','Dakhshinkhan, Dhaka','sewing'),
('Marjahan Accessories',  '+0166787855','Dakhshinkhan, Dhaka','finishing'),
('Mayer Doya Fabrics',  '+01415559995','Mirpur, Dhaka','sewing');
go