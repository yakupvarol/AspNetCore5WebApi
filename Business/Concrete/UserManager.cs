using Business.Abstract;
using DataAccess.Abstract;
using DTO.WebApi;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task<IEnumerable<UserAll>> GetAllAsync()
        {
            return await _userDal.GetAll().ToListAsync();
        }

        public async Task<UserGet> GetByIdAsync(int id)
        {
            return await _userDal.GetById(id).FirstOrDefaultAsync();
        }

        public async Task<UserGet> GetAsync(UserGetRequest src)
        {
            return await _userDal.Get(src).FirstOrDefaultAsync();
        }

        public async Task<GetPagedList> GetPagedListAsync(GetPagedListRequest src)
        {
            var dt = _userDal.GetPagedList(src);
            var data = await dt.Skip((src.page - 1) * src.rowCount).Take(src.rowCount).ToListAsync();
            var count = await dt.CountAsync();

            return new GetPagedList { items = data, itemCount = count };
        }

        public Users Create(User dt)
        {
            return _userDal.Create(dt);
        }

        public async Task<int> CreateAsync(Users dt)
        {
            return  await _userDal.CreateAsync(dt);
        }

        public int Edit(User dt)
        {
            return _userDal.Edit(dt);
        }

        public bool Update(User dt)
        {
            return _userDal.Update(dt);
        }

        public Users Update(Users dt)
        {
            return _userDal.Update(dt);
        }

        public async Task<bool> UpdateAsync(User dt)
        {
            return await _userDal.UpdateAsync(dt);
        }

        public bool DeleteBy(Users dt)
        {
            return _userDal.DeleteBy(dt);
        }

        public Task<bool> FindRemoveByAsync(UserGetRequest src)
        {
            return _userDal.FindRemoveByAsync(src);
        }

        public bool RemoveById(int id)
        {
            return _userDal.RemoveById(id);
        }
    }
}
