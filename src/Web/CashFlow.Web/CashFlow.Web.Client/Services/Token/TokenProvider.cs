using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace CashFlow.Web.Client.Services.Token;

public class TokenProvider : ITokenProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly NavigationManager _navigationManager;

    public TokenProvider(ILocalStorageService localStorage, NavigationManager navigationManager)
    {
        _localStorage = localStorage;
        _navigationManager = navigationManager;
    }

    public async Task<string?> Get()
    {
        var token = await _localStorage.GetItemAsync<string>("asp_token");

        if (string.IsNullOrEmpty(token))
        {
            _navigationManager.NavigateTo("/login", forceLoad: true);

            return null;
        }

        return token;
    }
}
