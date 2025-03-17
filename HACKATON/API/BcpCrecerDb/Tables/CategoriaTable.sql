CREATE TABLE Categorias (
  Id              INT           NOT NULL    IDENTITY    PRIMARY KEY,
  Nombre          VARCHAR(100)   NOT NULL,
  Imagen           VARCHAR(max)  NOT NULL,
  Color           VARCHAR(100)  NOT NULL
)
GO