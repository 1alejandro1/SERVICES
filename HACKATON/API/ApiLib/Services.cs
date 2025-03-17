
using System.Data.SqlClient;
using ApiLib;
using ApiLib.Models;

namespace ApiLib
{
    public class Services : BcpContracts
    {
        public string connectionstring;
        public Services()
        {
            //TODO: cambiar a variable de appsettings
            connectionstring = "Server=tcp:bcpserver.database.windows.net,1433;Initial Catalog=BcpCrecer;Persist Security Info=False;User ID=creceruser;Password=ulNFXDLbJuzo!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
        public LoginResponse Login(LoginRequest request)
        {
            LoginResponse response = new LoginResponse(0,"",0,"", "", "", "", "", "", "", "", "","","");
            using SqlConnection connection = new(connectionstring);
            using SqlCommand cmd = new("[dbo].Login", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", request.telefono);
            cmd.Parameters.AddWithValue("@Contrasena", request.password);
            connection.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {

                    LoginResponse res = new LoginResponse
                    (
                        id: Convert.ToInt32(sdr["Id"]),
                        email: Convert.ToString(sdr["Email"]) == null ? "" : Convert.ToString(sdr["Email"])!,
                        telefono: Convert.ToInt32(sdr["Telefono"]),
                        nombre: Convert.ToString(sdr["Nombre"]) == null ? "" : Convert.ToString(sdr["Nombre"])!,
                        paterno: Convert.ToString(sdr["Paterno"]) == null ? "" : Convert.ToString(sdr["Paterno"])!,
                        materno: Convert.ToString(sdr["Materno"]) == null ? "" : Convert.ToString(sdr["Materno"])!,
                        imagen: Convert.ToString(sdr["Imagen"]) == null ? "" : Convert.ToString(sdr["Imagen"])!,
                        fecnac: Convert.ToString(sdr["Fecnac"]) == null ? "" : Convert.ToString(sdr["Fecnac"])!,
                        idc: Convert.ToString(sdr["Idc"]) == null ? "" : Convert.ToString(sdr["Idc"])!,
                        tipoidc: Convert.ToString(sdr["Tipoidc"]) == null ? "" : Convert.ToString(sdr["Tipoidc"])!,
                        extidc: Convert.ToString(sdr["Extidc"]) == null ? "" : Convert.ToString(sdr["Extidc"])!,
                        Role: Convert.ToString(sdr["Role"]) == null ? "" : Convert.ToString(sdr["Role"])!,
                        complementoidc: Convert.ToString(sdr["Complementoidc"]) == null ? "" : Convert.ToString(sdr["Complementoidc"])!,
                        token: ""!
                    );

                    response = res;
                }
            }
            connection.Close();
            return response;
        }
        public RegistroResponse SignIn(RegistroRequest request)
        {

            RegistroResponse response = new RegistroResponse(0, "", 0, "", "", "", "", "", "", "", "", "", "", "");
            using SqlConnection connection = new(connectionstring);
            using SqlCommand cmd = new("[dbo].SignIn", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Telefono", request.telefono);
            cmd.Parameters.AddWithValue("@Email", request.email);
            cmd.Parameters.AddWithValue("@Contrasena", request.password);
            cmd.Parameters.AddWithValue("@Nombre", request.nombre);
            cmd.Parameters.AddWithValue("@Paterno", request.paterno);
            cmd.Parameters.AddWithValue("@Materno", request.materno);
            cmd.Parameters.AddWithValue("@Imagen", request.imagen);
            cmd.Parameters.AddWithValue("@Fecnac", request.fecnac);
            cmd.Parameters.AddWithValue("@Idc", request.idc);
            cmd.Parameters.AddWithValue("@Tipoidc", request.tipoidc);
            cmd.Parameters.AddWithValue("@Extidc", request.extidc);
            cmd.Parameters.AddWithValue("@Complementoidc", request.complementoidc);
            cmd.Parameters.AddWithValue("@Role", request.Role);
            connection.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    if (sdr["Id"].ToString() != "" || sdr["Id"].ToString() != null)
                    {
                        RegistroResponse res = new RegistroResponse
                (
                    id: Convert.ToInt32(sdr["Id"]),
                    email: Convert.ToString(sdr["Email"]) == null ? "" : Convert.ToString(sdr["Email"])!,
                    telefono: Convert.ToInt32(sdr["Telefono"]),
                    nombre: Convert.ToString(sdr["Nombre"]) == null ? "" : Convert.ToString(sdr["Nombre"])!,
                    paterno: Convert.ToString(sdr["Paterno"]) == null ? "" : Convert.ToString(sdr["Paterno"])!,
                    materno: Convert.ToString(sdr["Materno"]) == null ? "" : Convert.ToString(sdr["Materno"])!,
                    imagen: Convert.ToString(sdr["Imagen"]) == null ? "" : Convert.ToString(sdr["Imagen"])!,
                    fecnac: Convert.ToString(sdr["Fecnac"]) == null ? "" : Convert.ToString(sdr["Fecnac"])!,
                    idc: Convert.ToString(sdr["Idc"]) == null ? "" : Convert.ToString(sdr["Idc"])!,
                    tipoidc: Convert.ToString(sdr["Tipoidc"]) == null ? "" : Convert.ToString(sdr["Tipoidc"])!,
                    extidc: Convert.ToString(sdr["Extidc"]) == null ? "" : Convert.ToString(sdr["Extidc"])!,
                    complementoidc: Convert.ToString(sdr["Complementoidc"]) == null ? "" : Convert.ToString(sdr["Complementoidc"])!,
                    Role: Convert.ToString(sdr["Role"]) == null ? "" : Convert.ToString(sdr["Role"])!,
                    token: ""!
                );
                        response = res;
                    }
                    else
                    {
                        return response;
                    }
                }
            }
            connection.Close();
            return response;
        }
        public CategoriasResponse GetCategorias()
        {
            
            List<Categoria> Categorias=new();
            CategoriasResponse response = new CategoriasResponse(Categorias);
            using SqlConnection connection = new(connectionstring);
            using SqlCommand cmd = new("[dbo].GetCategorias", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {                
                while (sdr.Read())
                {
                    Categorias.Add(new Categoria
                    (
                        Id: Convert.ToInt32(sdr["Id"]),
                        Nombre: Convert.ToString(sdr["Nombre"]) == null ? "" : Convert.ToString(sdr["Nombre"])!,
                        Imagen: Convert.ToString(sdr["Imagen"]) == null ? "" : Convert.ToString(sdr["Imagen"])!,
                        Color: Convert.ToString(sdr["Color"]) == null ? "" : Convert.ToString(sdr["Color"])!
                    ));
                    response = new CategoriasResponse(Categorias);
                }
            }
            connection.Close();
            return response;
        }
        public EmpresasResponse GetEmpresas(EmpresasRequest request)
        {

            List<Empresa> Empresas = new();
            EmpresasResponse response = new EmpresasResponse(Empresas);
            using SqlConnection connection = new(connectionstring);
            using SqlCommand cmd = new("[dbo].GetEmpresas", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CategoriaId", request.CategoriaId);
            connection.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    Empresas.Add(new Empresa
                    (
                        Id: Convert.ToInt32(sdr["Id"]),
                        Nombre: Convert.ToString(sdr["Nombre"]) == null ? "" : Convert.ToString(sdr["Nombre"])!,
                        Imagen: Convert.ToString(sdr["Imagen"]) == null ? "" : Convert.ToString(sdr["Imagen"])!,
                        Color: Convert.ToString(sdr["Color"]) == null ? "" : Convert.ToString(sdr["Color"])!,
                        CategoriaId: Convert.ToInt32(sdr["CategoriaId"])
                    ));
                    response = new EmpresasResponse(Empresas);
                }
            }
            connection.Close();
            return response;
        }
        public ProductosResponse GetProductos(ProductosRequest request)
        {

            List<Producto> Productos = new();
            ProductosResponse response = new ProductosResponse(Productos);
            using SqlConnection connection = new(connectionstring);
            using SqlCommand cmd = new("[dbo].GetProductosByEmpresaId", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpresaId", request.EmpresaId);
            connection.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    Productos.Add(new Producto
                    (
                        Id: Convert.ToInt32(sdr["Id"]),
                        Nombre: Convert.ToString(sdr["Nombre"]) == null ? "" : Convert.ToString(sdr["Nombre"])!,
                        Imagen: Convert.ToString(sdr["Imagen"]) == null ? "" : Convert.ToString(sdr["Imagen"])!,
                        Color: Convert.ToString(sdr["Color"]) == null ? "" : Convert.ToString(sdr["Color"])!,
                        Precio: Convert.ToDouble(sdr["Precio"]),
                        Descuento: Convert.ToDouble(sdr["Descuento"]),
                        Descripcion: Convert.ToString(sdr["Descripcion"]) == null ? "" : Convert.ToString(sdr["Descripcion"])!,
                        EmpresaId: Convert.ToInt32(sdr["EmpresaId"])
                    ));
                    response = new ProductosResponse(Productos);
                }
            }
            connection.Close();
            return response;
        }
        public bool DeleteProducto(ProductoRequest request)
        {
            try
            {
                using SqlConnection connection = new(connectionstring);
                using SqlCommand cmd = new("[dbo].[DeleteProducto]", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", request.ProductoId);
                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                return true;
                connection.Close();
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool DeleteEmpresa(EmpresaRequest request)
        {
            try
            {
                using SqlConnection connection = new(connectionstring);
                using SqlCommand cmd = new("[dbo].[DeleteEmpresa]", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", request.EmpresaId);
                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                return true;
                connection.Close();
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool DeleteCategoria(CategoriasRequest request)
        {
            try
            {
                using SqlConnection connection = new(connectionstring);
                using SqlCommand cmd = new("[dbo].[DeleteCategoria]", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", request.CategoriaId);
                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                return true;
                connection.Close();
            }
            catch (Exception)
            {

                return false;
            }
        }
        public RegProductoResponse SetProductos(NewProducto producto)
        {
            RegProductoResponse response = new RegProductoResponse(0, "", "", "", 0, 0, "", 0);
            try
            {
                using SqlConnection connection = new(connectionstring);
                using SqlCommand cmd = new("[dbo].SetProductos", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@Imagen", producto.Imagen);
                cmd.Parameters.AddWithValue("@Color", producto.Color);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@Descuento", producto.Descuento);
                cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                cmd.Parameters.AddWithValue("@EmpresaId", producto.EmpresaId);
                connection.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["Id"].ToString() != "" || sdr["Id"].ToString() != null)
                        {
                            RegProductoResponse res = new RegProductoResponse (
                        Id: Convert.ToInt32(sdr["Id"]),
                        Nombre: Convert.ToString(sdr["Nombre"]) == null ? "" : Convert.ToString(sdr["Nombre"])!,
                        Imagen: Convert.ToString(sdr["Imagen"]) == null ? "" : Convert.ToString(sdr["Imagen"])!,
                        Color: Convert.ToString(sdr["Color"]) == null ? "" : Convert.ToString(sdr["Color"])!,
                        Precio: Convert.ToInt32(sdr["Precio"]),
                        Descuento: Convert.ToInt32(sdr["Descuento"]),
                        Descripcion: Convert.ToString(sdr["Descripcion"]) == null ? "" : Convert.ToString(sdr["Descripcion"])!,
                        EmpresaId: Convert.ToInt32(sdr["EmpresaId"])
                       );
                            response = res;
                        }
                        else
                        {
                            return response;
                        }
                    }
                }
                connection.Close();
                return response;
            }
            catch(Exception ex)
            {
                return response;
            }

            }
        public RegEmpresaResponse SetEmpresa(NewEmpresa empresa)
        {
            RegEmpresaResponse response = new RegEmpresaResponse(0, "", "", "", 0);
            try
            {
                using SqlConnection connection = new(connectionstring);
                using SqlCommand cmd = new("[dbo].SetEmpresa", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", empresa.Nombre);
                cmd.Parameters.AddWithValue("@Imagen", empresa.Imagen);
                cmd.Parameters.AddWithValue("@Color", empresa.Color);
                cmd.Parameters.AddWithValue("@CategoriaId", empresa.CategoriaId);
                connection.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["Id"].ToString() != "" || sdr["Id"].ToString() != null)
                        {
                            RegEmpresaResponse res = new RegEmpresaResponse(
                        Id: Convert.ToInt32(sdr["Id"]),
                        Nombre: Convert.ToString(sdr["Nombre"]) == null ? "" : Convert.ToString(sdr["Nombre"])!,
                        Imagen: Convert.ToString(sdr["Imagen"]) == null ? "" : Convert.ToString(sdr["Imagen"])!,
                        Color: Convert.ToString(sdr["Color"]) == null ? "" : Convert.ToString(sdr["Color"])!,     
                        CategoriaId: Convert.ToInt32(sdr["CategoriaId"])           
                       );
                            response = res;
                        }
                        else
                        {
                            return response;
                        }
                    }
                }
                connection.Close();
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }

        }
        public RegCategoriaResponse SetCategoria(NewCategoria categoria)
        {
            RegCategoriaResponse response = new RegCategoriaResponse(0, "", "", "");
            try
            {
                using SqlConnection connection = new(connectionstring);
                using SqlCommand cmd = new("[dbo].SetCategoria", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                cmd.Parameters.AddWithValue("@Imagen", categoria.Imagen);
                cmd.Parameters.AddWithValue("@Color", categoria.Color);
                connection.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["Id"].ToString() != "" || sdr["Id"].ToString() != null)
                        {
                            RegCategoriaResponse res = new RegCategoriaResponse(
                        Id: Convert.ToInt32(sdr["Id"]),
                        Nombre: Convert.ToString(sdr["Nombre"]) == null ? "" : Convert.ToString(sdr["Nombre"])!,
                        Imagen: Convert.ToString(sdr["Imagen"]) == null ? "" : Convert.ToString(sdr["Imagen"])!,
                        Color: Convert.ToString(sdr["Color"]) == null ? "" : Convert.ToString(sdr["Color"])!
                       );
                            response = res;
                        }
                        else
                        {
                            return response;
                        }
                    }
                }
                connection.Close();
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }

        }
        public CarritoResponse GetCarrito(int UserId)
        {
            List<CarritoItem> CarritoList = new();
            CarritoResponse response = new CarritoResponse(CarritoList);
            using SqlConnection connection = new(connectionstring);
            using SqlCommand cmd = new("[dbo].GetCarrito", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            connection.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    var newproducto=new Producto
                    (
                        Id: Convert.ToInt32(sdr["ProductoId"]),
                        Nombre: Convert.ToString(sdr["ProductoNombre"]) == null ? "" : Convert.ToString(sdr["ProductoNombre"])!,
                        Imagen: Convert.ToString(sdr["ProductoImagen"]) == null ? "" : Convert.ToString(sdr["ProductoImagen"])!,
                        Color: Convert.ToString(sdr["ProductoColor"]) == null ? "" : Convert.ToString(sdr["ProductoColor"])!,
                        Precio: Convert.ToDouble(sdr["ProductoPrecio"]),
                        Descuento: Convert.ToDouble(sdr["ProductoDescuento"]),
                        Descripcion: Convert.ToString(sdr["ProductoDescripcion"]) == null ? "" : Convert.ToString(sdr["ProductoDescripcion"])!,
                        EmpresaId: Convert.ToInt32(sdr["EmpresaId"])
                    );
                    var newEmpresa = new Empresa
                    (
                        Id: Convert.ToInt32(sdr["EmpresaId"]),
                        Nombre: Convert.ToString(sdr["EmpresaNombre"]) == null ? "" : Convert.ToString(sdr["EmpresaNombre"])!,
                        Imagen: Convert.ToString(sdr["EmpresaImagen"]) == null ? "" : Convert.ToString(sdr["EmpresaImagen"])!,
                        Color: Convert.ToString(sdr["EmpresaColor"]) == null ? "" : Convert.ToString(sdr["EmpresaColor"])!,
                        CategoriaId: Convert.ToInt32(sdr["CategoriaId"])
                    );
                    CarritoList.Add(new CarritoItem
                    (
                        newproducto,
                        newEmpresa,
                        Cantidad: Convert.ToInt32(sdr["Cantidad"])
                    ));
                    response = new CarritoResponse(CarritoList);
                }
            }
            connection.Close();
            return response;
        }
        public RegCategoriaResponse UpdateCategoria(UpdateCategoria request)
        {
            RegCategoriaResponse response = new RegCategoriaResponse(0, "", "", "");
            try
            {
                using SqlConnection connection = new(connectionstring);
                using SqlCommand cmd = new("[dbo].[UpdateCategoria]", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", request.Id);
                cmd.Parameters.AddWithValue("@Nombre", request.Nombre);
                cmd.Parameters.AddWithValue("@Imagen", request.Imagen);
                cmd.Parameters.AddWithValue("@Color", request.Color);
                connection.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["Id"].ToString() != "" || sdr["Id"].ToString() != null)
                        {
                            RegCategoriaResponse res = new RegCategoriaResponse(
                        Id: Convert.ToInt32(sdr["Id"]),
                        Nombre: Convert.ToString(sdr["Nombre"]) == null ? "" : Convert.ToString(sdr["Nombre"])!,
                        Imagen: Convert.ToString(sdr["Imagen"]) == null ? "" : Convert.ToString(sdr["Imagen"])!,
                        Color: Convert.ToString(sdr["Color"]) == null ? "" : Convert.ToString(sdr["Color"])!
                       );
                            response = res;
                        }
                        else
                        {
                            return response;
                        }
                    }
                }
                connection.Close();
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }

        }
        public RegEmpresaResponse UpdateEmpresa(UpdateEmpresa request)
        {
            RegEmpresaResponse response = new RegEmpresaResponse(0, "", "", "", 0);
            try
            {
                using SqlConnection connection = new(connectionstring);
                using SqlCommand cmd = new("[dbo].[UpdateEmpresa]", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", request.Id);
                cmd.Parameters.AddWithValue("@Nombre", request.Nombre);
                cmd.Parameters.AddWithValue("@Imagen", request.Imagen);
                cmd.Parameters.AddWithValue("@Color", request.Color);
                cmd.Parameters.AddWithValue("@CategoriaId", request.CategoriaId);
                connection.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["Id"].ToString() != "" || sdr["Id"].ToString() != null)
                        {
                            RegEmpresaResponse res = new RegEmpresaResponse(
                        Id: Convert.ToInt32(sdr["Id"]),
                        Nombre: Convert.ToString(sdr["Nombre"]) == null ? "" : Convert.ToString(sdr["Nombre"])!,
                        Imagen: Convert.ToString(sdr["Imagen"]) == null ? "" : Convert.ToString(sdr["Imagen"])!,
                        Color: Convert.ToString(sdr["Color"]) == null ? "" : Convert.ToString(sdr["Color"])!,
                        CategoriaId: Convert.ToInt32(sdr["CategoriaId"])
                       );
                            response = res;
                        }
                        else
                        {
                            return response;
                        }
                    }
                }
                connection.Close();
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }

        }
        public RegProductoResponse UpdateProducto(UpdateProducto request)
        {
            RegProductoResponse response = new RegProductoResponse(0, "", "", "", 0, 0, "", 0);
            try
            {
                using SqlConnection connection = new(connectionstring);
                using SqlCommand cmd = new("[dbo].[UpdateProducto]", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", request.Id);
                cmd.Parameters.AddWithValue("@Nombre", request.Nombre);
                cmd.Parameters.AddWithValue("@Imagen", request.Imagen);
                cmd.Parameters.AddWithValue("@Color", request.Color);
                cmd.Parameters.AddWithValue("@Precio", request.Precio);
                cmd.Parameters.AddWithValue("@Descuento", request.Descuento);
                cmd.Parameters.AddWithValue("@Descripcion", request.Descripcion);
                cmd.Parameters.AddWithValue("@EmpresaId", request.EmpresaId);
                connection.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["Id"].ToString() != "" || sdr["Id"].ToString() != null)
                        {
                            RegProductoResponse res = new RegProductoResponse(
                        Id: Convert.ToInt32(sdr["Id"]),
                        Nombre: Convert.ToString(sdr["Nombre"]) == null ? "" : Convert.ToString(sdr["Nombre"])!,
                        Imagen: Convert.ToString(sdr["Imagen"]) == null ? "" : Convert.ToString(sdr["Imagen"])!,
                        Color: Convert.ToString(sdr["Color"]) == null ? "" : Convert.ToString(sdr["Color"])!,
                        Precio: Convert.ToInt32(sdr["Precio"]),
                        Descuento: Convert.ToInt32(sdr["Descuento"]),
                        Descripcion: Convert.ToString(sdr["Descripcion"]) == null ? "" : Convert.ToString(sdr["Descripcion"])!,
                        EmpresaId: Convert.ToInt32(sdr["EmpresaId"])
                       );
                            response = res;
                        }
                        else
                        {
                            return response;
                        }
                    }
                }
                connection.Close();
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }

        }
        public CarritoResponse AddItemToCarrito(int UserId, int ProductoId, int Cantidad)
        {
            throw new NotImplementedException();
        }

        public CarritoResponse UpdateItemToCarrito(int UserId, int ProductoId, int Cantidad)
        {
            throw new NotImplementedException();
        }

        public CarritoResponse DeleteItemToCarrito(int ProductId, int CantidadId)
        {
            throw new NotImplementedException();
        }
    }
}

