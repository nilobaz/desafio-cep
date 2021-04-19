using DesafioCepMOBRJ.Helpers;
using DesafioCepMOBRJ.Models;
using System.Collections.Generic;

namespace DesafioCepMOBRJ.Services
{
    public class CepRepository : ICepRepository
    {
        DatabaseHelper _databaseHelper;
        public CepRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }

        public List<CepModel> GetAllCepsData()
        {
            return _databaseHelper.GetAllCepsData();
        }
        public CepModel GetCepData(string cep)
        {
            return _databaseHelper.GetCepData(cep);
        }
        public void InsertCep(CepModel cep)
        {
            _databaseHelper.InsertCep(cep);
        }
    }
}
