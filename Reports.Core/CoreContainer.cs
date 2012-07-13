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

            Register(Component.For(typeof(IRequestStatusDao)).
                  ImplementedBy(typeof(RequestStatusDao)).
                  LifeStyle.Is(type));

            Register(Component.For(typeof(IPositionDao)).
                  ImplementedBy(typeof(PositionDao)).
                  LifeStyle.Is(type));

            Register(Component.For(typeof(IVacationDao)).
                  ImplementedBy(typeof(VacationDao)).
                  LifeStyle.Is(type));

            Register(Component.For(typeof(IUserToDepartmentDao)).
                  ImplementedBy(typeof(UserToDepartmentDao)).
                  LifeStyle.Is(type));

            Register(Component.For(typeof(IRequestNextNumberDao)).
                  ImplementedBy(typeof(RequestNextNumberDao)).
                  LifeStyle.Is(type));

            Register(Component.For(typeof(IVacationCommentDao)).
                 ImplementedBy(typeof(VacationCommentDao)).
                 LifeStyle.Is(type));
			//AddComponent<IDBVersionDao, DBVersionDao>();

            Register(Component.For(typeof(ISessionFactory))
                    .Named("ISessionFactory")
                    .Instance(SessionFactory)
                    .LifeStyle.Is(LifestyleType.Singleton));
			//Kernel.AddComponentInstance("ISessionFactory", typeof(ISessionFactory), SessionFactory);
        }
        #endregion

    }
}
