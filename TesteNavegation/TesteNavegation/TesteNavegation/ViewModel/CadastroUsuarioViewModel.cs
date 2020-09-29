using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TesteNavegation.Interface;
using TesteNavegation.Models;
using TesteNavegation.Services;
using TesteNavegation.Views;
using Xamarin.Forms;

namespace TesteNavegation.ViewModel
{
    public class CadastroUsuarioViewModel :  BaseViewModel
    {
        private INavigation _navigation;
        private string _entryCep;
        private string _entryRua;
        private string _entryBairro;
        private string _entryCidade;
        private string _entryEstado;
        private string _entryNome;
        private string _entryCpf;
        private string _entrySenha;
        private string _entryConfirmaSenha;
        private string _entryLogin;
        private bool _boolRua;
        private bool _boolCidade;
        private bool _boolEstado;
        private bool _boolBairro;
        private bool _boolSalvar;
        private bool _boolCancelar;
        private bool _boolvisibleSenha;
        private bool _boolvisibleConfirmaSenha;
        private bool _liberado;
        private bool _visibleUserImage;
        private bool _boxVisible;
        private bool _labelPhotoVisible;
        private string _colorCep;
        private string _colorCpf;
        private string _caminhoSenha;
        private string _caminhoConfirmaSenha;
        private string _bgSenha;
        private string _bgConfirmaSenha;
        private string _caminho;
        private ImageSource _sourcePhoto;
        private Usuario _usuario;
        private Command _btnSalvar;
        private Command _btnCancelar;
        private Command _btnLimpar;
        private Command _tappedVisibleSenha;
        private Command _tappedVisibleConfirmaSenha;
        private Command _tappedPhoto;
        private CepApiService _cep;

        public CadastroUsuarioViewModel(INavigation navigation, Usuario usuario)
        {
            _usuario = usuario;
            _navigation = navigation;
            _cep = new CepApiService();

            if (_usuario != null)
            {
                AlterarCadastro();
            }
            else
            {
                NovoCadastro();
            }
            
        }

        public string EntryCep { get { return _entryCep; } set { _entryCep = value; OnPropertyChanged("EntryCep"); } }
        public string EntryRua { get { return _entryRua; } set { _entryRua = value; OnPropertyChanged("EntryRua"); } }
        public string EntryBairro { get { return _entryBairro; } set { _entryBairro = value; OnPropertyChanged("EntryBairro"); } }
        public string EntryCidade { get { return _entryCidade; } set { _entryCidade = value; OnPropertyChanged("EntryCidade"); } }
        public string EntryEstado { get { return _entryEstado; } set { _entryEstado = value; OnPropertyChanged("EntryEstado"); } }
        public string EntryNome { get { return _entryNome; } set { _entryNome = value; OnPropertyChanged("EntryNome"); } }
        public string EntryCpf { get { return _entryCpf; } set { _entryCpf = value; OnPropertyChanged("EntryCpf"); } }
        public string EntryLogin { get { return _entryLogin; } set { _entryLogin = value; OnPropertyChanged("EntryLogin"); } }
        public string EntrySenha { get { return _entrySenha; } set { _entrySenha = value; OnPropertyChanged("EntrySenha"); } }
        public string EntryCofirmaSenha { get { return _entryConfirmaSenha; } set { _entryConfirmaSenha = value; OnPropertyChanged("EntryCofirmaSenha"); } }
        public string LabelColorCpf { get { return _colorCpf; } set { _colorCpf = value; OnPropertyChanged("LabelColorCpf"); } }
        public string LabelColorCep { get { return _colorCep; } set { _colorCep = value; OnPropertyChanged("LabelColorCep"); } }
        public string CaminhoSenha { get { return _caminhoSenha; } set { _caminhoSenha = value; OnPropertyChanged("CaminhoSenha"); } }
        public string CaminhoConfirmaSenha { get { return _caminhoConfirmaSenha; } set { _caminhoConfirmaSenha = value; OnPropertyChanged("CaminhoConfirmaSenha"); } }
        public string BgSenha { get { return _bgSenha; } set { _bgSenha = value; OnPropertyChanged("BgSenha"); } }
        public ImageSource FotoUsuario { get { return _sourcePhoto; } set { _sourcePhoto = value; OnPropertyChanged("FotoUsuario"); } }
        public string BgConfirmaSenha { get { return _bgConfirmaSenha; } set { _bgConfirmaSenha = value; OnPropertyChanged("BgConfirmaSenha"); } }
        public bool BoolRua { get { return _boolRua; } set { _boolRua = value; OnPropertyChanged("BoolRua"); } }
        public bool BoolCidade { get { return _boolCidade; } set { _boolCidade = value; OnPropertyChanged("BoolCidade"); } }
        public bool BoolEstado { get { return _boolEstado; } set { _boolEstado = value; OnPropertyChanged("BoolEstado"); } }
        public bool BoolBairro { get { return _boolBairro; } set { _boolBairro = value; OnPropertyChanged("BoolBairro"); } }
        public bool BoolSalvar { get { return _boolSalvar; } set { _boolSalvar = value; OnPropertyChanged("BoolSalvar"); } }
        public bool BoolCancelar { get { return _boolCancelar; } set { _boolCancelar = value; OnPropertyChanged("BoolCancelar"); } }
        public bool BoolVisibleSenha { get { return _boolvisibleSenha; } set { _boolvisibleSenha = value; OnPropertyChanged("BoolVisibleSenha"); } }
        public bool BoolVisibleConfirmaSenha { get { return _boolvisibleConfirmaSenha; } set { _boolvisibleConfirmaSenha = value; OnPropertyChanged("BoolVisibleConfirmaSenha"); } }
        public bool Liberado { get { return _liberado; } set { _liberado = value; OnPropertyChanged("Liberado"); } }
        public bool VisibleUserImage { get { return _visibleUserImage; } set { _visibleUserImage = value; OnPropertyChanged("VisbleUserImage"); } }
        public bool BoxVisible { get { return _boxVisible; } set { _boxVisible = value; OnPropertyChanged("BoxVisible"); } }
        public bool LabelPhotoVisible { get { return _labelPhotoVisible; } set { _labelPhotoVisible = value; OnPropertyChanged("LabelPhotoVisible"); } }


        public Command BtnSalvar => _btnSalvar ?? (_btnSalvar = new Command(async () =>
        {
            if (_usuario == null)
            {
                if (string.IsNullOrEmpty(EntryNome) || string.IsNullOrEmpty(EntryCpf) || 
                    BgConfirmaSenha == "Red" ||string.IsNullOrEmpty(EntryCofirmaSenha))
                {
                    await App.Current.MainPage.DisplayAlert("Aviso!", "Campos não preenchidos adequadamente", "Ok");
                }
                else
                {
                    if (!ValidacaoCpf() && LabelColorCep == "Red" || LabelColorCep == "Black")
                    {
                        await App.Current.MainPage.DisplayAlert("Aviso!", "CPF ou CEP INVÁLIDO(s)", "Ok");
                    }
                    else
                    {
                        var resp = await App.Current.MainPage.DisplayAlert("", "Tem certeza que deseja cadastrar este usuario?", "Sim", "Nao");
                        if (resp)
                        {
                            var endereco = new Endereco()
                            {
                                Id = Guid.NewGuid(),
                                logradouro = EntryRua,
                                bairro = EntryBairro,
                                localidade = EntryCidade,
                                uf = EntryEstado,
                                cep = EntryCep

                            };
                            new Data.EnderecoRepository().InsertEndereco(endereco);
                            var usuario = new Usuario()
                            {
                                Id = Guid.NewGuid(),
                                cpf = EntryCpf.Replace(".", "").Replace("-", ""),
                                nome = EntryNome,
                                login = EntryLogin,
                                senha = EntrySenha,
                                EnderecoId = endereco.Id,
                                picture = _caminho
                            };
                            new Data.UsuarioRepository().InsertUsuario(usuario);
                            await App.Current.MainPage.DisplayAlert("Otimo!", "Usuario Cadastrado com Sucesso!", "Ok");
                            await _navigation.PopAsync();
                        }

                        else
                        {
                            await App.Current.MainPage.DisplayAlert("", "Operação cancelada", "Ok");
                            SemPreenchimento();
                        }
                    }
                }
            }//FIM DO CÓDIGO DE INSERÇÃO DO USUÁRIO

            else
            {
                var resp = await App.Current.MainPage.DisplayAlert("", "Tem certeza que deseja alterar este usuario?", "Sim", "Nao");
                if (resp)
                {
                    if (!ValidacaoCpf() || LabelColorCep == "Red" || LabelColorCep == "Black")
                    {
                        await App.Current.MainPage.DisplayAlert("Aviso!", "CPF/CEP INVÁLIDO(S)", "Ok");
                    }
                    else
                    {
                        var endereco = new Endereco()
                        {
                            Id = _usuario.endereco.Id,
                            logradouro = EntryRua,
                            bairro = EntryBairro,
                            localidade = EntryCidade,
                            uf = EntryEstado,
                            cep = EntryCep.Replace("-", "")
                 
                        };
                        new Data.EnderecoRepository().AlterarEndereco(endereco);
                        var usuario = new Usuario()
                        {
                            Id = _usuario.Id,
                            cpf = EntryCpf,
                            nome = EntryNome,
                            EnderecoId = endereco.Id,
                            login = EntryLogin,
                            senha = EntrySenha,
                            picture = _caminho
                            
                        };
                        new Data.UsuarioRepository().ReplaceUsuario(usuario);
                        await App.Current.MainPage.DisplayAlert("Otimo!", "Usuario Alterado com Sucesso!", "Ok");
                        await _navigation.PopAsync();
                        // await _navigation.RemovePage();
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("", "Operação cancelada", "Ok");
                }
            }
            
        }));
        public Command BtnCancelar => _btnCancelar ?? (_btnCancelar = new Command(() =>
        {
            this._navigation.PopAsync();

        }));
        public Command BtnLimpar => _btnLimpar ?? (_btnLimpar = new Command(() =>
        {
            EntryNome = "";
            EntryCpf = "";
            EntryRua = "";
            EntryCidade = "";
            EntryEstado = "";
            EntryBairro = "";
            EntryCep = "";
            EntryLogin = "";
            EntryCofirmaSenha = "";
            EntrySenha = "";
            _caminho = "";
            BoxVisible = true;
            LabelPhotoVisible = true;
            FotoUsuario = null;


        }));

        public Command Tapped_VisibleSenha => _tappedVisibleSenha ?? (_tappedVisibleSenha = new Command(() =>
        {
            if (BoolVisibleSenha == true)
            {
                CaminhoSenha = "olho.png";
                BoolVisibleSenha = false;

            }
            else
            {
                CaminhoSenha = "ocultar.png";
                BoolVisibleSenha = true;
            }

        }));
        public Command Tapped_VisibleConfirmaSenha => _tappedVisibleConfirmaSenha ?? (_tappedVisibleConfirmaSenha = new Command(() =>
        {
            if (BoolVisibleConfirmaSenha == true)
            {
                CaminhoConfirmaSenha = "olho.png";
                BoolVisibleConfirmaSenha = false;

            }
            else
            {
                CaminhoConfirmaSenha = "ocultar.png";
                BoolVisibleConfirmaSenha = true;
            }

        }));

        public Command TapPhoto => _tappedPhoto ?? (_tappedPhoto = new Command( async () =>
        {
            var resp = await App.Current.MainPage.DisplayActionSheet("Imagem", "Cancelar", "", "Galeria", "Camera");
            //FotoUsuario = new Image();
            if(resp == "Camera")
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsTakePhotoSupported || !CrossMedia.Current.IsCameraAvailable)
                {
                    await App.Current.MainPage.DisplayAlert("Ops", "Nenhuma câmera detectada.", "OK");

                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        SaveToAlbum = true,
                        Directory = "Demo"

                    });

                if (file == null)
                    return;

                

                var image = ImageSource.FromFile(file.Path);

                FotoUsuario = image;

                _caminho = file.Path;

                BoxVisible = false;
                LabelPhotoVisible = false;
                VisibleUserImage = true;
                await App.Current.MainPage.DisplayAlert("", FotoUsuario.ToString() + " " + VisibleUserImage, "Ok");

            }

            else if(resp == "Galeria")
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await App.Current.MainPage.DisplayAlert("Ops", "Galeria de fotos não suportada.", "OK");

                    return;
                }

                var file = await CrossMedia.Current.PickPhotoAsync();

                if (file == null)
                    return;

                _caminho = file.Path;
                FotoUsuario = ImageSource.FromStream(() =>
                {
                   var stream = file.GetStream();
                   file.Dispose();
                   return stream;

                });
            }

            BoxVisible = false;
            LabelPhotoVisible = false;

        }));





        public async void ConsultaCep()
        {
            try
            {
                string cep = EntryCep;
                cep = cep.Replace("-", "");
                var content = await _cep.GetEnderecoByCepAsync(cep);
                if (content != null)
                {
                    if (string.IsNullOrEmpty(content.uf))
                    {
                        SemPreenchimento();
                        LabelColorCep = "Red";
                       // await App.Current.MainPage.DisplayAlert("Aviso!", "CEP inválido", "Ok");
                    }
                    else
                    {
                        EntryRua = content.logradouro;
                        EntryCidade = content.localidade;
                        EntryEstado = content.uf;
                        EntryBairro = content.bairro;
                        BoolRua = true;
                        BoolCidade = true;
                        BoolEstado = true;
                        BoolBairro = true;
                        BoolSalvar = true;
                        BoolCancelar = true;
                        LabelColorCep = "Blue";
                    }
                }
            }
            catch (Exception e)
            {

                await App.Current.MainPage.DisplayAlert("Aviso!", e.Message, "Ok"); ;
            }
           

            
        }
        public void SemPreenchimento()
        {
            
            EntryRua = "";
            EntryCidade = "";
            EntryEstado = "";
            EntryBairro = "";
            BoolRua = false;
            BoolCidade = false;
            BoolEstado = false;
            BoolBairro = false;
            BoolSalvar = false;
            BoolCancelar = false;
        }

       
        void NovoCadastro()
        {
            BoolEstado = false;
            BoolCidade = false;
            BoolRua = false;
            BoolBairro = false;
            BoolSalvar = false;
            BoolCancelar = false;
            BoolVisibleSenha = true;
            BoolVisibleConfirmaSenha = true;
            Liberado = false;
            VisibleUserImage = true;
            BoxVisible = true;
            LabelPhotoVisible = true;
            CaminhoSenha = "ocultar.png";
            CaminhoConfirmaSenha = "ocultar.png";
        }

        void AlterarCadastro()
        {

            EntryNome = _usuario.nome;
            EntryLogin = _usuario.login;
            EntryCpf = _usuario.cpf;
            EntryRua = _usuario.endereco.logradouro;
            EntryBairro = _usuario.endereco.bairro;
            EntryCidade = _usuario.endereco.localidade;
            EntryEstado = _usuario.endereco.uf;
            EntryCep = _usuario.endereco.cep;
            EntrySenha = _usuario.senha;
            EntryCofirmaSenha = _usuario.senha;    
            CaminhoConfirmaSenha = "ocultar.png";
            CaminhoSenha = "ocultar.png";
            BoolEstado = true;
            BoolCidade = true;
            BoolRua = true;
            BoolBairro = true;
            BoolSalvar = true;
            BoolCancelar = true;
            BoolVisibleSenha = true;
            BoolVisibleConfirmaSenha = true;
            Liberado = true;
            VisibleUserImage = true;

            if (string.IsNullOrEmpty(_usuario.picture))
            {
                BoxVisible = true;
                LabelPhotoVisible = true;
                // VisibleUserImage = false;
            }
            else
            {
                FotoUsuario = ImageSource.FromFile(_usuario.picture);
                BoxVisible = false;
                LabelPhotoVisible = false;
               //VisibleUserImage = true;
            }
            
        }

        public bool ValidacaoCpf()
        {
            int controle = 0;
            var cpf = EntryCpf;
            char igual = cpf[0];
            if (string.IsNullOrEmpty(cpf))
            {
                return false;
            }

            cpf = cpf.Replace(".", "").Replace("-", "");
            cpf = cpf.Trim();
            if (cpf.Length != 11)
                return false;
            for(int i = 1; i < cpf.Length; i++)
            {
                if (cpf[i] == igual)
                    controle++;
            }
            if (controle >= 10)
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito += resto.ToString();
            return cpf.EndsWith(digito);
        }

        
    }

 }

