using Auth.Application.Interfaces;
using System.Text;

namespace Auth.Infrastructure.Services;

public class PasswordGenerator : IPasswordGenerator
{
    private const string ValidPasswordChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-_";
    private const int PasswordLength = 16;

    public string GeneratePassword()
    {
        var password = new StringBuilder();
        var random = new Random();

        for (var i = 0; i < PasswordLength; i++)
        {
            var index = random.Next(ValidPasswordChars.Length);
            password.Append(ValidPasswordChars[index]);
        }

        return password.ToString();
    }
}