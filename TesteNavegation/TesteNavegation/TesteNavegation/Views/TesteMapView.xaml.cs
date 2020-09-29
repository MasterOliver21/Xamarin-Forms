using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace TesteNavegation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TesteMapView : ContentPage
    {
        public TesteMapView()
        {
            InitializeComponent();
        }
        public async void btn_clicked(object sender, System.EventArgs e)
        {
            try
            {
                var location = new Location(Convert.ToDouble(entryLatitude.Text),
                Convert.ToDouble(entryLongitude.Text));
                var options = new MapLaunchOptions
                {
                    Name = entryNome.Text
                };
                await Map.OpenAsync(location, options);
            }
            catch (Exception ex)
            {
                // Não foi possivel acessar o mapa
                await DisplayAlert("Erro : ", ex.Message, "Ok");
            }
        }
    }
}