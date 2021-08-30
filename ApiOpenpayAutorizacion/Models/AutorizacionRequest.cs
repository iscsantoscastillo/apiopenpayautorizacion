using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiOpenpayAutorizacion.Models
{
    public class AutorizacionRequest
    {
        public string Folio { get; set; }
        public string Local_date { get; set; }
        public decimal Amount { get; set; }
        public int Trx_no { get; set; }
        public int Authorization_number { get; set; }


    }
}
