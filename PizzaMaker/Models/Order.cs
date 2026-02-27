using System;
using System.Collections.Generic;

namespace PizzaMaker.Models
{
    internal class Order
    {
        private Guid _id;
        private List<Pizza> _pizzas;
        private int _totalPrice;
        private string _comment;
        private Date _date;

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public List<Pizza> Pizzas
        {
            get { return _pizzas; }
            set { _pizzas = value; }
        }
        public int TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }
        public Date DateDilivery
        {
            get { return _date; }
            set { _date = value; }
        }
    }
}
