using System;
using System.Windows.Forms;
using CapaDominio;
using CapaEntidad;

namespace CapaPresentacion
{
    public partial class FrmProductos : FormBase
    {
        public FrmProductos()
        {
            InitializeComponent();
        }

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Productos Productos = new CDo_Productos();
        CE_Productos Producto = new CE_Productos();

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            CargarDatos();

            dataGridView1.Columns[0].Visible = false;// id producto

            dataGridView1.Columns[1].Width = 150;// codigo producto
            dataGridView1.Columns[2].Width = 270;// codigo producto
            dataGridView1.Columns[3].Width = 300;// codigo producto
            dataGridView1.Columns[4].Width = 150;// codigo producto
            dataGridView1.Columns[5].Width = 140;// codigo producto
            dataGridView1.Columns[6].Width = 140;// codigo producto
            dataGridView1.Columns[7].Width = 150;// codigo producto

            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;// codigo producto
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;// codigo producto
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;// codigo producto
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;// codigo producto

            dataGridView1.Columns[5].DefaultCellStyle.Format = "#,##0.00";
            dataGridView1.Columns[6].DefaultCellStyle.Format = "#,##0.00";

            Procedimientos.AlternarColorFilaDataGridView(dataGridView1);
        }

        private void CargarDatos()
        {
            dataGridView1.DataSource = Procedimientos.CargarDatos("Productos");
            dataGridView1.ClearSelection();
        }

        private void AgPro_UpdateEventHandler(object sender, FrmAgregarProducto.UpdateEventArgs args)
        {
            CargarDatos();
        }

        private void EdPro_UpdateEventHandler(object sender, FrmEditarProductos.UpdateEventArgs args)
        {
            CargarDatos();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmAgregarProducto AgregarProducto = new FrmAgregarProducto(this);
            AgregarProducto.UpdateEventHandler += AgPro_UpdateEventHandler;
            AgregarProducto.ShowDialog();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No hay registro para editar", "Editar Producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    if (dataGridView1.SelectedRows == null)
                    {
                        return;
                    }
                    else
                    {
                        FrmEditarProductos EditarProductos = new FrmEditarProductos(this);
                        EditarProductos.UpdateEventHandler += EdPro_UpdateEventHandler;
                        EditarProductos.txtId_Producto.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                        EditarProductos.txtCodProducto.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                        EditarProductos.txtNomProducto.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                        EditarProductos.txtDesProducto.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                        EditarProductos.txtPresentacion.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                        EditarProductos.txtCostoUnitario.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                        EditarProductos.txtPrecioVenta.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                        EditarProductos.cboTipoCargo.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                        EditarProductos.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Debe seleccionar un producto", "Editar Producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        public override void Eliminar()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No hay Registros para eliminar", "Eliminar Producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    if (dataGridView1.SelectedRows == null)
                    {
                        return;
                    }
                    else
                    {
                        DialogResult Resultados = MessageBox.Show("¿Está seguro que desea ELIMINAR este producto? El producto se eliminara de la base de datos", "Eliminar Producto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (Resultados == DialogResult.Yes)
                        {
                            Producto.id_Producto = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                            Productos.EliminarProducto(Producto);
                            CargarDatos();
                        }
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Debe seleccionar un producto para eliminar", "Eliminar Producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}
