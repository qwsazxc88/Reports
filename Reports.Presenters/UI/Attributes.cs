using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Routing;

namespace Reports.Presenters.UI
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidatePasswordLengthAttribute : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "Поле '{0}' должно быть не менее {1} символов.";
        private const int MinCharacters = 8; //Membership.Provider.MinRequiredPasswordLength;

        public ValidatePasswordLengthAttribute()
            : base(DefaultErrorMessage)
        {
        }

        #region IClientValidatable Members

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata,
                                                                               ControllerContext context)
        {
            return new[]
                       {
                           new ModelClientValidationStringLengthRule(FormatErrorMessage(metadata.GetDisplayName()),
                                                                     MinCharacters, int.MaxValue)
                       };
        }

        #endregion

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString,
                                 name, MinCharacters);
        }

        public override bool IsValid(object value)
        {
            var valueAsString = value as string;
            return (valueAsString != null && valueAsString.Length >= MinCharacters);
        }
    }
    public class LocalizationDisplayNameAttribute : DisplayNameAttribute
    {
        protected readonly DisplayAttribute display;
        public LocalizationDisplayNameAttribute(string resourceName, Type resourceType)
        {
            display = new DisplayAttribute
            {
                ResourceType = resourceType,
                Name = resourceName
            };
        }
        public override string DisplayName
        {
           get  { return display.GetName(); }
        }
    }
    public class EmailAttribute : RegularExpressionAttribute
	{
	   public EmailAttribute()
	            : base(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}")
	        {
                ErrorMessage = "Неправильный адрес эл. почты";
	        }

	}
    public class RequiredTrueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            if (value.GetType() != typeof(bool)) throw new InvalidOperationException("Can only be used on bool props.");

            return (bool)value == true;
        }
    }
    public abstract class MetadataAttribute : Attribute
    {
        /// <summary>
        /// Method for processing custom attribute data.
        /// </summary>
        /// <param name="modelMetaData">A ModelMetaData instance.</param>
        public abstract void Process(ModelMetadata modelMetaData);
    }
    
    public class AutoCompleteAttribute : MetadataAttribute
    {
        public RouteValueDictionary RouteValueDictionary;

        public AutoCompleteAttribute(string controller, string action, string parameterName)
        {
            RouteValueDictionary = new RouteValueDictionary
                                            {
                                                {"Controller", controller},
                                                {"Action", action},
                                                {parameterName, string.Empty}
                                            };
        }

        public override void Process(ModelMetadata modelMetaData)
        {
            modelMetaData.AdditionalValues.Add("AutoCompleteUrlData", RouteValueDictionary);
            modelMetaData.TemplateHint = "AutoComplete";
        }
    }   
    
}