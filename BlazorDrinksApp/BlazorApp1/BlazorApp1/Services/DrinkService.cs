using Microsoft.AspNetCore.Components;
using WebApplication1.Model;

namespace BlazorApp1.Services;

public class DrinkService(HttpClient httpClient, NavigationManager navigationManager)
{
    public async Task<IEnumerable<Drink>> GetDrinks()
    {
        var result = await httpClient.GetFromJsonAsync<IEnumerable<Drink>>("api/Drinks");
        return result ?? [];
    }

    public async Task AddDrink(Drink? drink)
    {
        var result = await httpClient.PostAsJsonAsync("api/Drinks", drink);
    }

    public async Task DeleteDrink(Guid id)
    {
        var result = await httpClient.DeleteAsync($"api/Drinks/{id}");
    }

    public async Task<Drink?> GetDrinkById(Guid id)
    {
        var result = await httpClient.GetFromJsonAsync<Drink>($"api/Drinks/{id}");
        return result;
    }

    public async Task UpdateDrink(Guid id, Drink drink)
    {
        var result = await httpClient.PutAsJsonAsync($"api/Drinks/{id}", drink);
    }

    public void ReturnTo(string uri) => navigationManager.NavigateTo(uri);
}