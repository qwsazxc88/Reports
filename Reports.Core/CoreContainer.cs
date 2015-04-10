using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NHibernate;
using Reports.Core.Dao;
using Reports.Core.Dao.Impl;

namespace Reports.Core
{
    public class CoreContainer : WindsorContainer
    {
        #region Fields

        private bool _initialized;
        protected LifestyleType type = LifestyleType.PerWebRequest;//LifestyleType.Thread;
        #endregion


        #region NonOptionalFacility

        //private class NonOptionalFacility : AbstractFacility
        //{
        //    protected override void Init()
        //    {
        //        Kernel.ComponentModelBuilder.AddContributor(new NonOptionalInspector());
        //    }
        //}

        #endregion


        #region NonOptionalInspector

        //private class NonOptionalInspector : IContributeComponentModelConstruction
        //{
        //    public void ProcessModel(IKernel kernel, ComponentModel model)
        //    {
        //        if (model == null)
        //            throw new ArgumentNullException("model", Resources.Exception_NoParameterInProcessModel);
        //        PropertyInfo[] props = model.Implementation.GetProperties(
        //            BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);

        //        foreach (PropertyInfo prop in props)
        //        {
        //            Attribute[] attrs = Attribute.GetCustomAttributes(prop);
        //            bool NotIsOptional = true;
        //            foreach (Attribute attr in attrs)
        //            {
        //                System.ComponentModel.CategoryAttribute catAttr = attr as System.ComponentModel.CategoryAttribute;
        //                if (catAttr != null)
        //                {
        //                    if (catAttr.Category.Equals("optional", StringComparison.OrdinalIgnoreCase))
        //                    {
        //                        NotIsOptional = false;
        //                        break;
        //                    }
        //                }
        //            }

        //            if (NotIsOptional)
        //            {
        //                PropertySet propSet = model.Properties.FindByPropertyInfo(prop);

        //                if (propSet != null)
        //                {
        //                    if (propSet.Dependency.DependencyType == DependencyType.Service)
        //                        propSet.Dependency.IsOptional = false;
        //                }
        //            }
        //        }
        //    }
        //}

        #endregion

        #region Methods
        public void Initialize()
        {
            if (!_initialized)
            {
                AddComponents();
                _initialized = true;
            }
        }

        private ISessionFactory _sessionFactory;// = null;
		public virtual ISessionFactory SessionFactory
		{
			get
			{
			    return _sessionFactory ?? NHibernateHelper.GetSessionFactory();
			}
		    set 
			{
				_sessionFactory = value;
			}
		}


        public virtual void AddComponents()
        {
           // AddFacility("NonOptionalFacility", new NonOptionalFacility());
            Register(Component.For(typeof(IUserDao)).
                     ImplementedBy(typeof(UserDao)).
                     LifeStyle.Is(type));
			//AddComponent("UserDao", typeof(IUserDao), typeof(UserDao));
            Register(Component.For(typeof(ITimesheetDayDao)).
                     ImplementedBy(typeof(TimesheetDayDao)).
                     LifeStyle.Is(type));
            //AddComponent<ITimesheetDayDao, TimesheetDayDao>();
            Register(Component.For(typeof(ITimesheetDao)).
                     ImplementedBy(typeof(TimesheetDao)).
                     LifeStyle.Is(type));
            //AddComponent<ITimesheetDao, TimesheetDao>();
            Register(Component.For(typeof(ITimesheetStatusDao)).
                     ImplementedBy(typeof(TimesheetStatusDao)).
                     LifeStyle.Is(type));
            //AddComponent<ITimesheetStatusDao, TimesheetStatusDao>();
            Register(Component.For(typeof(IDocumentDao)).
                     ImplementedBy(typeof(DocumentDao)).
                     LifeStyle.Is(type));
			//AddComponent<IDocumentDao, DocumentDao>();
            Register(Component.For(typeof(IRoleDao)).
                     ImplementedBy(typeof(RoleDao)).
                     LifeStyle.Is(type));
            //AddComponent<IRoleDao, RoleDao>();
            Register(Component.For(typeof(IUserLoginDao)).
                     ImplementedBy(typeof(UserLoginDao)).
                     LifeStyle.Is(type));
            //AddComponent<IUserLoginDao, UserLoginDao>();
            Register(Component.For(typeof(IEmployeeDocumentTypeDao)).
                     ImplementedBy(typeof(EmployeeDocumentTypeDao)).
                     LifeStyle.Is(type));
            //AddComponent<IEmployeeDocumentTypeDao, EmployeeDocumentTypeDao>();
            Register(Component.For(typeof(IEmployeeDocumentSubTypeDao)).
                    ImplementedBy(typeof(EmployeeDocumentSubTypeDao)).
                    LifeStyle.Is(type));
            //AddComponent<IEmployeeDocumentSubTypeDao, EmployeeDocumentSubTypeDao>();
            Register(Component.For(typeof(IAttachmentDao)).
                    ImplementedBy(typeof(AttachmentDao)).
                    LifeStyle.Is(type));
            //AddComponent<IAttachmentDao, AttachmentDao>();
            Register(Component.For(typeof(ISettingsDao)).
                    ImplementedBy(typeof(SettingsDao)).
                    LifeStyle.Is(type));
            //AddComponent<ISettingsDao, SettingsDao>();
            Register(Component.For(typeof(IExportImportActionDao)).
                    ImplementedBy(typeof(ExportImportActionDao)).
                    LifeStyle.Is(type));
            //AddComponent<IExportImportActionDao, ExportImportActionDao>();
            Register(Component.For(typeof(IInformationDao)).
                    ImplementedBy(typeof(InformationDao)).
                    LifeStyle.Is(type));
            //AddComponent<IInformationDao, InformationDao>();
            Register(Component.For(typeof(IDocumentCommentDao)).
                    ImplementedBy(typeof(DocumentCommentDao)).
                    LifeStyle.Is(type));
            //AddComponent<IDocumentCommentDao, DocumentCommentDao>();
            Register(Component.For(typeof(IDBVersionDao)).
                    ImplementedBy(typeof(DBVersionDao)).
                    LifeStyle.Is(type));

            Register(Component.For(typeof(IDepartmentDao)).
                    ImplementedBy(typeof(DepartmentDao)).
                    LifeStyle.Is(type));

            Register(Component.For(typeof(IVacationTypeDao)).
                  ImplementedBy(typeof(VacationTypeDao)).
                  LifeStyle.Is(type));

            Register(Component.For(typeof(IAdditionalVacationTypeDao)).
                  ImplementedBy(typeof(AdditionalVacationTypeDao)).
                  LifeStyle.Is(type));

            Register(Component.For(typeof(IRequestStatusDao)).
                  ImplementedBy(typeof(RequestStatusDao)).
                  LifeStyle.Is(type));

            Register(Component.For(typeof(IPositionDao)).
                  ImplementedBy(typeof(PositionDao)).
                  LifeStyle.Is(type));

            Register(Component.For(typeof(IVacationDao)).
                  ImplementedBy(typeof(VacationDao)).
                  LifeStyle.Is(type));

            /*Register(Component.For(typeof(IUserToDepartmentDao)).
                  ImplementedBy(typeof(UserToDepartmentDao)).
                  LifeStyle.Is(type));*/

            Register(Component.For(typeof(IRequestNextNumberDao)).
                  ImplementedBy(typeof(RequestNextNumberDao)).
                  LifeStyle.Is(type));

            Register(Component.For(typeof(IVacationCommentDao)).
                 ImplementedBy(typeof(VacationCommentDao)).
                 LifeStyle.Is(type));

            Register(Component.For(typeof(IAbsenceTypeDao)).
                 ImplementedBy(typeof(AbsenceTypeDao)).
                 LifeStyle.Is(type));

            Register(Component.For(typeof(IAbsenceDao)).
                 ImplementedBy(typeof(AbsenceDao)).
                 LifeStyle.Is(type));

            Register(Component.For(typeof(IAbsenceCommentDao)).
                 ImplementedBy(typeof(AbsenceCommentDao)).
                 LifeStyle.Is(type));

            Register(Component.For(typeof (ISicklistTypeDao)).
                         ImplementedBy(typeof (SicklistTypeDao)).
                         LifeStyle.Is(type));
            Register(Component.For(typeof (ISicklistPaymentRestrictTypeDao)).
                         ImplementedBy(typeof (SicklistPaymentRestrictTypeDao)).
                         LifeStyle.Is(type));
            Register(Component.For(typeof (ISicklistPaymentPercentDao)).
                         ImplementedBy(typeof (SicklistPaymentPercentDao)).
                         LifeStyle.Is(type));
            Register(Component.For(typeof (ISicklistDao)).
                         ImplementedBy(typeof (SicklistDao)).
                         LifeStyle.Is(type));
            Register(Component.For(typeof (IRequestAttachmentDao)).
                         ImplementedBy(typeof (RequestAttachmentDao)).
                         LifeStyle.Is(type));
            Register(Component.For(typeof (ISicklistCommentDao)).
                         ImplementedBy(typeof (SicklistCommentDao)).
                         LifeStyle.Is(type));
            Register(Component.For(typeof (IHolidayWorkTypeDao)).
                         ImplementedBy(typeof (HolidayWorkTypeDao)).
                         LifeStyle.Is(type));
            Register(Component.For(typeof (IHolidayWorkDao)).
                         ImplementedBy(typeof (HolidayWorkDao)).
                         LifeStyle.Is(type));
            Register(Component.For(typeof (IHolidayWorkCommentDao)).
                         ImplementedBy(typeof (HolidayWorkCommentDao)).
                         LifeStyle.Is(type));
            Register(Component.For(typeof (IMissionTypeDao)).
                         ImplementedBy(typeof (MissionTypeDao)).
                         LifeStyle.Is(type));
            Register(Component.For(typeof (IMissionDao)).
                         ImplementedBy(typeof (MissionDao)).
                         LifeStyle.Is(type));
            Register(Component.For(typeof (IMissionCommentDao)).
                         ImplementedBy(typeof (MissionCommentDao)).
                         LifeStyle.Is(type));
            Register(Component.For(typeof(IDismissalTypeDao)).
                         ImplementedBy(typeof(DismissalTypeDao)).
                         LifeStyle.Is(type));
            Register(Component.For(typeof(IDismissalDao)).
                        ImplementedBy(typeof(DismissalDao)).
                        LifeStyle.Is(type));
            Register(Component.For(typeof(IDismissalCommentDao)).
                         ImplementedBy(typeof(DismissalCommentDao)).
                         LifeStyle.Is(type));
            Register(Component.For(typeof(IClearanceChecklistCommentDao)).
                         ImplementedBy(typeof(ClearanceChecklistCommentDao)).
                         LifeStyle.Is(type));
            Register(Component.For(typeof(IClearanceChecklistDao)).
                        ImplementedBy(typeof(ClearanceChecklistDao)).
                        LifeStyle.Is(type));
            Register(Component.For(typeof(ITimesheetCorrectionTypeDao)).
                         ImplementedBy(typeof(TimesheetCorrectionTypeDao)).
                         LifeStyle.Is(type));
            Register(Component.For(typeof(ITimesheetCorrectionDao)).
                         ImplementedBy(typeof(TimesheetCorrectionDao)).
                         LifeStyle.Is(type));
            Register(Component.For(typeof(ITimesheetCorrectionCommentDao)).
                        ImplementedBy(typeof(TimesheetCorrectionCommentDao)).
                        LifeStyle.Is(type));
            Register(Component.For(typeof(IEmploymentTypeDao)).
                        ImplementedBy(typeof(EmploymentTypeDao)).
                        LifeStyle.Is(type));
            Register(Component.For(typeof(IEmploymentHoursTypeDao)).
                        ImplementedBy(typeof(EmploymentHoursTypeDao)).
                        LifeStyle.Is(type));
            Register(Component.For(typeof(IEmploymentDao)).
                       ImplementedBy(typeof(EmploymentDao)).
                       LifeStyle.Is(type));
            Register(Component.For(typeof(IEmploymentCommentDao)).
                     ImplementedBy(typeof(EmploymentCommentDao)).
                     LifeStyle.Is(type));
            Register(Component.For(typeof(IEmploymentAdditionDao)).
                     ImplementedBy(typeof(EmploymentAdditionDao)).
                     LifeStyle.Is(type));
            Register(Component.For(typeof(IEmploymentEducationTypeDao)).
                     ImplementedBy(typeof(EmploymentEducationTypeDao)).
                     LifeStyle.Is(type));
            Register(Component.For(typeof(IEmploymentHigherEducationDiplomaDao)).
                     ImplementedBy(typeof(EmploymentHigherEducationDiplomaDao)).
                     LifeStyle.Is(type));
            Register(Component.For(typeof(IEmploymentCandidateCommentDao)).
                     ImplementedBy(typeof(EmploymentCandidateCommentDao)).
                     LifeStyle.Is(type));
            
            Register(Component.For(typeof(IRequestPrintFormDao)).
                     ImplementedBy(typeof(RequestPrintFormDao)).
                     LifeStyle.Is(type));
			//AddComponent<IDBVersionDao, DBVersionDao>();

            Register(Component.For(typeof(ISicklistBabyMindingTypeDao)).
                     ImplementedBy(typeof(SicklistBabyMindingTypeDao)).
                     LifeStyle.Is(type));
            Register(Component.For(typeof(IInspectorToUserDao)).
                    ImplementedBy(typeof(InspectorToUserDao)).
                    LifeStyle.Is(type));

            Register(Component.For(typeof(IChiefToUserDao)).
                ImplementedBy(typeof(ChiefToUserDao)).
                LifeStyle.Is(type));

            Register(Component.For(typeof(IWorkingGraphicDao)).
               ImplementedBy(typeof(WorkingGraphicDao)).
               LifeStyle.Is(type));
            Register(Component.For(typeof(IWorkingDaysConstantDao)).
                ImplementedBy(typeof(WorkingDaysConstantDao)).
                LifeStyle.Is(type));

            Register(Component.For(typeof(IWorkingGraphicTypeDao)).
              ImplementedBy(typeof(WorkingGraphicTypeDao)).
              LifeStyle.Is(type));

            Register(Component.For(typeof(IWorkingCalendarDao)).
              ImplementedBy(typeof(WorkingCalendarDao)).
              LifeStyle.Is(type));

            Register(Component.For(typeof(IChildVacationDao)).
                  ImplementedBy(typeof(ChildVacationDao)).
                  LifeStyle.Is(type));

            Register(Component.For(typeof(IChildVacationCommentDao)).
                 ImplementedBy(typeof(ChildVacationCommentDao)).
                 LifeStyle.Is(type));

            Register(Component.For(typeof(IDeductionTypeDao)).
                       ImplementedBy(typeof(DeductionTypeDao)).
                       LifeStyle.Is(type));

            Register(Component.For(typeof(IDeductionKindDao)).
                      ImplementedBy(typeof(DeductionKindDao)).
                      LifeStyle.Is(type));

            Register(Component.For(typeof(IDeductionDao)).
                      ImplementedBy(typeof(DeductionDao)).
                      LifeStyle.Is(type));

            Register(Component.For(typeof(ITerraPointDao)).
                      ImplementedBy(typeof(TerraPointDao)).
                      LifeStyle.Is(type));
            Register(Component.For(typeof(ITerraPointToUserDao)).
                      ImplementedBy(typeof(TerraPointToUserDao)).
                      LifeStyle.Is(type));
            Register(Component.For(typeof(ITerraGraphicDao)).
                    ImplementedBy(typeof(TerraGraphicDao)).
                    LifeStyle.Is(type));

            Register(Component.For(typeof(IMissionGoalDao)).
                  ImplementedBy(typeof(MissionGoalDao)).
                  LifeStyle.Is(type));




            Register(Component.For(typeof(IMissionAirTicketTypeDao)).
               ImplementedBy(typeof(MissionAirTicketTypeDao)).
               LifeStyle.Is(type));
            Register(Component.For(typeof(IMissionCountryDao)).
               ImplementedBy(typeof(MissionCountryDao)).
               LifeStyle.Is(type));
            Register(Component.For(typeof(IMissionDailyAllowanceDao)).
               ImplementedBy(typeof(MissionDailyAllowanceDao)).
               LifeStyle.Is(type));
            Register(Component.For(typeof(IMissionResidenceDao)).
              ImplementedBy(typeof(MissionResidenceDao)).
              LifeStyle.Is(type));
            Register(Component.For(typeof(IMissionTrainTicketTypeDao)).
              ImplementedBy(typeof(MissionTrainTicketTypeDao)).
              LifeStyle.Is(type));
            Register(Component.For(typeof(IMissionGraidDao)).
             ImplementedBy(typeof(MissionGraidDao)).
             LifeStyle.Is(type));

            Register(Component.For(typeof(IMissionOrderDao)).
             ImplementedBy(typeof(MissionOrderDao)).
             LifeStyle.Is(type));
            Register(Component.For(typeof(IMissionOrderCommentDao)).
             ImplementedBy(typeof(MissionOrderCommentDao)).
             LifeStyle.Is(type));
            Register(Component.For(typeof(IMissionTargetDao)).
             ImplementedBy(typeof(MissionTargetDao)).
             LifeStyle.Is(type));

            Register(Component.For(typeof(IMissionReportDao)).
                ImplementedBy(typeof(MissionReportDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IMissionReportCostTypeDao)).
               ImplementedBy(typeof(MissionReportCostTypeDao)).
               LifeStyle.Is(type));
            Register(Component.For(typeof(IMissionReportCostDao)).
              ImplementedBy(typeof(MissionReportCostDao)).
              LifeStyle.Is(type));
            Register(Component.For(typeof(IMissionReportCommentDao)).
                ImplementedBy(typeof(MissionReportCommentDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IAccountDao)).
                ImplementedBy(typeof(AccountDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IContractorDao)).
               ImplementedBy(typeof(ContractorDao)).
               LifeStyle.Is(type));
            Register(Component.For(typeof(IMissionPurchaseBookDocumentDao)).
                ImplementedBy(typeof(MissionPurchaseBookDocumentDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IMissionPurchaseBookRecordDao)).
             ImplementedBy(typeof(MissionPurchaseBookRecordDao)).
             LifeStyle.Is(type));

            Register(Component.For(typeof(IAppointmentCommentDao)).
                ImplementedBy(typeof(AppointmentCommentDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IAppointmentDao)).
                ImplementedBy(typeof(AppointmentDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IAppointmentReasonDao)).
                ImplementedBy(typeof(AppointmentReasonDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IAppointmentReportDao)).
               ImplementedBy(typeof(AppointmentReportDao)).
               LifeStyle.Is(type));
            Register(Component.For(typeof(IAppointmentEducationTypeDao)).
               ImplementedBy(typeof(AppointmentEducationTypeDao)).
               LifeStyle.Is(type));
            Register(Component.For(typeof(IAppointmentReportCommentDao)).
              ImplementedBy(typeof(AppointmentReportCommentDao)).
              LifeStyle.Is(type));

            Register(Component.For(typeof(IEmploymentCandidateDao)).
                ImplementedBy(typeof(EmploymentCandidateDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IEmploymentCommonDao)).
                ImplementedBy(typeof(EmploymentCommonDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IEmploymentGeneralInfoDao)).
                ImplementedBy(typeof(EmploymentGeneralInfoDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IInsuredPersonTypeDao)).
                ImplementedBy(typeof(InsuredPersonTypeDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IEmploymentPassportDao)).
                ImplementedBy(typeof(EmploymentPassportDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IDocumentTypeDao)).
                ImplementedBy(typeof(DocumentTypeDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IEmploymentEducationDao)).
                ImplementedBy(typeof(EmploymentEducationDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IEmploymentFamilyDao)).
                ImplementedBy(typeof(EmploymentFamilyDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IEmploymentMilitaryServiceDao)).
                ImplementedBy(typeof(EmploymentMilitaryServiceDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IEmploymentExperienceDao)).
                ImplementedBy(typeof(EmploymentExperienceDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IEmploymentContactsDao)).
                ImplementedBy(typeof(EmploymentContactsDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IEmploymentBackgroundCheckDao)).
                ImplementedBy(typeof(EmploymentBackgroundCheckDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IEmploymentOnsiteTrainingDao)).
                ImplementedBy(typeof(EmploymentOnsiteTrainingDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IEmploymentManagersDao)).
                ImplementedBy(typeof(EmploymentManagersDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IEmploymentPersonnelManagersDao)).
                ImplementedBy(typeof(EmploymentPersonnelManagersDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IEmploymentCandidateDocNeededDao)).
                ImplementedBy(typeof(EmploymentCandidateDocNeededDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(ICountryDao)).
                ImplementedBy(typeof(CountryDao)).
                LifeStyle.Is(type));
            Register(Component.For(typeof(IAccessGroupDao)).
                  ImplementedBy(typeof(AccessGroupDao)).
                  LifeStyle.Is(type));
            Register(Component.For(typeof(IDisabilityDegreeDao)).
                  ImplementedBy(typeof(DisabilityDegreeDao)).
                  LifeStyle.Is(type));
            Register(Component.For(typeof(IPersonalAccountContractorDao)).
                  ImplementedBy(typeof(PersonalAccountContractorDao)).
                  LifeStyle.Is(type));
            Register(Component.For(typeof(IScheduleDao)).
                  ImplementedBy(typeof(ScheduleDao)).
                  LifeStyle.Is(type));
            Register(Component.For(typeof(IManualRoleRecordDao)).
                  ImplementedBy(typeof(ManualRoleRecordDao)).
                  LifeStyle.Is(type));

            Register(Component.For(typeof(IMissionHotelsDao)).
             ImplementedBy(typeof(MissionHotelsDao)).
             LifeStyle.Is(type));

            Register(Component.For(typeof(IGpdRefDetailDao)).
             ImplementedBy(typeof(GpdRefDetailDao)).
             LifeStyle.Is(type));
            Register(Component.For(typeof(IGpdDetailSetDao)).
             ImplementedBy(typeof(GpdDetailSetDao)).
             LifeStyle.Is(type));
            Register(Component.For(typeof(IGpdContractDao)).
             ImplementedBy(typeof(GpdContractDao)).
             LifeStyle.Is(type));
            Register(Component.For(typeof(IGpdActDao)).
             ImplementedBy(typeof(GpdActDao)).
             LifeStyle.Is(type));


            Register(Component.For(typeof (IHelpVersionDao)).
                         ImplementedBy(typeof (HelpVersionDao)).
                         LifeStyle.Is(type));
            Register(Component.For(typeof(IHelpFaqDao)).
                       ImplementedBy(typeof(HelpFaqDao)).
                       LifeStyle.Is(type));
            Register(Component.For(typeof(IHelpServicePeriodDao)).
                       ImplementedBy(typeof(HelpServicePeriodDao)).
                       LifeStyle.Is(type));
            Register(Component.For(typeof(IHelpServiceProductionTimeDao)).
                      ImplementedBy(typeof(HelpServiceProductionTimeDao)).
                      LifeStyle.Is(type));
            Register(Component.For(typeof(IHelpServiceTransferMethodDao)).
                      ImplementedBy(typeof(HelpServiceTransferMethodDao)).
                      LifeStyle.Is(type));
            Register(Component.For(typeof(IHelpServiceTypeDao)).
                      ImplementedBy(typeof(HelpServiceTypeDao)).
                      LifeStyle.Is(type));
            Register(Component.For(typeof(IHelpServiceRequestDao)).
                     ImplementedBy(typeof(HelpServiceRequestDao)).
                     LifeStyle.Is(type));
            Register(Component.For(typeof(IHelpServiceRequestCommentDao)).
                    ImplementedBy(typeof(HelpServiceRequestCommentDao)).
                    LifeStyle.Is(type));
            Register(Component.For(typeof(IHelpQuestionRequestDao)).
                  ImplementedBy(typeof(HelpQuestionRequestDao)).
                  LifeStyle.Is(type));
            Register(Component.For(typeof(IHelpQuestionTypeDao)).
                 ImplementedBy(typeof(HelpQuestionTypeDao)).
                 LifeStyle.Is(type));
            Register(Component.For(typeof(IHelpQuestionSubtypeDao)).
                 ImplementedBy(typeof(HelpQuestionSubtypeDao)).
                 LifeStyle.Is(type));
            Register(Component.For<IEmploymentSignersDao>()
                .ImplementedBy<EmploymentSignersDao>()
                .LifeStyle.Is(type));
            
            Register(Component.For(typeof(ISessionFactory))
                    .Named("ISessionFactory")
                    .Instance(SessionFactory)
                    .LifeStyle.Is(LifestyleType.Singleton));
			//Kernel.AddComponentInstance("ISessionFactory", typeof(ISessionFactory), SessionFactory);
            Register(Component.For(typeof(INoteTypeDao))
                .ImplementedBy<NoteTypeDao>()
                .LifeStyle.Is(type));
            Register(Component.For(typeof(IAnalyticalStatementDao))
                .ImplementedBy<AnalyticalStatementDao>()
                .LifeStyle.Is(type));
            Register(Component.For(typeof(ISurchargeDao))
                .ImplementedBy<SurchargeDao>()
                .LifeStyle.Is(type));
            Register(Component.For(typeof(IDeductionImportDao))
                .ImplementedBy<DeductionImportDao>()
                .LifeStyle.Is(type));
            Register(Component.For(typeof(IGpdChargingTypeDao))
                .ImplementedBy<GpdChargingTypeDao>()
                .LifeStyle.Is(type));
        }
        #endregion

    }
}
