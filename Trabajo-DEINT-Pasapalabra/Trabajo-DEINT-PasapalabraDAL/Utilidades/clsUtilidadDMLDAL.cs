using System;
using System.Collections.Generic;
using System.Text;

namespace Trabajo_DEINT_PasapalabraDAL.Utilidades
{
    /// <summary>
    /// Contiene los atributos y métodos que SIEMPRE usaremos en las clases que ejecute sentencias DML, Insert, Update, Delete ...
    /// </summary>
    public abstract class clsUtilidadDMLDAL : clsUtilidadBaseDAL
    {
        //NOTA: Dichos métodos sobre esta clase no controlan ninguna SqlException ya que lo lanzan para
        //que se encarguen el método que lo llama

        #region metodos publicos
        /// <summary>
        /// <b>Prototipo:</b> public static int ejecutarSentenciaDML(String sentenciaDML)<br/>
        /// <b>Comentarios:</b> Ejecuta una sentencia DML<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Mediante las propiedades heredadas y una sentenciaDML, ejecuta dicha sentencia y devolviendo
        /// el numero de filas afectado
        /// </summary>
        /// <param name="sentenciaDML"></param>
        /// <returns> int representando el número de filas afectadas por dicha sentenciaDML</returns>
        public static int ejecutarSentenciaDML(String sentenciaDML)
        {
            MiComando.CommandText = sentenciaDML;
            MiComando.Connection = MiConexion.Conexion;
            return MiComando.ExecuteNonQuery();
        }

        /// <summary>
        /// <b>Prototipo:</b> public static int ejecutarSentenciaDMLCondicion(String sentenciaDML, int condicion)<br/>
        /// <b>Comentarios:</b> Ejecuta una sentenciaDML DML con una condición, siendo esta normalmente una PK<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Mediante las propiedades heredadas y una sentenciaDML sql con una condición,
        /// añade dicho parámetro y ejecuta la sentenciaDML completa, al final, devuelve el numero de filas afectado
        /// </summary>
        /// <param name="sentenciaDML"></param>
        /// <param name="condicion"></param>
        /// <returns> int representando el número de filas afectadas por dicha sentenciaDML</returns>
        public static int ejecutarSentenciaDMLCondicion(String sentenciaDML, int condicion)
        {
            MiComando.Parameters.Add("@param", System.Data.SqlDbType.Int).Value = condicion;
            MiComando.CommandText = sentenciaDML + "@param";
            MiComando.Connection = MiConexion.Conexion;
            return MiComando.ExecuteNonQuery();
        }

        /// /// <summary>
        /// <b>Prototipo:</b> public static int ejecutarProcedimientoAlmacenado(String procedimiento)<br/>
        /// <b>Comentarios:</b> Ejecuta un procedimiento almacenado<br/>
        /// <b>Precondiciones:</b> si el SP tiene variables, estas ya deben haber sido instanciadas en MiComando<br/>
        /// <b>Postcondiciones:</b> Mediante las propiedades heredadas y un procedimiento pasado por parámetro
        /// ejecuta dicho SP, al final, devuelve el numero de filas afectado
        /// </summary>
        /// <param name="procedimiento"></param>
        /// <returns></returns>
        /// <returns> int representando el número de filas afectadas por dicha sentenciaDML</returns>
        public static int ejecutarProcedimientoAlmacenado(String procedimiento)
        {
            MiComando.CommandType = System.Data.CommandType.StoredProcedure;
            MiComando.CommandText = procedimiento;
            MiComando.Connection = MiConexion.Conexion;
            return MiComando.ExecuteNonQuery();

        }
        #endregion
    }
}
