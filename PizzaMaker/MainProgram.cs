using PizzaMaker.Models;
using PizzaMaker.Managers;
using System;
using System.Collections.Generic;

namespace PizzaMaker
{
    internal class MainProgram
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
                new Ingredient {Name = "Базилик", Price = 45},
                new Ingredient {Name = "Креветки", Price = 60},
                new Ingredient {Name = "Крабовое мясо", Price = 100},
                new Ingredient {Name = "Кунжут", Price = 40},
                new Ingredient {Name = "Ананас", Price = 50},
                new Ingredient {Name = "Грибы", Price = 45},
                new Ingredient {Name = "Маслины", Price = 40},
                new Ingredient {Name = "Говяжий фарш", Price = 70},
                new Ingredient {Name = "Курица", Price = 55}
            };
            List<Base> bases = new List<Base>()
            {
                new Base {Name = "Классическая", Price = 250},
                new Base {Name = "Тонкое", Price = 200},
                new Base {Name = "Слоеное", Price = 210},
                new Base {Name = "Черное", Price = 230},
                new Base {Name = "Толстое", Price = 270},
                new Base {Name = "Чесночное", Price = 260}
            };
            List<Pizza> pizzas = new List<Pizza>()
            {
                new Pizza
                {
                    Name = "Маргарита",
                    PizzaBase = bases[0],
                    Size = "Средняя",
                    Ingredients = new List<Ingredient>()
                    {
                        ingredients[0], ingredients[1], ingredients[4]
                    },
                    Price = 345
                },
                new Pizza
                {
                    Name = "Гавайская",
                    PizzaBase = bases[1],
                    Size = "Средняя",
                    Ingredients = new List<Ingredient>()
                    {
                        ingredients[0], ingredients[12], ingredients[8], ingredients[7]
                    }, 
                    Price = 375
                },
                new Pizza
                {
                    Name = "Пеперони",
                    PizzaBase = bases[4],
                    Size = "Средняя",
                    Ingredients = new List<Ingredient>()
                    {
                        ingredients[0], ingredients[1], ingredients[2]
                    },
                    Price = 375
                },
                new Pizza
                {
                    Name = "Куринная",
                    PizzaBase = bases[5],
                    Size = "Средняя",
                    Ingredients = new List<Ingredient>()
                    {
                        ingredients[12], ingredients[7], ingredients[3]
                    },
                    Price = 380
                },
                new Pizza
                {
                    Name = "Морская",
                    PizzaBase = bases[1],
                    Size = "Средняя",
                    Ingredients = new List<Ingredient>()
                    {
                        ingredients[6], ingredients[5], ingredients[0], ingredients[10]
                    },
                    Price = 430
                }
            };
            List<Border> borders = new List<Border>()
            {
                new Border
                {
                    Name = "Классический",
                    AvailablePizzas = new List<Pizza>()
                    {
                        pizzas[1], pizzas[2], pizzas[0]
                    },
                    UsedIngridient = null
                },
                new Border
                {
                    Name = "С кунжутом",
                    AvailablePizzas = new List<Pizza>()
                    {
                        pizzas[1], pizzas[2]
                    },
                    UsedIngridient = ingredients[7],
                    Price = 0
                },
                new Border
                {
                    Name = "С сыром",
                    AvailablePizzas = new List<Pizza>()
                    {
                        pizzas[2]
                    },
                    UsedIngridient = ingredients[0],
                    Price = 0
                },
                new Border
                {
                    Name = "С томатами",
                    AvailablePizzas = new List<Pizza>()
                    {
                        pizzas[0], pizzas[1], pizzas[2]
                    },
                    UsedIngridient = ingredients[1],
                    Price = 0
                },
                new Border
                {
                    Name = "С курицей",
                    AvailablePizzas = new List<Pizza>()
                    {
                        pizzas[3]
                    },
                    UsedIngridient = ingredients[12],
                    Price = 0
                },
                new Border
                {
                    Name = "С базиликом",
                    AvailablePizzas = new List<Pizza>()
                    {
                        pizzas[0], pizzas[1], pizzas[2], pizzas[3], pizzas[4]
                    },
                    UsedIngridient = ingredients[4],
                    Price = 0
                }
            };

            pizzas[0].PizzaBorder = borders[0]; pizzas[1].PizzaBorder = borders[0]; pizzas[2].PizzaBorder = borders[0];
            pizzas[3].PizzaBorder = borders[0]; pizzas[4].PizzaBorder = borders[0];

            ManageIngredients ingredients_manager = new ManageIngredients(ingredients);
            ManageBase base_manager = new ManageBase(bases);
            ManagePizza pizza_manager = new ManagePizza(pizzas, ingredients, bases, borders);
            ManageOrders orders = new ManageOrders(pizzas, ingredients, bases, borders);

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
                Console.WriteLine("5. Сделать заказ");
                Console.WriteLine("0. Выйти из приложения\n");
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
                        Console.WriteLine("=== СПИСКИ ИНГРЕДИЕНТОВ, ОСНОВ, ПИЦЦ, ЗАКАЗОВ ===\n");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("1. Посмотреть полные списки"); 
                        Console.WriteLine("2. Применить фильтрацию");
                        Console.WriteLine("0. Назад\n");
                        Console.Write("Выберите действие: ");
                        Console.ResetColor();
                        string filtrating = Console.ReadLine().Trim();

                        switch(filtrating)
                        { 
                            case "1":
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("=== СПИСКИ ИНГРЕДИЕНТОВ, ОСНОВ, ПИЦЦ, ЗАКАЗОВ ===\n");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("1. Посмотреть ингредиенты");
                                Console.WriteLine("2. Посмотреть основы");
                                Console.WriteLine("3. Посмотреть пиццы");
                                Console.WriteLine("4. Посмотреть сделанные заказы");
                                Console.WriteLine("0. Назад\n");
                                Console.Write("Выберите действие: ");
                                Console.ResetColor();

                                second_choice = Console.ReadLine().Trim();

                                switch (second_choice)
                                {
                                    case "1":
                                        Console.Clear();
                                        ingredients_manager.PrintIngredients();
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        Console.WriteLine("\nНажмите Enter для продолжения...");
                                        Console.ReadLine();
                                        Console.ResetColor();
                                        break;
                                    case "2":
                                        Console.Clear();
                                        base_manager.PrintBases();
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        Console.WriteLine("\nНажмите Enter для продолжения...");
                                        Console.ReadLine();
                                        Console.ResetColor();
                                        break;
                                    case "3":
                                        Console.Clear();
                                        pizza_manager.PrintPizzas();
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        Console.WriteLine("\nНажмите Enter для продолжения...");
                                        Console.ReadLine();
                                        Console.ResetColor();
                                        break;
                                    case "4":
                                        Console.Clear();
                                        orders.PrintOrders();
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
                            case "2":
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("=== СПИСКИ ИНГРЕДИЕНТОВ, ОСНОВ, ПИЦЦ, ЗАКАЗОВ C ФИЛЬТРАЦИЕЙ===\n");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("1. Посмотреть ингредиенты");
                                Console.WriteLine("2. Посмотреть основы");
                                Console.WriteLine("3. Посмотреть пиццы");
                                Console.WriteLine("4. Посмотреть сделанные заказы");
                                Console.WriteLine("0. Назад\n");
                                Console.Write("Выберите действие: ");
                                Console.ResetColor();

                                second_choice = Console.ReadLine().Trim();

                                switch (second_choice)
                                {
                                    case "1":
                                        Console.Clear();
                                        ingredients_manager.FilterIngredients();
                                        break;
                                    case "2":
                                        Console.Clear();
                                        base_manager.FilterBases();
                                        break;
                                    case "3":
                                        Console.Clear();
                                        pizza_manager.FilterPizzas();
                                        break;
                                    case "4":
                                        Console.Clear();
                                        orders.FilterOrders();
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

                    case "5":
                        orders.Adding();
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