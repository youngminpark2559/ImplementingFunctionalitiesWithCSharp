


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLotDAL.EF;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace AutoLotDAL.Repos
{
    public abstract class BaseRepo<T> where T : class, new()
    {
        //Creating my context class affects bad to performance,
        //because it exchanges signals between base DbContext and database.
        //This is resolved in several techniques, one of them is using singleton option for BaseRepo(?)
        public AutoLotEntities Context { get; } = new AutoLotEntities();
        protected DbSet<T> Table;

        internal int SaveChanges()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //Thrown when there is a concurrency error
                //for now, just rethrow the exception
                throw;
            }
            catch (DbUpdateException ex)
            {
                //Thrown when database update fails
                //Examine the inner exception(s) for additional
                //details and affected objects
                //for now, just rethrow the exception
                throw;
            }
            catch (CommitFailedException ex)
            {
                //handle transaction failures here
                //for now, just rethrow the exception
                throw;
            }
            catch (Exception ex)
            {
                //some other exception happened and should be handled
                throw;
            }
        }

        internal async Task<int> SaveChangesAsync()
        {
            try
            {
                return await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //Thrown when there is a concurrency error
                //for now, just rethrow the exception
                throw;
            }
            catch (DbUpdateException ex)
            {
                //Thrown when database update fails
                //Examine the inner exception(s) for additional
                //details and affected objects
                //for now, just rethrow the exception
                throw;
            }
            catch (CommitFailedException ex)
            {
                //handle transaction failures here
                //for now, just rethrow the exception
                throw;
            }
            catch (Exception ex)
            {
                //some other exception happened and should be handled
                throw;
            }
        }






        //GetOne(), GetOneAsync() wrap Find(), FindAsync() of DbSet<T>.
        public T GetOne(int? id) => Table.Find(id);

        public Task<T> GetOneAsync(int? id) => Table.FindAsync(id);

        public List<T> GetAll() => Table.ToList();

        public Task<List<T>> GetAllAsync() => Table.ToListAsync();


        //These are for last 4 method in IRepo.
        //Theae deal with raw SQL string, so that I should be careful about security against something like SQL injection.
        public List<T> ExecuteQuery(string sql) => Table.SqlQuery(sql).ToList();

        public Task<List<T>> ExecuteQueryAsync(string sql)
          => Table.SqlQuery(sql).ToListAsync();

        public List<T> ExecuteQuery(string sql, object[] sqlParametersObjects)
          => Table.SqlQuery(sql, sqlParametersObjects).ToList();

        public Task<List<T>> ExecuteQueryAsync(string sql, object[] sqlParametersObjects)
          => Table.SqlQuery(sql).ToListAsync();








        //Implementation for Add group 
        public int Add(T entity)
        {
            Table.Add(entity);
            return SaveChanges();
        }
        public Task<int> AddAsync(T entity)
        {
            Table.Add(entity);
            return SaveChangesAsync();
        }

        public int AddRange(IList<T> entities)
        {
            Table.AddRange(entities);
            return SaveChanges();
        }
        public Task<int> AddRangeAsync(IList<T> entities)
        {
            Table.AddRange(entities);
            return SaveChangesAsync();
        }



        //For update records.
        public int Save(T entity)
        {
            //First, set EntityState of the entity to Modified.
            Context.Entry(entity).State = EntityState.Modified;
            
            //And call.
            return SaveChanges();
        }

        public Task<int> SaveAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return SaveChangesAsync();
        }




        //For delete records.
        public int Delete(T entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        public Task<int> DeleteAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            return SaveChangesAsync();
        }






        //These are implementation of Dispose() from IDisposable.
        //These ensure any resources are released.
        bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                Context.Dispose();
                // Free any managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }
    }

}

