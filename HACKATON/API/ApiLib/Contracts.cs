using System;
using ApiLib;
using ApiLib.Models;

namespace ApiLib
{
	public interface BcpContracts
	{
		LoginResponse Login(LoginRequest request);
		RegistroResponse SignIn(RegistroRequest request);
		CategoriasResponse GetCategorias();
		EmpresasResponse GetEmpresas(EmpresasRequest request);
		ProductosResponse GetProductos(ProductosRequest request);
		bool DeleteProducto(ProductoRequest request);
		bool DeleteCategoria(CategoriasRequest request);
		bool DeleteEmpresa(EmpresaRequest request);
		RegProductoResponse SetProductos(NewProducto producto);
		RegEmpresaResponse SetEmpresa(NewEmpresa empresa);
		RegCategoriaResponse SetCategoria(NewCategoria categoria);
		RegCategoriaResponse UpdateCategoria(UpdateCategoria categoria);
		RegProductoResponse UpdateProducto(UpdateProducto producto);
		RegEmpresaResponse UpdateEmpresa(UpdateEmpresa empresa);
		CarritoResponse GetCarrito(int userId);
		CarritoResponse AddItemToCarrito(int UserId, int ProductoId, int Cantidad);
		CarritoResponse UpdateItemToCarrito(int UserId, int ProductoId, int Cantidad);
		CarritoResponse DeleteItemToCarrito(int UserId, int ProductoId);
	}
}

