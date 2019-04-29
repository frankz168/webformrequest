using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDelamiFormRequest.Domain
{
    public class TR_FORM5_REPAIR_PERMINTAAN
    {
        public int ID_PERBAIKAN { get; set; }
        public string NO_FORM { get; set; }
        public string NO_PERMINTAAN { get; set; }
        public string PERMINTAAN_PERBAIKAN { get; set; }
        public string PERMINTAAN_PERBAIKAN_2 { get; set; }
        public string PIC { get; set; }
        public DateTime? COMPLETE_DATE { get; set; }
        public DateTime? ACTUAL_FINISH_DATE { get; set; }
        public Decimal BUDGET { get; set; }
        public string UPLOAD_FILE { get; set; }
        public string MODIFIED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
    } 

}