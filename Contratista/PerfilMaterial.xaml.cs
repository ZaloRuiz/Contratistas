using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.Net.Http;
using Contratista.Datos;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contratista
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PerfilMaterial : TabbedPage
	{
        int Numero_telefono = 0;
        private string queryrubro;
        private int idMaterial;
        private string Latitud;
        private string Longitud;
        ObservableCollection<Productos> producto = new ObservableCollection<Productos>();
        public ObservableCollection<Productos> productos { get { return producto; } }
        public PerfilMaterial(int id_material, string nombre, int telefono, string email, string direccion, string ubicacion_lat, string ubicacion_long,
            string foto, int nit, string rubro, decimal calificacion, string descripcion)
        {
            InitializeComponent();

            Latitud = ubicacion_lat;
            Longitud = ubicacion_long;
            Numero_telefono = telefono;
            idMaterial = id_material;
            nombretxt.Text = nombre;
            rubrotxt.Text = rubro;
            imgPerfil.Source = "http://dmrbolivia.online" + foto;
            txtdescripcion.Text = descripcion;
            txtTelefono.Text = telefono.ToString();
            califtxt.Text = calificacion.ToString();
            txtDireccion.Text = direccion;
            GetInfo();
            GetPromo();
        }

        private async void GetInfo()
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("http://dmrbolivia.online/api_contratistas/productos/listaProducto.php");
                var product = JsonConvert.DeserializeObject<List<Productos>>(response);

                foreach (var item in product.Distinct())
                {
                    if (item.id_material == idMaterial)
                    {
                        productos.Add(new Productos
                        {
                            nombre = item.nombre,
                            id_producto = item.id_producto,
                            imagen_1 = item.imagen_1,
                            imagen_2 = item.imagen_2,
                            descripcion = item.descripcion
                        });
                    }
                }
            }

            catch (Exception err)
            {
                await DisplayAlert("ERROR", err.ToString(), "OK");
            }
            listaProducto.ItemsSource = productos.Distinct();
        }

        private async void ListaProducto_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var detalles = e.Item as Productos;
            await Navigation.PushAsync(new VerProducto(detalles.id_producto, detalles.nombre, detalles.descripcion, detalles.imagen_1,
                detalles.imagen_2, detalles.id_material));
        }

        private async void GetPromo()
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("http://dmrbolivia.online/api_contratistas/promociones/listaPromocionMaterial.php");
                var listpromo = JsonConvert.DeserializeObject<List<Promocion_material>>(response);

                foreach (var item in listpromo.Distinct())
                {
                    if (item.id_material == idMaterial)
                    {
                        StackLayout stk1 = new StackLayout();
                        stk1.BackgroundColor = Color.LightGray;
                        stkPromoActiva.Children.Add(stk1);

                        Label txtNombre = new Label();
                        txtNombre.Text = item.nombre;
                        txtNombre.TextColor = Color.Black;
                        txtNombre.FontSize = 30;
                        stk1.Children.Add(txtNombre);

                        Image img = new Image();
                        img.Source = "http://dmrbolivia.online" + item.imagen;
                        img.HeightRequest = 200;
                        img.Aspect = Aspect.AspectFit;
                        img.HorizontalOptions = LayoutOptions.CenterAndExpand;
                        stk1.Children.Add(img);

                        Label txtDesc = new Label();
                        txtDesc.Text = item.descripcion;
                        txtDesc.FontSize = 15;
                        txtDesc.TextColor = Color.Black;
                        stk1.Children.Add(txtDesc);

                        BoxView bv = new BoxView();
                        bv.HeightRequest = 5;
                        bv.Color = Color.Gray;
                    }
                }
            }
            catch (Exception erro)
            {
                Console.Write("EEERRROOOORRR= " + erro);
            }
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