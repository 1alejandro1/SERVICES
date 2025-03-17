CREATE PROCEDURE [dbo].[GetCategorias] AS BEGIN
SELECT Id,
    Nombre,
    Imagen,
    Color
FROM dbo.Categorias
END