using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BankMidterm.Validators
{
    interface AdditionalValidator<T>
    {
        ModelStateDictionary check(T entity, ModelStateDictionary modelState);
    }
}
