using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities;

namespace TalabatCore.Specifications
{
    public interface ISpecification<T> where T: BaseEntity
    {
        /// <summary>
        /// where  => Expression<Func<T,bool>>
        /// includes =>Expression<Func<T,object>>
        /// </summary>

        public Expression<Func<T,bool>> Criteria { get; set; }

        public List<Expression<Func<T,object>>> Includes { get; set; }



    }
}
