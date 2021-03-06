﻿using System.Collections.Generic;

namespace Reports.Presenters.UI.ViewModel
{
    public class RequestCommentsModel
    {
        public int RequestId { get; set; }
        public int RequestTypeId { get; set; }
        public string AddCommentText { get; set; }
        public bool HasParent { get; set; }
        public IList<RequestCommentModel> Comments { get; set; }        
    }

    public class RequestCommentModel
    {
        public string Creator { get; set; }
        public string CreatedDate { get; set; }
        public string Comment { get; set; }
    }

    public class CommentsModel
    {
        public int RequestId { get; set; }
        public int RequestTypeId { get; set; }
        public IList<RequestCommentModel> Comments { get; set; }
        public bool IsAddAvailable { get; set; }
    }
}
