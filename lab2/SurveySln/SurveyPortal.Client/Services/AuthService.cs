using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Text.Json;

namespace SurveyPortal.Client.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<bool> Login(string email, string password)
        {
            var loginModel = new { email = email, password = password };

            var response = await _httpClient.PostAsJsonAsync("login", loginModel);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<JsonElement>();
                var token = responseContent.GetProperty("accessToken").GetString();

                await _localStorage.SetItemAsync("authToken", token);
                await _localStorage.SetItemAsync("userEmail", email);

                ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(email, token);

                return true;
            }
            return false;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("userEmail");
            ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
        }
    }
}