CREATE PROCEDURE [dbo].[SignIn] @Telefono INT,
@Email VARCHAR (100),
@Contrasena VARCHAR (100),
@Nombre VARCHAR (100),
@Paterno VARCHAR (100),
@Materno VARCHAR (100),
@Imagen VARCHAR (MAX),
@Fecnac VARCHAR (100),
@Idc VARCHAR (100),
@Tipoidc VARCHAR (100),
@Extidc VARCHAR (100),
@Complementoidc VARCHAR (100) AS BEGIN
INSERT INTO [dbo].Users (
        Email,
        Contrasena,
        Nombre,
        Paterno,
        Materno,
        Imagen,
        Fecnac,
        Idc,
        Tipoidc,
        Extidc,
        Complementoidc
    )
VALUES (
        @Email,
        @Contrasena,
        @Nombre,
        @Paterno,
        @Materno,
        @Imagen,
        @Fecnac,
        @Idc,
        @Tipoidc,
        @Extidc,
        @Complementoidc
    );
END