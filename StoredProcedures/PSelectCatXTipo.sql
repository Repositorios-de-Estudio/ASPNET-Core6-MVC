CREATE PROCEDURE PselectCatXTipo
AS
BEGIN
	SELECT Tipo FROM [IngresoGastosDB].[dbo].[CategoriaTipo]
END
GO

-- USE [IngresoGastosDB]
-- EXEC PselectCatXTipo;
