using System.Collections.Generic;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class DepartmentTreeModel
    {
        public int DepartmentID { get; set; }
        //public Child Root { get; set; }
        public int Level2ID { get; set; }
        public List<IdNameDto> Level2;
        public int Level3ID { get; set; }
        public List<IdNameDto> Level3;
        public int Level4ID { get; set; }
        public List<IdNameDto> Level4;
        public int Level5ID { get; set; }
        public List<IdNameDto> Level5;
        public int Level6ID { get; set; }
        public List<IdNameDto> Level6;
        public int Level7ID { get; set; }
        public List<IdNameDto> Level7;
    }
    /*public class DepartmentChildrenModel
    {
        public string Error { get; set; }
        public List<IdNameDto> Children;
    }*/
    //public class Child
    //{
    //    public int DepartmentID { get; set; }
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public int? Parent { get; set; }
    //    public IList<Child> Children { get; set; } 
    //}
}