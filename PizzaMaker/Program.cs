using System;
using System.Collections.Generic;

namespace PizzaMaker
{
    class Ingredient
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return $"{Name} | цена: {Price} руб.\n";
        }
    }

    class Base
    {
        public string Name { get; set; }
        private static int _classic_base_price;
        private int _price;
        public int Price
        {
            get { return _price; }
            set
            {
                if (Name == "Классическая" && _classic_base_price == 0)
                {
                    _classic_base_price = value;
                    _price = value;
                }
                else if (_classic_base_price > 0)
                {
                    int max_price = (int)(_classic_base_price * 1.2);
                    if (value <= max_price)
                    {
                        _price = value;
                    }
                    else
                    {
                        Console.WriteLine($"Стоимость не может превышать {max_price} руб.");
                    }
                }
                else
                {
                    _price = value;
                }
            }
        }

        public override string ToString()
        {
            return $"{Name} | цена: {Price} руб.\n";
        }
    }

    class Pizza
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public Base PizzaBase { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public override string ToString()
        {
            return $"{Name} | цена: {Price} руб.\n";
        }
    }

    class ManageIngredients
    {
        private List<Ingredient> Ingredients;

        public ManageIngredients(List<Ingredient> ingredients)
        {
            Ingredients = ingredients;
        }

        public List<Ingredient> GetIngredients()
        {
            return Ingredients;
        }

        public void PrintIngredients()
        {
            for (int i = 0; i < Ingredients.Count; i++)
            {
                Console.WriteLine($"{i + 1}." + Ingredients[i].ToString());
            }
        }

        public void Adding()
        {
            string name;
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== СОЗДАНИЕ НОВОГО ИНГРЕДИЕНТА ===\n");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nВведите название ингредиента или 0 чтобы отменить добавление: ");
                Console.ResetColor();

                name = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(name))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("=== СОЗДАНИЕ НОВОГО ИНГРЕДИЕНТА ===\n");

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Название не может быть пустым!");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\nНажмите Enter чтобы продолжить...");
                    Console.ResetColor();
                    Console.ReadLine();
                    continue;
                }

                if (name == "0")
                    return;

                name = char.ToUpper(name[0]) + name.Substring(1);

                if (Ingredients.Exists(i => i.Name == name))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("=== СОЗДАНИЕ НОВОГО ИНГРЕДИЕНТА ===\n");

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nИнгредиент с названием {name} уже существует.");

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
                Console.WriteLine("=== СОЗДАНИЕ НОВОГО ИНГРЕДИЕНТА ===\n");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nВведите стоимость ингредиента или 0 чтобы прекратить добавление: ");
                Console.ResetColor();

                if (!int.TryParse(Console.ReadLine().Trim(), out price) || price < -1)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("=== СОЗДАНИЕ НОВОГО ИНГРЕДИЕНТА ===\n");

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nСтоимость должна быть целым положительным числом.\n");

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ResetColor();
                    Console.ReadLine();
                    continue;
                }

                if (price == -1)
                    return;

                break;
            }

            Ingredient new_ingridient = new Ingredient
            {
                Name = name,
                Price = price
            };

            Ingredients.Add(new_ingridient);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== СОЗДАНИЕ НОВОГО ИНГРЕДИЕНТА ===\n");

            PrintIngredients();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nНовый Ингредиент успешно добавлен!\n");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nНажмите Enter чтобы продолжить...");
            Console.ResetColor();
            Console.ReadLine();
        }

        public void Removing()
        {
            if (Ingredients.Count == 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== УДАЛЕНИЕ ИНГРЕДИЕНТА ===\n");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nСписок ингредиентов пуст.");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\nНажмите Enter чтобы продолжить...");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== УДАЛЕНИЕ ИНГРЕДИЕНТА ===\n");

                PrintIngredients();

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\nКакой ингредиент хотите удалить, введите номер или 0 чтобы прекратить удаление: ");
                Console.ResetColor();

                int ingredient_to_remove = -1;

                if (!int.TryParse(Console.ReadLine().Trim(), out ingredient_to_remove) ||
                    ingredient_to_remove < 1 || ingredient_to_remove > Ingredients.Count)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("=== УДАЛЕНИЕ ИНГРЕДИЕНТА ===\n");

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nНеверный выбор номера ингредиента!");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\nНажмите Enter чтобы продолжить...");
                    Console.ResetColor();
                    Console.ReadLine();
                    continue;
                }

                if (ingredient_to_remove == 0)
                    return;

                Ingredients.RemoveAt(ingredient_to_remove - 1);

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== УДАЛЕНИЕ ИНГРЕДИЕНТА ===\n");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ингредиент успешно удалена!");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ResetColor();
                Console.ReadLine();

                break;
            }
        }

        public void Editing()
        {
            if (Ingredients.Count == 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА ===\n");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nСписок ингредиентов пуст.\n");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            int ingredient_to_edit = -1;
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА ===\n\n");

                PrintIngredients();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nКакой ингредиент вы хотите изменить, введите номер или 0 чтобы отменить редактирование: ");
                Console.ResetColor();

                if (!int.TryParse(Console.ReadLine().Trim(), out ingredient_to_edit) ||
                    ingredient_to_edit < 1 || ingredient_to_edit > Ingredients.Count)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА ===\n");

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nТакого ингредиента нет.\n");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ResetColor();
                    Console.ReadLine();
                    continue;
                }

                if (ingredient_to_edit == 0)
                    return;

                break;
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА: {Ingredients[ingredient_to_edit - 1]} ===\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nЧто вы хотите изменить:\n");
            Console.WriteLine("1.Название\n");
            Console.WriteLine("2.Стоимость\n");
            Console.WriteLine("0.Отменить изменения\n");
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
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА: {Ingredients[ingredient_to_edit - 1]} ===\n");

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nВведите новое название или 0 чтобы отменить редактирование: ");
                        Console.ResetColor();

                        string new_name = Console.ReadLine().Trim();

                        if (string.IsNullOrEmpty(new_name))
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА: {Ingredients[ingredient_to_edit - 1]} ===\n");

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nНазвание не может быть пустым.");
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("Нажмите Enter чтобы продолжить...");
                            Console.ResetColor();
                            Console.ReadLine();
                            continue;
                        }

                        if (new_name == "0") return;

                        new_name = char.ToUpper(new_name[0]) + new_name.Substring(1);

                        if (Ingredients.Exists(b => b.Name == new_name))
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА: {Ingredients[ingredient_to_edit - 1]} ===\n");

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Ингредиент с названием {new_name} уже существует.\n");

                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("Нажмите Enter чтобы продолжить...");
                            Console.ResetColor();
                            Console.ReadLine();
                            continue;
                        }

                        Ingredients[ingredient_to_edit - 1].Name = new_name;

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА: {Ingredients[ingredient_to_edit - 1]} ===\n");

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nНазвание успешно изменено!");
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
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА: {Ingredients[ingredient_to_edit - 1]} ===\n");

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nВведите стоимость ингредиента:\n");
                        Console.ResetColor();

                        int new_price = -2;
                        if (!int.TryParse(Console.ReadLine().Trim(), out new_price) || new_price < -2)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА: {Ingredients[ingredient_to_edit - 1]} ===\n");

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nСтоимость должна быть целым положительным числом.");
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("Нажмите Enter чтобы продолжить...");
                            Console.ResetColor();
                            Console.ReadLine();
                            continue;
                        }

                        if (new_price == -1)
                            return;

                        Ingredients[ingredient_to_edit - 1].Price = new_price;

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА: {Ingredients[ingredient_to_edit - 1]} ===\n");

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nСтоимость успешно изменена!");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Новая стоимость: {new_price}.");
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
                    Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА: {Ingredients[ingredient_to_edit - 1]} ===\n");

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Такой команды не существует.");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ResetColor();
                    Console.ReadLine();
                    break;
            }
        }
    }

    class ManageBase
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
            for (int i = 0; i < Bases.Count; i++)
            {
                Console.Write($"{i + 1}." + Bases[i].ToString());
            }
        }

        private int GetClassicBasePrice()
        {
            Base classicBase = Bases.Find(b => b.Name == CLASSIC_BASE_NAME);
            return classicBase?.Price ?? 0;
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
                    base_to_remove < 1 || base_to_remove > Bases.Count)
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
                    base_to_edit < 1 || base_to_edit > Bases.Count)
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
                        Console.WriteLine("Введите стоимость основы для пиццы или 0 чтобы отменить редактирование:\n");
                        Console.ResetColor();

                        int new_price = -1;
                        if (!int.TryParse(Console.ReadLine().Trim(), out new_price) || new_price < 0)
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

                        if (new_price == 0)
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

    class ManagePizza
    {
        private List<Pizza> Pizzas;
        private List<Ingredient> Available_ingredients;
        private List<Base> Available_bases;

        public ManagePizza(List<Ingredient> ingredients, List<Base> bases)
        {
            Pizzas = new List<Pizza>();
            Available_bases = bases;
            Available_ingredients = ingredients;
        }

        public void PrintPizzas()
        {
            for (int i = 0; i < Pizzas.Count; i++)
            {
                Console.WriteLine($"{i + 1}." + Pizzas[i].ToString());
            }
        }

        private void PrintIngredients(Pizza pizza)
        {
            for (int i = 0; i < pizza.Ingredients.Count; i++)
            {
                Console.WriteLine($"{i + 1}.{pizza.Ingredients[i].Name}");
            }
        }

        public void PrintPizzaStructure(Pizza pizza)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"=== СОСТАВ ПИЦЦЫ: {pizza.Name} ===\n");
            Console.ResetColor();

            Console.WriteLine($"Используемая основа: {pizza.PizzaBase} | цена: {pizza.PizzaBase.Price}.\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Используемые ингредиенты:\n");
            Console.ResetColor();

            PrintIngredients(pizza);
        }

        public void PrintAvailableBases()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Доступные основы:\n");
            Console.ResetColor();
            for (int i = 0; i < Available_bases.Count; i++)
            {
                Console.WriteLine($"{i + 1}." + Available_bases[i].ToString());
            }
        }

        public void PrintAvailableIngredients()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Доступные ингредиенты:\n");
            Console.ResetColor();
            for (int i = 0; i < Available_ingredients.Count; i++)
            {
                Console.WriteLine($"{i + 1}." + Available_ingredients[i].ToString());
            }
        }

        public int CalculatingPizzaPrice(Base selected_base, List<Ingredient> selected_ingredients)
        {
            int total_sum = selected_base.Price;
            foreach (Ingredient ingr in selected_ingredients)
            {
                total_sum += ingr.Price;
            }

            return total_sum;
        }

        public void Adding()
        {
            if (Available_bases.Count == 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== СОЗДАНИЕ НОВОЙ ПИЦЦЫ ===\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nНевозможно создать пиццу без основы! Сначала добавьте основу для пиццы.");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ResetColor();

                Console.ReadLine();
                return;
            }

            string name;
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== СОЗДАНИЕ НОВОЙ ПИЦЦЫ ===\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Введите название пиццы или 0 чтобы отменить создание: ");
                Console.ResetColor();

                name = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(name))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("=== СОЗДАНИЕ НОВОЙ ПИЦЦЫ ===\n");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Название не может быть пустым!\n");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ResetColor();

                    Console.ReadLine();
                    continue;
                }

                if (name == "0")
                    return;

                name = char.ToUpper(name[0]) + name.Substring(1);

                if (Pizzas.Exists(p => p.Name == name))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("=== СОЗДАНИЕ НОВОЙ ПИЦЦЫ ===\n");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Пицца с названием {name} уже существует");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ResetColor();

                    Console.ReadLine();
                    continue;
                }

                break;
            }

            int base_index;
            Base selected_base;
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n\n");
                Console.ResetColor();

                PrintAvailableBases();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nВыберите основу для пиццы, введите номер или 0 чтобы отменить создание: ");
                Console.ResetColor();

                if (!int.TryParse(Console.ReadLine(), out base_index) || base_index < 1 || base_index > Available_bases.Count)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nНеверный выбор основы!\n");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ResetColor();

                    Console.ReadLine();
                    continue;
                }

                if (base_index == 0)
                    return;

                selected_base = Available_bases[base_index - 1];
                break;
            }

            List<Ingredient> selected_ingredients = new List<Ingredient>();
            bool adding = true;
            while (adding)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                Console.ResetColor();

                Console.WriteLine($"\nВыбраная основа: {selected_base.Name}\n");
                PrintAvailableIngredients();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nВыберите ингредиент для добавления, введите номер или 0 для завершения: ");
                Console.ResetColor();

                int ingredient_index;
                if (!int.TryParse(Console.ReadLine(), out ingredient_index) || ingredient_index < 0 || ingredient_index > Available_ingredients.Count)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nНеверный выбор ингредиента!\n");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ResetColor();

                    Console.ReadLine();
                    continue;
                }

                if (ingredient_index == 0)
                {
                    adding = false;
                    break;
                }

                Ingredient selected_ingredient = Available_ingredients[ingredient_index - 1];

                if (selected_ingredients.Exists(i => i.Name == selected_ingredient.Name))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nЭтот ингредиент уже добавлен!\n");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ResetColor();

                    Console.ReadLine();
                    continue;
                }

                selected_ingredients.Add(selected_ingredient);

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Ингредиент {selected_ingredient.Name} успешно добавлен!\n");
                Console.ResetColor();

                int current_price = CalculatingPizzaPrice(selected_base, selected_ingredients);
                Console.WriteLine($"Текущая стоимость пиццы: {current_price} руб.");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Нажмите Enter для продолжения...");
                Console.ResetColor();
                Console.ReadLine();
            }

            int total_price = CalculatingPizzaPrice(selected_base, selected_ingredients);

            Pizza new_pizza = new Pizza
            {
                Name = name,
                Price = total_price,
                PizzaBase = selected_base,
                Ingredients = selected_ingredients
            };

            Pizzas.Add(new_pizza);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
            Console.ResetColor();

            Console.WriteLine($"Основа: {selected_base.Name}\n");
            Console.WriteLine("Добавленные ингредиенты:\n");
            foreach (Ingredient ingr in selected_ingredients)
            {
                Console.WriteLine($" - {ingr.Name} | цена: {ingr.Price} руб.\n");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Новая пицца успешно создана!\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ResetColor();

            Console.ReadLine();
        }

        public void Removing()
        {
            if (Pizzas.Count == 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== УДАЛЕНИЕ ПИЦЦЫ ===\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nСписок пицц пуст.\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ResetColor();

                Console.ReadLine();
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== УДАЛЕНИЕ ПИЦЦЫ ===\n\n");
                Console.ResetColor();

                PrintPizzas();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nВведите номер пиццы, которую хотите удалить или 0 чтобы отменить удаление: ");
                Console.ResetColor();

                int pizza_to_delete = -1;
                if (!int.TryParse(Console.ReadLine().Trim(), out pizza_to_delete) ||
                    pizza_to_delete < 0 || pizza_to_delete > Pizzas.Count)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("=== УДАЛЕНИЕ ПИЦЦЫ ===\n");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nНеверный выбор пиццы!\n");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ResetColor();

                    Console.ReadLine();
                    continue;
                }

                if (pizza_to_delete == 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("=== УДАЛЕНИЕ ПИЦЦЫ ===\n");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\nУдаление отменено.\n");
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ResetColor();

                    Console.ReadLine();
                    return;
                }

                Pizzas.RemoveAt(pizza_to_delete - 1);

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== УДАЛЕНИЕ ПИЦЦЫ ===\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nПицца успешно удалена!\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ResetColor();

                Console.ReadLine();
                return;
            }
        }

        public void Editing()
        {
            if (Pizzas.Count == 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== РЕДАКТИРОВАНИЕ ПИЦЦЫ ===\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nСписок пицц пуст.");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ResetColor();

                Console.ReadLine();
                return;
            }

            int pizza_to_edit = -1;
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== РЕДАКТИРОВАНИЕ ПИЦЦЫ ===\n\n");
                Console.ResetColor();

                PrintPizzas();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nВведите номер пиццы, которую хотите изменить или 0 чтобы отменить редактирование: ");
                Console.ResetColor();

                if (!int.TryParse(Console.ReadLine().Trim(), out pizza_to_edit) ||
                    pizza_to_edit < 0 || pizza_to_edit > Pizzas.Count)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("=== РЕДАКТИРОВАНИЕ ПИЦЦЫ ===\n");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nНеверный выбор пиццы!\n");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ResetColor();

                    Console.ReadLine();
                    continue;
                }

                if (pizza_to_edit == 0)
                    return;

                break;
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nЧто вы хотите изменить:\n");
            Console.WriteLine("1.Название");
            Console.WriteLine("2.Состав");
            Console.WriteLine("0.Отменить изменения");
            Console.WriteLine("\nВведите номер: ");
            Console.ResetColor();

            string editing_parameter1 = Console.ReadLine().Trim();

            switch (editing_parameter1)
            {
                case "1":
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                            Console.ResetColor();

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\nВведите новое название или 0 чтобы отменить изменение: ");
                            Console.ResetColor();

                            string new_name = Console.ReadLine().Trim();
                            if (string.IsNullOrEmpty(new_name))
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                Console.ResetColor();

                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nНазвание не может быть пустым!");
                                Console.ResetColor();

                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                Console.ResetColor();

                                Console.ReadLine();
                                continue;
                            }

                            if (new_name == "0")
                                return;

                            new_name = char.ToUpper(new_name[0]) + new_name.Substring(1);

                            if (Pizzas.Exists(b => b.Name == new_name))
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                Console.ResetColor();

                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"\nПицца с названием {new_name} уже существует.\n");
                                Console.ResetColor();

                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                Console.ResetColor();

                                Console.ReadLine();
                                continue;
                            }

                            Pizzas[pizza_to_edit - 1].Name = new_name;
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                            Console.ResetColor();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nНазвание успешно изменено!\n");
                            Console.ResetColor();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Новое название: {new_name}.");
                            Console.ResetColor();

                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("Нажмите Enter чтобы продолжить...");
                            Console.ResetColor();

                            Console.ReadLine();
                            break;
                        }

                        break;
                    }

                case "2":
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                        Console.ResetColor();

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nЧто вы хотите поменять?\n");
                        Console.WriteLine("1.Ингредиенты");
                        Console.WriteLine("2.Основы(только изменение)");
                        Console.WriteLine("0.Отменить изменения");
                        Console.WriteLine("\nВведите номер: ");
                        Console.ResetColor();

                        string editing_paramter2 = Console.ReadLine().Trim();
                        switch (editing_paramter2)
                        {
                            case "1":
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                    Console.ResetColor();

                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("\nЧто вы хотите сделать?\n");
                                    Console.WriteLine("1.Добавить ингредиенты");
                                    Console.WriteLine("2.Удалить ингредиенты");
                                    Console.WriteLine("0.Отменить изменения");
                                    Console.WriteLine("\nВведите номер: ");
                                    Console.ResetColor();

                                    string editing_paramter3 = Console.ReadLine().Trim();
                                    switch (editing_paramter3)
                                    {
                                        case "1":
                                            {
                                                List<Ingredient> selected_ingredients = Pizzas[pizza_to_edit - 1].Ingredients;
                                                while (true)
                                                {
                                                    Console.Clear();
                                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                                    Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1].Name} ===\n");
                                                    Console.ResetColor();

                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                    Console.WriteLine($"\nОснова пиццы: {Pizzas[pizza_to_edit - 1].PizzaBase}\n");
                                                    PrintAvailableIngredients();
                                                    Console.WriteLine("\nВыберите ингредиент для добавления, введите номер или 0 для завершения: ");
                                                    Console.ResetColor();

                                                    int ingredient_index;
                                                    if (!int.TryParse(Console.ReadLine(), out ingredient_index) ||
                                                        ingredient_index < 0 || ingredient_index > Available_ingredients.Count)
                                                    {
                                                        Console.Clear();
                                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                                        Console.ResetColor();

                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                        Console.WriteLine("\nНеверный выбор ингредиента!\n");
                                                        Console.ResetColor();

                                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                                        Console.ResetColor();

                                                        Console.ReadLine();
                                                        continue;
                                                    }

                                                    if (ingredient_index == 0)
                                                        break;

                                                    Ingredient selected_ingredient = Available_ingredients[ingredient_index - 1];

                                                    if (selected_ingredients.Exists(i => i.Name == selected_ingredient.Name))
                                                    {
                                                        Console.Clear();
                                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                                        Console.ResetColor();

                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                        Console.WriteLine("\nЭтот ингредиент уже добавлен!\n");
                                                        Console.ResetColor();

                                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                                        Console.ResetColor();

                                                        Console.ReadLine();
                                                        continue;
                                                    }

                                                    selected_ingredients.Add(selected_ingredient);

                                                    Console.Clear();
                                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                                    Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                                    Console.ResetColor();

                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                    Console.WriteLine($"\nИнгредиент {selected_ingredient.Name} успешно добавлен!\n");
                                                    Console.ResetColor();

                                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                                    Console.ResetColor();

                                                    Console.ReadLine();
                                                }

                                                break;
                                            }

                                        case "2":
                                            {
                                                while (true)
                                                {
                                                    Console.Clear();
                                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                                    Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n\n");
                                                    Console.ResetColor();

                                                    PrintIngredients(Pizzas[pizza_to_edit - 1]);

                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                    Console.WriteLine("Введите номер ингредиента или 0 чтобы закончить удаление: ");
                                                    Console.ResetColor();

                                                    if (!int.TryParse(Console.ReadLine().Trim(), out int index_ingr) ||
                                                        index_ingr < 0 || index_ingr > Pizzas[pizza_to_edit - 1].Ingredients.Count)
                                                    {
                                                        Console.Clear();
                                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                                        Console.ResetColor();

                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                        Console.WriteLine("Такого ингредиента не существует!\n");
                                                        Console.ResetColor();

                                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                                        Console.WriteLine("Нажмите Enter для продолжения...");
                                                        Console.ResetColor();

                                                        Console.ReadLine();
                                                        continue;
                                                    }

                                                    if (index_ingr == 0)
                                                        break;

                                                    Pizzas[pizza_to_edit - 1].Ingredients.RemoveAt(index_ingr);

                                                    Console.Clear();
                                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                                    Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                                    Console.ResetColor();

                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                    Console.WriteLine("Ингредиент успешно удален!");
                                                    Console.ResetColor();

                                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                                    Console.WriteLine("Нажмите Enter для продолжения...");
                                                    Console.ResetColor();

                                                    Console.ReadLine();
                                                }

                                                break;
                                            }

                                        case "0":
                                            break;
                                        default:
                                            {
                                                Console.Clear();
                                                Console.ForegroundColor = ConsoleColor.Cyan;
                                                Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                                Console.ResetColor();

                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("\nТакой команды не существует!\n");
                                                Console.ResetColor();

                                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                                Console.ResetColor();

                                                Console.ReadLine();
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case "2":
                                {
                                    while (true)
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n\n");
                                        Console.ResetColor();

                                        PrintAvailableBases();

                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.WriteLine("Какую основу вы хотите выбрать, введите номер или 0 чтобы прекратить редактирование: ");
                                        Console.ResetColor();

                                        if (!int.TryParse(Console.ReadLine().Trim(), out int index_base) ||
                                            index_base < 1 || index_base > Available_bases.Count)
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Cyan;
                                            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                            Console.ResetColor();

                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Такой основы для пиццы не существует!\n");
                                            Console.ResetColor();

                                            Console.ForegroundColor = ConsoleColor.DarkGray;
                                            Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                            Console.ResetColor();

                                            Console.ReadLine();
                                            continue;
                                        }

                                        if (index_base == 0)
                                            break;

                                        Pizzas[pizza_to_edit - 1].PizzaBase = Available_bases[index_base - 1];

                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                        Console.ResetColor();

                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Основа успешно изменена!\n");
                                        Console.ResetColor();

                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                        Console.ResetColor();

                                        Console.ReadLine();
                                        break;
                                    }
                                    break;
                                }

                            case "0":
                                break;
                            default:
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                    Console.ResetColor();

                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nТакой команды не существует");
                                    Console.ResetColor();

                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                    Console.ResetColor();

                                    Console.ReadLine();
                                    break;
                                }
                        }
                        break;
                    }

                case "0":
                    break;
                default:
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                        Console.ResetColor();

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nНеверный ввод команды!\n");
                        Console.ResetColor();

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                        Console.ResetColor();

                        Console.ReadLine();
                        break;
                    }
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Console.Title = "The Dalmatian Pizza";

            List<Ingredient> ingredients = new List<Ingredient>()
            {
                new Ingredient {Name = "Сыр", Price = 30},
                new Ingredient {Name = "Томаты", Price = 40},
                new Ingredient {Name = "Пеперони", Price = 35},
                new Ingredient {Name = "Лук", Price = 25},
                new Ingredient {Name = "Базилик", Price = 45}
            };

            List<Base> bases = new List<Base>()
            {
                new Base {Name = "Классическая", Price = 220},
                new Base {Name = "Тонкое", Price = 200},
                new Base {Name = "Слоеное", Price = 210},
            };

            ManageIngredients ingredients_manager = new ManageIngredients(ingredients);
            ManageBase base_manager = new ManageBase(bases);
            ManagePizza pizza_manager = new ManagePizza(ingredients, bases);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Добро пожаловать в онлайн пиццерию 'The Dalmatian Pizza' ===\n");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\t\tНажмите Enter чтобы продолжить...");
            Console.ResetColor();
            Console.ReadLine();

            bool is_working = true;

            while (is_working)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== The Dalmatian Pizza ===\n");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1. Управление ингредиентами");
                Console.WriteLine("2. Управление основами для пиццы");
                Console.WriteLine("3. Управление пиццами");
                Console.WriteLine("4. Просмотр списков");
                Console.WriteLine("0. Выйти из приложения\n");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Выберите действие и введите его номер: ");
                Console.ResetColor();

                string first_choice = Console.ReadLine().Trim();
                string second_choice = "";

                switch (first_choice)
                {
                    case "1":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("=== УПРАВЛЕНИЕ ИНГРЕДИЕНТАМИ ===\n");

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("1. Добавить ингредиент");
                        Console.WriteLine("2. Редактировать ингредиент");
                        Console.WriteLine("3. Удалить ингредиент");
                        Console.WriteLine("0. Назад\n");

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("Выберите действие: ");
                        Console.ResetColor();

                        second_choice = Console.ReadLine().Trim();

                        switch (second_choice)
                        {
                            case "1":
                                ingredients_manager.Adding();
                                break;

                            case "2":
                                ingredients_manager.Editing();
                                break;

                            case "3":
                                ingredients_manager.Removing();
                                break;

                            case "0":
                                break;

                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nТакой команды не существует!");
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine("Нажмите Enter для продолжения...");
                                Console.ReadLine();
                                Console.ResetColor();
                                break;
                        }
                        break;

                    case "2":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("=== УПРАВЛЕНИЕ ОСНОВАМИ ===\n");

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("1. Добавить основу");
                        Console.WriteLine("2. Редактировать основу");
                        Console.WriteLine("3. Удалить основу");
                        Console.WriteLine("0. Назад\n");

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("Выберите действие: ");
                        Console.ResetColor();

                        second_choice = Console.ReadLine().Trim();

                        switch (second_choice)
                        {
                            case "1":
                                base_manager.Adding();
                                break;
                            case "2":
                                base_manager.Editing();
                                break;
                            case "3":
                                base_manager.Removing();
                                break;
                            case "0":
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nТакой команды не существует!");
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine("Нажмите Enter для продолжения...");
                                Console.ReadLine();
                                Console.ResetColor();
                                break;
                        }
                        break;

                    case "3":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("=== УПРАВЛЕНИЕ ПИЦЦАМИ ===\n");

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("1. Добавить пиццу");
                        Console.WriteLine("2. Редактировать пиццу");
                        Console.WriteLine("3. Удалить пиццу");
                        Console.WriteLine("0. Назад\n");

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("Выберите действие: ");
                        Console.ResetColor();

                        second_choice = Console.ReadLine().Trim();

                        switch (second_choice)
                        {
                            case "1":
                                pizza_manager.Adding();
                                break;
                            case "2":
                                pizza_manager.Editing();
                                break;
                            case "3":
                                pizza_manager.Removing();
                                break;
                            case "0":
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nТакой команды не существует!");
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                Console.ReadLine();
                                Console.ResetColor();
                                break;
                        }
                        break;

                    case "4":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("=== СПИСКИ ИНГРЕДИЕНТОВ, ОСНОВ, ПИЦЦ ===\n");

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("1. Посмотреть ингредиенты");
                        Console.WriteLine("2. Посмотреть основы");
                        Console.WriteLine("3. Посмотреть пиццы");
                        Console.WriteLine("0. Назад\n");

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("Выберите действие: ");
                        Console.ResetColor();

                        second_choice = Console.ReadLine().Trim();

                        switch (second_choice)
                        {
                            case "1":
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.White;
                                ingredients_manager.PrintIngredients();
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine("\nНажмите Enter для продолжения...");
                                Console.ReadLine();
                                Console.ResetColor();
                                break;
                            case "2":
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.White;
                                base_manager.PrintBases();
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine("\nНажмите Enter для продолжения...");
                                Console.ReadLine();
                                Console.ResetColor();
                                break;
                            case "3":
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.White;
                                pizza_manager.PrintPizzas();
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine("\nНажмите Enter для продолжения...");
                                Console.ReadLine();
                                Console.ResetColor();
                                break;
                            case "0":
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nТакой команды не существует!");
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine("Нажмите Enter для продолжения...");
                                Console.ReadLine();
                                Console.ResetColor();
                                break;
                        }
                        break;

                    case "0":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nСпасибо за использование The Dalmatian Pizza!\n");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                        Console.ReadLine();
                        Console.ResetColor();
                        is_working = false;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nТакой команды не существует!\n");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                        Console.ReadLine();
                        break;
                }

                Console.ResetColor();
            }
        }
    }
}