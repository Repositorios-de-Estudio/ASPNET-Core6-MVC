CREATE PROCEDURE PDeleteCategorias
(
    @id int
)
AS
BEGIN
	DELETE FROM [IngresoGastosDB].[dbo].[Categorias] WHERE @ID = id;  
END

-- USE [[IngresoGastosDB]]
-- EXEC PDeleteCategorias @id = 1;