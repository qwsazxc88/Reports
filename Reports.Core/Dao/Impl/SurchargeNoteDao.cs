using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Dto;
using Reports.Core.Domain;
namespace Reports.Core.Dao.Impl
{
    public class SurchargeNoteDao:DefaultDao<SurchargeNote>
    {
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
            var res = crit.List<SurchargeNote>().Select(x => new SurchargeNoteDto 
            { 
                Id=x.Id,
                AttachmentId = x.Attachment!=null?x.Attachment.Id:0,
                AttachmentName = x.Attachment!=null?x.Attachment.FileName:"",
                CountantDateAccept=x.CountantDateAccept,
                CountantId = x.Countant!=null?x.Countant.Id:0,
                CountantName =x.Countant!=null?x.Countant.Name:"",
                CreateDate=x.CreateDate,
                CreatorId=x.Creator.Id,
                NoteType=x.NoteType,
                PayDay=x.PayDay,
                PersonnelDateAccept=x.PersonnelDateAccept,
                PersonnelName=x.Personnel!=null?x.Personnel.Name:"",
                PersonnelsId=x.Personnel!=null?x.Personnel.Id:0

            });
            return res.ToList();
        }
    }
}
