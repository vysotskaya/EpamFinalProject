using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Helper
{
    public class ExpressionCreater<T>
    {
        public Expression<Func<T, bool>> GetExpression(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            parameters = parameters.ToList();
            var exprParams = new List<ParameterExpression>();
            var parameter = Expression.Parameter(typeof (T), "x");
            exprParams.Add(parameter);
            var prop = Expression.Property(parameter, parameters.ElementAt(0).Key);
            var constant = Expression.Constant(parameters.ElementAt(0).Value);
            var exprStart = Expression.Equal(prop, constant);
            BinaryExpression exprRes = null;
            for (var i = 1; i < parameters.Count(); i++)
            {
                parameter = Expression.Parameter(typeof(T), "x");
                //exprParams.Add(parameter);
                prop = Expression.Property(parameter, parameters.ElementAt(i).Key);
                constant = Expression.Constant(parameters.ElementAt(i).Value);
                var expr = Expression.Equal(prop, constant);
                exprRes = Expression.AndAlso(i == 1 ? exprStart : exprRes, expr);
            }

            var lambda = Expression.Lambda<Func<T, bool>>(exprRes ?? exprStart, exprParams.ToArray());
            return lambda;
        }
    }
}
