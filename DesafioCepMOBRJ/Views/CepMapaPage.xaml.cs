using DesafioCepMOBRJ.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DesafioCepMOBRJ.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CepMapaPage : ContentPage
    {
        CepMapaViewModel _viewModel;

        public CepMapaPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CepMapaViewModel();
            _viewModel._mapa = MeuMapa;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}