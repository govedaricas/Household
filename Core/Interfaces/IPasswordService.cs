using Infrastructure.Entities;

namespace Application.Interfaces
{
    public interface IPasswordService
    {
        byte[] HashPassword(string password, out byte[] salt);
        bool VerifyPassword(User user, string password);
    }
}