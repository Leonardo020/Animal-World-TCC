using MySql.Data.MySqlClient;
using ProjetoAW.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProjetoAW.Repositorio
{
    public class actionsProduto
    {
        Conexao cn = new Conexao();
        List<SelectListItem> categorias = new List<SelectListItem>();
        List<SelectListItem> fornecedores = new List<SelectListItem>();
        public void cadastraProduto(Produto produto)
        {
            MySqlCommand cmd = new MySqlCommand("call cadastroProduto(@nome, @desc, @quantidade, @valor, false, true, @imagem, @fornecedor, @categoria, @especie);", cn.Conectar());
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.nomeProduto;
            cmd.Parameters.Add("@desc", MySqlDbType.VarChar).Value = produto.descProduto;
            cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.quantidadeEstoque;
            cmd.Parameters.Add("@valor", MySqlDbType.Float).Value = produto.valorUnitario;
            cmd.Parameters.Add("@imagem", MySqlDbType.VarChar).Value = produto.imagemProduto;
            cmd.Parameters.Add("@fornecedor", MySqlDbType.Int32).Value = produto.codFornecedor;
            cmd.Parameters.Add("@categoria", MySqlDbType.Int32).Value = produto.codCategoria;
            cmd.Parameters.Add("@especie", MySqlDbType.Int32).Value = produto.codEspecie;
            cmd.ExecuteNonQuery();

            cn.Desconectar();
        }

        public List<Produto> consultaProduto(string filtro = "cod_produto desc")
        {
            List<Produto> produtos = new List<Produto>();

            MySqlCommand cmd = new MySqlCommand($@"select cod_produto, nome_produto, desc_produto, quantidade_estoque, valor_unitario, imagem_produto, nome_fornecedor, nome_categoria, nome_especie, isFavorito from produto pr
                                                    inner join fornecedor frn on pr.cod_fornecedor = frn.cod_fornecedor
                                                    inner join categoria ca on pr.cod_categoria = ca.cod_categoria
                                                    inner join especie es on pr.cod_especie = es.cod_especie
                                                    order by " + filtro ?? string.Empty, cn.Conectar());
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                produtos.Add(new Produto
                {
                    codProduto = Convert.ToInt32(dr["cod_produto"]),
                    nomeProduto = dr["nome_produto"].ToString(),
                    descProduto = dr["desc_produto"].ToString(),
                    quantidadeEstoque = Convert.ToInt32(dr["quantidade_estoque"]),
                    valorUnitario = Convert.ToDecimal(dr["valor_unitario"]),
                    imagemProduto = dr["imagem_produto"].ToString(),
                    nomeFornecedor = dr["nome_fornecedor"].ToString(),
                    nomeCategoria = dr["nome_categoria"].ToString(),
                    nomeEspecie = dr["nome_especie"].ToString(),
                    isFavorite = Convert.ToBoolean(dr["isFavorito"].ToString()),
                    /*codFornecedor = Convert.ToInt32(dr["cod_fornecedor"]),
                    codCategoria = Convert.ToInt32(dr["cod_categoria"]),
                    codEspecie = Convert.ToInt32(dr["cod_especie"]),*/
                });
            }

            cn.Desconectar();

            return produtos;
        }

        public Produto selecionaProdutoPorId(int id)
        {
            Produto produto = new Produto();
            MySqlCommand cmd = new MySqlCommand("selecionaProdutoPorId(@id)", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);

            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                produto.codProduto = Convert.ToInt32(dr["cod_produto"]);
                produto.nomeProduto = dr["nome_produto"].ToString();
                produto.descProduto = dr["desc_produto"].ToString();
                produto.quantidadeEstoque = Convert.ToInt32(dr["quantidade_estoque"]);
                produto.valorUnitario = Convert.ToDecimal(dr["valor_unitario"]);
                produto.imagemProduto = dr["imagem_produto"].ToString();
                produto.nomeFornecedor = dr["nome_fornecedor"].ToString();
                produto.nomeCategoria = dr["nome_categoria"].ToString();
                produto.nomeEspecie = dr["nome_especie"].ToString();
            }

            cn.Desconectar();

            return produto;
        }

        public void atualizaProduto(Produto produto)
        {
            MySqlCommand cmd = new MySqlCommand("call alterarProduto(@nome, @desc, @quantidade, @valor, false, true, @imagem, @fornecedor, @categoria, @especie, @id)", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", produto.codProduto);
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.nomeProduto;
            cmd.Parameters.Add("@desc", MySqlDbType.VarChar).Value = produto.descProduto;
            cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.quantidadeEstoque;
            cmd.Parameters.Add("@valor", MySqlDbType.Float).Value = produto.valorUnitario;
            cmd.Parameters.Add("@imagem", MySqlDbType.VarChar).Value = produto.imagemProduto;
            cmd.Parameters.Add("@fornecedor", MySqlDbType.Int32).Value = produto.codFornecedor;
            cmd.Parameters.Add("@categoria", MySqlDbType.Int32).Value = produto.codCategoria;
            cmd.Parameters.Add("@especie", MySqlDbType.Int32).Value = produto.codEspecie;
            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }

        public void excluiProduto(int id)
        {
            MySqlCommand cmd = new MySqlCommand("call excluiProduto(@id)", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }

        public List<Produto> selecionaFavoritos(int id)
        {
            List<Produto> produtos = new List<Produto>();
            MySqlCommand cmd = new MySqlCommand("call selecionaFavoritos(@cod)", cn.Conectar());
            cmd.Parameters.AddWithValue("@cod", id);

            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                produtos.Add(new Produto
                {
                    codProduto = Convert.ToInt32(dr["cod_produto"]),
                    nomeProduto = dr["nome_produto"].ToString(),
                    descProduto = dr["desc_produto"].ToString(),
                    quantidadeEstoque = Convert.ToInt32(dr["quantidade_estoque"]),
                    valorUnitario = Convert.ToDecimal(dr["valor_unitario"]),
                    imagemProduto = dr["imagem_produto"].ToString(),
                    nomeFornecedor = dr["nome_fornecedor"].ToString(),
                    nomeCategoria = dr["nome_categoria"].ToString(),
                    nomeEspecie = dr["nome_especie"].ToString(),
                    /*codFornecedor = Convert.ToInt32(dr["cod_fornecedor"]),
                    codCategoria = Convert.ToInt32(dr["cod_categoria"]),
                    codEspecie = Convert.ToInt32(dr["cod_especie"]),*/
                });
            }

            cn.Desconectar();

            return produtos;
        }

        public List<SelectListItem> carregaFornecedores()
        {
            MySqlCommand cmd = new MySqlCommand("select * from fornecedor", cn.Conectar());
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                fornecedores.Add(new SelectListItem
                {
                    Text = dr["nome_fornecedor"].ToString(),
                    Value = dr["cod_fornecedor"].ToString(),
                });
            }

            cn.Desconectar();

            return fornecedores;
        }

        public List<Fornecedor> consultaFornecedores()
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();
            MySqlCommand cmd = new MySqlCommand("select * from fornecedor", cn.Conectar());
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                fornecedores.Add(new Fornecedor
                {
                    codFornecedor = Convert.ToInt32(dr["cod_fornecedor"]),
                    nomeFornecedor = dr["nome_fornecedor"].ToString(),
                    emailFornecedor = dr["email_fornecedor"].ToString(),
                    imagemFornecedor = dr["imagem_fornecedor"].ToString(),
                });
            }

            return fornecedores;
        }

        public List<SelectListItem> carregaCategorias()
        {
            MySqlCommand cmd = new MySqlCommand("select * from categoria", cn.Conectar());
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                categorias.Add(new SelectListItem
                {
                    Text = dr["nome_categoria"].ToString(),
                    Value = dr["cod_categoria"].ToString(),
                });
            }

            cn.Desconectar();

            return categorias;
        }

        public void cadastraDesconto(Desconto desc)
        {
            MySqlCommand cmd = new MySqlCommand("call cadastroDesconto(@desc, 0);", cn.Conectar());
            cmd.Parameters.Add("@desc", MySqlDbType.Float).Value = desc.desconto;
        }

        public List<Produto> consultaProdutoPorCategoria(int id)
        {
            List<Produto> produtos = new List<Produto>();
            MySqlCommand cmd = new MySqlCommand("call consultaProdutoPorCategoria(@id);", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                produtos.Add(new Produto
                {
                    codProduto = Convert.ToInt32(dr["cod_produto"]),
                    nomeProduto = dr["nome_produto"].ToString(),
                    descProduto = dr["desc_produto"].ToString(),
                    quantidadeEstoque = Convert.ToInt32(dr["quantidade_estoque"]),
                    valorUnitario = Convert.ToDecimal(dr["valor_unitario"]),
                    imagemProduto = dr["imagem_produto"].ToString(),
                    nomeFornecedor = dr["nome_fornecedor"].ToString(),
                    nomeCategoria = dr["nome_categoria"].ToString(),
                    nomeEspecie = dr["nome_especie"].ToString(),
                });
            }

            cn.Desconectar();

            return produtos;
        }

        public List<Produto> consultaProdutoPorNome(string field)
        {
            List<Produto> produtos = new List<Produto>();
            MySqlCommand cmd = new MySqlCommand("call consultaProdutoPorNome(@nome);", cn.Conectar());
            cmd.Parameters.AddWithValue("@nome", field);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                produtos.Add(new Produto
                {
                    codProduto = Convert.ToInt32(dr["cod_produto"]),
                    nomeProduto = dr["nome_produto"].ToString(),
                    descProduto = dr["desc_produto"].ToString(),
                    quantidadeEstoque = Convert.ToInt32(dr["quantidade_estoque"]),
                    valorUnitario = Convert.ToDecimal(dr["valor_unitario"]),
                    imagemProduto = dr["imagem_produto"].ToString(),
                    nomeFornecedor = dr["nome_fornecedor"].ToString(),
                    nomeCategoria = dr["nome_categoria"].ToString(),
                    nomeEspecie = dr["nome_especie"].ToString(),
                });
            }

            cn.Desconectar();

            return produtos;
        }
    }
}