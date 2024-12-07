using CoinApp.Models;
using CoinApp.Services;

namespace CoinApp.Views;

public partial class CreateUpdate : ContentPage
{
	public Weapon toUpdate;
	public Weapon toCreate;

	private ApiService apiService = new ApiService();
	public CreateUpdate(Weapon weapon)
	{
		InitializeComponent();
		if(weapon == null)
		{
			ActionTo.Text = "Registering new Data...";
		}
		else
		{
			ActionTo.Text = "Updating Data...";

			NameEntry.Text = weapon.full_name;
			DateEntry.Text = weapon.date;
			DescriptionEntry.Text = weapon.description;
			this.toUpdate = weapon;

			Console.WriteLine(weapon.Id);
		}
		
	}

	public async void ActionToSend(object sender, EventArgs e)
	{
		if (NameEntry.Text == null || DateEntry.Text == null || DescriptionEntry.Text == null)
		{
			_ = DisplayAlert("Warning", "Please check your data", "ok");
			return;
		}

		if (this.toUpdate != null) {
			Console.WriteLine("Updating");
			var Weapon = new Weapon { 
				Id = this.toUpdate.Id,
				full_name = NameEntry.Text,
				date = DateEntry.Text,
				description = DescriptionEntry.Text,
			};

			if (await apiService.UpdateWeapon(Weapon))
			{
				await Navigation.PopToRootAsync();
			}
			else
			{
				await DisplayAlert("Error", "Failed to update weapon.", "OK");

			}
		}
		else
		{
			Console.WriteLine("Creating");
			var weaponTo = new Weapon { 
				full_name = NameEntry.Text,
				date = DateEntry.Text,
				description = DescriptionEntry.Text,
			};

			if(await apiService.CreateWeapon(weaponTo))
			{
				await Navigation.PopAsync();
			}
			else
			{
				await DisplayAlert("Error", "Failed to create weapon.", "OK");
			}
		}

	}
}