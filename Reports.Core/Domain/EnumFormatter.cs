// Copyright (C) 2007-2008 CorePartners. All rights reserved.
//

using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Provides methods for formatting enum values to user-friendly strings
    /// </summary>
    public sealed class EnumFormatter
    {
        private EnumFormatter()
        {
            
        }

        #region Helpers

        private static Exception NotFound(object value)
        {
            return new ArgumentException(string.Format(
                                             Resources.Culture,
                                             Resources.EnumFormatter_NotSupported, value));
        }

        #endregion

		#region Methods
		//public static string Format(CaseEditingStatus status)
		//{
		//    switch (status)
		//    {
		//        case CaseEditingStatus.NewCase:
		//            return Resources.CaseEditingStatus_NewCase;
		//        case CaseEditingStatus.ContentContributionByCaseEditor:
		//            return Resources.CaseEditingStatus_ContentContributionByCaseEditor;
		//        case CaseEditingStatus.SubmittedToSectionEditor:
		//            return Resources.CaseEditingStatus_SubmittedToSectionEditor;
		//        case CaseEditingStatus.ReviewBySectionEditor:
		//            return Resources.CaseEditingStatus_ReviewBySectionEditor;
		//        case CaseEditingStatus.SentBackToCaseEditor:
		//            return Resources.CaseEditingStatus_SentBackToCaseEditor;
		//        case CaseEditingStatus.SubmittedToAcr:
		//            return Resources.CaseEditingStatus_SubmittedToAcr;
		//        case CaseEditingStatus.ReviewByAcr:
		//            return Resources.CaseEditingStatus_ReviewByAcr;
		//        case CaseEditingStatus.SentBackToSectionEditor:
		//            return Resources.CaseEditingStatus_SentBackToSectionEditor;
		//        case CaseEditingStatus.SubmittedToChiefEditor:
		//            return Resources.CaseEditingStatus_SubmittedToChiefEditor;
		//        case CaseEditingStatus.ReviewByChiefEditor:
		//            return Resources.CaseEditingStatus_ReviewByChiefEditor;
		//        case CaseEditingStatus.SentBackToAcr:
		//            return Resources.CaseEditingStatus_SentBackToAcr;
		//        case CaseEditingStatus.ReadyToGo:
		//            return Resources.CaseEditingStatus_ReadyToGo;
		//        default: 
		//            throw (NotFound(status));
		//    }
		//}
		//public static string Format(CaseProductionStatus status)
		//{
		//    switch (status)
		//    {
		//        case CaseProductionStatus.Out:
		//            return Resources.CaseProductionStatus_Out;
		//        case CaseProductionStatus.In:
		//            return Resources.CaseProductionStatus_In;
		//        case CaseProductionStatus.Archive:
		//            return Resources.CaseProductionStatus_Archive;
		//        default:
		//            throw (NotFound(status));
		//    }
		//}
		//public static string Format(CaseSearchStatus status)
		//{
		//    switch (status)
		//    {
		//        case CaseSearchStatus.NonSearchable:
		//            return Resources.CaseSearchStatus_NonSearchable;
		//        case CaseSearchStatus.Searchable:
		//            return Resources.CaseSearchStatus_Searchable;
		//        default:
		//            throw (NotFound(status));
		//    }
		//}
		//public static string Format(CaseFlag flag)
		//{
		//    switch(flag)
		//    {
		//        case CaseFlag.Unset:
		//            return Resources.CaseFlag_Unset;
		//        case CaseFlag.Red:
		//            return Resources.CaseFlag_Red;
		//        case CaseFlag.Orange:
		//            return Resources.CaseFlag_Orange;
		//        case CaseFlag.Yellow:
		//            return Resources.CaseFlag_Yellow;
		//        case CaseFlag.Green:
		//            return Resources.CaseFlag_Green;
		//        case CaseFlag.Blue:
		//            return Resources.CaseFlag_Blue;
		//        case CaseFlag.Purple:
		//            return Resources.CaseFlag_Purple;
		//        default:
		//            throw (NotFound(flag));

		//    }
		//}
		//public static string Format(CaseCreditType type)
		//{
		//    switch (type)
		//    {
		//        case CaseCreditType.Author:
		//            return Resources.CaseCreditType_Author;
		//        case CaseCreditType.Reviewer:
		//            return Resources.CaseCreditType_Reviewer;
		//        default:
		//            throw (NotFound(type));
		//    }
		//}
		//public static string Format(CaseCreditStatusType type)
		//{
		//    switch (type)
		//    {
		//        case CaseCreditStatusType.Approved:
		//            return Resources.CaseCreditStatusType_Approved;
		//        case CaseCreditStatusType.Updated:
		//            return Resources.CaseCreditStatusType_Updated;
		//        default:
		//            throw (NotFound(type));
		//    }
		//}
		//public static string Format(CaseContentItemType item)
		//{
		//    switch (item)
		//    {
		//        case CaseContentItemType.BaseCaseTextElement:
		//            return Resources.CaseContentItemType_BaseCaseTextElement;
		//        case CaseContentItemType.CaseCredits:
		//            return Resources.CaseContentItemType_CaseCredits;
		//        case CaseContentItemType.Image:
		//            return Resources.CaseContentItemType_Image;
		//        //case CaseContentItemType.ArbitraryText:
		//        //    return Resources.CaseContentItemType_ArbitraryText;
		//        case CaseContentItemType.Question:
		//            return Resources.CaseContentItemType_Question;
		//        case CaseContentItemType.TemplatedItem:
		//            return Resources.CaseContentItemType_TemplatedItem;
		//        case CaseContentItemType.PageBreak:
		//            return Resources.CaseContentItemType_PageBreak;
		//        case CaseContentItemType.EmptyLine:
		//            return Resources.CaseContentItemType_EmptyLine;
		//        case CaseContentItemType.HorizontalLine:
		//            return Resources.CaseContentItemType_HorizontalLine;
		//        default:
		//            throw (NotFound(item));
		//    }
		//}
		//public static string Format(ContentElementType item)
		//{
		//    switch (item)
		//    {
		//        case ContentElementType.History:
		//            return Resources.ContentElementType_History;
		//        case ContentElementType.Findings:
		//            return Resources.ContentElementType_Findings;
		//        case ContentElementType.Diagnosis:
		//            return Resources.ContentElementType_Diagnosis;
		//        case ContentElementType.DiffDiagnosis:
		//            return Resources.ContentElementType_DifferentialDiagnosis;
		//        case ContentElementType.Discussion:
		//            return Resources.ContentElementType_Discussion;
		//        case ContentElementType.UniqueCaseDiscussion:
		//            return Resources.ContentElementType_UniqueCaseDiscussion;
		//        case ContentElementType.TreatmentFollowUp:
		//            return Resources.ContentElementType_Treatment;
		//        case ContentElementType.CasePoints:
		//            return Resources.ContentElementType_CasePoints;
		//        case ContentElementType.References:
		//            return Resources.ContentElementType_References;
		//        case ContentElementType.Patient:
		//            return Resources.ContentElementType_Patient;
		//        case ContentElementType.ArbitraryText:
		//            return Resources.ContentElementType_ArbitraryText;
		//        case ContentElementType.None:
		//            return string.Empty;
		//        default:
		//            throw (NotFound(item));
		//    }
		//}
		//public static string Format(CaseContentTextElementItemType item)
		//{
		//    switch (item)
		//    {
		//        case CaseContentTextElementItemType.History:
		//            return Resources.CaseContentTextElementItemType_History;
		//        case CaseContentTextElementItemType.Findings:
		//            return Resources.CaseContentTextElementItemType_Findings;
		//        case CaseContentTextElementItemType.Diagnosis:
		//            return Resources.CaseContentTextElementItemType_Diagnosis;
		//        case CaseContentTextElementItemType.DifferentialDiagnosis:
		//            return Resources.CaseContentTextElementItemType_DifferentialDiagnosis;
		//        case CaseContentTextElementItemType.Discussion:
		//            return Resources.CaseContentTextElementItemType_Discussion;
		//        case CaseContentTextElementItemType.UniqueCaseDiscussion:
		//            return Resources.CaseContentTextElementItemType_UniqueCaseDiscussion;
		//        case CaseContentTextElementItemType.Treatment:
		//            return Resources.CaseContentTextElementItemType_Treatment;
		//        case CaseContentTextElementItemType.CasePoints:
		//            return Resources.CaseContentTextElementItemType_CasePoints;
		//        case CaseContentTextElementItemType.References:
		//            return Resources.CaseContentTextElementItemType_References;
		//        case CaseContentTextElementItemType.Patient:
		//            return Resources.CaseContentTextElementItemType_Patient;
		//        case CaseContentTextElementItemType.ArbitraryText:
		//            return Resources.CaseContentTextElementItemType_ArbitraryText;
		//        default:
		//            throw (NotFound(item));
		//    }
		//}

		//public static string Format(CaseContentTemplatedItemType item)
		//{
		//    switch (item)
		//    {
		//        case CaseContentTemplatedItemType.SimpleImageView:
		//            return Resources.CaseContentTemplatedItemType_SimpleImageView;
		//        case CaseContentTemplatedItemType.StackView:
		//            return Resources.CaseContentTemplatedItemType_StackView;
		//        case CaseContentTemplatedItemType.FindingsView:
		//            return Resources.CaseContentTemplatedItemType_FindingsView;
		//        default:
		//            throw (NotFound(item));
		//    }
		//}

		//public static string Format (FirstLevAnatomyCategory anatCategory)
		//{
		//    switch(anatCategory)
		//    {
		//        case FirstLevAnatomyCategory.Mammo:
		//            return Resources.AnatomyCategory_Mammo;
		//        case FirstLevAnatomyCategory.Angio:
		//            return Resources.AnatomyCategory_Angio;
		//        case FirstLevAnatomyCategory.Chest:
		//            return Resources.AnatomyCategory_Chest;
		//        case FirstLevAnatomyCategory.FaceNeck:
		//            return Resources.AnatomyCategory_FaceNeck;
		//        case FirstLevAnatomyCategory.GI:
		//            return Resources.AnatomyCategory_GI;
		//        case FirstLevAnatomyCategory.GU:
		//            return Resources.AnatomyCategory_GU;
		//        case FirstLevAnatomyCategory.Heart:
		//            return Resources.AnatomyCategory_Heart;
		//        case FirstLevAnatomyCategory.Musculoskel:
		//            return Resources.AnatomyCategory_Musculoskel;
		//        case FirstLevAnatomyCategory.Neuro:
		//            return Resources.AnatomyCategory_Neuro;
		//        case FirstLevAnatomyCategory.Spine:
		//            return Resources.AnatomyCategory_Spine;
		//        default:
		//            throw (NotFound(anatCategory));
		//    }
		//}

		//public static string Format(ImageDisplayStyle displayStyle)
		//{
		//    switch (displayStyle)
		//    {
		//        case ImageDisplayStyle.Simple:
		//            return Resources.ImageDisplayStyle_Simple;
		//        case ImageDisplayStyle.Zoom:
		//            return Resources.ImageDisplayStyle_Zoom;
		//        default:
		//            throw (NotFound(displayStyle));
		//    }
		//}

		//public static string Format(SystemRole role)
		//{
		//    switch(role)
		//    {
		//        case SystemRole.ContentAdministrator:
		//            return Resources.SystemRole_ContentAdministrator;
		//        case SystemRole.EditingUser:
		//            return Resources.SystemRole_EditingUser;
		//        case SystemRole.Learner:
		//            return Resources.SystemRole_Learner;
		//        case SystemRole.SuperAdministrator:
		//            return Resources.SystemRole_SuperAdministrator;
		//        case SystemRole.UserAdministrator:
		//            return Resources.SystemRole_UserAdministrator;
		//        default:
		//            throw (NotFound(role));
		//    }
		//}

		//public static string Format(ContentManagementRole role)
		//{
		//    switch (role)
		//    {
		//        case ContentManagementRole.CreateCase:
		//            return Resources.ContentManagementRole_CreateCase;
		//        case ContentManagementRole.AcrPS:
		//            return Resources.ContentManagementRole_AcrPS;
		//        case ContentManagementRole.CaseContributor:
		//            return Resources.ContentManagementRole_CaseContributor;
		//        case ContentManagementRole.CaseEditor:
		//            return Resources.ContentManagementRole_CaseEditor;
		//        case ContentManagementRole.CategoryEditor:
		//            return Resources.ContentManagementRole_CategoryEditor;
		//        case ContentManagementRole.ChiefEditor:
		//            return Resources.ContentManagementRole_ChiefEditor;
		//        case ContentManagementRole.Reader:
		//            return Resources.ContentManagementRole_Reader;
		//        case ContentManagementRole.Reviewer:
		//            return Resources.ContentManagementRole_Reviewer;
		//        case ContentManagementRole.All:
		//            return Resources.ContentManagementRole_All;
		//        case ContentManagementRole.None:
		//            return Resources.ContentManagementRole_None;
		//        default:
		//            throw (NotFound(role));
		//    }
		//}


		//public static string Format (ProductType productType)
		//{
		//    switch(productType)
		//    {
		//        case ProductType.All:
		//            return Resources.Product_All;
		//        case ProductType.BaseCase:
		//            return Resources.Product_BaseCases;
		//        case ProductType.CaseInPoint:
		//            return Resources.Product_CaseInPoint;
		//        case ProductType.None:
		//            return Resources.Product_None;
		//        default:
		//            throw (NotFound(productType));
		//    }
		//}

		//public static string Format (CaseListSearchMode caseListSearchMode)
		//{
		//    switch(caseListSearchMode)
		//    {
		//        case CaseListSearchMode.All:
		//            return Resources.CaseListSearchMode_All;
		//        case CaseListSearchMode.Routed:
		//            return Resources.CaseListSearchMode_Routed;
		//            default:
		//            throw (NotFound(caseListSearchMode));
		//    }
		//}

		public static string Format (Target target)
		{
		    switch (target)
		    {
		        case Target.Blank:
		            return "_blank";
		        case Target.Parent:
		            return "_parent";
		        case Target.Self:
		            return "_self";
		        case Target.Top:
		            return "_top";
		            default:
		            throw (NotFound(target));
		    }
		}

		//public static string Format (QuestionType questionType)
		//{
		//    switch (questionType)
		//    {
		//        case QuestionType.NotSet:
		//            return Resources.QuestionType_NotSet;
		//        case QuestionType.MultipleChoice:
		//            return Resources.QuestionType_MultipleChoice;
		//        case QuestionType.CheckAllApply:
		//            return Resources.QuestionType_CheckAllApply;
		//        case QuestionType.TrueFalse:
		//            return Resources.QuestionType_TrueFalse;
		//        case QuestionType.Detection:
		//            return Resources.QuestionType_Detection;
		//    }
		//    throw NotFound(questionType);
		//}
		//public static string Format(LuceneSearchType searchType)
		//{
		//        switch (searchType)
		//        {
		//            case LuceneSearchType.CaseId:
		//                return Resources.LuceneSearchType_CaseId;
		//            case LuceneSearchType.Product:
		//                return Resources.LuceneSearchType_Product;
		//            case LuceneSearchType.Course:
		//                return Resources.LuceneSearchType_Course;
		//            case LuceneSearchType.Category1:
		//                return Resources.LuceneSearchType_Category1;
		//            case LuceneSearchType.Category2:
		//                return Resources.LuceneSearchType_Category2;
		//            case LuceneSearchType.Category3:
		//                return Resources.LuceneSearchType_Category3;
		//            case LuceneSearchType.EditingStatus:
		//                return Resources.LuceneSearchType_EditingStatus;
		//            case LuceneSearchType.ProductionStatus:
		//                return Resources.LuceneSearchType_ProductionStatus;
		//            case LuceneSearchType.SearchabilityStatus:
		//                return Resources.LuceneSearchType_SearchabilityStatus;
		//            case LuceneSearchType.CaseFlag:
		//                return Resources.LuceneSearchType_CaseFlag;
		//            case LuceneSearchType.CaseName:
		//                return Resources.LuceneSearchType_CaseName;
		//            case LuceneSearchType.CaseComment:
		//                return Resources.LuceneSearchType_CaseComment;
		//            case LuceneSearchType.AcrCode:
		//                return Resources.LuceneSearchType_ACRCode;
		//            case LuceneSearchType.Keywords:
		//                return Resources.LuceneSearchType_Keywords;
		//            case LuceneSearchType.CreditsRecordFullName:
		//                return Resources.LuceneSearchType_CreditsRecordFullName;
		//            case LuceneSearchType.InstitutionName:
		//                return Resources.LuceneSearchType_InstitutionName;
		//            case LuceneSearchType.Diagnosis:
		//                return Resources.LuceneSearchType_Diagnosis;
		//            case LuceneSearchType.History:
		//                return Resources.LuceneSearchType_History;
		//            case LuceneSearchType.Findings:
		//                return Resources.LuceneSearchType_Findings;
		//            case LuceneSearchType.DiffDiagnosis:
		//                return Resources.LuceneSearchType_DiffDiagnosis;
		//            case LuceneSearchType.Discussion:
		//                return Resources.LuceneSearchType_Discussion;
		//            case LuceneSearchType.UniqueCaseDiscussion:
		//                return Resources.LuceneSearchType_UniqueCaseDiscussion;
		//            case LuceneSearchType.TreatmentFollowUp:
		//                return Resources.LuceneSearchType_TreatmentFollowUp;
		//            case LuceneSearchType.CasePoints:
		//                return Resources.LuceneSearchType_CasePoints;
		//            case LuceneSearchType.References:
		//                return Resources.LuceneSearchType_References;
		//            case LuceneSearchType.PatientAge:
		//                return Resources.LuceneSearchType_PatientAge;
		//            case LuceneSearchType.ImageModality:
		//                return Resources.LuceneSearchType_ImageModality;
		//        }
		//        throw NotFound(searchType);
		//}

        /*
           public static string Format(UserRole role)
        {
            switch (role)
            {
                case UserRole.CaseContributor :
                    return Resources.Role_CaseContributor;
                case UserRole.ChiefEditor:
                    return Resources.Role_ChiefEditor;
                case UserRole.AcrPS:
                    return Resources.Role_ACRPS;
                case UserRole.Administrator:
                    return Resources.Role_Administrator;
            }
            throw (NotFound(role));
        }
         */
        #endregion
    }
}