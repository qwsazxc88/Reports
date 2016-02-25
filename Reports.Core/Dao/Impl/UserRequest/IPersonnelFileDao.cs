using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Dto;
namespace Reports.Core.Dao
{
    public interface IPersonnelFileDao: IDao<PersonnelFile>
    {
        IList<PersonnelFileDto> GetDocuments(int depId);
        IList<PersonnelFileDto> GetDocuments(string name);
        PersonnelFileDto GetDocumentForUser(int userid);
        void ReceiveSend(int UserId, int CurrentId);
        void CancelSend(int UserId);
    }
}
