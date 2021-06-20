using MySql.Data.MySqlClient;
using ProjetoAW.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProjetoAW.Repositorio
{
    public class actionsVenda
    {
        Conexao cn = new Conexao();
        public void cadastraVenda(Venda venda)
        {
            MySqlCommand cmd = new MySqlCommand("call cadastroVenda('Em Andamento', @data, @pagamento, @cli, @qtd, @valor);", cn.Conectar());
            cmd.Parameters.Add("@data", MySqlDbType.DateTime).Value = venda.dataVenda;
            cmd.Parameters.Add("@pagamento", MySqlDbType.Int32).Value = venda.codPagamento;
            cmd.Parameters.Add("@cli", MySqlDbType.Int32).Value = venda.codCli;
            cmd.Parameters.Add("@qtd", MySqlDbType.Int32).Value = venda.qtdItensVenda;
            cmd.Parameters.Add("@valor", MySqlDbType.Float).Value = venda.valorTotal;
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

        public Venda selecionaIdVenda()
        {
            Venda venda = new Venda();
            MySqlCommand cmd = new MySqlCommand("select cod_venda from Venda order by cod_venda desc limit 1", cn.Conectar());
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                venda.codVenda = Convert.ToInt32(dr["cod_venda"]);
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

        public void cadastraItemVenda(Pedido pedido)
        {
            MySqlCommand cmd = new MySqlCommand("call cadastraItemVenda(@venda, @produto, @qtd, @valor)", cn.Conectar());
            cmd.Parameters.Add("@venda", MySqlDbType.Int32).Value = pedido.codVenda;
            cmd.Parameters.Add("@produto", MySqlDbType.Int32).Value = pedido.codProduto;
            cmd.Parameters.Add("@qtd", MySqlDbType.Int32).Value = pedido.quantidadePedido;
            cmd.Parameters.Add("@valor", MySqlDbType.Float).Value = pedido.valorProduto;

            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }

        public List<SelectListItem> consultaPagamentos()
        {
            List<SelectListItem> pagamentos = new List<SelectListItem>();

            MySqlCommand cmd = new MySqlCommand("select * from pagamento", cn.Conectar());
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                pagamentos.Add(new SelectListItem{
                    Text = dr["tipo_pagamento"].ToString(),
                    Value = dr["cod_pagamento"].ToString(),
                });
            }

            cn.Desconectar();

            return pagamentos;
        }

    }
}