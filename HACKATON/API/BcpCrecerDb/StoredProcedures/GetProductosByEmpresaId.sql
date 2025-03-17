CREATE PROCEDURE [dbo].[GetProductosByEmpresaId] @EmpresaId INT AS BEGIN
SELECT Id,
    Nombre,
    Imagen,
    Color,
    Precio,
    Descuento,
    Descripcion,
    EmpresaId
FROM dbo.Productos
WHERE EmpresaId = @EmpresaId
END