CREATE PROCEDURE [dbo].[SetEmpresa] @Nombre VARCHAR (100),
@Imagen VARCHAR (max),
@Color VARCHAR (100),
@CategoriaId INT AS BEGIN
INSERT INTO [dbo].Empresas (Nombre, Imagen, Color, CategoriaId)
VALUES (@Nombre, @Imagen, @Color, @CategoriaId);
END