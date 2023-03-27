using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace TPFinal_MarcosCongregado
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        



        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre, dni, edad, salario;
            bool casado;
            nombre = tbxNombre.Text;
            dni = tbxDNI.Text;
            edad = tbxEdad.Text;
            salario = tbxSalario.Text;
            casado = rbnCasdSI.Checked;


            try
            {
            SqlConnection conexion = new SqlConnection("data source=DESKTOP-P7GMF2F; initial catalog=EMPLEADOS2_DB; integrated security=sspi");
            string query = "Insert into EMPLEADOS2 (NombreCompleto, DNI, Edad, Casado, Salario) values (@nombre,@dni,@edad,@casado,@salario)";
            conexion.Open();
            SqlCommand comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@dni", dni);
            comando.Parameters.AddWithValue("@edad", edad);
            comando.Parameters.AddWithValue("@casado", casado);
            comando.Parameters.AddWithValue("@salario", salario);
            comando.ExecuteNonQuery();
            conexion.Close();

            MessageBox.Show("Insertado");
            }
            catch
            {
                MessageBox.Show("Los datos ingresados son erroneos");
            }
            finally
            {
            tbxNombre.Clear();
            tbxDNI.Clear();
            tbxEdad.Clear();
            tbxSalario.Clear();

            }
            this.eMPLEADOS2TableAdapter.Fill(this.eMPLEADOS2_DBDataSet.EMPLEADOS2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'eMPLEADOS2_DBDataSet.EMPLEADOS2' Puede moverla o quitarla según sea necesario.
            this.eMPLEADOS2TableAdapter.Fill(this.eMPLEADOS2_DBDataSet.EMPLEADOS2);

        }

        private void button1_Click(object sender, EventArgs e)
        {
           SqlConnection conexion = new SqlConnection("data source=DESKTOP-P7GMF2F; initial catalog=EMPLEADOS2_DB; integrated security=sspi");
            
            if(dgvEmpleados.SelectedRows.Count > 0)
            {
                string nombre = "";

                foreach(DataGridViewRow row in dgvEmpleados.SelectedRows)
                {
                    nombre = row.Cells[0].Value.ToString();
                }
                conexion.Open();

                string query = "delete from EMPLEADOS2 where NombreCompleto = @nombre";
                SqlCommand comando = new SqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@nombre", nombre);
                int cant = comando.ExecuteNonQuery();
                if (cant == 1)
                    MessageBox.Show("Eliminado");
                else
                    MessageBox.Show("No  existe");
                conexion.Close();
                dgvEmpleados.Rows.Remove(dgvEmpleados.CurrentRow);
            }
            else
            {
                MessageBox.Show("Seleccionar fila completa");
            }
           
        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
            SqlConnection conexion = new SqlConnection("data source=DESKTOP-P7GMF2F; initial catalog=EMPLEADOS2_DB; integrated security=sspi");

            

            conexion.Open();
            string query = "UPDATE EMPLEADOS2 SET  DNI = @dni, Edad = @edad, Salario = @salario WHERE NombreCompleto = @nombre";
            SqlCommand comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@nombre", tbxNombre.Text);
            comando.Parameters.AddWithValue("@dni", tbxDNI.Text);
            comando.Parameters.Add("@edad", SqlDbType.Int).Value = tbxEdad.Text;
            comando.Parameters.Add("@salario", SqlDbType.Decimal).Value = tbxSalario.Text;

            int cant = comando.ExecuteNonQuery();
            if (cant == 1)
                MessageBox.Show("Modificado");
            else
                MessageBox.Show("No  existe");
            conexion.Close();
            this.eMPLEADOS2TableAdapter.Fill(this.eMPLEADOS2_DBDataSet.EMPLEADOS2);

            }
            catch
            {
                MessageBox.Show("El nombre no puede estar vacio");
            }
                        
            finally
            {
            tbxNombre.Clear();
            tbxDNI.Clear();
            tbxEdad.Clear();
            tbxSalario.Clear();

            }
            



            
        }

        private void btnHabilitar_Click(object sender, EventArgs e)
        {

            tbxNombre.Enabled = true;
            tbxDNI.Enabled = true;
            tbxEdad.Enabled = true;
            tbxSalario.Enabled = true;
            rbnCasdSI.Enabled = true;
            rbnCasdNO.Enabled = true;
        }

        private void btnRecuperar_Click(object sender, EventArgs e)
        {
            try
            {
            SqlConnection conexion = new SqlConnection("data source=DESKTOP-P7GMF2F; initial catalog=EMPLEADOS2_DB; integrated security=sspi");
            conexion.Open();

            string sql = "select DNI, Edad, Casado, Salario From EMPLEADOS2 where NombreCompleto = @nombre";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nombre", tbxNombre.Text);
            SqlDataReader registro = comando.ExecuteReader();

            if (registro.Read())
            {
                tbxDNI.Text = registro["DNI"].ToString();
                tbxEdad.Text = registro["Edad"].ToString();
                if (registro["Casado"].ToString().Equals(0))
                {
                        rbnCasdNO.Checked = true;
                }
                if (registro["Casado"].ToString().Equals(true))
                {
                    rbnCasdSI.Checked = true;
                }
                tbxSalario.Text = registro["Salario"].ToString();
            }
            else
            {
                MessageBox.Show("el Nombre no existe");
            }

            conexion.Close();

            }
            catch
            {
                MessageBox.Show("Ingrese el Nombre para recuperar los datos");
            }
            
        }
    }
}
