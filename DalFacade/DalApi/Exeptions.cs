using System;
namespace DalFacade.DalApi
{
    public class Exeptions
    {
        public Exeptions()
        {
            void NotFound()
            {
                Console.WriteLine("ERROR, NOT FOUND");
            }
            void Duplication()
            {
                Console.WriteLine("The object does already exist");
            }
        }
    }
}

