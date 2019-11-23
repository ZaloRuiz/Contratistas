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
	public partial class AgregarFormacionAcademica : ContentPage
	{
        private int IDProfesional;
		public AgregarFormacionAcademica (int IdProfesional)
		{
			InitializeComponent ();
            IDProfesional = IdProfesional;
		}

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            Formacion_academica formacion = new Formacion_academica()
            {

                titulo = txtTitulo.Text,
                lugar = txtLugar.Text,
                id_profesional = IDProfesional
            };

            var json = JsonConvert.SerializeObject(formacion);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var result = await client.PostAsync("http://dmrbolivia.online/api_contratistas/curriculum/agregarFormacionAcademica.php", content);

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