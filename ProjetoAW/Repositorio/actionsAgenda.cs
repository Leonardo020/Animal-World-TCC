using MySql.Data.MySqlClient;
using ProjetoAW.Models;
using System;
using System.Collections.Generic;

namespace ProjetoAW.Repositorio
{
    public class actionsAgenda
    {
        Conexao cn = new Conexao();

        public void verificaAgendamento(Agenda agenda)
        {
            MySqlCommand cmd = new MySqlCommand("select * from agenda where data_agenda = @data and horario_agenda = @horario", cn.Conectar());
            cmd.Parameters.Add("@data", MySqlDbType.DateTime).Value = agenda.dataAgenda;
            cmd.Parameters.Add("@horario", MySqlDbType.VarChar).Value = agenda.horarioAgenda;
            cmd.ExecuteNonQuery();

            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                agenda.confAgendamento = 1;
            }

            else
            {
                agenda.confAgendamento = 0;
            }

            cn.Desconectar();
        }

        public void realizaAgendamento(Agenda agenda)
        {
            MySqlCommand cmd = new MySqlCommand("call cadastroAgenda(@data, @horario,'Em Andamento', @servico, @cliente, @animal);", cn.Conectar());
            cmd.Parameters.Add("@data", MySqlDbType.DateTime).Value = agenda.dataAgenda;
            cmd.Parameters.Add("@horario", MySqlDbType.VarChar).Value = agenda.horarioAgenda;
            cmd.Parameters.Add("@servico", MySqlDbType.Int32).Value = agenda.codServico;
            cmd.Parameters.Add("@cliente", MySqlDbType.Int32).Value = agenda.codCli;
            cmd.Parameters.Add("@animal", MySqlDbType.Int32).Value = agenda.codAnimal;
            cmd.ExecuteNonQuery();

            cn.Desconectar();
        }

        public List<Agenda> consultaAgendamento()
        {
            List<Agenda> agendamentos = new List<Agenda>();

            MySqlCommand cmd = new MySqlCommand("call consultaAgenda();", cn.Conectar());
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                agendamentos.Add(new Agenda
                {
                    codAgenda = Convert.ToInt32(dr["cod_agenda"]),
                    dataAgenda = Convert.ToDateTime(dr["data_agenda"]),
                    horarioAgenda = dr["horario_agenda"].ToString(),
                    codServico = Convert.ToInt32(dr["cod_servico"]),
                    codAnimal = Convert.ToInt32(dr["cod_animal"]),
                    codCli = Convert.ToInt32(dr["cod_cli"]),
                    situacaoAgenda = dr["situacao_agenda"].ToString(),
                    nomeServico = dr["nome_servico"].ToString(),
                    nomeAnimal = dr["nome_animal"].ToString(),
                    nomeCli = dr["nome_cli"].ToString(),
                });
            }

            cn.Desconectar();

            return agendamentos;
        }

        /*ter lógica para que, de acordo com a situação do agendamento, apareça ou não os dados. Ex: se situacao = cancelado, não mostrar registro.*/
        public Agenda selecionaAgendamentoPorId(int id)
        {
            Agenda agenda = new Agenda();
            MySqlCommand cmd = new MySqlCommand("call selecionaAgendaPorId(@id);", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                agenda.codAgenda = Convert.ToInt32(dr["cod_agenda"]);
                agenda.dataAgenda = Convert.ToDateTime(dr["data_agenda"]);
                agenda.horarioAgenda = dr["horario_agenda"].ToString();
                agenda.codServico = Convert.ToInt32(dr["cod_servico"]);
                agenda.codAnimal = Convert.ToInt32(dr["cod_animal"]);
                agenda.codCli = Convert.ToInt32(dr["cod_cli"]);
                agenda.situacaoAgenda = dr["situacao_agenda"].ToString();
                agenda.nomeServico = dr["nome_servico"].ToString();
                agenda.nomeAnimal = dr["nome_animal"].ToString();
                agenda.nomeCli = dr["nome_cli"].ToString();
            }

            cn.Desconectar();

            return agenda;
        }

        public void atualizaAgendamento(Agenda agenda)
        {
            MySqlCommand cmd = new MySqlCommand("call alterarAgenda(@data, @horario, @situacao, @id);", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", agenda.codAgenda);
            cmd.Parameters.Add("@data", MySqlDbType.DateTime).Value = agenda.dataAgenda;
            cmd.Parameters.Add("@horario", MySqlDbType.VarChar).Value = agenda.horarioAgenda;
            cmd.Parameters.Add("@situacao", MySqlDbType.VarChar).Value = agenda.situacaoAgenda;
            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }

        /*public void excluiAgendamento(int id)
        {
            MySqlCommand cmd = new MySqlCommand("call excluiAgendamento;", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            cn.Desconectar();
        }*/

        public List<Agenda> selecionaAgendamentoPorCliente(int id)
        {
            List<Agenda> agendamentos = new List<Agenda>();

            MySqlCommand cmd = new MySqlCommand("call selecionaAgendaPorCliente(@cod);", cn.Conectar());
            cmd.Parameters.AddWithValue("@cod", id);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                agendamentos.Add(new Agenda
                {
                    codAgenda = Convert.ToInt32(dr["cod_agenda"]),
                    dataAgenda = Convert.ToDateTime(dr["data_agenda"]),
                    horarioAgenda = dr["horario_agenda"].ToString(),
                    situacaoAgenda = dr["situacao_agenda"].ToString(),
                    nomeServico = dr["nome_servico"].ToString(),
                    nomeAnimal = dr["nome_animal"].ToString(),
                    valorServico = Convert.ToDouble(dr["valor_servico"]),
                });
            }

            cn.Desconectar();

            return agendamentos;
        }

        public void cancelaAgendamento(int id)
        {
            MySqlCommand cmd = new MySqlCommand("update Agenda set situacao_agenda = 'Cancelado' where cod_agenda = @id", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }

    }
}