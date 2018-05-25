using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using App3.ViewModels;

namespace App3
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            Status.Text = txtName.Text + " - " + txtAge.Text;

            var abc = FirstVM.DoSomeDataAccess();

            Status.Text = abc;// txtName.Text + " - " + txtAge.Text;

        }
    }
}
