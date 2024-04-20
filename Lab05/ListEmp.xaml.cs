using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Lab05
{
    public partial class ListEmp : Window
    {
        public ListEmp()
        {
            InitializeComponent();
            ListarEmpleados();
        }
        private void Button_Eliminar(object sender, RoutedEventArgs e)
        {
            Empleado empleado = (Empleado)dgEmpleados.SelectedItem;
            string query = "USP_EliminarEmpleadoLogico";

            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("USP_EliminarEmpleadoLogico", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdEmpleado", empleado.IdEmpleado);
                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Empleado eliminado correctamente");
                        ListarEmpleados();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void ListarEmpleados()
        {
            string query = "USP_ListarEmpleados";

            List<Empleado> empleados = new List<Empleado>();

            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString.connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Empleado empleado = new Empleado();
                    empleado.IdEmpleado = Convert.ToInt32(dataReader["IdEmpleado"]);
                    empleado.Apellidos = dataReader["Apellidos"].ToString();
                    empleado.Nombre = dataReader["Nombre"].ToString();
                    empleado.Cargo = dataReader["Cargo"].ToString();
                    empleado.Ciudad = dataReader["Ciudad"].ToString();
                    empleado.Region = dataReader["Region"].ToString();
                    empleados.Add(empleado);
                }
                connection.Close();

                dgEmpleados.ItemsSource = empleados;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgEmpleados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Empleado empleado = (Empleado)dgEmpleados.SelectedItem;
            if (empleado != null)
            {
                txtID.Text = empleado.IdEmpleado.ToString();
                txtApellidos.Text = empleado.Apellidos;
                txtNombre.Text = empleado.Nombre;
                txtCargo.Text = empleado.Cargo;
                txtCiudad.Text = empleado.Ciudad;
                txtRegion.Text = empleado.Region;
            }
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            string query = "USP_ActualizarEmpleado";
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdEmpleado", txtID.Text);
                    command.Parameters.AddWithValue("@Apellidos", txtApellidos.Text);
                    command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    command.Parameters.AddWithValue("@Cargo", txtCargo.Text);
                    command.Parameters.AddWithValue("@Ciudad", txtCiudad.Text);
                    command.Parameters.AddWithValue("@Region", txtRegion.Text);
                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Empleado actualizado correctamente");
                        ListarEmpleados();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
