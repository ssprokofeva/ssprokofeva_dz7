using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tumakov
{
    internal class Program
    {
        public class BankAccount
        {
            private readonly string _accountNumber;
            private decimal _balance;

            public BankAccount(string accountNumber)
            {
                _accountNumber = accountNumber;
                _balance = 0m;
            }

            public string AccountNumber => _accountNumber;
            public decimal Balance => _balance;

            public void Deposit(decimal amount)
            {
                if (amount > 0)
                {
                    _balance += amount;
                }
            }

            public bool Withdraw(decimal amount)
            {
                if (_balance >= amount && amount > 0)
                {
                    _balance -= amount;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool Transfer(BankAccount destinationAccount, decimal amount)
            {
                if (Withdraw(amount))
                {
                    destinationAccount.Deposit(amount);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        static void Main(string[] args)
        {
            Task1();
            Task2();
            Task3();
            Task4();
            Task5();
            Task6();
            Console.WriteLine("Введите что-нибудь, чтобы закрыть");
            Console.ReadKey();
        }

        /// <summary>
        /// Упражнение 8.1
        /// </summary>
        static void Task1()
        {
            var accountA = new BankAccount("123456");
            var accountB = new BankAccount("654321");

            accountA.Deposit(1000m);

            if (accountA.Transfer(accountB, 500m))
            {
                Console.WriteLine($"Перевод выполнен. Баланс счета {accountA.AccountNumber}: {accountA.Balance}");
                Console.WriteLine($"Баланс счета {accountB.AccountNumber}: {accountB.Balance}");
            }
            else
            {
                Console.WriteLine("Ошибка! Недостаточно средств.");
            }
        }

        /// <summary>
        /// Упражнение 8.2
        /// </summary>
        static void Task2()
        {
            string inputString = "Привет";
            // Вызов метода ReverseString
            string reversedString = ReverseString(inputString);

            Console.WriteLine(reversedString);
        }

        static string ReverseString(string str)
        {
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        /// <summary>
        /// Упражнение 8.3
        /// </summary>
        static void Task3()
        {
            Console.Write("Введите имя файла: ");
            string fileName = Console.ReadLine();

            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл не найден!");
                return;
            }

            try
            {
                string content = File.ReadAllText(fileName);
                string upperCaseContent = content.ToUpper();

                string outputFileName = Path.GetFileNameWithoutExtension(fileName) + "_uppercase.txt";
                File.WriteAllText(outputFileName, upperCaseContent);

                Console.WriteLine($"Содержимое файла '{fileName}' было преобразовано в верхний регистр и сохранено в файле '{outputFileName}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Упражнение 8.4
        /// </summary>
        static void Task4()
        {
            object obj = "Hello, World!";

            CheckIfFormattable(obj);
        }

        static void CheckIfFormattable(object obj)
        {
            if (obj is IFormattable formattableObject)
            {
                Console.WriteLine($"{obj} реализует интерфейс IFormattable.");
            }
            else
            {
                Console.WriteLine($"{obj} НЕ реализует интерфейс IFormattable.");
            }
        }

        /// <summary>
        /// Домашнее задание 8.1
        /// </summary>
        static void Task5()
        {
            string inputFilePath = "input.txt";
            string outputFilePath = "output.txt";

            try
            {
                using (StreamReader reader = new StreamReader(inputFilePath))
                {
                    using (StreamWriter writer = new StreamWriter(outputFilePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            ProcessEmail(ref line);
                            writer.WriteLine(line);
                        }
                    }
                }

                Console.WriteLine("Список e-mail успешно сохранен в файл: " + outputFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при работе с файлами: " + ex.Message);
            }
        }

        static void ProcessEmail(ref string email)
        {
            int index = email.IndexOf('@');
            if (index != -1)
            {
                email = email.Substring(index + 1);
            }
        }

        /// <summary>
        /// Домашнее задание 8.2
        /// </summary>
        static void Task6()
        {
            List<Song> songs = new List<Song>();

            Song song1 = new Song("Jingle Bells", "James Lord Pierpont");
            Song song2 = new Song("Last Christmas", "Wham!");
            Song song3 = new Song("All I Want for Christmas Is You", "Mariah Carey");
            Song song4 = new Song("Santa Claus Is Coming to Town", "John Frederick Coots and Haven Gillespie");

            songs.Add(song1);
            songs.Add(song2);
            songs.Add(song3);
            songs.Add(song4);

            foreach (var song in songs)
            {
                song.PrintInfo();
            }

            if (song1.Equals(song2))
            {
                Console.WriteLine($"Песни {song1.Name} и {song2.Name} одинаковы.");
            }
            else
            {
                Console.WriteLine($"Песни {song1.Name} и {song2.Name} различны.");
            }
        }
    }

    class Song
    {
        public string Name { get; }
        public string Author { get; }

        public Song(string name, string author)
        {
            Name = name;
            Author = author;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"{Name} by {Author}");
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Song)obj;
            return (Name == other.Name && Author == other.Author);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Name.GetHashCode();
                hash = hash * 23 + Author.GetHashCode();
                return hash;
            }
        }
    }
}
