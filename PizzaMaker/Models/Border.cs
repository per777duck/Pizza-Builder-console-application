using System.Collections.Generic;

namespace PizzaMaker.Models
{
    internal class Border
    {
        private string _name;
        private List<Pizza> _availablePizzas;
        private Ingredient _ingredient;
        private int _price;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int Price
        {
            get { return _price; }
            set
            {
                if (_ingredient == null)
                    _price = 0;
                else
                    _price = _ingredient.Price;
            }
        }
        public List<Pizza> AvailablePizzas
        {
            get { return _availablePizzas; }
            set { _availablePizzas = value; }
        }
        public Ingredient UsedIngridient
        {
            get { return _ingredient; }
            set { _ingredient = value; }
        }

        public Border() { }

        public Border(Border other)
        {
            Name = other.Name;
            AvailablePizzas = other.AvailablePizzas;
            UsedIngridient = other.UsedIngridient;
            Price = other.Price;
        }

        public override string ToString()
        {
            return $"{Name} | Цена: {Price} руб.";
        }
    }
}
