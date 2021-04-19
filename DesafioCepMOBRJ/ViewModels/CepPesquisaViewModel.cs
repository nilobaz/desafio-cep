using DesafioCepMOBRJ.Models;
using DesafioCepMOBRJ.Services;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DesafioCepMOBRJ.ViewModels
{
    public class CepPesquisaViewModel : BaseViewModel
    {
        private string _CEP;
        public const string CEPPropName = "CEP";
        public string CEP
        {
            get { return this._CEP; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this._CEP = Regex.Replace(value, "[^0-9]", string.Empty);

                    this._CEP = this._CEP.Substring(0, this._CEP.Length > 8 ? 8 : this._CEP.Length);

                    //if (this._CEP.Length == 8)
                    //    this._CEP = $"{this._CEP.Substring(0, 5)}-{this._CEP.Substring(5)}";
                }
                else
                    this._CEP = value;
            }
        }


        private Command _BuscarCep;
        public Command BuscarCep
        {
            get
            {
                return this._BuscarCep ?? (this._BuscarCep = new Command(async () => await BuscarCepExecute(), () => { return !this.IsBusy; }));
            }
        }

        public CepPesquisaViewModel()
        {
            _cepRepository = new CepRepository();
        }

        public async Task BuscarCepExecute()
        {
            try
            {
                if (this.IsBusy)
                    return;

                this.IsBusy = true;

                this.BuscarCep.ChangeCanExecute();

                if (string.IsNullOrWhiteSpace(this._CEP))
                    await App.Current.MainPage.DisplayAlert("Desafio MOBRJ", "Informe um CEP.", "Ok");
                else
                {
                    //this._CEP = _CEP.Replace("-", string.Empty);

                    using (var _client = new System.Net.Http.HttpClient())
                    {
                        //Pesquisa o Cep digitado no webservice
                        using (var _resposta = await _client.GetAsync($"https://viacep.com.br/ws/{this._CEP}/json/"))
                        {

                            if (!_resposta.IsSuccessStatusCode)
                            {
                                await App.Current.MainPage.DisplayAlert("Desafio MOBRJ", "CEP inválido.\nTente novamente.", "Ok");
                                return;
                            }

                            var _respostaContent = await _resposta.Content.ReadAsStringAsync();

                            if (_respostaContent.Contains("\"erro\": true"))
                            {
                                await App.Current.MainPage.DisplayAlert("Desafio MOBRJ", "CEP inválido.\nTente novamente.", "Ok");
                                return;
                            }

                            var cepRetorno = JsonConvert.DeserializeObject<CepModel>(_respostaContent);

                            _respostaContent = _respostaContent.Replace("\"", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty).Replace(",", string.Empty);


                            //Exibe o Cep encontrado e dá opção de gravar no banco local
                            bool gravar = await App.Current.MainPage.DisplayAlert("CEP Encontrado!", _respostaContent, "Gravar", "Cancelar");

                            if (gravar)
                            {
                                var cepExite = _cepRepository.GetCepData(cepRetorno.Cep);

                                if (cepExite != null)
                                    await App.Current.MainPage.DisplayAlert("Desafio MOBRJ", "CEP já consta no banco!\nTente novamente.", "Ok");
                                else
                                {
                                    _cepRepository.InsertCep(cepRetorno);
                                    await App.Current.MainPage.DisplayAlert("Desafio MOBRJ", "CEP gravado com sucesso!", "Ok");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.IsBusy = false;
                this.BuscarCep.ChangeCanExecute();
            }
        }
    }
}
