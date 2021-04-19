using DesafioCepMOBRJ.Services;
using Xamarin.Forms;

namespace DesafioCepMOBRJ
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<CepRepository>();

            MainPage = new AppShell();
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
