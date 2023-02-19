using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

// intefaces of three main entities
namespace BlApi
{
    public interface IBl
    {
        public Icart Cart { get; }
        public Iorder Order { get; }
        public Iproduct Product { get; }
    }
}
