CREATE DATABASE Bank;

USE Bank;

CREATE TABLE Transaccion (
	id INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	tipoTransaccion NVARCHAR(15) CONSTRAINT chk_tipoTransaccion CHECK (tipoTransaccion IN ('Retiro', 'Consignacion')),
	ciudadOrigen VARCHAR(255) NOT NULL
)

CREATE TABLE Cliente (
	id INT NOT NULL PRIMARY KEY,
	nombre VARCHAR(255) NOT NULL,
	apellido VARCHAR(255) NOT NULL,
	tipoCliente NVARCHAR(10) CONSTRAINT chktipoCliente CHECK (tipoCliente IN ('Persona', 'Cuenta')),
	transaccionID INT FOREIGN KEY REFERENCES Transaccion(id)
	ON DELETE CASCADE
    ON UPDATE CASCADE
)

CREATE TABLE Cuenta (
	id INT NOT NULL PRIMARY KEY,
	saldo FLOAT NOT NULL,
	tipoCuenta NVARCHAR(10) CONSTRAINT chk_tipoCuenta CHECK (tipoCuenta IN ('Ahorro', 'Corriente')),
	ciudad VARCHAR(255) NOT NULL,
	clientID INT FOREIGN KEY REFERENCES Cliente(id),
	transaccionID INT FOREIGN KEY REFERENCES Transaccion(id)
	ON DELETE CASCADE
    ON UPDATE CASCADE
)

INSERT INTO Transaccion VALUES ('Consignacion', 'Bogota');
INSERT INTO Cliente VALUES (100000, 'Pablo', 'Merchan', 'Persona', 1);
INSERT INTO Cuenta VALUES (102030, 20.5, 'Ahorro', 'Bogota', 100000, 1);

SELECT * FROM Cliente;
SELECT * FROM Transaccion;
SELECT * FROM Cuenta;

DROP TABLE Cliente;
DROP TABLE Transaccion;
DROP TABLE Cuenta;