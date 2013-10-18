using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Reports.Presenters.UI;

namespace WebMvc
{
    public class CustomModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(
            IEnumerable<Attribute> attributes,
            Type containerType,
            Func<object> modelAccessor,
            Type modelType,
            string propertyName)
        {
            var modelMetadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

            //attributes.OfType<DataTypeAttribute>().ToList().ForEach(m => { if (m.DataType.Equals(DataType.Date) || (m.DataType.Equals(DataType.Custom) && m.CustomDataType == "DateTimeDto")) modelMetadata.TemplateHint = "DatePicker"; });
            attributes.OfType<DataTypeAttribute>().ToList().ForEach(m =>
            {
                if (m.DataType.Equals(DataType.Custom) &&
                 (m.CustomDataType == "TimesheetDto"))
                    modelMetadata.TemplateHint = "TimesheetEditor";
            });
            attributes.OfType<DataTypeAttribute>().ToList().ForEach(m =>
            {
                if (m.DataType.Equals(DataType.Custom) &&
                 (m.CustomDataType == "TimesheetDtoList"))
                    modelMetadata.TemplateHint = "TimesheetList";
                if (m.DataType.Equals(DataType.Custom) &&
                (m.CustomDataType == "TerraGraphicDtoList"))
                    modelMetadata.TemplateHint = "TerraGraphicsList";
            });
            //attributes.OfType<DataTypeAttribute>().ToList().ForEach(m =>
            //{
            //    if (m.DataType.Equals(DataType.Custom) && (m.CustomDataType == "DropdownDto"))
            //        modelMetadata.TemplateHint = "Dropdown";
            //});
            attributes.OfType<MetadataAttribute>().ToList().ForEach(x => x.Process(modelMetadata));

            return modelMetadata;
        }
    }
}