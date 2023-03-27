using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TPFinal_MarcosCongregado
{
    class EmpleadosConexion
    {

        public List<Empleados> ListaEmpleados()
        {

           List<Empleados> Lista = new List<Empleados>();

            SqlConnection conexion = new SqlConnection("data source=DESKTOP-P7GMF2F; initial catalog=EMPLEADOS2_DB; integrated security=sspi");

            string sql = "select ID, NombreCompleto, DNI, Edad, Casado, Salario from EMPLEADOS2";

            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataReader lector;

            conexion.Open();
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                  Empleados aux = new Empleados();
                  aux.ID = lector.GetInt32(0);
                  aux.NombreCompleto = lector.GetString(1);
                  aux.DNI = lector.GetString(2);
                  aux.Edad = lector.GetInt32(3);
                  aux.Casado = lector.GetBoolean(4);
                  aux.Salario = lector.GetDecimal(5);

                  Lista.Add(aux);
            }
            conexion.Close();

            return Lista;
        }
     }
}
