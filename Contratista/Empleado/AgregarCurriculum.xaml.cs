using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Contratista.Datos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Plugin.Permissions;
using Xamarin.Forms.PlatformConfiguration;

namespace Contratista.Empleado
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AgregarCurriculum : ContentPage
	{
        private int IdProfesional;
        
        public AgregarCurriculum (int idProfesional, string nombre_profesionales, string emails, int telefonos)
		{
			InitializeComponent ();
            IdProfesional = idProfesional;
            txtNombre.Text = nombre_profesionales;
            txtTelefono.Text = telefonos.ToString();
            txtEmail.Text = emails;
            Formacion_academica();
            Experiencia_laboral();
        }
        private async void Formacion_academica()
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("http://dmrbolivia.online/api_contratistas/curriculum/listaFormacionAcademica.php");
                var listformacade = JsonConvert.DeserializeObject<List<Formacion_academica>>(response);

                foreach (var item in listformacade.Distinct())
                {
                    if (item.id_profesional == IdProfesional)
                    {
                        StackLayout stk1 = new StackLayout();
                        stk1.BackgroundColor = Color.LightGray;
                        stkAcademica.Children.Add(stk1);

                        Label txtNombre = new Label();
                        txtNombre.Text = item.titulo;
                        txtNombre.TextColor = Color.Black;
                        txtNombre.FontSize = 30;
                        stk1.Children.Add(txtNombre);

                        Label txtDesc = new Label();
                        txtDesc.Text = item.lugar;
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

        private async void Experiencia_laboral()
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("http://dmrbolivia.online/api_contratistas/curriculum/listaExperienciaLaboral.php");
                var listexplaboral = JsonConvert.DeserializeObject<List<Experiencia_laboral>>(response);

                foreach (var item in listexplaboral.Distinct())
                {
                    if (item.id_profesional == IdProfesional)
                    {
                        StackLayout stk1 = new StackLayout();
                        stk1.BackgroundColor = Color.LightGray;
                        stkLaboral.Children.Add(stk1);

                        Label txtCargo = new Label();
                        txtCargo.Text = item.cargo;
                        txtCargo.TextColor = Color.Black;
                        txtCargo.FontSize = 30;
                        stk1.Children.Add(txtCargo);

                        Label txtEmp = new Label();
                        txtEmp.Text = item.empresa;
                        txtEmp.FontSize = 15;
                        txtEmp.TextColor = Color.Black;
                        stk1.Children.Add(txtEmp);

                        Label txtDurac = new Label();
                        txtDurac.Text = item.duracion;
                        txtDurac.FontSize = 15;
                        txtDurac.TextColor = Color.Black;
                        stk1.Children.Add(txtDurac);

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

        private void AgregarFormacionAcademica_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarFormacionAcademica(IdProfesional));
        }

        private void AgregarExperienciaLaboral_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarExperienciaLaboral(IdProfesional));
        }
    }
}