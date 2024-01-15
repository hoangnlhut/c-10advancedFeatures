using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain;

namespace WarehouseManagementSystem.Business
{
    public class PayloadValidator
    {
        public bool Validate(ReadOnlySpan<byte> payload)
        {
            var signature = payload[^128..];
            //payload[0] = 1; // it will change value after finishing this method --> we want a readonlyspan --> so using from Span to ReadOnlySpan
            foreach (var item in signature)
            {
                if (item == 1) return false;
            }
            return true;
        }

        //this method to check that 
        // if the ReadOnlySpan contains reference types and that reference type has properties that are not immutable , the data would be changeable
        public bool ValidateReferenceType(ReadOnlySpan<Order> orders)
        {
            //payload[0] = 1; // it will change value after finishing this method --> we want a readonlyspan --> so using from Span to ReadOnlySpan
            (orders[0].LineItems.ToArray())[0].Name = "Trang"; 
            foreach (var item in orders)
            {
                if (item.LineItems.Select(x => x.Name == "hoang1").ToString() == "hoang1") return false;
            }
            return true;
        }
    }
}
