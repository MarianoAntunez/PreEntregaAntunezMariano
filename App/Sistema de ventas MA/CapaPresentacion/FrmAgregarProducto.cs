using System;
using System.Windows.Forms;
using CapaDominio;
using CapaEntidad;

namespace CapaPresentacion
{
    public partial class FrmAgregarProducto : FormBase
    {
        public FrmAgregarProducto(FrmProductos Productos)
        {
            InitializeComponent();
        }

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Productos Productos = new CDo_Productos();
        CE_Productos Producto = new CE_Productos();

        public delegate void UpdateDelegte(object sender, UpdateEventArgs args);
        public event UpdateDelegte UpdateEventHandler;

        public class UpdateEventArgs : EventArgs
        {
            public string Data { get; set; }
        }

        protected void Agregar()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            UpdateEventHandler.Invoke(this, args);
        }

        private void FrmAgregarProducto_Load(object sender, EventArgs e)
        {
            GenerarCodigo();
        }

        private void GenerarCodigo()
        {
            txtCodProducto.Text = "PROD" + Procedimientos.GenerarCodigo("Productos");
        }

        private void txtNomProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtDesProducto.Focus();
                e.Handled = true;
            }
        }

        private void txtDesProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtPresentacion.Focus();
                e.Handled = true;
            }
        }

        private void txtPresentacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtCostoUnitario.Focus();
                e.Handled = true;
            }
        }

        private void txtCostoUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtPrecioVenta.Focus();
                e.Handled = true;
            }

            // Permitir solo dígitos (números) y el carácter de control Backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCostoUnitario_Leave(object sender, EventArgs e)
        {
            Procedimientos.FormatoMoneda(txtCostoUnitario);
        }

        private void txtPrecioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                cboTipoCargo.Focus();
                e.Handled = true;
            }
        }

        private void txtPrecioVenta_Leave(object sender, EventArgs e)
        {
            Procedimientos.FormatoMoneda(txtPrecioVenta);
        }

        private void cboTipoCargo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnGuardar.Focus();
                e.Handled = true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        public override bool Guardar()
        {
            try
            {
                if (txtCodProducto.Text == string.Empty || txtNomProducto.Text == string.Empty || txtDesProducto.Text == string.Empty ||
                    txtPresentacion.Text == string.Empty || txtCostoUnitario.Text == string.Empty || txtPrecioVenta.Text == string.Empty ||
                    cboTipoCargo.Text == string.Empty)
                {
                    MessageBox.Show("Debe completar todos los campos", "Agregar Producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    Producto.Codigo = txtCodProducto.Text.Trim();
                    Producto.Nombre = txtNomProducto.Text.Trim();
                    Producto.Descripcion = txtDesProducto.Text.Trim();
                    Producto.Presentacion = txtPresentacion.Text.Trim();
                    Producto.Costo_Unitario = Convert.ToDecimal(txtCostoUnitario.Text.Trim());
                    Producto.Precio_Venta = Convert.ToDecimal(txtPrecioVenta.Text.Trim());
                    Producto.Tipo_Cargo = cboTipoCargo.Text.Trim();

                    Productos.AgregarProducto(Producto);
                    MessageBox.Show("El producto fue agregado correctamente", "Agregar Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Procedimientos.LimpiarControles(this);
                    GenerarCodigo();
                    txtNomProducto.Focus();
                    Agregar();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("El producto no fue agregado por: " + ex.Message, "Agregar Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
    }
}
