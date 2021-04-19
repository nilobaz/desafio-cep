using DesafioCepMOBRJ.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DesafioCepMOBRJ.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CepListaPage : ContentPage
    {
        CepListaViewModel _viewModel;

        public CepListaPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CepListaViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();

            this.ListCep.ItemsSource = null;
            this.ListCep.ItemsSource = _viewModel._listaCeps;
        }
    }
}