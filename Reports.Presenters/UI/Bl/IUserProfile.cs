using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Dto;
using Reports.Presenters.UI.ViewModel;
namespace Reports.Presenters.UI.Bl
{
    public interface IUserProfile
    {
        void SendEmailToAll();
        PersonnelFileViewModel GetListModel();
        IList<PersonnelFileDto> GetPersonnelFileDocuments(int depId);
        IList<PersonnelFileDto> GetPersonnelFileDocuments(string name);
        void SendDocsTo(int PlaceId, int[] UserIds);
        void CancelSend(int[] UserIds);
        void ReceiveDocs(int[] UserIds);
    }
}
