using System;
using System.Collections.Generic;
using System.Text;

namespace Reports.Core.Utils
{
    [global::System.Serializable]
    public class IncorrectDatabaseVersionException: Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public IncorrectDatabaseVersionException() { }
        public IncorrectDatabaseVersionException(string message) : base(message) { }
        public IncorrectDatabaseVersionException(string message, System.Exception inner) : base(message, inner) { }
        protected IncorrectDatabaseVersionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
