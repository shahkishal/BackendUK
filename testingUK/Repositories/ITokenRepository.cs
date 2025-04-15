using Microsoft.AspNetCore.Identity;

namespace testingUK.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);

    }
}
