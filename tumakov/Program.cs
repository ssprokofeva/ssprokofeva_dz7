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
             
            private string _accountNumber;
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
                    _balance += amount;
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

        class Program 
        {
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
            /// упражнение 8.1
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
            /// упражнение 8.2
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
            /// упражнение 8.3
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
            /// упражнение 8.4
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
        }
        /// <summary>
        /// Домашнее задание 8.1
        /// </summary>
        static void Task5()
        { }
         
        public static void SearchMail(ref string s)
        {
            int index = s.IndexOf('#');
            if (index != -1)
            {
                s = s.Substring(index + 1);
            }
        }

        static void Main(string[] args)
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
                            SearchMail(ref line);  
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
        /// <summary>
        /// Домашнее задание 8.2
        /// </summary>
        static void Task6()
        {
            List<Song> songs = new List<Song>();

             
            Song song1 = new Song();
            song1.SetName("Jingle Bells");
            song1.SetAuthor("James Lord Pierpont");

            Song song2 = new Song();
            song2.SetName("Last Christmas");
            song2.SetAuthor("Wham!");

            Song song3 = new Song();
            song3.SetName("All I Want for Christmas Is You");
            song3.SetAuthor("Mariah Carey");

            Song song4 = new Song();
            song4.SetName("Santa Claus Is Coming to Town");
            song4.SetAuthor("John Frederick Coots and Haven Gillespie");

             
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
                Console.WriteLine($"Песни {song1.Title()} и {song2.Title()} одинаковы.");
            }
            else
            {
                Console.WriteLine($"Песни {song1.Title()} и {song2.Title()} различны.");
            }
        }
    }

    class Song
    {
        private string _name;
        private string _author;

        
        public Song Prev { get; set; }

        
        public void SetName(string name)
        {
            this._name = name;
        }

         
        public void SetAuthor(string author)
        {
            this._author = author;
        }

        
        public void PrintInfo()
        {
            Console.WriteLine($"{_name} by {_author}");
        }

         
        public string Title()
        {
            return $"{_name} by {_author}";
        }

         
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Song)obj;
            return (_name == other._name && _author == other._author);
        }

         
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + _name?.GetHashCode() ?? 0;
            hash = hash * 23 + _author?.GetHashCode() ?? 0;
            return hash;
        }
    }
}
