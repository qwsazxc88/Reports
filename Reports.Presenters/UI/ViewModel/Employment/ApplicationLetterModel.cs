using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class ApplicationLetterModel : AbstractEmploymentModel
    {
        public HttpPostedFileBase ApplicationLetterScanFile { get; set; }

        public string ApplicationLetterScanAttachmentFilename { get; set; }
        public int ApplicationLetterScanAttachmentId { get; set; }

        public ApplicationLetterModel()
        {
            this.Version = 0;
        }
    }
}
