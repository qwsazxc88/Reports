using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Presenters.UI.ViewModel
{
    public interface ICheckBoxes
    {
         bool IsApprovedByUser { get; set; }
         bool IsApprovedByUserHidden { get; set; }
         bool IsApprovedByUserEnable { get; set; }
         //bool IsApprovedByUserChecked { get; set; }
         bool IsApprovedByManager { get; set; }
         bool IsApprovedByManagerHidden { get; set; }
         bool IsApprovedByManagerEnable { get; set; }
         bool IsApprovedByPersonnelManager { get; set; }
         bool IsApprovedByPersonnelManagerHidden { get; set; }
         bool IsApprovedByPersonnelManagerEnable { get; set; }
         bool IsPostedTo1C { get; set; }
         bool IsPostedTo1CHidden { get; set; }
         bool IsPostedTo1CEnable { get; set; }

         bool IsApproved { get; set; }
         bool IsApprovedEnable { get; set; }
    }
    public interface IAttachment
    {
        string Attachment { get; set; }
        int AttachmentId { get; set; }
    }
    public interface IOrderScanAttachment
    {
        string OrderScanAttachment { get; set; }
        int OrderScanAttachmentId { get; set; }
    }
    public interface IUnsignedOrderScanAttachment
    {
        string UnsignedOrderScanAttachment { get; set; }
        int UnsignedOrderScanAttachmentId { get; set; }
    }
    public interface IT2ScanAttachment
    {
        string T2ScanAttachment { get; set; }
        int T2ScanAttachmentId { get; set; }
    }
    public interface IUnsignedT2ScanAttachment
    {
        string UnsignedT2ScanAttachment { get; set; }
        int UnsignedT2ScanAttachmentId { get; set; }
    }
    public interface IDismissalAgreementScanAttachment
    {
        string DismissalAgreementScanAttachment { get; set; }
        int DismissalAgreementScanAttachmentId { get; set; }
    }
    public interface IUnsignedDismissalAgreementScanAttachment
    {
        string UnsignedDismissalAgreementScanAttachment { get; set; }
        int UnsignedDismissalAgreementScanAttachmentId { get; set; }
    }
    public interface IF182NScanAttachment
    {
        string F182NScanAttachment { get; set; }
        int F182NScanAttachmentId { get; set; }
    }
    public interface IF2NDFLScanAttachment
    {
        string F2NDFLScanAttachment { get; set; }
        int F2NDFLScanAttachmentId { get; set; }
    }
    public interface IWorkbookRequestScanAttachment
    {
        string WorkbookRequestScanAttachment { get; set; }
        int WorkbookRequestScanAttachmentId { get; set; }
    }
    public interface ICheckForEntity
    {
        bool IsDeleteAvailable { get; set; }
        bool IsDelete { get; set; }
    }
    public interface ICheckForEntityBeginDate:ICheckForEntity
    {
        DateTime? BeginDate { get; set; }
    }
    public interface ICheckForEntityEndDate : ICheckForEntity
    {
        DateTime? EndDate { get; set; }
    }
}
