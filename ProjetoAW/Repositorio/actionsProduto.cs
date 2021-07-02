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
            MySqlCommand cmd = new MySqlCommand("call cadastroProduto(@nome, @desc, @quantidade, @valor, true, @imagem, @fornecedor, @categoria, @especie);", cn.Conectar());
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

        public List<Produto> consultaProdutoPorRelevancia()
        {
            List<Produto> produtos = new List<Produto>();

            MySqlCommand cmd = new MySqlCommand($@"call consultaProdutoPorQtd();", cn.Conectar());
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
                    isComercializavel = Convert.ToBoolean(dr["isComercializavel"]),
                    /*isFavorite = Convert.ToBoolean(dr["isFavorite"].ToString()),*/
                    /*codFornecedor = Convert.ToInt32(dr["cod_fornecedor"]),
                    codCategoria = Convert.ToInt32(dr["cod_categoria"]),
                    codEspecie = Convert.ToInt32(dr["cod_especie"]),*/
                });
            }

            cn.Desconectar();

            /*foreach(var item in produtos)
            {
                if(item.isFavorite == null)
                {
                    item.isFavorite = false;
                }
            }*/

            return produtos;
        }

        public List<Produto> consultaProduto(string filtro = "cod_produto desc")
        {
            List<Produto> produtos = new List<Produto>();

            MySqlCommand cmd = new MySqlCommand($@"select pr.cod_produto, nome_produto, desc_produto, quantidade_estoque, valor_unitario, imagem_produto, nome_fornecedor, nome_categoria, nome_especie, isComercializavel from produto pr
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
                    isComercializavel = Convert.ToBoolean(dr["isComercializavel"])
                    /*isFavorite = Convert.ToBoolean(dr["isFavorite"].ToString()),*/
                    /*codFornecedor = Convert.ToInt32(dr["cod_fornecedor"]),
                    codCategoria = Convert.ToInt32(dr["cod_categoria"]),
                    codEspecie = Convert.ToInt32(dr["cod_especie"]),*/
                });
            }

            cn.Desconectar();

            /*foreach(var item in produtos)
            {
                if(item.isFavorite == null)
                {
                    item.isFavorite = false;
                }
            }*/

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
                produto.isComercializavel = Convert.ToBoolean(dr["isComercializavel"]);
            }

            cn.Desconectar();

            return produto;
        }

        public void atualizaProduto(Produto produto)
        {
            MySqlCommand cmd = new MySqlCommand("call alterarProduto(@id, @nome, @desc, @quantidade, @valor, @imagem)", cn.Conectar());
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
            MySqlCommand cmd = new MySqlCommand("call deletaProduto(@id)", cn.Conectar());
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

        public void cadastraFornecedor(Fornecedor fornecedor)
        {
            MySqlCommand cmd = new MySqlCommand("insert into fornecedor values(default, @nome, @email, @imagem);", cn.Conectar());
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = fornecedor.nomeFornecedor;
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = fornecedor.emailFornecedor;
            cmd.Parameters.Add("@imagem", MySqlDbType.VarChar).Value = fornecedor.imagemFornecedor;
            cmd.ExecuteNonQuery();
            cn.Desconectar();
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
            cmd.Parameters.Add("@desc", MySqlDbType.Float).Value = desc.desconto / 100;
            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }

        public List<Desconto> consultaDescontos()
        {
            List<Desconto> descontos = new List<Desconto>();

            MySqlCommand cmd = new MySqlCommand("select * from desconto", cn.Conectar());
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                descontos.Add(new Desconto
                {
                    codDesconto = Convert.ToInt32(dr["cod_desconto"]),
                    desconto = Convert.ToDecimal(dr["desconto"]) * 100,
                    isDesconto = Convert.ToBoolean(dr["isDesconto"]),
                }); ;
            }
            return descontos;
        }

        public Desconto verificaDesconto()
        {
            Desconto desconto = new Desconto();
            MySqlCommand cmd = new MySqlCommand("select * from desconto where isDesconto = 1", cn.Conectar());
            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    desconto.codDesconto = Convert.ToInt32(dr["cod_desconto"]);
                    desconto.desconto = Convert.ToDecimal(dr["desconto"]);
                    desconto.isDesconto = Convert.ToBoolean(dr["isDesconto"]);
                }
            }

            else
            {
                desconto = null;
            }
            cn.Desconectar();
            return desconto;
        }

        public Desconto selecionaDescontoPorId(int id)
        {
            Desconto desconto = new Desconto();
            MySqlCommand cmd = new MySqlCommand("select * from desconto where cod_desconto = @id", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                desconto.codDesconto = Convert.ToInt32(dr["cod_desconto"]);
                desconto.desconto = Convert.ToDecimal(dr["desconto"]);
                desconto.isDesconto = Convert.ToBoolean(dr["isDesconto"]);
            }

            cn.Desconectar();
            return desconto;
        }

        public void acaoDesconto(Desconto desconto)
        {
            MySqlCommand cmd = new MySqlCommand("update desconto set isDesconto = @isDesconto where cod_desconto = @id", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", desconto.codDesconto);
            cmd.Parameters.Add("@isDesconto", MySqlDbType.Int32).Value = !desconto.isDesconto;
            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }

        public void excluiDesconto(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from Desconto where cod_desconto = @id", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            cn.Desconectar();
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
                    isComercializavel = Convert.ToBoolean(dr["isComercializavel"])
                });
            }

            cn.Desconectar();

            return produtos;
        }

        public List<Produto> consultaProdutoPorEspecie(int id)
        {
            List<Produto> produtos = new List<Produto>();
            MySqlCommand cmd = new MySqlCommand("call consultaProdutoPorEspecie(@id);", cn.Conectar());
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
                    isComercializavel = Convert.ToBoolean(dr["isComercializavel"])
                });
            }

            cn.Desconectar();

            return produtos;
        }

        public List<Produto> consultaProdutoPorFornecedor(int id)
        {
            List<Produto> produtos = new List<Produto>();
            MySqlCommand cmd = new MySqlCommand("call consultaProdutoPorFornecedor(@id);", cn.Conectar());
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
                    isComercializavel = Convert.ToBoolean(dr["isComercializavel"])
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
                    isComercializavel = Convert.ToBoolean(dr["isComercializavel"])
                });
            }

            cn.Desconectar();

            return produtos;
        }

        /*public void verificaFavorito(int id, int sessao, bool isFavorite)
        {

            var produto = new Produto();
            MySqlCommand cmd = new MySqlCommand("select * from FavoritoProduto where cod_produto = @id and cod_cli = @sessao", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@sessao", sessao);

            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                isFavorite = !isFavorite;
                cn.Desconectar();
                atualizaFavorito(id, sessao, isFavorite);
            }

            else
            {
                cn.Desconectar();
                cadastraFavoritoPorCliente(id, sessao, isFavorite);
            }

            while (dr.Read())
            {
                produto.isFavorite = Convert.ToBoolean(dr["isFavorite"]);
            }

        }

        public void atualizaFavorito(int produto, int cliente, bool isFavorite)
        {
            MySqlCommand cmd = new MySqlCommand("update favoritoproduto set isFavorite = @isFavorite where cod_produto = @produto and cod_cli = @cliente", cn.Conectar());
            cmd.Parameters.AddWithValue("@produto", MySqlDbType.Int32).Value = produto;
            cmd.Parameters.AddWithValue("@cliente", MySqlDbType.Int32).Value = cliente;
            cmd.Parameters.Add("@isFavorite", MySqlDbType.Int32).Value = isFavorite;

            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }

        public void cadastraFavoritoPorCliente(int produto, int cliente, bool isFavorite)
        {
            MySqlCommand cmd = new MySqlCommand("insert into favoritoproduto values(default, @isFavorite, @produto, @cliente);", cn.Conectar());
            cmd.Parameters.Add("@isFavorite", MySqlDbType.Int32).Value = isFavorite;
            cmd.Parameters.AddWithValue("@produto", produto);
            cmd.Parameters.AddWithValue("@cliente", cliente);

            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }*/

        public List<Produto> consultaLixeira()
        {
            List<Produto> produtos = new List<Produto>();
            MySqlCommand cmd = new MySqlCommand("select * from ArquivoMortoProduto;", cn.Conectar());
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
                    codFornecedor = Convert.ToInt32(dr["cod_fornecedor"]),
                    codCategoria = Convert.ToInt32(dr["cod_categoria"]),
                    codEspecie = Convert.ToInt32(dr["cod_especie"]),
                });
            }

            return produtos;
        }

        public Produto selecionaProdutoLixeira(int id)
        {
            Produto produto = new Produto();
            MySqlCommand cmd = new MySqlCommand("select * from ArquivoMortoProduto where cod_produto = @id", cn.Conectar());
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
                produto.codFornecedor = Convert.ToInt32(dr["cod_fornecedor"]);
                produto.codCategoria = Convert.ToInt32(dr["cod_categoria"]);
                produto.codEspecie = Convert.ToInt32(dr["cod_especie"]);
                produto.isComercializavel = Convert.ToBoolean(dr["isComercializavel"]);
            }

            cn.Desconectar();

            return produto;
        }

        public void excluiProdutoLixeira(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from ArquivoMortoProduto where cod_produto = @id", cn.Conectar());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            cn.Desconectar();
        }
    }
}