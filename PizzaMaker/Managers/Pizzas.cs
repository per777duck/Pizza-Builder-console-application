using PizzaMaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PizzaMaker.Managers
{
    internal class ManagePizza : IManager
    {
        private List<Pizza> Pizzas;
        private List<Ingredient> Available_ingredients;
        private List<Base> Available_bases;
        private List<Border> Available_Borders;

        public ManagePizza(List<Pizza> pizzas, List<Ingredient> ingredients,
            List<Base> bases, List<Border> availableBorders)
        {
            Pizzas = pizzas;
            Available_bases = bases;
            Available_ingredients = ingredients;
            Available_Borders = availableBorders;
        }

        public void PrintPizzas()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== СПИСОК ПИЦЦ ===\n");
            Console.ResetColor();
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
            Console.WriteLine($"Используемая основа: {pizza.PizzaBase.Name} | цена: {pizza.PizzaBase.Price} руб.");
            Console.WriteLine($"Используемый бортик: {pizza.PizzaBorder.Name} | цена: {pizza.PizzaBorder.Price} руб.");
            Console.WriteLine("Используемые ингредиенты:\n");
            Console.ResetColor();

            PrintIngredients(pizza);
        }

        private void PrintAvailableBases()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Доступные основы:\n");
            Console.ResetColor();
            for (int i = 0; i < Available_bases.Count; i++)
            {
                Console.WriteLine($"{i + 1}." + Available_bases[i].ToString());
            }
        }

        private List<int> PrintAvailableBorders(Pizza pizza)
        {
            List<int> border_indexes = new List<int>();
            int cnt = 1;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Доступные бортики:\n");
            Console.ResetColor();
            for (int i = 0; i < Available_Borders.Count; i++)
            {
                for (int j = 0; j < Available_Borders[i].AvailablePizzas.Count; j++)
                {
                    if (Available_Borders[i].AvailablePizzas[j].Name == pizza.Name)
                    {
                        border_indexes.Add(i);
                        Console.WriteLine($"{cnt++}.{Available_Borders[i].Name} | Цена: {Available_Borders[i].Price} руб.");
                        break;
                    }
                }
            }

            return border_indexes;
        }

        private void PrintBorders(List<int> border_indexes)
        {
            if (border_indexes.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Доступные бортики:");
                Console.ResetColor();
                for (int i = 0; i < border_indexes.Count; i++)
                {
                    Console.WriteLine($"{i + 1}.{Available_Borders[border_indexes[i]].Name} " +
                        $"| Цена:{Available_Borders[border_indexes[i]].Price} руб.");
                }

                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Доступные бортики:");
            Console.ResetColor();
            for (int i = 0; i < Available_Borders.Count; i++)
            {
                Console.WriteLine($"{i + 1}.{Available_Borders[i].Name} | Цена:{Available_Borders[i].Price} руб.");
            }
        }

        private void PrintBordersFromAll()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Доступные бортики:");
            Console.ResetColor();
            for (int i = 0; i < Available_Borders.Count; i++)
            {
                Console.WriteLine($"{i + 1}.{Available_Borders[i].Name} | Цена:{Available_Borders[i].Price} руб.");
            }
        }

        private void AddAvailablePizza(List<int> border_indexes, Pizza pizza)
        {
            for (int i = 0; i < border_indexes.Count; i++)
            {
                Available_Borders[border_indexes[i]].AvailablePizzas.Add(pizza);
            }
        }

        private void PrintAvailableIngredients()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Доступные ингредиенты:\n");
            Console.ResetColor();
            for (int i = 0; i < Available_ingredients.Count; i++)
            {
                Console.WriteLine($"{i + 1}." + Available_ingredients[i].ToString());
            }
        }

        private int CalculatingPizzaPrice(Border selected_border, Base selected_base, List<Ingredient> selected_ingredients)
        {
            int total_sum = (selected_base.Price + selected_border.Price);
            foreach (Ingredient ingr in selected_ingredients)
            {
                total_sum += ingr.Price;
            }

            return total_sum;
        }

        private void FilterByIngredients()
        {
            List<string> ingredient_to_search = new List<string>();
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== ФИЛЬТРАЦИЯ ПО ИНГРЕДИЕНТАМ ===\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Введите название ингредиента или 0 чтобы искать: ");
                Console.ResetColor();

                string ingr_name = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(ingr_name))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("=== ФИЛЬТРАЦИЯ ПО ИНГРЕДИЕНТАМ ===\n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Название ингредиента не может быть пустым!");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
                }

                if (ingr_name == "0")
                    break;

                ingr_name = char.ToUpper(ingr_name[0]) + ingr_name.Substring(1);

                ingredient_to_search.Add(ingr_name);
            }

            List<Pizza> filtred_pizzas = Pizzas
                .Where(p => ingredient_to_search.All(search => p.Ingredients.Any(ingr =>
                 ingr.Name.Equals(search, StringComparison.OrdinalIgnoreCase))))
                .ToList();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== ФИЛЬТРАЦИЯ ПО ИНГРЕДИЕНТАМ ===\n");

            foreach (Pizza pizza in filtred_pizzas)
            {
                PrintPizzaStructure(pizza);
                Console.WriteLine("\n");
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Нажмите Enter чтобы продолжить...");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void FilterByBases()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== ФИЛЬТРАЦИЯ ПО ОСНОВАМ ===\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Введите название основы: ");
            Console.ResetColor();

            string base_name = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(base_name))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== ФИЛЬТРАЦИЯ ПО ОСНОВАМ ===\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Название основы не может быть пустым!");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            base_name = char.ToUpper(base_name[0]) + base_name.Substring(1);

            List<Pizza> filtred_pizzas = Pizzas
                .Where(p => p.PizzaBase.Name == base_name).ToList();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== ФИЛЬТРАЦИЯ ПО ОСНОВАМ ===\n");

            foreach (Pizza pizza in filtred_pizzas)
            {
                PrintPizzaStructure(pizza);
                Console.WriteLine("\n");
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Нажмите Enter чтобы продолжить...");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void FilterByBorders()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== ФИЛЬТРАЦИЯ ПО БОРТИКАМ ===\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Введите название бортика: ");
            Console.ResetColor();

            string border_name = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(border_name))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== ФИЛЬТРАЦИЯ ПО БОРТИКАМ ===\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Название бортика не может быть пустым!");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            border_name = char.ToUpper(border_name[0]) + border_name.Substring(1);

            List<Pizza> filtred_pizzas = Pizzas
                .Where(p => p.PizzaBorder.Name == border_name).ToList();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== ФИЛЬТРАЦИЯ ПО БОРТИКАМ ===\n");

            foreach (Pizza pizza in filtred_pizzas)
            {
                PrintPizzaStructure(pizza);
                Console.WriteLine("\n");
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

            List<Pizza> filtered_pizzas = Pizzas
                .Where(i => i.Name.ToLower().Contains(search_text))
                .ToList();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== ФИЛЬТРАЦИЯ ПО НАЗВАНИЮ ===\n");
            Console.WriteLine($"Ингредиенты, содержащие '{search_text}' в названии:\n");
            foreach (Pizza pizza in filtered_pizzas)
            {
                Console.WriteLine(" - " + pizza.ToString());
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

            List<Pizza> filtered_pizzas = Pizzas.OrderByDescending(i => i.Price).ToList();
            foreach (Pizza pizza in filtered_pizzas)
            {
                Console.WriteLine(" - " + pizza.ToString());
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

            List<Pizza> filtered_pizzas = Pizzas.OrderBy(i => i.Price).ToList();
            foreach (Pizza pizza in filtered_pizzas)
            {
                Console.WriteLine(" - " + pizza.ToString());
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Нажмите Enter чтобы продолжить...");
            Console.ResetColor();
            Console.ReadLine();
        }

        public void FilterPizzas()
        {
            if (Pizzas.Count == 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== ФИЛЬТРАЦИЯ ПИЦЦ ===");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nСписок пицц пуст.");
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
                Console.WriteLine("=== ФИЛЬТРАЦИЯ ПИЦЦ ===");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1. По названию (содержит текст)");
                Console.WriteLine("2. По убыванию цены");
                Console.WriteLine("3. По возрастанию цены");
                Console.WriteLine("4. Ингредиенты");
                Console.WriteLine("5. Основа");
                Console.WriteLine("6. Ботик");
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
                        FilterByIngredients();
                        filtrating = false;
                        break;
                    case "5":
                        FilterByBases();
                        filtrating = false;
                        break;
                    case "6":
                        FilterByBorders();
                        filtrating = false;
                        break;
                    case "0":
                        return;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("=== ФИЛЬТРАЦИЯ ПИЦЦ ===");
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
                Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                Console.ResetColor();

                PrintAvailableBases();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nВыберите основу для пиццы, введите номер или 0 чтобы отменить создание: ");
                Console.ResetColor();

                if (!int.TryParse(Console.ReadLine(), out base_index) || base_index < 0 || base_index > Available_bases.Count)
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

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Хотите ли вы добавить доступные бортики с ингредиентами для этой пиццы?");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("По умолчанию доступны все бортики.");
            Console.Write("Введите да/нет:");

            string answer = Console.ReadLine().Trim().ToLower();
            List<int> border_indexes = new List<int>();
            if (answer == "да")
            {
                while (true)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                    Console.ResetColor();
                    PrintBordersFromAll();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Введите номер бортика, чтобы сделать его доступным для пиццы или 0 чтобы выйти.");
                    Console.ResetColor();

                    int choice = -1;
                    if (!int.TryParse(Console.ReadLine().Trim(), out choice) ||
                        choice < 0 || choice > Available_Borders.Count)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Такого бортика нет! Попробуйте еще раз.");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                        Console.ReadLine();
                        continue;
                    }

                    if (choice == 0)
                    {
                        break;
                    }

                    border_indexes.Add(choice - 1);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Бортик успешно добавлен!");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ReadLine();
                }
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Хотите ли вы добавить бортик с ингредиентами?");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("По умолчанию стоит классический бортик.");
            Console.Write("Введите да/нет:");

            Border selected_border = Available_Borders[0];
            answer = Console.ReadLine().Trim().ToLower();
            if (answer == "да")
            {
                while (true)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                    PrintBorders(border_indexes);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Какой бортик вы хотите выбрать, введите номер или 0 чтобы отменить");
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                    int choice = -1;
                    if (border_indexes.Count > 0)
                    {
                        if (!int.TryParse(Console.ReadLine().Trim(), out choice) ||
                        choice < 0 || choice > border_indexes.Count)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Такого бортика нет! Попробуйте еще раз.");
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("Нажмите Enter чтобы продолжить...");
                            Console.ReadLine();
                            continue;
                        }
                    }
                    else
                    {
                        if (!int.TryParse(Console.ReadLine().Trim(), out choice) ||
                            choice < 0 || choice > Available_Borders.Count)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Такого бортика нет! Попробуйте еще раз.");
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("Нажмите Enter чтобы продолжить...");
                            Console.ReadLine();
                            continue;
                        }
                    }
                    if (choice == 0)
                    {
                        break;
                    }

                    if (border_indexes.Count > 0)
                    {
                        selected_border = Available_Borders[border_indexes[choice - 1]];
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Бортик успешно добавлен!");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                        Console.ReadLine();
                        break;
                    }

                    selected_border = Available_Borders[choice - 1];
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Бортик успешно добавлен!");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ReadLine();
                    break;
                }
            }

            Console.ResetColor();
            List<Ingredient> selected_ingredients = new List<Ingredient>();
            bool adding = true;
            while (adding)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===");
                Console.ResetColor();

                Console.WriteLine($"Выбраная основа: {selected_base.Name}");
                Console.WriteLine($"Выбраный бортик: {selected_border.Name}\n");
                PrintAvailableIngredients();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Выберите ингредиент для добавления, введите номер или 0 для завершения: ");
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
                    Console.WriteLine("Этот ингредиент уже добавлен!");
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

                int current_price = CalculatingPizzaPrice(selected_border, selected_base, selected_ingredients);
                Console.WriteLine($"Текущая стоимость пиццы: {current_price} руб.");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Нажмите Enter для продолжения...");
                Console.ResetColor();
                Console.ReadLine();
            }

            int total_price = CalculatingPizzaPrice(selected_border, selected_base, selected_ingredients);

            Pizza new_pizza = new Pizza
            {
                Name = name,
                PizzaBase = selected_base,
                Ingredients = selected_ingredients,
                PizzaBorder = selected_border,
                Price = total_price,
            };

            Pizzas.Add(new_pizza);
            AddAvailablePizza(border_indexes, new_pizza);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
            Console.ResetColor();

            Console.WriteLine($"Основа: {selected_base.Name}");
            Console.WriteLine($"Бортик: {selected_border.Name}");
            Console.WriteLine("Добавленные ингредиенты:");
            foreach (Ingredient ingr in selected_ingredients)
            {
                Console.WriteLine($" - {ingr.Name} | цена: {ingr.Price} руб.");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nНовая пицца успешно создана!");
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
                        Console.WriteLine("3.Бортик(только изменение)");
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

                                                int total = CalculatingPizzaPrice(Pizzas[pizza_to_edit - 1].PizzaBorder,
                                                    Pizzas[pizza_to_edit - 1].PizzaBase, Pizzas[pizza_to_edit - 1].Ingredients);
                                                Pizzas[pizza_to_edit - 1].Price = total;

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

                                                int total1 = CalculatingPizzaPrice(Pizzas[pizza_to_edit - 1].PizzaBorder,
                                                    Pizzas[pizza_to_edit - 1].PizzaBase, Pizzas[pizza_to_edit - 1].Ingredients);
                                                Pizzas[pizza_to_edit - 1].Price = total1;

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
                                            index_base < 0 || index_base > Available_bases.Count)
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

                                    int total2 = CalculatingPizzaPrice(Pizzas[pizza_to_edit - 1].PizzaBorder,
                                                Pizzas[pizza_to_edit - 1].PizzaBase, Pizzas[pizza_to_edit - 1].Ingredients);
                                    Pizzas[pizza_to_edit - 1].Price = total2;

                                    break;
                                }

                            case "3":
                                while (true)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n\n");
                                    Console.ResetColor();
                                    List<int> border_indexes = PrintAvailableBorders(Pizzas[pizza_to_edit - 1]);
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Введите номер бортика который вы хотите добавить в пиццу или 0 чтобы выйти:");
                                    Console.ResetColor();

                                    int new_border = -1;
                                    if (!int.TryParse(Console.ReadLine().Trim(), out new_border) ||
                                            new_border < 0 || new_border > border_indexes.Count)
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n\n");
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Неверный ввод номера!");
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                        Console.ResetColor();
                                        Console.ReadLine();
                                        continue;
                                    }

                                    if (new_border == 0)
                                    {
                                        return;
                                    }

                                    Pizzas[pizza_to_edit - 1].PizzaBorder = Available_Borders[border_indexes[new_border - 1]];

                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n\n");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Бортик успешно изменен!");
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                    Console.ResetColor();
                                    Console.ReadLine();
                                    break;
                                }

                                int total3 = CalculatingPizzaPrice(Pizzas[pizza_to_edit - 1].PizzaBorder,
                                            Pizzas[pizza_to_edit - 1].PizzaBase, Pizzas[pizza_to_edit - 1].Ingredients);
                                Pizzas[pizza_to_edit - 1].Price = total3;

                                break;

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
}
