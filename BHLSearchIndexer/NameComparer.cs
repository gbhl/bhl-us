namespace BHL.SearchIndexer
{
    public class NameComparer
    {
        bool _sortDescending = false;

        public NameComparer(bool sortDescending = false)
        {
            _sortDescending = sortDescending;
        }

        public int Compare(Name name1, Name name2)
        {
            int ret = name1.id.CompareTo(name2.id);
            if (_sortDescending) ret = ret * -1;
            return ret;
        }
    }
}
