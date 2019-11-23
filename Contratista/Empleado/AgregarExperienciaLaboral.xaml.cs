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
using System.Net;

namespace Contratista.Empleado
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AgregarExperienciaLaboral : ContentPage
	{
        private int IDPROFESIONAL;
		public AgregarExperienciaLaboral (int IdProfesional)
		{
			InitializeComponent ();
            IDPROFESIONAL = IdProfesional;
		}

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            Experiencia_laboral experiencia = new Experiencia_laboral()
            {
                cargo = txtCargo.Text,
                empresa = txtEmpresa.Text,
                duracion = pick1.Date.ToString("d")+" - "+pick2.Date.ToString("d"),
                descripcion = txtDescripcion.Text,
                id_profesional = IDPROFESIONAL
            };

            var json = JsonConvert.SerializeObject(experiencia);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var result = await client.PostAsync("http://dmrbolivia.online/api_contratistas/curriculum/agregarExperienciaLaboral.php", content);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                await DisplayAlert("GUARDARDO", "Se agrego correctamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("ERROR", result.StatusCode.ToString(), "OK");
                await Navigation.PopAsync();
            }
        }
    }
}