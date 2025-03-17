CREATE PROCEDURE [dbo].[SetProductos] @Nombre VARCHAR (100),
@Imagen VARCHAR (max),
@Color VARCHAR (100),
@Precio decimal(18),
@Descuento decimal(18),
@Descripcion text,
@EmpresaId INT AS BEGIN
INSERT INTO [dbo].Productos (
        Nombre,
        Imagen,
        Color,
        Precio,
        Descuento,
        Descripcion,
        EmpresaId
    )
VALUES (
        @Nombre,
        @Imagen,
        @Color,
        @Precio,
        @Descuento,
        @Descripcion,
        @EmpresaId
    );
END