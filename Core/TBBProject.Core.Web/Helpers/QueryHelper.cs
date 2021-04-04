using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.ComponentModel;
using System.Globalization;
using System.Collections;
using Newtonsoft.Json.Linq;

namespace TBBProject.Core.Web.Helpers
{
    #region enums
    public enum EnumSortDirection
    {
        [Description("Asc")]
        Asc = 0,

        [Description("Desc")]
        Desc = 1,
    }

    public enum EnumFilterOperator
    {
        [Description("=")]
        eq,

        [Description("!=")]
        neq,

        [Description("<")]
        lt,

        [Description("<=")]
        lte,

        [Description(">")]
        gt,

        [Description(">=")]
        gte,

        [Description("StartsWith")]
        startswith,

        [Description("EndsWith")]
        endswith,

        [Description("Contains")]
        contains
    }

    #endregion



    #region models
    public class Filter
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public object Value { get; set; }
    }

    public class Sort
    {
        public string Field { get; set; }
        public string Direction { get; set; }
    }
    public class GridSearch
    {
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
        public JObject ExtraParams { get; set; } = new JObject();
        public List<Filter> Filters { get; set; } = new List<Filter>();
        public List<Sort> Sorts { get; set; } = new List<Sort>();
    }

    public class DataGridDto
    {
        public IEnumerable ListDto;
        public int TotalCount;
    }
    #endregion

    #region MyRegion
    public static class MyExtensions
    {

        public static IQueryable<T> GridSearchApply<T>(this IQueryable<T> query, GridSearch gridSearch, out int totalCount)
        {

            //https://github.com/StefH/System.Linq.Dynamic.Core

            foreach (var filter in gridSearch.Filters)
            {
                if (filter.Value != null && filter.Value.ToString().Length != 0)
                {
                    if (filter.Operator == "IN")
                    {
                        Guid[] values = ( (IEnumerable)filter.Value ).Cast<object>()
                             .Select(x => new Guid(x.ToString()))
                             .ToArray();
                        string[] field = filter.Field.Split(".");
                        var conditionField = "";
                        for (int i = 0; i < field.Length; i++)
                        {
                            if (i == field.Length - 1)
                            {
                                conditionField = conditionField + $"@0.Contains({field[i]})";
                                for (int x = 0; x < field.Length - 1; x++)
                                {
                                    conditionField = conditionField + ")";
                                }
                            }
                            else
                            {
                                conditionField = conditionField + $"{field[i]}.Any(";
                            }
                        }
                        query = query.Where(conditionField, values);
                    }
                    else if (filter.Operator == "Equal")
                    {
                        query = query.Where($"{filter.Field}.Equals(@0)", filter.Value);
                    }
                    else if (filter.Operator == "StartsWith")
                    {
                        query = query.Where($"{filter.Field}.StartsWith(@0)", filter.Value);
                    }
                    else if (filter.Operator == "EndsWith")
                    {
                        query = query.Where($"{filter.Field}.EndsWith(@0)", filter.Value);
                    }
                    else if (filter.Operator == "Contains")
                    {
                        query = query.Where($"{filter.Field}.Contains(@0)", filter.Value);
                    }
                    else
                    {
                        query = query.Where($"{filter.Field} {filter.Operator} @0", filter.Value.ToString() == "null" ? null : filter.Value);
                    }
                }
                else
                {
                    query = query.Where($"{filter.Field}=null");
                }
            }
            totalCount = query.Count();
            foreach (var sort in gridSearch.Sorts)
            {
                query = query.OrderBy($"{sort.Field} {sort.Direction}");
            }
            query = query.Skip(( gridSearch.PageIndex - 1 ) * gridSearch.PageSize).Take(gridSearch.PageSize);
            return query;
        }

        public static IQueryable<T> GridSearchApplyNoFilter<T>(this IQueryable<T> query, GridSearch gridSearch, out int totalCount)
        {



            totalCount = query.Count();

            foreach (var sort in gridSearch.Sorts)
            {
                query = query.OrderBy($"{sort.Field} {sort.Direction}");
            }
            query = query.Skip(( gridSearch.PageIndex - 1 ) * gridSearch.PageSize).Take(gridSearch.PageSize);
            return query;
        }
    }
    #endregion
}
