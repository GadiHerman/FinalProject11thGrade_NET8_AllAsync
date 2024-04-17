using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Producttype
    {
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public int QuantityInCategory { get; set; }

        public Producttype()
        {
        }

        public Producttype(int typeID, string typeName, int quantityInCategory)
        {
            TypeID = typeID;
            TypeName = typeName;
            QuantityInCategory = quantityInCategory;
        }
    }
}
