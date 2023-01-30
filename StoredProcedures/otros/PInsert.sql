CREATE PROCEDURE PInsert
(
    @Mail nvarchar(50),
    @Name nvarchar(50)
)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM DBtest.dbo.TDestinatarios 
					WHERE Mail = @mail )
		BEGIN
			INSERT INTO DBtest.dbo.TDestinatarios (Mail, Name)
			VALUES (@Mail, @Name)			   
		END
END


-- EXEC PInsert @Mail = 'correo1@mail.com', @Name = 'Pepito';