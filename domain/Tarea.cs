using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain
{
    public class Tarea
    {
        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Descripcion { get; private set; }
        public bool Completada { get; private set; }
        public DateTime FechaCreacion { get; private set; }

        //Traer información de la base de datos
        public Tarea(int id, string titulo, string descripcion, bool completada ,DateTime fecha)
        {
            Id = id;
            Titulo = titulo;
            Descripcion = descripcion;
            Completada = completada;
            FechaCreacion = fecha;
        }
        //Creamos una nueva tarea
        public Tarea(string titulo, string descripcion)
        {
            Titulo = ValidarContenido(titulo);
            Descripcion = ValidarContenido(descripcion);
            FechaCreacion = DateTime.Now;
            Completada = false;
        }

        public void CambiarTitulo(string nuevoTitulo)
        {
            Titulo = ValidarContenido(nuevoTitulo);
        }
        public void CambiarDescripcion(string nuevaDescripcion)
        {
            Descripcion = ValidarContenido(nuevaDescripcion);
        }
        private string ValidarContenido(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("Error no puede estar vacío el contenido");

            return value;
        }
        public void Completar()
        {
            Completada = true;
        }
    }
}
