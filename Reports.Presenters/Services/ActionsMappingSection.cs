// Copyright (C) 2007-2008 CorePartners. All rights reserved.
//

using System.Configuration;

namespace Reports.Presenters.Services
{
    /// <summary>
    /// Action Mappings configuration section.
    /// </summary>
    /// <remarks>
    /// Requires "<see cref="ActionsMappingSection.SectionName"/>" section in configuration file.
    /// </remarks>
    public class ActionsMappingSection : ConfigurationSection
    {
        public const string SectionName = "actionMappings";

        #region Fields

        private static ConfigurationPropertyCollection _propertyCollection;
        private static ConfigurationProperty _mappingsProperty;

        #endregion

        #region Helpers

        private static void InitPropertyCollection()
        {
            if (_propertyCollection == null)
            {
                _mappingsProperty = new ConfigurationProperty(null, typeof(KeyValueConfigurationCollection), null,
                                                              ConfigurationPropertyOptions.IsDefaultCollection);

                ConfigurationPropertyCollection propertyCollection = new ConfigurationPropertyCollection();
                propertyCollection.Add(_mappingsProperty);

                _propertyCollection = propertyCollection;
            }
        }

        #endregion

        #region Overrides

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                InitPropertyCollection();
                return _propertyCollection;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Mappings collection
        /// </summary>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public KeyValueConfigurationCollection Mappings
        {
            get
            {
                InitPropertyCollection();
                return (KeyValueConfigurationCollection)base[_mappingsProperty];
            }
        }

        #endregion
    }
}