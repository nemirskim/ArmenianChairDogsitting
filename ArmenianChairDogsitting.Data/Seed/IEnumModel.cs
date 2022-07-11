using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmenianChairDogsitting.Data.Seed;

public interface IEnumModel<TModel, TModelIdType>
{
    TModelIdType Id { get; set; }
    //string Name { get; set; }
}
