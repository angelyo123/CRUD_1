using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebApplicationExamenPOO.Models;

namespace WebApplicationExamenPOO.Controllers
{
    public class NegociosController : Controller
    {
        /*LISTAR PAISES*/
        IEnumerable<Paises> paises()
        {
            List<Paises> paises = new List<Paises>();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec sp_listar_paises", cn);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    paises.Add(new Paises
                    {
                        Idpais = dr.GetString(0),
                        NombrePais = dr.GetString(1),
                    });
                }
                dr.Close();
                cn.Close();
            }
            return paises;
        }

        public ActionResult ListarPaises()
        {
            return View(paises());
        }

        /***********AGREGAR CONSUMIDOR****/

        public ActionResult AgregarConsumidor()
        {
            return View(new Consumidor());
        }
        [HttpPost]
        public ActionResult AgregarConsumidor(Consumidor consumidor)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_agregar_consumidor", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idcont", consumidor.idConsumidor);
                        cmd.Parameters.AddWithValue("@nomcont", consumidor.Nombre);
                        cmd.Parameters.AddWithValue("@apecont", consumidor.Apellido);
                        cmd.Parameters.AddWithValue("@direccion", consumidor.Direccion);
                        cmd.Parameters.AddWithValue("@idpais", consumidor.IdPais);
                        cmd.Parameters.AddWithValue("@emailcont", consumidor.Email);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

                 
            }
            ViewBag.Mensaje = "El consumidor se ha agregado correctamente.";
            return View(consumidor);
        }

        /*LISTAR CONSUMIDORES*/

        /*LISTAR PAISES*/
        IEnumerable<Consumidor> consumidores()
        {
            List<Consumidor> consumidores = new List<Consumidor>();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec sp_listar_consumidor", cn);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    consumidores.Add(new Consumidor
                    {
                        idConsumidor = dr.GetString(0),
                        Nombre = dr.GetString(1),
                        Apellido = dr.GetString(2),
                        Direccion = dr.GetString(3),
                        IdPais = dr.GetString(4),
                        Email = dr.GetString(5),
                    });
                }
                dr.Close();
                cn.Close();
            }
            return consumidores;
        }

        public ActionResult ListarConsumidores()
        {
            return View(consumidores());
        }

        /*ACTUALIZAR CONSUMIDORES*/

        public ActionResult ActualizarConsumidor(string id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_buscar_consumidor_por_id", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idcont", id);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Consumidor consumidor = new Consumidor
                        {
                            idConsumidor = reader["idcont"].ToString(),
                            Nombre = reader["nomcont"].ToString(),
                            Apellido = reader["apecont"].ToString(),
                            Direccion = reader["direccion"].ToString(),
                            IdPais = reader["idpais"].ToString(),
                            Email = reader["emailcont"].ToString()
                        };

                        conn.Close();
                        return View(consumidor);
                    }
                    else
                    {
                        conn.Close();
                        return HttpNotFound();
                    }
                }
            }
        }


        [HttpPost]
        public ActionResult ActualizarConsumidor(Consumidor consumidor)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_actualizar_consumidor", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idcont", consumidor.idConsumidor);
                        cmd.Parameters.AddWithValue("@nomcont", consumidor.Nombre);
                        cmd.Parameters.AddWithValue("@apecont", consumidor.Apellido);
                        cmd.Parameters.AddWithValue("@direccion", consumidor.Direccion);
                        cmd.Parameters.AddWithValue("@idpais", consumidor.IdPais);
                        cmd.Parameters.AddWithValue("@emailcont", consumidor.Email);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

               
            }
            ViewBag.Mensaje = "El consumidor se ha actualizado correctamente.";

            return View(consumidor);
        }

        public ActionResult EliminarConsumidor(string id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_eliminar_consumidor", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idcont", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            ViewBag.Mensaje = "El consumidor se ha eliminado correctamente.";
            return RedirectToAction("ListarConsumidores");
        }

       
    public ActionResult BuscarConsumidor(string nombre)
        {
            List<Consumidor> consumidores = new List<Consumidor>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_buscar_consumidor", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nomcont", nombre);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Consumidor consumidor = new Consumidor
                        {
                            idConsumidor = reader["idcont"].ToString(),
                            Nombre = reader["nomcont"].ToString(),
                            Apellido = reader["apecont"].ToString(),
                            Direccion = reader["direccion"].ToString(),
                            IdPais = reader["idpais"].ToString(),
                            Email = reader["emailcont"].ToString()
                        };

                        consumidores.Add(consumidor);
                    }

                    conn.Close();
                }
            }
            ViewBag.Mensaje = "El consumidor se ha encontrado correctamente.";
            return View("ListarConsumidores", consumidores);
        }

        /*PROBLEMA NUMERO 2*/
        /*******FILTRADO**********/

        public ActionResult Filtrado()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Filtrado(string categoria)
        {
            if (!string.IsNullOrEmpty(categoria))
            {
                List<Producto> productos = new List<Producto>();

                string connectionString = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_listar_productos_por_categoria", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombreCategoria", categoria);

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Producto producto = new Producto
                            {
                                IdProducto = Convert.ToInt32(reader["IdProducto"]),
                                NombreProducto = reader["NomProducto"].ToString(),
                                Precio = Convert.ToDecimal(reader["PrecioUnidad"]),
                                IdCategoria = Convert.ToInt32(reader["IdCategoria"])
                            };
                            productos.Add(producto);
                        }
                        conn.Close();
                    }
                }

                ViewBag.NumRegistros = productos.Count;
                return View(productos);
            }
            else
            {
                ModelState.AddModelError("categoria", "Debe ingresar un nombre de categoría.");
                return View();
            }
        }

        /****************/


    }
}



  