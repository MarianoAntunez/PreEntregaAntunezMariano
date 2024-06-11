using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using CapaDatos;

namespace CapaDominio
{
    public class CDo_Procedimientos
    {
        CD_Procedimientos ObjProcedimientos = new CD_Procedimientos();

        //Metodo para cargar los datos de una tabla a un DataGridView
        public DataTable CargarDatos(string Tabla)
        {
            return ObjProcedimientos.CargarDatos(Tabla);
        }

        //Metodo para alternar los colores en las filas de un DataGridView
        public void AlternarColorFilaDataGridView(DataGridView Dgv)
        {
            ObjProcedimientos.AlternarColorFilaDataGridView(Dgv);
        }

        //Metodo para cargar los datos de una tabla a un DataGridView
        public string GenerarCodigo(string Tabla)
        {
            return ObjProcedimientos.GenerarCodigo(Tabla);
        }

        //Metodo para cargar los datos de una tabla a un DataGridView
        public string GenerarCodigoId(string Tabla)
        {
           return ObjProcedimientos.GenerarCodigoId(Tabla);
        }

        //Metodo para dar formato moneda a un textbox
        public void FormatoMoneda(TextBox xTBox)
        {
            ObjProcedimientos.FormatoMoneda(xTBox);
        }

        //Metodo para limpiar textbox
        public void LimpiarControles(Form xForm)
        {
           ObjProcedimientos.LimpiarControles(xForm);
        }

        //Metodo para llenar combobox
        public void LlenarComboBox(String Tabla, string Nombre, ComboBox xCBox)
        {
            ObjProcedimientos.LlenarComboBox(Tabla, Nombre, xCBox);
        }
    }
}
