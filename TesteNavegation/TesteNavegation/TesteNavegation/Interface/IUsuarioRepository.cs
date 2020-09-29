using System;
using System.Collections.Generic;
using System.Text;
using TesteNavegation.Models;

namespace TesteNavegation.Interface
{
    public interface IUsuarioRepository
    {
        Usuario GetUsuario(Guid Id);

        List<Usuario> GetUsuarios();

        Usuario GetUsuarioSenha(string login, string senha);

        void InsertUsuario(Usuario usuario);

        void ReplaceUsuario(Usuario usuario);
      
        void DeleteUsuario(Usuario usuario);

        List<Usuario> GetUsuariosByName(string nome);

        List<Usuario> GetUsuariosByLogin(string login);
    }
}
