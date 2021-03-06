﻿using System;
using System.Collections.Generic;
using System.Linq;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;
using Reports.Core.Utils;
using Reports.Presenters.Services;
using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl.Impl
{
    public class EmployeeBl : BaseBl, IEmployeeBl
    {
        protected IEmployeeDocumentTypeDao employeeDocumentTypeDao;
        protected IDocumentDao documentDao;
        protected IAttachmentDao attachmentDao;
        protected IRoleDao roleDao;
        protected ITimesheetStatusDao timesheetStatusDao;
        protected ITimesheetDao timesheetDao;
        protected ITimesheetDayDao timesheetDayDao;
        protected IDocumentCommentDao documentCommentDao;
        protected IWorkingGraphicDao workingGraphicDao;
        protected IWorkingDaysConstantDao workingDaysConstantDao;
        protected IWorkingGraphicTypeDao workingGraphicTypeDao;
        protected IWorkingCalendarDao workingCalendarDao;

        protected ITerraGraphicDao terraGraphicDao;

        protected IConfigurationService configurationService;
        public IConfigurationService ConfigurationService
        {
            set { configurationService = value; }
            get { return Validate.Dependency(configurationService); }
        }

        public int TimesheetPageSize
        {
            get { return ConfigurationService.TimesheetPageSize; }
        }

        public IEmployeeDocumentTypeDao EmployeeDocumentTypeDao
        {
            get { return Validate.Dependency(employeeDocumentTypeDao); }
            set { employeeDocumentTypeDao = value; }
        }
        public IDocumentDao DocumentDao
        {
            get { return Validate.Dependency(documentDao); }
            set { documentDao = value; }
        }
        public IDocumentCommentDao DocumentCommentDao
        {
            get { return Validate.Dependency(documentCommentDao); }
            set { documentCommentDao = value; }
        }
        public IAttachmentDao AttachmentDao
        {
            get { return Validate.Dependency(attachmentDao); }
            set { attachmentDao = value; }
        }
        public IRoleDao RoleDao
        {
            get { return Validate.Dependency(roleDao); }
            set { roleDao = value; }
        }
        public ITimesheetStatusDao TimesheetStatusDao
        {
            get { return Validate.Dependency(timesheetStatusDao); }
            set { timesheetStatusDao = value; }
        }
        public ITimesheetDao TimesheetDao
        {
            get { return Validate.Dependency(timesheetDao); }
            set { timesheetDao = value; }
        }
        public ITimesheetDayDao TimesheetDayDao
        {
            get { return Validate.Dependency(timesheetDayDao); }
            set { timesheetDayDao = value; }
        }
        public IWorkingGraphicDao WorkingGraphicDao
        {
            get { return Validate.Dependency(workingGraphicDao); }
            set { workingGraphicDao = value; }
        }
        public IWorkingGraphicTypeDao WorkingGraphicTypeDao
        {
            get { return Validate.Dependency(workingGraphicTypeDao); }
            set { workingGraphicTypeDao = value; }
        }
        public IWorkingDaysConstantDao WorkingDaysConstantDao
        {
            get { return Validate.Dependency(workingDaysConstantDao); }
            set { workingDaysConstantDao = value; }
        }
        public IWorkingCalendarDao WorkingCalendarDao
        {
            get { return Validate.Dependency(workingCalendarDao); }
            set { workingCalendarDao = value; }
        }

        public ITerraGraphicDao TerraGraphicDao
        {
            get { return Validate.Dependency(terraGraphicDao); }
            set { terraGraphicDao = value; }
        }

        public EmployeeDocumentListModel GetModel(int? ownerId, bool? viewHeader,
            bool? showNew, int? subtypeId)
        {
            //if(!userId.HasValue)
            //    userId = AuthenticationService.CurrentUser.Id;
            EmployeeDocumentListModel model = new EmployeeDocumentListModel
                                                  {
                                                      UserId = ownerId,
                                                      ViewHeader = viewHeader.HasValue && viewHeader.Value,
                                                      ShowNew = showNew.HasValue && showNew.Value,
                                                      DocumentSubTypeId = subtypeId.HasValue ? subtypeId.Value : 0,
                                                  };
            SetModel(model);
            return model;
        }
        protected List<EmployeeDocumentType> GetEmployeeDocumentTypes()
        {
            return EmployeeDocumentTypeDao.LoadAllSorted().ToList();
            //.ConvertAll(x => new IdNameDto(x.Id, x.Name));
        }
        protected string GetDocumentTypeNameForId(int id)
        {
            return EmployeeDocumentTypeDao.FindById(id).Name;
        }
        protected IList<DocumentDto> GetDocumentsListForOwner(int ownerId)
        {
            return DocumentDao.GetDocumentsForOwner(ownerId);
        }
        protected IList<DocumentDto> GetDocumentsListForManager(EmployeeDocumentListModel model)
        {
            int managerId = AuthenticationService.CurrentUser.Id;
            UserRole managerRole = AuthenticationService.CurrentUser.UserRole;
            bool showNew = model.ShowNew;
            return DocumentDao.GetDocumentsListForManager(managerId, managerRole, showNew, model.UserId, model.DocumentSubTypeId);
        }
        protected void SetModel(EmployeeDocumentListModel model)
        {
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.Employee) == UserRole.Employee)
            {
                if (!model.UserId.HasValue)
                    model.UserId = AuthenticationService.CurrentUser.Id;
                model.ViewHeader = true;
                model.AddVisible = true;
                model.DocumentTypesAndSubtypes = new List<IdNameDto>();
                model.DocumentSubTypeId = 0;
                //model.DocumentSubTypes = new List<IdNameDto>();
                SetUserModel(model, model.UserId.Value);
                model.Documents = GetDocumentsListForOwner(model.UserId.Value).ToList().
                    ConvertAll(x => new DocumentDtoModel
                                        {
                                            Date = x.Date.ToShortDateString(),
                                            Id = x.Id,
                                            IsApproved = x.IsApproved,
                                            Type = x.Type + (x.SubType == null
                                                                 ? string.Empty
                                                                 : " " + x.SubType),
                                            OwnerId = x.OwnerId,
                                        });
            }
            else
            {
                if (model.UserId.HasValue)
                    SetUserModel(model, model.UserId.Value);
                model.TypeFilterVisible = true;
                SetDocumentTypesAndSubtypesToModel(model);
                model.Documents = GetDocumentsListForManager(model).ToList().
                    ConvertAll(x => new DocumentDtoModel
                    {
                        Date = x.Date.ToShortDateString(),
                        Id = x.Id,
                        IsApproved = x.IsApproved,
                        Type = x.Type + (x.SubType == null
                                             ? string.Empty
                                             : " " + x.SubType),
                        OwnerId = x.OwnerId,
                    });
            }
        }
        protected void SetDocumentTypesAndSubtypesToModel(EmployeeDocumentListModel model)
        {
            List<IdNameDto> modelList = new List<IdNameDto>();
            List<EmployeeDocumentType> types = GetEmployeeDocumentTypes();
            IList<EmployeeDocumentSubType> subtypes = EmployeeDocumentTypeDao.LoadAllSubtype();
            foreach (var documentType in types)
            {
                IOrderedEnumerable<EmployeeDocumentSubType> typeSubtypes = subtypes.
                    Where(x => x.Type.Id == documentType.Id).
                    OrderBy(x => x.Name);
                modelList.AddRange(typeSubtypes.
                    Select(subtype =>
                        new IdNameDto(subtype.Id, documentType.Name + " " + subtype.Name)));
            }
            modelList.Insert(0,new IdNameDto(0,"Все"));
            model.DocumentTypesAndSubtypes = modelList;
            //if (model.DocumentSubTypeId == 0)
            //    model.DocumentSubTypeId = modelList[0].Id;

            //List<EmployeeDocumentType> types = GetEmployeeDocumentTypes();
            //model.DocumentTypes = types.ConvertAll(x => new IdNameDto(x.Id, x.Name));
            //model.DocumentTypes.Add(new IdNameDto(0, "Все"));
            //model.DocumentTypeId = model.DocumentTypeId;
            //if (model.DocumentTypeId == 0)
            //{
            //    model.DocumentSubTypes = new List<IdNameDto> { new IdNameDto(0, "Все") };
            //    model.DocumentSubTypeId = 0;
            //}
            //else
            //{
            //    model.DocumentSubTypes = GetDocumentSubTypes(types, model.DocumentTypeId);
            //    model.DocumentSubTypeId = model.DocumentTypeId;
            //}
        }
        public EmployeeDocumentEditModel GetDocumentEditModel(int id, int ownerId)
        {
            EmployeeDocumentEditModel model = new EmployeeDocumentEditModel { UserId = ownerId };
            User owner = SetUserModel(model, ownerId);
            SetEditModel(model, id);
            //SetCommentsModel(model, id);
            model.CommentsModel = SetCommentsModel(id);
            SetControlsState(model, owner);
            return model;
        }
        public DocumentCommentsModel GetCommentsModel(int documentId)
        {
            return SetCommentsModel(documentId);
        }


        protected void SetControlsState(EmployeeDocumentEditModel model, User owner)
        {
            switch (CurrentUser.UserRole)
            {
                case UserRole.Employee:
                    if (owner.Id != CurrentUser.Id)
                        throw new ArgumentException("Доступ к документу запрещен.");
                    model.IsEditable = !model.IsApprovedByManager;
                    model.IsSaveAvailable = !model.IsApprovedByManager;
                    break;
                case UserRole.Manager:
                    if (owner.Manager.Id != CurrentUser.Id)
                        throw new ArgumentException("Доступ к документу запрещен.");
                    model.IsApprovedByManagerEnable = !model.IsApprovedByManager;
                    model.IsSaveAvailable = !model.IsApprovedByManager;
                    break;
                case UserRole.BudgetManager:
                    //if (owner.BudgetManager.Id != CurrentUser.Id)
                    //    throw new ArgumentException("Доступ к документу запрещен.");
                    model.IsApprovedByBudgetManagerEnable = !model.IsApprovedByBudgetManager;
                    model.IsSaveAvailable = !model.IsApprovedByBudgetManager;
                    break;
                case UserRole.PersonnelManager:
                    if (owner.Personnels.Where(x => x.Id == CurrentUser.Id).FirstOrDefault() == null )
                        throw new ArgumentException("Доступ к документу запрещен.");
                    model.IsApprovedByPersonnelManagerEnable = !model.IsApprovedByPersonnelManager;
                    model.IsSaveAvailable = !model.IsApprovedByPersonnelManager;
                    break;
                case UserRole.Estimator:
                case UserRole.OutsourcingManager:
                    //if (owner.OutsourcingManager.Id != CurrentUser.Id)
                    //    throw new ArgumentException("Доступ к документу запрещен.");
                    model.IsApprovedByOutsorsingManagerEnable = !model.IsApprovedByOutsorsingManager;
                    model.IsSaveAvailable = !model.IsApprovedByOutsorsingManager;
                    model.IsSendToBillingAvailable = model.IsApprovedByOutsorsingManager && !model.SendEmailToBilling;
                    break;
            }
        }
        public bool SaveEditDocument(EmployeeDocumentEditModel model, UploadFileDto fileDto,
            out string Error)
        {
            User owner = SetUserModel(model, model.UserId.Value);
            bool result = SaveEditDocumentInternal(model, fileDto, owner, out Error);
            model.CommentsModel = SetCommentsModel(model.DocumentId);
            //SetCommentsModel(model, model.DocumentId);
            SetControlsState(model, owner);
            return result;
        }
        public bool SaveEditDocumentInternal(EmployeeDocumentEditModel model, UploadFileDto fileDto,
            User owner, out string Error)
        {
            try
            {
                bool isNewDocument = false;
                Error = null;
                List<EmployeeDocumentType> types = GetEmployeeDocumentTypes();
                model.DocumentTypes = types.ConvertAll(x => new IdNameDto(x.Id, x.Name));
                EmployeeDocumentType type = types.Where(x => x.Id == model.DocumentTypeId).First();
                IList<EmployeeDocumentSubType> subTypes = type.SubTypes.OrderBy(x => x.Name).ToList();
                model.DocumentSubTypes = subTypes.ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
                Document doc = null;
                if (model.DocumentId != 0)
                {
                    doc = DocumentDao.Load(model.DocumentId);
                    if (model.Version != doc.Version)
                    {
                        model.ReloadPage = true;
                        Error = "Документ был изменен.";
                        return false;
                    }
                    //throw new ModifyDocumentException("Документ был изменен.");
                }
                IUser user = AuthenticationService.CurrentUser;
                if (((user.UserRole & UserRole.Employee) != UserRole.Employee) && (model.DocumentId == 0))
                    throw new ArgumentException("Новый документ может быть создан только сотрудником");
                if ((user.UserRole & UserRole.Employee) == UserRole.Employee)
                {
                    
                    if (user.Id != owner.Id)
                        throw new ArgumentException("Доступ к документу запрещен.");
                    if (doc == null)
                    {
                        doc = new Document {Type = type, User = owner};
                        isNewDocument = true;
                    }
                    if (doc.ManagerDateAccept.HasValue)
                    {
                        model.ReloadPage = true;
                        Error = "Документ уже был одобрен руководителем, его изменение невозможно";
                        return false;
                    }
                    //throw new ModifyDocumentException("Документ уже был одобрен руководителем, его изменение невозможно");
                    Attachment attach;
                    doc = SetDocumentFromModel(model, fileDto, doc, type, subTypes, out attach);
                    model.Date = doc.LastModifiedDate;
                    model.DocumentId = doc.Id;
                    if (attach != null)
                    {
                        model.AttachmentId = attach.Id;
                        model.Attachment = attach.FileName;
                    }
                }
                else if ((user.UserRole & UserRole.Manager) == UserRole.Manager)
                {
                    if (user.Id != owner.Manager.Id)
                        throw new ArgumentException("Доступ к документу запрещен.");
                    if (doc.ManagerDateAccept.HasValue)
                    {
                        Error = "Документ уже был одобрен.";
                        return false;
                    }
                    if (model.IsApprovedByManager)
                    {
                        doc.ManagerDateAccept = DateTime.Now;
                        doc = DocumentDao.MergeAndFlush(doc);
                    }
                }
                else if ((user.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager)
                {
                    if (owner.Personnels.Where(x => x.Id == user.Id).FirstOrDefault() == null)
                        throw new ArgumentException("Доступ к документу запрещен.");
                    if (doc.PersonnelManagerDateAccept.HasValue)
                    {
                        Error = "Документ уже был одобрен.";
                        return false;
                    }
                    if (model.IsApprovedByPersonnelManager)
                    {
                        doc.PersonnelManagerDateAccept = DateTime.Now;
                        doc = DocumentDao.MergeAndFlush(doc);
                    }
                }
                else if ((user.UserRole & UserRole.BudgetManager) == UserRole.BudgetManager)
                {
                    //if (user.Id != owner.BudgetManager.Id)
                    //    throw new ArgumentException("Доступ к документу запрещен.");
                    if (doc.BudgetManagerDateAccept.HasValue)
                    {
                        Error = "Документ уже был одобрен.";
                        return false;
                    }
                    if (model.IsApprovedByBudgetManager)
                    {
                        doc.BudgetManagerDateAccept = DateTime.Now;
                        doc = DocumentDao.MergeAndFlush(doc);
                    }
                }
                else if ((user.UserRole & UserRole.OutsourcingManager) == UserRole.OutsourcingManager || (user.UserRole & UserRole.Estimator) == UserRole.Estimator)
                {
                    //if (user.Id != owner.OutsourcingManager.Id)
                    //    throw new ArgumentException("Доступ к документу запрещен.");
                    if (doc.OutsourcingManagerDateAccept.HasValue)
                    {
                        Error = "Документ уже был обработан.";
                        return false;
                    }
                    if (model.IsApprovedByOutsorsingManager)
                    {
                        doc.OutsourcingManagerDateAccept = DateTime.Now;
                        doc = DocumentDao.MergeAndFlush(doc);
                    }
                }
                model.Version = doc.Version;
                if(isNewDocument)
                {
                    Settings settings = SettingsDao.LoadFirst();
                    if(settings == null)
                        Log.Error("Отсутствуют настройки в базе данных.");   
                    else if (settings.SendEmailToManagerAboutNew)
                    {
                        if (doc.User.Manager == null || string.IsNullOrEmpty(doc.User.Manager.Email))
                            Log.WarnFormat("У пользователя {0} (Id {1}) отсутствует руководитель или e-mail руководителя не указан.", doc.User.FullName, doc.User.Id);
                        else
                        {
                            SendEmail(settings, model, doc.User.Manager.Email,
                                        "Новая заявка",
                                        string.Format("Пользователем {0} создана новая заявка.", doc.User.FullName));
                            if (!string.IsNullOrEmpty(model.EmailDto.Error))
                                Log.WarnFormat(
                                    "Письмо о новой заявке пользователя {0}(Id {1}) не было отправлено руководителю {2}.Ошибка: {3}",
                                    doc.User.FullName, doc.User.Id, doc.User.Manager.FullName, model.EmailDto.Error);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Error on DocumentEdit:", ex);
                Error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
        }
        protected Document SetDocumentFromModel(EmployeeDocumentEditModel model, UploadFileDto fileDto,
            Document doc, EmployeeDocumentType type,
            IList<EmployeeDocumentSubType> subTypes, out Attachment attach)
        {
            doc.Comment = model.EditComment;
            doc.LastModifiedDate = DateTime.Now;
            doc.Type = type;
            doc.SubType = subTypes.Where(x => x.Id == model.DocumentSubTypeId).FirstOrDefault();
            doc = DocumentDao.MergeAndFlush(doc);
            attach = null;
            if (model.DocumentId != 0)
                attach = AttachmentDao.FindByDocumentId(doc.Id);
            if (fileDto != null)
            {
                if (attach == null)
                    attach = new Attachment();
                attach.Context = fileDto.Context;
                attach.ContextType = fileDto.ContextType;
                attach.FileName = fileDto.FileName;
                attach.Document = doc;
                AttachmentDao.SaveAndFlush(attach);
            }
            return doc;
        }
        public void SetStaticDataToModel(EmployeeDocumentEditModel model)
        {
            SetUserModel(model, model.UserId.Value);
            model.CommentsModel = SetCommentsModel(model.DocumentId);
            //SetControlsState(model, owner);
            List<EmployeeDocumentType> types = GetEmployeeDocumentTypes();
            model.DocumentTypes = types.ConvertAll(x => new IdNameDto(x.Id, x.Name));
            EmployeeDocumentType type = types.Where(x => x.Id == model.DocumentTypeId).First();
            IList<EmployeeDocumentSubType> subTypes = type.SubTypes.OrderBy(x => x.Name).ToList();
            model.DocumentSubTypes = subTypes.ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            EmployeeDocumentSubType subtype = subTypes.Where(x => x.Id == model.DocumentSubTypeId).FirstOrDefault();
            model.DocumentSubTypeId = subtype == null ? 0 : subtype.Id;


        }
        protected DocumentCommentsModel SetCommentsModel(int id)
        {
            DocumentCommentsModel commentModel = new DocumentCommentsModel { DocumentId = id,Comments = new List<DocumentCommentModel>()};
            if (id > 0)
            {
                Document document = DocumentDao.Load(id);
                if ((document.Comments != null) && (document.Comments.Count() > 0))
                {
                    commentModel.Comments = document.Comments.OrderBy(x => x.DateCreated).ToList().
                        ConvertAll(x => new DocumentCommentModel
                                        {
                                            Comment = x.Comment,
                                            CreatedDate = x.DateCreated.ToString(),
                                            Creator = x.User.FullName,
                                        });
                }
            }
            return commentModel;
        }
        protected void SetEditModel(EmployeeDocumentEditModel model, int id)
        {
            List<EmployeeDocumentType> types = GetEmployeeDocumentTypes();
            model.DocumentTypes = types.ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (id > 0)
            {
                DocumentAndAttachmentDto doc = DocumentDao.GetDocumentAndAttachmentForId(id);
                if (doc == null)
                    throw new ArgumentException(string.Format("Не могу найти документ с Id {0} в базе данных.", id));
                model.Date = doc.Date;
                model.DocumentId = doc.Id;
                model.DocumentTypeId = doc.TypeId;
                model.DocumentSubTypeId = doc.SubTypeId;
                model.DocumentSubTypes = GetDocumentSubTypes(types, doc.TypeId);//subTypes.ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
                model.EditComment = doc.Comment;
                model.IsApprovedByManager = doc.ManagerDateAccept.HasValue;
                model.IsApprovedByPersonnelManager = doc.PersonnelManagerDateAccept.HasValue;
                model.IsApprovedByBudgetManager = doc.BudgetManagerDateAccept.HasValue;
                model.IsApprovedByOutsorsingManager = doc.OutsourcingManagerDateAccept.HasValue;
                model.Attachment = doc.AttachName;
                model.AttachmentId = doc.AttachId;
                model.Version = doc.Version;
                model.SendEmailToBilling = doc.SendEmailToBilling;
            }
            else
            {
                model.DocumentId = 0;
                model.Date = DateTime.Today;
                model.DocumentTypeId = model.DocumentTypes[0].Id;
                model.DocumentSubTypes = GetDocumentSubTypes(types, model.DocumentTypeId);
                    //subTypes.ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
                model.DocumentSubTypeId = model.DocumentSubTypes[0].Id;
            }
        }
        protected IList<IdNameDto> GetDocumentSubTypes(List<EmployeeDocumentType> typesList, int typeId)
        {
            IList<EmployeeDocumentSubType> subTypes = typesList.
                Where(x => x.Id == typeId).First()
                .SubTypes.OrderBy(x => x.Name).ToList();
            return subTypes.ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
        }
        public AttachmentModel GetFileContext(int attachmentId)
        {
            Attachment attachment = AttachmentDao.FindById(attachmentId);
            return new AttachmentModel
            {
                Context = attachment.Context,
                FileName = attachment.FileName,
                ContextType = attachment.ContextType
            };
        }
        public IList<IdNameDto> GetSubTypes(int typeId)
        {
            return
                EmployeeDocumentTypeDao.Load(typeId).SubTypes.ToList().
                    ConvertAll(x => new IdNameDto(x.Id, x.Name));
        }
        public bool SaveComment(SaveCommentModel model)
        {
            try
            {
                int userId = AuthenticationService.CurrentUser.Id;
                Document doc = documentDao.Load(model.DocumentId);
                User user = UserDao.Load(userId);
                DocumentComment comment = new DocumentComment
                                              {
                                                 Comment = model.Comment,
                                                 Document = doc,
                                                 DateCreated = DateTime.Now,
                                                 User = user,
                                              };
                DocumentCommentDao.MergeAndFlush(comment);
                //doc.Comments.Add(comment);
                //DocumentDao.MergeAndFlush(doc);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                model.Error = "Исключение: " + ex.GetBaseException().Message;
                return false;
            }
        }
        public bool SendToBilling(SendToBillingModel model)
        {
            try
            {
                IUser user = AuthenticationService.CurrentUser;
                if ((user.UserRole & UserRole.OutsourcingManager) != UserRole.OutsourcingManager || (user.UserRole & UserRole.Estimator) != UserRole.Estimator)
                {
                    model.Error = "Действие недоступно для данной роли.";
                    return false;
                }
                User current = UserDao.Load(user.Id);
                Document doc = DocumentDao.Load(model.DocumentId);
                if (!doc.OutsourcingManagerDateAccept.HasValue)
                {
                    model.Error = "Заявка не была обработана.";
                    return false;
                }
                string email = string.IsNullOrEmpty(current.Email) ? null : current.Email;
                SendEmailToBilling(model,email,
                                   string.Format("Заявка {0} {1}", doc.Type.Name, doc.SubType.Name), 
                                   string.Format("Пользователь {0} (код 1С {1}) <br>Текст: {2} <br>Дата заявки: {3}"
                                   ,doc.User.FullName,doc.User.Code
                                   ,doc.Comment,doc.LastModifiedDate));
                if (!string.IsNullOrEmpty(model.EmailDto.Error))
                {
                    model.Error = model.EmailDto.Error;
                    return false;
                }
                doc.SendEmailToBilling = true;
                DocumentDao.MergeAndFlush(doc);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                model.Error = "Исключение: " + ex.GetBaseException().Message;
                return false;
            }
        }

        public void GetEmployeeListModel(EmployeeListModel model)
        {
            int managerId = AuthenticationService.CurrentUser.Id;
            UserRole managerRole = AuthenticationService.CurrentUser.UserRole;
            int? roleId = new int?();
            if ((managerRole & UserRole.Admin) == UserRole.Admin)
            {
                roleId = model.RoleId;
                model.Roles = GetRoleList();
                model.IsRolesVisible = true;
                model.IsUserNameVisible = true;
                model.IsLinkToUser = true;
            }
            else
            {
                if ((managerRole & UserRole.OutsourcingManager) == UserRole.OutsourcingManager || (managerRole & UserRole.Estimator) == UserRole.Estimator)
                    model.IsUserNameVisible = true;
                model.Roles = new List<IdNameDto>();
            }
            int numberOfPages;
            int currentPage = model.CurrentPage;
            model.Employees = UserDao.GetUsersForManager(model.UserName, managerId,
                managerRole, roleId, ref currentPage, out numberOfPages).ToList().
                ConvertAll(x => new EmployeeDtoModel
                                    {
                                        Code = x.Code,
                                        Id = x.Id,
                                        Name = x.Name,
                                    });
            model.NumberOfPages = numberOfPages;
            model.CurrentPage = currentPage;
        }
        #region Timesheet List
        public void GetTimesheetListModel(TimesheetListModel model)
        {
            SetListboxes(model);
            SetTimesheetsInfo(model);
        }
        public void SetupDepartment(IDepartmentSelect model)
        {
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.Employee) == UserRole.Employee)
            {
                User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
                IdNameReadonlyDto dep = GetDepartmentDto(user);
                model.DepartmentName = dep.Name;
                model.DepartmentId = dep.Id;
                model.DepartmentReadOnly = dep.IsReadOnly;
            }
        }
        protected IList<DayRequestsDto> GetDayDtoList(int month, int year)
        {
            DateTime current = new DateTime(year, month, 1);
            IList<DayRequestsDto> dtoList = new List<DayRequestsDto>();
            for (int i = 0; i < 31; i++)
            {
                DayRequestsDto dto = new DayRequestsDto { Day = current };
                if (current.Month != month)
                    break;
                dtoList.Add(dto);
                current = current.AddDays(1);
            }
            return dtoList;
        }
        protected void SetTimesheetsInfo(TimesheetListModel model)
        {
            IUser user = AuthenticationService.CurrentUser;
            Log.Debug("Before GetRequestsForMonth");

            IList<DayRequestsDto> dayDtoList = GetDayDtoList(model.Month, model.Year);
            IList<IdNameDtoWithDates> uDtoList =
                UserDao.GetUsersForManagerWithDatePaged(user.Id, user.UserRole,
                        dayDtoList.First().Day, dayDtoList.Last().Day
                        ,model.DepartmentId,model.UserName);
            Log.Debug("After GetUsersForManagerWithDatePaged");
            int userCount = uDtoList.Count;
            model.TotalRecordsCount = userCount;
            int numberOfPages = Convert.ToInt32(Math.Ceiling((double)userCount / TimesheetPageSize));
            int currentPage = model.CurrentPage;
            if (currentPage > numberOfPages)
                currentPage = numberOfPages;
            if (currentPage == 0)
                currentPage = 1;
            uDtoList = uDtoList
                .Skip((currentPage - 1) * TimesheetPageSize)
                .Take(TimesheetPageSize).ToList();
            model.CurrentPage = currentPage;
            model.NumberOfPages = numberOfPages;
            if (userCount == 0)
            {
                model.TimesheetDtos = new List<TimesheetDto>();
                model.IsSaveVisible = false;
                return;
            }
            List<int> usIds = uDtoList.Select(x => x.Id).ToList();
            IList<WorkingCalendar> workDays = WorkingCalendarDao.GetEntitiesBetweenDates(model.Month, model.Year);
            //IList<TerraGraphicDbDto> tgList = TerraGraphicDao.LoadDtoForIdsList(usIds, model.Month, model.Year);
            IList<DayRequestsDto> dtos = TimesheetDao.GetRequestsForMonth
                (model.Month, model.Year, user.Id, user.UserRole,dayDtoList,uDtoList,workDays,
                /*tgList*/new List<TerraGraphicDbDto>(), true);
            Log.Debug("After GetRequestsForMonth");
            List<int> allUserIds = new List<int>();
            allUserIds = dtos.Aggregate(allUserIds,
                                        (current, dayRequestsDto) =>
                                        current.Union(dayRequestsDto.Requests.Select(x => x.UserId).Distinct().ToList())
                                            .ToList());
            Log.Debug("After aggregate");
            List<IdNameDto> userNameDtoList = new List<IdNameDto>();
            foreach (int userId in allUserIds)
            {
                foreach (var dayRequestsDto in dtos)
                {
                    RequestDto dto = dayRequestsDto.Requests.Where(y => y.UserId == userId).FirstOrDefault();
                    if(dto != null)
                    {
                        userNameDtoList.Add(new IdNameDto{Id = userId,Name = dto.UserName});
                        break;
                    }
                }
            }
            userNameDtoList = userNameDtoList.OrderBy(x => x.Name).ToList();
            allUserIds = userNameDtoList.ToList().ConvertAll(x => x.Id).ToList();
            Log.Debug("After create ordered user dto list ");
            List<TimesheetDto> list = new List<TimesheetDto>();
            //List<int> userIds = userNameDtoList.ConvertAll(x => x.Id);
            IList<WorkingGraphic> wgList = WorkingGraphicDao.LoadForIdsList(allUserIds,
                                                                            model.Month, model.Year);
            IList<WorkingGraphicTypeDto>  wgtList = WorkingGraphicTypeDao.GetWorkingGraphicTypeDtoForUsers(allUserIds);
           
            foreach (int userId in allUserIds)
            {
                //dtos.Where(x => x.Requests.Where(y => y.UserId == userId))
                TimesheetDto dto = new TimesheetDto();
                List<RequestDto> userDtoList = new List<RequestDto>();
                List<TimesheetDayDto> userDayList = new List<TimesheetDayDto>();
                IdNameDtoWithDates uDto = uDtoList.Where(x => x.Id == userId).FirstOrDefault();

                float wgHoursSum = 0;
                DateTime beginUserDate = dayDtoList.First().Day;
                DateTime endUserDate = dayDtoList.Last().Day;
                foreach (var dayRequestsDto in dtos)
                {
                    List<RequestDto> userList = dayRequestsDto.Requests.Where(x => x.UserId == userId).ToList();
                    string status = userList.Aggregate(string.Empty,
                                                       (curr, requestDto) => curr + requestDto.TimesheetCode + "/");
                    string hours = userList.Aggregate(string.Empty,
                                                      (curr, requestDto) =>
                                                      curr +
                                                      (requestDto.TimesheetHours.HasValue
                                                           ? requestDto.TimesheetHours.Value.ToString()
                                                           : " ") + "/");
                    float? wgHours; 
                    WorkingGraphic graphicEntity = wgList.Where(x => x.UserId == userId &&
                                                               x.Day == dayRequestsDto.Day).FirstOrDefault();
                    wgHours = graphicEntity == null ?
                        GetDefaultGraphicsForUser(wgtList,workDays, userId, dayRequestsDto.Day) 
                        : graphicEntity.Hours;
                    wgHoursSum += wgHours.HasValue ? wgHours.Value : 0;
                    string graphic = string.Empty;
                    if(wgHours.HasValue)
                    {
                         graphic = (int)wgHours.Value == wgHours.Value 
                                ? ((int)wgHours.Value).ToString() 
                                : wgHours.Value.ToString("0.0");
                    }
                    userDayList.Add(new TimesheetDayDto
                                                 {
                                                     Number = dayRequestsDto.Day.Day,
                                                     isHoliday = CoreUtils.IsDayHoliday(workDays,dayRequestsDto.Day),
                                                     Status = status.Substring(0,status.Length -1),
                                                     Hours = hours.Substring(0, hours.Length - 1),
                                                     Graphic = graphic,
                                                     Id = graphicEntity == null?0:graphicEntity.Id,
                                                 });
                    userDtoList.AddRange(userList);
                    RequestDto employmentDay = userList.Where(x => x.IsEmploymentDay).FirstOrDefault();
                    if (employmentDay != null && employmentDay.BeginDate > beginUserDate)
                        beginUserDate = employmentDay.BeginDate;
                    RequestDto dismissalDay = userList.Where(x => x.IsDismissalDay).FirstOrDefault();
                    if (dismissalDay != null && dismissalDay.EndDate < endUserDate)
                        endUserDate = dismissalDay.EndDate;
                }
                if ((user.UserRole & UserRole.Employee) != UserRole.Employee && uDto != null)
                {
                    int? workdaysSum = workDays.Where(x => x.Date >= beginUserDate && x.Date <= endUserDate).Sum(x => x.IsWorkingHours);
                    int daySum = workDays.Where(x => x.Date >= beginUserDate && x.Date <= endUserDate && x.IsWorkingHours.HasValue).Count();
                    userDayList.Add(new TimesheetDayDto
                    {
                        Number = 0,
                        isStatRecord = true,
                        isHoliday = false,
                        Status = daySum.ToString(),
                        Hours = workdaysSum.Value.ToString(),
                        StatCode = "Б",
                    });
                    //WorkingDaysConstant wdk = WorkingDaysConstantDao.LoadDataForMonth(model.Month, model.Year);
                    //if (wdk == null)
                    //    userDayList.Add(new TimesheetDayDto
                    //    {
                    //        Number = 0,
                    //        isStatRecord = true,
                    //        isHoliday = false,
                    //        Status = string.Empty,
                    //        Hours = string.Empty,
                    //        StatCode = "Б",
                    //    });
                    //else
                    //    userDayList.Add(new TimesheetDayDto
                    //    {
                    //        Number = 0,
                    //        isStatRecord = true,
                    //        isHoliday = false,
                    //        Status = wdk.Days.ToString(),
                    //        Hours = wdk.Hours.ToString(),
                    //        StatCode = "Б",
                    //    });
                    int sumDays = 0;
                    decimal sum = 0;
                    string graphic = null;
                    for (int i = 0; i < 5; i++)
                    {
                      
                        if(i == 0)
                        {
                            graphic = (int)wgHoursSum == wgHoursSum
                               ? ((int)wgHoursSum).ToString()
                               : wgHoursSum.ToString("0.0");
                        }
                        sum += uDto.userStats[i];
                        sumDays += uDto.userStatsDays[i];
                        userDayList.Add(new TimesheetDayDto
                        {
                            Number = 0,
                            isHoliday = false,
                            isStatRecord = true,
                            Status = uDto.userStatsDays[i].ToString(),
                            Hours = uDto.userStats[i].ToString(),
                            StatCode = GetStatCodeName(i),
                            Graphic = i == 0?graphic:null,
                        });
                    }
                    userDayList.Add(new TimesheetDayDto
                    {
                        Number = 0,
                        isHoliday = false,
                        isStatRecord = true,
                        Status = sumDays.ToString(),
                        Hours = sum.ToString(),
                        StatCode = "Всего",
                        Graphic = graphic,
                    });
                }
                dto.MonthAndYear = GetMonthName(model.Month) + " " + model.Year;
                dto.UserNameAndCode = userDtoList.First().UserName;
                dto.UserId = userId;
                dto.Days = userDayList;
                dto.IsHoursVisible = (user.UserRole & UserRole.Employee) != UserRole.Employee;
                dto.IsGraphicVisible = (user.UserRole & UserRole.Employee) != UserRole.Employee;
                dto.IsGraphicEditable = (user.UserRole & UserRole.Manager) == UserRole.Manager;
                list.Add(dto);
            }
            Log.Debug("After foreach");
            model.TimesheetDtos = list;
            model.IsSaveVisible = list.Count > 0 && (user.UserRole & UserRole.Manager) == UserRole.Manager;

        }
        protected string GetStatCodeName(int index)
        {
            switch (index)
            {
                case 0:
                    return "Ф";
                case 1:
                    return "О";
                case 2:
                    return "Б/л";
                case 3:
                    return "К";
                case 4:
                    return "Пр";
                default:
                    return string.Empty;
            }
        }
        protected float? GetDefaultGraphicsForUser(IList<WorkingGraphicTypeDto> wgtList, IList<WorkingCalendar> workDays, int userId, DateTime day)
        {
            /*if (day.DayOfWeek == DayOfWeek.Sunday ||
                day.DayOfWeek == DayOfWeek.Saturday)
                return null;*/
            int? workHours = CoreUtils.GetHoursForDay(workDays, day);
            if (!workHours.HasValue)
                return null;
            if(wgtList.Count == 0)
                return null;
           WorkingGraphicTypeDto dto = wgtList.Where(x => x.UserId == userId)
                                        .FirstOrDefault();
           if (dto == null)
               return null;
            return dto.FillDays.HasValue && dto.FillDays.Value? workHours.Value: new float?();
        }
        public void SaveGraphicsRecord(TimesheetListModel model)
        {
            Log.Debug("Before save WorkingGraphic records.");
            List<int> userIds = model.TimesheetDtos.ToList().ConvertAll(x => x.UserId);
            IList<WorkingGraphic> list = WorkingGraphicDao.LoadForIdsList(userIds,
                                                          model.Month, model.Year);
            foreach (TimesheetDto dto in model.TimesheetDtos)
            {
                foreach (TimesheetDayDto day in dto.Days)
                {
                    if (day.isStatRecord)
                        continue;
                    float? graphicHours = null;
                    if (!string.IsNullOrEmpty(day.Graphic))
                    {
                        double value;
                        if (double.TryParse(day.Graphic, out value))
                            graphicHours = (float) value;
                        else
                            Log.WarnFormat("Cannot parse day.Graphic {0}", day.Graphic);
                    }
                    WorkingGraphic entity;
                    if(day.Id == 0)
                    {
                        entity = new WorkingGraphic
                        {
                            Day = new DateTime(model.Year, model.Month, day.Number),
                            Hours = graphicHours,
                            UserId = dto.UserId,
                        };
                    }
                    else
                    {
                        entity = list.Where(x => x.Id == day.Id).FirstOrDefault();
                        if(entity == null)
                            throw new ArgumentException(string.Format("Не могу найти запись о рабочем графике с Id {0}",day.Id));
                        entity.Hours = graphicHours;
                    }
                    WorkingGraphicDao.Save(entity);
                }
                WorkingGraphicDao.Flush();
            }
            Log.Debug("After save WorkingGraphic records.");
        }
        public void SetListboxes(IYearMonthSelection model)
        {
            model.Monthes = GetMonthesList();
            model.Years = GetYearsList();
        }        
        #endregion
        #region Timesheet Year List
        public void GetTimesheetListModel(TimesheetYearListModel model)
        {
            SetYearTimesheetsInfo(model);
        }
        protected void SetYearTimesheetsInfo(TimesheetYearListModel model)
        {
            IUser user = AuthenticationService.CurrentUser;

            Log.Debug("Before GetRequestsForMonth");
            DateTime beginDate = model.BeginDate.Value;
            DateTime endDate = model.EndDate.Value;
            IList<DayRequestsDto> dayDtoList = GetDayDtoList(beginDate,endDate);
            model.DatesPeriod = string.Format("Период: {0} - {1}", beginDate.ToString("dd MMMM yyyy"), endDate.ToString("dd MMMM yyyy"));
            IList<IdNameDtoWithDates> uDtoList =
                UserDao.GetUsersForManagerWithDatePaged(user.Id, user.UserRole,beginDate, endDate, model.DepartmentId, model.UserName);
            Log.Debug("After GetUsersForManagerWithDatePaged");
            int userCount = uDtoList.Count;
            int numberOfPages = Convert.ToInt32(Math.Ceiling((double)userCount / TimesheetPageSize));
            int currentPage = model.CurrentPage;
            if (currentPage > numberOfPages)
                currentPage = numberOfPages;
            if (currentPage == 0)
                currentPage = 1;
            uDtoList = uDtoList
                .Skip((currentPage - 1) * TimesheetPageSize)
                .Take(TimesheetPageSize).ToList();
            model.CurrentPage = currentPage;
            model.NumberOfPages = numberOfPages;
            if (userCount == 0)
            {
                model.TimesheetDtos = new List<TimesheetDto>();
                //model.IsSaveVisible = false;
                return;
            }
            List<int> usIds = uDtoList.Select(x => x.Id).ToList();
            IList<WorkingCalendar> workDays = WorkingCalendarDao.GetEntitiesBetweenDates(beginDate, endDate);
            //IList<TerraGraphicDbDto> tgList = TerraGraphicDao.LoadDtoForIdsList(usIds,beginDate,endDate);

            IList<DayRequestsDto> dtos = TimesheetDao.GetRequestsForYear
                (beginDate, endDate, user.Id, user.UserRole, dayDtoList, uDtoList, workDays,/*tgList*/new List<TerraGraphicDbDto>());
            Log.Debug("After GetRequestsForMonth");
            List<int> allUserIds = new List<int>();
            allUserIds = dtos.Aggregate(allUserIds,
                                        (current, dayRequestsDto) =>
                                        current.Union(dayRequestsDto.Requests.Select(x => x.UserId).Distinct().ToList())
                                            .ToList());
            Log.Debug("After aggregate");
            List<IdNameDto> userNameDtoList = new List<IdNameDto>();
            foreach (int userId in allUserIds)
            {
                foreach (var dayRequestsDto in dtos)
                {
                    RequestDto dto = dayRequestsDto.Requests.Where(y => y.UserId == userId).FirstOrDefault();
                    if (dto != null)
                    {
                        userNameDtoList.Add(new IdNameDto { Id = userId, Name = dto.UserName });
                        break;
                    }
                }
            }
            userNameDtoList = userNameDtoList.OrderBy(x => x.Name).ToList();
            allUserIds = userNameDtoList.ToList().ConvertAll(x => x.Id).ToList();
            Log.Debug("After create ordered user dto list ");
            List<TimesheetDto> list = new List<TimesheetDto>();
            IList<WorkingGraphic> wgList = WorkingGraphicDao.LoadForIdsList(allUserIds,beginDate, endDate);
            IList<WorkingGraphicTypeDto> wgtList = WorkingGraphicTypeDao.GetWorkingGraphicTypeDtoForUsers(allUserIds);
            foreach (int userId in allUserIds)
            {
                TimesheetDto dto = new TimesheetDto();
                List<RequestDto> userDtoList = new List<RequestDto>();
                List<TimesheetDayDto> userDayList = new List<TimesheetDayDto>();
                IdNameDtoWithDates uDto = uDtoList.Where(x => x.Id == userId).FirstOrDefault();

                float wgHoursSum = 0;
                DateTime beginUserDate = beginDate;
                DateTime endUserDate = endDate;
                foreach (var dayRequestsDto in dtos)
                {
                    List<RequestDto> userList = dayRequestsDto.Requests.Where(x => x.UserId == userId).ToList();
                    float? wgHours;
                    WorkingGraphic graphicEntity = wgList.Where(x => x.UserId == userId &&
                                                               x.Day == dayRequestsDto.Day).FirstOrDefault();
                    wgHours = graphicEntity == null ?
                        GetDefaultGraphicsForUser(wgtList, workDays, userId, dayRequestsDto.Day)
                        : graphicEntity.Hours;
                    wgHoursSum += wgHours.HasValue ? wgHours.Value : 0;
                    userDtoList.AddRange(userList);
                    RequestDto employmentDay = userList.Where(x => x.IsEmploymentDay).FirstOrDefault();
                    if (employmentDay != null && employmentDay.BeginDate > beginUserDate)
                        beginUserDate = employmentDay.BeginDate;
                    RequestDto dismissalDay = userList.Where(x => x.IsDismissalDay).FirstOrDefault();
                    if (dismissalDay != null && dismissalDay.EndDate < endUserDate)
                        endUserDate = dismissalDay.EndDate;
                }
                if ((user.UserRole & UserRole.Employee) != UserRole.Employee && uDto != null)
                {
                    int? workdaysSum = workDays.Where(x => x.Date >= beginUserDate && x.Date <= endUserDate).Sum(x => x.IsWorkingHours);
                    int daySum = workDays.Where(x => x.Date >= beginUserDate && x.Date <= endUserDate && x.IsWorkingHours.HasValue).Count();
                    userDayList.Add(new TimesheetDayDto
                    {
                        Number = 0,
                        isStatRecord = true,
                        isHoliday = false,
                        Status = daySum.ToString(),
                        Hours = workdaysSum.Value.ToString(),
                        StatCode = "Б",
                    });

                    //List<WorkingDaysConstant> wdk = WorkingDaysConstantDao.LoadDataForDates(beginDate,endDate);
                    //if (wdk == null || wdk.Count == 0)
                    //    userDayList.Add(new TimesheetDayDto
                    //    {
                    //        Number = 0,
                    //        isStatRecord = true,
                    //        isHoliday = false,
                    //        Status = string.Empty,
                    //        Hours = string.Empty,
                    //        StatCode = "Б",
                    //    });
                    //else
                    //    userDayList.Add(new TimesheetDayDto
                    //    {
                    //        Number = 0,
                    //        isStatRecord = true,
                    //        isHoliday = false,
                    //        Status = wdk.Sum(x => x.Days).ToString(),
                    //        Hours = wdk.Sum(x => x.Hours).ToString(),
                    //        StatCode = "Б",
                    //    });
                    int sumDays = 0;
                    decimal sum = 0;
                    string graphic = null;
                    for (int i = 0; i < 5; i++)
                    {

                        if (i == 0)
                        {
                            graphic = (int)wgHoursSum == wgHoursSum
                               ? ((int)wgHoursSum).ToString()
                               : wgHoursSum.ToString("0.0");
                        }
                        sum += uDto.userStats[i];
                        sumDays += uDto.userStatsDays[i];
                        userDayList.Add(new TimesheetDayDto
                        {
                            Number = 0,
                            isHoliday = false,
                            isStatRecord = true,
                            Status = uDto.userStatsDays[i].ToString(),
                            Hours = uDto.userStats[i].ToString(),
                            StatCode = GetStatCodeName(i),
                            Graphic = i == 0 ? graphic : null,
                        });
                    }
                    userDayList.Add(new TimesheetDayDto
                    {
                        Number = 0,
                        isHoliday = false,
                        isStatRecord = true,
                        Status = sumDays.ToString(),
                        Hours = sum.ToString(),
                        StatCode = "Всего",
                        Graphic = graphic,
                    });
                }
                dto.UserNameAndCode = userDtoList.First().UserName;
                dto.UserId = userId;
                dto.Days = userDayList;
                dto.IsHoursVisible = (user.UserRole & UserRole.Employee) != UserRole.Employee;
                dto.IsGraphicVisible = (user.UserRole & UserRole.Employee) != UserRole.Employee;
                dto.IsGraphicEditable = false;
                list.Add(dto);
            }
            Log.Debug("After foreach");
            model.TimesheetDtos = list;
        }
        protected IList<DayRequestsDto> GetDayDtoList(DateTime beginDate, DateTime endDate)
        {
            IList<DayRequestsDto> dtoList = new List<DayRequestsDto>();
            DateTime current = beginDate;
            while(current <= endDate) 
            {
                DayRequestsDto dto = new DayRequestsDto { Day = current };
                dtoList.Add(dto);
                current = current.AddDays(1);
            }
            return dtoList;
        }
        #endregion
        public IList<IdNameDto> GetRoleList()
        {
            List<IdNameDto> dtoList = RoleDao.LoadAllSorted().ToList().
                ConvertAll(x => new IdNameDto(x.Id, x.Name));
            dtoList.Insert(0, new IdNameDto(0, "Все"));
            return dtoList;
        }
        public IList<IdNameDto> GetTimesheetStatusesList()
        {
            return TimesheetStatusDao.LoadAllSorted().ToList().
                ConvertAll(x => new IdNameDto(x.Id, x.ShortName));
        }
        #region Timesheet List Edit
        //public void GetTimesheetEditModel(TimesheetEditModel model)
        //{
        //    Timesheet timesheet = TimesheetDao.Load(model.Id);
        //    model.Month = timesheet.Month;
        //    SetTimesheetDto(model, timesheet);
        //    SetControlState(model, timesheet);
        //}
        //protected void SetTimesheetDto(TimesheetEditModel model, Timesheet timesheet)
        //{
        //    User owner = timesheet.User;
        //    TimesheetDto modelDto = new TimesheetDto
        //    {
        //        Id = timesheet.Id,
        //        //Statuses = model.Statuses,
        //        IsEditable = false,
        //        MonthAndYear = timesheet.Month.ToString("MMMM") + " " + timesheet.Month.Year,
        //        OwnerId = owner.Id,
        //        UserNameAndCode =
        //            User.GetFullName(owner.Name) + ", " +
        //            owner.Code,
        //        Days = timesheet.Days.ToList().ConvertAll(x => new TimesheetDayDto
        //        {
        //            Id = x.Id,
        //            Hours = x.Hours,
        //            Number = x.Number,
        //            Status = x.Status.ShortName,
        //            StatusId = x.Status.Id,
        //        })
        //    };
        //    model.TimesheetDto = modelDto;
        //}
        //protected void SetControlState(TimesheetEditModel model, Timesheet timesheet)
        //{
        //    int currentUserId = AuthenticationService.CurrentUser.Id;
        //    model.IsNotApprovedByPersonnel = timesheet.PersonnelNotAcceptDate.HasValue;
        //    model.IsNotApprovedByUser = timesheet.UserNotAcceptDate.HasValue;
        //    switch (AuthenticationService.CurrentUser.UserRole)
        //    {
        //        case UserRole.Employee:
        //            if (currentUserId != timesheet.User.Id)
        //                throw new ArgumentException("Доступ к документу запрещен.");
        //            model.IsNotApprovedByUserEnable = IsLastMonthWokingDay(model.Month);
        //            model.IsSaveAvailable = model.IsNotApprovedByUserEnable;
        //            break;
        //        case UserRole.Manager:
        //            if ((timesheet.User.Manager == null) || 
        //                (currentUserId != timesheet.User.Manager.Id))
        //                throw new ArgumentException("Доступ к документу запрещен.");
        //            model.TimesheetDto.IsEditable = true;
        //            model.IsSaveAvailable = true;
        //            model.ViewHeader = true;
        //            //model.TimesheetDto.Statuses = GetTimesheetStatusesList();
        //            SetUserModel(model, timesheet.User);
        //            break;
        //        case UserRole.PersonnelManager:
        //            if ((timesheet.User.PersonnelManager == null) ||
        //                (currentUserId != timesheet.User.PersonnelManager.Id))
        //                throw new ArgumentException("Доступ к документу запрещен.");
        //            model.IsNotApprovedByPersonnelEnable = true;
        //            model.IsSaveAvailable = true;
        //            model.ViewHeader = true;
        //            SetUserModel(model, timesheet.User);
        //            //model.TimesheetDto.Statuses = GetTimesheetStatusesList();
        //            break;
        //        default:
        //            throw new ArgumentException("Доступ к документу запрещен.");
        //    }
        //}
        protected bool IsLastMonthWokingDay(DateTime month)
        {
            //return true;
            DateTime firstMonthDate = new DateTime(month.Year, month.Month, 1);
            DateTime lastMonthDate = firstMonthDate.AddMonths(1).AddDays(-1);
            for (DateTime day = lastMonthDate; day >= firstMonthDate; day = day.AddDays(-1))
            {
                if (day.DayOfWeek != DayOfWeek.Sunday && day.DayOfWeek != DayOfWeek.Saturday)
                    return day == DateTime.Today;
            }
            return false;
        }
//        public void SetTimesheet(TimesheetEditModel model)
//        {
//            Timesheet timesheet = TimesheetDao.Load(model.TimesheetDto.Id);
//            switch (AuthenticationService.CurrentUser.UserRole)
//            {
//                case UserRole.Manager:
//                    if ((timesheet.User.Manager == null) || 
//                        (authenticationService.CurrentUser.Id != timesheet.User.Manager.Id))
//                        throw new ArgumentException("Доступ к документу запрещен.");
//                    IList<TimesheetStatus> statuses = TimesheetStatusDao.LoadAllSorted();
//                    IList<TimesheetDayDto> dtos = model.TimesheetDto.Days;
//                    foreach (TimesheetDay day in timesheet.Days)
//                    {
//                        TimesheetDayDto dto = dtos.Where(x => x.Id == day.Id).First();
//                        if (day.Status.Id != dto.StatusId)
//                        {
//                            TimesheetStatus status = statuses.Where(x => x.Id == dto.StatusId).First();
//                            day.Status = status;
//                        }
//                        day.Hours = dto.Hours;
//                    }
//                    TimesheetDao.SaveAndFlush(timesheet);
//                    break;
//                case UserRole.Employee:
//                    if (authenticationService.CurrentUser.Id != timesheet.User.Id)
//                        throw new ArgumentException("Доступ к документу запрещен.");
//                    bool isTimesheetIsAcceptedBefore = !timesheet.UserNotAcceptDate.HasValue;
//                    if (timesheet.UserNotAcceptDate.HasValue && !model.IsNotApprovedByUser)
//                        timesheet.UserNotAcceptDate = null;
//                    else if(!timesheet.UserNotAcceptDate.HasValue && model.IsNotApprovedByUser)
//                        timesheet.UserNotAcceptDate = DateTime.Now;
//                    TimesheetDao.SaveAndFlush(timesheet);
//                    if(isTimesheetIsAcceptedBefore && timesheet.UserNotAcceptDate.HasValue)
//                    {
//                        if (timesheet.User.Manager == null || string.IsNullOrEmpty(timesheet.User.Manager.Email))
//                            Log.WarnFormat("У пользователя {0} (Id {1}) отсутствует руководитель или e-mail руководителя не указан.", 
//                                timesheet.User.FullName, timesheet.User.Id);
//                        else
//                        {

//                            SendEmail(model, timesheet.User.Manager.Email,
//                                      string.Format("Несогласие с табелем"),
//                                      string.Format("Пользователь {0} не согласен с табелем за {1}. Дата {2}."
//                                      ,timesheet.User.FullName,GetMonth(timesheet.Month),
//                                      DateTime.Today.ToShortDateString()));
//                            if (!string.IsNullOrEmpty(model.EmailDto.Error))
//                                Log.WarnFormat(@"Письмо о несогласии с табелем за {4} 
//                                                пользователя {0}(Id {1}) не было отправлено 
//                                                руководителю {2}.Ошибка: {3}",
//                                    timesheet.User.FullName, timesheet.User.Id, 
//                                    timesheet.User.Manager.FullName, 
//                                    model.EmailDto.Error,
//                                    GetMonth(timesheet.Month));
//                        }
//                    }
//                    break;
//                case UserRole.PersonnelManager:
//                    if ((timesheet.User.PersonnelManager == null) || 
//                        (authenticationService.CurrentUser.Id != timesheet.User.PersonnelManager.Id))
//                        throw new ArgumentException("Доступ к документу запрещен.");
//                    if (timesheet.PersonnelNotAcceptDate.HasValue && !model.IsNotApprovedByPersonnel)
//                        timesheet.PersonnelNotAcceptDate = null;
//                    else if (!timesheet.PersonnelNotAcceptDate.HasValue && model.IsNotApprovedByPersonnel)
//                        timesheet.PersonnelNotAcceptDate = DateTime.Now;
//                    TimesheetDao.SaveAndFlush(timesheet);
//                    break;
//                default:
//                    throw new ArgumentException("Доступ к документу запрещен.");
//            }
//        }
        #endregion
        #region Timesheet List Edit Dialog
        public void GetEditDayModel(EditDayModel model)
        {
            TimesheetDay day = TimesheetDayDao.Load(model.Id);
            //model.StatusId = day.Status.Id;
            //model.Hours = day.Hours;
            model.Statuses = GetTimesheetStatusesList();
            DateTime month = day.Timesheet.Month;
            DateTime editDay = new DateTime(month.Year, month.Month, day.Number);
            model.Day = editDay.ToString("dd MMMM yyyy") + ", " + editDay.ToString("dddd");
        }
        #endregion
        public EmployeeTimesheetListModel GetEmployeeTimesheetListModel()
        {
            if((CurrentUser.UserRole & UserRole.Employee) != UserRole.Employee)
                throw new ArgumentException("Доступ к документу запрещен.");
            EmployeeTimesheetListModel model = new EmployeeTimesheetListModel
                                                   {
                                                       OwnerId = CurrentUser.Id
                                                   };
            model.Timesheets = TimesheetDao.GetTimesheetListForOwner(CurrentUser.Id).
                ToList().ConvertAll(x => new DocumentDtoModel
                                             {
                                                 Id = x.Id,
                                                 IsApproved = !x.UserNotAcceptDate.HasValue,
                                                 Date = x.Month.ToString("MM.yyyy"),
                                             });
            return model;
        }

        #region Graphics List
        public void GetGraphicsListModel(GraphicsListModel model)
        {
            SetListboxes(model);
            SetGraphicsInfo(model);
            UserRole role = CurrentUser.UserRole;
            model.IsSetShortNamesAvailable = ((role & UserRole.Manager) > 0) ||
                                             (role & UserRole.OutsourcingManager) > 0 ||
                                             (role & UserRole.Estimator) >0  ;
            model.IsShortNamesEditable = ((role & UserRole.Manager) > 0);
        }
        public void SetGraphicsInfo(GraphicsListModel model)
        {
            IUser user = AuthenticationService.CurrentUser;
            Log.Debug("SetGraphicsInfo:Before GetRequestsForMonth");

            IList<DayRequestsDto> dayDtoList = GetDayDtoList(model.Month, model.Year);
            IList<IdNameDtoWithDates> uDtoList =
                UserDao.GetUsersForManagerWithDatePagedForGraphics_NEW(user.Id, user.UserRole,
                        dayDtoList.First().Day, dayDtoList.Last().Day
                        , model.DepartmentId, model.UserName);
            Log.Debug("SetGraphicsInfo:After GetUsersForManagerWithDatePaged");
            int userCount = uDtoList.Count;
            //model.TotalRecordsCount = userCount;
            int numberOfPages = Convert.ToInt32(Math.Ceiling((double)userCount / TimesheetPageSize));
            int currentPage = model.CurrentPage;
            if (currentPage > numberOfPages)
                currentPage = numberOfPages;
            if (currentPage == 0)
                currentPage = 1;
            uDtoList = uDtoList
                .Skip((currentPage - 1) * TimesheetPageSize)
                .Take(TimesheetPageSize).ToList();
            model.CurrentPage = currentPage;
            model.NumberOfPages = numberOfPages;
            if (userCount == 0)
            {
                model.Dtos = new List<TerraGraphicDto>();
                return;
            }
            IList<WorkingCalendar> workDays = WorkingCalendarDao.GetEntitiesBetweenDates(model.Month, model.Year);

            IList<DayRequestsDto> dtos = TimesheetDao.GetRequestsForMonth
                (model.Month, model.Year, user.Id, user.UserRole, dayDtoList, uDtoList, workDays
                ,new List<TerraGraphicDbDto>(),false);
            Log.Debug("SetGraphicsInfo:After GetRequestsForMonth");
            List<int> allUserIds = new List<int>();
            allUserIds = dtos.Aggregate(allUserIds,
                                        (current, dayRequestsDto) =>
                                        current.Union(dayRequestsDto.Requests.Select(x => x.UserId).Distinct().ToList())
                                            .ToList());
            Log.Debug("SetGraphicsInfo:After aggregate");
            /*List<IdNameDto> userNameDtoList = new List<IdNameDto>();
            foreach (int userId in allUserIds)
            {
                foreach (var dayRequestsDto in dtos)
                {
                    RequestDto dto = dayRequestsDto.Requests.Where(y => y.UserId == userId).FirstOrDefault();
                    if (dto != null)
                    {
                        userNameDtoList.Add(new IdNameDto { Id = userId, Name = dto.UserName });
                        break;
                    }
                }
            }*/
            //userNameDtoList = userNameDtoList.OrderBy(x => x.Name).ToList();
            IList<User> users = UserDao.LoadForIdsList(allUserIds);
            allUserIds = users.ToList().ConvertAll(x => x.Id).ToList();
            Log.Debug("SetGraphicsInfo:After create ordered user dto list ");
            List<TerraGraphicDto> list = new List<TerraGraphicDto>();
            IList<TerraGraphicDbDto> tgList = TerraGraphicDao.LoadDtoForIdsList(allUserIds,model.Month, model.Year);
            foreach (int userId in allUserIds)
            {
                TerraGraphicDto dto = new TerraGraphicDto();
                List<TerraGraphicDayDto> userDayList = new List<TerraGraphicDayDto>();
                DateTime beginUserDate = dayDtoList.First().Day;
                DateTime endUserDate = dayDtoList.Last().Day;
                foreach (var dayRequestsDto in dtos)
                {
                    List<RequestDto> userList = dayRequestsDto.Requests.Where(x => x.UserId == userId).ToList();
                    decimal? hours = new decimal?();  
                    TerraGraphicDbDto graphicEntity = tgList.Where(x => x.UserId == userId && x.Day == dayRequestsDto.Day).FirstOrDefault();
                    if(graphicEntity != null)
                        hours = graphicEntity.Hours;

                    /*string status = userList.Aggregate(string.Empty,
                                                    (curr, requestDto) => curr + requestDto.TimesheetCode + "/");*/
                    /*if (graphicEntity == null)
                    {
                        RequestDto rDto = userList.Where(x => x.TimesheetHours.HasValue).FirstOrDefault();
                        if (rDto != null)
                            hours = rDto.TimesheetHours.Value;
                    }
                    else
                        hours = graphicEntity.Hours;*/
                    RequestDto employmentDay = userList.Where(x => x.IsEmploymentDay).FirstOrDefault();
                    if (employmentDay != null && employmentDay.BeginDate > beginUserDate)
                        beginUserDate = employmentDay.BeginDate;
                    RequestDto dismissalDay = userList.Where(x => x.IsDismissalDay).FirstOrDefault();
                    if (dismissalDay != null && dismissalDay.EndDate < endUserDate)
                        endUserDate = dismissalDay.EndDate;

                    bool? isCredit = new bool?();
                    if (graphicEntity != null)
                        isCredit = graphicEntity.IsCreditAvailable;

                    bool? isFactCredit = new bool?();
                    if (graphicEntity != null)
                        isFactCredit = graphicEntity.IsFactCreditAvailable;

                    userDayList.Add(new TerraGraphicDayDto
                                        {
                                            Number = dayRequestsDto.Day.Day,
                                            Day = dayRequestsDto.Day.ToString("dd.MM.yyyy"),
                                            isHoliday = CoreUtils.IsDayHoliday(workDays, dayRequestsDto.Day),
                                            Hours = hours,
                                            Id = graphicEntity == null ? 0 : graphicEntity.Id,
                                            IsCredits = isCredit.HasValue?isCredit.Value?"Да":"Нет":string.Empty,
                                            IsFactCredits = isFactCredit.HasValue ? isFactCredit.Value ? "Да" : "Нет" : string.Empty,
                                            TerraPointId = graphicEntity == null || !graphicEntity.PointId.HasValue ? 0 : graphicEntity.PointId,
                                            TerraPointName = graphicEntity == null || !graphicEntity.PointId.HasValue ? string.Empty
                                                                 : (string.IsNullOrEmpty(graphicEntity.PointName)
                                                                        ? "!"
                                                                        : graphicEntity.PointName),
                                            TerraPointTitle = graphicEntity == null ? string.Empty:graphicEntity.PointTitle,
                                            IsEditable = (user.UserRole & UserRole.Manager) == UserRole.Manager,

                                            FactHours = dayRequestsDto.Day.Date > DateTime.Today || graphicEntity == null ? null : graphicEntity.FactHours,
                                            FactPointTitle = dayRequestsDto.Day.Date > DateTime.Today || graphicEntity == null || !graphicEntity.FactPointId.HasValue ? string.Empty : graphicEntity.FactPointTitle,
                                            FactPointName = dayRequestsDto.Day.Date > DateTime.Today || graphicEntity == null || !graphicEntity.FactPointId.HasValue ? string.Empty
                                                                : (string.IsNullOrEmpty(graphicEntity.FactPointName)
                                                                       ? "!"
                                                                       : graphicEntity.FactPointName),
                                            //TabelStatus = status.Substring(0, status.Length - 1),
                    });
                }
                userDayList.Insert(0,new TerraGraphicDayDto
                {
                    Number = 0,
                    isStatRecord = true,
                    isHoliday = false,
                    StatCode = string.Empty,
                    IsCredits = "Кредиты",
                    IsFactCredits = "Кредиты факт",
                    TerraPointName = "Точка план",
                    FactPointName = "Точка факт",
                    //TabelStatus = "Табель",
                });
                decimal? planHours = userDayList.Sum(x => x.Hours);
                decimal? factHours = userDayList.Sum(x => x.FactHours);
                int? workdaysSum = workDays.Where(x => x.Date >= beginUserDate && x.Date <= endUserDate).Sum(x => x.IsWorkingHours);
                userDayList.Add(new TerraGraphicDayDto
                {
                    Number = 0,
                    isStatRecord = true,
                    isHoliday = false,
                    Hours = workdaysSum,
                    StatCode = "Б",
                });
                userDayList.Add(new TerraGraphicDayDto
                {
                    Number = 0,
                    isStatRecord = true,
                    isHoliday = false,
                    Hours = planHours,
                    StatCode = "План",
                    FactHours = factHours,
                });
                User usr = users.Where(x => x.Id == userId).FirstOrDefault();
                if (usr == null)
                    throw new ArgumentException(string.Format("Не найден пользователь с id {0}", userId));
                dto.MonthAndYear = GetMonthName(model.Month) + " " + model.Year;
                dto.UserNameAndCode = usr.Name;
                dto.UserId = userId;
                dto.Days = userDayList;
                dto.Postion = string.IsNullOrEmpty(usr.Position.Name) ? string.Empty : usr.Position.Name.Trim();
                dto.Rate = usr.Rate.HasValue ? usr.Rate.Value.ToString() : string.Empty;
                list.Add(dto);
            }
            Log.Debug("SetGraphicsInfo:After foreach");
            model.Dtos = list;
        }
        #endregion
    }
}