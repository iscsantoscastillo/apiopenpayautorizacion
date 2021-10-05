using ApiOpenpayAutorizacion.DB;
using ApiOpenpayAutorizacion.Helpers;
using ApiOpenpayAutorizacion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace ApiOpenpayAutorizacion.Repository 
{
    public class AutorizacionRepoImpl : IAutorizacionRepo
    {
        /// <summary>
        /// Este procedimiento bla bla bla
        /// </summary>
        /// <param name="auto"></param>
        /// <returns></returns>
        public int autorizar(AutorizacionRequest auto)
        {
            AutorizacionRequest a = null;
            int autorizacion_no = 0;
            Conexion conexionAD = new Conexion();
            try
            {
                using (SqlConnection cnn = new SqlConnection(conexionAD.cnCadena(Constantes.BD_SOFT)))
                {
                    cnn.Open();
                    string sp = "sp_openpay_autorizacion_insert";
                    using (SqlCommand sqlCommand = new SqlCommand(sp, cnn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.Add("@FOLIO", SqlDbType.VarChar);
                        sqlCommand.Parameters["@FOLIO"].Value = auto.Folio;
                        sqlCommand.Parameters.Add("@LOCAL_DATE", SqlDbType.DateTime);
                        sqlCommand.Parameters["@LOCAL_DATE"].Value = auto.Local_date;
                        sqlCommand.Parameters.Add("@AMOUNT", SqlDbType.Decimal);
                        sqlCommand.Parameters["@AMOUNT"].Value = auto.Amount;
                        sqlCommand.Parameters.Add("@TRX_NO", SqlDbType.VarChar);
                        sqlCommand.Parameters["@TRX_NO"].Value = auto.Trx_no;

                        
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            if (sqlDataReader.HasRows)
                            {
                                
                                while (sqlDataReader.Read())
                                {
                                    
                                    autorizacion_no = Int32.Parse(sqlDataReader["autorizacion_no"].ToString());

                                }
                            }
                            else {
                                
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }


            return autorizacion_no;
        }
        /// <summary>
        /// TODO Pendiente verificar si se valida hasta el numero de transaccion (TRX_NO) en sp_openpay_autorizacion_update
        /// Y también confirmar si se validará el MONTO. -- 960 segundos = 16 minutos
        /// </summary>
        /// <param name="auto"></param>
        /// <returns></returns>
        public bool cancelar(AutorizacionRequest auto) {

            
            Conexion conexionAD = new Conexion();
            try
            {
                using (SqlConnection cnn = new SqlConnection(conexionAD.cnCadena(Constantes.BD_SOFT)))
                {
                    cnn.Open();
                    string sp = "sp_openpay_autorizacion_update";
                    using (SqlCommand sqlCommand = new SqlCommand(sp, cnn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.Add("@FOLIO", SqlDbType.VarChar);
                        sqlCommand.Parameters["@FOLIO"].Value = auto.Folio;
                        sqlCommand.Parameters.Add("@LOCAL_DATE", SqlDbType.DateTime);
                        sqlCommand.Parameters["@LOCAL_DATE"].Value = auto.Local_date;
                        sqlCommand.Parameters.Add("@AMOUNT", SqlDbType.Decimal);
                        sqlCommand.Parameters["@AMOUNT"].Value = auto.Amount;
                        sqlCommand.Parameters.Add("@TRX_NO", SqlDbType.VarChar);
                        sqlCommand.Parameters["@TRX_NO"].Value = auto.Trx_no;
                        sqlCommand.Parameters.Add("@NUM_AUT", SqlDbType.Int);
                        sqlCommand.Parameters["@NUM_AUT"].Value = auto.Authorization_number;


                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            if (sqlDataReader.HasRows)
                            {

                                while (sqlDataReader.Read())
                                {
                                    //Si "resultado" es CERO entonces NO hubo filas afectadas
                                    return Int32.Parse(sqlDataReader["resultado"].ToString()) == 0? false:true;

                                }
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }


            return false; ;
        }
    }
}
