using DTO.WebApi;
using Entities.Concrete;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal
    {
        IQueryable<UserAll> GetAll();
        IQueryable<UserGet> GetById(int id);
        IQueryable<UserGet> Get(UserGetRequest src);
        IQueryable<UserAll> GetPagedList(GetPagedListRequest src);
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
