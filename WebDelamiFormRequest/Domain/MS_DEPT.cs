using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDelamiFormRequest.Domain
{
    public class MS_DEPT
    {
        public int ID { get; set; }
        public string KODE_DEPT { get; set; }
        public string DEPT { get; set; }
        public string KET { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public bool STATUS { get; set; }
    } 

}