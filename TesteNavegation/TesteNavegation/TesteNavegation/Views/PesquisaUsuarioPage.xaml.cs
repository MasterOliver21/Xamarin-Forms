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
    public partial class PesquisaUsuarioPage : ContentPage
    {
        PesquisarUsuarioViewModel pesquisaUsuario;
        public PesquisaUsuarioPage()
        {
           
            InitializeComponent();
            pesquisaUsuario = new PesquisarUsuarioViewModel();
            BindingContext = pesquisaUsuario;
        }

        private void UsuarioChanged(object sender, CheckedChangedEventArgs e)
        {
            if (pesquisaUsuario.IsCheckedUsuario)
            {
                pesquisaUsuario.IsReadUsuario = true;
                pesquisaUsuario.IsReadNome = false;
                pesquisaUsuario.EntryNome = "";
           }
           else
           {
               pesquisaUsuario.IsReadUsuario = false;
               pesquisaUsuario.IsReadNome = true;
               pesquisaUsuario.EntryUsuario = "";

            }
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var usuario = e.SelectedItem as Usuario;
            if (usuario == null)
                return;
            (sender as ListView).SelectedItem = null;
        }
    }
 }
