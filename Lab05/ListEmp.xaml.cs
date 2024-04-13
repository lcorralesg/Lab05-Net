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
            string connectionString = "Server=(localdb)\\local;Database=Neptuno;Integrated Security=true;";
            string query = "USP_ListarEmpleados";

            List<Empleado> empleados = new List<Empleado>();

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Empleado empleado = new Empleado();
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
    }
}
