using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;
using NHibernate.Linq;
namespace Reports.Core.Dao.Impl
{
    public class ManualDeductionDao:DefaultDao<ManualDeduction>,IManualDeductionDao
    {
        public ManualDeductionDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<ManualDeductionDto> GetDocuments(User CurrentUser, string UserName, Department department)
        {
            var query = Session.Query<ManualDeduction>().Where(x=>!x.DeleteDate.HasValue);
            if (department!=null)
            {
                query=query.Where(x=>x.User.Department.Path.Contains(department.Path));
            }
            if (!String.IsNullOrWhiteSpace(UserName))
            {
                query = query.Where(x => x.User.Name.Contains(UserName));
            }
              
            var result = query.ToList();
            switch (CurrentUser.UserRole)
            {
                case UserRole.Manager:
                    if (CurrentUser.ManualRoleRecords != null && CurrentUser.ManualRoleRecords.Any() && CurrentUser.Department != null)
                    {
                        result = result.Where(x => CurrentUser.ManualRoleRecords.Any(y =>y.TargetDepartment!=null? x.User.Department.Path.Contains(y.TargetDepartment.Path):false)
                            || CurrentUser.ManualRoleRecords.Any(y =>y.TargetUser!=null?y.TargetUser.Id == x.User.Id:false)
                            || x.User.Department.Path.Contains(CurrentUser.Department.Path)
                            ).ToList();
                    }
                    else if (CurrentUser.Department != null)
                    {
                        result = result.Where(x => x.User.Department.Path.Contains(CurrentUser.Department.Path)).ToList();
                    }
                    else result = result.Where(x => x.User.Id == CurrentUser.Id).ToList();
                    break;
                case UserRole.DismissedEmployee:
                case UserRole.Employee:
                    result = result.Where(x => x.User.Id == CurrentUser.Id).ToList();
                    break;
            }  
            int counter =1;
            return result.Select(x => new ManualDeductionDto
            {
                        NPP=(counter++),
                        UserName = x.User.Name,
                        Position = x.User.Position!=null?x.User.Position.Name:"",
                        AllSum = x.AllSum,
                        DeductionDate = x.MissionReport.MissionOrder.BeginDate.HasValue ? x.MissionReport.MissionOrder.BeginDate.Value : x.MissionReport.MissionOrder.CreateDate,
                        DeleteDate = x.DeleteDate.HasValue ? x.DeleteDate.Value.ToShortDateString() : "",
                        UserId = x.User.Id,
                        SendTo1C = x.SendTo1C.HasValue ? x.SendTo1C.Value.ToShortDateString() : "",
                        Dep7Name = x.User.Department != null ? x.User.Department.Name : "",
                        Dep3Name = x.User.Department !=null ?
                        (x.User.Department.Dep3 != null && x.User.Department.Dep3.Any() ? x.User.Department.Dep3.First().Name : "")
                        :"",
                        MissionReportNumber=x.MissionReport.Number,
                        MissionReportId = x.MissionReport.Id
            }).ToList();
        }
    }
}
