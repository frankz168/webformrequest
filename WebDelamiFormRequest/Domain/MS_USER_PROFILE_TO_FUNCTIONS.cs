using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDelamiFormRequest.Domain
{
    public class MS_USER_PROFILE_TO_FUNCTIONS
    {
        public int Id { get; set; }
        public string UserProfileId { get; set; }
        public string FunctionId { get; set; }
        public string Permission { get; set; }
        public string CB { get; set; }
        public DateTime? CO { get; set; }
        public string MB { get; set; }
        public DateTime? MO { get; set; }
    }

}