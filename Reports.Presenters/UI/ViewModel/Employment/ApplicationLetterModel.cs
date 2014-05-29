using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class ApplicationLetterModel
    {
        public int Version { get; set; }
        public int UserId { get; set; }

        public ApplicationLetterModel()
        {
            this.Version = 0;
        }
    }
}
