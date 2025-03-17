CREATE PROCEDURE [dbo].[Login] @Usuario varchar(30),
@Contrasena varchar(30) AS BEGIN
SELECT *
FROM [dbo].Users
WHERE Telefono = @Usuario
    AND Contrasena = @Contrasena
END