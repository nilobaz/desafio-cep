using DesafioCepMOBRJ.Models;
using DesafioCepMOBRJ.Services;
using DesafioCepMOBRJ.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace DesafioCepMOBRJ.ViewModels
{
    public class CepListaViewModel : BaseViewModel
    {
        private CepModel _selectedCep;
        public Command<CepModel> CepTapped { get; }

        public CepModel SelectedCep
        {
            get => _selectedCep;
            set
            {
                SetProperty(ref _selectedCep, value);
                OnItemSelected(value);
            }
        }

        public CepListaViewModel()
        {
            _cepRepository = new CepRepository();
            _listaCeps = new List<CepModel>();

            CepTapped = new Command<CepModel>(OnItemSelected);
        }

        public void OnAppearing()
        {
            try
            {
                SelectedCep = null;

                _listaCeps.Clear();
                var listaBD = _cepRepository.GetAllCepsData();

                if (listaBD.Count > 0)
                {
                    listaBD = listaBD.OrderByDescending(x => x.Id).ToList();
                    _listaCeps.AddRange(listaBD);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        async void OnItemSelected(CepModel item)
        {
            try
            {
                if (item == null)
                    return;

                await Shell.Current.GoToAsync($"{nameof(CepListaDetailPage)}?{nameof(CepListaDetailViewModel.ItemCep)}={item.Cep}");
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
