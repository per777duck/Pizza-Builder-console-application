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
            get {  return _price; }
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
            Console.Clear();
            Console.WriteLine("=== СОЗДАНИЕ НОВОГО ИНГРЕДИЕНТА ===\n");
            Console.WriteLine("\nВведите название ингредиента: ");
            string name = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(name))
            {
                Console.Clear();
                Console.WriteLine("=== СОЗДАНИЕ НОВОГО ИНГРЕДИЕНТА ===\n");
                Console.WriteLine("Название не может быть пустым!\n Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
                return;
            }
            
            name = char.ToUpper(name[0]) + name.Substring(1);

            if (Ingredients.Exists(i =>  i.Name == name))
            {
                Console.Clear();

                Console.WriteLine("=== СОЗДАНИЕ НОВОГО ИНГРЕДИЕНТА ===\n");
                Console.WriteLine($"\nИнгредиент с названием {name} уже существует.");
                return;
            }

            int price = -1;
            bool added = false;
            while (!added)
            {
                Console.Clear();
                Console.WriteLine("=== СОЗДАНИЕ НОВОГО ИНГРЕДИЕНТА ===\n");
                Console.WriteLine("\nВведите стоимость ингредиента: ");
                
                if (!int.TryParse(Console.ReadLine().Trim(), out price) || price < 0)
                {
                    Console.Clear();
                    Console.WriteLine("=== СОЗДАНИЕ НОВОГО ИНГРЕДИЕНТА ===\n");
                    Console.WriteLine("\nСтоимость должна быть целым положительным числом.\n");
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ReadLine();
                    continue;
                }

                added = true;
            }

            Ingredient new_ingridient = new Ingredient
            {
                Name = name,
                Price = price
            };

            Ingredients.Add(new_ingridient);
            Console.Clear();
            Console.WriteLine("=== СОЗДАНИЕ НОВОГО ИНГРЕДИЕНТА ===\n");
            PrintIngredients();
            Console.WriteLine("\nНовый Ингредиент успешно добавлен!\n\n");
            Console.WriteLine("\nНажмите Enter чтобы продолжить...");
            Console.ReadLine();
        }

        public void Removing()
        {
            if (Ingredients.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("=== УДАЛЕНИЕ ИНГРЕДИЕНТА ===\n");
                Console.WriteLine("\nСписок ингредиентов пуст.\nНажмите Enter чтобы продолжить...");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine("=== УДАЛЕНИЕ ИНГРЕДИЕНТА ===\n");
            PrintIngredients ();
            Console.WriteLine("\nКакой ингредиент хотите удалить?\nВведите номер: ");

            int ingredient_to_remove = -1;
            if (!int.TryParse(Console.ReadLine().Trim(), out ingredient_to_remove) ||
                ingredient_to_remove < 1 || ingredient_to_remove > Ingredients.Count)
            {
                Console.Clear();
                Console.WriteLine("=== УДАЛЕНИЕ ИНГРЕДИЕНТА ===\n");
                Console.WriteLine("\nНеверный выбор номера ингредиента!\nНажмите Enter чтобы продолжить...");
                Console.ReadLine();
                return;
            }

            Ingredients.RemoveAt(ingredient_to_remove - 1);
            Console.Clear();
            Console.WriteLine("=== УДАЛЕНИЕ ИНГРЕДИЕНТА ===\n");
            Console.WriteLine("\nИнгредиент успешно удалена!\nНажмите Enter чтобы продолжить...");
            Console.ReadLine();
        }

        public void Editing()
        {
            if (Ingredients.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА ===\n");
                Console.WriteLine("\nСписок ингредиентов пуст.\nНажмите Enter чтобы продолжить...");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine("=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА ===\n\n");
            PrintIngredients();
            Console.WriteLine("\nКакой ингредиент вы хотите изменить?\nВведите номер:\n");

            int ingredient_to_edit = -1;
            if (!int.TryParse(Console.ReadLine().Trim(), out ingredient_to_edit) ||
                ingredient_to_edit < 1 || ingredient_to_edit > Ingredients.Count)
            {
                Console.Clear();
                Console.WriteLine("=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА ===\n");
                Console.WriteLine("\nТакого ингредиента нет.\nНажмите Enter чтобы продолжить...");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА: {Ingredients[ingredient_to_edit - 1]} ===\n");
            Console.WriteLine("\nЧто вы хотите изменить:\n");
            Console.WriteLine("1.Название\n");
            Console.WriteLine("2.Стоимость\n");
            Console.WriteLine("0.Отменить изменения\n");
            Console.WriteLine("\nВведите номер: ");

            string editing_parameter = Console.ReadLine().Trim();

            switch (editing_parameter)
            {
                case "1":
                    {
                        Console.Clear();
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА: {Ingredients[ingredient_to_edit - 1]} ===\n");
                        Console.WriteLine("\nВведите новое название: ");
                        string new_name = Console.ReadLine().Trim();

                        if (string.IsNullOrEmpty(new_name))
                        {
                            Console.Clear();
                            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА: {Ingredients[ingredient_to_edit - 1]} ===\n");
                            Console.WriteLine("\nНазвание не может быть пустым. Нажмите Enter чтобы продолжить...");
                            Console.ReadLine();
                            return;
                        }

                        new_name = char.ToUpper(new_name[0]) + new_name.Substring(1);

                        if (Ingredients.Exists(b => b.Name == new_name))
                        {
                            Console.Clear();
                            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА: {Ingredients[ingredient_to_edit - 1]} ===\n");
                            Console.WriteLine($"Ингредиент с названием {new_name} уже существует.\n");
                            Console.WriteLine("Нажмите Enter чтобы продолжить...");
                            Console.ReadLine();
                            return;
                        }

                        Console.Clear();
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА: {Ingredients[ingredient_to_edit - 1]} ===\n");
                        Ingredients[ingredient_to_edit - 1].Name = new_name;
                        Console.WriteLine("\nНазвание успешно изменено!");
                        Console.WriteLine($"Новое название: {new_name}.\nНажмите Enter чтобы продолжить...");
                        Console.ReadLine();
                        break;
                    }
                case "2":
                    {
                        Console.Clear();
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА: {Ingredients[ingredient_to_edit - 1]} ===\n");
                        Console.WriteLine("\nВведите стоимость ингредиента:\n");

                        int new_price = -1;
                        if (!int.TryParse(Console.ReadLine().Trim(), out new_price) || new_price < 0)
                        {
                            Console.Clear();
                            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА: {Ingredients[ingredient_to_edit - 1]} ===\n");
                            Console.WriteLine("\nСтоимость должна быть целым положительным числом.\nНажмите Enter чтобы продолжить...");
                            Console.ReadLine();
                            return;
                        }

                        Ingredients[ingredient_to_edit - 1].Price = new_price;
                        Console.Clear();
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА: {Ingredients[ingredient_to_edit - 1]} ===\n");
                        Console.WriteLine("\nСтоимость успешно изменена!");
                        Console.WriteLine($"Новая стоимость: {new_price}.\nНажмите Enter чтобы продолжить...");
                        Console.ReadLine();
                        break;
                    }
                case "0":
                    {
                        Console.Clear();
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА: {Ingredients[ingredient_to_edit - 1]} ===\n");
                        Console.WriteLine("\nИзменение отменено.\nНажмите Enter чтобы продолжить...");
                        Console.ReadLine();
                        break;
                    }
                default:
                    {
                        Console.Clear();
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ИНГРЕДИЕНТА: {Ingredients[ingredient_to_edit - 1]} ===\n");
                        Console.WriteLine("'\nТакой команды не существует.\nНажмите Enter чтобы продолжить...");
                        Console.ReadLine();
                        break;
                    }
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
                Console.WriteLine($"{i + 1}." + Bases[i].ToString());
            }
        }

        private int GetClassicBasePrice()
        {
            Base classicBase = Bases.Find(b => b.Name == CLASSIC_BASE_NAME);
            return classicBase?.Price ?? 0;
        }

        public void Adding()
        {
            Console.Clear();
            Console.WriteLine("=== ДОБАВЛЕНИЕ НОВОЙ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");
            Console.WriteLine("\nВведите название основы для пиццы: ");
            string name = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(name))
            {
                Console.Clear();
                Console.WriteLine("=== ДОБАВЛЕНИЕ НОВОЙ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");
                Console.WriteLine("\nНазвание не может быть пустым.\nНажмите Enter чтобы продолжить...");
                Console.ReadLine();
                return;
            }

            name = char.ToUpper(name[0]) + name.Substring(1); 

            if (Bases.Exists(b => b.Name == name)) 
            {
                Console.Clear();
                Console.WriteLine("=== ДОБАВЛЕНИЕ НОВОЙ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");
                Console.WriteLine($"Основа с названием {name} уже существует.\nНажмите Enter чтобы продолжить...");
                Console.ReadLine();
                return;
            }

            int price = -1;
            bool added = false;
            while (!added)
            {
                Console.Clear();
                Console.WriteLine("=== ДОБАВЛЕНИЕ НОВОЙ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");
                Console.WriteLine("\nВведите стоимость основы для пиццы: ");

                if (!int.TryParse(Console.ReadLine().Trim(), out price) || price < 0)
                {
                    Console.Clear();
                    Console.WriteLine("=== ДОБАВЛЕНИЕ НОВОЙ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");
                    Console.WriteLine("Стоимость должна быть целым положительным числом.\n");
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ReadLine();
                    continue;
                }
                    
                if (name != CLASSIC_BASE_NAME)
                {
                    int classic_price = GetClassicBasePrice();
                    int max_price = (int) (classic_price * 1.2);

                    if (price > max_price)
                    {
                        Console.Clear();
                        Console.WriteLine("=== ДОБАВЛЕНИЕ НОВОЙ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");
                        Console.WriteLine($"\nЦена не может быть больше {max_price} руб.");
                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                        Console.ReadLine();
                        continue;
                    }
                }

                added = true;
            }

            Base new_base = new Base
            {
                Name = name,
                Price = price
            };

            Bases.Add(new_base);
            Console.Clear();
            Console.WriteLine("=== ДОБАВЛЕНИЕ НОВОЙ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");
            PrintBases();
            Console.WriteLine("\nНовая основа для пиццы успешно добавлена!");
            Console.WriteLine("Нажмите Enter чтобы продолжить...");
            Console.ReadLine();
        }

        public void Removing()
        {
            if (Bases.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("=== УДАЛЕНИЕ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");
                Console.WriteLine("\nСписок основ пуст.\nНажмите Enter чтобы продолжить...");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine("=== УДАЛЕНИЕ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n\n");
            PrintBases();
            Console.WriteLine("\nКакую основу для пиццы хотите удалить?\nВведите номер: ");

            int base_to_remove = -1;
            if (!int.TryParse(Console.ReadLine().Trim(), out base_to_remove) || 
                base_to_remove < 1 || base_to_remove > Bases.Count)
            {
                Console.Clear();
                Console.WriteLine("=== УДАЛЕНИЕ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");
                Console.WriteLine("\nНеверный выбор номера основы!\nНажмите Enter чтобы продолжить...");
                Console.ReadLine();
                return;
            }

            if (Bases[base_to_remove - 1].Name == CLASSIC_BASE_NAME)
            {
                Console.Clear();
                Console.WriteLine("=== УДАЛЕНИЕ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");
                Console.WriteLine("\nПредупреждение: Классическую основу нельзя удалить!\n");
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
                return;
            }

            Bases.RemoveAt(base_to_remove - 1);
            Console.Clear();
            Console.WriteLine("=== УДАЛЕНИЕ ОСНОВЫ ДЛЯ ПИЦЦЫ ===\n");
            Console.WriteLine("Основа для пиццы успешно удалена!\nНажмите Enter чтобы продолжить...");
            Console.ReadLine();
        }

        public void Editing()
        {
            if (Bases.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("=== РЕДАКТИРОВАНИЕ ОСНОВЫ ===\n");
                Console.WriteLine("\nСписок основ пуст.\nНажмите Enter чтобы продолжить...");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine("=== РЕДАКТИРОВАНИЕ ОСНОВЫ ===\n\n");
            PrintBases();
            Console.WriteLine("\nКакую основу для пиццы вы хотите изменить?\nВведите номер: ");

            int base_to_edit = -1;
            if (!int.TryParse(Console.ReadLine().Trim(), out base_to_edit) || 
                base_to_edit < 1 || base_to_edit > Bases.Count) 
            { 
                Console.Clear();
                Console.WriteLine("=== РЕДАКТИРОВАНИЕ ОСНОВЫ ===\n\n");
                Console.WriteLine("\nНеверный выбор номера основы!\nНажмите Enter чтобы продолжить...");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ОСНОВЫ: {Bases[base_to_edit - 1]} ===\n");
            Console.WriteLine("\nЧто вы хотите изменить:\n");
            Console.WriteLine("1.Название\n");
            Console.WriteLine("2.Стоимость\n");
            Console.WriteLine("0.Отменить изменения\n");
            Console.WriteLine("\nВведите номер: ");

            string editing_parameter = Console.ReadLine().Trim();

            switch (editing_parameter)
            {
                case "1":
                    {
                        Console.Clear();
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ОСНОВЫ: {Bases[base_to_edit - 1]} ===\n");
                        Console.WriteLine("\nВведите новое название: ");

                        string new_name = Console.ReadLine().Trim();
                        if (string.IsNullOrEmpty(new_name))
                        {
                            Console.Clear();
                            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ОСНОВЫ: {Bases[base_to_edit - 1]} ===\n");
                            Console.WriteLine("\nНазвание не может быть пустым!\nНажмите Enter чтобы продолжить...");
                            Console.ReadLine();
                            return;
                        }

                        new_name = char.ToUpper(new_name[0]) + new_name.Substring(1);

                        if (Bases.Exists(b => b.Name == new_name))
                        {
                            Console.Clear();
                            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ОСНОВЫ: {Bases[base_to_edit - 1]} ===\n");
                            Console.WriteLine($"\nОснова с названием {new_name} уже существует.\n");
                            Console.WriteLine("Нажмите Enter чтобы продолжить...");
                            Console.ReadLine();
                            return;
                        }

                        Bases[base_to_edit - 1].Name = new_name;
                        Console.Clear();
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ОСНОВЫ: {Bases[base_to_edit - 1]} ===\n");
                        Console.WriteLine("\nНазвание успешно изменено!\n");
                        Console.WriteLine($"Новое название: {new_name}.\nНажмите Enter чтобы продолжить...");
                        Console.ReadLine();
                        break;
                    }
                case "2":
                    {
                        Console.Clear();
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ОСНОВЫ: {Bases[base_to_edit - 1]} ===\n");
                        Console.WriteLine("Введите стоимость основы для пиццы:\n");
                        int new_price = -1;
                        if (!int.TryParse(Console.ReadLine().Trim(), out new_price) || new_price < 0)
                        {
                            Console.Clear();
                            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ОСНОВЫ: {Bases[base_to_edit - 1]} ===\n");
                            Console.WriteLine("\nСтоимость должна быть целым положительным числом.\n");
                            Console.WriteLine("Нажмите Enter чтобы продолжить...");
                            Console.ReadLine();
                            return;
                        }

                        if (Bases[base_to_edit - 1].Name != CLASSIC_BASE_NAME)
                        {
                            int classic_price = GetClassicBasePrice();
                            int max_price = (int)(classic_price * 1.2);

                            if (new_price > max_price)
                            {
                                Console.Clear();
                                Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ОСНОВЫ: {Bases[base_to_edit - 1]} ===\n");
                                Console.WriteLine($"\nЦена не может быть больше {max_price} руб.\n");
                                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                Console.ReadLine();
                                return;
                            }
                        }

                        Bases[base_to_edit - 1].Price = new_price;
                        Console.Clear();
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ОСНОВЫ: {Bases[base_to_edit - 1]} ===\n");
                        Console.WriteLine("\nСтоимость успешно изменена!\nНажмите Enter чтобы продолжить...");
                        Console.ReadLine();
                        break;
                    }
                case "0":
                    {
                        Console.Clear();
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ОСНОВЫ: {Bases[base_to_edit - 1]} ===\n");
                        Console.WriteLine("\nИзменение отменено.\nНажмите Enter чтобы продолжить...");
                        Console.ReadLine();
                        break;
                    }
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
            Console.WriteLine("Текущие ингредиенты:\n");
            for (int i = 0; i < pizza.Ingredients.Count; i++)
            {
                Console.WriteLine($"{i + 1}.{pizza.Ingredients[i].Name}");
            }
        }

        public void PrintPizzaStructure(Pizza pizza)
        {
            Console.Clear();
            Console.WriteLine($"=== СОСТАВ ПИЦЦЫ: {pizza.Name} ===\n");
            Console.WriteLine($"Используемая основа: {pizza.PizzaBase} | цена: {pizza.PizzaBase.Price}.\n");
            Console.WriteLine("Используемые ингредиенты:\n");
            for (int i = 0; i < pizza.Ingredients.Count; i++) 
            {
                Console.WriteLine($"{i + 1}." + pizza.Ingredients[i].ToString());
            }
        }

        public void PrintAvailableBases()
        {
            Console.WriteLine("Доступные основы:\n");
            for (int i = 0; i < Available_bases.Count; i++)
            {
                Console.WriteLine($"{i + 1}." + Available_bases[i].ToString());
            }
        }

        public void PrintAvailableIngredients()
        {
            Console.WriteLine("Доступные ингредиенты:\n");
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
                Console.WriteLine("=== СОЗДАНИЕ НОВОЙ ПИЦЦЫ ===\n");
                Console.WriteLine("\nНевозможно создать пиццу без основы! Сначала добавьте основу для пиццы.");
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine("=== СОЗДАНИЕ НОВОЙ ПИЦЦЫ ===\n");
            Console.WriteLine("Введите название пиццы: ");
            string name = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(name))
            {
                Console.Clear();
                Console.WriteLine("=== СОЗДАНИЕ НОВОЙ ПИЦЦЫ ===\n");
                Console.WriteLine("\nНазвание не может быть пустым!\n");
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
                return;
            }

            name = char.ToUpper(name[0]) + name.Substring(1);

            if (Pizzas.Exists(p => p.Name == name))
            {
                Console.Clear();
                Console.WriteLine("=== СОЗДАНИЕ НОВОЙ ПИЦЦЫ ===\n");
                Console.WriteLine($"Пицца с названием {name} уже существует");
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n\n");
            PrintAvailableBases();
            Console.WriteLine("\nВыберите основу для пиццы, введите номер: ");

            int base_index;
            if (!int.TryParse(Console.ReadLine(), out base_index) ||
                base_index < 1 || base_index > Available_bases.Count)
            {
                Console.Clear();
                Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                Console.WriteLine("\nНеверный выбор основы!\n");
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
                return;
            }

            Base selected_base = Available_bases[base_index - 1];

            List<Ingredient> selected_ingredients = new List<Ingredient>();
            bool adding = true;
            while (adding)
            {
                Console.Clear();
                Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                Console.WriteLine($"\nВыбраная основа: {selected_base.Name}\n");
                PrintAvailableIngredients();
                Console.WriteLine("\nВыберите ингредиент для добавления, введите номер или 0 для завершения: ");

                int ingredient_index;
                if (!int.TryParse(Console.ReadLine(), out ingredient_index) ||
                    ingredient_index < 0 || ingredient_index > Available_ingredients.Count)
                {
                    Console.Clear();
                    Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                    Console.WriteLine("\nНеверный выбор ингредиента!\n");
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
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
                    Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                    Console.WriteLine("\nЭтот ингредиент уже добавлен!\n");
                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                    Console.ReadLine();
                    continue;
                }

                selected_ingredients.Add(selected_ingredient);
                Console.Clear();
                Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                Console.WriteLine($"Ингредиент {selected_ingredient.Name} успешно добавлен!\n");

                Console.Clear();
                Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
                Console.WriteLine($"Основа: {selected_base.Name}\n");
                Console.WriteLine("Добавленные ингредиенты:\n");
                foreach (Ingredient ingr in selected_ingredients)
                {
                    Console.WriteLine($" - {ingr.Name} | цена: {ingr.Price} руб.\n");
                }
                int current_price = CalculatingPizzaPrice(selected_base, selected_ingredients);
                Console.WriteLine($"Текущая стоимость пиццы: {current_price} руб.");
                Console.WriteLine("Нажмите Enter для продолжения...");
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
            Console.WriteLine($"=== СОЗДАНИЕ ПИЦЦЫ: {name} ===\n");
            Console.WriteLine($"Основа: {selected_base.Name}\n");
            Console.WriteLine("Добавленные ингредиенты:\n");
            foreach (Ingredient ingr in selected_ingredients)
            {
                Console.WriteLine($" - {ingr.Name} | цена: {ingr.Price} руб.\n");
            }
            Console.WriteLine($"Текущая стоимость пиццы: {total_price} руб.\n");
            Console.WriteLine("Новая пицца успешно создана!\n");
            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
        }

        public void Removing()
        {
            if (Pizzas.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("=== УДАЛЕНИЕ ПИЦЦЫ ===\n");
                Console.WriteLine("\nСписок пицц пуст.\n");
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine("=== УДАЛЕНИЕ ПИЦЦЫ ===\n\n");
            PrintPizzas();
            Console.WriteLine("\nВведите номер пиццы, которую хотите удалить: ");

            int pizza_to_delete = -1;
            if (!int.TryParse(Console.ReadLine().Trim(), out pizza_to_delete) ||
                pizza_to_delete < 1 || pizza_to_delete > Pizzas.Count)
            {
                Console.Clear();
                Console.WriteLine("=== УДАЛЕНИЕ ПИЦЦЫ ===\n");
                Console.WriteLine("\nНеверный выбор пиццы!\n");
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
                return;
            }

            Pizzas.RemoveAt(pizza_to_delete);
            Console.Clear();
            Console.WriteLine("=== УДАЛЕНИЕ ПИЦЦЫ ===\n");
            Console.WriteLine("\nПицца успешно удалена!\n");
            Console.WriteLine("Нажмите Enter чтобы продолжить...");
            Console.ReadLine();
        }

        public void Editing()
        {
            if (Pizzas.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("=== РЕДАКТИРОВАНИЕ ПИЦЦЫ ===\n");
                Console.WriteLine("\nСписок пицц пуст.");
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine("=== РЕДАКТИРОВАНИЕ ПИЦЦЫ ===\n\n");
            PrintPizzas();
            Console.WriteLine("\nВведите номер пиццы, которую хотите изменить: ");

            int pizza_to_edit = -1;
            if (!int.TryParse(Console.ReadLine().Trim(), out pizza_to_edit) ||
                pizza_to_edit < 1 || pizza_to_edit > Pizzas.Count)
            {
                Console.Clear();
                Console.WriteLine("=== РЕДАКТИРОВАНИЕ ПИЦЦЫ ===\n");
                Console.WriteLine("\nНеверный выбор пиццы!\n");
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
            Console.WriteLine("\nЧто вы хотите изменить:\n");
            Console.WriteLine("1.Название\n");
            Console.WriteLine("2.Состав\n");
            Console.WriteLine("0.Отменить изменения\n");
            Console.WriteLine("\nВведите номер: ");

            string editing_parameter1 = Console.ReadLine().Trim();

            switch (editing_parameter1)
            {
                case "1":
                    {
                        Console.Clear();
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                        Console.WriteLine("\nВведите новое название: ");

                        string new_name = Console.ReadLine().Trim();
                        if (string.IsNullOrEmpty(new_name))
                        {
                            Console.Clear();
                            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                            Console.WriteLine("\nНазвание не может быть пустым!\nНажмите Enter чтобы продолжить...");
                            Console.ReadLine();
                            return;
                        }

                        new_name = char.ToUpper(new_name[0]) + new_name.Substring(1);

                        if (Pizzas.Exists(b => b.Name == new_name))
                        {
                            Console.Clear();
                            Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                            Console.WriteLine($"\nПицца с названием {new_name} уже существует.\n");
                            Console.WriteLine("Нажмите Enter чтобы продолжить...");
                            Console.ReadLine();
                            return;
                        }

                        Pizzas[pizza_to_edit - 1].Name = new_name;
                        Console.Clear();
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                        Console.WriteLine("\nНазвание успешно изменено!\n");
                        Console.WriteLine($"Новое название: {new_name}.\nНажмите Enter чтобы продолжить...");
                        Console.ReadLine();
                        break;
                    }
                case "2":
                    {
                        Console.Clear();
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                        Console.WriteLine("\nЧто вы хотите поменять?\n");
                        Console.WriteLine("1.Ингредиенты\n");
                        Console.WriteLine("2.Основы(только изменение)\n");
                        Console.WriteLine("0.Отменить изменения\n");
                        Console.WriteLine("\nВведите номер: ");

                        string editing_paramter2 = Console.ReadLine().Trim();
                        switch (editing_paramter2)
                        {
                            case "1":
                                {
                                    Console.Clear();
                                    Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                    Console.WriteLine("\nЧто вы хотите сделать?\n");
                                    Console.WriteLine("1.Добавить ингредиенты\n");
                                    Console.WriteLine("2.Удалить ингредиенты\n");
                                    Console.WriteLine("0.Отменить изменения\n");
                                    Console.WriteLine("\nВведите номер: ");

                                    string editing_paramter3 = Console.ReadLine().Trim();
                                    switch(editing_paramter3)
                                    {
                                        case "1":
                                            {
                                                Console.Clear();
                                                Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");

                                                List<Ingredient> selected_ingredients = Pizzas[pizza_to_edit - 1].Ingredients;
                                                bool adding = true;
                                                while (adding)
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1].Name} ===\n");
                                                    Console.WriteLine($"\nОснова пиццы: {Pizzas[pizza_to_edit - 1].PizzaBase}\n");
                                                    PrintAvailableIngredients();
                                                    Console.WriteLine("\nВыберите ингредиент для добавления, введите номер или 0 для завершения: ");

                                                    int ingredient_index;
                                                    if (!int.TryParse(Console.ReadLine(), out ingredient_index) ||
                                                        ingredient_index < 0 || ingredient_index > Available_ingredients.Count)
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                                        Console.WriteLine("\nНеверный выбор ингредиента!\n");
                                                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
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
                                                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                                        Console.WriteLine("\nЭтот ингредиент уже добавлен!\n");
                                                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                                        Console.ReadLine();
                                                        continue;
                                                    }

                                                    selected_ingredients.Add(selected_ingredient);

                                                    Console.Clear();
                                                    Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                                    Console.WriteLine($"\nОснова пиццы: {Pizzas[pizza_to_edit - 1].PizzaBase}\n");
                                                    Console.WriteLine("Добавленные ингредиенты:\n");
                                                    foreach (Ingredient ingr in selected_ingredients)
                                                    {
                                                        Console.WriteLine($" - {ingr.Name} | цена: {ingr.Price} руб.\n");
                                                    }
                                                    int current_price = CalculatingPizzaPrice(Pizzas[pizza_to_edit - 1].PizzaBase, selected_ingredients);
                                                    Console.WriteLine($"Текущая стоимость пиццы: {current_price} руб.");
                                                    Console.WriteLine("Нажмите Enter для продолжения...");
                                                    Console.ReadLine();
                                                }

                                                int total_price = CalculatingPizzaPrice(Pizzas[pizza_to_edit - 1].PizzaBase, selected_ingredients);
                                                Console.Clear();
                                                Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                                Console.WriteLine($"\nОснова пиццы: {Pizzas[pizza_to_edit - 1].PizzaBase}\n");
                                                Console.WriteLine("Добавленные ингредиенты:\n");
                                                foreach (Ingredient ingr in selected_ingredients)
                                                {
                                                    Console.WriteLine($" - {ingr.Name} | цена: {ingr.Price} руб.\n");
                                                }
                                                Console.WriteLine($"Текущая стоимость пиццы: {total_price} руб.");
                                                Console.WriteLine("Нажмите Enter для продолжения...");
                                                Console.ReadLine();
                                                break;
                                            }
                                        case "2":
                                            {
                                                bool removing = true;
                                                while (removing)
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n\n");
                                                    PrintIngredients(Pizzas[pizza_to_edit - 1]);
                                                    Console.WriteLine("Введите номер ингредиента или 0 чтобы закончить удаление: ");

                                                    if (!int.TryParse(Console.ReadLine().Trim(), out int index_ingr) ||
                                                        index_ingr < 0 || index_ingr > Pizzas[pizza_to_edit - 1].Ingredients.Count)
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                                        Console.WriteLine("Такого ингредиента не существует!\n");
                                                        Console.WriteLine("Нажмите Enter для продолжения...");
                                                        Console.ReadLine();
                                                        continue;
                                                    }

                                                    if (index_ingr == 0)
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                                        Console.WriteLine("Удаление прекращено!");
                                                        Console.WriteLine("Нажмите Enter для продолжения...");
                                                        Console.ReadLine();
                                                        removing = false;
                                                        return;
                                                    }

                                                    Pizzas[pizza_to_edit - 1].Ingredients.RemoveAt(index_ingr);

                                                    Console.Clear();
                                                    Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                                    Console.WriteLine("Ингредиент успешно удален!");
                                                    Console.WriteLine("Нажмите Enter для продолжения...");
                                                    Console.ReadLine();
                                                }
                                                break;
                                            }
                                        case "0":
                                            {
                                                Console.Clear();
                                                Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                                Console.WriteLine("\nРедактирование отменено\n");
                                                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                                Console.ReadLine();
                                                break;
                                            }
                                        default:
                                            {
                                                Console.Clear();
                                                Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                                Console.WriteLine("\nТакой команды не существует!\n");
                                                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                                Console.ReadLine();
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case "2":
                                {
                                    Console.Clear();
                                    Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n\n");
                                    PrintAvailableBases();
                                    Console.WriteLine("Какую основу вы хотите выбрать, введите номер: ");

                                    if (!int.TryParse(Console.ReadLine().Trim(), out int index_base) ||
                                        index_base < 1 || index_base > Available_bases.Count)
                                    {
                                        Console.Clear();
                                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                        Console.WriteLine("Такой основы для пиццы не существует!\n");
                                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                        Console.ReadLine();
                                        return;
                                    }

                                    Pizzas[pizza_to_edit - 1].PizzaBase = Available_bases[index_base - 1];

                                    Console.Clear();
                                    Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                                    Console.WriteLine("Основа успешно изменена!\n");
                                    Console.WriteLine("Нажмите Enter чтобы продолжить...");
                                    Console.ReadLine();
                                    break;
                                }
                        }

                        break;
                    }
                case "0":
                    {
                        Console.Clear();
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                        Console.WriteLine("Изменение отменено\n");
                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                        Console.ReadLine();
                        break;
                    }
                default:
                    {
                        Console.Clear();
                        Console.WriteLine($"=== РЕДАКТИРОВАНИЕ ПИЦЦЫ: {Pizzas[pizza_to_edit - 1]} ===\n");
                        Console.WriteLine("\nНеверный ввод команды!\n");
                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
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

            ManageIngredients ingredients_manager = new ManageIngredients(ingredients); ;
            ManageBase base_manager = new ManageBase(bases);
            ManagePizza pizza_manager = new ManagePizza(ingredients, bases);

            Console.WriteLine("=== Добро пожаловать в онлайн пиццерию 'The Dalmatian Pizza' ===\n");
            Console.WriteLine("\t\tНажмите Enter чтобы продолжить...");
            Console.ReadLine();

            bool is_working = true;
            while (is_working)
            {   
                string first_choice = "";
                Console.Clear();
                Console.WriteLine("=== The Dalmatian Pizza ===\n");
                Console.WriteLine("\n1.Управление ингредиентами\n");
                Console.WriteLine("2.Управление основами для пиццы\n");
                Console.WriteLine("3.Управление пиццами\n");
                Console.WriteLine("4.Просмотр списков(ингредиенты, основы, пиццы)\n");
                Console.WriteLine("0.Выйти из приложения\n");
                Console.WriteLine("\nВыберите действие и введите его номер: ");
                
                first_choice = Console.ReadLine().Trim();
                string second_choice = "";

                switch (first_choice)
                {
                    case "1":
                        {
                            Console.Clear();
                            Console.WriteLine("=== УПРАВЛЕНИЕ ИНГРЕДИЕНТАМИ ===\n");
                            Console.WriteLine("\n1.Добавить ингредиент\n");
                            Console.WriteLine("2.Редактировать ингредиент\n");
                            Console.WriteLine("3.Удалить ингредиент\n");
                            Console.WriteLine("0.Вернуться назад\n");
                            Console.WriteLine("\nВыберите действие и введите его номер: ");

                            second_choice = Console.ReadLine().Trim();

                            switch (second_choice)
                            {
                                case "1":
                                    {
                                        ingredients_manager.Adding();
                                        break;
                                    }
                                case "2":
                                    {
                                        ingredients_manager.Editing();
                                        break;
                                    }
                                case "3": 
                                    {
                                        ingredients_manager.Removing();
                                        break;
                                    }
                                case "0":
                                    {
                                        break;
                                    }
                                default:
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Такой команды не существует!\nНажмите Enter для продолжения...");
                                        Console.ReadLine();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "2":
                        {
                            Console.Clear();
                            Console.WriteLine("=== УПРАВЛЕНИЕ ОСНОВАМИ ===\n");
                            Console.WriteLine("\n1.Добавить основу\n");
                            Console.WriteLine("2.Редактировать основы\n");
                            Console.WriteLine("3.Удалить основу\n");
                            Console.WriteLine("0.Вернуться назад\n");
                            Console.WriteLine("\nВыберите действие и введите его номер: ");

                            second_choice = Console.ReadLine().Trim();

                            switch (second_choice)
                            {
                                case "1":
                                    {
                                        base_manager.Adding();
                                        break;
                                    }
                                case "2":
                                    {
                                        base_manager.Editing();
                                        break;
                                    }
                                case "3":
                                    {
                                        base_manager.Removing();
                                        break;
                                    }
                                case "0":
                                    {
                                        break;
                                    }
                                default:
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Такой команды не существует!\nНажмите Enter для продолжения...");
                                        Console.ReadLine();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "3":
                        {
                            Console.Clear();
                            Console.WriteLine("=== УПРАВЛЕНИЕ ПИЦЦАМИ ===\n");
                            Console.WriteLine("\n1.Добавить пиццу\n");
                            Console.WriteLine("2.Редактировать пиццу\n");
                            Console.WriteLine("3.Удалить пиццу\n");
                            Console.WriteLine("0.Вернуться назад\n");
                            Console.WriteLine("\nВыберите действие и введите его номер: ");

                            second_choice = Console.ReadLine().Trim();

                            switch (second_choice)
                            {
                                case "1":
                                    {
                                        pizza_manager.Adding();
                                        break;
                                    }
                                case "2":
                                    {
                                        pizza_manager.Editing();
                                        break;
                                    }
                                case "3":
                                    {
                                        pizza_manager.Removing();
                                        break;
                                    }
                                case "0":
                                    {
                                        break;
                                    }
                                default:
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Такой команды не существует!\nНажмите Enter чтобы продолжить...");
                                        Console.ReadLine();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "4":
                        {
                            Console.Clear();
                            Console.WriteLine("=== СПИСКИ ИНГРЕДИНЕТОВ, ОСНОВ, ПИЦЦ ===\n");
                            Console.WriteLine("\n1.Посмотреть ингредиенты\n");
                            Console.WriteLine("2.Посмотреть основы\n");
                            Console.WriteLine("3.Посмотреть пиццы\n");
                            Console.WriteLine("0.Назад\n");
                            Console.WriteLine("\nВыберите действие и введите его номер: ");

                            second_choice = Console.ReadLine().Trim();

                            switch (second_choice) 
                            {
                                case "1":
                                    {
                                        Console.Clear();
                                        ingredients_manager.PrintIngredients();
                                        Console.WriteLine("\nДля продолжения нажмите Enter...");
                                        Console.ReadLine();
                                        break;
                                    }
                                case "2":
                                    {
                                        Console.Clear();
                                        base_manager.PrintBases();
                                        Console.WriteLine("\nДля продолжения нажмите Enter...");
                                        Console.ReadLine();
                                        break;
                                    }
                                case "3":
                                    {
                                        Console.Clear();
                                        pizza_manager.PrintPizzas();
                                        Console.WriteLine("\nДля продолжения нажмите Enter...");
                                        Console.ReadLine();
                                        break;
                                    }
                                case "0": 
                                    {
                                        break;
                                    }
                                default: 
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Такой команды не существует!\nНажмите Enter...");
                                        Console.ReadLine();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "0": 
                        {
                            is_working = false;
                            break;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("Такой команды не существует!\nНажмите Enter...");
                            Console.ReadLine();
                            break;
                        }
                }
            }
        }
    }
}