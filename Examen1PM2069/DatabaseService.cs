using Examen1PM2069.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;



namespace Examen1PM2069.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Sitio>().Wait();
        }

        public async Task<List<Sitio>> GetSitiosAsync()
        {
            return await _database.Table<Sitio>().ToListAsync();
        }

        public async Task<int> SaveSitioAsync(Sitio sitio)
        {
            if (!string.IsNullOrWhiteSpace(sitio.Descripcion))
            {
                if (string.IsNullOrWhiteSpace(sitio.ImagenPath))
                {
                    sitio.ImagenPath = "default_image_path.jpg"; // Establece una imagen por defecto si no se proporciona una
                }

                return await _database.InsertAsync(sitio);
            }
            else
            {
                return -1; // Error de validación
            }
        }
    }
}
