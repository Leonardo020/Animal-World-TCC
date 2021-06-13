using MySql.Data.MySqlClient;
using ProjetoAW.Models;
using System;
using System.Collections.Generic;

namespace ProjetoAW.Repositorio
{
    public class actionsVenda
    {
        Conexao cn = new Conexao();
        public void cadastraVenda(Venda venda)
        {
            MySqlCommand cmd = new MySqlCommand("cadastroVenda(@situacao, @data, @pagamento, @cli, @servico, @produto)", cn.Conectar());
            cmd.Parameters.Add("@situacao", MySqlDbType.VarChar).Value = venda.situacao;
            cmd.Parameters.Add("@data", MySqlDbType.DateTime).Value = venda.dataVenda;
            cmd.Parameters.Add("@pagamento", MySqlDbType.Int32).Value = venda.codPagamento;
            cmd.Parameters.Add("@cli", MySqlDbType.Int32).Value = venda.codCli;
            cmd.Parameters.Add("@servico", MySqlDbType.Int32).Value = venda.codServico;
            cmd.Parameters.Add("@produto", MySqlDbType.Int32).Value = venda.codProduto;
            cmd.ExecuteNonQuery();

            cn.Desconectar();
        }

        public List<Venda> consultaVenda()
        {
            List<Venda> vendas = new List<Venda>();

            MySqlCommand cmd = new MySqlCommand("call consultaVenda();", cn.Conectar());
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                vendas.Add(new Venda
                {
                    codVenda = Convert.ToInt32(dr["cod_venda"]),
                    situacao = dr["situacao"].ToString(),
                    dataVenda = Convert.ToDateTime(dr["data_venda"]),
                    codPagamento = Convert.ToInt32(dr["cod_pagamento"]),
                    codCli = Convert.ToInt32(dr["cod_cli"]),
                    codServico = Convert.ToInt32(dr["cod_servico"]),
                    codProduto = Convert.ToInt32(dr["cod_Produto"]),
                });
            }

            cn.Desconectar();

            return vendas;
        }

        public Venda selecionaVendaPorId(int id)
        {
            Venda venda = new Venda();
            MySqlCommand cmd = new MySqlCommand("call selecionaVendaPorId(@id)", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                venda.codVenda = Convert.ToInt32(dr["cod_venda"]);
                venda.situacao = dr["situacao"].ToString();
                venda.dataVenda = Convert.ToDateTime(dr["data_venda"]);
                venda.codPagamento = Convert.ToInt32(dr["cod_pagamento"]);
                venda.codCli = Convert.ToInt32(dr["cod_cli"]);
                venda.codServico = Convert.ToInt32(dr["cod_servico"]);
                venda.codProduto = Convert.ToInt32(dr["cod_Produto"]);
            }

            cn.Desconectar();

            return venda;
        }

        public void atualizaVenda(Venda venda)
        {
            MySqlCommand cmd = new MySqlCommand("call alterarVenda(@situacao, @cod)", cn.Conectar());
            cmd.Parameters.Add("@situacao", MySqlDbType.VarChar).Value = venda.situacao;
            cmd.Parameters.Add("@cod", MySqlDbType.Int32).Value = venda.codVenda;
            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }

    }
}