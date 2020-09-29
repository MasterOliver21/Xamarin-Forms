using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TesteNavegation.Context;
using TesteNavegation.Interface;
using TesteNavegation.Models;

namespace TesteNavegation.Data
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private SQLiteConnection _dbContext;
        public UsuarioRepository()
        {
            _dbContext = TesteNavegationContext.Current.Conexao;
        }
        public Usuario GetUsuario(Guid id)
        {
            var retorno = _dbContext.FindWithQuery<Usuario>("select * from Usuario where Id = ?", id);
            if(retorno != null)
                retorno.endereco = new EnderecoRepository().GetEndereco(retorno.EnderecoId);

            return retorno;
        }

        public Usuario GetUsuarioSenha(string login, string senha)
        {
            var retorno = _dbContext.FindWithQuery<Usuario>("select * from Usuario where login = ? and senha = ?", login, senha);
            if(retorno != null)
            {
                return retorno;
            }
            return null;
        }

        public List<Usuario> GetUsuarios()
        {
           var retorno = _dbContext.Query<Usuario>("select * from Usuario");

            if (retorno != null)
                retorno.ForEach(u => u.endereco = new EnderecoRepository().GetEndereco(u.EnderecoId));

            return retorno;

        }
        public List<Usuario> GetUsuariosByName(string nome)
        {
            var retorno = _dbContext.Query<Usuario>("select * from Usuario where nome like '%" + nome + "%'");

            if (retorno != null)
                retorno.ForEach(u => u.endereco = new EnderecoRepository().GetEndereco(u.EnderecoId));

            return retorno;
        }

        public List<Usuario> GetUsuariosByLogin(string login)
        {
            var retorno = _dbContext.Query<Usuario>("select * from Usuario where login like '%"+ login + "%'");

            if (retorno != null)
                retorno.ForEach(u => u.endereco = new EnderecoRepository().GetEndereco(u.EnderecoId));

            return retorno;
        }

        public void InsertUsuario(Usuario usuario)
        {
            _dbContext.Insert(usuario);
        }

        public void ReplaceUsuario(Usuario usuario)
        {
            _dbContext.InsertOrReplace(usuario);
        }

        public void DeleteUsuario(Usuario usuario)
        {
            _dbContext.Delete(usuario);
        }
    }
}
