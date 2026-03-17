using Admin.Common;
using System;
using System.Collections.Generic;

namespace Admin.CustomerDelete
{
    public class DeleteService
    {
        public bool DeleteCustomerInteractive(IList<CustomerModel> customers)
        {
            if (customers.Count == 0)
            {
                Console.WriteLine("Không có customer nào.");
                return false;
            }

            var selectedIndex = PromptCustomerSelection(customers);
            if (selectedIndex < 0)
            {
                return false;
            }

            customers.RemoveAt(selectedIndex);
            return true;
        }

        private static int PromptCustomerSelection(IList<CustomerModel> customers)
        {
            Console.WriteLine("Danh sách customer:");
            for (int i = 0; i < customers.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {customers[i].Name}");
            }

            while (true)
            {
                Console.Write("Chọn customer cần xóa (Nhập 0 để thoát chương trình): ");
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Lua chon khong hop le.");
                    continue;
                }

                if (!int.TryParse(input, out int selectedIndex))
                {
                    Console.WriteLine("Lua chon khong hop le.");
                    continue;
                }

                if (selectedIndex == 0)
                {
                    return -1;
                }

                if (selectedIndex < 1 || selectedIndex > customers.Count)
                {
                    Console.WriteLine("Lua chon khong hop le.");
                    continue;
                }

                return selectedIndex - 1;
            }
        }
    }
}
