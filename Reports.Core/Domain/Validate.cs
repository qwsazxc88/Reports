using System;
using Reports.Core.Domain;

namespace Reports.Core
{
    
    /// <summary>
    /// Provides helper methods for arguments validation
    /// </summary>
    public static class Validate
    {
        public static T Dependency<T>(T dependencyPar)
        {
            if (dependencyPar == null)
                throw new InvalidOperationException(StringHelper.Format(Resources.Validate_DependencyNotSet, typeof(T).AssemblyQualifiedName));
            return dependencyPar;
        }

        public static void NotNull(object param, string paramName)
        {
            if (param == null)
                throw new ArgumentNullException(paramName);
        }

        public static void IdIsAssigned (IEntity <int> entity, string paramName)
        {
            Validate.NotNull(entity, paramName);
            if (entity.Id == 0)
                throw new ArgumentException(string.Format(Resources.Culture,
                    Resources.Validate_IdIsNotAssigned, paramName, entity.GetType().FullName));
        }
    }
}