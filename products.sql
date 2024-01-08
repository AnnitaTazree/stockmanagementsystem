CREATE TABLE products (
    productid INT NOT NULL PRIMARY KEY IDENTITY,
    productname NVARCHAR (100) NOT NULL,
    Category NVARCHAR (150) NOT NULL,
    Quantity NVARCHAR(20)  NOT NULL,
    issued_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO products (productname, Category, Quantity)
VALUES
('Needle', 'sewing', '20'),
('Thread', 'sewing', '150'),
('Scissor', 'cutting', '20'),
('Plain Machine', 'sewing', '35'),
('Overlock Machine', 'Sewing', '23' ),
('Embroidery Machine', 'Sewing', '15' ),
('Fatlock Machine', 'Sewing', '10' ),
('SnapButton Machine', 'Sewing', '9' ),
('Cutting Machine', 'Cutting', '15' ),
('Cutter', 'Finishing', '50' ),
('Sticker Machine', 'Cutting', '23' ),
('Fatlock Machine', 'Sewing', '10' ),
('Arrow sticker', 'Quality', '9' ),
('Size sticker', 'Quality', '15' ),
('Numbering sticker', 'Quality', '50' ),
('Carton sticker', 'Quality', '23' ),
('carton', 'Finishing', '2000'),
('scotchtape', 'Finishing', '2000'),
('Gum tap', 'Finishing', '2000'),
('poly', 'Finishing', '2000');
go