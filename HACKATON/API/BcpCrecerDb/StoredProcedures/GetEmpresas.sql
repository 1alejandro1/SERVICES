CREATE PROCEDURE [dbo].[GetEmpresas] @CategoriaId INT AS BEGIN
SELECT Id,
    Nombre,
    Imagen,
    Color,
    CategoriaId
FROM dbo.Empresas
WHERE CategoriaId = @CategoriaId
END