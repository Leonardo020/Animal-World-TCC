using MySql.Data.MySqlClient;
using ProjetoAW.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProjetoAW.Repositorio
{
    public class actionsAnimal
    {
        Conexao cn = new Conexao();
        List<SelectListItem> especies = new List<SelectListItem>();
        List<SelectListItem> racas = new List<SelectListItem>();
        List<SelectListItem> portes = new List<SelectListItem>();

        public void cadastraAnimal(Animal animal)
        {
            MySqlCommand cmd = new MySqlCommand("call cadastroAnimal(@nome, @desc, @idade, @temperamento, @imagem, @sexo, @porte, @raca, @especie, @cliente);", cn.Conectar());
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = animal.nomeAnimal;
            cmd.Parameters.Add("@desc", MySqlDbType.VarChar).Value = animal.descricaoAnimal;
            cmd.Parameters.Add("@idade", MySqlDbType.Int32).Value = animal.idade;
            cmd.Parameters.Add("@temperamento", MySqlDbType.VarChar).Value = animal.temperamento;
            cmd.Parameters.Add("@imagem", MySqlDbType.VarChar).Value = animal.imagemAnimal;
            cmd.Parameters.Add("@sexo", MySqlDbType.VarChar).Value = animal.sexo;
            cmd.Parameters.Add("@porte", MySqlDbType.Int32).Value = animal.codPorte;
            cmd.Parameters.Add("@raca", MySqlDbType.Int32).Value = animal.codRaca;
            cmd.Parameters.Add("@especie", MySqlDbType.Int32).Value = animal.codEspecie;
            cmd.Parameters.Add("@cliente", MySqlDbType.Int32).Value = animal.codCli;

            cmd.ExecuteNonQuery();

            cn.Desconectar();
        }

        public List<Animal> consultaAnimal()
        {
            List<Animal> animais = new List<Animal>();

            MySqlCommand cmd = new MySqlCommand("call consultaAnimal();", cn.Conectar());
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                animais.Add(new Animal
                {
                    codAnimal = Convert.ToInt32(dr["cod_animal"]),
                    nomeAnimal = dr["nome_animal"].ToString(),
                    idade = Convert.ToInt32(dr["idade_animal"]),
                    temperamento = dr["temperamento"].ToString(),
                    imagemAnimal = dr["imagem_animal"].ToString(),
                    sexo = dr["sexo"].ToString(),
                    descricaoAnimal = dr["descricao_animal"].ToString()
                });
            }

            cn.Desconectar();

            return animais;
        }

        public List<Animal> consultaAnimalPorCliente(int codigo)
        {
            List<Animal> animais = new List<Animal>();

            MySqlCommand cmd = new MySqlCommand("call selecionaAnimalPorCliente(@cod);", cn.Conectar());
            cmd.Parameters.AddWithValue("@cod", codigo);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                animais.Add(new Animal
                {
                    codAnimal = Convert.ToInt32(dr["cod_animal"]),
                    nomeAnimal = dr["nome_animal"].ToString(),
                    idade = Convert.ToInt32(dr["idade"]),
                    temperamento = dr["temperamento"].ToString(),
                    imagemAnimal = dr["imagem_animal"].ToString(),
                    sexo = dr["sexo"].ToString(),
                    descricaoAnimal = dr["descricao"].ToString(),
                });
            }

            cn.Desconectar();

            return animais;
        }

        public Animal consultaAnimalPorId(int id)
        {
            Animal animal = new Animal();
            MySqlCommand cmd = new MySqlCommand("call selecionaAnimalPorId(@id);", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                animal.codAnimal = Convert.ToInt32(dr["cod_animal"]);
                animal.nomeAnimal = dr["nome_animal"].ToString();
                animal.idade = Convert.ToInt32(dr["idade"]);
                animal.temperamento = dr["temperamento"].ToString();
                animal.imagemAnimal = dr["imagem_animal"].ToString();
                animal.sexo = dr["sexo"].ToString();
                animal.descricaoAnimal = dr["descricao"].ToString();
                animal.nomeEspecie = dr["nome_especie"].ToString();
                animal.nomeRaca = dr["nome_raca"].ToString();
                animal.nomePorte = dr["nome_porte"].ToString();
            }

            cn.Desconectar();

            if (animal.sexo == "M")
            {
                animal.sexo = "Masculino";
            }

            else
            {
                animal.sexo = "Feminino";
            }

            return animal;

        }

        public void atualizaAnimal(Animal animal)
        {
            MySqlCommand cmd = new MySqlCommand("call alterarAnimal(@id, @nome, @desc, @idade, @imagem);", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", animal.codAnimal);
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = animal.nomeAnimal;
            cmd.Parameters.Add("@desc", MySqlDbType.VarChar).Value = animal.descricaoAnimal;
            cmd.Parameters.Add("@idade", MySqlDbType.Int32).Value = animal.idade;
            cmd.Parameters.Add("@imagem", MySqlDbType.VarChar).Value = animal.imagemAnimal;
            cmd.ExecuteNonQuery();

            cn.Desconectar();
        }

        public void excluiAnimal(int id)
        {
            MySqlCommand cmd = new MySqlCommand("call excluiAnimal(@id);", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }

        public List<SelectListItem> carregaEspecie()
        {
            MySqlCommand cmd = new MySqlCommand("select * from Especie", cn.Conectar());
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                especies.Add(new SelectListItem
                {
                    Text = dr["nome_especie"].ToString(),
                    Value = dr["cod_especie"].ToString(),
                });
            }

            cn.Desconectar();

            return especies;
        }

        public List<SelectListItem> carregaRacas()
        {
            MySqlCommand cmd = new MySqlCommand("select * from Raca", cn.Conectar());
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                racas.Add(new SelectListItem
                {
                    Text = dr["nome_raca"].ToString(),
                    Value = dr["cod_raca"].ToString(),
                });
            }
            cn.Desconectar();

            return racas;
        }

        public List<Raca> consultaRacasPorEspecie(int id)
        {
            List<Raca> raca = new List<Raca>();
            MySqlCommand cmd = new MySqlCommand("select * from Raca where cod_especie = @id", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                raca.Add(new Raca
                {
                    codRaca = Convert.ToInt32(dr["cod_raca"]),
                    codEspecie = Convert.ToInt32(dr["cod_especie"]),
                    nomeRaca = dr["nome_raca"].ToString(),
                });
            }
            cn.Desconectar();

            return raca;
        }

        public List<SelectListItem> carregaPortes()
        {
            MySqlCommand cmd = new MySqlCommand("select * from Porte", cn.Conectar());
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                portes.Add(new SelectListItem
                {
                    Text = dr["nome_porte"].ToString(),
                    Value = dr["cod_porte"].ToString(),
                });
            }

            cn.Desconectar();

            return portes;
        }

        public List<SelectListItem> carregaAnimais(int id)
        {
            List<SelectListItem> animais = new List<SelectListItem>();
            MySqlCommand cmd = new MySqlCommand("select * from animal where cod_cli = @id", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                animais.Add(new SelectListItem
                {
                    Text = dr["nome_animal"].ToString(),
                    Value = dr["cod_animal"].ToString(),
                });
            }

            return animais;
        }
    }

}