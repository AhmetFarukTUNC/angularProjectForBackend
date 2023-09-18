using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext>:IEntityRepository<TEntity> where TEntity:class,IEntity,new() where TContext:DbContext,new()
    {
        public void Add(TEntity entity)

        {
            //IDisposible Pattern implementation
            using (TContext context = new TContext())
            {

                var addedEntity = context.Entry(entity); // gelen veriyi veri tabanındaki uygun veriyle eşleştirir.
                addedEntity.State = EntityState.Added; //veri tabanına ekler.
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            //IDisposible Pattern implementation
            using (TContext context = new TContext())
            {

                var deletedEntity = context.Entry(entity); // gelen veriyi veri tabanındaki uygun veriyle eşleştirir.
                deletedEntity.State = EntityState.Deleted; //veri tabandan ilgili veriyi siler.
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            //IDisposible Pattern implementation
            using (TContext context = new TContext())
            {

                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            //IDisposible Pattern implementation
            using (TContext context = new TContext())
            {

                var updatedEntity = context.Entry(entity); // gelen veriyi veri tabanındaki uygun veriyle eşleştirir.
                updatedEntity.State = EntityState.Modified; //veri tabanın ilgili verisini günceller.
                context.SaveChanges();
            }
        }

    }
}
