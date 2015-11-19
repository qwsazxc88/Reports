﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using Reports.Core.Domain;
using Reports.Core.Dao;
using Reports.Core.Utils;
namespace Reports.Core
{
    public static class QueryCreator
    {
        public static Expression<Func<TDomain, bool>> Create<TDomain, TSearchModel>(TSearchModel searchmodel,User currentuser)
        {
            //Expression для модели поиска
            ConstantExpression search = ConstantExpression.Constant(searchmodel);
            //Expression для домена
            ParameterExpression domain = Expression.Parameter(typeof(TDomain), "domain");
            //Получаем Expression для прав пользователя
            Expression rights = Expression.Constant(true);
            var smattrib = searchmodel.GetType().GetCustomAttributes(typeof(SearchModelAttribute), false);
            smattrib.NotNullAndAny()
                .OnSuccess(x => 
                        rights = GetUserRights(currentuser, domain, ((SearchModelAttribute)smattrib.First()).RightsProperty))
                .OnError(x=>
                        rights = GetUserRights(currentuser, domain));
            
            //Получаем все свойства модели поиска
            var props = typeof(TSearchModel).GetProperties();
            //Expression результата, по умолчанию true
            Expression result = Expression.Constant(true);
            //Проходим по всем свойствам 
            foreach (var prop in props)
            {
                //Если  значение в модели = null, тогда игнорируем его
                if (prop.GetValue(searchmodel, null) == null) continue;
                //Получаем все атрибуты свойства
                var attrs=prop.GetCustomAttributes(typeof(SearchFieldAttribute), false);
                //Для каждого атрибута изменяем результат
                foreach (SearchFieldAttribute attr in attrs)
                {
                    //Если значение поля = игнорируемому значению, то пропускаем этот атрибут
                    if (prop.GetValue(searchmodel, null).Equals(attr.IgnoreValue)) continue;
                    //получаем Property домена из заданого значения в атрибуте                   
                    var domainprop = domain.GetProperty(attr.ModelParam);
                    //получаем Property для модели поиска
                    Expression searchprop = Expression.Property(search, prop);
                    if (attr.IsNullable)
                        searchprop = searchprop.GetProperty("Value");
                    switch (attr.Comparer)
                    {
                        case ComparerEnum.Equals:
                            result = Expression.And(result, Expression.Equal(domainprop, searchprop));
                            break;
                        case ComparerEnum.EqualsOrGreatar:
                            result = Expression.And(result, Expression.GreaterThanOrEqual(domainprop, searchprop));
                            break;
                        case ComparerEnum.EqualsOrLess:
                            result = Expression.And(result, Expression.LessThanOrEqual(domainprop, searchprop));
                            break;
                        case ComparerEnum.GreaterThan:
                            result = Expression.And(result, Expression.GreaterThan(domainprop, searchprop));
                            break;
                        case ComparerEnum.LessThan:
                            result = Expression.And(result, Expression.LessThan(domainprop, searchprop));
                            break;
                        case ComparerEnum.Like:
                            result = Expression.And(result, Expression.Call(domainprop, typeof(string).GetMethod("Contains"),searchprop));
                            break;
                        case ComparerEnum.NotEquals:
                            result = Expression.And(result, Expression.NotEqual(domainprop, searchprop));
                            break;
                        case ComparerEnum.Department:
                            var dep = Ioc.Resolve<IDepartmentDao>().Load((int)prop.GetValue(searchmodel, null));
                            var departmentconstant = Expression.Constant(dep.Path);                
                            var contains = Expression.Call(domainprop.GetProperty("Path"), typeof(string).GetMethod("Contains"), departmentconstant);
                            result = Expression.And(result, contains);
                            break;
                    }
                }                
            }
            //Добавляем права
            result = Expression.And(result, rights);
            //создаём результат
            return Expression.Lambda<Func<TDomain, bool>>(result, domain);
        }
        private static Expression GetUserRights(User user, Expression domain, string departmentContainer="User.Department")
        {
            var userdepprop = domain.GetProperty(departmentContainer+".Path");
            var useridprop = domain.GetProperty("User.Id");
            Expression result = Expression.Constant(false);
            switch (user.UserRole)
            {
                case UserRole.Manager:
                    List<Department> deps = Ioc.Resolve<IManualRoleRecordDao>().QueryExpression(x => x.User.Id == user.Id && x.TargetDepartment != null).Select(x => x.TargetDepartment).ToList();
                    if (deps == null) deps = new List<Department>();
                    deps.Add(user.Department);
                    foreach (var d in deps)
                    {
                        var departmentconstant = Expression.Constant(d.Path);
                        result=Expression.Or(result,Expression.Call(userdepprop, typeof(string).GetMethod("Contains"), departmentconstant));
                    }
                    break;
                case UserRole.ConsultantOutsourcing:
                case UserRole.OutsourcingManager:
                    result = Expression.Constant(true);
                    break;
                case UserRole.Employee:
                    result = Expression.Equal(useridprop, Expression.Constant(user.Id));
                    break;
                case UserRole.PersonnelManager:
                    var users = Ioc.Resolve<IUserDao>().GetUsersForPersonnel(user.Id).Select(x=>x.Id).ToList();
                    result = Expression.Or(result, Expression.Call(Expression.Constant(users), typeof(List<int>).GetMethod("Contains"),useridprop));                   
                    break;
            }
            return result;
        }
        public static Expression GetProperty(this Expression expression, string property)
        {
            string[] props = property.Split('.');
            Expression result = Expression.Property(expression, props[0]);
            for (int i = 1; i < props.Length; i++)
                result = Expression.Property(result, props[i]);
            return result;
        }
    }
}
