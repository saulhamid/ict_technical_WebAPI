using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ict_technical_WebAPI.Model.Utility
{
  public class ConfigurationPrefix
    {
        public int PageSize = 20;

        #region PREFIX_PROCUREMENT
        public int PROCUREMENT_LENGTH = 5;

        public string PROCUREMENT_ERF = "ERF";
        public string PROCUREMENT_RFQ = "RFQ";
        public string PROCUREMENT_RFQ_Vendor = "SQV";
        public string PROCUREMENT_QUO = "QUO";
        public string PROCUREMENT_PO = "PO";
        public string PROCUREMENT_GRN = "GRN";
        public string PROCUREMENT_PI = "PI";
        public string ProductConfigurator_ISSUE = "ISS";
        public string ProductConfigurator_Issue_Return = "ISSR";
        public string ProductConfigurator_Receive = "REC";
        public string ProductConfigurator_Receive_Return = "RECR";
        public string ProductConfigurator_GRN_CN = "GRNC";
        public string ProductConfigurator_GRN_DN = "GRND";
        public string ProductConfigurator_StockLedger = "STL";
        public string PREFIX_ProductConfigurator_LENGTH = "5";


      #endregion PREFIX_PROCUREMENT
        public bool IsOverAllow = false;
        #region ImageS PATH
        public string IMAGE_PATH = @"D:\ERPImages\";
        public string REPORT_PATH = @"D:\ERPReports\";
        #endregion Image

        #region Attachment
        public string PM_ATTACHMENT_PATH = "/Content/PM/";
        #endregion
    }
}
