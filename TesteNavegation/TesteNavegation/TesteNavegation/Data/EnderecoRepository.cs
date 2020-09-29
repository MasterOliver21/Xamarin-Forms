using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TesteNavegation.Context;
using TesteNavegation.Interface;
using TesteNavegation.Models;

namespace TesteNavegation.Data
{
    public class EnderecoRepository :  IEnderecoRepository
    {
        private SQLiteConnection _dbContext;
        public EnderecoRepository()
        {
            _dbContext = TesteNavegationContext.Current.Conexao;
        }

        public void InsertEndereco(Endereco endereco)
        {
            _dbContext.Insert(endereco);
        }

        public Endereco GetEndereco(Guid id)
        {
            return _dbContext.FindWithQuery<Endereco>("SELECT * FROM ENDERECO WHERE ID = ?", id);
        }
        public List<Endereco> GetEnderecos()
        {
            return _dbContext.Query<Endereco>("select * from Endereco");
        }

        public void AlterarEndereco(Endereco endereco)
        {

            _dbContext.InsertOrReplace(endereco);
        }
        public void DeleteEndereco(Endereco endereco)
        {

            _dbContext.Delete(endereco);
        }
    }
}
