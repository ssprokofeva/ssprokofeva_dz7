using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ssprokofeva_dz7
{
    namespace Program 
    {
        // Базовый класс сотрудника
        abstract class Employee
        {
            protected string Name;
            protected List<Employee> Subordinates;

            public Employee(string name)
            {
                Name = name;
                Subordinates = new List<Employee>();
            }

            public virtual bool CanReceiveTask(Employee from, string task)
            {
                return false;
            }

            public void AddSubordinate(Employee subordinate)
            {
                Subordinates.Add(subordinate);
            }

            public void AssignTask(Employee to, string task)
            {
                if (to.CanReceiveTask(this, task))
                {
                    Console.WriteLine($"{this.Name} дает задачу \"{task}\" сотруднику {to.Name}. Задача принята.");
                }
                else
                {
                    Console.WriteLine($"{this.Name} пытается дать задачу \"{task}\" сотруднику {to.Name}, но сотрудник отказывается.");
                }
            }
        }

        // Генеральный директор
        class CEO : Employee
        {
            public CEO(string name) : base(name) { }

            public override bool CanReceiveTask(Employee from, string task)
            {
                return true;
            }
        }

        // Финансовый директор
        class FinancialDirector : Employee
        {
            public FinancialDirector(string name) : base(name) { }

            public override bool CanReceiveTask(Employee from, string task)
            {
                return from is CEO;
            }
        }

        // Директор по автоматизации
        class AutomationDirector : Employee
        {
            public AutomationDirector(string name) : base(name) { }

            public override bool CanReceiveTask(Employee from, string task)
            {
                return from is CEO;
            }
        }

        // Начальник отдела информационных технологий
        class ITDepartmentHead : Employee
        {
            public ITDepartmentHead(string name) : base(name) { }

            public override bool CanReceiveTask(Employee from, string task)
            {
                return from is AutomationDirector || from is CEO;
            }
        }

        // Заместитель начальника отдела информационных технологий
        class DeputyITDepartmentHead : Employee
        {
            public DeputyITDepartmentHead(string name) : base(name) { }

            public override bool CanReceiveTask(Employee from, string task)
            {
                return from is ITDepartmentHead || from is CEO;
            }
        }

        // Главный бухгалтер
        class ChiefAccountant : Employee
        {
            public ChiefAccountant(string name) : base(name) { }

            public override bool CanReceiveTask(Employee from, string task)
            {
                return from is FinancialDirector || from is CEO;
            }
        }

        // Разработчик
        class Developer : Employee
        {
            public Developer(string name) : base(name) { }

            public override bool CanReceiveTask(Employee from, string task)
            {
                return from is ITDepartmentHead || from is DeputyITDepartmentHead || from is CEO;
            }
        }

        // Системщик
        class SysAdmin : Employee
        {
            public SysAdmin(string name) : base(name) { }

            public override bool CanReceiveTask(Employee from, string task)
            {
                return from is ITDepartmentHead || from is DeputyITDepartmentHead || from is CEO;
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                // Создаем иерархию компании
                CEO timur = new CEO("Тимур");
                FinancialDirector rashid = new FinancialDirector("Рашид");
                AutomationDirector oIlkham = new AutomationDirector("О Ильхам");
                ITDepartmentHead orkadiy = new ITDepartmentHead("Оркадий");
                DeputyITDepartmentHead volodja = new DeputyITDepartmentHead("Володя");
                ChiefAccountant lukas = new ChiefAccountant("Лукас");
                Developer dinara = new Developer("Динара");
                Developer marat = new Developer("Марат");
                Developer ildar = new Developer("Ильдар");
                Developer anton = new Developer("Антон");
                SysAdmin ilshat = new SysAdmin("Ильшат");
                SysAdmin ivanych = new SysAdmin("Иваныч");
                SysAdmin ilja = new SysAdmin("Илья");
                SysAdmin vita = new SysAdmin("Витя");
                SysAdmin zhenja = new SysAdmin("Женя");

                // Устанавливаем связи подчинения
                timur.AddSubordinate(rashid);
                timur.AddSubordinate(oIlkham);
                rashid.AddSubordinate(lukas);
                oIlkham.AddSubordinate(orkadiy);
                orkadiy.AddSubordinate(volodja);
                volodja.AddSubordinate(dinara);
                volodja.AddSubordinate(marat);
                volodja.AddSubordinate(ildar);
                volodja.AddSubordinate(anton);
                volodja.AddSubordinate(ilshat);
                volodja.AddSubordinate(ivanych);
                volodja.AddSubordinate(ilja);
                volodja.AddSubordinate(vita);
                volodja.AddSubordinate(zhenja);

                // Передача задач
                timur.AssignTask(rashid, "Автоматизация бухгалтерского учета");
                rashid.AssignTask(lukas, "Проверить отчетность за прошлый квартал");
                oIlkham.AssignTask(orkadiy, "Разработать новую систему мониторинга сети");
                orkadiy.AssignTask(volodja, "Провести тестирование новой системы");
                volodja.AssignTask(dinara, "Исправить баг в приложении X");
                volodja.AssignTask(ilshat, "Настроить новый сервер");

                Console.WriteLine("Нажмите любую клавишу для завершения...");
                Console.ReadKey();
            }
        }
    }
}