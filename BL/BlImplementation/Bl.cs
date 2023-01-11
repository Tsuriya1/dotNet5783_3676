using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

sealed internal class Bl : IBl
{
    public Icart Cart => new BlImplementation.Cart();

    public Iorder Order => new BlImplementation.Order();

    public BlApi.Iproduct Product => new BlImplementation.Product();
}
