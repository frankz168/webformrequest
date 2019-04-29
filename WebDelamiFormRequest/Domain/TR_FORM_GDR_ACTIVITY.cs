using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDelamiFormRequest.Domain
{
    public class TR_FORM_GDR_ACTIVITY
    {
        public int ID { get; set; }
        public string USERNAME { get; set; }
        public DateTime? ACTIVITY_TIME { get; set; }
        public string KODE_FORM { get; set; }
        public string NO_FORM { get; set; }
        public string STATUS { get; set; }
        public string DESCRIPTION { get; set; }
        public string REVISION { get; set; }
        public int URUTAN { get; set; }
        public string SP { get; set; }
        public string USER_CURRENT { get; set; }
        public string NEXT_USER { get; set; }
        public int URUTAN_USER_CURRENT { get; set; }
        public int URUTAN_NEXT_USER { get; set; }
    } 
}