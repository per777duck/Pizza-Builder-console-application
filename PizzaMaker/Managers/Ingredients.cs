using PizzaMaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaMaker.Managers
{
    internal class ManageIngredients : IManager
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== СПИСОК ИНГРЕДИЕНТОВ ===\n");
            Console.ResetColor();
            for (int i = 0; i < Ingredients.Count; i++)
            {
                Console.WriteLine($"{i + 1}." + Ingredients[i].ToString());
            }
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

            List<Ingredient> filtered_ingredients = Ingredients
                .Where(i => i.Name.ToLower().Contains(search_text)).ToList();

            Console.WriteLine($"Ингредиенты, содержащие '{search_text}' в названии:\n");
            foreach (Ingredient ingr in filtered_ingredients)
            {
                Console.WriteLine(" - " + ingr.ToString());
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

            List<Ingredient> filtered_ingredients = Ingredients.OrderByDescending(i => i.Price).ToList();
            foreach (Ingredient ingr in filtered_ingredients)
            {
                Console.WriteLine(" - " + ingr.ToString());
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

            List<Ingredient> filtered_ingredients = Ingredients.OrderBy(i => i.Price).ToList();
            foreach (Ingredient ingr in filtered_ingredients)
            {
                Console.WriteLine(" - " + ingr.ToString());
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Нажмите Enter чтобы продолжить...");
            Console.ResetColor();
            Console.ReadLine();
        }

        public void FilterIngredients()
        {
            if (Ingredients.Count == 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== ФИЛЬТРАЦИЯ ИНГРЕДИЕНТОВ ===");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nСписок ингредиентов пуст.");
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
                Console.WriteLine("=== ФИЛЬТРАЦИЯ ИНГРЕДИЕНТОВ ===");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1. По названию (содержит текст)");
                Console.WriteLine("2. По убыванию цены");
                Console.WriteLine("3. По возрастанию цены");
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
                    case "0":
                        return;
                    default:
                        Console.Clear();
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
                Console.WriteLine("\nВведите стоимость ингредиента или -1 чтобы прекратить добавление: ");
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
                    ingredient_to_remove < 0 || ingredient_to_remove > Ingredients.Count)
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
                    ingredient_to_edit < 0 || ingredient_to_edit > Ingredients.Count)
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
}
