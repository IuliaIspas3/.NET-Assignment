using System.Security.Claims;
using BlazorAssig.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorAssig.Auth;

public class CustomAuthProvider : AuthenticationStateProvider
{
    
    private readonly IAuthService authService;

    public CustomAuthProvider(IAuthService authService)
    {
        this.authService = authService;
        authService.OnAuthStateChanged += AuthStateChanged;
    }
    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal principal = await authService.GetAuthAsync();
        
        return new AuthenticationState(principal);
    }
    
    private void AuthStateChanged(ClaimsPrincipal principal)
    {
        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(principal)
            )
        );
    }
}