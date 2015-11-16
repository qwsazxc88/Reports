using System.Collections.Generic;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.Bl
{
    public interface IAutoComplete
    {
        List<IdNameDto> SearchUsers(string name);
        List<IdNameDto> SearchDepartments(string name);
        /*List<IDNameDictionary> SearchUsers(string name);
        List<IDNameDictionary> SearchVendors(string name);*/
    }
}
