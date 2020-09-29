using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using TesteNavegation.Views;
using TesteNavegation.Models;
using TesteNavegation.Data;

namespace TesteNavegation.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        #region Propriedades
        private INavigation _navigation;
        private string _entryLogin;
        private string _entrySenha;
        private string _lblValidade;
        private Command _btn_Entrar;
        private Command _novoCadastro;
        private UsuarioRepository _usuarioRepository;
        #endregion

        public LoginViewModel(INavigation navigation)
        {
            _navigation = navigation;
            _usuarioRepository = new UsuarioRepository();
         
        }
        public string EntryLogin { get { return _entryLogin; } set { _entryLogin = value; OnPropertyChanged("EntryLogin"); } }
        public string EntrySenha { get { return _entrySenha; } set { _entrySenha = value; OnPropertyChanged("EntrySenha"); } }
        public string LblValidade { get { return _lblValidade; } set { _lblValidade = value; OnPropertyChanged("LblValidade"); } }
        
        //Ao botão pressionado, faz as verificações dos Entrys.
        public  Command Btn_Entrar => _btn_Entrar ?? (_btn_Entrar = new Command(() =>
        {
            if (string.IsNullOrEmpty(EntryLogin) || string.IsNullOrEmpty(EntrySenha))
            {
                LblValidade = "Preencha os campos corretamente!";
            }
            else
            {   
                var usuario = _usuarioRepository.GetUsuarioSenha(EntryLogin, EntrySenha);
                
                if(usuario != null)
                {
                    if (EntryLogin == usuario.login && EntrySenha == usuario.senha) //Se o Entry Login e Senha forem respectivamente iguais a teste e 123
                    {
                        LblValidade = "Acesso Liberado"; // Ele escreve Usuário liberado
                        App.Current.MainPage = new NavigationPage(new UsuarioPage()) { BarBackgroundColor = Color.FromHex("#DD8E39") };

                    }
                    else
                    {
                        App.Current.MainPage.DisplayAlert("Aviso", "Usuário ou Senha incorretos.", "Ok"); //Se não, manda um alerta.
                        LblValidade = "Usuário/Senha incorretos";
                    }
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Aviso", "Usuário inexistente", "Ok"); //Se não, manda um alerta.
                    LblValidade = "Usuário não existe na base de dados.";
                }
                
            }
        }));

        //Ao apertar o botão limpar, ele esvazia todos os campos.
        public Command NovoCadastro => _novoCadastro ?? (_novoCadastro = new Command(() =>

            _navigation.PushAsync(new CadastroUsuarioPage(null))
      
        ));

    

    }
}
