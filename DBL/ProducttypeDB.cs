using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class ProducttypeDB : BaseDB<Producttype>
    {
        protected override async Task<List<Producttype>> CreateListModelAsync(List<object[]> rows)
        {
            List<Producttype> typeList = new List<Producttype>();
            foreach (object[] item in rows)
            {
                Producttype p;
                p = (Producttype)await CreateModelAsync(item);
                typeList.Add(p);
            }
            return typeList;
        }

        protected override async Task<Producttype> CreateModelAsync(object[] row)
        {
            Producttype p = new Producttype();
            p.TypeID = int.Parse(row[0].ToString());
            p.TypeName = row[1].ToString();
            p.QuantityInCategory = int.Parse(row[2].ToString());
            return p;
        }

        protected override string GetPrimaryKeyName()
        {
            return "ProductTypeID";
        }

        protected override string GetTableName()
        {
            return "producttypes";
        }

        public async Task<List<Producttype>> GetAllWithQuantityAsync()
        {
            string sql = @"Select
                                producttypes.ProductTypeID,
                                producttypes.ProductTypeName,
                                Count(products.ProductID) As CountID
                            From
                                producttypes Inner Join
                                products On products.ProductTypeID = producttypes.ProductTypeID
                            Group By
                                producttypes.ProductTypeID,
                                producttypes.ProductTypeName";
            return (List<Producttype>)await SelectAllAsync(sql);
        }
    }
}
