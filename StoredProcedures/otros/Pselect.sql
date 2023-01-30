CREATE PROCEDURE Pselect
(
    @ntop int
)
AS
BEGIN
    SELECT TOP(@nTop) Mail, Name FROM DBtest.dbo.TDestinatarios
END

-- USE [Empresa]
-- EXEC Pselect @ntop = 1000;

