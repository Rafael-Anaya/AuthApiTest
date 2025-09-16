CREATE DATABASE AuthApi 

Use AuthApi


--Creación de tabla de Usuarios

CREATE TABLE Usuarios (
    Id                INT IDENTITY PRIMARY KEY, 
    Nombres           VARCHAR(100) NOT NULL,
    Apellidos         VARCHAR(100) NOT NULL,
    FechaNacimiento   DATE NOT NULL,
    Direccion         VARCHAR(255) NOT NULL,
    Password          VARCHAR(120) NOT NULL,
    Telefono          VARCHAR(20) NOT NULL,
    Email             VARCHAR(150) NOT NULL UNIQUE,
    Estado            VARCHAR(1) NOT NULL DEFAULT 'A',
    FechaCreacion     DATE NOT NULL DEFAULT GETDATE(),
    FechaModificacion DATE NULL,
    
    CONSTRAINT chk_estado CHECK (Estado IN ('A', 'I'))
);


-- Inserción del primer usuario por defecto
INSERT INTO Usuarios (
    Nombres,
    Apellidos,
    FechaNacimiento,
    Direccion,
    Password,
    Telefono,
    Email,
    FechaCreacion
)
VALUES (
    'Administrador',             -- Nombres
    'Sistema',                   -- Apellidos
    '1990-01-01',                -- FechaNacimiento
    'Dirección del Admin',       -- Direccion
    '5997441171671814138679518223612922947249451091219013990156237151069920112283178',-- Password (Admin123) 
    '71727374',                 -- Telefono
    'admin@authapi.com',         -- Email
    GETDATE()
);


