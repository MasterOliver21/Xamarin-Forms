using System;
using System.Collections.Generic;
using System.Text;
using TesteNavegation.Views;
using Xamarin.Forms;

namespace TesteNavegation.ViewModel
{
   public class UsuarioViewModel : BaseViewModel
    {
        private INavigation _navigation;
        private Command _tappedCadastrar;
        private Command _tappedListar;
        private Command _btnLogout;

        public UsuarioViewModel(INavigation navigation)
        {
            this._navigation = navigation;
        }

        public Command Tapped_Listar => _tappedListar ?? (_tappedListar = new Command(() =>
        {
            _navigation.PushAsync(new TabbedMenu());
            //App.Current.MainPage = new NavigationPage(new TabbedMenu());
        }));
        public Command Tapped_Cadastrar => _tappedCadastrar ?? (_tappedCadastrar = new Command(() =>
        {
            _navigation.PushAsync(new CadastroUsuarioPage(null));
        }));

        public Command BtnLogout => _btnLogout ?? (_btnLogout = new Command(() =>
        {
            App.Current.MainPage = new NavigationPage(new LoginPage()) { BarBackgroundColor = Color.FromHex("#DD8E39") };
        }));

    }
}
