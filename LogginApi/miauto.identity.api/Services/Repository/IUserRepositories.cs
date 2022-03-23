using miauto.identity.api.Models.Entity;
using miauto.identity.api.Models.Request;
using miauto.identity.api.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace miauto.identity.api.Services.Repository
{
    public interface IUserRepositories
    {
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetUser(int id);
        Task<bool> InsertDefaultUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
        Task<UserResponse> Auth(AuthRequest authRequest);
    }
}
