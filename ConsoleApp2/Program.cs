#nullable disable
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ZooManagementApp
{
    class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public double FoodKg { get; set; }

        public Animal(int id, string name, string species, double foodKg)
        {
            Id = id;
            Name = name;
            Species = species;
            FoodKg = foodKg;
        }
    }

    class Program
    {
        static List<Animal> zooAnimals = new List<Animal>();
        static int nextId = 1;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Система Управління Зоопарком (Lab 4)";

            SeedData();

            bool isRunning = true;

            while (isRunning)
            {
                ShowMenu();
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowAllAnimals();
                        break;
                    case "2":
                        AddNewAnimal();
                        break;
                    case "3":
                        SearchAnimal();
                        break;
                    case "4":
                        DeleteAnimal();
                        break;
                    case "5":
                        SortMenu();
                        break;
                    case "6":
                        ShowStatistics();
                        break;
                    case "0":
                        Console.WriteLine("Зміна завершена. До побачення!");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Невірний вибір! Спробуйте ще раз.");
                        PressAnyKey();
                        break;
                }
            }
        }

        static void SeedData()
        {
            zooAnimals.Add(new Animal(nextId++, "Сімба", "Лев", 7.5));
            zooAnimals.Add(new Animal(nextId++, "Марті", "Зебра", 5.0));
            zooAnimals.Add(new Animal(nextId++, "Глорія", "Бегемот", 40.0));
            zooAnimals.Add(new Animal(nextId++, "Мелман", "Жираф", 30.0));
            zooAnimals.Add(new Animal(nextId++, "Шкіпер", "Пінгвін", 0.5));
            zooAnimals.Add(new Animal(nextId++, "Кінг Джуліан", "Лемур", 0.3));
        }

        static void ShowMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== 🦁 ZOO MANAGEMENT SYSTEM 🐘 ===");
            Console.ResetColor();
            Console.WriteLine("1. 📋 Список тварин");
            Console.WriteLine("2. ➕ Додати тварину");
            Console.WriteLine("3. 🔍 Пошук (за кличкою або видом)");
            Console.WriteLine("4. ❌ Видалити тварину");
            Console.WriteLine("5. 📊 Сортування (за кількістю їжі)");
            Console.WriteLine("6. 📈 Статистика (витрати корму)");
            Console.WriteLine("0. 🚪 Вихід");
            Console.Write("\nВаш вибір > ");
        }

        static void ShowAllAnimals()
        {
            Console.Clear();
            Console.WriteLine("=====================================================================");
            Console.WriteLine("| {0,-3} | {1,-15} | {2,-15} | {3,15} |", "ID", "Кличка", "Вид", "Корм (кг/день)");
            Console.WriteLine("=====================================================================");

            foreach (Animal a in zooAnimals)
            {
                Console.WriteLine("| {0,-3} | {1,-15} | {2,-15} | {3,15:F2} |", a.Id, a.Name, a.Species, a.FoodKg);
            }
            Console.WriteLine("=====================================================================");
            PressAnyKey();
        }

        static void AddNewAnimal()
        {
            Console.Clear();
            try
            {
                Console.WriteLine("--- НОВИЙ МЕШКАНЕЦЬ ---");
                Console.Write("Введіть кличку: ");
                string name = Console.ReadLine();

                Console.Write("Введіть вид (напр. Тигр): ");
                string species = Console.ReadLine();

                Console.Write("Скільки їсть в день (кг): ");
                double food = double.Parse(Console.ReadLine());

                if (food < 0) throw new Exception("Тварина не може їсти від'ємну кількість їжі!");

                zooAnimals.Add(new Animal(nextId++, name, species, food));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Тварину успішно додано до вольєру!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Помилка: {ex.Message}");
                Console.ResetColor();
            }
            PressAnyKey();
        }

        static void SearchAnimal()
        {
            Console.Clear();
            Console.Write("Введіть назву або вид для пошуку: ");
            string query = Console.ReadLine().ToLower();
            bool found = false;

            Console.WriteLine("\nРезультати:");
            foreach (Animal a in zooAnimals)
            {
                if (a.Name.ToLower().Contains(query) || a.Species.ToLower().Contains(query))
                {
                    Console.WriteLine($" -> [{a.Species}] {a.Name} — їсть {a.FoodKg} кг");
                    found = true;
                }
            }
            if (!found) Console.WriteLine("На жаль, нікого не знайдено.");
            PressAnyKey();
        }

        static void DeleteAnimal()
        {
            Console.Clear();
            ShowAllAnimals();
            Console.Write("\nВведіть ID тварини, яку переводимо в інший зоопарк: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                int index = zooAnimals.FindIndex(a => a.Id == id);
                if (index != -1)
                {
                    zooAnimals.RemoveAt(index);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Тварину видалено зі списку.");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("ID не знайдено.");
                }
            }
            else
            {
                Console.WriteLine("Некоректний номер.");
            }
            PressAnyKey();
        }

        static void SortMenu()
        {
            Console.Clear();
            Console.WriteLine("--- СОРТУВАННЯ ЗА АПЕТИТОМ (кг їжі) ---");
            Console.WriteLine("1. Bubble Sort (Ручний метод - Бульбашка)");
            Console.WriteLine("2. Built-in Sort (Стандартний C#)");
            Console.Write("Вибір > ");
            string choice = Console.ReadLine();

            Stopwatch sw = new Stopwatch();

            if (choice == "1")
            {
                sw.Start();
                for (int i = 0; i < zooAnimals.Count - 1; i++)
                {
                    for (int j = 0; j < zooAnimals.Count - i - 1; j++)
                    {
                        if (zooAnimals[j].FoodKg > zooAnimals[j + 1].FoodKg)
                        {
                            var temp = zooAnimals[j];
                            zooAnimals[j] = zooAnimals[j + 1];
                            zooAnimals[j + 1] = temp;
                        }
                    }
                }
                sw.Stop();
                Console.WriteLine($"\nСортування бульбашкою виконано за {sw.ElapsedTicks} тіків.");
            }
            else if (choice == "2")
            {
                sw.Start();
                zooAnimals.Sort((x, y) => x.FoodKg.CompareTo(y.FoodKg));
                sw.Stop();
                Console.WriteLine($"\nВбудоване сортування виконано за {sw.ElapsedTicks} тіків.");
            }

            ShowAllAnimals();
        }

        static void ShowStatistics()
        {
            Console.Clear();
            if (zooAnimals.Count == 0)
            {
                Console.WriteLine("Зоопарк порожній.");
                PressAnyKey();
                return;
            }

            double totalFood = 0;
            double minFood = double.MaxValue;
            double maxFood = double.MinValue;
            Animal eatsLeast = zooAnimals[0];
            Animal eatsMost = zooAnimals[0];

            foreach (var a in zooAnimals)
            {
                totalFood += a.FoodKg;

                if (a.FoodKg < minFood)
                {
                    minFood = a.FoodKg;
                    eatsLeast = a;
                }
                if (a.FoodKg > maxFood)
                {
                    maxFood = a.FoodKg;
                    eatsMost = a;
                }
            }

            Console.WriteLine("=== 📊 СТАТИСТИКА ХАРЧУВАННЯ ===");
            Console.WriteLine($"Всього тварин: {zooAnimals.Count}");
            Console.WriteLine($"Загалом потрібно корму: {totalFood} кг/день");
            Console.WriteLine($"Середній апетит: {Math.Round(totalFood / zooAnimals.Count, 2)} кг");
            Console.WriteLine($"--------------------------------");
            Console.WriteLine($"Найбільше їсть: {eatsMost.Species} {eatsMost.Name} ({maxFood} кг)");
            Console.WriteLine($"Найменше їсть: {eatsLeast.Species} {eatsLeast.Name} ({minFood} кг)");

            PressAnyKey();
        }

        static void PressAnyKey()
        {
            Console.WriteLine("\nНатисніть Enter щоб продовжити...");
            Console.ReadLine();
        }
    }
}