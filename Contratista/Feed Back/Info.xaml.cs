using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contratista.Feed_Back
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Info : ContentPage
	{
		public Info ()
		{
			InitializeComponent ();
            //On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }

        private void BtnContactanos_Clicked(object sender, EventArgs e)
        {
            //contactanos
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //contactanos
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            //manual de usuario
        }

        private void BtnManual_Clicked(object sender, EventArgs e)
        {
            //manual de usuario
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            //info de la app
        }

        private void BtnInfoApp_Clicked(object sender, EventArgs e)
        {
            //info de la app
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            //terminos y condiciones
        }

        private void BtnTerminos_Clicked(object sender, EventArgs e)
        {
            //terminos y condiciones
        }
    }
}