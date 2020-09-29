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
    public partial class ListarPage : ContentPage
    {
        ListarViewModel listarView;
        public ListarPage()
        {
            InitializeComponent();
            listarView = new ListarViewModel(this.Navigation);
            BindingContext = listarView;
        }

        private void click_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var usuario = e.SelectedItem as Usuario;
            if (usuario == null)
                return;
            listarView.Decisao(usuario);
            (sender as ListView).SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            listarView = new ListarViewModel(this.Navigation);
            BindingContext = listarView;

        }
    }
}