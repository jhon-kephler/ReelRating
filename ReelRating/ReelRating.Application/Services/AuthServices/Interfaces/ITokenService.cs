using ReelRating.Domain.Entities;

namespace ReelRating.Application.Services.AuthServices.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Customer user);
    }
}