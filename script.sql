CREATE DATABASE Bank;

USE Bank;

CREATE TABLE Admin (
	id INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	email VARCHAR(255) NOT NULL,
	password VARCHAR (255) NOT NULL
)

CREATE TABLE Cliente (
	id INT NOT NULL PRIMARY KEY,
	nombre VARCHAR(255) NOT NULL,
	apellido VARCHAR(255) NOT NULL,
	tipoCliente NVARCHAR(10) CONSTRAINT chktipoCliente CHECK (tipoCliente IN ('Persona', 'Cuenta')),
)

CREATE TABLE Cuenta (
	id INT NOT NULL PRIMARY KEY,
	saldo FLOAT NOT NULL CONSTRAINT chk_saldo CHECK (saldo >= 0),
	tipoCuenta NVARCHAR(10) CONSTRAINT chk_tipoCuenta CHECK (tipoCuenta IN ('Ahorro', 'Corriente')),
	ciudad VARCHAR(255) NOT NULL,
	clienteID INT FOREIGN KEY REFERENCES Cliente(id)
	ON DELETE CASCADE
    ON UPDATE CASCADE
)

CREATE TABLE Transaccion (
	id INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	tipoTransaccion NVARCHAR(15) CONSTRAINT chk_tipoTransaccion CHECK (tipoTransaccion IN ('Retiro', 'Consignacion')),
	ciudadOrigen VARCHAR(255) NOT NULL,
	monto FLOAT NOT NULL,
	fecha DATE NOT NULL,
	cuentaID INT FOREIGN KEY REFERENCES Cuenta(id),
	clienteID INT FOREIGN KEY REFERENCES Cliente(id)
	ON DELETE CASCADE
    ON UPDATE CASCADE
)

INSERT INTO Admin VALUES ('admin@bluesoft.com', 'admin');

INSERT INTO Cliente VALUES (100000, 'Pablo', 'Merchan', 'Persona');
INSERT INTO Cliente VALUES (200000, 'Juan', 'Rodriguez', 'Persona');

INSERT INTO Cuenta VALUES (111111, 400000.0, 'Ahorro', 'Bogota', 200000);

INSERT INTO Cuenta VALUES (123456, 30000, 'Ahorro', 'Ibague', 100000);
INSERT INTO Cuenta VALUES (102030, 200000.0, 'Ahorro', 'Bogota', 100000);
INSERT INTO Cuenta VALUES (405060, 5000000.0, 'Ahorro', 'Bogota', 100000);

INSERT INTO Transaccion VALUES ('Consignacion', 'Ibague', 3500000, '01-03-2024', 102030, 100000);
INSERT INTO Transaccion VALUES ('Retiro', 'Bogota', 22000, '01-22-2024', 102030, 100000);
INSERT INTO Transaccion VALUES ('Retiro', 'Bogota', 103000, '03-16-2023', 102030, 100000);
INSERT INTO Transaccion VALUES ('Consignacion', 'Bogota', 51000, '04-16-2023', 102030, 100000);
INSERT INTO Transaccion VALUES ('Consignacion', 'Bogota', 51000, '10-07-2023', 102030, 100000);
INSERT INTO Transaccion VALUES ('Consignacion', 'Bogota', 51000, '07-26-2023', 102030, 100000);
INSERT INTO Transaccion VALUES ('Retiro', 'Barranquilla', 1028000, '01-22-2024', 102030, 100000);
INSERT INTO Transaccion VALUES ('Retiro', 'Barranquilla', 1100000, '01-24-2024', 102030, 100000);
INSERT INTO Transaccion VALUES ('Consignacion', 'Bogota', 2000000, '01-28-2024', 102030, 100000);
INSERT INTO Transaccion VALUES ('Retiro', 'Ibague', 1100000, '01-12-2024', 102030, 100000);
INSERT INTO Transaccion VALUES ('Retiro', 'Bogota', 1350000, '01-19-2024', 102030, 100000);
INSERT INTO Transaccion VALUES ('Retiro', 'Medellin', 2000000, '01-16-2024', 405060, 100000);

INSERT INTO Transaccion VALUES ('Retiro', 'Cartagena', 1550000, '01-21-2024', 111111, 200000);
INSERT INTO Transaccion VALUES ('Consignacion', 'Ibague', 10000, '01-03-2024', 111111, 200000);
INSERT INTO Transaccion VALUES ('Retiro', 'Bogota', 20000, '01-22-2024', 111111, 200000);
INSERT INTO Transaccion VALUES ('Retiro', 'Santa Marta', 200000, '01-22-2024', 111111, 200000);

SELECT * FROM Cliente;
SELECT * FROM Transaccion;
SELECT * FROM Cuenta;
SELECT * FROM Admin;

