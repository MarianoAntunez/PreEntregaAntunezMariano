using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Productos
    {
        CD_Conexion Con = new CD_Conexion();
        SqlCommand Cmd;

        //Metodo para agregar producto
        public void AgregarProducto(CE_Productos Productos)
        {
            Cmd = new SqlCommand("AgregarProducto", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Codigo", Productos.Codigo));
            Cmd.Parameters.Add(new SqlParameter("@Nombre", Productos.Nombre));
            Cmd.Parameters.Add(new SqlParameter("@Descripcion", Productos.Descripcion));
            Cmd.Parameters.Add(new SqlParameter("@Presentacion", Productos.Presentacion));
            Cmd.Parameters.Add(new SqlParameter("@Costo_unitario", Productos.Costo_Unitario));
            Cmd.Parameters.Add(new SqlParameter("@Precio_venta", Productos.Precio_Venta));
            Cmd.Parameters.Add(new SqlParameter("@Tipo_cargo", Productos.Tipo_Cargo));
            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        //Metodo para editar producto
        public void EditarProducto(CE_Productos Productos)
        {
            Cmd = new SqlCommand("EditarProducto", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Codigo", Productos.Codigo));
            Cmd.Parameters.Add(new SqlParameter("@Nombre", Productos.Nombre));
            Cmd.Parameters.Add(new SqlParameter("@Descripcion", Productos.Descripcion));
            Cmd.Parameters.Add(new SqlParameter("@Presentacion", Productos.Presentacion));
            Cmd.Parameters.Add(new SqlParameter("@Costo_unitario", Productos.Costo_Unitario));
            Cmd.Parameters.Add(new SqlParameter("@Precio_venta", Productos.Precio_Venta));
            Cmd.Parameters.Add(new SqlParameter("@Tipo_cargo", Productos.Tipo_Cargo));
            Cmd.Parameters.Add(new SqlParameter("@Id_producto", Productos.id_Producto));
            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        //Metodo para eliminar producto
        public void EliminarProducto(CE_Productos Productos)
        {
            int Existencia = 0;

            Cmd = new SqlCommand("Select Cantidad From Inventario Where Id_Inventario=" + Productos.id_Producto + "", Con.Abrir());
            Cmd.CommandType = CommandType.Text;

            SqlDataReader Dr = Cmd.ExecuteReader();
            if (Dr.Read())
            {
                Existencia = Convert.ToInt32(Dr["Cantidad"].ToString());
            }
            Dr.Close();

            if (Existencia != 0)
            {
                MessageBox.Show("El Producto contiene existencia, no puede ser eliminado", "Eliminar Producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                Cmd = new SqlCommand("EliminarProducto", Con.Abrir());
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.Add(new SqlParameter("@Id_producto", Productos.id_Producto));
                Cmd.ExecuteNonQuery();

                MessageBox.Show("El producto fue eliminado con exito", "Eliminar Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Con.Cerrar();
            }
        }
    }
}
