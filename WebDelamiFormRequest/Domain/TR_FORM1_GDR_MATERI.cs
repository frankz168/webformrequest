using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDelamiFormRequest.Domain
{
    public class TR_FORM1_GDR_MATERI
    {
        public int ID_MATERI { get; set; }
        public string NO_FORM { get; set; }
        public string JENIS_MATERIAL_CETAK { get; set; }
        public string UKURAN { get; set; }
        public Decimal JUMLAH_A4 { get; set; }
        public Decimal JUMLAH_A5 { get; set; }
        public Decimal JUMLAH_QTY { get; set; }
        public string PENJELASAN { get; set; }
    } 

}