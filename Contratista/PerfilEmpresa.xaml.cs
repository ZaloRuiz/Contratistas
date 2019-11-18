using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contratista.Datos;
using Newtonsoft.Json;
using Xamarin.Essentials;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace Contratista
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PerfilEmpresa : TabbedPage
	{
        int Numero_telefono = 0;
        private int IdEmpresa;
        private string Latitud;
        private string Longitud;
        ObservableCollection<Portafolio_empresa> portafolios_empresas = new ObservableCollection<Portafolio_empresa>();
        public ObservableCollection<Portafolio_empresa> Portafolio_Empresa { get { return portafolios_empresas; } }
        public PerfilEmpresa(int id_empresa, string nombre, int telefono, string email, string direccion, string ubicacion_lat,
            string ubicacion_long, string foto, string rubro, decimal calificacion, string descripcion)
        {
            InitializeComponent();
            Numero_telefono = telefono;
            Latitud = ubicacion_lat;
            Longitud = ubicacion_long;
            IdEmpresa = id_empresa;
            nombretxt.Text = nombre;
            txtTelefono.Text = telefono.ToString();
            imgPerfil.Source = "http://dmrbolivia.online" + foto;
            califtxt.Text = calificacion.ToString();
            txtdescripcion.Text = descripcion;
            txtDireccion.Text = direccion;
            txtEmail.Text = email;
            GetInfo();
        }

        private async void GetInfo()
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("http://dmrbolivia.online/api_contratistas/portafolios/listaPortafolio_empresa.php");
                var portafoliosE = JsonConvert.DeserializeObject<List<Portafolio_empresa>>(response);

                foreach (var item in portafoliosE.Distinct())
                {
                    if (item.id_empresa == IdEmpresa)
                    {
                        portafolios_empresas.Add(new Portafolio_empresa
                        {
                            nombre = item.nombre,
                            id_portafolio_e = item.id_portafolio_e,
                            imagen_1 = item.imagen_1,
                            imagen_2 = item.imagen_2,
                            imagen_3 = item.imagen_3,
                            imagen_4 = item.imagen_4,
                            imagen_5 = item.imagen_5,
                            imagen_6 = item.imagen_6,
                            imagen_7 = item.imagen_7,
                            id_empresa = item.id_empresa
                        });
                    }
                }
            }

            catch (Exception err)
            {
                await DisplayAlert("ERROR", err.ToString(), "OK");
            }
            listaPortafolios.ItemsSource = portafolios_empresas.Distinct();
        }

        private async void ListaPortafolios_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var detalles = e.Item as Portafolio_empresa;
            await Navigation.PushAsync(new VerPortafolioEmpresa(detalles.id_portafolio_e, detalles.nombre, detalles.imagen_1, detalles.imagen_2,
                detalles.imagen_3, detalles.imagen_4, detalles.imagen_5, detalles.imagen_6, detalles.imagen_7, detalles.id_empresa));
        }

        private async void BtnIrDireccion_Clicked(object sender, EventArgs e)
        {
            try
            {
                var location = new Location(Convert.ToDouble(Latitud), Convert.ToDouble(Longitud));
                var options = new MapLaunchOptions { Name = nombretxt.Text };
                await Map.OpenAsync(location, options); ;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Faild", ex.Message, "OK");
            }
        }
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                PhoneDialer.Open(Numero_telefono.ToString());
            }
            catch (Exception err)
            {
                await DisplayAlert("ERROR", err.ToString(), "OK");
            }
        }

        private async void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                Datos.Chat.Open("+591" + Numero_telefono, " ");
            }
            catch (Exception err)
            {
                await DisplayAlert("Error", err.Message, "OK");
            }
        }
    }
}