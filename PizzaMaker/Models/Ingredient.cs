namespace PizzaMaker.Models
{
    internal class Ingredient
    {
        private string _name;
        private int _price;
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

        public Ingredient() { }

        public Ingredient(Ingredient other)
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
