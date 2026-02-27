namespace PizzaMaker.Models
{
    internal class Date
    {
        private string _month;
        private string _day;
        private string _hour;
        private string _minutes;

        public string Month
        {
            get { return _month; }
            set { _month = value; }
        }
        public string Day
        {
            get { return _day; }
            set { _day = value; }
        }
        public string Hour
        {
            get { return _hour; }
            set { _hour = value; }
        }
        public string Minutes
        {
            get { return _minutes; }
            set { _minutes = value; }
        }

        public override string ToString()
        {
            return $"{Month}.{Day}. Время: {Hour}:{Minutes}";
        }
    }
}
