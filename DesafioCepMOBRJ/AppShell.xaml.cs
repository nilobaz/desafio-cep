using DesafioCepMOBRJ.ViewModels;
using DesafioCepMOBRJ.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DesafioCepMOBRJ
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CepsPage), typeof(CepsPage));
            Routing.RegisterRoute(nameof(CepListaDetailPage), typeof(CepListaDetailPage));
        }
    }
}
