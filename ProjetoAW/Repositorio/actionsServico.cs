using MySql.Data.MySqlClient;
using ProjetoAW.Models;
using System;
using System.Collections.Generic;

namespace ProjetoAW.Repositorio
{
    public class actionsServico
    {
        Conexao cn = new Conexao();
        public void cadastraServico(Servico servico)
        {
            MySqlCommand cmd = new MySqlCommand("call cadastroServico(@nome, @valor, @imagem, @horario, @desc)", cn.Conectar());
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = servico.nomeServico;
            cmd.Parameters.Add("@valor", MySqlDbType.Float).Value = servico.valorServico;
            cmd.Parameters.Add("@imagem", MySqlDbType.VarChar).Value = servico.imagemServico;
            cmd.Parameters.Add("@horario", MySqlDbType.VarChar).Value = servico.horaServico;
            cmd.Parameters.Add("@desc", MySqlDbType.VarChar).Value = servico.descServico;

            cmd.ExecuteNonQuery();

            cn.Desconectar();
        }

        public List<Servico> consultaServico()
        {
            List<Servico> servicos = new List<Servico>();

            MySqlCommand cmd = new MySqlCommand("call consultaServico();", cn.Conectar());
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                servicos.Add(new Servico
                {
                    codServico = Convert.ToInt32(dr["cod_servico"]),
                    nomeServico = dr["nome_servico"].ToString(),
                    valorServico = Convert.ToDecimal(dr["valor_servico"]),
                    imagemServico = dr["imagem_servico"].ToString(),
                    horaServico = dr["horario_servico"].ToString(),
                    descServico = dr["desc_servico"].ToString(),
                });
            }
            cn.Desconectar();

            return servicos;
        }

        public Servico selecionaServicoPorId(int id)
        {
            Servico servico = new Servico();
            MySqlCommand cmd = new MySqlCommand("selecionaServicoPorId(@id);", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                servico.codServico = Convert.ToInt32(dr["cod_servico"]);
                servico.nomeServico = dr["nome_servico"].ToString();
                servico.valorServico = Convert.ToDecimal(dr["valor_servico"]);
                servico.imagemServico = dr["imagem_servico"].ToString();
                servico.descServico = dr["desc_servico"].ToString();
                servico.horaServico = dr["horario_servico"].ToString();
            }

            cn.Desconectar();

            return servico;
        }

        public void atualizaServico(Servico servico)
        {
            MySqlCommand cmd = new MySqlCommand("call alterarServico(@id, @nome, @valor, @imagem, @horario, @desc);", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", servico.codServico);
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = servico.nomeServico;
            cmd.Parameters.Add("@valor", MySqlDbType.Float).Value = servico.valorServico;
            cmd.Parameters.Add("@imagem", MySqlDbType.VarChar).Value = servico.imagemServico;
            cmd.Parameters.Add("@horario", MySqlDbType.VarChar).Value = servico.horaServico;
            cmd.Parameters.Add("@desc", MySqlDbType.VarChar).Value = servico.descServico;
            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }

        public void excluiServico(int id)
        {
            MySqlCommand cmd = new MySqlCommand("call deletaServico(@id);", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }

        public List<Servico> consultaLixeira()
        {
            List<Servico> servicos = new List<Servico>();

            MySqlCommand cmd = new MySqlCommand("select * from ArquivoMortoServico;", cn.Conectar());
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                servicos.Add(new Servico
                {
                    codServico = Convert.ToInt32(dr["cod_servico"]),
                    nomeServico = dr["nome_servico"].ToString(),
                    valorServico = Convert.ToDecimal(dr["valor_servico"]),
                    imagemServico = dr["imagem_servico"].ToString(),
                    horaServico = dr["horario_servico"].ToString(),
                    descServico = dr["desc_servico"].ToString(),
                });
            }
            cn.Desconectar();

            return servicos;
        }

        public Servico selecionaServicoLixeira(int id)
        {
            Servico servico = new Servico();
            MySqlCommand cmd = new MySqlCommand("select * from ArquivoMortoServico where cod_servico = @id", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);

            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                servico.codServico = Convert.ToInt32(dr["cod_servico"]);
                servico.nomeServico = dr["nome_servico"].ToString();
                servico.valorServico = Convert.ToDecimal(dr["valor_servico"]);
                servico.imagemServico = dr["imagem_Servico"].ToString();
                servico.horaServico = dr["horario_servico"].ToString();
                servico.descServico = dr["desc_servico"].ToString();
            }

            cn.Desconectar();

            return servico;
        }

        public void excluiServicoLixeira(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from ArquivoMortoServico where cod_servico = @id", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }
    }
}
