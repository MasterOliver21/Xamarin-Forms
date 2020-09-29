using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using TesteNavegation.Data;
using TesteNavegation.Models;
using TesteNavegation.Views;
using Xamarin.Forms;

namespace TesteNavegation.ViewModel
{
    public class ListarViewModel : BaseViewModel
    {
        private INavigation _navigation;
        private UsuarioRepository _usuarioRepository;
     
        private ObservableCollection<Usuario> usuarios;
        public ObservableCollection<Usuario> Usuarios
        {
            get { return usuarios; }
            set
            {
                usuarios = value;
            }
        }
        public ListarViewModel(INavigation navigation)
        {
            _navigation = navigation;

            Usuarios = new ObservableCollection<Usuario>();
            _usuarioRepository = new UsuarioRepository();
            foreach (var item in _usuarioRepository.GetUsuarios())
            {
                Usuarios.Add(item);
            }
        }

        public async void Decisao(Usuario user)
        {
            string op = await App.Current.MainPage.DisplayActionSheet("Escolha uma opção:", "Cancelar", "", "Deletar", "Alterar");
            if(op == "Alterar")
            {
               await _navigation.PushAsync(new CadastroUsuarioPage(user));
            }
            else if(op == "Deletar" )
            {
                 bool resp = await App.Current.MainPage.DisplayAlert("Aviso!", "Voce deseja deletar permanentemente este usuario?", "Sim", "Nao");
                if (resp)
                {
                    new Data.EnderecoRepository().DeleteEndereco(user.endereco);
                    new Data.UsuarioRepository().DeleteUsuario(user);
                    await App.Current.MainPage.DisplayAlert("Aviso!", "Usuario deletado com sucesso", "Ok");
                    await _navigation.PopAsync();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Aviso!", "Operação cancelada!", "Ok");
                }
            }
           
        }



    }
}
