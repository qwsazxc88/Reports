using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Dto;
using Reports.Core.Domain;
using Reports.Core.Services;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
namespace Reports.Core.Dao.Impl
{
    public class SurchargeNoteDao : DefaultDao<SurchargeNote>, ISurchargeNoteDao
    {
        public SurchargeNoteDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<SurchargeNoteDto> GetDocuments(
               int userId,
               UserRole role,
               int departmentId,
               int typeId,
               int status,
               DateTime? beginDate,
               DateTime? endDate,
               string userName,
               int sortedBy,
               bool? sortDescending,
               string docNumber
            )
        {
            var depts=Session.Query<ManualRoleRecord>()
                .Where(x => x.Role.Id == 1 && x.TargetDepartment != null && x.User.Id == userId && x.TargetDepartment.ItemLevel == 3)
                .Select(x => x.TargetDepartment).Distinct()
                .ToList();
            var user = UserDao.Load(userId);
            Department dep=null;
            var crit=Session.CreateCriteria<SurchargeNote>();
            crit.CreateAlias("Creator", "creator", NHibernate.SqlCommand.JoinType.InnerJoin);
            crit.CreateAlias("Creator.Department", "department", NHibernate.SqlCommand.JoinType.LeftOuterJoin);
            crit.Add(Restrictions.Eq("NoteType",typeId));
            
            if (status > 0)
            {
                if (status == 1) { crit.Add(Restrictions.IsNull("DeleteDate")); crit.Add(Restrictions.IsNull("CountantDateAccept")); crit.Add(Restrictions.IsNull("PersonnelDateAccept")); };
                if (status == 2) { crit.Add(Restrictions.IsNull("DeleteDate")); crit.Add(Restrictions.IsNotNull("PersonnelDateAccept")); crit.Add(Restrictions.IsNull("CountantDateAccept")); };
                if (status == 3) { crit.Add(Restrictions.IsNull("DeleteDate")); crit.Add(Restrictions.IsNotNull("CountantDateAccept")); };
                if (status == 4) { crit.Add(Restrictions.IsNotNull("DeleteDate")); };
                if (status == 5) { crit.Add(Restrictions.IsNull("DeleteDate")); crit.Add(Restrictions.IsNotNull("PersonnelManagerDateAccept")); crit.Add(Restrictions.IsNull("CountantDateAccept")); crit.Add(Restrictions.IsNull("PersonnelDateAccept")); };
            }
            if (!String.IsNullOrWhiteSpace(userName))
            { 
                crit.Add(Restrictions.Like("creator.Name",userName.Trim()+"%"));
            }
            if (departmentId > 0)
            {
                dep = Ioc.Resolve<IDepartmentDao>().Load(departmentId);
                if (user.Department != null) 
                    if (!dep.Path.Contains(user.Department.Path) && !depts.Contains(dep)) dep = null;
            }
            if (dep != null)
            {
                    crit.Add(Restrictions.Like("department.Path", dep.Path + "%"));
            }
            else
            {
                if (user != null && role != UserRole.ConsultantPersonnel)
                {
                    if(user.Department!=null)
                    crit.Add(Restrictions.In("creator.Department", depts) || Restrictions.Like("department.Path", user.Department.Path + "%"));
                    
                }
            }
            if (beginDate.HasValue)
            {
                crit.Add(Restrictions.Where<SurchargeNote>(x=>x.CreateDate>=beginDate.Value));
            }
            if (endDate.HasValue)
            {
                crit.Add(Restrictions.Where<SurchargeNote>(x => x.CreateDate <= endDate.Value+TimeSpan.FromDays(1)));
            }
            if (!String.IsNullOrWhiteSpace(docNumber))
            {
                int num;
                var pr=int.TryParse(docNumber, out num);
                if(pr)
                    crit.Add(Restrictions.Eq("Number",num));
            }
            var res = crit.List<SurchargeNote>().Select(x => new SurchargeNoteDto 
            { 
                Id=x.Id,
                CountantDateAccept=x.CountantDateAccept,
                CountantId = x.Countant!=null?x.Countant.Id:0,
                CountantName =x.Countant!=null?x.Countant.Name:"",
                CreateDate=x.CreateDate,
                CreatorId=x.Creator.Id,
                CreatorName=x.Creator.Name,
                Position = x.Creator.Position!=null?x.Creator.Position.Name:"",
                Number=x.Number.ToString(),
                NoteType=x.NoteType,
                PayDay=x.PayDay,
                PayDayEnd = x.PayDayEnd,
                PayType = x.PayType,
                DismissalDate = x.DismissalDate,
                MonthId = x.MonthId,
                PersonnelDateAccept=x.PersonnelDateAccept,
                PersonnelName=x.Personnel!=null?x.Personnel.Name:"",
                PersonnelsId=x.Personnel!=null?x.Personnel.Id:0,
                PersonnelManagerBankDateAccept=x.PersonnelManagerDateAccept,
                PersonnelManagerBankName=x.PersonnelManagerBank!=null?x.PersonnelManagerBank.Name:"",
                DepartmentId=x.DocDep7.Id,
                Dep3Name=x.DocDep3.Name,
                DepartmentName=x.DocDep7.Name,
                Status=x.DeleteDate.HasValue?"Заявка отклонена":(x.CountantDateAccept.HasValue)?"Отработана расчётным отделом":(x.PersonnelDateAccept.HasValue)?"Отработана отделом кадров":(x.PersonnelManagerDateAccept.HasValue)?"Отработана УКДиУ":"Заявка создана"
            });
            /*if (role == UserRole.PersonnelManager && userId != 10)
            {
                var employees = UserDao.GetUsersForPersonnel(userId).ToArray();
                res = res.Where(x => employees.Any(y => y.Id == x.CreatorId));
            }*/
            switch (sortedBy)
            {
                case 1:
                    res=res.OrderBy(x => x.CreateDate);
                    break;
                case 2:
                    res = res.OrderBy(x => x.Number);
                    break;
                case 3:
                    res = res.OrderBy(x => x.CreatorName);
                    break;
                case 4:
                    res = res.OrderBy(x => x.Position);
                    break;
                case 5:
                    res = res.OrderBy(x => x.Dep3Name);
                    break;
                case 6:
                    res = res.OrderBy(x => x.DepartmentName);
                    break;
                case 7:
                    res = res.OrderBy(x => x.PayDay);
                    break;
                case 8:
                    res = res.OrderBy(x => x.PersonnelDateAccept);
                    break;
                case 9:
                    res = res.OrderBy(x => x.CountantDateAccept);
                    break;
                case 10:
                    res = res.OrderBy(x => x.Status);
                    break;
                case 11:
                    res = res.OrderBy(x => x.PersonnelManagerBankDateAccept);
                    break;
                case 12:
                    res = res.OrderBy(x => x.PayDayEnd);
                    break;
                case 13:
                    res = res.OrderBy(x => x.PayType);
                    break;
                case 14:
                    res = res.OrderBy(x => x.PayDay);
                    break;
                case 15:
                    res = res.OrderBy(x => x.DismissalDate);
                    break;
                case 16:
                    res = res.OrderBy(x => x.MonthId);
                    break;
            }
            if (sortDescending.HasValue && sortDescending.Value)
                res=res.Reverse();
            return res.ToList();
        }
    }
}
