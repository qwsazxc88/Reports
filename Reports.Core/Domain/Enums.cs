namespace Reports.Core.Domain
{
    public enum ReportType
    {
        CalList = 1,
        NDFL = 2
    }

    public enum QuestionTypes
    {
        WithAnswer = 0,
        WithoutAnswer = 1,
        All = 2
    }

    public class CommonConstants
    {
        public const string UnhandledExceptionSessionKey = "UNHANDLED_EXCEPTION";
        public const string StatisticsSessionKey = "STATICTICS";
        public const string UserSessionStorageKey = "Acr3S.UserInfo";
        public const string LockedCasesSessionStorageKey = "LockedCases";
        public const string ErrorPageExceptionKey = "CacheId";
    }

//    /// <summary>
//    /// Editing status model
//    /// </summary>
//    public enum CaseEditingStatus
//    {
//        NewCase = 0,
//        ContentContributionByCaseEditor = 1,
//        SubmittedToSectionEditor = 2,
//        ReviewBySectionEditor = 3,
//        SentBackToCaseEditor = 4,
//        SubmittedToAcr = 5,
//        ReviewByAcr = 6,
//        SentBackToSectionEditor = 7,
//        SubmittedToChiefEditor = 8,
//        ReviewByChiefEditor = 9,
//        SentBackToAcr = 10,
//        ReadyToGo = 11
//    }

//    /// <summary>
//    /// Production status model
//    /// </summary>
//    public enum CaseProductionStatus
//    {
//        Out = 0,
//        In = 1,
//        Archive = 2
//    }

//    /// <summary>
//    /// Search status model
//    /// </summary>
//    public enum CaseSearchStatus
//    {
//        NonSearchable = 0,
//        Searchable = 1
//    }

//    /// <summary>
//    /// System Role
//    /// </summary>
//    [Flags]
//    public enum SystemRole
//    {
//        None = 0,
//        Learner = 1,
//        EditingUser = 2,
//        UserAdministrator = 4,
//        ContentAdministrator = 8,
//        SuperAdministrator = 0x10
//    }

//    /// <summary>
//    /// Content management role
//    /// </summary>
//    [Flags]
//    public enum ContentManagementRole
//    {
//        None = 0,
//        All = Reader | CaseContributor | CaseEditor | AcrPS | ChiefEditor | Reviewer | CategoryEditor,
//        Reader = 1,
//        CaseContributor = 2,
//        CaseEditor = 4,
//        AcrPS = 8,
//        ChiefEditor = 0x10,
//        Reviewer = 0x20,
//        CategoryEditor = 0x40,
//        CreateCase = 0x80
//    }

//    /// <summary>
//    /// Type of message
//    /// </summary>
//    public enum MessageType
//    {
//        Notification = 0,
//        Notice = 1,
//        Info = 2,
//        NewUser = 3,
//        PrivilegesGranted = 4,
//        NewUserApproved = 5,
//        PasswordRestore = 6,
//        CaseDeleted = 7
//    }

//    /// <summary>
//    /// Type of case content element
//    /// </summary>
//    [Flags]
//    public enum ContentElementType
//    {
//        None = 0,
//        Diagnosis = 1,
//        History = 2,
//        Findings = 4,
//        DiffDiagnosis = 8,
//        Discussion = 0x10,
//        UniqueCaseDiscussion = 0x20,
//        TreatmentFollowUp = 0x40,
//        CasePoints = 0x80,
//        References = 0x100,
//        Patient = 0x200,
//        ArbitraryText = 0x400
//    }
//    public enum CaseActiveTab
//    {
//        Properties = 0,
//        Images = 1,
//    }
//    //public enum CaseTextItemsActiveItem
//    //{
//    //    NoItem = 0,
//    //    History = 1,
//    //    Findings = 2,
//    //    Diagnosis = 3,
//    //    DifferentialDiagnosis = 4,
//    //    Discussion = 5,
//    //    UniqueCaseDiscussion = 6,
//    //    Treatment = 7,
//    //    CasePoints = 8,
//    //    References = 9,
//    //    Patient = 10
//    //}
//    public enum EditableObject
//    {
//        Comment,
//        Diagnosis,
//        DiffDiagnosis,
//        Discussion,
//        History,
//        AbbrHistory,
//        CasePoint,
//        Reference,
//        QuestionStem,
//        MultiAnswer,
//        Answer,
//        HomePageNotes,
//        HomePageEditors,
//        ImageCapture,
//        ExplanationText,
//        Patient,
//        CaseCredit
//    }

//    public enum QuestionType
//    {
//        NotSet = -1,
//        MultipleChoice = 0,
//        CheckAllApply =1,
//        TrueFalse = 2,
//        Detection = 3
//    }
//    public enum CaseFlag
//    {
//        Unset = 0, 
//        Red = 1, 
//        Orange = 2, 
//        Yellow = 3, 
//        Green = 4, 
//        Blue = 5, 
//        Purple = 6
//    }
//    public enum CaseCreditType
//    {
//        Author = 0,
//        Reviewer = 1
//    }
//    public enum CaseCreditContainerType
//    {
//        Add = 0,
//        Edit = 1
//    }

//    public enum ImageItemViewMode
//    {
//        NoItem = 0,
//        EditItem = 1
//    }
//    public enum CaseCreditStatusType
//    {
//        Updated = 0,
//        Approved = 1
//    }

//    public enum ImageDisplayStyle
//    {
//        Simple = 0,
//        Zoom = 1
//    }
//    public enum CaseContentActiveView
//    {
//        Blank = 0,
//        AddItem = 1,
//        EditItem = 2
//    }
//    public enum CaseContentAddActiveView
//    {
//        Blank = 0,
//        AddText = 1,
//        AddQuestion = 2,
//        NoQuestions = 3
//    }

//    public enum CaseTemplateItemsActiveView
//    {
//        Blank = 0,
//        AddItem = 1,
//        EditItem = 2
//    }
//    public enum CaseContentItemType
//    {
//        BaseCaseTextElement = 1,
//        CaseCredits = 2,
//        Image = 3,
////        ArbitraryText = 4,
//        Question = 5,
//        TemplatedItem = 6,
//        PageBreak = 7,
//        EmptyLine = 8,
//        HorizontalLine = 9
//    }
//    [Flags]
//    public enum CaseContentTextElementItemType
//    {
//        History = ContentElementType.History,
//        Findings = ContentElementType.Findings,
//        Diagnosis = ContentElementType.Diagnosis,
//        DifferentialDiagnosis = ContentElementType.DiffDiagnosis,
//        Discussion = ContentElementType.Discussion ,
//        UniqueCaseDiscussion = ContentElementType.UniqueCaseDiscussion,
//        Treatment = ContentElementType.TreatmentFollowUp,
//        CasePoints = ContentElementType.CasePoints,
//        References = ContentElementType.References,
//        Patient = ContentElementType.Patient,
//        ArbitraryText = ContentElementType.ArbitraryText
//    }

//    public enum CaseContentTemplatedItemType
//    {
//        SimpleImageView = 1,
//        StackView = 2,
//        FindingsView = 3
//    }
//    public enum CaseContextEditItemType
//    {
//        EditBlank = 0,
//        EditBaseText = 1,
//        EditPatient = 2,
//        EditQuestion = 3,
//        //EditArbitraryText = 4,
//        EditImage = 4,
//        EditTemplatedItem = 5
//        //EditSimpleImageViewer = 6,
//        //EditStackViewer = 7,
//        //EditFindingsViewer = 8,

//    }

//    public enum MediaContentType : int
//    {
//        None = 0,
//        Image = 1,
//        Video = 2
//    }

//    public enum FirstLevAnatomyCategory
//    {
//        Mammo = 0,
//        Neuro = 1,
//        FaceNeck = 2,
//        Spine = 3,
//        Musculoskel = 4,
//        Heart = 5,
//        Chest = 6,
//        GI = 7,
//        GU = 8,
//        Angio = 9
//    }
//    public enum TextSearchOption
//    {
//        All,
//        Any,
//        Exact
//    }

//    public enum GraphVertexType
//    {
//        Case = 1,
//        Category,
//        Course,
//        Product,
//        Root
//    }

//    public enum CaseListSearchMode
//    {
//        Routed,
//        All
//    }

//    [Flags]
//    public enum CasePhase
//    {
//        None = 0,
//        CC = 1,
//        CatEdit = 2,
//        Acr = 4,
//        ChifEdit = 8,
//        Rtg = 0x10
//    }

    public enum Target
    {
        Blank,
        Self,
        Parent,
        Top
    }

//    public enum AnnotationLayerType
//    {
//        Generic,
//        Watermark,
//        Detection  
//    }

//    public enum LuceneSearchType
//    {
//      CaseId,
//      Product,
//      Course,
//      Category1, 
//      Category2,
//      Category3,
//      EditingStatus,
//      ProductionStatus,
//      SearchabilityStatus,
//      CaseFlag,
//      CaseName,
//      CaseComment,
//      AcrCode,
//      Keywords,
//      CreditsRecordFullName,
//      InstitutionName,
//      Diagnosis,
//      History,
//      Findings,
//      DiffDiagnosis,
//      Discussion,
//      UniqueCaseDiscussion,
//      TreatmentFollowUp,
//      CasePoints,
//      References,
//      PatientAge,
//      ImageModality
//    }

//    public enum SpellCheckerType
//    {
//        Google = 0,
//        Sentry = 1 
//    }
}