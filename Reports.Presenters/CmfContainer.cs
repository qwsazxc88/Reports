//using Acr3S.Web;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Reports.CommonWeb;
using Reports.Core.Services;
using Reports.Presenters.Services;
using Reports.Presenters.Services.Impl;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.Bl.Impl;

namespace Reports.Presenters
{
    public class CmfContainer : WebContainer
    {
        public override void AddComponents()
        {

            base.AddComponents();
            Register(Component.For(typeof(IDocumentMovementsBl))
                        .ImplementedBy(typeof(DocumentMovementsBl))
                        .LifeStyle.Is(type));
            Register(Component.For(typeof(IBaseBl))
                        .ImplementedBy(typeof(BaseBl))
                        .LifeStyle.Is(type));
            //AddComponent<ILoginBl, LoginBl>();
            Register(Component.For(typeof(ILoginBl))
                        .ImplementedBy(typeof(LoginBl))
                        .LifeStyle.Is(type));
            //AddComponent<IBaseBl, BaseBl>();
            Register(Component.For(typeof(IEmployeeBl))
                        .ImplementedBy(typeof (EmployeeBl))
                        .LifeStyle.Is(type));
            //AddComponentent<IEmployeeBl, EmployeeBl>();

            Register(Component.For(typeof(IAdminBl))
                         .ImplementedBy(typeof(AdminBl))
                         .LifeStyle.Is(type));

            Register(Component.For(typeof(IRequestBl))
                        .ImplementedBy(typeof(RequestBl))
                        .LifeStyle.Is(type));

            Register(Component.For(typeof(IAppointmentBl))
                        .ImplementedBy(typeof(AppointmentBl))
                        .LifeStyle.Is(type));

            Register(Component.For(typeof(IHelpBl))
                      .ImplementedBy(typeof(HelpBl))
                      .LifeStyle.Is(type));

            Register(Component.For(typeof(IEmploymentBl))
                        .ImplementedBy(typeof(EmploymentBl))
                        .LifeStyle.Is(type));

            Register(Component.For(typeof(IAutoComplete))
                       .ImplementedBy(typeof(AutoComplete))
                       .LifeStyle.Is(type));

            Register(Component.For(typeof(IGpdBl))
                        .ImplementedBy(typeof(GpdBl))
                        .LifeStyle.Is(type));
            //AddComponent<IAdminBl, AdminBl>();
            Register(Component.For(typeof(ISurchargeBL))
                .ImplementedBy(typeof(SurchargeBl))
                .LifeStyle.Is(type));
            Register(Component.For(typeof(IStaffListBl))
                        .ImplementedBy(typeof(StaffListBl))
                        .LifeStyle.Is(type));
            
            Register(Component.For(typeof(IStaffMovementsBl))
                       .ImplementedBy(typeof(StaffMovementsBl))
                       .LifeStyle.Is(type));
            Register(Component.For(typeof(IAuthenticationService)).ImplementedBy(typeof(AuthenticationService)).
                    Named("IAuthenticationService").LifeStyle.Is(type));
            //AddComponent("IAuthenticationService", typeof(IAuthenticationService), typeof(AuthenticationService));
            Register(Component.For(typeof(IFormsAuthenticationService)).ImplementedBy(typeof(FormsAuthenticationService)).
                    Named("IFormsAuthenticationService").LifeStyle.Is(type));
            //AddComponent("IFormsAuthenticationService", typeof(IFormsAuthenticationService),typeof(FormsAuthenticationService));
            Register(Component.For(typeof (IConfigurationService)).ImplementedBy(typeof (ConfigurationService)).
                    Named("IConfigurationService").LifeStyle.Is(type));
            //AddComponent("ConfigurationService", typeof(IConfigurationService), typeof(ConfigurationService));

//			  AddComponent<IActionResolver, DefaultActionResolver>();
//            AddComponent("CipRedirectActionResolver", typeof(IActionResolver), typeof(CipRedirectActionResolver));


//            AddComponent("CreateCasePresenter", typeof (CreateCasePresenter));
//            AddComponent("CaseListPresenter", typeof(CaseListPresenter));
//            AddComponent("CasePropsPresenter", typeof(CasePropsPresenter ));
            //AddComponent("GetDocHandler", typeof(GetDocHandler));
            //AddComponent<ViewHeaderPresenter>();
//            AddComponent<ViewCaseQuestionsPresenter>();
//            AddComponent<ViewCaseContentPresenter>();
//            AddComponent<ViewCaseTemplatedItemsPresenter>();
//            AddComponent<ViewCaseTextItemsPresenter>();
//            AddComponent<ViewCaseCreditsPresenter>();
//            AddComponent<ViewCaseImagesPresenter>();
//            AddComponent<BaseCasePreviewPresenter>();
//            AddComponent<SimpleSearchFilterPresenter>();
//            AddComponent<SimpleSearchListPresenter>();
//            AddComponent<InsertImagePresenter>();
//            AddComponent<PreviewImagePresenter>();
//            AddComponent<InsertImageInstanceToFindingSectionTextPresenter>();
//            AddComponent<PreviewRichTextPresenter>();
            //AddComponent<UserPropertiesPresenter>();
            //AddComponent<UsersListPresenter>();
            //AddComponent<UserChangePasswordPresenter>();
            //AddComponent<CalculationListsPresenter>();
            //AddComponent<NDFLPresenter>();
            //AddComponent<UserLoginListPresenter>();
            //AddComponent<QuestionsListPresenter>();
            //AddComponent<QuestionPresenter>();
//              AddComponent<ViewCalcListPresenter>();
//            AddComponent<EditImageInstancePresenter>();
//			  AddComponent("ConfigurationService", typeof(IConfigurationService), typeof(ConfigurationService));
//            AddComponent("ImageStorageService", typeof(IImageStorageService), typeof(ImageStorageService));

//            AddComponent("IEmailService", typeof(IEmailService), typeof(EmailService));
//            AddComponent("IMailService", typeof(IMailService), typeof(MailService));
//            AddComponent("ConfigurationViewContentElementService", typeof(IConfigurationViewContentElementService), typeof(ConfigurationViewContentElementService));
//            AddComponent("ContentElementConverterService", typeof(IContentElementConverterService), typeof(ContentElementConverterService));
//            AddComponent<IConfigurationProductSitesService, ConfigurationProductSitesService>();
//            AddComponent("TimssConfigSettingsManager", typeof(ITimssConfigSettingsManager), typeof(TimssConfigSettingsManager));
//            AddComponent<ISearchCasesSettingsConfigManager, SearchCasesSettingsConfigManager>();
//            AddComponent<IConfigurationCryptoKeyService,ConfigurationCryproKeyService>();
//            AddComponent<ICryptoService, CryptoService>();
//            AddComponent<IConfigurationCipTemplatedScriptItemService, ConfigurationCipTemplatedScriptItemService>();

//            AddComponent<MasterPagePresenter<IMasterPage>>();


//            // Admin parts
//            AddComponent<ListUserCreditsCasesPresenter>();
//            AddComponent<CourseCasesListPresenter>();
//            AddComponent("GetImageHandler", typeof(GetImageHandler));
//            AddComponent("PendingApprovedPresenter", typeof(PendingApprovedPresenter));
//            AddComponent("LoginPresenter", typeof(LoginPresenter));
//            AddComponent("RestorePasswordPresenter", typeof(RestorePasswordPresenter));
//            AddComponent("FindUserAccountPresenter", typeof(FindUserAccountPresenter));
//            AddComponent("FindCoursePresenter", typeof(FindCoursePresenter));
//            AddComponent("CoursesListPresenter", typeof(CoursesListPresenter));
//            AddComponent("CoursePresenter", typeof(CoursePresenter));
//            AddComponent("ListUserAccountsPresenter", typeof(ListUserAccountsPresenter));
//            AddComponent("UserEditProfilePresenter", typeof(UserEditProfilePresenter));
//            AddComponent("UserEditProfileAttributesPresenter", typeof(UserEditProfileAttributesPresenter));
//            AddComponent("UserRegisterPresenter", typeof(UserRegisterPresenter));
//            AddComponent("UserChangePasswordPresenter", typeof(UserChangePasswordPresenter));
//            AddComponent("InstitutionPresenter", typeof(InstitutionPresenter));
//            AddComponent("InstitutionsListPresenter", typeof(InstitutionsListPresenter));
//            AddComponent("InstitutionsListSelectorPresenter", typeof(InstitutionsListSelectorPresenter));
////            AddComponent("UserInstitutionPresenter", typeof(UserInstitutionPresenter));
//            AddComponent("AdminUserCreditsPresenter", typeof(UI.Admin.UserCreditsPresenter));
//            AddComponent("ProfileUserCreditsPresenter", typeof(UI.Profile.UserCreditsPresenter));
//            AddComponent("ProfileUserCreditPresenter", typeof(UI.Profile.UserCreditPresenter));
//			  AddComponent("UserNamePresenter", typeof(UserNamePresenter));
//            AddComponent("FindInstitutionPresenter", typeof(FindInstitutionPresenter));
//            AddComponent("ImageModalityPresenter", typeof(ImageModalityPresenter));
//            AddComponent("ImageModalityListPresenter", typeof(ImageModalityListPresenter));
//            AddComponent("ImagePositioningPresenter", typeof(ImagePositioningPresenter));
//            AddComponent("ImagePositioningListPresenter", typeof(ImagePositioningListPresenter));
//            AddComponent("PulseSequenceTypePresenter", typeof(PulseSequenceTypePresenter));
//            AddComponent("PulseSequenceTypeListPresenter", typeof(PulseSequenceTypeListPresenter));
//            AddComponent("PermissionPresenter", typeof(PermissionPresenter));
//            AddComponent("ListUserSelectorPresenter", typeof(ListUserSelectorPresenter));
//            AddComponent("CategoryPresenter", typeof(CategoryPresenter));
//            AddComponent("CategoriesListPresenter", typeof(CategoriesListPresenter));
//            AddComponent("ProductCourseCategoriesPresenter", typeof(ProductCourseCategoriesPresenter));
//            AddComponent("UserEditTimssProfilePresenter", typeof(UserEditTimssProfilePresenter));
//            AddComponent("ListUserSelectorForInstitutionPresenter", typeof(ListUserSelectorForInstitutionPresenter));
//            AddComponent("ListItemsPermissionPresenter", typeof(ListItemsPermissionPresenter));
//            AddComponent<ISpellCheckerConfigurationService, SentrySpellCheckerConfigurationService>();


            //Kernel.AddComponentInstance("IEmailQueueService", typeof(IEmailQueueService),
            //                            EmailManager.Instance);
        }
    }
}