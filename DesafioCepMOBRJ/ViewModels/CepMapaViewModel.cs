using DesafioCepMOBRJ.Models;
using DesafioCepMOBRJ.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Maps;

namespace DesafioCepMOBRJ.ViewModels
{
    public class CepMapaViewModel : BaseViewModel
    {    
        public Map _mapa;

        public CepMapaViewModel()
        {
            _cepRepository = new CepRepository();
            _listaCeps = new List<CepModel>();
            _mapa = new Map();
        }

        //Preenche o mapa com pins dos registros no banco de dados
        public async void OnAppearing()
        {
            try
            {
                this.IsBusy = true;

                _listaCeps.Clear();
                var listaBD = _cepRepository.GetAllCepsData();

                if (listaBD.Count > 0)
                {
                    listaBD = listaBD.OrderByDescending(x => x.Id).ToList();
                    _listaCeps.AddRange(listaBD);

                    Geocoder posicoes = new Geocoder();

                    foreach (CepModel c in _listaCeps)
                    {
                        var cepPosicoes = (await posicoes.GetPositionsForAddressAsync(c.Cep)).ToList();

                        if (cepPosicoes.Count != 0)
                        {
                            var pinCep = new Pin
                            {
                                Type = PinType.Place,
                                Label = c.Cep,
                                Position = new Position(cepPosicoes[0].Latitude, cepPosicoes[0].Longitude)
                            };

                            _mapa.Pins.Add(pinCep);
                        }
                    }

                    //Centraliza o mapa para o último Cep gravado
                    var ultimoCep = (await posicoes.GetPositionsForAddressAsync(_listaCeps[0].Cep)).ToList();
                    _mapa.MoveToRegion(MapSpan.FromCenterAndRadius(
                             new Position(ultimoCep[0].Latitude, ultimoCep[0].Longitude), Distance.FromKilometers(3.0)));
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.IsBusy = false;
            }
        }
    }
}
