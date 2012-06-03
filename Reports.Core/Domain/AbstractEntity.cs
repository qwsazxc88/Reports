using System;
using System.Collections.Generic;
using System.Collections;


namespace Reports.Core.Domain
{
    /// <summary>
    /// Abstract class for entity with int Id
    /// </summary>
    [Serializable]
    public class AbstractEntity : IEntity<int>
    {
        private int _id;

        public const string IdName = "Id";
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        #region Helpers

		//protected static void CopyCollection<T>(ICollection<T> destination, ICollection<T> source, AbstractEntity parent) where T : ICopieable<T>
		//{
		//    Validate.NotNull(destination,"destination");
		//    Validate.NotNull(source, "source");
		//    destination.Clear();
		//    foreach (T t in source)
		//    {
		//        destination.Add(t.Copy(parent));
		//    }
		//}

		//protected static Image GetImageByImageFileId(BaseCase baseCase, int imageFileId)
		//{
		//    foreach (ImageGroup imGroup in baseCase.ImageGroups)
		//    {
		//        foreach (Image image in imGroup.Images)
		//        {
		//            if (image.ImageFile.Id == imageFileId) return image;
		//        }
		//    }
		//    return null;
		//}

        protected static int GetNextEntityId(int id, IEnumerator list)
        {
            Validate.NotNull(list, "list");
            int nextId = 0;
            if (id == 0)
            {
                nextId = GetFirstEntityId(list);
            }
            else
            {
                int findId = 0;
                while (list.MoveNext())
                {
                    AbstractEntity entity = list.Current as AbstractEntity;
                    if (entity != null)
                    {
                        if (entity.Id != id)
                            nextId = entity.Id;
                        if (entity.Id == id)
                            findId = id;
                        else if (findId == id)
                            break;
                    }
                }
            }
            return nextId;
        }

        protected static int GetFirstEntityId(IEnumerator list)
        {
            Validate.NotNull(list, "list");
            int firstId = 0;
            if (list.MoveNext())
            {
                AbstractEntity entity = list.Current as AbstractEntity;
                if (entity != null)
                    firstId = entity.Id;
            }
            return firstId;
        }

        #endregion
    }
}