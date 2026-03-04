using EcommerceDev.Application.Common;
using EcommerceDev.Core.Repositories;
using EcommerceDev.Core.Services;

namespace EcommerceDev.Application.Commands.Auth.Login;

public class LoginCommandHandler : IHandler<LoginCommand, ResultViewModel<LoginResponse>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginCommandHandler(
        ICustomerRepository customerRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService)
    {
        _customerRepository = customerRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<ResultViewModel<LoginResponse>> HandleAsync(LoginCommand request)
    {
        // Find customer by email
        var customer = await _customerRepository.GetCustomerByEmailAsync(request.Email);
        if (customer == null)
        {
            return ResultViewModel<LoginResponse>.Error("Invalid email or password");
        }

        // Verify password
        if (!_passwordHasher.VerifyPassword(request.Password, customer.PasswordHash))
        {
            return ResultViewModel<LoginResponse>.Error("Invalid email or password");
        }

        // Check if account is active
        if (!customer.IsActive)
        {
            return ResultViewModel<LoginResponse>.Error("Account is inactive");
        }

        // Update last login date
        customer.LastLoginDate = DateTime.UtcNow;
        await _customerRepository.UpdateCustomerAsync(customer);

        // Generate JWT token
        var token = _jwtTokenService.GenerateToken(customer);

        var response = new LoginResponse
        {
            Token = token,
            CustomerId = customer.Id,
            FullName = customer.FullName,
            Email = customer.Email
        };

        return ResultViewModel<LoginResponse>.Success(response);
    }
}
