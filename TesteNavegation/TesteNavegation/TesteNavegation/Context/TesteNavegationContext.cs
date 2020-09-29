using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TesteNavegation.Interface;
using TesteNavegation.Models;
using Xamarin.Forms;

namespace TesteNavegation.Context
{
   public class TesteNavegationContext
    {

        private static TesteNavegationContext _Lazy;

        private static SQLiteConnection _SqliteConnection;
        
        public static TesteNavegationContext Current 
        { 
            get
            {
                if(_Lazy == null)
                    _Lazy = new TesteNavegationContext();

                return _Lazy;
            } 
        }

        private TesteNavegationContext()
        {
            _SqliteConnection = new SQLiteConnection(DependencyService.Get<IDbPath>().GetDbPath());
            _SqliteConnection.CreateTable<Usuario>();
            _SqliteConnection.CreateTable<Endereco>();
        }

        public SQLiteConnection Conexao
        {
            get{ return _SqliteConnection; }
            set { _SqliteConnection = value; }
        }
    }
}
