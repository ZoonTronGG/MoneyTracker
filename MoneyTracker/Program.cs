using System;

namespace MoneyTracker
{
    public static class Program
    {
        public static void Main()
        {
            var Tom = new Person { Name = "Tom" };

            var moneyTracker = new MoneyTracker(Tom);
            var category = new Category {
                Name = "Продукты",
                OperationType = OperationType.Expense
            };
            var incomeCategory = new Category
            {
                Name = "Зарплата",
                OperationType = OperationType.Income
            };
            moneyTracker.Initialize();
            moneyTracker.AddTransaction(-10000, WalletType.BankCard,
                category, DateTime.Now, "Шоколад");
            moneyTracker.AddTransaction(10000, WalletType.BankCard,
                incomeCategory, DateTime.Now, "ЗП за май");
            moneyTracker.FindTransactionsByCategory(category);
        }
    }
}