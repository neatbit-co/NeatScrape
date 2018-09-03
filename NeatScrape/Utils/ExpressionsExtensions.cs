using System;
using System.Linq.Expressions;

namespace NeatScrape.Utils
{
    internal static class ExpressionsExtensions
    {
        public static string GetName<T, TProp>(this Expression<Func<T, TProp>> exp)
        {
            if (exp.Body is MemberExpression body)
            {
                return body.Member.Name;
            }

            var unaryBody = (UnaryExpression)exp.Body;
            body = unaryBody.Operand as MemberExpression;

            return body?.Member.Name;
        }

        public static Type GetObjectType<T>(this Expression<Func<T, object>> expr)
        {
            if ((expr.Body.NodeType != ExpressionType.Convert) && (expr.Body.NodeType != ExpressionType.ConvertChecked))
            {
                return expr.Body.Type;
            }

            if (expr.Body is UnaryExpression unary)
            {
                return unary.Operand.Type;
            }

            return expr.Body.Type;
        }
    }
}
