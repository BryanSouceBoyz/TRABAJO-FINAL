using System;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace SolMedBasicConsole
{
    internal class Program
    {
        static bool RealizarLogin(out string rol, out string usuarioLogueado)

        {
            rol = "";
            usuarioLogueado = "";
            Console.WriteLine("=== SISTEMA DE CONTROL DE CITAS MÉDICAS ===");
            Console.Write("Usuario: ");
            string usuario = Console.ReadLine();
            Console.Write("Contraseña: ");
            string clave = Console.ReadLine();
            using SqlConnection conexion = Connection.ObtenerConexion();

            conexion.Open();
            string query = "SELECT Rol FROM Usuarios WHERE Usuario = @usuario AND Contraseña = @clave";
            using SqlCommand cmd = new(query, conexion);
            cmd.Parameters.AddWithValue("@usuario", usuario);
            cmd.Parameters.AddWithValue("@clave", clave);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                rol = dr["Rol"].ToString();
                usuarioLogueado = usuario;
                return true;
            }
            else
            {
                throw new Exception("Credenciales incorrectas");
            }
        }

        static void MostrarMenu(string usuario, string rol)
        {
            int opcion = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("=== MENÚ PRINCIPAL ===");
                Console.WriteLine("Bienvenido, " + usuario + " - Rol: " + rol);
                Console.WriteLine("1. Gestionar Pacientes");
                Console.WriteLine("2. Gestionar Doctores");
                Console.WriteLine("3. Gestionar Citas");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");
                int.TryParse(Console.ReadLine(), out opcion);
                switch (opcion)
                {
                    case 1:
                        GestionarPacientes();
                        break;
                    case 2:
                        GestionarDoctores();
                        break;
                    case 3:
                        GestionarCitas();
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
                if (opcion != 4)
                {
                    Console.WriteLine("Presione Enter para continuar...");
                    Console.ReadLine();
                }
            } while (opcion != 4);
        }

        static void GestionarPacientes()
        {
            int opcion = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("=== GESTIÓN DE PACIENTES ===");
                Console.WriteLine("1. Agregar Paciente");
                Console.WriteLine("2. Listar Pacientes");
                Console.WriteLine("3. Regresar al Menú Principal");
                Console.Write("Seleccione una opción: ");
                int.TryParse(Console.ReadLine(), out opcion);
                switch (opcion)
                {
                    case 1:
                        AgregarPaciente();
                        break;
                    case 2:
                        ListarPacientes();
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
                if (opcion != 3)
                {
                    Console.WriteLine("Presione Enter para continuar...");
                    Console.ReadLine();
                }
            } while (opcion != 3);
        }

        static void AgregarPaciente()
        {
            Console.Clear();
            Console.WriteLine("=== AGREGAR NUEVO PACIENTE ===");
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();
            Console.Write("Teléfono: ");
            string telefono = Console.ReadLine();
            Console.Write("Dirección: ");
            string direccion = Console.ReadLine();
            using (SqlConnection conexion = Connection.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    string query = "INSERT INTO Pacientes (Nombre, Apellido, Telefono, Direccion) VALUES (@nombre, @apellido, @telefono, @direccion)";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@apellido", apellido);
                        cmd.Parameters.AddWithValue("@telefono", telefono);
                        cmd.Parameters.AddWithValue("@direccion", direccion);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch { }
            }
        }

        static void ListarPacientes()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE PACIENTES ===");
            using (SqlConnection conexion = Connection.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT Id, Nombre, Apellido, Telefono, Direccion FROM Pacientes";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            Console.WriteLine("ID: {0} | {1} {2} | Tel: {3} | Dir: {4}",
                                dr["Id"], dr["Nombre"], dr["Apellido"], dr["Telefono"], dr["Direccion"]);
                        }
                    }
                }
                catch { }
            }
            Console.WriteLine("Presione Enter para continuar...");
            Console.ReadLine();
        }

        static void GestionarDoctores()
        {
            int opcion = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("=== GESTIÓN DE DOCTORES ===");
                Console.WriteLine("1. Agregar Doctor");
                Console.WriteLine("2. Listar Doctores");
                Console.WriteLine("3. Regresar al Menú Principal");
                Console.Write("Seleccione una opción: ");
                int.TryParse(Console.ReadLine(), out opcion);
                switch (opcion)
                {
                    case 1:
                        AgregarDoctor();
                        break;
                    case 2:
                        ListarDoctores();
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
                if (opcion != 3)
                {
                    Console.WriteLine("Presione Enter para continuar...");
                    Console.ReadLine();
                }
            } while (opcion != 3);
        }

        static void AgregarDoctor()
        {
            Console.Clear();
            Console.WriteLine("=== AGREGAR NUEVO DOCTOR ===");
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();
            Console.Write("Especialidad: ");
            string especialidad = Console.ReadLine();
            Console.Write("Teléfono: ");
            string telefono = Console.ReadLine();
            using (SqlConnection conexion = Connection.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    string query = "INSERT INTO Doctores (Nombre, Apellido, Especialidad, Telefono) VALUES (@nombre, @apellido, @especialidad, @telefono)";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@apellido", apellido);
                        cmd.Parameters.AddWithValue("@especialidad", especialidad);
                        cmd.Parameters.AddWithValue("@telefono", telefono);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch { }
            }
        }

        static void ListarDoctores()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE DOCTORES ===");
            using (SqlConnection conexion = Connection.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT Id, Nombre, Apellido, Especialidad, Telefono FROM Doctores";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            Console.WriteLine("ID: {0} | {1} {2} | Esp: {3} | Tel: {4}",
                                dr["Id"], dr["Nombre"], dr["Apellido"], dr["Especialidad"], dr["Telefono"]);
                        }
                    }
                }
                catch { }
            }
            Console.WriteLine("Presione Enter para continuar...");
            Console.ReadLine();
        }

        static void GestionarCitas()
        {
            int opcion = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("=== GESTIÓN DE CITAS ===");
                Console.WriteLine("1. Agendar Cita");
                Console.WriteLine("2. Listar Citas");
                Console.WriteLine("3. Regresar al Menú Principal");
                Console.Write("Seleccione una opción: ");
                int.TryParse(Console.ReadLine(), out opcion);
                switch (opcion)
                {
                    case 1:
                        AgendarCita();
                        break;
                    case 2:
                        ListarCitas();
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
                if (opcion != 3)
                {
                    Console.WriteLine("Presione Enter para continuar...");
                    Console.ReadLine();
                }
            } while (opcion != 3);
        }

        //TODO: Agregar Citas
        static void AgendarCita()
        {
            Console.Clear();
            Console.WriteLine("=== AGENDAR NUEVA CITA ===");

            Console.Write("ID del Paciente: ");
            if (!int.TryParse(Console.ReadLine(), out int pacienteId))
            {
                Console.WriteLine("ID de paciente inválido.");
                Console.ReadLine();
                return;
            }

            Console.Write("ID del Doctor: ");
            if (!int.TryParse(Console.ReadLine(), out int doctorId))
            {
                Console.WriteLine("ID de doctor inválido.");
                Console.ReadLine();
                return;
            }

            Console.Write("Fecha (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime fecha))
            {
                Console.WriteLine("Formato de fecha inválido.");
                Console.ReadLine();
                return;
            }

            Console.Write("Hora (HH:MM o HH:MM:SS): ");
            string horaStr = Console.ReadLine();
            if (!TimeSpan.TryParse(horaStr, out TimeSpan hora))
            {
                Console.WriteLine("Formato de hora inválido.");
                Console.ReadLine();
                return;
            }

            using (SqlConnection conexion = Connection.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    string query = @"
                INSERT INTO Citas (PacienteId, DoctorId, Fecha, Hora)
                VALUES (@pacienteId, @doctorId, @fecha, @hora)";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@pacienteId", pacienteId);
                        cmd.Parameters.AddWithValue("@doctorId", doctorId);
                        cmd.Parameters.AddWithValue("@fecha", fecha);
                        cmd.Parameters.AddWithValue("@hora", hora);
                        int filas = cmd.ExecuteNonQuery();
                        Console.WriteLine(filas == 1
                            ? "Cita agendada correctamente."
                            : "No se insertó ninguna fila.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al agendar cita: " + ex.Message);
                }
            }

            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
        }


        static void ListarCitas()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE CITAS ===");
            using (SqlConnection conexion = Connection.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    string query = @"
                SELECT 
                    c.Id,
                    p.Nombre + ' ' + p.Apellido AS Paciente,
                    d.Nombre + ' ' + d.Apellido AS Doctor,
                    c.Fecha,
                    c.Hora
                FROM Citas c
                INNER JOIN Pacientes p ON c.PacienteId = p.Id
                INNER JOIN Doctores d ON c.DoctorId = d.Id
                ORDER BY c.Fecha, c.Hora
            ";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (!dr.HasRows)
                        {
                            Console.WriteLine("No hay citas agendadas.");
                        }
                        else
                        {
                            while (dr.Read())
                            {
                                int id = dr.GetInt32(0);
                                string paciente = dr.GetString(1);
                                string doctor = dr.GetString(2);
                                DateTime fecha = dr.GetDateTime(3);
                                TimeSpan hora = dr.GetFieldValue<TimeSpan>(4);

                                Console.WriteLine(
                                    "Cita ID: {0} | Paciente: {1} | Doctor: {2} | Fecha: {3:yyyy-MM-dd} | Hora: {4:hh\\:mm}",
                                    id, paciente, doctor, fecha, hora);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al listar citas: " + ex.Message);
                }
            }
            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            try
            {
                try
                {
                    using SqlConnection conexion = Connection.ObtenerConexion();
                    conexion.Open();
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error conectando con la base de datos");
#if DEBUG
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
#endif
                    System.Environment.Exit(1);
                }
                string rol, usuario;
                if (RealizarLogin(out rol, out usuario))
                {
                    Console.WriteLine("Login exitoso. Presione Enter para continuar...");
                    Console.ReadLine();
                    MostrarMenu(usuario, rol);
                }
                else
                {
                    Console.WriteLine("Usuario o contraseña incorrectos. Presione Enter para salir...");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nOcurrio un error inesperado ---\n{0}", ex.Message);

                #if DEBUG
                    Console.WriteLine(ex.StackTrace);
                #endif

                Console.ReadLine();
            }
        }
    }
}