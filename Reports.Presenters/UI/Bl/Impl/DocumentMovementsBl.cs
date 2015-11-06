using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core;
using Reports.Core.Dto;
using Reports.Core.Domain;
using Reports.Core.Dao;
using Reports.Presenters.UI.ViewModel;
namespace Reports.Presenters.UI.Bl.Impl
{
    public class DocumentMovementsBl: BaseBl, IDocumentMovementsBl
    {
        #region Dao
        private IDocumentMovementsDao documentMovementsDao;
        public IDocumentMovementsDao DocumentMovementsDao
        {
            get { return Validate.Dependency(documentMovementsDao); }
            set { documentMovementsDao = value; }
        }
        private IDocumentMovements_DocTypesDao documentMovements_DocTypesDao;
        public IDocumentMovements_DocTypesDao DocumentMovements_DocTypesDao
        {
            get { return Validate.Dependency(documentMovements_DocTypesDao); }
            set { documentMovements_DocTypesDao = value; }
        }
        private IDocumentMovements_SelectedDocsDao documentMovements_SelectedDocs;
        public IDocumentMovements_SelectedDocsDao DocumentMovements_SelectedDocs
        {
            get { return Validate.Dependency(documentMovements_SelectedDocs); }
            set { documentMovements_SelectedDocs = value; }
        }
        private IDocumentMovementsRoleRecordsDao documentMovementsRoleRecordsDao;
        public IDocumentMovementsRoleRecordsDao DocumentMovementsRoleRecordsDao
        {
            get { return Validate.Dependency(documentMovementsRoleRecordsDao); }
            set { documentMovementsRoleRecordsDao = value; }
        }
        #endregion
        public List<Core.Dto.DocumentMovementsDto> GetDocuments(ViewModel.DocumentMovementsListModel model)
        {
            User user = UserDao.Load(CurrentUser.Id);            
            var query = QueryCreator.Create<DocumentMovements, ViewModel.DocumentMovementsListModel>(model, user, CurrentUser.UserRole);
            var docs = DocumentMovementsDao.Find(query.Compile()).Select(x => new DocumentMovementsDto().FromDomain(x)).ToList();
            return docs;
        }

        public DocumentMovementsEditModel GetEditModel(int Id)
        {
            DocumentMovementsEditModel model = new DocumentMovementsEditModel();
            model.Id = Id;
            SetModel(model);            
            return model;
        }

        public DocumentMovementsListModel GetListModel()
        {
            return new DocumentMovementsListModel();
        }
        private DocumentMovements CreateNewEntity()
        {
            DocumentMovements entity = new DocumentMovements();
            var doctypes = documentMovements_DocTypesDao.LoadAll().ToList();
            entity.Sender = UserDao.Load(CurrentUser.Id);
            entity.CreateDate = DateTime.Now;
            entity.Docs = new List<DocumentMovements_SelectedDocs>();
            doctypes.ForEach(x => entity.Docs.Add(new DocumentMovements_SelectedDocs { DocType=x }));
            return entity;
        }
        private void SetModel(DocumentMovementsEditModel model)
        {
            DocumentMovements entity = null;
            var doctypes = documentMovements_DocTypesDao.LoadAll();
            model.SelectedDocs = new List<DocumentMovementsSelectedDocsDto>();
            foreach (var doctype in doctypes)
            {
                model.SelectedDocs.Add(new DocumentMovementsSelectedDocsDto { Type = doctype.Id, TypeName = doctype.Name });
            }
            if (model.Id > 0)
                entity = DocumentMovementsDao.Load(model.Id);
            else entity = CreateNewEntity();

            model.Id = entity.Id;
            model.Sender.Id = entity.Sender.Id;
            if (entity.Receiver != null)
            {
                model.Receiver.Id = entity.Receiver.Id;
                LoadUserData(model.Receiver);
            }
            if (entity.User != null)
            {
                model.User.Id = entity.User.Id;
                LoadUserData(model.User);
            }
            var docs= entity.Docs.Select(x => new DocumentMovementsSelectedDocsDto().FromDomain(x)).ToList();
            docs.ForEach(x =>
            {
                var doc = model.SelectedDocs.First(y => y.Type == x.Type);                
                doc.RecieverCheck = x.RecieverCheck;
                doc.RecieverCheckDate = x.RecieverCheckDate;
                doc.SenderCheck = x.SenderCheck;
                doc.SenderCheckDate = x.SenderCheckDate;
            });
            model.Descript = entity.Descript;            

        }
        public DocumentMovementsEditModel SaveModel(DocumentMovementsEditModel model)
        {
            DocumentMovements entity = null;
            if (model.Id > 0)
            {
                entity = DocumentMovementsDao.Load(model.Id);
            }
            else
            {
                entity = CreateNewEntity();
            }
            var newdocs = model.SelectedDocs.Where(x => !entity.Docs.Any(y => y.DocType.Id == x.Type));
            foreach (var newdoc in newdocs)
                entity.Docs.Add(new DocumentMovements_SelectedDocs { DocType = DocumentMovements_DocTypesDao.Load(newdoc.Type) });
            if (entity.Sender.Id == CurrentUser.Id)
            {
                if (!entity.SendDate.HasValue)
                {
                    entity.User = UserDao.Load(model.User.Id);
                    entity.Receiver = UserDao.Load(model.Receiver.Id);
                    entity.Descript = model.Descript;
                }
                if (model.SenderAccept && !entity.SendDate.HasValue)
                {
                    entity.SendDate = DateTime.Now;
                }
                foreach (var doc in entity.Docs)
                {
                    var editeddoc = model.SelectedDocs.First(x => x.Type == doc.DocType.Id);
                    if (editeddoc == null) continue;
                    if (doc.SenderCheck != editeddoc.SenderCheck) doc.SenderCheckDate = editeddoc.SenderCheck ? DateTime.Now : new DateTime?();
                    doc.SenderCheck = editeddoc.SenderCheck;                    
                }
            }
            if (entity.Receiver != null && entity.Receiver.Id == CurrentUser.Id)
            {
                if (model.ReceiverAccept && !entity.ReceiverCheckDate.HasValue)
                {
                    entity.ReceiverCheckDate = DateTime.Now;
                }
                foreach (var doc in entity.Docs)
                {
                    var editeddoc = model.SelectedDocs.First(x => x.Type == doc.DocType.Id);
                    if (editeddoc == null) continue;
                    if (doc.RecieverCheck != editeddoc.RecieverCheck) doc.RecieverCheckDate = editeddoc.RecieverCheck ? DateTime.Now : new DateTime?();
                    doc.RecieverCheck = editeddoc.RecieverCheck;
                }
            }
            DocumentMovementsDao.SaveAndFlush(entity);
            DocumentMovementsEditModel newmodel = new DocumentMovementsEditModel();
            newmodel.Id = entity.Id;
            SetModel(newmodel);
            return newmodel;
        }
    }
}
