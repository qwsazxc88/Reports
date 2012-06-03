using System;

namespace Reports.Core.Exceptions
{
    public class ModifyDocumentException:ApplicationException
    {
        public ModifyDocumentException(string message) : base(message)
        {
        }
    }
}