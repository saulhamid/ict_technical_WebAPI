using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ict_technical_WebAPI.Model.Utility
{
    public static class Message
    {
        #region requisition mail
        public static string requisitionMailSubject = "Requisition is waiting for your approval";
        public static string requisitionMailNoReceiver = "There have no approval person for approve this requisition.";
        public static string requisitionNewInfo = "Requisition is waiting for your approval (level 1) .";
        public static string requisitionApprove1Info = "Level 1 already approved. Waiting for next level approval.";
        public static string requisitionApprove2Info = "Level 2 already approved. Waiting for next level approval."; 
        public static string requisitionApprove3Info = "Level 3 already approved.";
        #endregion requisition mail
        #region CommonMessage
        public static string SUCCESS { get { return "Request Successful."; } }
        public static string SAVED { get { return "Save Successfully !"; } }
        public static string SAVED_ERROR { get { return "Data Not Save."; } }
        public static string FAILED { get { return "Request Failed."; } }
        public static string UPDATED { get { return "Updated Successfully !"; } }
        public static string UPDATED_ERROR { get { return "Data not Updated !"; } }
        public static string DELETED { get { return "Deleted Successfully !"; } }
        public static string DELETE_ERROR { get { return "Data not Deleted"; } }
        public static string INVAILD_DATA { get { return "Invaild Data !"; } }
        public static string DUPLICATE { get { return "Data is Duplicated!"; } }
        public static string DATA_EXISTS { get { return "Data is Exists."; } }
        public static string NOTFOUND { get { return "Data Not Found"; } }
       public static string noimage { get { return "~/img/avatars/male.png"; } }

        public static string NOT_FOUND { get { return "Not Found"; } }
        #endregion CommonMessage
    }
}
