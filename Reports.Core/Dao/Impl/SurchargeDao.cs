﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Services;
namespace Reports.Core.Dao.Impl
{
    public class SurchargeDao:DefaultDao<Surcharge>, ISurchargeDao
    {
        private IRequestNextNumberDao NextNumberDao = Ioc.Resolve<IRequestNextNumberDao>();
        private IDepartmentDao DepartmentDao = Ioc.Resolve<IDepartmentDao>();
        private string GetDep3Name(Department Dep)
        {
            var dep3 = DepartmentDao.GetParentDepartmentWithLevel(Dep, 3);
            return dep3 == null ? "" : dep3.Name;
        }
        public SurchargeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<Dto.SurchargeDto> GetDocuments(int userId, UserRole role, int departmentId, int statusId, DateTime? beginDate, DateTime? endDate, string userName, int sortedBy, bool? sortDescending, string Number)
        {
            IEnumerable<Surcharge> res=LoadAll();
            if (beginDate != null)
            {
                res = res.Where(x => x.EditDate > beginDate);
            }
            if (endDate != null)
            {
                res = res.Where(x => x.EditDate < endDate);
            }
            if (departmentId > 0)
            {
                res = res.Where(x => x.User.Department.Id == departmentId);
            }
            if (statusId > 0)
            {
                switch (statusId)
                {
                        //Выгружен в 1с
                    case 1: res = res.Where(x => x.SendTo1C != null);
                        break;
                        //Не выгружен в 1с
                    case 2: res = res.Where(x => x.SendTo1C == null);
                        break;
                }
            }
            if(!String.IsNullOrWhiteSpace(Number))
                res = res.Where(x => x.Number.ToString() == Number);
            return res.ToList().ConvertAll<Dto.SurchargeDto>(x => new Dto.SurchargeDto 
                    {
                        UserId= x.User.Id,
                        UserName=x.User.Name,
                        Number=x.Number.ToString(),
                        SurchargeDate=x.SurchargeDate,
                        Sum=x.Sum,
                        Position = x.User.Position!=null?x.User.Position.Name:"",
                        Dep7Name = x.User.Department!=null? x.User.Department.Name:"",
                        Dep3Name = GetDep3Name(x.User.Department),
                        Status = x.SendTo1C == null ? "Проводки не сформированы" : "Проводки сформированы"
                    });
        }
        public int AddDocument(int userId, decimal sum, int creatorId, DateTime editDate, int missionReportId)
        {
            var User = UserDao.Load(userId);
            var Editor = UserDao.Load(creatorId);
            var MissionReport = Ioc.Resolve<IMissionReportDao>().Load(missionReportId);

            var document = new Surcharge { 
                Sum = sum,
                Editor = Editor,
                User = User,
                EditDate = editDate,
                MissionReport = MissionReport,
                SurchargeDate = editDate,
                Number = NextNumberDao.GetNextNumberForType((int)Reports.Core.Enum.RequestTypeEnum.Surcharge)
            };
            
            SaveAndFlush(document);
            return document.Number;
        }
    }
}
