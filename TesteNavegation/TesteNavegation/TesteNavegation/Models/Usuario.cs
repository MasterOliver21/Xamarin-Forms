using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteNavegation.Models
{
    public class Usuario
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string picture { get; set; }
        public Guid EnderecoId { get; set; }

        [Ignore]
        public Endereco endereco { get; set; }
        [Ignore]
        public string NomeFormatado
        {
            get
            {
                return $"Nome: {nome.ToUpper()}";
            }
        }
        [Ignore]
        public string UsuarioFormatado
        {
            get
            {
                return $"Usuário: {login.ToUpper()}";
            }
        }

        [Ignore]
        public string EnderecoFormatado
        {
            get
            {
                return $"Endereço: {endereco.logradouro}, {endereco.bairro}, {endereco.localidade}-{endereco.uf} ";

            }
        }

    }
}
