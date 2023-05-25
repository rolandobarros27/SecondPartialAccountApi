using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    public class AccountModel
    {
        public int id { get; set; }
        public int id_account { get; set; }
        public string name { get; set; }
        public string number { get; set; }
        public double balance { get; set; }
        public double limit_balance { get; set; }
        public double limit_transfer { get; set; }
        public bool status { get; set; }
        //public PersonaModel titular { get; set; }
    }
}
