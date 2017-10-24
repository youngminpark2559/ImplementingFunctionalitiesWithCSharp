using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLotDAL.Repos
{
   //Implemented repository pattern, creating layers composed of IRepo / generic BaseRepo implementing IRepo, which accesses to my context class(AutoLotEntities) which accesses to base DbContext which communicates to database / client layer of classes(CustomerRepo, OrderRepo,...) invoking members in IRepo.


    //This interface exposes unimplemented components for CRUD both in synchronous and asynchronous versions.
    interface IRepo<T>
    {
        int Add(T entity);
        Task<int> AddAsync(T entity);
        int AddRange(IList<T> entities);
        Task<int> AddRangeAsync(IList<T> entities);
        int Save(T entity);
        Task<int> SaveAsync(T entity);
        int Delete(int id);
        Task<int> DeleteAsync(int id);
        int Delete(T entity);
        Task<int> DeleteAsync(T entity);
        T GetOne(int? id);
        Task<T> GetOneAsync(int? id);
        List<T> GetAll();
        Task<List<T>> GetAllAsync();

        //I can pass in SQL query into these members,
        //and these ones load and track DbSet<T> of the context class.
        //I can call SQL within context by using these one but using LINQ can replace these functionality.
        List<T> ExecuteQuery(string sql);
        List<T> ExecuteQuery(string sql, object[] sqlParametersObjects);
        Task<List<T>> ExecuteQueryAsync(string sql);
        Task<List<T>> ExecuteQueryAsync(string sql, object[] sqlParametersObjects);
    }
}
