/****** Object:  Database [BcpCrecer]    Script Date: 23/06/2022 15:57:49 p. m. ******/
CREATE DATABASE [BcpCrecer]  (EDITION = 'GeneralPurpose', SERVICE_OBJECTIVE = 'GP_S_Gen5_4', MAXSIZE = 10 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS;
GO
ALTER DATABASE [BcpCrecer] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BcpCrecer] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BcpCrecer] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BcpCrecer] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BcpCrecer] SET ARITHABORT OFF 
GO
ALTER DATABASE [BcpCrecer] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BcpCrecer] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BcpCrecer] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BcpCrecer] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BcpCrecer] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BcpCrecer] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BcpCrecer] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BcpCrecer] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BcpCrecer] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [BcpCrecer] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BcpCrecer] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [BcpCrecer] SET  MULTI_USER 
GO
ALTER DATABASE [BcpCrecer] SET ENCRYPTION ON
GO
ALTER DATABASE [BcpCrecer] SET QUERY_STORE = ON
GO
ALTER DATABASE [BcpCrecer] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  User [creceruser]    Script Date: 23/06/2022 15:57:50 p. m. ******/
CREATE USER [creceruser] WITH DEFAULT_SCHEMA=[dbo]
GO
sys.sp_addrolemember @rolename = N'db_owner', @membername = N'creceruser'
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 23/06/2022 15:57:50 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Imagen] [varchar](max) NOT NULL,
	[Color] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empresas]    Script Date: 23/06/2022 15:57:50 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empresas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Imagen] [varchar](max) NOT NULL,
	[Color] [varchar](100) NOT NULL,
	[CategoriaId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 23/06/2022 15:57:50 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Imagen] [varchar](max) NOT NULL,
	[Color] [varchar](100) NOT NULL,
	[Precio] [decimal](18, 0) NOT NULL,
	[Descuento] [decimal](18, 0) NOT NULL,
	[Descripcion] [text] NULL,
	[EmpresaId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 23/06/2022 15:57:50 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Telefono] [int] NOT NULL,
	[Contrasena] [varchar](100) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Paterno] [varchar](100) NOT NULL,
	[Materno] [varchar](100) NOT NULL,
	[Imagen] [varchar](max) NOT NULL,
	[FecNac] [varchar](100) NOT NULL,
	[Idc] [varchar](100) NOT NULL,
	[TipoIdc] [varchar](100) NOT NULL,
	[ExtIdc] [varchar](100) NOT NULL,
	[ComplementoIdc] [varchar](100) NOT NULL,
	[Role] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categorias] ON 

INSERT [dbo].[Categorias] ([Id], [Nombre], [Imagen], [Color]) VALUES (2, N'Restaurantes', N'https://cdn.vuetifyjs.com/images/cards/cooking.png', N'sin color')
INSERT [dbo].[Categorias] ([Id], [Nombre], [Imagen], [Color]) VALUES (14, N'Celulares', N'https://cdn.forbes.co/2020/11/Xiaomi-1280x720-JPG.jpg', N'sin color')
INSERT [dbo].[Categorias] ([Id], [Nombre], [Imagen], [Color]) VALUES (15, N'Farmacias', N'https://elblogdesecuritas.es/wp-content/uploads/2022/02/alarmas-para-farmacias.jpg', N'Sin Color')
INSERT [dbo].[Categorias] ([Id], [Nombre], [Imagen], [Color]) VALUES (19, N'Moda', N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTqgBw1dio54igX4Rxyld1Z7C1t1lxkkRpckw&usqp=CAU', N'sin color')
SET IDENTITY_INSERT [dbo].[Categorias] OFF
SET IDENTITY_INSERT [dbo].[Empresas] ON 

INSERT [dbo].[Empresas] ([Id], [Nombre], [Imagen], [Color], [CategoriaId]) VALUES (1, N'Pollos Copacabana', N'https://debolivia.site/wp-content/uploads/classified-listing/2021/06/Pollos-Copacabana-23-493x493.jpg', N'sin color', 2)
INSERT [dbo].[Empresas] ([Id], [Nombre], [Imagen], [Color], [CategoriaId]) VALUES (2, N'Apple', N'https://photos5.appleinsider.com/gallery/47670-93100-000-lead-Apple-Logo-xl.jpg', N'sin color', 14)
INSERT [dbo].[Empresas] ([Id], [Nombre], [Imagen], [Color], [CategoriaId]) VALUES (3, N'Samsung', N'https://upload.wikimedia.org/wikipedia/commons/f/f1/Samsung_logo_blue.png', N'sin color', 14)
INSERT [dbo].[Empresas] ([Id], [Nombre], [Imagen], [Color], [CategoriaId]) VALUES (4, N'Sony', N'https://fotografias-neox.atresmedia.com/clipping/cmsimages02/2018/03/07/A4AFBC2E-4734-4C55-A4E0-E1372C101D33/98.jpg?crop=1200,675,x0,y263&width=1900&height=1069&optimize=high&format=webply', N'sin color', 14)
INSERT [dbo].[Empresas] ([Id], [Nombre], [Imagen], [Color], [CategoriaId]) VALUES (6, N'Pluma de pato', N'https://tottobo.vteximg.com.br/arquivos/ids/218110-292-292/Chaqueta-Para-Hombre-Colormen-Totto-Ra41367-2120-N01_1.jpg?v=637725312007330000', N'sin color', 19)
INSERT [dbo].[Empresas] ([Id], [Nombre], [Imagen], [Color], [CategoriaId]) VALUES (7, N'Pluma de pato', N'https://tottobo.vteximg.com.br/arquivos/ids/218110-292-292/Chaqueta-Para-Hombre-Colormen-Totto-Ra41367-2120-N01_1.jpg?v=637725312007330000', N'sin color', 19)
SET IDENTITY_INSERT [dbo].[Empresas] OFF
SET IDENTITY_INSERT [dbo].[Productos] ON 

INSERT [dbo].[Productos] ([Id], [Nombre], [Imagen], [Color], [Precio], [Descuento], [Descripcion], [EmpresaId]) VALUES (2, N'Combo Fiesta', N'https://urgente.bo/sites/default/files/styles/thumb_600x465/public/Copacabana2_0.jpg', N'sin color', CAST(27 AS Decimal(18, 0)), CAST(20 AS Decimal(18, 0)), N'2 presas de pollo con papas y refresco', 1)
INSERT [dbo].[Productos] ([Id], [Nombre], [Imagen], [Color], [Precio], [Descuento], [Descripcion], [EmpresaId]) VALUES (3, N'Combo Fiesta', N'https://urgente.bo/sites/default/files/styles/thumb_600x465/public/Copacabana2_0.jpg', N'sin color', CAST(27 AS Decimal(18, 0)), CAST(20 AS Decimal(18, 0)), N'2 presas de pollo con papas y refresco', 1)
INSERT [dbo].[Productos] ([Id], [Nombre], [Imagen], [Color], [Precio], [Descuento], [Descripcion], [EmpresaId]) VALUES (4, N'Balde de 8 Alitas', N'https://debolivia.site/wp-content/uploads/2021/06/copacabana-portada-4-1.jpg', N'sin color', CAST(35 AS Decimal(18, 0)), CAST(20 AS Decimal(18, 0)), N'Promocion de Balde de 8 alitas ', 1)
INSERT [dbo].[Productos] ([Id], [Nombre], [Imagen], [Color], [Precio], [Descuento], [Descripcion], [EmpresaId]) VALUES (5, N'Sony Xperia', N'https://i.blogs.es/d08541/sony-xperia-1-ii-01-trasera-02/1366_2000.jpg', N'sin color', CAST(2560 AS Decimal(18, 0)), CAST(10 AS Decimal(18, 0)), N'Celular muy bueno .....', 4)
INSERT [dbo].[Productos] ([Id], [Nombre], [Imagen], [Color], [Precio], [Descuento], [Descripcion], [EmpresaId]) VALUES (8, N'Pollo a la coca cola', N'https://cdn.pedidosdeuna.com/product/579237/579237_main.jpg', N'sin color', CAST(20 AS Decimal(18, 0)), CAST(5 AS Decimal(18, 0)), N'Pollo con sabor a coca cola', 1)
SET IDENTITY_INSERT [dbo].[Productos] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Email], [Telefono], [Contrasena], [Nombre], [Paterno], [Materno], [Imagen], [FecNac], [Idc], [TipoIdc], [ExtIdc], [ComplementoIdc], [Role]) VALUES (1, N'dtoro@bcp.com.bo', 69714579, N'bcp1234', N'Douglas', N'Toro', N'Salas', N'https://www.terrifictresses.com/wp-content/uploads/hot-rollers-featured_opt.jpg', N'10-01-1995', N'9986799', N'Q', N'LP', N'00', N'Administrador')
INSERT [dbo].[Users] ([Id], [Email], [Telefono], [Contrasena], [Nombre], [Paterno], [Materno], [Imagen], [FecNac], [Idc], [TipoIdc], [ExtIdc], [ComplementoIdc], [Role]) VALUES (2, N'mchavezch@bcp.com.bo', 78783505, N'123abc', N'Mikael', N'Chavez', N'Choque', N'https://concepto.de/wp-content/uploads/2018/08/persona-e1533759204552.jpg', N'1991-11-25', N'4368184', N'Q', N'LP', N'00', N'Administrador')
INSERT [dbo].[Users] ([Id], [Email], [Telefono], [Contrasena], [Nombre], [Paterno], [Materno], [Imagen], [FecNac], [Idc], [TipoIdc], [ExtIdc], [ComplementoIdc], [Role]) VALUES (3, N'marcelomendezlaruta09@gmail.com', 76791300, N'bcp123', N'Marcelo', N'Mendez', N'Laruta', N'utlfoto', N'19/04/10987', N'6767294', N'Q', N'LP', N'00', N'Cliente')
INSERT [dbo].[Users] ([Id], [Email], [Telefono], [Contrasena], [Nombre], [Paterno], [Materno], [Imagen], [FecNac], [Idc], [TipoIdc], [ExtIdc], [ComplementoIdc], [Role]) VALUES (4, N'mmendez@bcp.com.bo', 70644877, N'bcp123', N'Marcelo', N'Mendez', N'Laruta', N'https://zipmex.com/static/d1af016df3c4adadee8d863e54e82331/Twitter-NFT-profile.jpg', N'19/04/87', N'6767297', N'Q', N'LP', N'00', N'Administrador')
INSERT [dbo].[Users] ([Id], [Email], [Telefono], [Contrasena], [Nombre], [Paterno], [Materno], [Imagen], [FecNac], [Idc], [TipoIdc], [ExtIdc], [ComplementoIdc], [Role]) VALUES (8, N'qwerty@gmail.com', 78787878, N'123abc', N'qwerty', N'Perez', N'NA', N'data:image/jpeg;base64,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', N'1994-05-11', N'01234567', N'Q', N'LP', N'00', N'Administrador')
SET IDENTITY_INSERT [dbo].[Users] OFF
/****** Object:  StoredProcedure [dbo].[DeleteCategoria]    Script Date: 23/06/2022 15:57:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteCategoria] 
@Id int
as
begin

DELETE FROM [dbo].[Categorias]
      WHERE Id = @Id

end
GO
/****** Object:  StoredProcedure [dbo].[DeleteEmpresa]    Script Date: 23/06/2022 15:57:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[DeleteEmpresa] 
@Id int
as
begin
DELETE FROM [dbo].[Empresas]
      WHERE Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[DeleteProducto]    Script Date: 23/06/2022 15:57:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteProducto] 
@Id int
as
begin
DELETE FROM [dbo].[Productos]
      WHERE Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[GetCategorias]    Script Date: 23/06/2022 15:57:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCategorias]
AS
BEGIN

  SELECT Id,Nombre,Imagen,Color FROM dbo.Categorias WHERE Id <> 12

END
GO
/****** Object:  StoredProcedure [dbo].[GetEmpresas]    Script Date: 23/06/2022 15:57:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetEmpresas]
  @CategoriaId INT
AS
BEGIN

  SELECT Id,Nombre,Imagen,Color,CategoriaId FROM dbo.Empresas WHERE CategoriaId=@CategoriaId

END
GO
/****** Object:  StoredProcedure [dbo].[GetProductosByEmpresaId]    Script Date: 23/06/2022 15:57:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProductosByEmpresaId]
  @EmpresaId INT
AS
BEGIN

  SELECT Id,Nombre,Imagen,Color,Precio,Descuento,Descripcion,EmpresaId FROM dbo.Productos WHERE EmpresaId=@EmpresaId

END
GO
/****** Object:  StoredProcedure [dbo].[Login]    Script Date: 23/06/2022 15:57:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Login]
  @Usuario varchar(30),
  @Contrasena varchar(30)
AS
BEGIN

  SELECT [Id]
      ,[Email]
      ,[Telefono]
      ,[Contrasena]
      ,[Nombre]
      ,[Paterno]
      ,[Materno]
      ,[Imagen]
      ,[FecNac]
      ,[Idc]
      ,[TipoIdc]
      ,[ExtIdc]
      ,[ComplementoIdc]
	  ,[Role]
	  FROM [dbo].Users WHERE Telefono=@Usuario AND Contrasena=@Contrasena

END
GO
/****** Object:  StoredProcedure [dbo].[SetCategoria]    Script Date: 23/06/2022 15:57:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SetCategoria] @Nombre VARCHAR (100),
@Imagen VARCHAR (max),
@Color VARCHAR (100)AS BEGIN
DECLARE @CANT_INI INT
DECLARE @CANT_FIN INT
 SELECT @CANT_INI  = (SELECT COUNT(*) FROM [dbo].Categorias)
 INSERT INTO [dbo].Categorias(
        Nombre,
        Imagen,
        Color
    )
VALUES (
        @Nombre,
        @Imagen,
        @Color
    );
		 SELECT @CANT_FIN  = (SELECT COUNT(*) FROM [dbo].Categorias)
IF(@CANT_INI < @CANT_FIN)
BEGIN
SELECT TOP 1 * FROM [dbo].Categorias WHERE Nombre = @Nombre and Color = @Color
END
ELSE
SELECT '' as Id

END
GO
/****** Object:  StoredProcedure [dbo].[SetEmpresa]    Script Date: 23/06/2022 15:57:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SetEmpresa] @Nombre VARCHAR (100),
@Imagen VARCHAR (max),
@Color VARCHAR (100),
@CategoriaId INT AS BEGIN
DECLARE @CANT_INI INT
DECLARE @CANT_FIN INT
 SELECT @CANT_INI  = (SELECT COUNT(*) FROM [dbo].Empresas)
 INSERT INTO [dbo].Empresas ( Nombre, Imagen, Color, CategoriaId )
	VALUES
		( @Nombre,
			@Imagen,
			@Color,
		@CategoriaId );
		 SELECT @CANT_FIN  = (SELECT COUNT(*) FROM [dbo].Empresas)
IF(@CANT_INI < @CANT_FIN)
BEGIN
SELECT TOP 1 * FROM [dbo].Empresas WHERE Nombre = @Nombre and Color = @Color and CategoriaId = @CategoriaId
END
ELSE
SELECT '' as Id
	

END
GO
/****** Object:  StoredProcedure [dbo].[SetProductos]    Script Date: 23/06/2022 15:57:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SetProductos] @Nombre VARCHAR (100),
@Imagen VARCHAR (max),
@Color VARCHAR (100),
@Precio decimal(18),
@Descuento decimal(18),
@Descripcion text,
@EmpresaId INT AS BEGIN
DECLARE @CANT_INI INT
DECLARE @CANT_FIN INT
 SELECT @CANT_INI  = (SELECT COUNT(*) FROM [dbo].Productos)

INSERT INTO [dbo].Productos (Nombre, Imagen, Color,Precio, Descuento,Descripcion,EmpresaId)
VALUES (@Nombre, @Imagen, @Color,@Precio, @Descuento,@Descripcion,@EmpresaId);
		 SELECT @CANT_FIN  = (SELECT COUNT(*) FROM [dbo].Productos)
IF(@CANT_INI < @CANT_FIN)
BEGIN
SELECT TOP 1 * FROM [dbo].Productos WHERE Nombre = @Nombre and Color = @Color and EmpresaId = @EmpresaId
END
ELSE
SELECT '' as Id
	
END
GO
/****** Object:  StoredProcedure [dbo].[SignIn]    Script Date: 23/06/2022 15:57:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SignIn] 
@Telefono INT,
@Email VARCHAR ( 100 ),
@Contrasena VARCHAR ( 100 ),
@Nombre VARCHAR ( 100 ),
@Paterno VARCHAR ( 100 ),
@Materno VARCHAR ( 100 ),
@Imagen VARCHAR ( MAX ),
@Fecnac VARCHAR ( 100 ),
@Idc VARCHAR ( 100 ),
@Tipoidc VARCHAR ( 100 ),
@Extidc VARCHAR ( 100 ),
@Complementoidc VARCHAR ( 100 ),
@Role VARCHAR ( 50 )
AS BEGIN
DECLARE @CANT_INI INT
DECLARE @CANT_FIN INT
 SELECT @CANT_INI  = (SELECT COUNT(*) FROM [dbo].[Users])
 
	INSERT INTO [dbo].Users (
			[Email]
           ,[Telefono]
           ,[Contrasena]
           ,[Nombre]
           ,[Paterno]
           ,[Materno]
           ,[Imagen]
           ,[FecNac]
           ,[Idc]
           ,[TipoIdc]
           ,[ExtIdc]
           ,[ComplementoIdc]
		   ,[Role]
	)
	VALUES
		(
			@Email,
			@Telefono,
			@Contrasena,
			@Nombre,
			@Paterno,
			@Materno,
			@Imagen,
			@Fecnac,
			@Idc,
			@Tipoidc,
			@Extidc,
			@Complementoidc,
			@Role
		);
		 SELECT @CANT_FIN  = (SELECT COUNT(*) FROM [dbo].[Users])
IF(@CANT_INI < @CANT_FIN)
BEGIN
SELECT TOP 1 * FROM [dbo].[Users] WHERE Telefono = @Telefono and Contrasena = @Contrasena
END
ELSE
SELECT '' as Id

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCategoria]    Script Date: 23/06/2022 15:57:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCategoria] 
@Id int,
@Nombre varchar(100),
@Imagen varchar(max),
@Color varchar(100)
as
begin

 UPDATE [dbo].[Categorias]
   SET [Nombre] = @Nombre
      ,[Imagen] = @Imagen
      ,[Color] = @Color
 WHERE Id = @Id
 
SELECT @Id as Id, @Nombre as Nombre, @Imagen as Imagen, @Color as Color
end
GO
/****** Object:  StoredProcedure [dbo].[UpdateEmpresa]    Script Date: 23/06/2022 15:57:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateEmpresa] 
@Id int,
@Nombre varchar(100),
@Imagen varchar(max),
@Color varchar(100),
@CategoriaId int
as
begin

UPDATE [dbo].[Empresas]
   SET [Nombre] = @Nombre
      ,[Imagen] = @Imagen
      ,[Color] = @Color
	  ,[CategoriaId] = @CategoriaId
 WHERE Id = @Id
 SELECT @Id as Id, @Nombre as Nombre, @Imagen as Imagen, @Color as Color,  @CategoriaId as CategoriaId
end
GO
/****** Object:  StoredProcedure [dbo].[UpdateProducto]    Script Date: 23/06/2022 15:57:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateProducto] 
@Id int,
@Nombre varchar(100),
@Imagen varchar(max),
@Color varchar(100),
@Precio decimal(18,0),
@Descuento decimal(18,0),
@Descripcion text,
@EmpresaId int
as
begin

UPDATE [dbo].[Productos]
   SET [Nombre] = @Nombre
      ,[Imagen] = @Imagen
      ,[Color] = @Color
	  ,[Precio] = @Precio
	  ,[Descuento] = @Descuento
	  ,[Descripcion] = @Descripcion
	  ,[EmpresaId] = @EmpresaId
 WHERE Id = @Id
 SELECT @Id as Id, @Nombre as Nombre, @Imagen as Imagen, @Color as Color, @Precio as Precio, @Descuento as Descuento, @Descripcion as Descripcion, @EmpresaId as EmpresaId
end
GO
ALTER DATABASE [BcpCrecer] SET  READ_WRITE 
GO
