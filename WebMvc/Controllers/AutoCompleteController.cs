using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Reports.Core;
using Reports.Presenters.UI.Bl;
using Newtonsoft.Json;

namespace WebMvc.Controllers
{
    [Authorize]
    public class AutoCompleteController : BaseController
    {
        protected IAutoComplete autoComplete;
        public IAutoComplete AutoComplete
        {
            get
            {
                autoComplete = Ioc.Resolve<IAutoComplete>();
                return Validate.Dependency(autoComplete);
            }
        }
        public ContentResult Departments(string searchText)
        {
            var result = AutoComplete.SearchDepartments(searchText).Select(r => new { label = r.Name, value = r.Id.ToString() });

            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(result);
            return Content(jsonString);
        }
        public ContentResult GetUsers(string name)
        {
            if (name.Length < 3) return Content("[]");
            var result = AutoComplete.SearchUsers(name);
            return Content
                (JsonConvert.SerializeObject(result));
        }
        /*public ContentResult Vendors(string searchText)
        {
            var result = Ioc.Resolve<IAutoComplete>().SearchVendors(searchText).Select(r => new { label = r.Name, value = r.ID.ToString() });

            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(result);
            return Content(jsonString);
        }

        public ContentResult Countries(string searchText)
        {
            var filteredCountries = GetEuropeanCountries().Where(x => x.ToLower().Contains(searchText.ToLower())).Select(r => new { label = r, value = 1 });

            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(filteredCountries).ToString();
            return Content(jsonString);
        }

        private IList<string> GetEuropeanCountries()
        {
            return new List<string>
            {
                "AD - Andorra",
                "AL - Albania",
                "AT - Austria",
                "BE - Belgium",
                "BA - Bosnia and Herzegovina",
                "BG - Bulgaria",
                "BO - Bolivia",
                "HR - Croatia",
                "CY - Cyprus",
                "CZ - Czech Republic",
                "DK - Denmark",
                "EE - Estonia",
                "FI - Finland",
                "FR - France",
                "DE - Germany",
                "GR - Greece",
                "HU - Hungary",
                "IS - Iceland",
                "IE - Ireland",
                "IT - Italy",
                "LV - Latvia",
                "LI - Liechtenstein",
                "LT - Lithuania",
                "LU - Luxembourg",
                "MK - Macedonia, the Former Yugoslav Republic of",
                "MT - Malta",
                "MD - Moldova, Republic of",
                "MC - Monaco",
                "NL - Netherlands",
                "NO - Norway",
                "PL - Poland",
                "PT - Portugal",
                "RO - Romania",
                "RU - Russian Federation",
                "SM - San Marino",
                "CS - Serbia and Montenegro",
                "SK - Slovakia",
                "SI - Slovenia",
                "ES - Spain",
                "SE - Sweden",
                "CH - Switzerland",
                "UA - Ukraine",
                "GB - United Kingdom",
                "VA - Holy See (Vatican City State)"
            };
        }*/
    }
}
