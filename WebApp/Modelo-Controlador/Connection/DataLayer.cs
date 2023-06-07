using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApp.Modelo_Controlador.Connection
{
    public class DataLayer
    {
        DataSet dsReturn;

        public DataSet DsReturn { get => dsReturn; set => dsReturn = value; }
    }
}