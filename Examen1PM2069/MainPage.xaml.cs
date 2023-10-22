using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Examen1PM2069.Models;
using System.Collections.Generic;
using System.Linq;

namespace Examen1PM2069
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private string rutaImagen;
        private string baseDatosPath = Path.Combine(FileSystem.AppDataDirectory, "sitios.db");
        private SQLiteAsyncConnection conexion;
        private object imgFoto;

        public MainPage()
        {
            InitializeComponent();
            conexion = new SQLiteAsyncConnection(baseDatosPath);
            conexion.CreateTableAsync<Sitio>().Wait();
        }

        private async void TomarFoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                var opciones = new MediaCaptureOptions
                {
                    Directory = "TuCarpetaDeImagenes",
                    Name = $"{DateTime.Now:yyyyMMddHHmmss}.jpg"
                };

                var foto = await MediaPicker.CapturePhotoAsync(opciones);

                if (foto != null)
                {
                    rutaImagen = foto.FullPath;
                    imgFoto.Source = ImageSource.FromFile(rutaImagen);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error al tomar la foto: {ex.Message}", "OK");
            }
        }

        private async void GuardarSitio_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rutaImagen) && !string.IsNullOrEmpty(txtDescripcion.Text))
            {
                var sitio = new Sitio
                {
                    Imagen = rutaImagen,
                    Descripcion = txtDescripcion.Text
                };

                await conexion.InsertAsync(sitio);
                LimpiarFormulario();
                await DisplayAlert("Éxito", "Sitio guardado con éxito", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Debes tomar una foto y proporcionar una descripción", "OK");
            }
        }

        private async void ListarSitios_Clicked(object sender, EventArgs e)
        {
            var sitios = await conexion.Table<Sitio>().ToListAsync();
            await Navigation.PushAsync(new ListaSitiosPage(sitios));
        }

        private void LimpiarFormulario()
        {
            rutaImagen = string.Empty;
            imgFoto.Source = null;
            txtDescripcion.Text = string.Empty;
        }
    }
}

