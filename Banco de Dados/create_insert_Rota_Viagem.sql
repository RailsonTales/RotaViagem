
CREATE DATABASE RotaViagem;
GO

USE RotaViagem;
GO

CREATE TABLE Rotas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Origem NVARCHAR(100) NOT NULL,
    Destino NVARCHAR(100) NOT NULL,
    Valor DECIMAL(18, 2) NOT NULL
);

INSERT INTO Rotas (Origem, Destino, Valor) VALUES
('GRU', 'BRC', 10.00),
('BRC', 'SCL', 5.00),
('GRU', 'CDG', 75.00),
('GRU', 'SCL', 20.00),
('GRU', 'ORL', 56.00),
('ORL', 'CDG', 5.00),
('SCL', 'ORL', 20.00);
