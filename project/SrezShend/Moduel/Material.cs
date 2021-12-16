using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrezShend
{
    partial class Material
    {
        public string ValidImage
        {
            get 
            { 
                if (String.IsNullOrWhiteSpace(Image) || String.IsNullOrEmpty(Image)) return @"\img\materials\picture.png";
                else return Image;
            }
        }
        public string ValidSuppliers
        {
            get 
            {
                string suppliers = "";
                List<Supplier> suppliersList = Supplier.ToList();
                if (suppliersList != null && suppliersList.Count() > 0)
                {
                    for (int i = 0; i < suppliersList.Count(); i++)
                    {
                        suppliers += suppliersList[i];
                        if (suppliersList.Last() == suppliersList[i]) suppliers += ".";
                        else suppliers += ", ";
                    }
                }
                else suppliers = "Отсутствуют.";
                return suppliers;
            }
        }
    }
}
