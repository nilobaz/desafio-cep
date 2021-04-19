using DesafioCepMOBRJ.Models;
using PCLExt.FileStorage;
using PCLExt.FileStorage.Folders;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace DesafioCepMOBRJ.Helpers
{
    public class DatabaseHelper
    {
        static SQLiteConnection sqliteconnection;
        public const string DbFileName = "CepsDB.db";

        public DatabaseHelper()
        {
            var pasta = new LocalRootFolder();
            var arquivo = pasta.CreateFile(DbFileName, CreationCollisionOption.OpenIfExists);
            sqliteconnection = new SQLiteConnection(arquivo.Path);
            sqliteconnection.CreateTable<CepModel>();
        }

        public List<CepModel> GetAllCepsData()
        {
            return (from data in sqliteconnection.Table<CepModel>()
                    select data).ToList();
        }

        public CepModel GetCepData(string cep)
        {
            return sqliteconnection.Table<CepModel>().FirstOrDefault(t => t.Cep == cep);
        }

        public void InsertCep(CepModel Cep)
        {
            sqliteconnection.Insert(Cep);
        }
    }
}