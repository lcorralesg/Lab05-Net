using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab05
{
    /// <summary>
    /// Lógica de interacción para InsertEmp.xaml
    /// </summary>
    public partial class InsertEmp : Window
    {
        public InsertEmp()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=(localdb)\\local;Database=Neptuno;Integrated Security=true;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("USP_InsertarEmpleado", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Apellidos", txtApellidos.Text);
                    command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    command.Parameters.AddWithValue("@Cargo", txtCargo.Text);
                    command.Parameters.AddWithValue("@Tratamiento", txtTratamiento.Text);
                    command.Parameters.AddWithValue("@FechaNacimiento", dpFechaNacimiento.Text);
                    command.Parameters.AddWithValue("@FechaContratacion", dpFechaContratacion.Text);
                    command.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                    command.Parameters.AddWithValue("@Ciudad", txtCiudad.Text);
                    command.Parameters.AddWithValue("@Region", txtRegion.Text);
                    command.Parameters.AddWithValue("@CodigoPostal", txtCodigoPostal.Text);
                    command.Parameters.AddWithValue("@Pais", txtPais.Text);
                    command.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                    command.Parameters.AddWithValue("@Extension", txtExtension.Text);
                    command.Parameters.AddWithValue("@Notas", txtNotas.Text);
                    command.Parameters.AddWithValue("@Jefe", txtJefe.Text);
                    command.Parameters.AddWithValue("@SueldoBasico", txtSueldoBasico.Text);
                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Empleado insertado correctamente");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
    }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
