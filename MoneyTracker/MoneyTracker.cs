using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker
{
    public class MoneyTracker
    {
        private Person Owner { get; set; }

        public List<Wallet> Wallets { get; set; } = new List<Wallet>();
        public List<Transaction> Transactions{ get; set; } = new List<Transaction>();

        public List<Category> Categories { get; set; } = new List<Category>();

        public MoneyTracker (Person owner)
        {
            Owner = owner;
        }

        public void Initialize()
        {
            InitializeWallets();
            InitializeCategories();
        }

        public void AddTransaction(int sum, WalletType walletType, 
            Category category, DateTime dateTran, string comment)
        {
            var wallet = Wallets.Find(w => w.WalletType == walletType);
            var categoryFound = Categories.Find(c => c.OperationType == category.OperationType);
            if (wallet == null)
            {
                throw new Exception($"У {Owner.Name} нет кошелька {walletType}");
            }
            if (categoryFound == null)
            {
                throw new Exception($"У {Owner.Name} нет такой категории: {category.Name}");
            }

            if (sum < 0)
            {
                if (categoryFound.OperationType == OperationType.Income)
                {
                    throw new Exception("Нельзя добавить в доходную категорию операцию по снятию");
                }
                Withdraw(sum, wallet);
            }
            else if (sum > 0)
            {
                if (categoryFound.OperationType == OperationType.Expense)
                {
                    throw new Exception("Нельзя добавить в расходную категорию операцию по добавлению");
                }
                Enroll(sum, wallet);
            }
            else 
            {
                throw new Exception("Сумма не может быть равна нулю");
            }

            Transactions.Add(new Transaction
            {
                Amount = sum,
                Category = category,
                Date = dateTran,
                Comment = comment
            });
        }
        public void FindTransactionsByCategory(Category category)
        {
            var foundCategory = Categories.Find(c => c.Name == category.Name);
            
            if (foundCategory == null)
            {
                throw new Exception($"У {Owner.Name} нет такой категории: {category.Name}");
            }

            Console.WriteLine($"Категория: {foundCategory.Name}");
            foreach (var transaction in Transactions)
            {
                if (transaction.Category.Name == foundCategory.Name)
                {
                    Console.WriteLine($"{transaction.Date.ToString("dd/MM/yyyy")}: {transaction.Amount}тг\t{transaction.Comment}");
                }
            }          
        }

        private void InitializeWallets()
        {
            Wallets.AddRange(new List<Wallet>
            {
                new Wallet
                {
                    WalletType = WalletType.BankCard,
                    Amount = 100000
                },
                new Wallet
                {
                    WalletType = WalletType.Cash,
                    Amount = 30000
                }
            });
        }

        private void InitializeCategories()
        {
            Categories.AddRange(new List<Category>
            {
                new Category
                {
                    Name = "Продукты",
                    OperationType = OperationType.Expense
                },
                new Category
                {
                    Name = "Хоз.товары",
                    OperationType = OperationType.Expense
                },
                new Category
                {
                    Name = "Здоровье",
                    OperationType = OperationType.Expense
                },
                new Category
                {
                    Name = "Зарплата",
                    OperationType = OperationType.Income
                },
                new Category
                {
                    Name = "Премия",
                    OperationType = OperationType.Income
                }
            }) ;
        }

        private static void Withdraw(int sum, Wallet wallet)
        {
            if (wallet.Amount >= sum * -1)
            {
                wallet.Amount -= sum * -1;
                Console.WriteLine($"Со счета списано {sum}тг. Остаток - {wallet.Amount}");
            }
            else
            {
                throw new Exception("Не хватает");  
            }
        }

        private static void Enroll(int sum, Wallet wallet)
        {
            wallet.Amount += sum;
            Console.WriteLine($"На счет добавлено {sum}тг. Остаток - {wallet.Amount}");
        }
    }
}
