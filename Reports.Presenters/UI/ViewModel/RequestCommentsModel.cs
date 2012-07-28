using System.Collections.Generic;

namespace Reports.Presenters.UI.ViewModel
{
    public class RequestCommentsModel
    {
        public int RequestId { get; set; }
        public int RequestTypeId { get; set; }
        public IList<RequestCommentModel> Comments { get; set; }
    }

    public class RequestCommentModel
    {
        public string Creator { get; set; }
        public string CreatedDate { get; set; }
        public string Comment { get; set; }
    }
}
