using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using WebShopCMS.Interface;
using WebShopCMS.Models;

namespace WebShopCMS.DataAccess
{
    public class DataAccess<T> : IDataAccess<T> where T : class
    {
        private WebShopDbContext _webShopDbContext = new WebShopDbContext();
        string _className;
        string _tableName;
        public string Include;
        public FilterObject<T> FilterObject;
        private Filter<T> Filter;
        private Expression<Func<T, bool>> WhereLambda;

        public DataAccess()
        {
            _className = typeof(T).Name;
            _tableName = typeof(T).Name + "s";
            Filter = new Filter<T>();
            FilterObject = new FilterObject<T>(Filter);
        }

        public void Add(T instance)
        {
            var set = _webShopDbContext.Set<T>().Add(instance);
            SubmitChanges();
        }

        public ICollection<T> GetAll()
        {
            return (from table in _webShopDbContext.Set<T>()
                    select table).ToList<T>();
        }

        public T GetById(object key)
        {
            return _webShopDbContext.Set<T>().Find(key);
        }

        public void Delete(T instance)
        {
            _webShopDbContext.Set<T>().Remove(instance);
            SubmitChanges();
        }

        public void Update(T instance)
        {
            _webShopDbContext.Entry<T>(instance).State = EntityState.Modified;
            SubmitChanges();
        }

        public void SubmitChanges()
        {
            _webShopDbContext.SaveChanges();
        }


        public ICollection<T> GetLimitedResult(int skip, int take)
        {
            return (from set in _webShopDbContext.Set<T>()
                    select set).Skip(skip).Take(take).ToList<T>();
        }

        public ICollection<T> GetByKeys(string[] keys)
        {

            return null;

        }

        public void AddRange(ICollection<T> instances)
        {
            throw new NotImplementedException();
        }

        public ICollection<T> Go()
        {
            ICollection<T> result = null;
            WhereLambda = FilterObject.GetLambda();
            if (Include != null)
            {
                result = _webShopDbContext.Set<T>().Include(Include).Where(WhereLambda).ToList<T>();
                Include = null;
            }
            else
            {
                result = _webShopDbContext.Set<T>().Where(WhereLambda).ToList<T>();
            }

            Filter.FilterObjects.Clear();

            return result;
        }
    }

    public class Filter<T>
    {

        public List<FilterObject<T>> FilterObjects = new List<FilterObject<T>>();
        public ParameterExpression Param { get; set; }

        public Filter()
        {
            
            Param = Expression.Parameter(typeof(T), typeof(T).Name.First().ToString().ToLower());
        }

    }

    public class FilterObject<T>
    {
        private Filter<T> Filter { get; set; }
        private MemberExpression Left { get; set; }
        private ConstantExpression Right { get; set; }
        private Func<Expression, Expression, Expression> Operator { get; set; }
        private BinaryExpression Condition { get; set; }
        private Dictionary<string, Func<Expression, Expression, Expression>> expressions;

        public FilterObject(Filter<T> Filter)
        {
            expressions = new Dictionary<string, Func<Expression, Expression, Expression>>();
            expressions.Add("==", Expression.Equal);
            expressions.Add("<>", Expression.NotEqual);
            expressions.Add(">", Expression.GreaterThan);
            expressions.Add("<", Expression.LessThan);
            expressions.Add(">=", Expression.GreaterThanOrEqual);
            expressions.Add("<=", Expression.LessThanOrEqual);

            this.Filter = Filter;
        }

        public FilterObject<T> Create(string filter)
        {
            var tuple = GetFilter(filter);

            var filterObject = new FilterObject<T>(this.Filter);

            filterObject.Left = Expression.Property(filterObject.Filter.Param, tuple.Item1.ToString());
            filterObject.Right = Expression.Constant(Convert.ChangeType(tuple.Item3, filterObject.Left.Type));

            var expressionMethod = expressions[tuple.Item2.ToString()];

            filterObject.Condition = (BinaryExpression)expressionMethod(filterObject.Left, filterObject.Right);

            Filter.FilterObjects.Add(filterObject);

            return new FilterObject<T>(this.Filter);
        }

        public FilterObject<T> Or(string filter)
        {
            var filterObject = new FilterObject<T>(this.Filter);

            var tuple = GetFilter(filter);

            filterObject.Left = Expression.Property(filterObject.Filter.Param, tuple.Item1.ToString());
            filterObject.Right = Expression.Constant(Convert.ChangeType(tuple.Item3, filterObject.Left.Type));

            filterObject.Operator = Expression.Or;
            var expressionMethod = expressions[tuple.Item2.ToString()];

            filterObject.Condition = (BinaryExpression)expressionMethod(filterObject.Left, filterObject.Right);

            Filter.FilterObjects.Add(filterObject);
            return new FilterObject<T>(this.Filter);

        }

        public FilterObject<T> And(string filter)
        {
            var filterObject = new FilterObject<T>(this.Filter);

            var tuple = GetFilter(filter);

            filterObject.Left = Expression.Property(filterObject.Filter.Param, tuple.Item1.ToString());
            filterObject.Right = Expression.Constant(Convert.ChangeType(tuple.Item3, filterObject.Left.Type));

            filterObject.Operator = Expression.And;
            var expressionMethod = expressions[tuple.Item2.ToString()];

            filterObject.Condition = (BinaryExpression)expressionMethod(filterObject.Left, filterObject.Right);

            Filter.FilterObjects.Add(filterObject);
            return new FilterObject<T>(this.Filter);
        }

        public Expression<Func<T, bool>> GetLambda()
        {
            Expression expr = null;

            this.Filter.FilterObjects.ForEach(f =>
            {
                if (expr == null)
                {
                    expr = f.Condition;
                }

                if (f.Operator != null)
                {
                    expr = f.Operator(expr, f.Condition);
                }

            });

            var a = Expression.Lambda<Func<T, bool>>(expr, this.Filter.Param);

            return a;

        }

        public Tuple<object, object, object> GetFilter(string filter)
        {
            string op = null;
            expressions.Keys.ToList<string>().ForEach(k =>
            {
                if (filter.Contains(k))
                {
                    op = k;
                }
            });

            var left = filter.Substring(0, filter.IndexOf(op)).Trim();
            var right = filter.Substring(filter.IndexOf(op) + op.Length, filter.Length - filter.IndexOf(op) - op.Length).Trim();

            return new Tuple<object, object, object>(left, op, right);
        }


    }
}