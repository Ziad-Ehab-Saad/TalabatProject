using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities;
using TalabatCore.Specifications;

namespace TalabatRepository
{
    internal static class SpecificationEvaluation<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            if (spec.Criteria is not null)
            {
                //spec.Criteria = p=>p.id==10
                query = query.Where(spec.Criteria);
            }
            //storeContext.product.where(cre).include(e=>e.employee).Include(b=>b.brands);
            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            return query;
        }
       


        //if (spec.Includes.Count > 0)
        //{
        //    foreach (var includeExpression in spec.Includes)
        //    {
        //        query = query.Include(includeExpression);
        //    }
        //}




    }
}
