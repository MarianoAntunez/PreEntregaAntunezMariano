﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CapaDatos
{
    public class CD_Procedimientos
    {
        CD_Conexion Con = new CD_Conexion();

        SqlCommand Cmd;
        SqlDataReader Dr;
        DataTable Dt;

        //Metodo para cargar los datos de una tabla a un DataGridView
        public DataTable CargarDatos(string Tabla)
        {
            Dt = new DataTable("Cargar_Datos");
            Cmd = new SqlCommand("Select * From " + Tabla, Con.Abrir());
            Cmd.CommandType = CommandType.Text;

            Dr = Cmd.ExecuteReader();
            Dt.Load(Dr);
            Dr.Close();

            Con.Cerrar();
            return Dt;
        }

        //Metodo para alternar los colores en las filas de un DataGridView
        public void AlternarColorFilaDataGridView(DataGridView Dgv)
        {
            Dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            Dgv.DefaultCellStyle.BackColor = Color.White;
        }

        //Metodo para cargar los datos de una tabla a un DataGridView
        public string GenerarCodigo(string Tabla)
        {
            string Codigo = string.Empty;
            int Total = 0;

            Cmd = new SqlCommand("Select Count(*) as TotalRegustros From " + Tabla, Con.Abrir());
            Cmd.CommandType = CommandType.Text;

            Dr = Cmd.ExecuteReader();

            if (Dr.Read())
            {
                Total = Convert.ToInt32(Dr["TotalRegustros"]) + 1;
            }
            Dr.Close();

            if (Total < 10)
            {
                Codigo = "0000000" + Total;
            }
            else if (Total < 100)
            {
                Codigo = "000000" + Total;
            }
            else if (Total < 1000)
            {
                Codigo = "00000" + Total;
            }
            else if (Total < 10000)
            {
                Codigo = "0000" + Total;
            }
            else if (Total < 100000)
            {
                Codigo = "000" + Total;
            }
            else if (Total < 1000000)
            {
                Codigo = "00" + Total;
            }
            else if (Total < 10000000)
            {
                Codigo = "0" + Total;
            }

            Con.Cerrar();
            return Codigo;
        }

        //Metodo para cargar los datos de una tabla a un DataGridView
        public string GenerarCodigoId(string Tabla)
        {
            string Codigo = string.Empty;
            int Total = 0;

            Cmd = new SqlCommand("Select Count(*) as TotalRegustros From " + Tabla, Con.Abrir());
            Cmd.CommandType = CommandType.Text;

            Dr = Cmd.ExecuteReader();

            if (Dr.Read())
            {
                Total = Convert.ToInt32(Dr["TotalRegustros"]) + 1;
            }
            Dr.Close();

            Con.Cerrar();
            return Codigo;
        }

        //Metodo para dar formato moneda a un textbox
        public void FormatoMoneda(TextBox xTBox)
        {
            if (xTBox.Text == string.Empty)
            {
                return;
            }
            else
            {
                decimal Monto;

                Monto = Convert.ToDecimal(xTBox.Text);
                xTBox.Text = Monto.ToString("N2");
            }
        }

        //Metodo para limpiar textbox
        public void LimpiarControles(Form xForm)
        {
            foreach (var xCtrl in xForm.Controls)
            {
                if (xCtrl is TextBox)
                {
                    ((TextBox)xCtrl).Text = String.Empty;
                }
                else if (xCtrl is ComboBox)
                {
                    ((ComboBox)xCtrl).Text = String.Empty;
                }
            }
        }

        //Metodo para llenar combobox
        public void LlenarComboBox(String Tabla, string Nombre, ComboBox xCBox)
        {
            Cmd = new SqlCommand("Select * From" + Tabla, Con.Abrir());
            Cmd.CommandType = CommandType.Text;

            Dr = Cmd.ExecuteReader();

            while (Dr.Read())
            {
                xCBox.Items.Add(Dr[Nombre].ToString());
            }
        }

    }
}
