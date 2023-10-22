using Examen1PM2069.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Examen1PM2069
{
    internal class ListaSitiosPage : Page
    {
        private List<Sitio> sitios;

        public ListaSitiosPage(List<Sitio> sitios)
        {
            this.sitios = sitios;
        }
    }
}