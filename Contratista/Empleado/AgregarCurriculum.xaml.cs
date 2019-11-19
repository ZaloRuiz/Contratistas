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
        private MediaFile _mediaFile2;
        private FileData _mediaFile;
        private string ruta;
        private FileData filedata;
        public AgregarCurriculum (int idProfesional)
		{
			InitializeComponent ();
            IdProfesional = idProfesional;
		}

        private async void PickFile_Clicked(object sender, EventArgs args)
        {
            await PickAndShowFile(null);
        }

        private async Task PickAndShowFile(string[] fileTypes)
        {
            try
            {
                _mediaFile = await CrossFilePicker.Current.PickFile();
                if (_mediaFile != null)
                {
                    FileNameLabel.Text = _mediaFile.FileName;
                    FilePathLabel.Text = _mediaFile.FilePath;
                    ruta = "/api_contratistas/PDF/" + FileNameLabel.Text;
                }
            }
            catch (Exception ex)
            {
                FileNameLabel.Text = ex.ToString();
                FilePathLabel.Text = string.Empty;
                FileImagePreview.IsVisible = true;
            }
        }
        
        private async void BtnAgregarPDF_Clicked(object sender, EventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();
                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(_mediaFile.GetStream()), "\"file\"", $"\"{_mediaFile.FilePath}\"");
                var result = await client.PostAsync("http://dmrbolivia.online/api_contratistas/subirPDF.php", content);

                Curriculum curriculum = new Curriculum()
                {
                    direccion = ruta,
                    id_profesional = IdProfesional
                };
                var json = JsonConvert.SerializeObject(curriculum);
                var content1 = new StringContent(json, Encoding.UTF8, "application/json");
                var result1 = await client.PostAsync("http://dmrbolivia.online/api_contratistas/curriculum/agregarCurriculum.php", content1);

                if (result1.StatusCode == HttpStatusCode.OK)
                {
                    await DisplayAlert("GUARDADO", "Se agrego correctamente", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("ERROR", result.StatusCode.ToString(), "OK");
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }
        }
    }
}