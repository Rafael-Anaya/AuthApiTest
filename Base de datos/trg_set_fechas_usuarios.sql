--Creación de trigger para actualizar campos de control 

CREATE TRIGGER trg_set_fechas_usuarios
ON Usuarios
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Si es un INSERT, poner FechaCreacion
    UPDATE u
    SET FechaCreacion = ISNULL(u.FechaCreacion, GETDATE()), 
        FechaModificacion = GETDATE()
    FROM Usuarios u
    INNER JOIN inserted i ON u.Id = i.Id;
END;
