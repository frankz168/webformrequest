using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDelamiFormRequest.Domain
{
    public class MS_USER
    {
        public Int64 ID { get; set; }
        public int ID_DEPT { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string DEPT { get; set; }
        public string EMAIL { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public bool STATUS { get; set; }
        public string FORGOT_PASSWORD { get; set; }
        public string FORGOT_PASSWORD_TOKEN { get; set; }
        public DateTime? LAST_PASSWORD_CHANGE { get; set; }
        public string KD_BRAND { get; set; }
        public string KD_JABATAN { get; set; } 
    } 
}