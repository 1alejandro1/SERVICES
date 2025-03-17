CREATE TABLE Productos (
  Id              INT           NOT NULL    IDENTITY    PRIMARY KEY,
  Nombre          VARCHAR(100)   NOT NULL,
  Imagen           VARCHAR(max)  NOT NULL,
  Color           VARCHAR(100)  NOT NULL,
  Precio	DECIMAL NOT NULL,
  Descuento DECIMAL NOT NULL,
  Descripcion TEXT,
  EmpresaId int
)
GO