using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Dto;
using Reports.Core.Domain;
using Reports.Core.Services;
using NHibernate;
using NHibernate.Criterion;
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
               DateTime? beginDate,
               DateTime? endDate,
               string userName,
               int sortedBy,
               bool? sortDescending,
               string docNumber
            )
        {
            var crit=Session.CreateCriteria<SurchargeNote>();
            crit.CreateAlias("Creator", "creator", NHibernate.SqlCommand.JoinType.InnerJoin);
            crit.CreateAlias("Creator.Department", "department", NHibernate.SqlCommand.JoinType.LeftOuterJoin);
            crit.Add(Restrictions.Eq("NoteType",typeId));
            if (!String.IsNullOrWhiteSpace(userName))
            { 
                crit.Add(Restrictions.Like("creator.Name",userName.Trim()+"%"));
            }
            if (departmentId > 0)
            {
                var dep=Ioc.Resolve<IDepartmentDao>().Load(departmentId);
                if(dep!=null)
                    crit.Add(Restrictions.Like("department.Path", dep.Path+"%"));
            }
            if (beginDate.HasValue)
            {
                crit.Add(Restrictions.Where<SurchargeNote>(x=>x.CreateDate>=beginDate.Value));
            }
            if (endDate.HasValue)
            {
                crit.Add(Restrictions.Where<SurchargeNote>(x => x.CreateDate <= endDate.Value));
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
                PersonnelDateAccept=x.PersonnelDateAccept,
                PersonnelName=x.Personnel!=null?x.Personnel.Name:"",
                PersonnelsId=x.Personnel!=null?x.Personnel.Id:0,
                DepartmentId=x.DocDep7.Id,
                Dep3Name=x.DocDep3.Name,
                DepartmentName=x.DocDep7.Name

            });
            return res.ToList();
        }
    }
}
