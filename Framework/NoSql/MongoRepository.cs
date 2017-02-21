using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;

namespace ArkApplication.Framework.NoSql
{
      /// <summary>
    /// Deals with entities in MongoDb.
    /// </summary>
    /// <typeparam name="T">The type contained in the repository.</typeparam>
    /// <typeparam name="TKey">The type used for the entity's Id.</typeparam>
    public class MongoRepository<T, TKey> : INoSqlRepository<T, TKey>
        where T : IEntity<TKey>
    {

         #region Filed

         private readonly IMongoDatabase _database;

         #endregion

         #region Constr

         public MongoRepository(IConfiguration config)
         {
              _database = Connect(config["Nosql:Mongo"], config["Nosql:Db"]);
         }

         #endregion

         #region Method

         public IEnumerable<T> Collection()
         {
             return _database.GetCollection<T>(typeof(T).Name).AsQueryable();
         }

         public T GetById(TKey id)
         {

             var query = Builders<T>.Filter.Eq(e => e.Id, id);
             var temp = _database.GetCollection<T>(typeof(T).Name).Find(query).ToListAsync();
             return temp.Result.FirstOrDefault();
         }

         public T Add(T entity)
         {
             _database.GetCollection<T>(typeof(T).Name).InsertOneAsync(entity);
             return entity;
         }

         public void Add(IEnumerable<T> entities)
         {
             _database.GetCollection<T>(typeof(T).Name).InsertManyAsync(entities);
         }

         public T Update(T entity)
         {
             var query = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
             _database.GetCollection<T>(typeof(T).Name).ReplaceOneAsync(query, entity);
             return entity;
         }

         public void Update(IEnumerable<T> entities)
         {
            foreach(var item in entities)
            {
                this.Update(item);
            }
         }

         public void Delete(TKey id)
         {
             var query = Builders<T>.Filter.Eq(e => e.Id, id);
             _database.GetCollection<T>(typeof(T).Name).DeleteOneAsync(query);
         }

         public void Delete(T entity)
         {
             this.Delete(entity.Id);
         }

         public void Delete(Expression<Func<T, bool>> predicate)
         {
             foreach (T entity in this.Collection().AsQueryable<T>().Where(predicate))
             {
                this.Delete(entity.Id);
             }
         }

         public void DeleteAll()
         {
             foreach(var item in this.Collection())
             {
                 this.Delete(item);
             }
         }

         public long Count()
         {
             return this.Collection().Count();
         }

         public bool Exists(Expression<Func<T, bool>> predicate)
         {
             return this.Collection().AsQueryable<T>().Any(predicate);
         }

         #endregion

         #region Extention

         private IMongoDatabase Connect(string connection, string db)
         {
             var client = new MongoClient(connection);
             var database = client.GetDatabase(db);
             return database;
         }

         #endregion

        #region IQueryable<T>

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator&lt;T&gt; object that can be used to iterate through the collection.</returns>
        public virtual IEnumerator<T> GetEnumerator()
        {
            return this.Collection().AsQueryable<T>().GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.Collection().AsQueryable<T>().GetEnumerator();
        }

        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression tree associated with this instance of IQueryable is executed.
        /// </summary>
        public virtual Type ElementType
        {
            get { return this.Collection().AsQueryable<T>().ElementType; }
        }

        /// <summary>
        /// Gets the expression tree that is associated with the instance of IQueryable.
        /// </summary>
        public virtual Expression Expression
        {
            get { return this.Collection().AsQueryable<T>().Expression; }
        }

        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        public virtual IQueryProvider Provider
        {
            get { return this.Collection().AsQueryable<T>().Provider; }
        }

        #endregion
    }

    public class MongoRepository<T> : MongoRepository<T, string>, INoSqlRepository<T>
        where T : IEntity<string>
    {
        public MongoRepository(IConfiguration config)
            : base(config) { }
    }


}