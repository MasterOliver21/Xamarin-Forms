using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteNavegation.Models;
using TesteNavegation.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TesteNavegation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroUsuarioPage : ContentPage
    {
        CadastroUsuarioViewModel cadastroUsuarioViewModel;
        private Usuario _usuario;
        public CadastroUsuarioPage(Usuario usuario)
        {
            InitializeComponent();
            cadastroUsuarioViewModel = new CadastroUsuarioViewModel(this.Navigation, usuario);
            _usuario = usuario;
            BindingContext = cadastroUsuarioViewModel;
        }

        void Entry_TextCepChanged(object sender, TextChangedEventArgs e)
        {
            //var oldText = e.OldTextValue;
            var newText = e.NewTextValue;
            //App.Current.MainPage.DisplayAlert("Teste", newText, "Ok");

           if(newText.Length == 9)
            {
                cadastroUsuarioViewModel.ConsultaCep();
               
            }
            else
            {
                cadastroUsuarioViewModel.SemPreenchimento();
                cadastroUsuarioViewModel.LabelColorCep = "Black";
            }
          
           

           
            
        }

        private void Entry_TextCpfChanged(object sender, TextChangedEventArgs e)
        {

            var newText = e.NewTextValue;
            if( newText.Length == 14)
            {
               bool validacaoCpf = cadastroUsuarioViewModel.ValidacaoCpf();
                if (!validacaoCpf)
                {
                    // App.Current.MainPage.DisplayAlert("Aviso!", "CPF INVÁLIDO!", "Ok");
                    cadastroUsuarioViewModel.LabelColorCpf = "Red";
                }
                else
                {
                    //App.Current.MainPage.DisplayAlert("Aviso!", "CPF VÁLIDO!", "Ok");
                    cadastroUsuarioViewModel.LabelColorCpf = "Blue";
                }
            }
            else
            {
                cadastroUsuarioViewModel.LabelColorCpf = "Black";
            }
        }

        private void Entry_TextConfirmaSenhaChanged(object sender, TextChangedEventArgs e)
        {
            if (cadastroUsuarioViewModel.Liberado)
            {
                var newText = e.NewTextValue;
                if (newText.Length >= cadastroUsuarioViewModel.EntrySenha.Length)
                {
                    if (newText != cadastroUsuarioViewModel.EntrySenha)
                    {
                        cadastroUsuarioViewModel.BgConfirmaSenha = "Red";
                    }
                    else
                    {
                        cadastroUsuarioViewModel.BgConfirmaSenha = "Transparent";
                    }
                }
                else
                {
                    cadastroUsuarioViewModel.BgConfirmaSenha = "Transparent";
                }
            }
           
        }

        private void Entry_TextSenhaChanged(object sender, TextChangedEventArgs e)
        {
            cadastroUsuarioViewModel.Liberado = true;
        }
    }
}