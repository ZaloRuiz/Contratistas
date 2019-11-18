using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Collections.ObjectModel;
using System.Net.Http;
using Contratista.Datos;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contratista
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PerfilProfesional : TabbedPage
	{
        int Numero_telefono = 0;
        private string queryrubro;
        private int idProfesional;
        ObservableCollection<Portafolio_profesional> portafolio_Profesionals = new ObservableCollection<Portafolio_profesional>();
        public ObservableCollection<Portafolio_profesional> Portafolios { get { return portafolio_Profesionals; } }
        public PerfilProfesional(int id_profesional, string nombre, string apellido_paterno, string apellido_materno, int telefono, string email,
            string direccion, string foto, string cedula_identidad, string rubro, decimal calificacion, string estado, int prioridad, string descripcion,
            int nit, string curriculum)
        {
            InitializeComponent();
            Numero_telefono = telefono;
            queryrubro = rubro;
            idProfesional = id_profesional;
            nombretxt.Text = nombre + " " + apellido_paterno + " " + apellido_materno;
            rubrotxt.Text = rubro;
            imgPerfil.Source = "http://dmrbolivia.online" + foto;
            txtdescripcion.Text = descripcion;
            txtTelefono.Text = telefono.ToString();
            califtxt.Text = calificacion.ToString();
            txtEmail.Text = email;
            GetInfo();
        }
        private async void GetInfo()
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("http://dmrbolivia.online/api_contratistas/portafolios/listaPortafolio_profesional.php");
                var portafolios = JsonConvert.DeserializeObject<List<Portafolio_profesional>>(response);

                foreach (var item in portafolios.Distinct())
                {
                    if (item.id_profesional == idProfesional)
                    {
                        portafolio_Profesionals.Add(new Portafolio_profesional
                        {
                            nombre = item.nombre,
                            id_portafolio_p = item.id_portafolio_p,
                            imagen_1 = item.imagen_1,
                            imagen_2 = item.imagen_2,
                            imagen_3 = item.imagen_3,
                            imagen_4 = item.imagen_4,
                            imagen_5 = item.imagen_5,
                            imagen_6 = item.imagen_6,
                            imagen_7 = item.imagen_7,
                            id_profesional = item.id_profesional
                        });
                    }
                }
            }
            catch (Exception err)
            {
                await DisplayAlert("ERROR", err.ToString(), "OK");
            }
            listPortafolio.ItemsSource = portafolio_Profesionals.Distinct();
        }
        private async void ListPortafolio_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var detalles = e.Item as Portafolio_profesional;
            await Navigation.PushAsync(new VerPortafolio(detalles.id_portafolio_p, detalles.nombre, detalles.imagen_1, detalles.imagen_2, detalles.imagen_3,
                                                            detalles.imagen_4, detalles.imagen_5, detalles.imagen_6, detalles.imagen_7, detalles.id_profesional));
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