CREATE DATABASE Usuarios 

Use Usuarios


--Creación de tabla de Usuarios

CREATE TABLE Usuarios (
    Id              INT IDENTITY PRIMARY KEY, 
    Nombres         VARCHAR(100) NOT NULL,
    Apellidos       VARCHAR(100) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Direccion       VARCHAR(255) NOT NULL,
    Password        VARCHAR(120) NOT NULL,
    Telefono        VARCHAR(20) NOT NULL,
    Email           VARCHAR(150) NOT NULL UNIQUE,
    Estado          VARCHAR(1) NOT NULL,
    FechaCreacion   DATE NOT NULL,
    FechaModificacion DATE NULL,
    
    CONSTRAINT chk_estado CHECK (Estado IN ('A', 'I'))
);

select * from Usuarios

DELETE FROM Usuarios
