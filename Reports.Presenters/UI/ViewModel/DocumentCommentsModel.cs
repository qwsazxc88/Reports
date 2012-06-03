using System;
using System.Collections.Generic;

namespace Reports.Presenters.UI.ViewModel
{
    public class DocumentCommentsModel
    {
        public int DocumentId { get; set; }
        public IList<DocumentCommentModel> Comments { get; set; }
    }

    public class DocumentCommentModel
    {
        public string Creator { get; set; }
        public string CreatedDate { get; set; }
        public string Comment { get; set; }
    }
}