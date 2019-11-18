using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Contratista.Datos;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.ObjectModel;

namespace Contratista
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Busqueda : ContentPage
	{
        private string TxtBuscado;
        List<Datos.Contratista> Items;
        public Busqueda (string TextoBuscador)
		{
			InitializeComponent ();
            TxtBuscado = TextoBuscador;
            sb_search.Text = TextoBuscador;
            InitList();
            InitSearchBar();
        }
        
        async void InitList()
        {
            Items = new List<Datos.Contratista>();

            try
            {
                HttpClient client = new HttpClient();
                var url_contratista = new Uri("http://dmrbolivia.online/api_contratistas/contratistas/listaContratista.php");
                string result = await client.GetStringAsync(url_contratista);
                var usuarios = JsonConvert.DeserializeObject<List<Datos.Contratista>>(result);

                foreach (var item in usuarios.Distinct())
                {
                    Items.Add(new Datos.Contratista
                    {
                        nombre = item.nombre,
                        id_contratista = item.id_contratista,
                        apellido_paterno = item.apellido_paterno,
                        apellido_materno = item.apellido_materno,
                        telefono = item.telefono,
                        descripcion = item.descripcion,
                        rubro = item.rubro,
                    });
                }
            }
            catch (Exception err)
            {
                await DisplayAlert("ERROR", err.ToString(), "OK");
            }
            listSearch.ItemsSource = Items;

        }
        void InitSearchBar()
        {
            sb_search.TextChanged += (s, e) => FilterItem(sb_search.Text);
            sb_search.SearchButtonPressed += (s, e) => FilterItem(sb_search.Text);
        }

        private void FilterItem(string filter)
        {
            listSearch.BeginRefresh();
            if (string.IsNullOrWhiteSpace(filter))
            {
                listSearch.ItemsSource = Items;
            }
            else if(string.IsNullOrEmpty(filter))
            {
                listSearch.ItemsSource = Items;
            }
            else
            {
                listSearch.ItemsSource = Items.Where(x => x.nombre.ToLower().Contains(filter.ToLower()));
            }
            listSearch.EndRefresh();
        }
    }
}