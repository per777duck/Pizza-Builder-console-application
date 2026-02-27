using System.Collections.Generic;

namespace PizzaMaker.Models
{
    internal class Pizza
    {
        private string _name;
        private int _price;
        private Base _pizzaBase;
        private Border _pizzaBorder;
        private List<Ingredient> _ingredients;
        private string _size;
        private List<Border> _coupleBorders;

        public List<Border> CoupleBorders
        {
            get { return _coupleBorders; }
            set { _coupleBorders = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }
        public Base PizzaBase
        {
            get { return _pizzaBase; }
            set { _pizzaBase = value; }
        }
        public Border PizzaBorder
        {
            get { return _pizzaBorder; }
            set { _pizzaBorder = value; }
        }
        public List<Ingredient> Ingredients
        {
            get { return _ingredients; }
            set { _ingredients = value; }
        }

        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public Pizza() { }

        public Pizza(Pizza other)
        {
            Name = other.Name;
            PizzaBase = new Base(other.PizzaBase);
            PizzaBorder = new Border(other.PizzaBorder);
            Ingredients = new List<Ingredient>();
            Size = other.Size;
            foreach (Ingredient ingr in other.Ingredients)
            {
                Ingredients.Add(new Ingredient(ingr));
            }
            Price = other.Price;
        }

        public override string ToString()
        {
            return $"{_name} | цена: {_price} руб.";
        }
    }
}
