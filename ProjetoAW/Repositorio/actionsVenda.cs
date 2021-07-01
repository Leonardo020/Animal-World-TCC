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
                    codProduto = Convert.ToInt32(dr["cod_produto"]),
                });
            }

            cn.Desconectar();

            return vendas;
        }

        public List<Venda> consultaEntrega()
        {
            List<Venda> entregas = new List<Venda>();

            MySqlCommand cmd = new MySqlCommand("call consultaEntrega();", cn.Conectar());
            MySqlDataReader dr = cmd.ExecuteReader();


            while (dr.Read())
            {
                entregas.Add(new Venda
                {

                    codEntrega = Convert.ToInt32(dr["cod_entrega"]),
                    codVenda = Convert.ToInt32(dr["cod_pedido"]),
                    dataEntrega = Convert.ToDateTime(dr["data_entrega"]),
                    situacao = dr["situacao"].ToString(),
                    dataVenda = Convert.ToDateTime(dr["data_venda"]),
                    tipoPagamento = dr["tipo_pagamento"].ToString(),
                    nomeCli = dr["nome_cli"].ToString(),
                    qtdItensVenda = Convert.ToInt32(dr["qtd_total"]),
                    valorTotal = Convert.ToDecimal(dr["valor_total"]),

                });
            }
            cn.Desconectar();

            return entregas;
        }


        public int selecionaIdVenda()
        {
            var idVenda = 0;
            MySqlCommand cmd = new MySqlCommand("select cod_venda from Venda order by cod_venda desc limit 1", cn.Conectar());
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                idVenda = Convert.ToInt32(dr["cod_venda"]);
            }

            cn.Desconectar();

            return idVenda;
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
                pagamentos.Add(new SelectListItem
                {
                    Text = dr["tipo_pagamento"].ToString(),
                    Value = dr["cod_pagamento"].ToString(),
                });
            }

            cn.Desconectar();

            return pagamentos;
        }

        public List<Venda> selecionaVendaPorCliente(int id)
        {
            List<Venda> vendas = new List<Venda>();
            MySqlCommand cmd = new MySqlCommand("selecionaVendaPorCliente(@id);", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                vendas.Add(new Venda
                {
                    codVenda = Convert.ToInt32(dr["cod_venda"]),
                    situacao = dr["situacao"].ToString(),
                    qtdItensVenda = Convert.ToInt32(dr["qtd_total"]),
                    dataVenda = Convert.ToDateTime(dr["data_venda"]),
                    tipoPagamento = dr["tipo_pagamento"].ToString(),
                    valorTotal = Convert.ToDecimal(dr["valor_total"]),
                    codEntrega = Convert.ToInt32(dr["cod_entrega"]),
                    dataEntrega = Convert.ToDateTime(dr["data_entrega"]),
                });
            }

            cn.Desconectar();

            return vendas;
        }

        public List<Pedido> selecionaItensPorVenda(int id)
        {
            List<Pedido> pedidos = new List<Pedido>();
            MySqlCommand cmd = new MySqlCommand("selecionaItensVenda(@id);", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                pedidos.Add(new Pedido
                {
                    quantidadePedido = Convert.ToInt32(dr["qtd_item"]),
                    valorTotal = Convert.ToDecimal(dr["valor_item"]),
                    produto = dr["nome_produto"].ToString(),
                    imagemProduto = dr["imagem_produto"].ToString(),
                });
            }


            cn.Desconectar();

            return pedidos;
        }

        public Venda selecionaEntregaPorVenda(int id)
        {
            Venda venda = new Venda();
            MySqlCommand cmd = new MySqlCommand("consultaEntregaPorVenda(@id);", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                venda.codEntrega = Convert.ToInt32(dr["cod_entrega"]);
                venda.codVenda = Convert.ToInt32(dr["cod_pedido"]);
                venda.dataEntrega = Convert.ToDateTime(dr["data_entrega"]);
                venda.situacao = dr["situacao"].ToString();
                venda.dataVenda = Convert.ToDateTime(dr["data_venda"]);
                venda.tipoPagamento = dr["tipo_pagamento"].ToString();
                venda.nomeCli = dr["nome_cli"].ToString();
                venda.cpfCli = dr["cpf_cli"].ToString();
                venda.cep = dr["cep"].ToString();
                venda.numero = Convert.ToInt32(dr["numero"]);
                venda.logradouro = dr["logradouro"].ToString();
                venda.bairro = dr["bairro"].ToString();
                venda.estado = dr["estado"].ToString();
                venda.cidade = dr["cidade"].ToString();
                venda.qtdItensVenda = Convert.ToInt32(dr["qtd_total"]);
                venda.valorTotal = Convert.ToDecimal(dr["valor_total"]);
            }


            cn.Desconectar();

            return venda;
        }

        public void concluiVenda(int id)
        {
            MySqlCommand cmd = new MySqlCommand("update Venda set situacao = 'Concluído' where cod_venda = @id", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }

        public void cancelaVenda(int id)
        {
            MySqlCommand cmd = new MySqlCommand("update Venda set situacao = 'Cancelado' where cod_venda = @id", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }

    }
}