using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Domain;
using Reports.Presenters.Services;
using Reports.Presenters.UI.ViewModel.Employment2;


namespace Reports.Presenters.UI.Bl.Impl
{
    public class EmploymentBl : BaseBl, IEmploymentBl
    {
        #region Dependencies

        protected IEmploymentCandidateDao employmentCandidateDao;
        public IEmploymentCandidateDao EmploymentCandidateDao
        {
            get { return Validate.Dependency(employmentCandidateDao); }
            set { employmentCandidateDao = value; }
        }

        protected IEmploymentCommonDao employmentCommonDao;
        public IEmploymentCommonDao EmploymentCommonDao
        {
            get { return Validate.Dependency(employmentCommonDao); }
            set { employmentCommonDao = value; }
        }

        protected IEmploymentGeneralInfoDao employmentGeneralInfoDao;
        public IEmploymentGeneralInfoDao EmploymentGeneralInfoDao
        {
            get { return Validate.Dependency(employmentGeneralInfoDao); }
            set { employmentGeneralInfoDao = value; }
        }

        protected IEmploymentPassportDao employmentPassportDao;
        public IEmploymentPassportDao EmploymentPassportDao
        {
            get { return Validate.Dependency(employmentPassportDao); }
            set { employmentPassportDao = value; }
        }

        protected IEmploymentEducationDao employmentEducationDao;
        public IEmploymentEducationDao EmploymentEducationDao
        {
            get { return Validate.Dependency(employmentEducationDao); }
            set { employmentEducationDao = value; }
        }

        protected IEmploymentFamilyDao employmentFamilyDao;
        public IEmploymentFamilyDao EmploymentFamilyDao
        {
            get { return Validate.Dependency(employmentFamilyDao); }
            set { employmentFamilyDao = value; }
        }

        protected IEmploymentMilitaryServiceDao employmentMilitaryServiceDao;
        public IEmploymentMilitaryServiceDao EmploymentMilitaryServiceDao
        {
            get { return Validate.Dependency(employmentMilitaryServiceDao); }
            set { employmentMilitaryServiceDao = value; }
        }

        protected IEmploymentExperienceDao employmentExperienceDao;
        public IEmploymentExperienceDao EmploymentExperienceDao
        {
            get { return Validate.Dependency(employmentExperienceDao); }
            set { employmentExperienceDao = value; }
        }

        protected IEmploymentContactsDao employmentContactsDao;
        public IEmploymentContactsDao EmploymentContactsDao
        {
            get { return Validate.Dependency(employmentContactsDao); }
            set { employmentContactsDao = value; }
        }

        protected IEmploymentBackgroundCheckDao employmentBackgroundCheckDao;
        public IEmploymentBackgroundCheckDao EmploymentBackgroundCheckDao
        {
            get { return Validate.Dependency(employmentBackgroundCheckDao); }
            set { employmentBackgroundCheckDao = value; }
        }

        protected IEmploymentOnsiteTrainingDao employmentOnsiteTrainingDao;
        public IEmploymentOnsiteTrainingDao EmploymentOnsiteTrainingDao
        {
            get { return Validate.Dependency(employmentOnsiteTrainingDao); }
            set { employmentOnsiteTrainingDao = value; }
        }

        protected IEmploymentManagersDao employmentManagersDao;
        public IEmploymentManagersDao EmploymentManagersDao
        {
            get { return Validate.Dependency(employmentManagersDao); }
            set { employmentManagersDao = value; }
        }

        protected IEmploymentPersonnelManagersDao employmentPersonnelManagersDao;
        public IEmploymentPersonnelManagersDao EmploymentPersonnelManagersDao
        {
            get { return Validate.Dependency(employmentPersonnelManagersDao); }
            set { employmentPersonnelManagersDao = value; }
        }

        protected ICountryDao countryDao;
        public ICountryDao CountryDao
        {
            get { return Validate.Dependency(countryDao); }
            set { countryDao = value; }
        }

        #endregion

        #region Get Model

        public GeneralInfoModel GetGeneralInfoModel(int? userId)
        {
            // TODO: EMPL доработать реализацию
            //UserRole role = AuthenticationService.CurrentUser.UserRole;
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            GeneralInfoModel model = new GeneralInfoModel();
            GeneralInfo entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<GeneralInfo>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentGeneralInfoDao.Get(id.Value);
            }
            if (entity != null)
            {
                model.AgreedToPersonalDataProcessing = entity.AgreedToPersonalDataProcessing;
                //model.Citizenship = entity.Citizenship.Id;
                model.CityOfBirth = entity.CityOfBirth;
                model.DateOfBirth = entity.DateOfBirth;
                // Disabilities
                model.DistrictOfBirth = entity.DistrictOfBirth;
                model.FirstName = entity.FirstName;
                // Foreign languages
                model.INN = entity.INN;
                //model.InsuredPersonType = entity.InsuredPersonType.Id;
                model.IsMale = entity.IsMale;
                model.IsPatronymicAbsent = entity.Patronymic.Length > 0;
                model.LastName = entity.LastName;
                // Name changes
                model.Patronymic = entity.Patronymic;
                model.RegionOfBirth = entity.RegionOfBirth;
                model.SNILS = entity.SNILS;
                //model.Status = entity.Status;
                model.UserId = entity.Candidate.User.Id;
                model.Version = entity.Version;
            }
            else
            {
                model = new GeneralInfoModel { UserId = userId.Value };
            }
            LoadDictionaries(model);
            // TODO: EMPL загрузка данных из БД и наполнение модели
            return model;
        }

        public PassportModel GetPassportModel(int? userId)
        {
            // TODO: EMPL доработать реализацию
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            PassportModel model = new PassportModel();
            Passport entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<Passport>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentPassportDao.Get(id.Value);
            }
            if (entity != null)
            {
                model.Apartment = entity.Apartment;
                model.Building = entity.Building;
                model.City = entity.City;
                model.District = entity.District;
                model.InternalPassportDateOfIssue = entity.InternalPassportDateOfIssue;
                model.InternalPassportIssuedBy = entity.InternalPassportIssuedBy;
                //////////
            }
            else
            {
                model = new PassportModel { UserId = userId.Value };
            }
            LoadDictionaries(model);
            return model;
        }

        public EducationModel GetEducationModel(int? userId)
        {
            // TODO: EMPL заменить реализацией
            return new EducationModel();
        }

        public FamilyModel GetFamilyModel(int? userId)
        {
            // TODO: EMPL заменить реализацией
            return new FamilyModel();
        }

        public MilitaryServiceModel GetMilitaryServiceModel(int? userId)
        {
            // TODO: EMPL доработать реализацию
            MilitaryServiceModel model = new MilitaryServiceModel();
            LoadDictionaries(model);
            return model;
        }

        public ExperienceModel GetExperienceModel(int? userId)
        {
            // TODO: EMPL заменить реализацией
            return new ExperienceModel();
        }

        public ContactsModel GetContactsModel(int? userId)
        {
            // TODO: EMPL заменить реализацией
            return new ContactsModel();
        }

        public BackgroundCheckModel GetBackgroundCheckModel(int? userId)
        {
            // TODO: EMPL заменить реализацией
            return new BackgroundCheckModel();
        }

        public OnsiteTrainingModel GetOnsiteTrainingModel(int? userId)
        {
            // TODO: EMPL заменить реализацией
            return new OnsiteTrainingModel();
        }

        public ManagersModel GetManagersModel(int? userId)
        {
            // TODO: EMPL заменить реализацией
            return new ManagersModel();
        }

        public PersonnelManagersModel GetPersonnelManagersModel(int? userId)
        {
            // TODO: EMPL заменить реализацией
            return new PersonnelManagersModel();
        }

        public RosterModel GetRosterModel()
        {
            // TODO: EMPL заменить реализацией
            return new RosterModel();
        }

        public SignersModel GetSignersModel()
        {
            // TODO: EMPL заменить реализацией
            return new SignersModel();
        }

        #endregion

        #region Set Model

        public void SetGeneralInfoModel(GeneralInfoModel model, bool hasError)
        {

        }
        
        public void SetPassportModel(PassportModel model, bool hasError)
        {

        }
        
        public void SetEducationModel(EducationModel model, bool hasError)
        {

        }
        
        public void SetFamilyModel(FamilyModel model, bool hasError)
        {

        }
        
        public void SetMilitaryServiceModel(MilitaryServiceModel model, bool hasError)
        {

        }
        
        public void SetExperienceModel(ExperienceModel model, bool hasError)
        {

        }
        
        public void SetContactsModel(ContactsModel model, bool hasError)
        {

        }
        
        public void SetBackgroundCheckModel(BackgroundCheckModel model, bool hasError)
        {

        }
        
        public void SetOnsiteTrainingModel(OnsiteTrainingModel model, bool hasError)
        {

        }
        
        public void SetManagersModel(ManagersModel model, bool hasError)
        {

        }
        
        public void SetPersonnelManagersModel(PersonnelManagersModel model, bool hasError)
        {

        }
        
        public void SetRosterModel(RosterModel model, bool hasError)
        {

        }
        
        public void SetSignersModel(SignersModel model, bool hasError)
        {

        }

        #endregion

        #region Save Model

        public bool SaveModel<TVM, TE>(TVM model, out string error)
            where TVM: AbstractEmploymentModel
            where TE: new()
        {
            error = string.Empty;
            User user = null;
            IUser current = AuthenticationService.CurrentUser;
            TE entity = new TE(); ;
            try
            {
                user = UserDao.Load(model.UserId);
                if (model.UserId == AuthenticationService.CurrentUser.Id)
                {
                    SetEntity<TVM, TE>(entity, model);
                    EmploymentCommonDao.SaveOrUpdateDocument<TE>(entity, model.UserId);
                }
            }
            catch (Exception)
            {

            }
            finally
            {

            }

            return false;
        }

        #endregion

        #region LoadDictionaries

        protected void LoadDictionaries(GeneralInfoModel model)
        {
            // Страны/Гражданства
            //model.CitizenshipItems = CountryDao.LoadAllSorted().ToList().ConvertAll(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).OrderBy(x => x.Value);
            
            /*model.InsuredPersonTypeItems = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem {Text = "Тип1", Value = "1"},
                    new SelectListItem {Text = "Тип2", Value = "2"},
                    new SelectListItem {Text = "Тип3", Value = "3"}
                },
                "Value", "Text"
            );
            
            model.StatusItems = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem {Text = "Резидент", Value = "1"},
                    new SelectListItem {Text = "Нерезидент", Value = "2"}
                },
                "Value", "Text"
            );*/
        }
        protected void LoadDictionaries(PassportModel model)
        {
            /*model.DocumentTypeItems = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem {Text = "Паспорт РФ", Value = "1"},
                    new SelectListItem {Text = "Военный билет", Value = "2"},
                    new SelectListItem {Text = "Водительское удостоверение", Value = "3"},
                    new SelectListItem {Text = "Студенческий билет", Value = "4"},
                    new SelectListItem {Text = "Справка", Value = "5"}
                },
                "Value", "Text"
            );*/
        }
        protected void LoadDictionaries(EducationModel model)
        {

        }
        protected void LoadDictionaries(FamilyModel model)
        {

        }
        protected void LoadDictionaries(MilitaryServiceModel model)
        {            
            model.RankItems = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem {Text = "Подлежит призыву", Value = "0"},
                    new SelectListItem {Text = "Рядовой", Value = "1"},
                    new SelectListItem {Text = "Матрос", Value = "2"},
                    new SelectListItem {Text = "Ефрейтор", Value = "3"},
                    new SelectListItem {Text = "Ст. матрос", Value = "4"},
                    new SelectListItem {Text = "Мл. сержант", Value = "5"},
                    new SelectListItem {Text = "Старшина 2-й статьи", Value = "6"},
                    new SelectListItem {Text = "Сержант", Value = "7"},
                    new SelectListItem {Text = "Старшина 1-й статьи", Value = "8"},
                    new SelectListItem {Text = "Ст. сержант", Value = "9"},
                    new SelectListItem {Text = "Гл. старшина", Value = "10"},
                    new SelectListItem {Text = "Старшина", Value = "11"},
                    new SelectListItem {Text = "Гл. корабельный старшина", Value = "12"},
                    new SelectListItem {Text = "Прапорщик", Value = "13"},
                    new SelectListItem {Text = "Мичман", Value = "14"},
                    new SelectListItem {Text = "Ст. прапорщик", Value = "15"},
                    new SelectListItem {Text = "Ст. мичман", Value = "16"},
                    new SelectListItem {Text = "Мл. лейтенант", Value = "17"},
                    new SelectListItem {Text = "Лейтенант", Value = "18"},
                    new SelectListItem {Text = "Ст. лейтенант", Value = "19"},
                    new SelectListItem {Text = "Капитан", Value = "20"},
                    new SelectListItem {Text = "Капитан-лейтенант", Value = "21"},
                    new SelectListItem {Text = "Майор", Value = "22"},
                    new SelectListItem {Text = "Капитан 3 ранга", Value = "23"},
                    new SelectListItem {Text = "Подполковник", Value = "24"},
                    new SelectListItem {Text = "Капитан 2 ранга", Value = "25"},
                    new SelectListItem {Text = "Полковник", Value = "26"},
                    new SelectListItem {Text = "Капитан 1 ранга", Value = "27"},
                    new SelectListItem {Text = "Генерал-майор", Value = "28"},
                    new SelectListItem {Text = "Контр-адмирал", Value = "29"},
                    new SelectListItem {Text = "Генерал-лейтенант", Value = "30"},
                    new SelectListItem {Text = "Вице-адмирал", Value = "31"},
                    new SelectListItem {Text = "Генерал-полковник", Value = "32"},
                    new SelectListItem {Text = "Адмирал", Value = "33"},
                    new SelectListItem {Text = "Генерал армии", Value = "34"},
                    new SelectListItem {Text = "Адмирал флота", Value = "35"},
                    new SelectListItem {Text = "Маршал РФ", Value = "36"}
                },
                "Value", "Text"
            );
            model.RegistrationExpirationItems = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem {Text = "-", Value = "0"},
                    new SelectListItem {Text = "Снят с воинского учета по возрасту", Value = "1"},
                    new SelectListItem {Text = "Снят с воинского учета по состоянию здоровья", Value = "2"}
                },
                "Value", "Text"
            );
            model.PersonnelCategoryItems = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem {Text = "Руководители", Value = "1"},
                    new SelectListItem {Text = "Специальности", Value = "2"},
                    new SelectListItem {Text = "Другие служащие", Value = "3"},
                    new SelectListItem {Text = "Рабочие", Value = "4"}
                },
                "Value", "Text"
            );
            model.PersonnelTypeItems = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem {Text = "Офицеры", Value = "1"},
                    new SelectListItem {Text = "Прочие (прапорщики, солдаты, мичманы, сержанты, матросы...)", Value = "2"}
                },
                "Value", "Text"
            );
            model.ConscriptionStatusItems = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem {Text = "Не подлежит", Value = "1"},
                    new SelectListItem {Text = "Подлежит", Value = "2"},
                    new SelectListItem {Text = "Ограниченно годен", Value = "3"}
                },
                "Value", "Text"
            );
        }
        protected void LoadDictionaries(ExperienceModel model)
        {

        }
        protected void LoadDictionaries(ContactsModel model)
        {

        }
        protected void LoadDictionaries(BackgroundCheckModel model)
        {

        }
        protected void LoadDictionaries(OnsiteTrainingModel model)
        {

        }
        protected void LoadDictionaries(ManagersModel model)
        {

        }
        protected void LoadDictionaries(PersonnelManagersModel model)
        {

        }
        protected void LoadDictionaries(RosterModel model)
        {

        }
        protected void LoadDictionaries(SignersModel model)
        {

        }

        #endregion

        #region SetEntity

        protected void SetEntity<TVM, TE>(TE entity, TVM viewModel)
        {
            switch (entity.GetType().Name)
            {
                case "GeneralInfo":
                    SetGeneralInfoEntity(entity as GeneralInfo, viewModel as GeneralInfoModel);
                    break;
                case "Passport":
                    SetPassportEntity(entity as Passport, viewModel as PassportModel);
                    break;
                default:
                    break;
            }            
        }

        protected void SetGeneralInfoEntity(GeneralInfo entity, GeneralInfoModel viewModel)
        {
            //entity.AgreedToPersonalDataProcessing = viewModel.AgreedToPersonalDataProcessing;
            entity.Candidate = EmploymentCommonDao.GetCandidateByUserId(viewModel.UserId);
            // entity.Citizenship = 
            entity.CityOfBirth = viewModel.CityOfBirth;
            entity.DateOfBirth = viewModel.DateOfBirth;
            // entity.Disabilities = 
            entity.DistrictOfBirth = viewModel.DistrictOfBirth;
            entity.FirstName = viewModel.FirstName;
            // entity.ForeignLanguages = 

            entity.INN = viewModel.INN;
            // entity.InsuredPersonType = 
            entity.IsMale = viewModel.IsMale;
            entity.LastName = viewModel.LastName;
            // entity.NameChanges = 
            entity.Patronymic = viewModel.Patronymic;
            entity.RegionOfBirth = viewModel.RegionOfBirth;
            entity.SNILS = viewModel.SNILS;
            //entity.Status = viewModel.Status;
            // entity.Version = 
        }

        protected void SetPassportEntity(Passport entity, PassportModel viewModel)
        {
            entity.Apartment = viewModel.Apartment;
            entity.Building = viewModel.Building;
            entity.Candidate = EmploymentCommonDao.GetCandidateByUserId(viewModel.UserId);
            entity.City = viewModel.City;
            entity.District = viewModel.District;
            //entity.DocumentType
            entity.InternalPassportDateOfIssue = viewModel.InternalPassportDateOfIssue;
            entity.InternalPassportIssuedBy = viewModel.InternalPassportIssuedBy;
            entity.InternalPassportNumber = viewModel.InternalPassportNumber;
            entity.InternalPassportSeries = viewModel.InternalPassportSeries;
            entity.InternalPassportSubdivisionCode = viewModel.InternalPassportSubdivisionCode;
            entity.InternationalPassportDateOfIssue = viewModel.InternationalPassportDateOfIssue;
            entity.InternationalPassportIssuedBy = viewModel.InternationalPassportIssuedBy;
            entity.InternationalPassportNumber = viewModel.InternationalPassportNumber;
            entity.InternationalPassportSeries = viewModel.InternationalPassportSeries;
            entity.Region = viewModel.Region;
            entity.RegistrationDate = viewModel.RegistrationDate;
            entity.Street = viewModel.Street;
            entity.StreetNumber = viewModel.StreetNumber;
            entity.ZipCode = viewModel.ZipCode;
        }

        #endregion
    }
}