using DesafioCepMOBRJ.Models;
using System.Collections.Generic;

namespace DesafioCepMOBRJ.Services
{
    public interface ICepRepository
    {
        List<CepModel> GetAllCepsData();
        CepModel GetCepData(string cep);
        void InsertCep(CepModel cep);
    }
}
