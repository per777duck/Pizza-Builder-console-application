using PizzaMaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaMaker.Managers
{
    internal class ManageOrders
    {
        private List<Order> Orders;
        private List<Pizza> Available_pizzas;
        private List<Base> Available_bases;
        private List<Ingredient> Available_ingredients;
        private List<Border> Available_borders;

        public ManageOrders(List<Pizza> pizzas, List<Ingredient> available_ingredients,
            List<Base> available_bases, List<Border> available_borders)
        {
            Orders = new List<Order>();
            Available_pizzas = pizzas;
            Available_ingredients = available_ingredients;
            Available_bases = available_bases;
            Available_borders = available_borders;
        }

        public void PrintOrders()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== СПИСОК ЗАКАЗОВ ===");
            Console.ResetColor();
            foreach (Order order in Orders)
            {
                Console.WriteLine($"Заказ -- {order.Id} --");
                Console.WriteLine($"Комментарий к заказу: {order.Comment}");
                if (order.DateDilivery.Day == "Сегодня")
                {
                    Console.WriteLine("Заказ будет доставлен через час");
                }
                else
                {
                    Console.WriteLine($"Дата доставки: " + order.DateDilivery.ToString());
                }

                Console.WriteLine($"Стоимость заказа: {order.TotalPrice} руб.");

                int cnt = 1;
                foreach (Pizza pizza in order.Pizzas)
                {
                    Console.WriteLine($"\t{cnt}. Пицца: {pizza.Name} | Цена: {pizza.Price} руб.");
                    Console.WriteLine($"\t  Используемая основа: {pizza.PizzaBase}");
                    if (pizza.CoupleBorders == null)
                    {
                        Console.WriteLine($"\t  Используемый бортик: {pizza.PizzaBorder.Name}");
                    }
                    else if (pizza.CoupleBorders.Count > 1)
                    {
                        Console.WriteLine($"\t  Используемые бортики: {pizza.CoupleBorders[0].Name}, {pizza.CoupleBorders[1].Name}");
                    }
                    else
                    {
                        Console.WriteLine($"\t  Используемый бортик: {pizza.CoupleBorders[0].Name}");
                    }
                    Console.WriteLine("\t  Используемые ингредиенты: ");
                    foreach (Ingredient ingr in pizza.Ingredients)
                    {
                        Console.WriteLine($"\t   - {ingr.Name}");
                    }
                    cnt++;
                }
            }
        }

        private void PrintAvailablePizzas()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Доступные пиццы:");
            Console.ResetColor();
            for (int i = 0; i < Available_pizzas.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Название пиццы: {Available_pizzas[i].Name} | Цена: {Available_pizzas[i].Price} руб.");
                Console.WriteLine($"\tИспользуемая основа: {Available_pizzas[i].PizzaBase.Name}");
                Console.WriteLine($"\tИспользуемый бортик: {Available_pizzas[i].PizzaBorder.Name}");
                Console.WriteLine("\tИспользуемые ингредиенты:");
                for (int j = 0; j < Available_pizzas[i].Ingredients.Count; j++)
                {
                    Console.WriteLine($"\t - {Available_pizzas[i].Ingredients[j].Name}");
                }
            }
        }

        private void PrintAvailablePizzasCouple()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Доступные пиццы:");
            Console.ResetColor();
            for (int i = 0; i < Available_pizzas.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Название пиццы: {Available_pizzas[i].Name} | Цена: {Available_pizzas[i].Price / 1.5} руб.");
                Console.WriteLine($"\tИспользуемая основа: {Available_pizzas[i].PizzaBase.Name}");
                Console.WriteLine($"\tИспользуемый бортик: {Available_pizzas[i].PizzaBorder.Name}");
                Console.WriteLine("\tИспользуемые ингредиенты:");
                for (int j = 0; j < Available_pizzas[i].Ingredients.Count; j++)
                {
                    Console.WriteLine($"\t - {Available_pizzas[i].Ingredients[j].Name}");
                }
            }
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

        private void PrintAvailableBorders()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Доступные бортики:\n");
            Console.ResetColor();
            for (int i = 0; i < Available_borders.Count; i++)
            {
                Console.WriteLine($"{i + 1}.{Available_borders[i].Name} | Цена: {Available_borders[i].Price} руб.");
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

        private void PrintAvailableIngredientsToDouble(List<Ingredient> Available_ingredients)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Доступные ингредиенты:");
            Console.ResetColor();
            for (int i = 0; i < Available_ingredients.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Available_ingredients[i].Name} | Цена: {Available_ingredients[i].Price} руб.");
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

        private int CalculatingPizzaCouplePrice(Pizza pizza1, Pizza pizza2, Border border1, Border border2)
        {
            int total_sum = (int)((pizza1.Price / 1.5) + (pizza2.Price / 1.5));
            if (border1 == border2)
                total_sum += (int)((border1.Price / 1.5));
            else
                total_sum += (int)((border1.Price / 1.5) + (border2.Price / 1.5));
            return total_sum;
        }

        private int CalculatingOrderPrice(List<Pizza> selected_pizzas)
        {
            int total = 0;
            foreach (Pizza pizza in selected_pizzas)
            {
                total += pizza.Price;
            }

            return total;
        }

        private void PrintOrder(Order order)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"=== ЗАКАЗ НОМЕР: {order.Id} ===");
            Console.ResetColor();
            Console.WriteLine($"Комментарий к заказу: {order.Comment}");
            if (order.DateDilivery.Day == "Сегодня")
            {
                Console.WriteLine("Заказ будет доставлен через час");
            }
            else
            {
                Console.WriteLine($"Дата доставки: " + order.DateDilivery.ToString());
            }

            Console.WriteLine($"Стоимость заказа: {order.TotalPrice} руб.");

            int cnt = 1;
            foreach (Pizza pizza in order.Pizzas)
            {
                Console.WriteLine($"\t{cnt}. Пицца: {pizza.Name} | Цена: {pizza.Price} руб.");
                Console.WriteLine($"\t  Используемая основа: {pizza.PizzaBase}");
                if (pizza.CoupleBorders == null)
                {
                    Console.WriteLine($"\t  Используемый бортик: {pizza.PizzaBorder.Name}");
                }
                else if (pizza.CoupleBorders.Count > 1)
                {
                    Console.WriteLine($"\t  Используемые бортики: {pizza.CoupleBorders[0].Name}, {pizza.CoupleBorders[1].Name}");
                }
                else
                {
                    Console.WriteLine($"\t  Используемый бортик: {pizza.CoupleBorders[0].Name}");
                }
                Console.WriteLine("\t  Используемые ингредиенты: ");
                foreach (Ingredient ingr in pizza.Ingredients)
                {
                    Console.WriteLine($"\t   - {ingr.Name}");
                }
                cnt++;
            }
        }

        private void FilterDescendingOrderPrice()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== ФИЛЬТРАЦИЯ ПО УБЫВАНИЮ ===\n");
            Console.ResetColor();

            List<Order> filtered_orders = Orders.OrderByDescending(i => i.TotalPrice).ToList();
            foreach (Order order in filtered_orders)
            {
                PrintOrder(order);
                Console.WriteLine("\n");
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

            List<Order> filtered_orders = Orders.OrderBy(i => i.TotalPrice).ToList();
            foreach (Order order in filtered_orders)
            {
                PrintOrder(order);
                Console.WriteLine("\n");
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Нажмите Enter чтобы продолжить...");
            Console.ResetColor();
            Console.ReadLine();
        }

        public void FilterOrders()
        {
            if (Orders.Count == 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== ФИЛЬТРАЦИЯ ЗАКАЗОВ ===");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nСписок заказов пуст.");
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
                Console.WriteLine("=== ФИЛЬТРАЦИЯ ЗАКАЗОВ ===");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1. По убыванию цены");
                Console.WriteLine("2. По возрастанию цены");
                Console.WriteLine("0. Вернуться назад\n");
                Console.WriteLine("Выберите тип фильтрации и введите номер: ");
                Console.ResetColor();

                string choice = Console.ReadLine().Trim();

                switch (choice)
                {
                    case "1":
                        FilterAscendingOrderPrice();
                        filtrating = false;
                        break;
                    case "2":
                        FilterDescendingOrderPrice();
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
            bool making_order = true;
            List<Pizza> selected_pizzas = new List<Pizza>();
            while (making_order)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n1. Добавить пиццу из меню");
                Console.WriteLine("2. Создать свою пиццу");
                Console.WriteLine("3. Пицца пополам");
                Console.WriteLine("0. Закончить заказ");
                Console.WriteLine("\nВведите номер: ");
                Console.ResetColor();

                string choice = Console.ReadLine().Trim();
                switch (choice)
                {
                    case "1":
                        while (true)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                            PrintAvailablePizzas();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Какую пиццу хотите добавить в заказ, введите номер или 0 чтобы не добавлять: ");
                            Console.ResetColor();

                            int choosed_pizza = -1;
                            if (!int.TryParse(Console.ReadLine().Trim(), out choosed_pizza) ||
                                choosed_pizza < 0 || choosed_pizza > Available_pizzas.Count)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nТакой пиццы не существует!");
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                Console.ReadLine();
                                continue;
                            }

                            if (choosed_pizza == 0) break;

                            int choosed_size = -1;
                            while (true)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("\n1. Маленькая");
                                Console.WriteLine("2. Средняя");
                                Console.WriteLine("3. Большая\n");
                                Console.WriteLine("Каккого размера будет пицца, введите номер или 0 чтобы выбрать по умолчанию: ");
                                Console.ResetColor();

                                if (!int.TryParse(Console.ReadLine().Trim(), out choosed_size) ||
                                    choosed_size < 0 || choosed_size > 3)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nТакого размера пиццы не существует!");
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                    Console.ReadLine();
                                    continue;
                                }

                                break;
                            }

                            Pizza added_pizza = new Pizza(Available_pizzas[choosed_pizza - 1]);

                            if (choosed_size == 1) added_pizza.Size = "Маленькая";
                            if (choosed_size == 2) added_pizza.Size = "Средняя";
                            if (choosed_size == 3) added_pizza.Size = "Большая";

                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\nХотите ли вы удвоить какие-нибудь ингредиенты(введите да/нет): ");

                            string answer_double = Console.ReadLine().Trim().ToLower();
                            if (answer_double == "да")
                            {
                                List<Ingredient> doubled_ingredients = new List<Ingredient>();
                                while (true)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                                    PrintAvailableIngredientsToDouble(added_pizza.Ingredients);
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Введите номер ингредиента который хотите удвоить или 0 чтобы выйти: ");

                                    int choosed_ingredient = -1;
                                    if (!int.TryParse(Console.ReadLine().Trim(), out choosed_ingredient) ||
                                        choosed_ingredient < 0 || choosed_ingredient > added_pizza.Ingredients.Count)
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\nТакого ингредиента не существует!");
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                        Console.ReadLine();
                                        continue;
                                    }

                                    if (choosed_ingredient == 0) break;

                                    if (doubled_ingredients.Contains(added_pizza.Ingredients[choosed_ingredient - 1]))
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\nДанный ингредиент уже удвоен!");
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                        Console.ReadLine();
                                        continue;
                                    }

                                    added_pizza.Ingredients.Add(added_pizza.Ingredients[choosed_ingredient - 1]);
                                    doubled_ingredients.Add(added_pizza.Ingredients[choosed_ingredient - 1]);

                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("\nИнгредиент успешно удвоен!");
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                    Console.ReadLine();
                                }
                            }

                            selected_pizzas.Add(added_pizza);
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nПицца успешно добавлена!");
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("Нажмите Enter чтобы продолжить...");
                            Console.ReadLine();
                        }
                        break;
                    case "2":
                        if (Available_bases.Count == 0)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("=== СОЗДАНИЕ СВОЕЙ ПИЦЦЫ ===\n");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nНевозможно создать пиццу без основы! Сначала добавьте основу для пиццы.");
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
                            Console.WriteLine("=== СОЗДАНИЕ СВОЕЙ ПИЦЦЫ ===\n");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Введите название пиццы или 0 чтобы отменить создание: ");
                            Console.ResetColor();

                            name = Console.ReadLine().Trim();

                            if (string.IsNullOrEmpty(name))
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("=== СОЗДАНИЕ СВОЕЙ ПИЦЦЫ ===\n");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Название не может быть пустым!\n");
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                Console.ResetColor();
                                Console.ReadLine();
                                continue;
                            }

                            if (name == "0") break;

                            name = char.ToUpper(name[0]) + name.Substring(1);

                            if (selected_pizzas.Exists(p => p.Name == name))
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("=== СОЗДАНИЕ СВОЕЙ ПИЦЦЫ ===\n");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Пицца с названием {name} уже существует!");
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                Console.ResetColor();
                                Console.ReadLine();
                                continue;
                            }

                            break;
                        }

                        if (name != "0")
                        {
                            int base_index;
                            Base selected_base = new Base();
                            while (true)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                                PrintAvailableBases();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("\nВыберите основу для пиццы, введите номер или 0 чтобы отменить создание: ");
                                Console.ResetColor();

                                if (!int.TryParse(Console.ReadLine(), out base_index) || base_index < 0 || base_index > Available_bases.Count)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Неверный выбор основы!");
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                    Console.ResetColor();
                                    Console.ReadLine();
                                    continue;
                                }

                                if (base_index == 0) break;

                                selected_base = Available_bases[base_index - 1];
                                break;
                            }

                            if (base_index != 0)
                            {
                                int choosed_size = -1;
                                while (true)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("\n1. Маленькая");
                                    Console.WriteLine("2. Средняя");
                                    Console.WriteLine("3. Большая\n");
                                    Console.WriteLine("Каккого размера будет пицца, введите номер или 0 чтобы выбрать по умолчанию: ");
                                    Console.ResetColor();

                                    if (!int.TryParse(Console.ReadLine().Trim(), out choosed_size) ||
                                        choosed_size < 0 || choosed_size > 3)
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\nТакого размера пиццы не существует!");
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                        Console.ReadLine();
                                        continue;
                                    }

                                    break;
                                }

                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Хотите ли вы добавить бортик с ингредиентами?");
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine("По умолчанию стоит классический бортик.");
                                Console.Write("Введите да/нет:");

                                Border selected_border = Available_borders[0];
                                string answer = Console.ReadLine().Trim().ToLower();
                                if (answer == "да")
                                {
                                    while (true)
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                                        PrintAvailableBorders();
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.WriteLine("Какой бортик вы хотите выбрать, введите номер или 0 чтобы отменить");
                                        Console.ForegroundColor = ConsoleColor.DarkGray;

                                        int border_choice = -1;
                                        if (!int.TryParse(Console.ReadLine().Trim(), out border_choice) ||
                                            border_choice < 0 || border_choice > Available_borders.Count)
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

                                        if (border_choice == 0) break;

                                        selected_border = Available_borders[border_choice - 1];
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
                                while (true)
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
                                    if (!int.TryParse(Console.ReadLine(), out ingredient_index) ||
                                        ingredient_index < 0 || ingredient_index > Available_ingredients.Count)
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\nНеверный выбор ингредиента!\n");
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                        Console.ResetColor();
                                        Console.ReadLine();
                                        continue;
                                    }

                                    if (ingredient_index == 0) break;

                                    Ingredient selected_ingredient = Available_ingredients[ingredient_index - 1];

                                    if (selected_ingredients.Exists(i => i.Name == selected_ingredient.Name))
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Этот ингредиент уже добавлен!");
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
                                    CoupleBorders = new List<Border>(),
                                    Price = total_price,
                                };

                                new_pizza.CoupleBorders.Add(selected_border);

                                if (choosed_size == 1) new_pizza.Size = "Маленькая";
                                if (choosed_size == 2) new_pizza.Size = "Средняя";
                                if (choosed_size == 3) new_pizza.Size = "Большая";

                                selected_pizzas.Add(new_pizza);

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
                        }
                        break;
                    case "3":
                        int first_couple = -1;
                        int second_couple = -1;
                        int choosed_base = -1;
                        int choosed_border1 = 1;
                        int choosed_border2 = 1;
                        int choice_size = 0;
                        while (true)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("=== СОЗДАНИЕ ПИЦЦЫ ИЗ ПОЛОВИНОК ===");
                            PrintAvailablePizzasCouple();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Какая будет первая половинка введите номер или 0 чтобы отменить:");
                            Console.ResetColor();

                            if (!int.TryParse(Console.ReadLine().Trim(), out first_couple) ||
                                first_couple < 0 || first_couple > Available_pizzas.Count)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("=== СОЗДАНИЕ ПИЦЦЫ ИЗ ПОЛОВИНОК ===");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nТакой пиццы не существует!");
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                Console.ResetColor();
                                Console.ReadLine();
                                continue;
                            }

                            break;
                        }

                        if (first_couple != 0)
                        {
                            while (true)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("=== СОЗДАНИЕ ПИЦЦЫ ИЗ ПОЛОВИНОК ===");
                                PrintAvailablePizzasCouple();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Какая будет вторая половинка введите номер или 0 чтобы отменить:");
                                Console.ResetColor();

                                if (!int.TryParse(Console.ReadLine().Trim(), out second_couple) ||
                                    second_couple < 0 || second_couple > Available_pizzas.Count)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("=== СОЗДАНИЕ ПИЦЦЫ ИЗ ПОЛОВИНОК ===");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nТакой пиццы не существует!");
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                    Console.ResetColor();
                                    Console.ReadLine();
                                    continue;
                                }

                                break;
                            }

                            if (second_couple != 0)
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("=== СОЗДАНИЕ ПИЦЦЫ ИЗ ПОЛОВИНОК ===");
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("\n1. Маленькая");
                                    Console.WriteLine("2. Средняя");
                                    Console.WriteLine("3. Большая");
                                    Console.WriteLine("Какого размера будет пицца:");
                                    Console.ResetColor();

                                    if (!int.TryParse(Console.ReadLine().Trim(), out choice_size) ||
                                        choice_size < 0 || choice_size > 3)
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine("=== СОЗДАНИЕ ПИЦЦЫ ИЗ ПОЛОВИНОК ===");
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\nТакого размера не существует!");
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                        Console.ResetColor();
                                        Console.ReadLine();
                                        continue;
                                    }

                                    break;
                                }

                                while (true)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("=== СОЗДАНИЕ ПИЦЦЫ ИЗ ПОЛОВИНОК ===");
                                    PrintAvailableBases();
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Какую основу будем использовать введите номер или 0 чтобы отменить: ");
                                    Console.ResetColor();

                                    if (!int.TryParse(Console.ReadLine().Trim(), out choosed_base) ||
                                        choosed_base < 0 || choosed_base > Available_bases.Count)
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine("=== СОЗДАНИЕ ПИЦЦЫ ИЗ ПОЛОВИНОК ===");
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\nТакой основы не существует!");
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                        Console.ResetColor();
                                        Console.ReadLine();
                                        continue;
                                    }

                                    break;
                                }

                                if (choosed_base != 0)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("=== СОЗДАНИЕ ПИЦЦЫ ИЗ ПОЛОВИНОК ===");
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("\nХотите ли вы использовать бортик с ингредиентами?(да/нет)");

                                    string choice_border = Console.ReadLine().Trim().ToLower();
                                    if (choice_border == "да")
                                    {
                                        bool adding_border = true;
                                        while (adding_border)
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Cyan;
                                            Console.WriteLine("=== СОЗДАНИЕ ПИЦЦЫ ИЗ ПОЛОВИНОК ===");
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("\n1. Сделать общий бортик");
                                            Console.WriteLine("2. Сделать раздельный бортик");

                                            string choice_border2 = Console.ReadLine().Trim().ToLower();
                                            switch (choice_border2)
                                            {
                                                case "1":
                                                    while (true)
                                                    {
                                                        Console.Clear();
                                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                                        Console.WriteLine("=== СОЗДАНИЕ ПИЦЦЫ ИЗ ПОЛОВИНОК ===");
                                                        PrintAvailableBorders();
                                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                                        Console.WriteLine("\nВведите номер желаемого бортика или 0 чтобы отменить: ");

                                                        if (!int.TryParse(Console.ReadLine().Trim(), out choosed_border1) ||
                                                            choosed_border1 < 0 || choosed_border1 > Available_borders.Count)
                                                        {
                                                            Console.Clear();
                                                            Console.ForegroundColor = ConsoleColor.Cyan;
                                                            Console.WriteLine("=== СОЗДАНИЕ ПИЦЦЫ ИЗ ПОЛОВИНОК ===");
                                                            Console.ForegroundColor = ConsoleColor.Red;
                                                            Console.WriteLine("\nТакого бортика не существует!");
                                                            Console.ForegroundColor = ConsoleColor.DarkGray;
                                                            Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                                            Console.ResetColor();
                                                            Console.ReadLine();
                                                            continue;
                                                        }

                                                        adding_border = false;
                                                        break;
                                                    }
                                                    break;
                                                case "2":
                                                    while (true)
                                                    {
                                                        Console.Clear();
                                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                                        Console.WriteLine("=== СОЗДАНИЕ ПИЦЦЫ ИЗ ПОЛОВИНОК ===");
                                                        PrintAvailableBorders();
                                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                                        Console.WriteLine("\nВведите номер первого желаемого бортика или 0 чтобы отменить: ");

                                                        if (!int.TryParse(Console.ReadLine().Trim(), out choosed_border1) ||
                                                            choosed_border1 < 0 || choosed_border1 > Available_borders.Count)
                                                        {
                                                            Console.Clear();
                                                            Console.ForegroundColor = ConsoleColor.Cyan;
                                                            Console.WriteLine("=== СОЗДАНИЕ ПИЦЦЫ ИЗ ПОЛОВИНОК ===");
                                                            Console.ForegroundColor = ConsoleColor.Red;
                                                            Console.WriteLine("\nТакого бортика не существует!");
                                                            Console.ForegroundColor = ConsoleColor.DarkGray;
                                                            Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                                            Console.ResetColor();
                                                            Console.ReadLine();
                                                            continue;
                                                        }

                                                        break;
                                                    }

                                                    while (true)
                                                    {
                                                        Console.Clear();
                                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                                        Console.WriteLine("=== СОЗДАНИЕ ПИЦЦЫ ИЗ ПОЛОВИНОК ===");
                                                        PrintAvailableBorders();
                                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                                        Console.WriteLine("\nВведите номер второго желаемого бортика: ");

                                                        if (!int.TryParse(Console.ReadLine().Trim(), out choosed_border2) ||
                                                            choosed_border2 < 0 || choosed_border2 > Available_borders.Count)
                                                        {
                                                            Console.Clear();
                                                            Console.ForegroundColor = ConsoleColor.Cyan;
                                                            Console.WriteLine("=== СОЗДАНИЕ ПИЦЦЫ ИЗ ПОЛОВИНОК ===");
                                                            Console.ForegroundColor = ConsoleColor.Red;
                                                            Console.WriteLine("\nТакого бортика не существует!");
                                                            Console.ForegroundColor = ConsoleColor.DarkGray;
                                                            Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                                            Console.ResetColor();
                                                            Console.ReadLine();
                                                            continue;
                                                        }

                                                        adding_border = false;
                                                        break;
                                                    }
                                                    break;
                                                default:
                                                    Console.Clear();
                                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                                    Console.WriteLine("=== СОЗДАНИЕ ПИЦЦЫ ИЗ ПОЛОВИНОК ===");
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("\nТакой команды не существует!");
                                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                                    Console.ResetColor();
                                                    Console.ReadLine();
                                                    break;
                                            }
                                        }
                                    }

                                    Pizza new_pizza1 = new Pizza(Available_pizzas[first_couple - 1]);
                                    Pizza new_pizza2 = new Pizza(Available_pizzas[second_couple - 1]);
                                    Pizza new_pizza = new Pizza()
                                    {
                                        Name = $"{new_pizza1.Name} и {new_pizza2.Name}",
                                        PizzaBase = Available_bases[choosed_base - 1],
                                        CoupleBorders = new List<Border>()
                                    };

                                    new_pizza.CoupleBorders.Add(Available_borders[choosed_border1 - 1]);
                                    new_pizza.CoupleBorders.Add(Available_borders[choosed_border2 - 1]);

                                    new_pizza.Price = CalculatingPizzaCouplePrice(new_pizza1, new_pizza2,
                                        new_pizza.CoupleBorders[0], new_pizza.CoupleBorders[1]);

                                    new_pizza.Ingredients = new List<Ingredient>();

                                    foreach (Ingredient ingr in new_pizza1.Ingredients)
                                    {
                                        new_pizza.Ingredients.Add(ingr);
                                    }
                                    foreach (Ingredient ingr in new_pizza2.Ingredients)
                                    {
                                        new_pizza.Ingredients.Add(ingr);
                                    }
                                    if (choice_size == 1) new_pizza.Size = "Маленькая";
                                    if (choice_size == 2) new_pizza.Size = "Средняя";
                                    if (choice_size == 3) new_pizza.Size = "Большая";

                                    selected_pizzas.Add(new_pizza);
                                }
                            }
                        }
                        break;

                    case "0":
                        making_order = false;
                        break;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nНеверный ввод команды!");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                        Console.ReadLine();
                        break;
                }
            }

            if (selected_pizzas.Count > 0)
            {
                int total_price = CalculatingOrderPrice(selected_pizzas);
                Order new_order = new Order()
                {
                    Pizzas = selected_pizzas,
                    Id = Guid.NewGuid(),
                    TotalPrice = total_price,
                    DateDilivery = new Date()
                };

                bool choosing_date = true;
                while (choosing_date)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n1. Как можно быстрее.");
                    Console.WriteLine("2. Назначить дату и время.");
                    Console.WriteLine("Когда будет удобно доставить заказ? Введите номер: ");
                    Console.ResetColor();

                    string choice_date = Console.ReadLine().Trim();
                    switch (choice_date)
                    {
                        case "1":
                            new_order.DateDilivery.Day = "Сегодня";
                            new_order.DateDilivery.Month = "";
                            new_order.DateDilivery.Hour = "";
                            new_order.DateDilivery.Minutes = "";
                            choosing_date = false;
                            break;
                        case "2":
                            new_order.DateDilivery.Day = "Сегодня";
                            new_order.DateDilivery.Month = "";
                            new_order.DateDilivery.Hour = "";
                            new_order.DateDilivery.Minutes = "";
                            string str_month;
                            string str_day;
                            string str_hour;
                            string str_minutes;
                            while (true)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Введите месяц(в формате ММ) или 0 чтобы назначить дату на ближайшее время: ");
                                Console.ResetColor();

                                str_month = Console.ReadLine().Trim();

                                if (!int.TryParse(str_month, out int choosed_month) ||
                                    choosed_month < 0 || choosed_month > 12)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nТакого месяца не существует!");
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                    Console.ReadLine();
                                    continue;
                                }

                                break;
                            }
                            while (true)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Введите день(в формате ДД) или 0 чтобы назначить дату на ближайшее время: ");
                                Console.ResetColor();

                                str_day = Console.ReadLine().Trim();

                                if (!int.TryParse(str_day, out int choosed_day) ||
                                    choosed_day < 0 || choosed_day > 30)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nТакого дня не существует!");
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                    Console.ReadLine();
                                    continue;
                                }

                                break;
                            }
                            while (true)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Введите час(в формате ЧЧ) или 0 чтобы назначить дату на ближайшее время: ");
                                Console.ResetColor();

                                str_hour = Console.ReadLine().Trim();

                                if (!int.TryParse(str_hour, out int choosed_hour) ||
                                    choosed_hour < 0 || choosed_hour > 24)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nТакого часа не существует!");
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                    Console.ReadLine();
                                    continue;
                                }

                                break;
                            }
                            while (true)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Введите минуты(в формате ММ) или -1 чтобы назначить дату на ближайшее время: ");
                                Console.ResetColor();

                                str_minutes = Console.ReadLine().Trim();

                                if (!int.TryParse(str_minutes, out int choosed_minutes) ||
                                    choosed_minutes < -1 || choosed_minutes > 59)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nТакого дня не существует!");
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                    Console.ReadLine();
                                    continue;
                                }

                                break;
                            }

                            if (str_month != "0" && str_day != "0" && str_hour != "0" && str_minutes != "-1")
                            {
                                new_order.DateDilivery.Day = str_day;
                                new_order.DateDilivery.Month = str_month;
                                new_order.DateDilivery.Hour = str_hour;
                                new_order.DateDilivery.Minutes = str_minutes;
                            }
                            choosing_date = false;
                            break;
                        default:
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nТакой команды не существует!");
                            break;
                    }
                }

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nКомментарий к заказу: ");
                string comment = Console.ReadLine().Trim();

                new_order.Comment = comment;
                Orders.Add(new_order);

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nЗаказ успешно создан!");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== СОЗДАНИЕ ЗАКАЗА ===");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nНе добавлено ни одной пиццы, заказ отменен!");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Нажмите Enter чтобы продолжить...");
            Console.ReadLine();
            return;
        }
    }
}
