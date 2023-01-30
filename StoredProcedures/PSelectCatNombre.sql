CREATE PROCEDURE PSelectCatNombre
AS
BEGIN
	SELECT Tipo FROM [IngresoGastosDB].[dbo].[NombreCategoria]
END
GO

-- INSERT INTO NombreCategoria (Nombre) VALUES ('Ingreso');
-- INSERT INTO NombreCategoria (Nombre) VALUES ('Gasto');
-- USE [NombreCategoria]
-- EXEC PSelectCatNombre;
