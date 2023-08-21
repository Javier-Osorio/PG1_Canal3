using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Modelo_Controlador.Model.Programacion
{
    public class Backup_series
    {
        private int ID_backup_serie;
        private int ID_nombre;
        private string nombre;
        private string fecha_backup;
        private string cantidad_episodio_min;
        private string cantidad_episodio_max;
        private string observaciones;
        private int ID_tipo_serie;
        private string tipo_serie;
        private int ID_casa_productora;
        private string casa_productora;
        private int ID_ubicacion;
        private string ubicacion;
        private int estado;
        private string fecha_min;
        private string fecha_max;
        private string fecha_busqueda;

        public int ID_backup_serie1 { get => ID_backup_serie; set => ID_backup_serie = value; }
        public int ID_nombre1 { get => ID_nombre; set => ID_nombre = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Cantidad_episodio_min { get => cantidad_episodio_min; set => cantidad_episodio_min = value; }
        public string Fecha_backup { get => fecha_backup; set => fecha_backup = value; }
        public string Cantidad_episodio_max { get => cantidad_episodio_max; set => cantidad_episodio_max = value; }
        public string Observaciones { get => observaciones; set => observaciones = value; }
        public int ID_tipo_serie1 { get => ID_tipo_serie; set => ID_tipo_serie = value; }
        public string Tipo_serie { get => tipo_serie; set => tipo_serie = value; }
        public int ID_casa_productora1 { get => ID_casa_productora; set => ID_casa_productora = value; }
        public string Casa_productora { get => casa_productora; set => casa_productora = value; }
        public int ID_ubicacion1 { get => ID_ubicacion; set => ID_ubicacion = value; }
        public string Ubicacion { get => ubicacion; set => ubicacion = value; }
        public int Estado { get => estado; set => estado = value; }
        public string Fecha_min { get => fecha_min; set => fecha_min = value; }
        public string Fecha_max { get => fecha_max; set => fecha_max = value; }
        public string Fecha_busqueda { get => fecha_busqueda; set => fecha_busqueda = value; }
    }
}