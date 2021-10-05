using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiOpenpayAutorizacion.Models
{
    public class AutorizacionRequest
    {
        public string Folio { get; set; }
        public string Local_date { get; set; }//aaaa-MM-dd Ej: 2021-10-05T11:12:00-05:00
        public decimal Amount { get; set; }
        public long Trx_no { get; set; } //Valor maximo: 9,223,372,036,854,775,807
        public int Authorization_number { get; set; }


    }
}
