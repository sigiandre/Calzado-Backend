# Calzado-Backend
Web APIs Profesionales con .NET Core

## Comandos que use mediante el proyecto
NETCORE 3.1

### Instalar en el proyecto Calzado.Data
————————————————————
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools

dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools

### Instalar en el proyecto ejecutable o JCalzado.WebAPI
—————————————————————————————
Install-Package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Desing

### Usando Fluent API
——————————
scaffold-dbcontext “Data Source=#######; Initial Catalog=TiendaDb; Integrated Security=True;” Microsoft.EntityFrameworkCore.SqlServer

dotnet ef dbcontext scaffold “Data Source=#######; Initial Catalog=TiendaDb; Integrated Security=True;” Microsoft.EntityFrameworkCore.SqlServer

### Usando Data Annotations
———————————————
scaffold-dbcontext “Data Source=#######; Initial Catalog=TiendaDb; Integrated Security=True;” Microsoft.EntityFrameworkCore.SqlServer —data—annotations

dotnet ef dbcontext “Data Source=#######; Initial Catalog=TiendaDb; Integrated Security=True;” Microsoft.EntityFrameworkCore.SqlServer —data—annotations

## Script BD
USE master;
GO
CREATE DATABASE TiendaDb
GO
USE TiendaDb
GO
CREATE TABLE Perfil(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Nombre NVARCHAR(50) NULL
);
GO
CREATE TABLE Producto(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Nombre NVARCHAR(256) NULL,
    Precio DECIMAL(18,2) NOT NULL,
    Estatus INT NOT NULL,
    FechaRegistro DATETIME2(7) NOT NULL
);
GO
CREATE TABLE Usuario(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Nombre NVARCHAR(50) NULL,
    Apellidos NVARCHAR(256) NULL,
    Email NVARCHAR(100) NULL,
    Username NVARCHAR(25) NULL,
    Password NVARCHAR(512) NULL,
    Estatus INT NOT NULL,
    PerfilId INT NOT NULL,
    CONSTRAINT fk_usuario_perfil FOREIGN KEY (PerfilId) REFERENCES Perfil(Id)
);
GO
CREATE TABLE Orden(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    CantidadArticulos DECIMAL(18,2) NOT NULL,
    Importe DECIMAL(18,2) NOT NULL,
    FechaRegistro DATETIME2(7) NOT NULL,
    FechaActualizacion DATETIME2(7) NULL,
    UsuarioId INT NOT NULL,
    EstatusOrden INT NOT NULL
    CONSTRAINT fk_orden_usuario FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id)
);
GO
CREATE TABLE DetalleOrden(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    OrdenId INT NOT NULL,
    ProductoId INT NOT NULL,
    Cantidad DECIMAL(18,2) NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    Total DECIMAL(18,2) NOT NULL,
    CONSTRAINT fk_detalleorden_orden FOREIGN KEY (OrdenId) REFERENCES Orden(Id),
    CONSTRAINT fk_detalleorden_producto FOREIGN KEY (ProductoId) REFERENCES Producto(Id)
);
GO
INSERT INTO Perfil(Nombre) VALUES(N'Administrador');
INSERT INTO Perfil(Nombre) VALUES(N'Vendedor');
INSERT INTO Perfil(Nombre) VALUES(N'Cliente');
GO
SELECT * FROM Perfil;
GO
INSERT INTO Producto(Nombre,Precio,Estatus,FechaRegistro) VALUES(N'Chanel Negro', 15.00, 1, '2020-03-11 12:30:00.0000000');
INSERT INTO Producto(Nombre,Precio,Estatus,FechaRegistro) VALUES(N'Chanel Beige', 16.00, 1, '2020-03-12 10:30:00.0000000');
INSERT INTO Producto(Nombre,Precio,Estatus,FechaRegistro) VALUES(N'Chanel Cocoa', 14.00, 1, '2020-03-13 15:30:00.0000000');
INSERT INTO Producto(Nombre,Precio,Estatus,FechaRegistro) VALUES(N'Charol Blanco Nero', 20.00, 1, '2020-03-14 16:30:00.0000000');
INSERT INTO Producto(Nombre,Precio,Estatus,FechaRegistro) VALUES(N'Charol Negro Nero', 25.00, 1, '2020-03-14 16:30:00.0000000');
INSERT INTO Producto(Nombre,Precio,Estatus,FechaRegistro) VALUES(N'Camurga Negro', 22.00, 1, '2020-03-16 09:30:00.0000000');
INSERT INTO Producto(Nombre,Precio,Estatus,FechaRegistro) VALUES(N'Camurga Caramelo', 20.00, 1, '2020-03-16 10:30:00.0000000');
INSERT INTO Producto(Nombre,Precio,Estatus,FechaRegistro) VALUES(N'Camurga Nude', 21.00, 1, '2020-03-17 14:30:00.0000000');
INSERT INTO Producto(Nombre,Precio,Estatus,FechaRegistro) VALUES(N'Camurga Vino', 22.00, 1, '2020-03-17 14:30:00.0000000');
INSERT INTO Producto(Nombre,Precio,Estatus,FechaRegistro) VALUES(N'Chemiroke Negro', 20.00, 1, '2020-03-18 18:30:00.0000000');
INSERT INTO Producto(Nombre,Precio,Estatus,FechaRegistro) VALUES(N'Chemiroke Blanco', 19.00, 1, '2020-03-18 18:30:00.0000000');
INSERT INTO Producto(Nombre,Precio,Estatus,FechaRegistro) VALUES(N'Lona Engomada Cambradora Neroflex', 20.00, 1, '2020-03-19 12:30:00.0000000');
GO
SELECT * FROM Producto;
GO
INSERT INTO Usuario(Nombre, Apellidos,Email,Username, [Password], Estatus, PerfilId) VALUES(N'Andre',N'Diaz Quiroz',N'andrew_programador@hotmail.com',N'admin',N'123456',1,1);
INSERT INTO Usuario(Nombre, Apellidos,Email,Username, [Password], Estatus, PerfilId) VALUES(N'Alberto',N'Morales',N'alberto@gmail.com',N'vendedor',N'123456',1,2);
INSERT INTO Usuario(Nombre, Apellidos,Email,Username, [Password], Estatus, PerfilId) VALUES(N'Vilma',N'Tito',N'vilma@gmail.com',N'cliente',N'123456',1,3);
GO
SELECT * FROM Usuario;
GO
INSERT INTO Orden(CantidadArticulos, Importe, FechaRegistro,FechaActualizacion,UsuarioId, EstatusOrden) VALUES(150,2250.00,'2020-03-15 10:30:00.0000000','2020-03-15 13:30:00.0000000',1,1);
INSERT INTO Orden(CantidadArticulos, Importe, FechaRegistro,FechaActualizacion,UsuarioId, EstatusOrden) VALUES(250,5000.00,'2020-03-19 14:30:00.0000000','2020-03-19 16:30:00.0000000',2,1);
INSERT INTO Orden(CantidadArticulos, Importe, FechaRegistro,FechaActualizacion,UsuarioId, EstatusOrden) VALUES(350,7700.00,'2020-03-17 09:30:00.0000000','2020-03-17 10:30:00.0000000',3,1);
GO
SELECT * FROM Orden;
GO
INSERT INTO DetalleOrden(OrdenId, ProductoId, Cantidad, PrecioUnitario, Total) VALUES(1,1,10,15,150);
INSERT INTO DetalleOrden(OrdenId, ProductoId, Cantidad, PrecioUnitario, Total) VALUES(2,10,8,20,160);
INSERT INTO DetalleOrden(OrdenId, ProductoId, Cantidad, PrecioUnitario, Total) VALUES(3,6,15,22,330);
GO
SELECT * FROM DetalleOrden;

--EJEMPLOS DETALLEORDEN CON PRODUCTOS
SELECT d.Id, p.Nombre, d.Cantidad, d.PrecioUnitario, d.Total 
FROM DetalleOrden d inner join Producto p on d.ProductoId = p.Id;
