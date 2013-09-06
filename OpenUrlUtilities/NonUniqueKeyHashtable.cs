using System;
using System.Collections;
using System.Text;

namespace MOBOT.OpenUrl.Utilities
{
    // Borrowed from http://www.csharphelp.com/archives3/archive539.html
    public class NonUniqueHashtable : IEnumerable
    {
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private ArrayList alContainer = null;

        public DictionaryEntry this[int index]
        {
            get { return (DictionaryEntry)alContainer[index]; }
            set { alContainer[index] = value; }
        }

        public NonUniqueHashtable()
        {
            alContainer = new ArrayList();
        }

        public void Add(object Key, object Value)
        {
            DictionaryEntry entry = new DictionaryEntry(Key, Value);
            alContainer.Add(entry);
        }

        public NonUniqueKeyEnumerator GetEnumerator()
        {
            return new NonUniqueKeyEnumerator(this);
        }

        public int Length
        {
            get { return alContainer.Count; }
        }

        //inner class to Enable IEnumerator for our collection
        public class NonUniqueKeyEnumerator : IEnumerator
        {
            private int i; //index;
            private NonUniqueHashtable nuHashtable;

            public NonUniqueKeyEnumerator(NonUniqueHashtable nonUniqueHashtable)
            {
                nuHashtable = nonUniqueHashtable;
                i = -1;
            }

            public void Reset()
            {
                i = -1;
            }

            public bool MoveNext()
            {
                i++;
                return (i < nuHashtable.alContainer.Count);
            }

            public int Current
            {
                get { return (i); }
            }

            object IEnumerator.Current
            {
                get { return (Current); }
            }

        }
    }
}
