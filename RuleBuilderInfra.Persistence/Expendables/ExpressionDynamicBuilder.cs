using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Persistence
{
    public static class ExpressionDynamicBuilder
    {
        private static readonly Dictionary<int, Delegate> Cache = new Dictionary<int, Delegate>();

        public static Func<T, bool> GetPredicate<T>(Expression<Func<T, bool>> expression)
        {
            var key = expression.GetHashCode();
            if (Cache.TryGetValue(key, out Delegate cachedDelegate))
            {
                return (Func<T, bool>)cachedDelegate;
            }
            var compiledDelegate = expression.Compile();

            Cache[key] = compiledDelegate;

            return compiledDelegate;
        }
        public static Expression<Func<T, bool>> CreateEqualExpression<T>(string propertyName, object value)
        {
            var param = Expression.Parameter(typeof(T), "P");

            var member = Expression.Property(param, propertyName);

            var constant = Expression.Constant(value);

            var body = Expression.Equal(member, constant);

            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        public static BinaryExpression CreateEqualBinaryExpression<T>(string propertyName, object value, Type typeValue)
        {
            var param = Expression.Parameter(typeof(T), "P");

            var member = Expression.Property(param, propertyName);

            var constant = Expression.Constant(value, typeValue);

            return Expression.Equal(member, constant);
        }

        public static Expression<Func<T, bool>> NotEqualExpression<T>(string propertyName, object value)
        {
            var param = Expression.Parameter(typeof(T), "P");

            var member = Expression.Property(param, propertyName);

            var constant = Expression.Constant(value);

            var body = Expression.NotEqual(member, constant);

            return Expression.Lambda<Func<T, bool>>(body, param);
        }



        public static BinaryExpression NotEqualBinaryExpression<T>(string propertyName, object value, Type typeValue)
        {
            var param = Expression.Parameter(typeof(T), "P");

            var member = Expression.Property(param, propertyName);

            var constant = Expression.Constant(value, typeValue);

            var body = Expression.NotEqual(member, constant);

            return body;
        }

        public static BinaryExpression GreaterThanBinaryExpression<T>(string propertyName, object value, Type typeValue)
        {
            var param = Expression.Parameter(typeof(T), "P");

            var member = Expression.Property(param, propertyName);

            var constant = Expression.Constant(value, typeValue);

            var body = Expression.GreaterThan(member, constant);

            return body;
        }

        public static BinaryExpression GreaterThanEqualBinaryExpression<T>(string propertyName, object value, Type typeValue)
        {
            var param = Expression.Parameter(typeof(T), "P");

            var member = Expression.Property(param, propertyName);

            var constant = Expression.Constant(value, typeValue);

            var body = Expression.GreaterThanOrEqual(member, constant);

            return body;
        }

        public static BinaryExpression LessThanBinaryExpression<T>(string propertyName, object value, Type typeValue)
        {
            var param = Expression.Parameter(typeof(T), "P");

            var member = Expression.Property(param, propertyName);

            var constant = Expression.Constant(value, typeValue);

            var body = Expression.LessThan(member, constant);

            return body;
        }

        public static BinaryExpression LessThanEqualBinaryExpression<T>(string propertyName, object value, Type typeValue)
        {
            var param = Expression.Parameter(typeof(T), "P");

            var member = Expression.Property(param, propertyName);

            var constant = Expression.Constant(value, typeValue);

            var body = Expression.LessThanOrEqual(member, constant);

            return body;
        }

        public static Expression ContainsExpression<T>(string propertyName, string value, Type typeValue)
        {
            var param = Expression.Parameter(typeof(T), "p");

            var member = Expression.Property(param, propertyName);

            var constant = Expression.Constant(value, typeValue);

            var body = Expression.Call(member, "Contains", Type.EmptyTypes, constant);

            return body;
        }


        public static MethodCallExpression StartsWithExpression<T>(string propertyName, string startsWith, Type typeValue)
        {

            var startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });

            var p = Expression.Parameter(typeof(T), "p"); // p => ...
            var memberExpression = Expression.Property(p, propertyName); // ... p.Propery

            var startsWithValue = Expression.Constant(startsWith, typeValue); // "some prefix"

            var startsWithExpression = Expression.Call(memberExpression, startsWithMethod, startsWithValue); // ... p.Property.StartsWith("some prefix")

            return startsWithExpression;
        }

        //public static Expression<Func<T, bool>> StartsWithExpression<T>(this IQueryable<T> input, string propertyName, string startsWith)
        //{
        //    var elemenType = input.ElementType;
        //    var property = elemenType.GetProperty(propertyName);
        //    if (property == null)
        //        throw new ArgumentException($"There is no property {propertyName} in {elemenType.Name}");
        //    if (property.PropertyType != typeof(string))
        //        throw new ArgumentException($"Expected string property but actual type is {property.PropertyType.Name}");

        //    var startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });

        //    var p = Expression.Parameter(elemenType, "p"); // p => ...
        //    var memberExpression = Expression.Property(p, property); // ... p.Propery
        //    var startsWithValue = Expression.Constant(startsWith); // "some prefix"
        //    var startsWithExpression = Expression.Call(memberExpression, startsWithMethod, startsWithValue); // ... p.Property.StartsWith("some prefix")
        //    var result = Expression.Lambda<Func<T, bool>>(startsWithExpression, p); // p => p.Property.StartsWith("some prefix")

        //    return result;
        //}




        public static Expression<Func<T, bool>> EqualExpression<T>(IDictionary<string, object> filters)
        {
            var param = Expression.Parameter(typeof(T), "P");

            Expression? body = null;

            foreach (var pair in filters)
            {
                var member = Expression.Property(param, pair.Key);

                var constant = Expression.Constant(pair.Value);

                var expression = Expression.Equal(member, constant);

                body = body == null ? expression : Expression.AndAlso(body, expression);
            }

            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        public static Expression<Func<T, bool>> InExpression<T>(string propertyName, object value)
        {
            var param = Expression.Parameter(typeof(T), "p");

            var member = Expression.Property(param, propertyName);

            var propertyType = ((PropertyInfo)member.Member).PropertyType;

            var constant = Expression.Constant(value);

            var body = Expression.Call(typeof(Enumerable), "Contains", new[] { propertyType }, constant, member);

            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        public static Expression<Func<T, bool>> NestedExpression<T>(string propertyName, object value)
        {
            var param = Expression.Parameter(typeof(T), "p");
            Expression member = param;
            foreach (var namePart in propertyName.Split('.'))
            {
                member = Expression.Property(member, namePart);
            }
            var constant = Expression.Constant(value);
            var body = Expression.Equal(member, constant);
            return Expression.Lambda<Func<T, bool>>(body, param);
        }


        public static Expression<Func<T, bool>> BetweenExpression<T>(
            string propertyName,
            object lowerValue,
            object upperValue
        )
        {
            var param = Expression.Parameter(typeof(T), "p");
            var member = Expression.Property(param, propertyName);
            var body = Expression.AndAlso(
                Expression.GreaterThanOrEqual(member, Expression.Constant(lowerValue)),
                Expression.LessThanOrEqual(member, Expression.Constant(upperValue))
            );
            return Expression.Lambda<Func<T, bool>>(body, param);
        }
    }
}
