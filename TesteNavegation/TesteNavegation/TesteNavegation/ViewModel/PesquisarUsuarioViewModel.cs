using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TesteNavegation.Data;
using TesteNavegation.Models;
using TesteNavegation.ViewModel;
using Xamarin.Forms;

namespace TesteNavegation.ViewModel
{
    public class PesquisarUsuarioViewModel : BaseViewModel
    { 
        private UsuarioRepository _usuarioRepository;

        private ObservableCollection<Usuario> usuarios;
        private bool _isCheckedRadioNome;
        private bool _isCheckedRadioUsuario;
        private bool _isReadNome;
        private bool _isReadUsuario;
        private string _entryNome;
        private string _entryUsuario;
        private Command _btnPesquisar;

        public PesquisarUsuarioViewModel()
        {
            Usuarios = new ObservableCollection<Usuario>();

            _usuarioRepository = new UsuarioRepository();

            IsCheckedNome = true;
            IsReadNome = true;
            IsReadUsuario = false;
        }

        public ObservableCollection<Usuario> Usuarios
        {
            get { return usuarios; }
            set
            {
                usuarios = value;
            }
        }
        public bool IsCheckedNome { get { return _isCheckedRadioNome; } set { _isCheckedRadioNome = value; OnPropertyChanged("IsCheckedNome"); } }
        public bool IsCheckedUsuario { get { return _isCheckedRadioUsuario; } set { _isCheckedRadioUsuario = value; OnPropertyChanged("IsCheckedUsuario"); } }
        public bool IsReadNome { get { return _isReadNome; } set { _isReadNome = value; OnPropertyChanged("IsReadNome"); } }
        public bool IsReadUsuario { get { return _isReadUsuario; } set { _isReadUsuario = value; OnPropertyChanged("IsReadUsuario"); } }
        public string EntryNome { get { return _entryNome; } set { _entryNome = value; OnPropertyChanged("EntryNome"); } }
        public string EntryUsuario { get { return _entryUsuario; } set { _entryUsuario = value; OnPropertyChanged("EntryUsuario"); } }

        public Command BtnPesquisar => _btnPesquisar ?? (_btnPesquisar = new Command(() =>
        {
            Usuarios.Clear();
            if(string.IsNullOrEmpty(EntryNome) && string.IsNullOrEmpty(EntryUsuario))
            {
                App.Current.MainPage.DisplayAlert("Aviso!", "Preencha um campo para pesquisa.", "Ok");
            }
            else
            {
                if (IsCheckedNome && !IsCheckedUsuario)
                {
                    var users = _usuarioRepository.GetUsuariosByName(EntryNome);
                    if(users.Count > 0)
                    {
                        foreach (var item in users)
                        {
                            Usuarios.Add(item);
                        }
                    }
                    else
                    {
                        App.Current.MainPage.DisplayAlert("Aviso!", "Nome não encontrado", "Ok");
                    }
                   

                }
                else if (IsCheckedUsuario && !IsCheckedNome)
                {
                    var users = _usuarioRepository.GetUsuariosByLogin(EntryUsuario);
                    if(users.Count > 0)
                    {
                        foreach (var item in users)
                        {
                            Usuarios.Add(item);
                        }
                    }
                    else
                    {
                        App.Current.MainPage.DisplayAlert("Aviso!", "Usuário não encontrado", "Ok");
                    }
                    
                }
            }
           
        }));
        

    }
}
