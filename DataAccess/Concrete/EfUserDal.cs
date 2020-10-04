using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.DataAccess.EntityFramework.Repository;
using DataAccess.Abstract;
using DTO.WebApi;
using Entities.Concrete;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfUserDal : EfEntityRepositoryBase<Users, EFContext>, IUserDal
    {
        private readonly IMapper _mapper;
      
        public EfUserDal(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IQueryable<UserAll> GetAll()
        {
            return GetAllBy().ProjectTo<UserAll>(_mapper.ConfigurationProvider);
        }

        public IQueryable<UserGet> GetById(int id)
        {
            return FindBy(x=> x.Id==id).ProjectTo<UserGet>(_mapper.ConfigurationProvider);
        }

        public IQueryable<UserGet> Get(UserGetRequest src)
        {
            return FindBy(x => x.Id == src.id).ProjectTo<UserGet>(_mapper.ConfigurationProvider);
        }

        public IQueryable<UserAll> GetPagedList(GetPagedListRequest src)
        {
            var dt = FindBy(x => ((src.lastName == null) || x.LastName == src.lastName)).ProjectTo<UserAll>(_mapper.ConfigurationProvider);

            return SortBy(src,dt);
        }


        public Users Create(User dt)
        {
            var result = Add(_mapper.Map<Users>(dt));
            _uow.Commit();
            return result;
        }

        public async Task<int> CreateAsync(Users dt)
        {
            var result = await AddAsync(dt);
            await _uow.CommitAsync();
            return result.Id;
        }


        public int Edit(User dt)
        {
            Update(_mapper.Map(dt, FirstBy(p => p.Id == dt.id)));
            return _uow.SaveChanges(); 
        }

        public bool Update(User dt)
        {
            UpdateVoid(dt, dt.id);
            return _uow.Commit();
        }

        public async Task<bool> UpdateAsync(User dt)
        {
            bool result = false;
            
            var rs = await UpdateAsync(dt, x => x.Id == dt.id);
            if (rs != null)
                result= await _uow.CommitAsync();

            return result;
        }

        public Users UpdateModel(Users dt)
        {
            var result = Update(dt);
            _uow.Commit();
            return result;
        }


        public bool DeleteBy(Users dt)
        {
            Delete(dt);
            return _uow.Commit();
        }

        public async Task<bool> FindRemoveByAsync(UserGetRequest src)
        {
            var result = await FindAndRemoveAsync(x => ((src.id == 0) || x.Id == src.id) && ((src.email == null) || x.Email == src.email));
            await _uow.SaveChangesAsync();
            return result;
        }

        public bool RemoveById(int id)
        {
            var result = FindAndRemove(id);
            _uow.SaveChanges();
            return result;
        }

        public dynamic SortBy(GetPagedListRequest src, IQueryable<UserAll> dt)
        {
            switch (src.sort)
            {
                case "firstName" when src.sortDirection == SortDirection.desc.ToString():
                    dt = dt.OrderByDescending(x => x.firstName);
                    break;
                case "firstName" when src.sortDirection == SortDirection.asc.ToString():
                    dt = dt.OrderBy(x => x.firstName);
                    break;
                case "lastName" when src.sortDirection == SortDirection.desc.ToString():
                    dt = dt.OrderByDescending(x => x.lastName);
                    break;
                case "lastName" when src.sortDirection == SortDirection.asc.ToString():
                    dt = dt.OrderBy(x => x.lastName);
                    break;
            }

            return dt;
        }
    }
}
