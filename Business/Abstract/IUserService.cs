using DTO.WebApi;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IEnumerable<UserAll>> GetAllAsync();
        Task<UserGet> GetByIdAsync(int id);
        Task<UserGet> GetAsync(UserGetRequest src);
        Task<GetPagedList> GetPagedListAsync(GetPagedListRequest src);
        Users Create(User dt);
        Task<int> CreateAsync(Users dt);
        int Edit(User dt);
        bool Update(User dt);
        Users Update(Users dt);
        Task<bool> UpdateAsync(User dt);
        bool DeleteBy(Users dt);
        Task<bool> FindRemoveByAsync(UserGetRequest src);
        bool RemoveById(int id);
    }
}
