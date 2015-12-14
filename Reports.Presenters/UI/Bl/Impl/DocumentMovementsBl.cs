using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core;
using Reports.Core.Dto;
using Reports.Core.Domain;
using Reports.Core.Dao;
using Reports.Presenters.UI.ViewModel;
using Newtonsoft.Json;
using System.Linq.Expressions;
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
        public DocumentMovementsEditModel GetCreateWithoutSendModel()
        {
            var model = new DocumentMovementsEditModel();
            var doctypes = documentMovements_DocTypesDao.LoadAll();
            model.SelectedDocs = new List<DocumentMovementsSelectedDocsDto>();
            model.RuscountUsers = DocumentMovementsRoleRecordsDao.LoadAll().Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
            foreach (var doctype in doctypes)
            {
                model.SelectedDocs.Add(new DocumentMovementsSelectedDocsDto { Type = doctype.Id, TypeName = doctype.Name });
            }
            return model;
        }
        public int SaveCreateWithoutSendModel(DocumentMovementsEditModel model)
        {
            DocumentMovements entity = new DocumentMovements();
            entity.SendDate = DateTime.Now;
            entity.ReceiverCheckDate = DateTime.Now;
            entity.CreateDate = DateTime.Now;
            entity.Direction = 2;
            entity.StatusId = 3;
            entity.Receiver = UserDao.Load(10);
            entity.ReceiverRuscount = DocumentMovementsRoleRecordsDao.Load(model.SenderRuscount);
            entity.Sender = UserDao.Load(model.User.Id);
            entity.Descript = model.Descript;
            entity.User = entity.Sender;
            entity.Docs = new List<DocumentMovements_SelectedDocs>();
            var newdocs = model.SelectedDocs.Where(x => x.RecieverCheck).ToList();
            foreach (var newdoc in newdocs)
                entity.Docs.Add(new DocumentMovements_SelectedDocs { DocType = DocumentMovements_DocTypesDao.Load(newdoc.Type), Movement = entity, RecieverCheck= true, SenderCheck = true, RecieverCheckDate=DateTime.Now, SenderCheckDate = DateTime.Now });
            DocumentMovementsDao.SaveAndFlush(entity);
            return entity.Id;
        }
        public GridDefinition GetDocuments(ViewModel.DocumentMovementsListModel model)
        {            
            User user = UserDao.Load(CurrentUser.Id);            
            var domain = Expression.Parameter(typeof(DocumentMovements), "domain");
            //var query = QueryCreator.Create<DocumentMovements, ViewModel.DocumentMovementsListModel>(model, user, CurrentUser.UserRole);
            var query = QueryCreator.Create<DocumentMovements, ViewModel.DocumentMovementsListModel>(model, user, CurrentUser.UserRole,domain,QueryCreator.GetUserRightsForDocumentMovements(user,CurrentUser.UserRole,domain));
            var docs = DocumentMovementsDao.Find(query.Compile()).ToList();
            string[] statuses=new string[]{"","Черновик","Отправлено", "Получено"};
            string[] directions = new string[] { "","В Банк", "От Банка" };
            List<DocumentMovementsDto> docDtos = new List<DocumentMovementsDto>();
            foreach (var doc in docs)
            {
                var newelement = new DocumentMovementsDto
                {
                    Id = doc.Id,
                    CreateDate = doc.CreateDate,
                    Descript = doc.Descript,
                    Direction = directions[doc.Direction],
                    Receiver = doc.ReceiverRuscount != null ? doc.ReceiverRuscount.Name : doc.Receiver.Name,
                    ReceiverAccept = doc.ReceiverCheckDate.HasValue,
                    ReceiveDate = doc.ReceiverCheckDate,
                    SendDate = doc.SendDate,
                    Sender = (doc.SenderRuscount != null) ? doc.SenderRuscount.Name : doc.Sender.Name,
                    Status = statuses[doc.StatusId],
                    User = doc.User != null ? doc.User.Name : "",
                    UserDep3 = doc.User != null ? (doc.User.Department != null ? (doc.User.Department.Dep3 != null ? doc.User.Department.Dep3.First().Name : "") : "") : "",
                    UserDep7 = doc.User != null ? (doc.User.Department != null ? doc.User.Department.Name : "") : "",
                    subGridOptions = UIGrid_Helper.GetGridDefinition(doc.Docs.Select(x => new DocDto { DocType=x.DocType.Id, DocumentName = x.DocType.Name, DocumentReceived = x.RecieverCheck, DocumentSended = x.SenderCheck }).ToList())
                };
                ((GridDefinition)newelement.subGridOptions).enableVerticalScrollbar = 1;
                ((GridDefinition)newelement.subGridOptions).IsGridEditable = (CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && doc.ReceiverRuscount != null;
                docDtos.Add(newelement);
            }
            var result = UIGrid_Helper.GetGridDefinition(docDtos);
            if (CurrentUser.Id == 5 || CurrentUser.Id == 10) result.IsGridEditable = true;
            return result;
        }
        public void SaveModelsFromList(DocumentMovementsEditModel[] models)
        {
            if (models == null || models.Length==0) return;
            for (int i = 0; i < models.Length; i++)
            {
                var tmp = models[i];
                var movement = DocumentMovementsDao.Load(tmp.Id);
                foreach (var doc in tmp.SelectedDocs)
                {
                    var current=movement.Docs.Where(x => x.DocType.Id == doc.Type);
                    if (current != null && current.Any()) 
                    { 
                        var d = current.First(); 
                        if (d.RecieverCheck != doc.RecieverCheck) 
                        { 
                            d.RecieverCheck = doc.RecieverCheck; 
                            d.RecieverCheckDate = DateTime.Now; 
                        } 
                    }
                }
                if (!movement.ReceiverCheckDate.HasValue && tmp.ReceiverAccept)
                {
                    movement.ReceiverCheckDate = DateTime.Now;
                }
                if (movement.ReceiverCheckDate.HasValue) movement.StatusId = 3;
                DocumentMovementsDao.SaveAndFlush(movement);
            }
        }
        public DocumentMovementsEditModel GetEditModel(int Id)
        {
            DocumentMovementsEditModel model = new DocumentMovementsEditModel();
            model.Id = Id;
            SetModel(model);            
            return model;
        }
        public void SetDocumentReceived(int UserId, int TypeId)
        {
            DocumentMovements doc;
            var docs = DocumentMovementsDao.Find(x => x.User.Id == UserId && x.Direction==2 && x.Docs.Any(y => y.DocType.Id == TypeId && y.SenderCheck==true && y.RecieverCheck==false));
            if (docs != null && docs.Any())
            {
                doc = docs.OrderByDescending(x => x.CreateDate).First();
                var element = doc.Docs.First(x => x.DocType.Id == TypeId);
                DocumentMovements_SelectedDocs.SaveAndFlush(element);
                DocumentMovementsDao.SaveAndFlush(doc);
            }
            else
            {
                doc = CreateNewEntity();
                doc.User = UserDao.Load(UserId);
                doc.Sender = doc.User;
                doc.Receiver = UserDao.Load(CurrentUser.Id);
                doc.SendDate = DateTime.Now;
                doc.ReceiverCheckDate = DateTime.Now;
                doc.Direction = 2;
                doc.Descript = "Авто";
                doc.Docs.Add(new DocumentMovements_SelectedDocs
                {
                    DocType = DocumentMovements_DocTypesDao.Load(TypeId),
                    SenderCheck = true,
                    RecieverCheck = true,
                    SenderCheckDate = DateTime.Now,
                    Movement = doc,
                    RecieverCheckDate = DateTime.Now
                });
                DocumentMovementsDao.SaveAndFlush(doc);
            }
        }
        public DocumentMovementsListModel GetListModel()
        {
            var model =  new DocumentMovementsListModel();
            model.IsSaveAvailable = (CurrentUser.UserRole & (UserRole.PersonnelManager)) > 0;
            model.IsAddAvailable = (CurrentUser.UserRole &(UserRole.Manager | UserRole.PersonnelManager))>0;
            return model;
        }
        private DocumentMovements CreateNewEntity()
        {
            DocumentMovements entity = new DocumentMovements();            
            entity.Sender = UserDao.Load(CurrentUser.Id);
            entity.CreateDate = DateTime.Now;
            entity.Docs = new List<DocumentMovements_SelectedDocs>();            
            return entity;
        }
        private void SetModel(DocumentMovementsEditModel model)
        {            
            DocumentMovements entity = null;
            var doctypes = documentMovements_DocTypesDao.LoadAll();
            model.SelectedDocs = new List<DocumentMovementsSelectedDocsDto>();
            model.RuscountUsers = DocumentMovementsRoleRecordsDao.LoadAll().Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
            if (model.Id > 0)
            {
                entity = DocumentMovementsDao.Load(model.Id);
                if (entity.SendDate.HasValue)
                    foreach (var doc in entity.Docs)
                    {
                        model.SelectedDocs.Add(new DocumentMovementsSelectedDocsDto { Type = doc.DocType.Id, TypeName = doc.DocType.Name, SenderCheck = doc.SenderCheck, SenderCheckDate = doc.SenderCheckDate, RecieverCheck = doc.RecieverCheck, RecieverCheckDate = doc.RecieverCheckDate });
                    }
                else
                    foreach (var doctype in doctypes)
                    {
                        model.SelectedDocs.Add(new DocumentMovementsSelectedDocsDto { Type = doctype.Id, TypeName = doctype.Name });
                    }
            }
            else
            {
                entity = CreateNewEntity();
                foreach (var doctype in doctypes)
                {
                    model.SelectedDocs.Add(new DocumentMovementsSelectedDocsDto { Type = doctype.Id, TypeName = doctype.Name });
                }
            }
            model.Id = entity.Id;
            model.Sender.Id = entity.Sender.Id;
            if (entity.SenderRuscount!=null)
            {
                model.User.Name = entity.SenderRuscount.Name;
            }
            else
            {
                LoadUserData(model.Sender);
            }
            if (CurrentUser.Id == model.Sender.Id) model.IsUserSender = true;
            if (entity.Receiver != null)
            {
                if (entity.ReceiverRuscount != null)
                {
                    model.Receiver.Id = (CurrentUser.UserRole& UserRole.PersonnelManager)>0?CurrentUser.Id:entity.Receiver.Id;
                    model.Receiver.Name = entity.ReceiverRuscount.Name;
                }
                else
                {
                    model.Receiver.Id = entity.Receiver.Id;
                    LoadUserData(model.Receiver);
                }
                if (CurrentUser.Id == model.Receiver.Id) model.IsUserReceiver = true;
            }
            if (entity.User != null)
            {
                model.User.Id = entity.User.Id;
                LoadUserData(model.User);
            }
            if (!entity.SendDate.HasValue)
            {
                var docs = entity.Docs.Select(x => new DocumentMovementsSelectedDocsDto().FromDomain(x)).ToList();
                docs.ForEach(x =>
                {
                    var doc = model.SelectedDocs.First(y => y.Type == x.Type);
                    doc.RecieverCheck = x.RecieverCheck;
                    doc.RecieverCheckDate = x.RecieverCheckDate;
                    doc.SenderCheck = x.SenderCheck;
                    doc.SenderCheckDate = x.SenderCheckDate;
                });
            }
            model.SenderAccept = entity.SendDate.HasValue;
            model.ReceiverAccept = entity.ReceiverCheckDate.HasValue;
            model.SendDate = entity.SendDate;
            model.CreateDate = entity.CreateDate;
            model.Descript = entity.Descript;
            if ((CurrentUser.UserRole & (UserRole.Manager | UserRole.Secretary | UserRole.ConsultantPersonnel)) > 0)
                model.IsUserFromBank = true;
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
            var newdocs = model.SelectedDocs.Where(x =>x.SenderCheck && !entity.Docs.Any(y => y.DocType.Id == x.Type));
            foreach (var newdoc in newdocs)
                entity.Docs.Add(new DocumentMovements_SelectedDocs { DocType = DocumentMovements_DocTypesDao.Load(newdoc.Type), Movement = entity });
            if (entity.Sender.Id == CurrentUser.Id)
            {
                if (!entity.SendDate.HasValue)
                {
                    if (entity.Id == 0)
                    {
                        entity.User = UserDao.Load(model.User.Id);
                        if ((CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                        {
                            entity.Direction = 1;
                            var sender = DocumentMovementsRoleRecordsDao.Load(model.SenderRuscount);
                            entity.SenderRuscount = sender;
                            entity.Receiver = UserDao.Load(model.Receiver.Id);
                        }
                        else
                        {
                            entity.Direction = 2;
                            var receiver = DocumentMovementsRoleRecordsDao.Load(model.Receiver.Id);
                            entity.Receiver = receiver.User;
                            entity.ReceiverRuscount = receiver;                            
                        }
                    }
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
            entity.StatusId = 1;
            if (entity.SendDate.HasValue) entity.StatusId = 2;
            if (entity.ReceiverCheckDate.HasValue) entity.StatusId = 3;
            DocumentMovementsDao.SaveAndFlush(entity);
            DocumentMovementsEditModel newmodel = new DocumentMovementsEditModel();
            newmodel.Id = entity.Id;
            SetModel(newmodel);
            return newmodel;
        }
        public List<IdNameDto> GetRuscountUsers(string query)
        {
            var usrs = DocumentMovementsRoleRecordsDao.Find(x => x.User.Name.Contains(query));
            if (usrs != null && usrs.Any())
                return usrs.Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
            else return new List<IdNameDto>();
        }
    }
}
