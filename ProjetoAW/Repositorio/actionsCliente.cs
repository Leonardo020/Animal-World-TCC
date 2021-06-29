using MySql.Data.MySqlClient;
using ProjetoAW.Models;
using System;
using System.Collections.Generic;

namespace ProjetoAW.Repositorio
{
    public class actionsCliente
    {
        Conexao cn = new Conexao();
        public void cadastraCliente(Cliente cli)
        {
            MySqlCommand cmd = new MySqlCommand("call cadastroCliente(@nome, @email, @cpf, @telefone, @logradouro, @numero, @bairro, @cep, @estado, @cidade, @usuario, @senha, 2);", cn.Conectar());
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cli.nomeCli;
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = cli.emailCli;
            cmd.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = cli.cpfCli;
            cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = cli.telefoneCli;
            cmd.Parameters.Add("@logradouro", MySqlDbType.VarChar).Value = cli.logradouroCli;
            cmd.Parameters.Add("@numero", MySqlDbType.Int32).Value = cli.numeroCli;
            cmd.Parameters.Add("@bairro", MySqlDbType.VarChar).Value = cli.bairroCli;
            cmd.Parameters.Add("@cep", MySqlDbType.VarChar).Value = cli.cepCli;
            cmd.Parameters.Add("@estado", MySqlDbType.VarChar).Value = cli.estadoCli;
            cmd.Parameters.Add("@cidade", MySqlDbType.VarChar).Value = cli.cidadeCli;
            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = cli.usuarioCli;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = cli.senhaCli;

            cmd.ExecuteNonQuery();

            cn.Desconectar();
        }

        public List<Cliente> consultaCliente()
        {
            List<Cliente> clientes = new List<Cliente>();

            MySqlCommand cmd = new MySqlCommand("call consultaCliente();", cn.Conectar());
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                /*formatando valores, desta forma colocando a máscara de exibição para o usuário*/
                clientes.Add(new Cliente
                {
                    codCli = Convert.ToInt32(dr["cod_cli"]),
                    nomeCli = dr["nome_cli"].ToString(),
                    emailCli = dr["email_cli"].ToString(),
                    cpfCli = Convert.ToUInt64(dr["cpf_cli"]).ToString(@"000\.000\.000\-00"),
                    telefoneCli = Convert.ToUInt64(dr["telefone"]).ToString(@"(00) 00000\-0000"),
                    logradouroCli = dr["logradouro"].ToString(),
                    numeroCli = Convert.ToInt32(dr["numero"]),
                    bairroCli = dr["bairro"].ToString(),
                    cepCli = Convert.ToUInt64(dr["cep"]).ToString(@"00000\-000"),
                    estadoCli = dr["estado"].ToString(),
                    cidadeCli = dr["cidade"].ToString(),
                    usuarioCli = dr["usuario_cli"].ToString(),
                    senhaCli = dr["senha_cli"].ToString(),
                });
            }

            cn.Desconectar();

            return clientes;
        }

        public Cliente selecionaClientePorId(int id)
        {
            Cliente cliente = new Cliente();
            MySqlCommand cmd = new MySqlCommand("call selecionaClientePorId(@id);", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cliente.codCli = Convert.ToInt32(dr["cod_cli"]);
                cliente.nomeCli = dr["nome_cli"].ToString();
                cliente.emailCli = dr["email_cli"].ToString();
                cliente.cpfCli = dr["cpf_cli"].ToString();
                cliente.telefoneCli = dr["telefone"].ToString();
                cliente.logradouroCli = dr["logradouro"].ToString();
                cliente.numeroCli = Convert.ToInt32(dr["numero"]);
                cliente.bairroCli = dr["bairro"].ToString();
                cliente.cepCli = dr["cep"].ToString();
                cliente.estadoCli = dr["estado"].ToString();
                cliente.cidadeCli = dr["cidade"].ToString();
                cliente.usuarioCli = dr["usuario_cli"].ToString();
                cliente.senhaCli = dr["senha_cli"].ToString();
            }

            cn.Desconectar();

            return cliente;
        }

        public void atualizaCliente(Cliente cli)
        {
            MySqlCommand cmd = new MySqlCommand("call alterarCliente(@id, @nome, @email, @telefone, @logradouro, @numero, @bairro, @cep, @estado, @cidade);", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", cli.codCli);
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cli.nomeCli;
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = cli.emailCli;
            cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = cli.telefoneCli;
            cmd.Parameters.Add("@logradouro", MySqlDbType.VarChar).Value = cli.logradouroCli;
            cmd.Parameters.Add("@numero", MySqlDbType.Int32).Value = cli.numeroCli;
            cmd.Parameters.Add("@bairro", MySqlDbType.VarChar).Value = cli.bairroCli;
            cmd.Parameters.Add("@cep", MySqlDbType.VarChar).Value = cli.cepCli;
            cmd.Parameters.Add("@estado", MySqlDbType.VarChar).Value = cli.estadoCli;
            cmd.Parameters.Add("@cidade", MySqlDbType.VarChar).Value = cli.cidadeCli;

            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }

        public List<Cliente> consultaClientePorNome(string search)
        {
            List<Cliente> clientes = new List<Cliente>();

            MySqlCommand cmd = new MySqlCommand("call consultaClientePorNome(@search);", cn.Conectar());
            cmd.Parameters.AddWithValue("@search", search);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                clientes.Add(new Cliente
                {
                    codCli = Convert.ToInt32(dr["cod_cli"]),
                    nomeCli = dr["nome_cli"].ToString(),
                    emailCli = dr["email_cli"].ToString(),
                    cpfCli = Convert.ToUInt64(dr["cpf_cli"]).ToString(@"000\.000\.000\-00"),
                    telefoneCli = Convert.ToUInt64(dr["telefone"]).ToString(@"(00) 00000\-0000"),
                    logradouroCli = dr["logradouro"].ToString(),
                    numeroCli = Convert.ToInt32(dr["numero"]),
                    bairroCli = dr["bairro"].ToString(),
                    cepCli = Convert.ToUInt64(dr["cep"]).ToString(@"00000\-000"),
                    estadoCli = dr["estado"].ToString(),
                    cidadeCli = dr["cidade"].ToString(),
                    usuarioCli = dr["usuario_cli"].ToString(),
                    senhaCli = dr["senha_cli"].ToString(),
                });
            }

            cn.Desconectar();

            return clientes;
        }

        public void excluiCliente(int id)
        {
            MySqlCommand cmd = new MySqlCommand("call excluiCliente(@id);", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            cn.Desconectar();
        }
    }
}