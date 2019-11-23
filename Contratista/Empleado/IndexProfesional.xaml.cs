using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Contratista.Datos;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace Contratista.Empleado
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IndexProfesional : TabbedPage
	{
        private int idProfesional;
        private string nombre_profesionales;
        private string emails;
        private int telefonos;
        ObservableCollection<Portafolio_profesional> portafolio_Profesionals = new ObservableCollection<Portafolio_profesional>();
        public ObservableCollection<Portafolio_profesional> Portafolios { get { return portafolio_Profesionals; } }
        public IndexProfesional(int id_profesional, string nombre, string apellido_paterno, string apellido_materno, int telefono, string email, string rubro, string estado,
                                 int prioridad, int nit, decimal calificacion, string foto, string descripcion, string curriculum)
        {
            InitializeComponent();
            idProfesional = id_profesional;
            nombre_profesionales = nombre + " " + apellido_paterno + " " + apellido_materno;
            emails = email;
            telefonos = telefono;
            txtNombre.Text = nombre + " " + apellido_paterno + " " + apellido_materno;
            txtTelefono.Text = telefono.ToString();
            txtEmail.Text = email;
            txtRubro.Text = rubro;
            txtEstado.Text = estado;
            txtPrioridad.Text = prioridad.ToString();
            txtNit.Text = nit.ToString();
            txtDescripcion.Text = descripcion;
            txtCurriculum.Text = curriculum;
            img_perfil.Source = "http://dmrbolivia.online" + foto;
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

            catch (Exception erro)
            {
                Console.Write("EEERRROOOORRR= " + erro);
            }
            listPortafolios.ItemsSource = portafolio_Profesionals.Distinct();
        }
        private async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            var detalles = e.Item as Portafolio_profesional;
            await Navigation.PushAsync(new MostrarPortafolio(detalles.id_portafolio_p, detalles.nombre, detalles.imagen_1, detalles.imagen_2, detalles.imagen_3,
                                                            detalles.imagen_4, detalles.imagen_5, detalles.imagen_6, detalles.imagen_7, detalles.id_profesional));
        }
        protected void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarPortafolio(idProfesional, nombre_profesionales));
        }
        private void BtnCV_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarCurriculum(idProfesional, nombre_profesionales, emails, telefonos));
        }
    }
}