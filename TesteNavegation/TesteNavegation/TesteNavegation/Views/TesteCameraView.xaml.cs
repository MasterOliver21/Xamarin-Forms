using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TesteNavegation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TesteCameraView : ContentPage
    {
        public TesteCameraView()
        {
            InitializeComponent();
            
        }


        private async void TirarFoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakePhotoSupported || !CrossMedia.Current.IsCameraAvailable)
            {
                await DisplayAlert("Ops", "Nenhuma câmera detectada.", "OK");

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


            //this.Img.IsEnabled = true;
            //this.Img.IsVisible = true;
            //this.Box.IsEnabled = false;
            //this.Box.IsVisible = false;
            //this.Label.IsEnabled = false;
            //this.Label.IsVisible = false;

            this.Img.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;

            });



        }

        private async void EscolherFoto(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Ops", "Galeria de fotos não suportada.", "OK");

                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return;

            //imgFoto.Source = ImageSource.FromStream(() =>
            //{
            //    var stream = file.GetStream();
            //    file.Dispose();
            //    return stream;

            //});
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            TirarFoto();
        }
    }
}
