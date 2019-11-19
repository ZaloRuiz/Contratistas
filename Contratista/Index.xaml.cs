using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;

using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using TabbedPage = Xamarin.Forms.TabbedPage;

using System.Net.Http;
using Newtonsoft.Json;
using Contratista.Datos;
using Contratista.Empleado;

namespace Contratista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Index : TabbedPage
    {
        private string TextoBuscador;
        public Index()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

        }

        private void BtnIalbanil_Clicked(object sender, EventArgs e)
        {
           Navigation.PushAsync(new MenuContratistas());
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
           Navigation.PushAsync(new MenuContratistas());
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
          Navigation.PushAsync(new MenuProfesionales());
        }
        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
           Navigation.PushAsync(new MenuServicios());
        }
        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuMateriales());
        }
        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuEmpresas());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
           Navigation.PushAsync(new MenuEmpresas());
        }

        private void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Feed_Back.Info());
        }

        private async void BtnIngresar_Clicked(object sender, EventArgs e)
        {
            cargando.IsVisible = true;
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("http://dmrbolivia.online/api_contratistas/queryLogin.php");
                var data = JsonConvert.DeserializeObject<List<Login>>(response);

                foreach (var item in data)
                {
                    var responseC = await client.GetStringAsync("http://dmrbolivia.online/api_contratistas/contratistas/listaContratista.php");
                    var dataC = JsonConvert.DeserializeObject<List<Datos.Contratista>>(responseC);
                    if (usuarioEntry.Text == item.usuario)
                    {
                        if (contrasenaEntry.Text == item.contrasena)
                        {
                            foreach (var item2 in dataC)
                            {
                                if (usuarioEntry.Text == item2.usuario)
                                {
                                    await Navigation.PushAsync(new IndexEmpleado(item2.id_contratista, item2.nombre, item2.apellido_paterno, item2.apellido_materno,
                                                                                 item2.telefono, item2.rubro, item2.estado, item2.calificacion, item2.foto, item2.descripcion));
                                    cargando.IsVisible = false;
                                }
                                else
                                {
                                    ;
                                }
                            }

                            var responseP = await client.GetStringAsync("http://dmrbolivia.online/api_contratistas/profesionales/listaProfesional.php");
                            var dataP = JsonConvert.DeserializeObject<List<Profesional>>(responseP);
                            foreach (var item3 in dataP)
                            {
                                if (usuarioEntry.Text == item3.usuario)
                                {
                                    await Navigation.PushAsync(new IndexProfesional(item3.id_profesional, item3.nombre, item3.apellido_paterno, item3.apellido_materno,
                                                                                    item3.telefono, item3.email, item3.rubro, item3.estado, item3.prioridad, item3.nit,
                                                                                    item3.calificacion, item3.foto, item3.descripcion, item3.curriculum));
                                    cargando.IsVisible = false;
                                }
                                else
                                {
                                    ;
                                }
                            }

                            var responseS = await client.GetStringAsync("http://dmrbolivia.online/api_contratistas/servicios/listaServicio.php");
                            var dataS = JsonConvert.DeserializeObject<List<Servicio>>(responseS);
                            foreach (var item4 in dataS)
                            {

                                if (usuarioEntry.Text == item4.usuario)
                                {
                                    await Navigation.PushAsync(new IndexServicio(item4.id_servicio, item4.nombre, item4.telefono, item4.email,
                                                                                  item4.rubro, item4.estado, item4.prioridad, item4.calificacion, item4.foto, item4.descripcion,
                                                                                  item4.nit));
                                    cargando.IsVisible = false;
                                }
                                else
                                {
                                    ;
                                }
                            }

                            var responseM = await client.GetStringAsync("http://dmrbolivia.online/api_contratistas/materiales/listaMaterial.php");
                            var dataM = JsonConvert.DeserializeObject<List<Material>>(responseM);
                            foreach (var item5 in dataM)
                            {
                                if (usuarioEntry.Text == item5.usuario)
                                {
                                    await Navigation.PushAsync(new IndexMaterial(item5.id_material, item5.nombre, item5.telefono, item5.email,
                                                                                  item5.rubro, item5.prioridad, item5.calificacion, item5.foto, item5.descripcion,
                                                                                  item5.nit));
                                    cargando.IsVisible = false;
                                }
                                else
                                {
                                    ;
                                }
                            }

                            var responseE = await client.GetStringAsync("http://dmrbolivia.online/api_contratistas/empresas/listaEmpresa.php");
                            var dataE = JsonConvert.DeserializeObject<List<Empresa>>(responseE);
                            foreach (var item6 in dataE)
                            {
                                if (usuarioEntry.Text == item6.usuario)
                                {
                                    await Navigation.PushAsync(new IndexEmpresa(item6.id_empresa, item6.nombre, item6.telefono, item6.email,
                                                                                  item6.rubro, item6.prioridad, item6.calificacion, item6.foto, item6.descripcion,
                                                                                  item6.nit));
                                    cargando.IsVisible = false;
                                }
                                else
                                {
                                    ;
                                }
                            }
                        }
                        else
                        {
                            await DisplayAlert("", "Contrasena incorrecta", "OK");
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                await DisplayAlert("Error", erro.ToString(), "OK");
            }
        }
        
        private void Buscador_SearchButtonPressed(object sender, EventArgs e)
        {
            TextoBuscador = buscador.Text;
            Navigation.PushAsync(new Busqueda(TextoBuscador));
        }
    }
}