using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDelamiFormRequest.Domain
{
    public class MS_USER_HANDLE
    {
        public Int64 ID { get; set; }
        public string KODE_FORM { get; set; }
        public string KD_JABATAN { get; set; }
        public int URUTAN { get; set; }
        public string ACTION { get; set; }
        public string PAGE_NAME { get; set; }
        public string SP { get; set; }

    } 
}