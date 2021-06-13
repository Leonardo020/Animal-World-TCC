using MySql.Data.MySqlClient;
using ProjetoAW.Models;
using System;
using System.Collections.Generic;

namespace ProjetoAW.Repositorio
{
    public class actionsPedido
    {
        Conexao cn = new Conexao();
        public void cadastraPedido(Pedido pedido)
        {
            MySqlCommand cmd = new MySqlCommand("call cadastroPedido(@valor, @quantidade, @produto, @cli)", cn.Conectar());
            cmd.Parameters.Add("@valor", MySqlDbType.Float).Value = pedido.valorTotal;
            cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = pedido.quantidadePedido;
            cmd.Parameters.Add("@produto", MySqlDbType.Int32).Value = pedido.codProduto;
            cmd.Parameters.Add("@cli", MySqlDbType.Int32).Value = pedido.codCli;

            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }

        public List<Pedido> consultaPedido()
        {
            List<Pedido> pedidos = new List<Pedido>();

            MySqlCommand cmd = new MySqlCommand("", cn.Conectar());
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                pedidos.Add(new Pedido
                {
                    codPedido = Convert.ToInt32(dr["cod_pedido"]),
                    valorTotal = Convert.ToDouble(dr["valor_total"]),
                    quantidadePedido = Convert.ToInt32(dr["quantidade_pedido"]),
                    codProduto = Convert.ToInt32(dr["cod_produto"]),
                });
            }

            cn.Desconectar();

            return pedidos;
        }

        public Pedido selecionaPedidoPorId(int id)
        {
            Pedido pedido = new Pedido();
            MySqlCommand cmd = new MySqlCommand("call selecionaPedidoPorId(@id)", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                pedido.codPedido = Convert.ToInt32(dr["cod_pedido"]);
                pedido.valorTotal = Convert.ToDouble(dr["valor_total"]);
                pedido.quantidadePedido = Convert.ToInt32(dr["quantidade_pedido"]);
                pedido.codProduto = Convert.ToInt32(dr["cod_produto"]);
                pedido.codCli = Convert.ToInt32(dr["cod_cli"]);
            }

            cn.Desconectar();

            return pedido;
        }

        public void atualizaPedido(Pedido pedido)
        {
            MySqlCommand cmd = new MySqlCommand("call alterarPedido(@quantidade, @id)", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", pedido.codPedido);
            cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = pedido.quantidadePedido;
            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }

        public void excluiPedido(int id)
        {
            MySqlCommand cmd = new MySqlCommand("call excluiPedido(@id)", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }
    }
}