using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain;
using bll;

namespace ui
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }
        static void Menu()
        {
            TareaService service = new TareaService();

            Console.WriteLine("         MENU            ");

            while (true)
            {
                Console.WriteLine("===================================");
                Console.WriteLine("1. Listar tareas. " +
                    "\n2. Listar tarea por ID." +
                    "\n3. Agregar tarea." +
                    "\n4. Modificar tarea." +
                    "\n5. Modificar estado. " +
                    "\n6. Eliminar tarea" +
                    "\n7. Salir.");

                Console.WriteLine("===================================");

                Console.Write("Ingrese una opción: ");
                if (!int.TryParse(Console.ReadLine(), out int value))
                {
                    Console.WriteLine("ERROR: opción ingresada inválida. Vuelva a intentarlo");
                    continue;
                }
                Console.WriteLine("===================================");
                switch (value)
                {
                    case 1:
                        ListarTodasTareas(service);
                        break;
                    case 2:
                        ListarPorId(service);
                        break;
                    case 3:
                        AgregarTarea(service);
                        break;
                    case 4:
                        ModificarTarea(service);
                        break;
                    case 5:
                        CambiarEstado(service);
                        break;
                    case 6:
                        EliminarTarea(service);
                        break;
                    case 7:
                        return;
                    default:
                        Console.WriteLine("Opción inválida");
                        break;
                }

            }
        }
        static void ListarTodasTareas(TareaService service)
        {
            List<Tarea> tareas = service.ObtenerTodasTareas();

            foreach(var item in tareas)
            {
                string estado = item.Completada ? "Completada" : "Pendiente";

                Console.WriteLine($"ID: {item.Id}, titulo: {item.Titulo}, descripción: {item.Descripcion}," +
                   $"\nfecha creación: {item.FechaCreacion}, estado: {estado}");
            }


        }
        static void ListarPorId(TareaService service)
        {
            Console.Write("Ingrese ID: ");
            if(!int.TryParse(Console.ReadLine(), out int value))
            {
                Console.WriteLine("Error: opción inválida.");
                return;
            }

            Tarea tarea = service.ObtenerPorId(value);

            if(tarea == null)
            {
                Console.WriteLine("El ID ingresado no existe en el sistema.");
                return;
            }

            string estado = tarea.Completada ? "Completada" : "Pendiente";

            Console.WriteLine($"ID: {tarea.Id}, titulo: {tarea.Titulo}, descripción: {tarea.Descripcion}," +
                   $"\nfecha creación: {tarea.FechaCreacion}, estado: {estado}");
        }
        static void AgregarTarea(TareaService service)
        {
            string titulo = ValidarIngresoTitulo();

            string descripcion = ValidarIngresoDescripcion();

            Tarea tarea = new Tarea(titulo, descripcion);

            service.AgregarTarea(tarea);

            Console.WriteLine("Se ha agregado la actividad con éxito");

        }
        static void ModificarTarea(TareaService service)
        {
            Console.WriteLine("Lista de actividades.");
            ListarTodasTareas(service);

            Console.Write("Ingrese ID: ");
            if(!int.TryParse(Console.ReadLine(), out int id) && id <= 0)
            {
                Console.WriteLine("Error");
                return;
            }

            Console.WriteLine("Ingrese nueva información: ");
            string titulo = ValidarIngresoTitulo();
            string descripcion = ValidarIngresoDescripcion();

            service.CambiarTarea(id, titulo, descripcion);
            Console.WriteLine("La actividad fue actualizada con éxito");
            
        }
        static void CambiarEstado(TareaService service)
        {
            Console.WriteLine("Lista de actividades.");
            ListarTodasTareas(service);

            Console.Write("Ingrese ID de la actividad que quiere cambiar su estado: ");
            if (!int.TryParse(Console.ReadLine(), out int id) && id <= 0)
            {
                Console.WriteLine("Error");
                return;
            }

            bool estado = ValidarEstado();

            service.CambiarEstadoTarea(id, estado);
            Console.WriteLine("El estado ha sido modificado con éxito");

        }
        static void EliminarTarea(TareaService service)
        {
            ListarTodasTareas(service);
            try
            {
                Console.Write("Ingrese ID para eliminar tarea: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("error");
                    return;
                }

                service.EliminarTarea(id);
                Console.WriteLine("Tarea eliminada con exito");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static bool ValidarEstado()
        {
            while (true)
            {
                Console.WriteLine("Ingrese: ");
                Console.WriteLine("0. Completada - 1. Pendiente");

                Console.Write("Ingrese estado: ");
                if (!int.TryParse(Console.ReadLine(), out int estado))
                {
                    Console.WriteLine("Debe ingresar un número.");
                    continue;
                }

                if (estado == 0)
                    return true;

                if (estado == 1)
                    return false;

                Console.WriteLine("Estado inválido.");
            }
        }
        private static string ValidarIngresoTitulo() 
        {
            while (true)
            {
                Console.Write("Ingrese título de la actividad: ");
                string titulo = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(titulo))
                    return titulo;

                Console.WriteLine("Error: El título no puede estar vacío.");
            }
        }
        private static string ValidarIngresoDescripcion()
        {
            while (true)
            {
                Console.Write("Ingrese descripción de la actividad: ");
                string descripcion = Console.ReadLine();

                if(!string.IsNullOrWhiteSpace(descripcion))
                    return descripcion;

                Console.WriteLine("La descripción de la actividad no puede estar vacía");
            }
        }
    }
}
