using PizzaMaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaMaker.Managers
{
    internal class ManageBase : IManager
    {
        private List<Base> Bases;
        private const string CLASSIC_BASE_NAME = "Классическая";

        public ManageBase(List<Base> bases)
        {
            Bases = bases;
        }

        public List<Base> GetBases()
        {
            return Bases;
        }

        public void PrintBases()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== СПИСОК ОСНОВ ===\n");
            Console.ResetColor();
            for (int i = 0; i < Bases.Count; i++)
            {
                Console.WriteLine($"{i + 1}." + Bases[i].ToString());
            }
        }

        private int GetClassicBasePrice()
        {
            Base classicBase = Bases.Find(b => b.Name == CLASSIC_BASE_NAME);
            return classicBase?.Price ?? 0;
        }

        private void FilterLessClassicBase()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== ФИЛЬТРАЦИЯ ОСНОВ ДЕШЕВЛЕ КЛАССИЧЕСКОЙ ===\n");
            Console.ResetColor();

            int classic_price = GetClassicBasePrice();
            List<Base> filtered_bases = Bases.Where(i => i.Price < classic_price).ToList();
            foreach (Base b in filtered_bases)
            {
                Console.WriteLine(" - " + b.ToString());
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Нажмите Enter чтобы продолжить...");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void FilterOverClassicBase()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== ФИЛЬТРАЦИЯ ОСНОВ ДОРОЖЕ КЛАССИЧЕСКОЙ ===\n");
            Console.ResetColor();

            int classic_price = GetClassicBasePrice();
            List<Base> filtered_bases = Bases.Where(i => i.Price > classic_price).ToList();
            foreach (Base b in filtered_bases)
            {
                Console.WriteLine(" - " + b.ToString());
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Нажмите Enter чтобы продолжить...");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void FilterByName()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== ФИЛЬТРАЦИЯ ПО НАЗВАНИЮ ===\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Введите текст для поиска в названии: ");
            Console.ResetColor();

            string search_text = Console.ReadLine().Trim().ToLower();

            if (string.IsNullOrEmpty(search_text))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== ФИЛЬТРАЦИЯ ПО НАЗВАНИЮ ===\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Текст для поиска не может быть пустым!");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            List<Base> filtered_bases = Bases
                .Where(i => i.Name.ToLower().Contains(search_text))
                .ToList();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== ФИЛЬТРАЦИЯ ПО НАЗВАНИЮ ===\n");
            Console.WriteLine($"Ингредиенты, содержащие '{search_text}' в названии:\n");
            foreach (Base b in filtered_bases)
            {
                Console.WriteLine(" - " + b.ToString());
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Нажмите Enter чтобы продолжить...");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void FilterDescendingOrderPrice()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== ФИЛЬТРАЦИЯ ПО УБЫВАНИЮ ===\n");
            Console.ResetColor();

            List<Base> filtered_bases = Bases.OrderByDescending(i => i.Price).ToList();
            foreach (Base b in filtered_bases)
            {
                Console.WriteLine(" - " + b.ToString());
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Нажмите Enter чтобы продолжить...");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void FilterAscendingOrderPrice()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== ФИЛЬТРАЦИЯ ПО ВОЗРАСТАНИЮ ===\n");
            Console.ResetColor();

            List<Base> filtered_bases = Bases.OrderBy(i => i.Price).ToList();
            foreach (Base b in filtered_bases)
            {
                Console.WriteLine(" - " + b.ToString());
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Нажмите Enter чтобы продолжить...");
            Console.ResetColor();
            Console.ReadLine();
        }

        public void FilterBases()
        {
            if (Bases.Count == 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== ФИЛЬТРАЦИЯ ОСНОВ ===");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nСписок основ пуст.");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            bool filtrating = true;
            while (filtrating)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== ФИЛЬТРАЦИЯ ОСНОВ ===");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1. По названию (содержит текст)");
                Console.WriteLine("2. По убыванию цены");
                Console.WriteLine("3. По возрастанию цены");
                Console.WriteLine("4. Основ дешевле классической");
                Console.WriteLine("5. Основ дороже классической");
                Console.WriteLine("0. Вернуться назад\n");
                Console.WriteLine("Выберите тип фильтрации и введите номер: ");
                Console.ResetColor();

                string choice = Console.ReadLine().Trim();

                switch (choice)
                {
                    case "1":
                        FilterByName();
                        filtrating = false;
                        break;
                    case "2":
                        FilterDescendingOrderPrice();
                        filtrating = false;
                        break;
                    case "3":
                        FilterAscendingOrderPrice();
                        filtrating = false;
                        break;
                    case "4":
                        FilterLessClassicBase();
                        filtrating = false;
                        break;
                    case "5":
                        FilterOverClassicBase();
                        filtrating = false;
                        break;
                    case "0":
                        return;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("=== ФИЛЬТРАЦИЯ ОСНОВ ===");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Неверный выбор!");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                }
            }
        }

        public void Adding()
        {
            string name;
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== ДОБАВЛЕНИЕ НОВОЙ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nВведите название основы для пиццы или 0 чтобы отменить добавление: ");
                Console.ResetColor();

                name = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(name))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("=== ДОБАВЛЕНИЕ НОВОЙ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nНазвание не может быть пустым.");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ResetColor();
                    Console.ReadLine();
                    continue;
                }

                if (name == "0")
                    return;

                name = char.ToUpper(name[0]) + name.Substring(1);

                if (Bases.Exists(b => b.Name == name))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("=== ДОБАВЛЕНИЕ НОВОЙ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Основа с названием {name} уже существует.\n");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ResetColor();
                    Console.ReadLine();
                    continue;
                }

                break;
            }

            int price = -2;
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== ДОБАВЛЕНИЕ НОВОЙ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nВведите стоимость основы для пиццы или -1 чтобы отменить добавление: ");
                Console.ResetColor();

                if (!int.TryParse(Console.ReadLine().Trim(), out price) || price < -1)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("=== ДОБАВЛЕНИЕ НОВОЙ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Стоимость должна быть целым положительным числом.");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ResetColor();
                    Console.ReadLine();
                    continue;
                }

                if (price == -1)
                    return;

                if (name != CLASSIC_BASE_NAME)
                {
                    int classic_price = GetClassicBasePrice();
                    int max_price = (int)(classic_price * 1.2);

                    if (price > max_price)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("=== ДОБАВЛЕНИЕ НОВОЙ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\nЦена не может быть больше {max_price} руб.");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                        Console.ResetColor();
                        Console.ReadLine();
                        continue;
                    }
                }

                break;
            }

            Base new_base = new Base
            {
                Name = name,
                Price = price
            };

            Bases.Add(new_base);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== ДОБАВЛЕНИЕ НОВОЙ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");
            Console.ResetColor();

            PrintBases();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nНовая основа для пиццы успешно добавлена!");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Нажмите Enter чтобы продолжить...");
            Console.ResetColor();
            Console.ReadLine();
        }

        public void Removing()
        {
            if (Bases.Count == 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== УДАЛЕНИЕ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nСписок основ пуст.\n");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            int base_to_remove = -1;
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== УДАЛЕНИЕ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n\n");
                Console.ResetColor();

                PrintBases();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nКакую основу хотите удалить, введите номер или 0 чтобы отменить удаление: ");
                Console.ResetColor();

                if (!int.TryParse(Console.ReadLine().Trim(), out base_to_remove) ||
                    base_to_remove < 0 || base_to_remove > Bases.Count)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("=== УДАЛЕНИЕ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nНеверный выбор номера основы!\n");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ResetColor();
                    Console.ReadLine();
                    continue;
                }

                if (base_to_remove == 0)
                    return;

                if (Bases[base_to_remove - 1].Name == CLASSIC_BASE_NAME)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("=== УДАЛЕНИЕ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nПредупреждение: Классическую основу нельзя удалить!\n");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ResetColor();
                    Console.ReadLine();
                    continue;
                }

                break;
            }

            Bases.RemoveAt(base_to_remove - 1);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== УДАЛЕНИЕ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Основа для пиццы успешно удалена!\n");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Нажмите Enter чтобы продолжить...");
            Console.ResetColor();
            Console.ReadLine();
        }

        public void Editing()
        {
            if (Bases.Count == 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== РЕДАКТИРОВАНИЕ ОСНОВЫ ===\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Список основ пуст.");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            int base_to_edit = -1;
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== РЕДАКТИРОВАНИЕ ОСНОВЫ ===\n");
                Console.ResetColor();

                PrintBases();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nКакую основу вы хотите изменить, введите номер или 0 чтобы отменить редактирование: ");
                Console.ResetColor();

                if (!int.TryParse(Console.ReadLine().Trim(), out base_to_edit) ||
                    base_to_edit < 0 || base_to_edit > Bases.Count)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("=== РЕДАКТИРОВАНИЕ ОСНОВЫ ===\n\n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nНеверный выбор номера основы!\n");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ResetColor();
                    Console.ReadLine();
                    continue;
                }

                if (base_to_edit == 0)
                    return;

                break;
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ОСНОВЫ: {Bases[base_to_edit - 1]} ===\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nЧто вы хотите изменить:\n");
            Console.WriteLine("1.Название\n");
            Console.WriteLine("2.Стоимость\n");
            Console.WriteLine("0.Отменить изменения");
            Console.WriteLine("\nВведите номер: ");
            Console.ResetColor();

            string editing_parameter = Console.ReadLine().Trim();

            switch (editing_parameter)
            {
                case "1":
                    while (true)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ОСНОВЫ: {Bases[base_to_edit - 1]} ===\n");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nВведите новое название или 0 чтобы отменить редактирование: ");
                        Console.ResetColor();

                        string new_name = Console.ReadLine().Trim();
                        if (string.IsNullOrEmpty(new_name))
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ОСНОВЫ: {Bases[base_to_edit - 1]} ===\n");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nНазвание не может быть пустым!\n");
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("Нажмите Enter чтобы продолжить...");
                            Console.ResetColor();
                            Console.ReadLine();
                            continue;
                        }

                        if (new_name == "0")
                            return;

                        new_name = char.ToUpper(new_name[0]) + new_name.Substring(1);

                        if (Bases.Exists(b => b.Name == new_name))
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ОСНОВЫ: {Bases[base_to_edit - 1]} ===\n");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"\nОснова с названием {new_name} уже существует.\n");
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("Нажмите Enter чтобы продолжить...");
                            Console.ResetColor();
                            Console.ReadLine();
                            continue;
                        }

                        Bases[base_to_edit - 1].Name = new_name;
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ОСНОВЫ: {Bases[base_to_edit - 1]} ===\n");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nНазвание успешно изменено!\n");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Новое название: {new_name}.");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                    }
                    break;

                case "2":
                    while (true)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ОСНОВЫ: {Bases[base_to_edit - 1]} ===\n");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Введите стоимость основы для пиццы или -1 чтобы отменить редактирование:\n");
                        Console.ResetColor();

                        int new_price = -2;
                        if (!int.TryParse(Console.ReadLine().Trim(), out new_price) || new_price < -1)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ОСНОВЫ: {Bases[base_to_edit - 1]} ===\n");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nСтоимость должна быть целым положительным числом.\n");
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("Нажмите Enter чтобы продолжить...");
                            Console.ResetColor();
                            Console.ReadLine();
                            continue;
                        }

                        if (new_price == -1)
                            return;

                        if (Bases[base_to_edit - 1].Name != CLASSIC_BASE_NAME)
                        {
                            int classic_price = GetClassicBasePrice();
                            int max_price = (int)(classic_price * 1.2);

                            if (new_price > max_price)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ОСНОВЫ: {Bases[base_to_edit - 1]} ===\n");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"\nЦена не может быть больше {max_price} руб.\n");
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                Console.ResetColor();
                                Console.ReadLine();
                                continue;
                            }
                        }

                        Bases[base_to_edit - 1].Price = new_price;
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ОСНОВЫ: {Bases[base_to_edit - 1]} ===\n");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nСтоимость успешно изменена!\n");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                    }
                    break;

                case "0":
                    break;

                default:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ОСНОВЫ: {Bases[base_to_edit - 1]} ===\n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Такой команды не существует!");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ResetColor();
                    Console.ReadLine();
                    break;
            }
        }
    }
}
