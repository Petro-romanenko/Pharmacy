using System;
using System.Text;

namespace Pharmacy
{
    internal class Program
    {
        static void Main(string[] args)// Головний цикл меню застосунку Аптека

        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool running = true;
            while (running)
            {
                Console.WriteLine("=== АПТЕКА ===");
                Console.WriteLine("1 - довідка про програму");
                Console.WriteLine("2 - таблиця вартості замовлення (табулювання)");
                Console.WriteLine("3 - звіт продажів по відділах і днях (таблиця з двома входами)");
                Console.WriteLine("0 - вихід");
                Console.Write("\nВаш вибір: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowAbout();
                        Pause();
                        break;
                    case "2":
                        ShowCostTable();
                        Pause();
                        break;
                    case "3":
                        ShowSalesTable();
                        Pause();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("Роботу завершено. До побачення!");
                        break;
                    default:
                        Console.WriteLine("Помилка: немає такого пункту меню. Спробуйте ще раз.");
                        Pause();
                        break;
                }
            }
        }
        static void Pause()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу для повернення в меню...");
            Console.ReadKey(true);
            Console.Clear();
        }
        static void ShowAbout()
        {
            Console.WriteLine("--- ДОВІДКА ---");
            Console.WriteLine("Тема проєкту: Аптека (облік лікарських засобів)");
            Console.WriteLine("Головна сутність: Препарат (ліки)");
            Console.WriteLine("Підсумкова величина: Загальна вартість наявних препаратів на складі (грн)");
            Console.WriteLine("\nПоля запису (7 полів):");
            Console.WriteLine("1. Код препарату (int)");
            Console.WriteLine("2. Назва препарату (string)");
            Console.WriteLine("3. Категорія (string)");
            Console.WriteLine("4. Виробник (string)");
            Console.WriteLine("5. Ціна за упаковку, грн (double)");
            Console.WriteLine("6. Кількість упаковок (int)");
            Console.WriteLine("7. Термін придатності (DateTime)");
        }
        static void ShowCostTable()
        {
            Console.WriteLine("--- ТАБЛИЦЯ ВАРТОСТІ ЗАМОВЛЕННЯ ---");
            Console.WriteLine("Формула: Вартість = Вага * 120 - 5 * Sqrt(Вага)\n");

            double from = 0.5;
            double to = 3.0;
            double step = 0.25;

            Console.WriteLine($"{"Вага (кг)",12} | {"Вартість (грн)",16}");
            Console.WriteLine(new string('-', 32));
            for (double weight = from; weight <= to + 1e-9; weight += step)
            {
                double cost = weight * 120.0 - 5.0 * Math.Sqrt(weight);
                Console.WriteLine($"{weight,12:F2} | {cost,16:F2}");
            }
        }
        static void ShowSalesTable()
        {
            Console.WriteLine("--- ЗВІТ ПРОДАЖІВ (УПАКОВОК) ---");
            Console.Write("Введіть кількість відділів (рядків, від 1 до 15): ");
            if (!int.TryParse(Console.ReadLine(), out int rows) || rows <= 0 || rows > 15)
            {
                Console.WriteLine("Помилка: введено некоректну кількість рядків.");
                return;
            }
            Console.Write("Введіть кількість днів (стовпців, від 1 до 10): ");
            if (!int.TryParse(Console.ReadLine(), out int cols) || cols <= 0 || cols > 10)
            {
                Console.WriteLine("Помилка: введено некоректну кількість стовпців.");
                return;
            }

            Console.WriteLine();
            int[] colTotals = new int[cols];
            int grandTotal = 0;
            Console.Write($"{"Відділ",10} |");
            for (int col = 1; col <= cols; col++)
            {
                Console.Write($"{$"День {col}",8}");
            }
            Console.WriteLine($" | {"Разом",8}");
            Console.WriteLine(new string('-', 12 + cols * 8 + 11));
            for (int row = 1; row <= rows; row++)
            {
                Console.Write($"{$"Відділ {row}",10} |");
                int rowTotal = 0;

                for (int col = 1; col <= cols; col++)
                {
                    int sold = (row * 3 + col * 2) % 15 + 5;

                    rowTotal += sold;
                    colTotals[col - 1] += sold;

                    Console.Write($"{sold,8}");
                }

                grandTotal += rowTotal;
                Console.WriteLine($" | {rowTotal,8}");
            }
            Console.WriteLine(new string('-', 12 + cols * 8 + 11));
            Console.Write($"{"Разом",10} |");
            for (int col = 0; col < cols; col++)
            {
                Console.Write($"{colTotals[col],8}");
            }
            Console.WriteLine($" | {grandTotal,8}");
        }
    }
}
