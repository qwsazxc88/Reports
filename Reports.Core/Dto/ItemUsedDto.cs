using System;
using System.Collections.Generic;
using System.Text;

namespace Reports.Core.Dto
{
    public class ItemUsedDto
    {
        #region Fields
        private int _id;
        private int _counter;
        #endregion
        #region Properties
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public virtual int Counter
        {
            get { return _counter; }
            set { _counter = value; }
        }
        #endregion
        #region Constructors
        public ItemUsedDto()
        {
        }
        public ItemUsedDto(int id,int counter)
        {
            _id = id;
            _counter = counter;
        }
        #endregion
    }
}
