using Admin.Common;
using System.Collections.Generic;

namespace Admin.CustomerEdit
{
    public interface IEditService
    {
        bool EditCustomerInteractive(IList<CustomerModel> customers);
    }
}
