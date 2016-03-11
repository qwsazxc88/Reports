using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    class MenuDao : DefaultDao<Menu>, IMenuDao
    {
        public MenuDao(ISessionManager sessionManager) : base(sessionManager) { }

        /*protected System.Collections.Generic.IList<MenuDto TODO: MenuDto > GetMenuForRole(int roleId)
        {      SELECT Menu.Name, Menu.LinkController, Menu.LinkAction, Menu.Parent 
                FORM MenuForRole 
                JOIN Menu ON MenuForRole.MenuId=Menu.id 
                WHERE (RoleId=1 AND Menu.IsVisible=1 AND MenuForRole.NotAllow=0);
            
            string sqlQuery = @"SELECT Menu.Name, Menu.LinkController, Menu.LinkAction, Menu.Parent FORM MenuForRole JOIN Menu ON MenuForRole.MenuId=Menu.id ";
            IQuery query = Session.CreateSQLQuery(sqlQuery).AddScalar(AAAAAAAAAA); //TODO: FIXME //WHERE (RoleId=1 AND Menu.IsVisible=1 AND MenuForRole.NotAllow=0);"
            IList<MenuDto> menuList = query.SetResultTransformer(Transformers.AliasToBean(typeof(MenuDto))).List<MenuDto>();
            return menuList;
        } */

        IList<MenuDto> GetMenuForRole(int userRole)
        {
            string sqlString = @"SELECT 
            Menu.Name, Menu.LinkController, Menu.LinkAction, Menu.Parent 
            FORM MenuForRole 
            JOIN Menu ON MenuForRole.MenuId=Menu.id";
            /* var query = Session.CreateSQLQuery(sqlString);
            //query.AddScalar();
           // var tr = Session.BeginTransaction();
           // var dto = Session.Get<MenuDto>();
           using (ITransaction transaction = session.BeginTransaction())
            {
                
            }
            var data = query.SetResultTransformer(Transformers.AliasToBean(typeof(MenuDto))).List<MenuDto>();*/
            /*SELECT Menu.Name, Menu.LinkController, Menu.LinkAction, Menu.Parent 
                FROM MenuForRole 
                JOIN Menu ON MenuForRole.MenuId=Menu.id 
                WHERE (RoleId=1 AND Menu.IsVisible=1 AND MenuForRole.NotAllow=0);
             -----------------------------------------------------------------------
             * SELECT Menu.Name, Menu.LinkController, Menu.LinkAction, Menu.Parent 
                FROM Menu JOIN MenuForRole ON Menu.id=MenuForRole.MenuId 
                WHERE (RoleId=2 AND Menu.IsVisible=1 AND MenuForRole.NotAllow=0)
             */
            IList < MenuDto > menuList = 
                Session.QueryOver<MenuDto>()
                .Select(x => x.Name,
                        x => x.LinkController,
                        x => x.LinkAction,
                        x => x.Parent)
                .JoinQueryOver( a => a.Menuforrole)
                .Where( a => a.Role == userRole )
                .List();
            return menuList;
        }

        bool AddMenuForRole(int userRole, int menuId)
        {
            Session.Save(placeholder);
            transaction.Commit();
            return true;
        }
        bool RemoveMenuForRole(int userRole, int menuId);
    }
}
