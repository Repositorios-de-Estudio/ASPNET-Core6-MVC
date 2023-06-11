CREATE PROCEDURE PInsertCategorias
(
    @nombre nvarchar(120),
    @tipo nvarchar(2),
	@estado bit
)
AS
BEGIN
	INSERT INTO [IngresoGastosDB].[dbo].[Categorias] ([NombreCategoria], Tipo, Estado) 
	values (@nombre,@tipo,@estado);			   

END

-- USE [IngresoGastosDB]
-- EXEC PInsertCategorias @nombre = 'Recibo 1', @tipo = 'apellido1', @estado = 1;