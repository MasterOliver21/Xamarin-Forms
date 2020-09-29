using System;
using TesteNavegation.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TesteNavegation
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new CadastroUsuarioPage(null);
            MainPage = new NavigationPage(new LoginPage()) { BarBackgroundColor = Color.FromHex("#DD8E39")};
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
