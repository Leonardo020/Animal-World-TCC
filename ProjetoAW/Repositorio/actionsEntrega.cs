using MySql.Data.MySqlClient;
using ProjetoAW.Models;
using System;
using System.Collections.Generic;

namespace ProjetoAW.Repositorio
{
    public class actionsEntrega
    {
        Conexao cn = new Conexao();
        public void cadastraEntrega(Entrega entrega)
        {
            MySqlCommand cmd = new MySqlCommand("call cadastroEntrega(@data, @pedido, @animal, @cliente);", cn.Conectar());
            cmd.Parameters.Add("@data", MySqlDbType.DateTime).Value = entrega.dataEntrega;
            //cmd.Parameters.Add("@valor", MySqlDbType.Float).Value = entrega.valorEntrega;
            cmd.Parameters.Add("@pedido", MySqlDbType.Int32).Value = entrega.codPedido;
            cmd.Parameters.Add("@animal", MySqlDbType.Int32).Value = entrega.codAnimal;
            cmd.Parameters.Add("@cliente", MySqlDbType.Int32).Value = entrega.codCli;

            cmd.ExecuteNonQuery();

            cn.Desconectar();
        }

        public Entrega selecionaEntregaPorId(int id)
        {
            Entrega entrega = new Entrega();
            MySqlCommand cmd = new MySqlCommand("call selecionaEntregaPorId(@id);", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                entrega.codEntrega = Convert.ToInt32(dr["cod_entrega"]);
                entrega.dataEntrega = Convert.ToDateTime(dr["data_entrega"]);
                entrega.valorEntrega = Convert.ToDecimal(dr["valor_entrega"]);
                entrega.codPedido = Convert.ToInt32(dr["cod_entrega"]);
                entrega.codAnimal = Convert.ToInt32(dr["cod_animal"]);
                entrega.codCli = Convert.ToInt32(dr["cod_cli"]);
                entrega.nomeCli = dr["nome_cli"].ToString();
                entrega.nomeAnimal = dr["nome_animal"].ToString();
            }

            cn.Desconectar();

            return entrega;
        }

        public void atualizaEntrega(Entrega entrega)
        {
            MySqlCommand cmd = new MySqlCommand("call alterarEntrega(@data, @id);", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", entrega.codEntrega);
            cmd.Parameters.Add("@data", MySqlDbType.DateTime).Value = entrega.dataEntrega;
            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }

        public void excluiEntrega(int id)
        {
            MySqlCommand cmd = new MySqlCommand("call excluiEntrega(@id);", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }
    }
}