// Copyright (C) 2007-2008 CorePartners. All rights reserved.
//

using System;
using System.Configuration;
using Reports.Core;
using Reports.Core.Services;
using Reports.Core.UI;

namespace Reports.Presenters.Services
{
    /// <summary>
    /// Default <see cref="IActionResolver"/> implementation.
    /// </summary>
    public class DefaultActionResolver : IActionResolver
    {
        #region Fields

        private IConfigurationService _configurationService;

        #endregion

        #region Properties

        public IConfigurationService ConfigurationService
        {
            set { _configurationService = value; }
            get { return _configurationService; }
        }

        #endregion

        #region Helpers

        private static string MakeAbsoluteUrl(string url)
        {
            if (!url.Contains(":") &&
                !url.StartsWith("/") &&
                !url.StartsWith("\\") &&
                !url.StartsWith("~"))
                url = "~/" + url;

            return url;
        }

        #endregion

        #region IActionResolver implementation

        public virtual string ResolveAction(string actionName)
        {
            Validate.NotNull(actionName, "actionName");

            ActionsMappingSection section =
                _configurationService.GetSection<ActionsMappingSection>(ActionsMappingSection.SectionName);

            if (section == null)
                throw new InvalidOperationException(
                    string.Format(Resources.Culture, Resources.Section_NotFound /*"Секция {0} не найдена"*/, ActionsMappingSection.SectionName));

            KeyValueConfigurationElement element = section.Mappings[actionName];
            if (element != null)
            {
                string url = element.Value;

                if (!string.IsNullOrEmpty(url))
                {
                    return MakeAbsoluteUrl(url);
                }
            }

            throw new InvalidOperationException(
                string.Format(Resources.Culture, Resources.DefaultActionResolver_ActionIsNotMapped /*"Активность {0} не описана."*/, actionName));
        }

        #endregion
    }
}