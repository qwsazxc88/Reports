using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core;
using Reports.Core.Dao;
using LinqToExcel;
namespace Reports.ExcelHelper
{
    public static class DeductionConverter
    {
        public static List<Deduction> ConvertFromFile(string path)
        {
            List<Deduction> result= new List<Deduction>();
            IUserDao UserDao = Ioc.Resolve<IUserDao>();
            IDeductionKindDao DeductionKindDao = Ioc.Resolve<IDeductionKindDao>();
            IDeductionTypeDao DeductionTypeDao = Ioc.Resolve<IDeductionTypeDao>();
            var kinds = DeductionKindDao.LoadAll();
            var excel = new ExcelQueryFactory(path);
            var names = excel.GetWorksheetNames();
            var ws = excel.Worksheet(names.First());
            var data = ws.ToList();
            foreach (var el in data) 
            {
                var deduction = new Deduction()
                {
                    DeductionDate = DateTime.Parse(el[4].Value.ToString()),
                    User=UserDao.FindByCnilc(el[1].Value.ToString()),
                    Sum=decimal.Parse(el[2].Value.ToString()),
                    Type=DeductionTypeDao.Load(1),
                    Kind=kinds.Where(x=>x.Name.Contains(el[3].Value.ToString().Substring(el[3].Value.ToString().LastIndexOf('#')))).First(),
                    EditDate=DateTime.Now
                };
                result.Add(deduction);
            }
            return result;
        }
    }
}
