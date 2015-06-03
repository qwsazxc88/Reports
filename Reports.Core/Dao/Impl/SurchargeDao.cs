using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Services;
using NHibernate.Linq;
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
            if (!String.IsNullOrWhiteSpace(Number))
            {
                res = res.Where(x => x.Number.ToString() == Number);
            }
            if (!String.IsNullOrWhiteSpace(userName))
            {
                res= res.Where(x=>x.User.Name.ToLower().Contains(userName.ToLower()));
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
            
            var dto= res.ToList().ConvertAll<Dto.SurchargeDto>(x => new Dto.SurchargeDto 
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
            switch (sortedBy)
            {
                case 1: dto=dto.OrderBy(x => x.UserName).ToList();
                    break;
                case 2: dto = dto.OrderBy(x => x.Position).ToList();
                    break;
                case 3: dto = dto.OrderBy(x => x.Dep3Name).ToList();
                    break;
                case 4: dto = dto.OrderBy(x => x.Dep7Name).ToList();
                    break;
                case 5: dto = dto.OrderBy(x => x.EditDate).ToList();
                    break;
                case 6: dto = dto.OrderBy(x => x.Sum).ToList();
                    break;
                case 7: dto = dto.OrderBy(x => x.Number).ToList();
                    break;
                case 8: dto = dto.OrderBy(x => x.Status).ToList();
                    break;
            }
            if (sortDescending.HasValue && sortDescending.Value) dto.Reverse();
            return dto;
        }
        public bool IsSurchargeAvailable(int missionReportId)
        {
            var el=Session.Query<Surcharge>().Where(x => x.MissionReport.Id == missionReportId);
            if (el != null && el.Any()) return true;
            else return false;
        }
        public int AddDocument(int userId, decimal sum, int creatorId, DateTime editDate, int missionReportId, int deductionNumber)
        {
            if (IsSurchargeAvailable(missionReportId)) return -1;
            var User = UserDao.Load(userId);
            var Editor = UserDao.Load(creatorId);
            var MissionReport = Ioc.Resolve<IMissionReportDao>().Load(missionReportId);
            var deductionDao=Ioc.Resolve<IDeductionDao>();
            var deduction = deductionDao.GetDeductionByNumber(deductionNumber);
            
            var document = new Surcharge { 
                Sum = sum,
                Editor = Editor,
                User = User,
                EditDate = editDate,
                MissionReport = MissionReport,
                SurchargeDate = editDate,
                Number = NextNumberDao.GetNextNumberForType((int)Reports.Core.Enum.RequestTypeEnum.Surcharge),
                Deduction=deduction
            };
            
            SaveAndFlush(document);
            return document.Number;
        }
    }
}
