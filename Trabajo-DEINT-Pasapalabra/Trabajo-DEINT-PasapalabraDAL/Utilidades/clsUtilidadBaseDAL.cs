using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Trabajo_DEINT_PasapalabraDAL.Conexion;

namespace Trabajo_DEINT_PasapalabraDAL.Utilidades
{
    /// <summary>
    /// Contiene todos los atributos y metodos que SIEMPRE se usan al trabajar con datos en la capa DAL, 
    /// como la conexion, el comando, e instanciar abrir dicha conexion 
    /// </summary>
    public abstract class clsUtilidadBaseDAL
    {
        #region propiedades publicas
        public static clsMyConnection MiConexion { get; set; }
        public static SqlCommand MiComando { get; set; }
        #endregion
        #region metodos publicos
        /// <summary>
        /// <b>Prototipo:</b> public static void instanciarConexion()<br/>
        /// <b>Comentarios:</b> Método que instancia una conexión a una BBDD<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Los atributos MiConexion y MiCommando son instanciados,luego, se abre una conexión a la BD guardada en los atributos
        /// del objeto tipo MiConexion
        /// </summary>
        public static void instanciarConexion()
        {
            MiConexion = new clsMyConnection();
            MiComando = new SqlCommand();
            MiConexion.getConnection();
        }
        #endregion
    }
}
