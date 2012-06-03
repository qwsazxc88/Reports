using System.Collections.Generic;
using Iesi.Collections.Generic;
using NHibernate;

namespace Reports.Core.Domain
{
    public class Report : AbstractUsedEntityWithVersion
    {
        #region Fields

        private string _name;
        private string _path;
//        private bool isUsed;

        #endregion

        #region Properties

        public virtual string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            Report institution = obj as Report;
            if (institution == null) return false;
            return Equals(Name, institution.Name) && Equals(Path, institution.Path);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + 29*Path.GetHashCode();
        }
        #endregion

        #region Constructors

        public Report()
        {
        }


        public Report(string name, string path)
        {
            _name = name;
            _path = path;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region MetaData

        private static  int _maxNameLength;
        private static int _maxPathLength;

        public static int MaxNameLength
        {
            get { return _maxNameLength; }
            internal set { _maxNameLength = value; }
        }

        public static int MaxPathLength
        {
            get { return _maxPathLength; }
            internal set { _maxPathLength = value; }
        }

        #endregion
    }
}