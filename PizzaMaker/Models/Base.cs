using System;

namespace PizzaMaker.Models
{
    internal class Base
    {
        private string _name;
        private int _price;
        private static int _classic_base_price;
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

        public Base() { }

        public Base(Base other)
        {
            Name = other.Name;
            Price = other.Price;
        }
        public override string ToString()
        {
            return $"{_name} | цена: {_price} руб.";
        }
    }
}
