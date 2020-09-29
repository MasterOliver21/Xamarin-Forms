using System;
using System.Collections.Generic;
using System.Text;
using TesteNavegation.Models;

namespace TesteNavegation.Interface
{
    public interface IEnderecoRepository
    {
        void InsertEndereco(Endereco endereco);
        Endereco GetEndereco(Guid id);

        List<Endereco> GetEnderecos();

        void AlterarEndereco(Endereco endereco);
        void DeleteEndereco(Endereco endereco);
    }
}
