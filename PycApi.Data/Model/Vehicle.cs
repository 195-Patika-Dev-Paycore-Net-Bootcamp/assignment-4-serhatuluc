using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PycApi.Data.Model
{
    public class Vehicle
    {
        public virtual int Id { get; set; }
        public virtual string name { get; set; }
        public virtual string plate { get; set; }
    }
}
