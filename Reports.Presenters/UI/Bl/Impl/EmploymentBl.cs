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
                model.InternalPassportNumber = entity.InternalPassportNumber;
                model.InternalPassportSeries = entity.InternalPassportSeries;
                model.InternalPassportSubdivisionCode = entity.InternalPassportSubdivisionCode;
                //model.InternationalPassportDateOfIssue = entity.InternationalPassportDateOfIssue;
                model.InternationalPassportIssuedBy = entity.InternationalPassportIssuedBy;
                model.InternationalPassportNumber = entity.InternationalPassportNumber;
                model.InternationalPassportSeries = entity.InternationalPassportSeries;
                model.Region = entity.Region;
                model.RegistrationDate = entity.RegistrationDate;
                model.Street = entity.Street;
                model.StreetNumber = entity.StreetNumber;
                model.UserId = entity.Candidate.User.Id;
                model.ZipCode = entity.ZipCode;
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
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            EducationModel model = new EducationModel();
            Education entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<Education>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentEducationDao.Get(id.Value);
            }
            if (entity != null)
            {
                //model.Certifications
                //model.HigherEducationDiplomas
                //model.PostGraduateEducationDiplomas
                //model.Training
                model.UserId = entity.Candidate.User.Id;
            }
            else
            {
                model = new EducationModel { UserId = userId.Value };
            }
            LoadDictionaries(model);
            return model;
        }

        public FamilyModel GetFamilyModel(int? userId)
        {
            // TODO: EMPL заменить реализацией
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            FamilyModel model = new FamilyModel();
            Family entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<Family>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentFamilyDao.Get(id.Value);
            }
            if (entity != null)
            {
                //model.Certifications
                //model.HigherEducationDiplomas
                //model.PostGraduateEducationDiplomas
                //model.Training
                model.UserId = entity.Candidate.User.Id;
            }
            else
            {
                model = new FamilyModel { UserId = userId.Value };
            }
            LoadDictionaries(model);
            return model;
        }

        public MilitaryServiceModel GetMilitaryServiceModel(int? userId)
        {
            // TODO: EMPL доработать реализацию
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            MilitaryServiceModel model = new MilitaryServiceModel();
            MilitaryService entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<MilitaryService>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentMilitaryServiceDao.Get(id.Value);
            }
            if (entity != null)
            {
                model.CombatFitness = entity.CombatFitness;
                model.Commissariat = entity.Commissariat;
                //model.ConscriptionStatus
                model.IsAssigned = entity.IsAssigned;
                model.IsLiableForMilitaryService = entity.IsLiableForMilitaryService;
                model.MilitaryCardDate = entity.MilitaryCardDate;
                model.MilitaryCardNumber = entity.MilitaryCardNumber;
                model.MilitaryServiceRegistrationInfo = entity.MilitaryServiceRegistrationInfo;
                model.MilitarySpecialityCode = entity.MilitarySpecialityCode;
                //model.PersonnelCategory = entity.PersonnelCategory;
                //model.PersonnelType
                //model.Rank
                //model.RegistrationExpiration = entity.RegistrationExpiration;
                //model.ReserveCategory = entity.ReserveCategory;
                model.SpecialityCategory = entity.SpecialityCategory;
                model.UserId = entity.Candidate.User.Id;
            }
            else
            {
                model = new MilitaryServiceModel { UserId = userId.Value };
            }
            LoadDictionaries(model);
            return model;
        }

        public ExperienceModel GetExperienceModel(int? userId)
        {
            // TODO: EMPL заменить реализацией
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            ExperienceModel model = new ExperienceModel();
            Experience entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<Experience>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentExperienceDao.Get(id.Value);
            }
            if (entity != null)
            {
                model.UserId = entity.Candidate.User.Id;
                model.WorkBookDateOfIssue = entity.WorkBookDateOfIssue;
                model.WorkBookNumber = entity.WorkBookNumber;
                model.WorkBookSeries = entity.WorkBookSeries;
                model.WorkBookSupplementDateOfIssue = entity.WorkBookSupplementDateOfIssue;
                model.WorkBookSupplementNumber = entity.WorkBookSupplementNumber;
                model.WorkBookSupplementSeries = entity.WorkBookSupplementSeries;
            }
            else
            {
                model = new ExperienceModel { UserId = userId.Value };
            }
            LoadDictionaries(model);
            return model;
        }

        public ContactsModel GetContactsModel(int? userId)
        {
            // TODO: EMPL заменить реализацией
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            ContactsModel model = new ContactsModel();
            Contacts entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<Contacts>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentContactsDao.Get(id.Value);
            }
            if (entity != null)
            {
                model.Apartment = entity.Apartment;
                model.Building = entity.Building;
                model.City = entity.City;
                model.District = entity.District;
                model.Email = entity.Email;
                model.HomePhone = entity.HomePhone;
                model.Mobile = entity.Mobile;
                model.Region = entity.Region;
                model.Street = entity.Street;
                model.StreetNumber = entity.StreetNumber;
                model.UserId = entity.Candidate.User.Id;
                model.WorkPhone = entity.WorkPhone;
                model.ZipCode = entity.ZipCode;
            }
            else
            {
                model = new ContactsModel { UserId = userId.Value };
            }
            LoadDictionaries(model);
            return model;
        }

        public BackgroundCheckModel GetBackgroundCheckModel(int? userId)
        {
            // TODO: EMPL заменить реализацией
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            BackgroundCheckModel model = new BackgroundCheckModel();
            BackgroundCheck entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<BackgroundCheck>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentBackgroundCheckDao.Get(id.Value);
            }
            if (entity != null)
            {
                model.AutomobileLicensePlateNumber = entity.AutomobileLicensePlateNumber;
                model.AutomobileMake = entity.AutomobileMake;
                model.AverageSalary = entity.AverageSalary;
                model.ChronicalDiseases = entity.ChronicalDiseases;
                //model.DriversLicenseCategories = entity.DriversLicenseCategories;
                //model.DriversLicenseDateOfIssue = entity.DriversLicenseDateOfIssue;
                model.DriversLicenseNumber = entity.DriversLicenseNumber;
                model.DrivingExperience = entity.DrivingExperience;
                model.HasAutomobile = entity.AutomobileMake.Length > 0;
                model.HasDriversLicense = entity.DriversLicenseDateOfIssue.HasValue;
                model.Hobbies = entity.Hobbies;
                model.ImportantEvents = entity.ImportantEvents;
                model.IsReadyForBusinessTrips = entity.IsReadyForBusinessTrips;
                model.Liabilities = entity.Liabilities;
                model.MilitaryOperationsExperience = entity.MilitaryOperationsExperience;
                model.PositionSought = entity.PositionSought;
                model.PreviousDismissalReason = entity.PreviousDismissalReason;
                model.PreviousSuperior = entity.PreviousSuperior;
                //model.References
                model.Sports = entity.Sports;
                model.UserId = entity.Candidate.User.Id;
            }
            else
            {
                model = new BackgroundCheckModel { UserId = userId.Value };
            }
            LoadDictionaries(model);
            return model;
        }

        public OnsiteTrainingModel GetOnsiteTrainingModel(int? userId)
        {
            // TODO: EMPL заменить реализацией
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            OnsiteTrainingModel model = new OnsiteTrainingModel();
            OnsiteTraining entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<OnsiteTraining>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentOnsiteTrainingDao.Get(id.Value);
            }
            if (entity != null)
            {
                model.BeginningDate = entity.BeginningDate;
                model.Comments = entity.Comments;
                model.Description = entity.Description;
                model.EndDate = entity.EndDate;
                model.IsComplete = entity.IsComplete;
                model.IsConfirmed = entity.IsConfirmed;
                model.ReasonsForIncompleteTraining = entity.ReasonsForIncompleteTraining;
                model.Results = entity.Results;
                model.Type = entity.Type;
                model.UserId = entity.Candidate.User.Id;
            }
            else
            {
                model = new OnsiteTrainingModel { UserId = userId.Value };
            }
            LoadDictionaries(model);
            return model;
        }

        public ManagersModel GetManagersModel(int? userId)
        {
            // TODO: EMPL заменить реализацией
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            ManagersModel model = new ManagersModel();
            Managers entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<Managers>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentManagersDao.Get(id.Value);
            }
            if (entity != null)
            {
                model.Bonus = entity.Bonus;
                // Department, Directorate, etc ???
                model.IsFront = entity.IsFront;
                model.IsLiable = entity.IsLiable;
                model.PersonalAddition = entity.PersonalAddition;
                model.PositionAddition = entity.PositionAddition;
                model.ProbationaryPeriod = entity.ProbationaryPeriod;
                model.RequestNumber = entity.RequestNumber;
                model.UserId = entity.Candidate.User.Id;
                model.WorkCity = entity.WorkCity;

            }
            else
            {
                model = new ManagersModel { UserId = userId.Value };
            }
            LoadDictionaries(model);
            return model;
        }

        public PersonnelManagersModel GetPersonnelManagersModel(int? userId)
        {
            // TODO: EMPL заменить реализацией
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            PersonnelManagersModel model = new PersonnelManagersModel();
            PersonnelManagers entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<Managers>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentPersonnelManagersDao.Get(id.Value);
            }
            if (entity != null)
            {
                //model.ApprovedByPersonnelManager = entity.ApprovedByPersonnelManager;
                model.AreaAddition = entity.AreaAddition;
                model.AreaMultiplier = entity.AreaMultiplier;
                model.CompetenceAddition = entity.CompetenceAddition;
                model.ContractDate = entity.ContractDate;
                model.ContractNumber = entity.ContractNumber;
                model.EmploymentDate = entity.EmploymentDate;
                model.EmploymentOrderDate = entity.EmploymentOrderDate;
                model.EmploymentOrderNumber = entity.EmploymentOrderNumber;
                model.FrontOfficeExperienceAddition = entity.FrontOfficeExperienceAddition;
                model.InsurableExperienceDays = entity.InsurableExperienceDays;
                model.InsurableExperienceMonths = entity.InsurableExperienceMonths;
                model.InsurableExperienceYears = entity.InsurableExperienceYears;
                model.NorthernAreaAddition = entity.InsurableExperienceYears;
                model.OverallExperienceDays = entity.OverallExperienceDays;
                model.OverallExperienceMonths = entity.OverallExperienceMonths;
                model.OverallExperienceYears = entity.OverallExperienceYears;
                model.PersonalAccount = entity.PersonalAccount;
                model.PersonalAccountContractor = entity.PersonalAccountContractor;
                model.TravelRelatedAddition = entity.TravelRelatedAddition;
                model.UserId = entity.Candidate.User.Id;
            }
            else
            {
                model = new PersonnelManagersModel { UserId = userId.Value };
            }
            LoadDictionaries(model);
            return model;
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
            catch (Exception exc)
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
                case "Education":
                    SetEducationEntity(entity as Education, viewModel as EducationModel);
                    break;
                case "Family":
                    SetFamilyEntity(entity as Family, viewModel as FamilyModel);
                    break;
                case "MilitaryService":
                    SetMilitaryServiceEntity(entity as MilitaryService, viewModel as MilitaryServiceModel);
                    break;
                case "Experience":
                    SetExperienceEntity(entity as Experience, viewModel as ExperienceModel);
                    break;
                case "Contacts":
                    SetContactsEntity(entity as Contacts, viewModel as ContactsModel);
                    break;
                case "BackgroundCheck":
                    SetBackgroundCheckEntity(entity as BackgroundCheck, viewModel as BackgroundCheckModel);
                    break;
                case "OnsiteTraining":
                    SetOnsiteTrainingEntity(entity as OnsiteTraining, viewModel as OnsiteTrainingModel);
                    break;
                case "Managers":
                    SetManagersEntity(entity as Managers, viewModel as ManagersModel);
                    break;
                case "PersonnelManagers":
                    SetPersonnelManagersEntity(entity as PersonnelManagers, viewModel as PersonnelManagersModel);
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

        protected void SetEducationEntity(Education entity, EducationModel viewModel)
        {
            entity.Candidate = EmploymentCommonDao.GetCandidateByUserId(viewModel.UserId);
            //entity.Certifications
            //entity.HigherEducationDiplomas
            //entity.PostGraduateEducationDiplomas
            //entity.Training
        }

        protected void SetFamilyEntity(Family entity, FamilyModel viewModel)
        {
            entity.Candidate = EmploymentCommonDao.GetCandidateByUserId(viewModel.UserId);
            //entity.Children
            entity.Cohabitants = viewModel.Cohabitants;
            //entity.Father
            //entity.Mother
            //entity.Spouse
        }

        protected void SetMilitaryServiceEntity(MilitaryService entity, MilitaryServiceModel viewModel)
        {
            entity.Candidate = EmploymentCommonDao.GetCandidateByUserId(viewModel.UserId);
            entity.CombatFitness = viewModel.CombatFitness;
            entity.Commissariat = viewModel.Commissariat;
            //entity.ConscriptionStatus = viewModel.ConscriptionStatus;
            entity.IsAssigned = viewModel.IsAssigned;
            entity.IsLiableForMilitaryService = viewModel.IsLiableForMilitaryService;
            entity.MilitaryCardDate = viewModel.MilitaryCardDate;
            entity.MilitaryCardNumber = viewModel.MilitaryCardNumber;
            entity.MilitaryServiceRegistrationInfo = viewModel.MilitaryServiceRegistrationInfo;
            entity.MilitarySpecialityCode = viewModel.MilitarySpecialityCode;
            //entity.PersonnelCategory
            //entity.PersonnelType
            //entity.Rank
            //entity.RegistrationExpiration
            //entity.ReserveCategory = viewModel.ReserveCategory;
            entity.SpecialityCategory = viewModel.SpecialityCategory;
        }

        protected void SetExperienceEntity(Experience entity, ExperienceModel viewModel)
        {
            entity.Candidate = EmploymentCommonDao.GetCandidateByUserId(viewModel.UserId);
            //entity.ExperienceItems
            entity.WorkBookDateOfIssue = viewModel.WorkBookDateOfIssue;
            entity.WorkBookNumber = viewModel.WorkBookNumber;
            entity.WorkBookSeries = viewModel.WorkBookSeries;            
            entity.WorkBookSupplementDateOfIssue = viewModel.WorkBookSupplementDateOfIssue;
            entity.WorkBookSupplementNumber = viewModel.WorkBookSupplementNumber;
            entity.WorkBookSupplementSeries = viewModel.WorkBookSupplementSeries;
        }

        protected void SetContactsEntity(Contacts entity, ContactsModel viewModel)
        {
            entity.Apartment = viewModel.Apartment;
            entity.Building = viewModel.Building;
            entity.Candidate = EmploymentCommonDao.GetCandidateByUserId(viewModel.UserId);
            entity.City = viewModel.City;
            entity.District = viewModel.District;
            entity.Email = viewModel.Email;
            entity.HomePhone = viewModel.HomePhone;
            entity.Mobile = viewModel.Mobile;
            entity.Region = viewModel.Region;
            entity.Street = viewModel.Street;
            entity.StreetNumber = viewModel.StreetNumber;
            entity.WorkPhone = viewModel.WorkPhone;
            entity.ZipCode = viewModel.ZipCode;
        }

        protected void SetBackgroundCheckEntity(BackgroundCheck entity, BackgroundCheckModel viewModel)
        {
            entity.AutomobileLicensePlateNumber = viewModel.AutomobileLicensePlateNumber;
            entity.AutomobileMake = viewModel.AutomobileMake;
            entity.AverageSalary = viewModel.AverageSalary;
            entity.Candidate = EmploymentCommonDao.GetCandidateByUserId(viewModel.UserId);
            entity.ChronicalDiseases = viewModel.ChronicalDiseases;
            //entity.DriversLicenseCategories = viewModel.DriversLicenseCategories;
            entity.DriversLicenseDateOfIssue = viewModel.DriversLicenseDateOfIssue;
            entity.DriversLicenseNumber = viewModel.DriversLicenseNumber;
            entity.DrivingExperience = viewModel.DrivingExperience;
            entity.Hobbies = viewModel.Hobbies;
            entity.ImportantEvents = viewModel.ImportantEvents;
            entity.IsReadyForBusinessTrips = viewModel.IsReadyForBusinessTrips;
            entity.Liabilities = viewModel.Liabilities;
            entity.MilitaryOperationsExperience = viewModel.MilitaryOperationsExperience;
            entity.PositionSought = viewModel.PositionSought;
            entity.PreviousDismissalReason = viewModel.PreviousDismissalReason;
            entity.PreviousSuperior = viewModel.PreviousSuperior;
            //entity.References = 
            entity.Sports = viewModel.Sports;
        }

        protected void SetOnsiteTrainingEntity(OnsiteTraining entity, OnsiteTrainingModel viewModel)
        {
            entity.BeginningDate = viewModel.BeginningDate;
            entity.Candidate = EmploymentCommonDao.GetCandidateByUserId(viewModel.UserId);
            entity.Comments = viewModel.Comments;
            entity.Description = viewModel.Description;
            entity.EndDate = viewModel.EndDate;
            entity.IsComplete = viewModel.IsComplete;
            entity.IsConfirmed = viewModel.IsConfirmed;
            entity.ReasonsForIncompleteTraining = viewModel.ReasonsForIncompleteTraining;
            entity.Results = viewModel.Results;
            entity.Type = viewModel.Type;
        }

        protected void SetManagersEntity(Managers entity, ManagersModel viewModel)
        {
            entity.Bonus = viewModel.Bonus;
            entity.Candidate = EmploymentCommonDao.GetCandidateByUserId(viewModel.UserId);
            //entity.Department = 
            //entity.Directorate = 
            //entity.EmploymentConditions = 
            entity.IsFront = viewModel.IsFront;
            entity.IsLiable = viewModel.IsLiable;
            entity.PersonalAddition = viewModel.PersonalAddition; 
            //entity.Position
            entity.PositionAddition = viewModel.PositionAddition;
            entity.ProbationaryPeriod = viewModel.ProbationaryPeriod;
            entity.RequestNumber = viewModel.RequestNumber;
            //entity.Schedule = 
            entity.WorkCity = viewModel.WorkCity;
        }

        protected void SetPersonnelManagersEntity(PersonnelManagers entity, PersonnelManagersModel viewModel)
        {
            //entity.ApprovedByPersonnelManager = viewModel.ApprovedByPersonnelManager;
            entity.AreaAddition = viewModel.AreaAddition;
            entity.AreaMultiplier = viewModel.AreaMultiplier;
            entity.Candidate = EmploymentCommonDao.GetCandidateByUserId(viewModel.UserId);
            entity.CompetenceAddition = viewModel.CompetenceAddition;
            entity.ContractDate = viewModel.ContractDate;
            entity.ContractNumber = viewModel.ContractNumber;
            entity.EmploymentDate = viewModel.EmploymentDate;
            entity.EmploymentOrderDate = viewModel.EmploymentOrderDate;
            entity.EmploymentOrderNumber = viewModel.EmploymentOrderNumber;
            entity.FrontOfficeExperienceAddition = viewModel.FrontOfficeExperienceAddition;
            entity.InsurableExperienceDays = viewModel.InsurableExperienceDays;
            entity.InsurableExperienceMonths = viewModel.InsurableExperienceMonths;
            entity.InsurableExperienceYears = viewModel.InsurableExperienceYears;
            entity.NorthernAreaAddition = viewModel.NorthernAreaAddition;
            entity.OverallExperienceDays = viewModel.OverallExperienceDays;
            entity.OverallExperienceMonths = viewModel.OverallExperienceMonths;
            entity.OverallExperienceYears = viewModel.OverallExperienceYears;
            entity.PersonalAccount = viewModel.PersonalAccount;
            entity.PersonalAccountContractor = viewModel.PersonalAccountContractor;
            entity.TravelRelatedAddition = viewModel.TravelRelatedAddition;
        }

        #endregion
    }
}