using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core;
using Newtonsoft.Json;
namespace Reports.Presenters
{
    
    public class GridDefinition
    {
        public GridDefinition()
        {
            enableColumnResizing = true;
            enableFiltering = true;
            enableVerticalScrollbar = 0;
            enableHorizontalScrollbar = 0;
        }
        [JsonProperty]
        public bool enableColumnResizing { get; set; }
        [JsonProperty]
        public int enableVerticalScrollbar {get;set;}
        [JsonProperty]
        public int enableHorizontalScrollbar { get; set; }
        [JsonProperty]
        public bool enableFiltering {get;set;}
        [JsonProperty]
        public ngGridRow[] columnDefs { get; set; }
        [JsonProperty]
        public object data { get; set; }
        [JsonProperty]
        public bool IsGridEditable { get; set; }
    }
    public static class UIGrid_Helper
    {
        public static GridDefinition GetGridDefinition<T>(IList<T> model)
        {
            var result = new GridDefinition();
            result.columnDefs = GetColumnDef<T>().ToArray();
            result.data = model;
            return result;
        }
        private static Dictionary<Type, List<ngGridRow>> Cache = new Dictionary<Type, List<ngGridRow>>();
        private static List<ngGridRow> GetColumnDef<T>()
        {
            if (Cache.Any(x => x.Key == typeof(T))) return Cache[typeof(T)];
            List<ngGridRow> columns = new List<ngGridRow>();
            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                var attrs = prop.GetCustomAttributes(typeof(ngGridRowAttribute), false);
                if (attrs != null && attrs.Any())
                {
                    var attr = (ngGridRowAttribute)attrs.First();
                    
                    columns.Add(new ngGridRow(attr));                   
                }
            }
            Cache.Add(typeof(T), columns);
            return columns;
        }
    }
}
