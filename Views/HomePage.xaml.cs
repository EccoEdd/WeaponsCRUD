using CoinApp.Models;
using CoinApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Timers;

namespace CoinApp.Views;

public partial class HomePage : ContentPage
{
	private ApiService apiService = new ApiService();
	private WeaponList weapons = new WeaponList();
	public ObservableCollection<Weapon> WeaponList {  get; set; } = new ObservableCollection<Weapon>();
	public HomePage()
	{
		InitializeComponent();
		this.getData();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		this.WeaponList.Clear();
		this.getData();
	}

	private async void getData()
	{
		if(SessionManager.Instance.IsAuthenticated) {
			var data = await apiService.GetWeapons();
			this.weapons = data;

			WeaponList.Clear();
			foreach(var weapon in weapons.Weapons)
			{
				WeaponList.Add(weapon);
			}

			collectionCoins.ItemsSource = WeaponList;
		}
		else
		{
			_ = DisplayAlert("Error", "An error has acourred, please re-start the app", "I understand");
			Application.Current.MainPage = new NavigationPage(new LogInPage());
		}
	}

	private async void OnClickCreate(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new CreateUpdate(null));
	}

	private async void OnTapped(object sender, EventArgs e)
	{
		var frame = sender as Frame;
		var selectedWeapon = frame?.BindingContext as Weapon;

		if (selectedWeapon != null)
			await Navigation.PushAsync(new ChangePage(selectedWeapon));
		else 
			return;
	}

	public async void OnLogOut(object sender, EventArgs e)
	{
		bool confirmed = await Application.Current.MainPage.DisplayAlert("Login Out", "Are you sure you want to end this session?", "Yes", "No");
		if (confirmed)
		{
			await apiService.LogoutAsync(SessionManager.Instance.token);

			SessionManager.Instance.ClearToken();
			Application.Current.MainPage = new NavigationPage(new LogInPage());
		}
	}
}