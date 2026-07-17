using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Implementations.Sql_Server;
using domain;

namespace bll
{
    public class TareaService
    {
        private TareaRepository _repository;

        public TareaService()
        {
            _repository = new TareaRepository();
        }

        public List<Tarea> ObtenerTodasTareas()
        {
            return _repository.ObtenerTodasTareas();
        }
        public Tarea ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }
        public void AgregarTarea(Tarea tarea)
        {
            _repository.AgregarTarea(tarea);
        }
        public void CambiarTarea(int id, string nuevoTitulo, string nuevaDescripcion)
        {
            Tarea tarea = _repository.ObtenerPorId(id); 

            if(tarea == null)
            {
                throw new Exception("La tarea no existe");
            }

            tarea.CambiarTitulo(nuevoTitulo);
            tarea.CambiarDescripcion(nuevaDescripcion);

            _repository.ModificarTarea(tarea);
        }
        public void CambiarEstadoTarea(int id, bool estado)
        {
            Tarea tarea = _repository.ObtenerPorId(id);

            if (tarea == null)
                throw new Exception("La tarea no existe");

            if (estado)
            {
                tarea.Completar(estado);
            }
            _repository.ModificarEstadoTarea(tarea);

        }
        public void EliminarTarea(int id)
        {
            int filasAfectadas = _repository.EliminarTarea(id);

            if (filasAfectadas == 0)
                throw new Exception("El ID ingresado no existe en el sistema.");

        }
    }
}
