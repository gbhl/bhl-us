namespace BHL.Search
{
    public class NameHit : Hit
    {
        private string _name = string.Empty;
        private int _count = 0;

        public string Name
        {
            get { return _name; }
            set { _name = value ?? string.Empty; }
        }

        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }
    }
}
