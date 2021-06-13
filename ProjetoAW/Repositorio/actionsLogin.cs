using MySql.Data.MySqlClient;
using ProjetoAW.Models;
using System;

namespace ProjetoAW.Repositorio
{
    public class actionsLogin
    {
        Conexao cn = new Conexao();

        public void testaLogin(Login login)
        {
            MySqlCommand cmd = new MySqlCommand("select * from Login where usuario_login = @usuario and senha_login = @senha", cn.Conectar());
            cmd.Parameters.AddWithValue("@usuario", login.usuarioLogin);
            cmd.Parameters.AddWithValue("@senha", login.senhaLogin);

            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    login.usuarioLogin = dr["usuario_login"].ToString();
                    login.senhaLogin = dr["senha_login"].ToString();
                    login.nivelLogin = Convert.ToInt32(dr["nivel"]);
                }

            }

            else
            {
                login.usuarioLogin = null;
                login.senhaLogin = null;
                login.nivelLogin = Convert.ToInt32(null);
            }

            cn.Desconectar();
        }

        public void identificaUsuario(Login login)
        {

            MySqlCommand cmd = new MySqlCommand(@"select usuario_login, senha_login, nivel, cod_cli, nome_cli, cod_func, nome_func, cod_gerente, nome_gerente from login lg
                left join cliente cl on lg.usuario_login = cl.usuario_cli and lg.senha_login = cl.senha_cli
                left join funcionario fn on lg.usuario_login = fn.usuario_func and lg.senha_login = fn.senha_func
                left join gerente gr on lg.usuario_login = gr.usuario_gerente and lg.senha_login = gr.senha_gerente 
                where usuario_login = @usuario and senha_login = @senha;", cn.Conectar());
            cmd.Parameters.AddWithValue("@usuario", login.usuarioLogin);
            cmd.Parameters.AddWithValue("@senha", login.senhaLogin);

            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (login.nivelLogin == 2)
                {
                    login.codCli = Convert.ToInt32(dr["cod_cli"]);
                    login.nomeCli = dr["nome_cli"].ToString();
                }

                else if (login.nivelLogin == 1)
                {
                    login.codFunc = Convert.ToInt32(dr["cod_func"]);
                    login.nomeFunc = dr["nome_func"].ToString();
                }

                else
                {
                    login.codGerente = Convert.ToInt32(dr["cod_gerente"]);
                    login.nomeGerente = dr["nome_gerente"].ToString();
                }
            }

            cn.Desconectar();
        }
    }
}