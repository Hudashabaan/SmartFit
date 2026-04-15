using MediatR;
using Microsoft.AspNetCore.Identity;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.Authentication.Commands.Login;
using SmartFit.Domain.Entities;
using System;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(
        UserManager<ApplicationUser> userManager,
        IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            throw new Exception("Invalid email or password");

        var result = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!result)
            throw new Exception("Invalid email or password");

        // 🔐 هنا بنولد التوكن
        var token = _jwtService.GenerateToken(user.Id, user.Email);

        return token;
    }
}
