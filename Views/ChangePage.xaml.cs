using CoinApp.Models;
using CoinApp.Services;

namespace CoinApp.Views;

public partial class ChangePage : ContentPage
{
	private Weapon selectedWeapon;
	private ApiService apiService = new ApiService();

	public ChangePage(Weapon weapon)
	{
		InitializeComponent();
		fname.Text = $"Name: {weapon.full_name}";
		date.Text = $"Date related: {weapon.date}";
		description.Text = $"Description: {weapon.description}";

		this.selectedWeapon = weapon;
	}

	private async void OnClickUpdate(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new CreateUpdate(this.selectedWeapon));
	}
	private async void OnClickDelete(object sender, EventArgs e)
	{
		bool confirmed = await Application.Current.MainPage.DisplayAlert("Deleting", "Are you sure you want to delete this weapon?", "Yes", "No");
		if (confirmed)
		{
			await apiService.DeleteWeapon(this.selectedWeapon);
			await Navigation.PopAsync();
		}
	}
}