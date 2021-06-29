using MySql.Data.MySqlClient;
using ProjetoAW.Models;
using System;
using System.Collections.Generic;

namespace ProjetoAW.Repositorio
{
    public class actionsFuncionario
    {
        Conexao cn = new Conexao();
        public void cadastraFuncionario(Funcionario func)
        {
            MySqlCommand cmd = new MySqlCommand("call cadastroFuncionario(@nome, @email, @usuario, @senha, 2)", cn.Conectar());
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = func.nomeFunc;
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = func.emailFunc;
            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = func.usuarioFunc;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = func.senhaFunc;
            cmd.ExecuteNonQuery();

            cn.Desconectar();
        }

        public List<Funcionario> consultaFuncionario()
        {
            List<Funcionario> funcionarios = new List<Funcionario>();

            MySqlCommand cmd = new MySqlCommand("call consultaFunc();", cn.Conectar());
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                funcionarios.Add(new Funcionario
                {
                    codFunc = Convert.ToInt32(dr["cod_func"]),
                    nomeFunc = dr["nome_func"].ToString(),
                    emailFunc = dr["email_func"].ToString(),
                    usuarioFunc = dr["usuario_func"].ToString(),
                    senhaFunc = dr["senha_func"].ToString(),
                });
            }

            cn.Desconectar();

            return funcionarios;
        }

        public Funcionario selecionaFuncionarioPorId(int id)
        {
            Funcionario func = new Funcionario();

            MySqlCommand cmd = new MySqlCommand("selecionaFuncionario(@id)", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                func.codFunc = Convert.ToInt32(dr["cod_func"]);
                func.nomeFunc = dr["nome_func"].ToString();
                func.emailFunc = dr["email_func"].ToString();
                func.usuarioFunc = dr["usuario_func"].ToString();
                func.senhaFunc = dr["senha_func"].ToString();
            }

            cn.Desconectar();

            return func;
        }

        public List<Funcionario> consultaFuncionarioPorNome(string search)
        {
            List<Funcionario> funcionarios = new List<Funcionario>();

            MySqlCommand cmd = new MySqlCommand("call consultaFuncPorNome(@search);", cn.Conectar());
            cmd.Parameters.AddWithValue("@search", search);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                funcionarios.Add(new Funcionario
                {
                    codFunc = Convert.ToInt32(dr["cod_func"]),
                    nomeFunc = dr["nome_func"].ToString(),
                    emailFunc = dr["email_func"].ToString(),
                    usuarioFunc = dr["usuario_func"].ToString(),
                    senhaFunc = dr["senha_func"].ToString(),
                });
            }

            cn.Desconectar();

            return funcionarios;
        }


        public void atualizaFuncionario(Funcionario func)
        {
            MySqlCommand cmd = new MySqlCommand("alterarFunc(@nome, @email, @usuario, @senha, @cod)", cn.Conectar());
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = func.nomeFunc;
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = func.emailFunc;
            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = func.usuarioFunc;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = func.senhaFunc;
            cmd.Parameters.Add("@cod", MySqlDbType.Int32).Value = func.codFunc;
            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }

        public void excluiFuncionario(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from Funcionario where cod_func = @id", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }
    }
}