using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDelamiFormRequest.Domain
{
    public class TR_FORM_GDR_CUST
    {
        public int ID { get; set; }
        public string KODE_FORM { get; set; }
        public string NO_FORM { get; set; }
        public string kode_cust { get; set; }
        public string kode_ct { get; set; }
        public string site { get; set; }
        public string nama_cust { get; set; }
        public string nama_ct { get; set; }
    }
}