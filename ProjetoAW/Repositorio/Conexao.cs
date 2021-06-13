using MySql.Data.MySqlClient;
using System;

namespace ProjetoAW.Repositorio
{
    public class Conexao
    {
        MySqlConnection cn = new MySqlConnection("server=localhost; user=root; database=bdpetshop; password=0431723748");
        public static string msg;
        public MySqlConnection Conectar()
        {
            try
            {
                cn.Open();
            }

            catch (Exception e)
            {
                msg = e.Message;
            }

            return cn;
        }

        public MySqlConnection Desconectar()
        {
            try
            {
                cn.Close();
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            return cn;
        }
    }
}