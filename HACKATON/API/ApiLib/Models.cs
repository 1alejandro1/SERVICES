namespace ApiLib.Models
{
	
	public record LoginRequest(int telefono, string password);
	public record LoginResponse(int id, string email,int telefono, string nombre, string paterno,	string materno,	string imagen,	string fecnac,	string idc,string tipoidc,	string extidc,	string complementoidc, string Role, string token);
	public record RegistroRequest(string email, string password, int telefono,string nombre, string paterno, string materno, string imagen, string fecnac, string idc, string tipoidc, string extidc, string complementoidc, string Role);
	public record RegistroResponse(int? id, string email, int telefono, string nombre, string paterno, string materno, string imagen, string fecnac, string idc, string tipoidc, string extidc, string complementoidc,string Role, string token);
	public record CategoriasResponse(List<Categoria> Categorias);
	public record Categoria(int? Id, string Nombre, string Imagen, string Color);
	public record RegCategoriaResponse(int? Id, string Nombre, string Imagen, string Color);
	public record NewCategoria(string Nombre, string Imagen, string Color);
	public record UpdateCategoria(int? Id, string Nombre, string Imagen, string Color);
	public record Empresa(int? Id, string Nombre, string Imagen, string Color, int CategoriaId);
	public record RegEmpresaResponse(int? Id, string Nombre, string Imagen, string Color, int CategoriaId);
	public record NewEmpresa(string Nombre, string Imagen, string Color, int CategoriaId);
	public record UpdateEmpresa(int? Id, string Nombre, string Imagen, string Color, int CategoriaId);
	public record EmpresasRequest(int? CategoriaId);
	public record EmpresaRequest(int? EmpresaId);
	public record CategoriasRequest(int? CategoriaId);
	public record EmpresasResponse(List<Empresa> Empresas);
	public record Producto(int? Id,string Nombre, string Imagen, string Color, double Precio, double Descuento,string Descripcion,int EmpresaId);
	public record RegProductoResponse(int? Id,string Nombre, string Imagen, string Color, double Precio, double Descuento,string Descripcion,int EmpresaId);
	public record NewProducto(string Nombre, string Imagen, string Color, double Precio, double Descuento,string Descripcion,int EmpresaId);
	public record UpdateProducto(int? Id, string Nombre, string Imagen, string Color, double Precio, double Descuento,string Descripcion,int EmpresaId);
	public record ProductosRequest(int EmpresaId);
	public record ProductoRequest(int ProductoId);
	public record ProductosResponse(List<Producto> Productos);
	public record struct CarritoResponse(List<CarritoItem> Productos);
	public record struct CarritoItem(Producto Producto,Empresa Empresa, int Cantidad);
}

