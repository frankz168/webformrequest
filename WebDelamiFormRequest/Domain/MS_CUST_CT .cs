using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDelamiFormRequest.Domain
{
    public class MS_CUST_CT
    {
        public Decimal id { get; set; }
        public string kode_cust { get; set; }
        public string kode_ct { get; set; }
        public string site { get; set; }
        public string nama_cust { get; set; }
        public string nama_ct { get; set; }
        public string brand { get; set; }
        public string bln_live { get; set; }
        public string td { get; set; }
        public string folder_dbox { get; set; }
        public string password { get; set; }
        public string nm_login_op { get; set; }
        public string nm_login_RM { get; set; }
        public string nm_login_RS { get; set; }
    }
}