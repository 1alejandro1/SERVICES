CREATE PROCEDURE [dbo].[SetCategoria] @Nombre VARCHAR (100),
@Imagen VARCHAR (max),
@Color VARCHAR (100) AS BEGIN
INSERT INTO [dbo].Categorias (
        Nombre,
        Imagen,
        Color
    )
VALUES (
        @Nombre,
        @Imagen,
        @Color
    );
END