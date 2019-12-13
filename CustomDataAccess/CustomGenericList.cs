using System;
using System.Collections.Generic;

namespace CustomDataAccess
{
    [Serializable]
    public class CustomGenericList<T>: List<T>
    {

    }

    /// <summary>
    /// Specifies how elements in a list are sorted.
    /// </summary>
    [Serializable]
    public enum SortOrder
    {
        /// <summary>
        /// The elements are sorted in ascending order.
        /// </summary>
        Ascending = 0,
        /// <summary>
        /// The elements are sorted in descending order.
        /// </summary>
        Descending = 1
    }

    /*
    /// <summary>
    /// Custom generic list. Implements <see cref="ICloneable"/>, <see cref="ICollection"/>, <see cref="IDisposable"/>, <see cref="IEnumerable"/>, <see cref="IList"/> and <see cref="ISortable"/> interfaces.
    /// Facilitates creating a strongly type list using generics.
    /// The list can be sorted dynamically by specifying any public property on the hosted type.
    /// </summary>
    [Serializable]
    public class CustomGenericList<T> : CollectionBase, ICloneable, ICollection, IDisposable, IList, ISortable, IComparable
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomGenericList()
        {
            _SortObjectType = typeof (T);
        }

        #endregion Constructors

        #region Destructor

        /// <summary>
        /// Destructor only executed if the Dispose method does not get executed.
        /// Do not provide destructors in types derived from this class.
        /// </summary>
        ~CustomGenericList()
        {
            Dispose(false);
        }

        #endregion Destructor

        #region Getter/Setter for collection

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// This is a thread-safe operation when the collection is synchronized.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <value>
        /// The element at the specified <paramref name="index"/>.
        /// </value>
        public T this[int index]
        {
            get
            {
                return (T) InnerList[index];
            }
            set
            {
                InnerList[index] = value;
            }
        }

        #endregion Getter/Setter for collection

        #region HashCode

        private void resetHashCode()
        {
            _hashcode = Guid.NewGuid().GetHashCode();
        }

        private int _hashcode = Guid.NewGuid().GetHashCode();

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>Hash code for this instance as an integer.</returns>
        public override int GetHashCode()
        {
            return _hashcode;
        }

        #endregion HashCode

        #region CompareTo

        /// <summary>
        /// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="CustomGenericList"/>.
        /// Compares the element count of each and iteratively compares the elements of this instance with the elements of the specified object.
        /// This is a thread-safe operation when the list is synchronized.
        /// </summary>
        /// <param name="obj">An <see cref="CustomGenericList"/> object to compare with this instance.</param>
        /// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
        public int CompareTo(Object obj)
        {
            if (IsSynchronized)
            {
                lock (syncRoot)
                {
                    return compareTo(obj);
                }
            }
            else
            {
                return compareTo(obj);
            }
        }

        private int compareTo(Object obj)
        {
            bool equal = true;
            if (obj is CustomGenericList<T>)
            {
                CustomGenericList<T> o = (CustomGenericList<T>) obj;

                if (Count == o.Count)
                {
                    int j = Count;
                    // compare each item within the list
                    for (int i = 0; i < j; i++)
                    {
                        if (this[i] is IComparable && o[i] is IComparable)
                        {
                            IComparable a = (IComparable) this[i];
                            IComparable b = (IComparable) o[i];
                            if (a.CompareTo(b) != 0)
                            {
                                equal = false;
                                break;
                            }
                        }
                        else if (this[i] == null && o[i] == null)
                        {
                            // corresponding items are null
                        }
                        else
                        {
                            equal = false;
                            break;
                        }
                    }

                    o = null;

                    if (equal)
                    {
                        return 0; // true;
                    }
                    else
                    {
                        return -1; // false;
                    }
                }
                else // Count != o.Count
                {
                    o = null;
                    return -1; // false;
                }
            }
            else
            {
                throw new ArgumentException("Argument is not of type CustomGenericList");
            }
        }

        #endregion CompareTo

        #region Operators

        /// <summary>
        /// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
        /// </summary>
        /// <param name="a">The first <see cref="CustomGenericList"/> object to compare.</param>
        /// <param name="b">The second <see cref="CustomGenericList"/> object to compare.</param>
        /// <returns>true if values of operands are equal, false otherwise.</returns>
        public static bool operator ==(CustomGenericList<T> a, CustomGenericList<T> b)
        {
            if (((Object) a) == null || ((Object) b) == null)
            {
                if (((Object) a) == null && ((Object) b) == null)
                {
                    return true;
                }
            }
            else
            {
                int r = a.CompareTo(b);

                if (r == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Inequality operator (!=) returns false if its operands are equal, true otherwise.
        /// </summary>
        /// <param name="a">The first <see cref="CustomGenericList"/> object to compare.</param>
        /// <param name="b">The second <see cref="CustomGenericList"/> object to compare.</param>
        /// <returns>false if values of operands are equal, false otherwise.</returns>
        public static bool operator !=(CustomGenericList<T> a, CustomGenericList<T> b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Returns true the specified object is equal to this object instance, false otherwise.
        /// </summary>
        /// <param name="obj">The <see cref="CustomGenericList"/> object to compare with the current <see cref="CustomGenericList"/>.</param>
        /// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
        public override bool Equals(Object obj)
        {
            if (!(obj is CustomGenericList<T>))
            {
                return false;
            }

            return this == (CustomGenericList<T>) obj;
        }

        #endregion Operators

        #region IndexOf

        /// <summary>
        ///	Searches for the specified object and return the zero-based index of the first occurrence within the entire collection.
        /// </summary>
        /// <param name="value">The object to locate in the collection. The value can be null.</param>
        /// <returns>The zero-based index of the first occurrence of value within the entire collection, if found; otherwise, -1.</returns>
        public virtual int IndexOf(T value)
        {
            return indexOf(value);
        }

        private int indexOf(T value)
        {
            if (value == null)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (this[i] == null)
                    {
                        return i;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Count; i++)
                {
                    if (value.Equals(this[i]))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        #endregion IndexOf

        #region Clone

        /// <summary>
        /// Creates a copy of the current instance.
        /// </summary>
        /// <returns>New instance of the current instance.</returns>
        public Object Clone()
        {
            return clone();
        }

        /// <summary>
        /// Creates a clone of the current instance.
        /// This is a thread-safe operation when the list is synchronized.
        /// </summary>
        /// <returns>New instance of the current instance.</returns>
        protected Object clone()
        {
            if (IsSynchronized)
            {
                lock (syncRoot)
                {
                    return deepCopy();
                }
            }
            else
            {
                return deepCopy();
            }
        }

        /// <summary>
        /// Creates a deep copy of the current instance.
        /// </summary>
        /// <returns>New instance of the current instance.</returns>
        private CustomGenericList<T> deepCopy()
        {
            CustomGenericList<T> o = new CustomGenericList<T>();

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    binaryFormatter.Serialize(memoryStream, this);
                    memoryStream.Position = 0;
                    o = (CustomGenericList<T>) binaryFormatter.Deserialize(memoryStream);
                }
            }
            finally
            {
                binaryFormatter = null;
            }

            return o;
        }

        #endregion Clone

        #region synchronized

        /// <summary>
        /// Returns a <see cref="CustomGenericList"/> wrapper that is synchronized (thread-safe).
        /// </summary>
        /// <param name="collection">The <see cref="CustomGenericList"/> to synchronize.</param>
        /// <returns>A <see cref="CustomGenericList"/> wrapper that is synchronized (thread-safe).</returns>
        public static CustomGenericList<T> Synchronized(CustomGenericList<T> collection)
        {
            return synchronized(collection);
        }

        /// <summary>
        /// Returns a wrapper that is synchronized (thread-safe).
        /// </summary>
        /// <param name="collection">The object to synchronize.</param>
        /// <returns>A object wrapper that is synchronized (thread-safe).</returns>
        protected static CustomGenericList<T> synchronized(CustomGenericList<T> collection)
        {
            CustomGenericList<T> o = collection.deepCopy();

            o._IsSynchronized = true;

            o.resetHashCode();

            return o;
        }

        #endregion synchronized

        #region SyncRoot

        private Object _syncRoot = new Object();

        /// <summary>
        /// Returns an internal object that can be used to synchronize access to this collection. Utilized only within the collection.
        /// </summary>
        internal Object syncRoot
        {
            get
            {
                return _syncRoot;
            }
        }

        private Object _SyncRoot = new Object();

        /// <summary>
        /// Returns an object that can be used to synchronize access to this collection.
        /// </summary>
        public Object SyncRoot
        {
            get
            {
                return _SyncRoot;
            }
        }

        #endregion SyncRoot

        protected Type _SortObjectType = null;

        /// <summary>
        /// Object type of the host elements within a strongly typed list. Used to facilitate dynamic sorting of the strongly typed list.
        /// </summary>
        public Type SortObjectType
        {
            get
            {
                return _SortObjectType;
            }
        }

        /// <summary>
        /// Sorts the elements of this list. The sort criteria is explicitly defined by one or more sort directives. 
        /// This is a thread-safe operation when the list is synchronized.
        /// </summary>
        /// <param name="sortDirectives">Specifies the order elements are sorted and the columns used in sorting elements.</param>
        /// <returns>Object array of errors encountered.</returns>
        public Object[] Sort(params SortDirective[] sortDirectives)
        {
            if (IsSynchronized)
            {
                lock (syncRoot)
                {
                    return sort(SortObjectType, sortDirectives);
                }
            }
            else
            {
                return sort(SortObjectType, sortDirectives);
            }
        }

        /// <summary>
        /// Sorts the elements of this list. The sort criteria is explicitly defined by the column. 
        /// This is a thread-safe operation when the list is synchronized.
        /// </summary>
        /// <param name="sortOrder">Specifies the order elements are sorted.</param>
        /// <param name="sortColumn">Specifies the column used in sorting elements.</param>
        /// <returns>Object array of errors encountered.</returns>
        public Object[] Sort(SortOrder sortOrder, string sortColumn)
        {
            SortDirective[] sortDirectives = new SortDirective[] {new SortDirective(sortOrder, sortColumn)};

            if (IsSynchronized)
            {
                lock (syncRoot)
                {
                    return sort(SortObjectType, sortDirectives);
                }
            }
            else
            {
                return sort(SortObjectType, sortDirectives);
            }
        }

        /// <summary>
        /// Sorts the elements of this list. The sort criteria is explicitly defined by the columns. The order in which the columns are specified determine sorting. For example, Sort(SortOrder.Ascending, "FirstName", "LastName") sorts this list by first name in ascending order, then by last name in ascending order.  Sort(SortOrder.Ascending, "LastName", "FirstName") sorts this list by last name in ascending order, the by first name in ascending order.
        /// This is a thread-safe operation when the list is synchronized.
        /// </summary>
        /// <param name="sortOrder">Specifies the order elements are sorted.</param>
        /// <param name="sortColumns">Specifies the columns used in sorting elements.</param>
        /// <returns>Object array of errors encountered.</returns>
        public Object[] Sort(SortOrder sortOrder, params string[] sortColumns)
        {
            SortDirective[] sortDirectives = new SortDirective[] {new SortDirective(sortOrder, sortColumns)};

            if (IsSynchronized)
            {
                lock (syncRoot)
                {
                    return sort(SortObjectType, sortDirectives);
                }
            }
            else
            {
                return sort(SortObjectType, sortDirectives);
            }
        }

        /// <summary>
        /// This signature provides a method to detail the sorting criteria as it relates to aggregated objects referenced by a public property of the main object contained within this list.
        /// The sort criteria is explicitly defined by the columns. The order in which the columns are specified determine sorting. For example, Sort("CustomerAddress", "State") defines an aggregated class referenced by the 'CustomerAddress' property containing a public property of 'State'. This will sort the list by the value of property 'State' in the aggregated object. The last sort column specified in this nested sort should be of a type that is sortable, e.g. string, int, DateTime, i.e. a type that fully implements IComparable.
        /// Note: In this example, the object referenced by the 'CustomerAddress' containing the public property 'State', must implement IComparable. This operation requires only a simple implementation of IComparable on the aggregated object. For example, the following sample method is consistent with the requirements of this operation. Example: public int CompareTo(object obj){return 0;}
        /// This is a thread-safe operation when the list is synchronized.
        /// </summary>
        /// <param name="sortColumns">Specifies the columns used in sorting elements.</param>
        /// <returns>Object array of errors encountered.</returns>
        public Object[] Sort(params string[] sortColumns)
        {
            SortDirective[] sortDirectives = new SortDirective[] {new SortDirective(SortOrder.Ascending, sortColumns)};

            if (IsSynchronized)
            {
                lock (syncRoot)
                {
                    return sort(SortObjectType, sortDirectives);
                }
            }
            else
            {
                return sort(SortObjectType, sortDirectives);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortColumns"></param>
        /// <returns></returns>
        private string createKey(string[] sortColumns)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in sortColumns)
            {
                sb.AppendFormat("{0}\n", s);
            }
            string temp = sb.ToString().TrimEnd('\n');

            sb = null;

            return temp;
        }

        private Object[] sort(Type sortObjectType, SortDirective[] sortDirectives)
        {
            if (sortObjectType == null)
            {
                throw new ApplicationException("Sort object type is null. Sort object type is required.");
            }

            Hashtable missing = new Hashtable();
            Hashtable directives = new Hashtable();

            // flush any duplicate column directives, only the first directive is utilized
            foreach (SortDirective directive in sortDirectives)
            {
                string key = createKey(directive.SortColumns);
                if (!directives.ContainsKey(key))
                {
                    bool exists = false;
                    foreach (PropertyInfo info in sortObjectType.GetProperties())
                    {
                        if (info.Name == directive.SortColumns[0])
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (exists)
                    {
                        directives.Add(key, directive);
                    }
                    else
                    {
                        missing.Add(directive.SortColumns[0], directive);
                    }
                }
            }

            // some of the directives reference a column that does not exist
            if (missing.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                int x = 0;
                foreach (string key in missing.Keys)
                {
                    if (missing.Count > 1 && x == missing.Count - 1)
                    {
                        sb.Append(" and ");
                    }
                    else if (sb.ToString() != string.Empty)
                    {
                        sb.Append(", ");
                    }
                    sb.AppendFormat("'{0}'", key);
                    x++;
                }

                string missingColumns = sb.ToString();
                sb = null;

                if (missing.Count == 1)
                {
                    throw new ApplicationException(string.Format("The column named {0} is not a public property of '{1}'. Please check the spelling of the column name.", missingColumns, sortObjectType.ToString()));
                }
                else
                {
                    throw new ApplicationException(string.Format("The columns named {0} are not a public property of '{1}'. Please check the spelling of the column names.", missingColumns, sortObjectType.ToString()));
                }
            }

            IGenericComparer comparer = new GenericComparer();
            comparer.SortObjectType = sortObjectType;
            comparer.SortDirectives = sortDirectives;

            InnerList.Sort(comparer);

            Object[] o = new Object[comparer.Errors.Count];
            comparer.Errors.Values.CopyTo(o, 0);

            comparer = null;

            return o;
        }

        /// <summary>
        /// Sorts the elements using the specified comparer.
        /// This is a thread-safe operation when the list is synchronized.
        /// </summary>
        /// <param name="comparer">The <see cref="System.Collections.IComparer"/> implementation to use when comparing elements within this list.</param>
        public void Sort(IComparer comparer)
        {
            if (IsSynchronized)
            {
                lock (syncRoot)
                {
                    InnerList.Sort(comparer);
                }
            }
            else
            {
                InnerList.Sort(comparer);
            }
        }
        
        /// <summary>
        /// Sorts the elements using the System.IComparable implementation of each element.
        /// This is a thread-safe operation when the list is synchronized.
        /// </summary>
        public void Sort()
        {
            if (IsSynchronized )
            {
                lock(syncRoot)
                {
                    InnerList.Sort();
                }
            }
            else
            {
                InnerList.Sort();
            }
        }

        #region IsReadOnly

        /// <summary>
        /// Gets an value indicating whether this list is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        #endregion IsReadOnly

        #region Add to collection

        private int add(T value)
        {
            return InnerList.Add(value);
        }

        /// <summary>
        /// Adds an object to the end of the collection.
        /// This is a thread-safe operation when the list is synchronized.
        /// </summary>
        /// <param name="value">The object to be added to the end of the collection.
        /// The value can be null.</param>
        /// <returns>Index at which the value has been added as an integer.</returns>
        public int Add(T value)
        {
            if (IsSynchronized)
            {
                lock (syncRoot)
                {
                    return add(value);
                }
            }
            else
            {
                return add(value);
            }
        }

        /// <summary>
        /// Adds the specified objects to the end of the collection.
        /// This is a thread-safe operation when the list is synchronized.
        /// </summary>
        /// <param name="value">The objects to be added to the end of the collection.</param>
        public void AddRange(params T[] value)
        {
            foreach (T item in value)
            {
                Add(item);
            }
        }

			public void AddRange( CustomGenericList<T> values )
			{
				foreach ( T item in values )
				{
					Add( item );
				}
			}

        #endregion Add to collection

        #region Contains

        /// <summary>
        ///	Searches for the specified object and returns true, if found; otherwise false.
        /// </summary>
        /// <param name="value">The object to locate in the collection. The value can be null.</param>
        /// <returns>true, if found; otherwise, false.</returns>
        public bool Contains(T value)
        {
            if (IndexOf(value) >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Contains

        #region Clear collection

        private void clear()
        {
            InnerList.Clear();
        }

        /// <summary>
        /// Removes all objects from the collection.
        /// This is a thread-safe operation when the list is synchronized.
        /// </summary>
        new public void Clear()
        {
            if (IsSynchronized)
            {
                lock (syncRoot)
                {
                    clear();
                }
            }
            else
            {
                clear();
            }
        }

        #endregion Clear collection

        #region IsFixedSize

        /// <summary>
        /// Gets a value indicating whether this list is a fixed size.
        /// </summary>
        public bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        #endregion IsFixedSize

        #region IsSynchronized

        internal bool _IsSynchronized = false;

        /// <summary>
        /// Gets a value indicating whether access to this list is synchronized (thread-safe).
        /// </summary>
        public bool IsSynchronized
        {
            get
            {
                return _IsSynchronized;
            }
        }

        #endregion IsSynchronized

        #region Count

        /// <summary>
        /// Gets the number of elements contained in this instance.  
        /// This is a thread-safe operation when the list is synchronized.
        /// </summary>
        new public int Count
        {
            get
            {
                if (IsSynchronized)
                {
                    lock (syncRoot)
                    {
                        return InnerList.Count;
                    }
                }
                else
                {
                    return InnerList.Count;
                }
            }
        }

        #endregion Count

        #region Remove from collection

        private void remove(T value)
        {
            int i = IndexOf(value);
            if (i >= 0)
            {
                RemoveAt(i);
            }
        }

        /// <summary>
        /// Removes the first occurrence of the specified object from the collection.
        /// This is a thread-safe operation when the list is synchronized.
        /// </summary>
        /// <param name="value">The object to remove from the collection. The value can be null.</param>
        public void Remove(T value)
        {
            if (IsSynchronized)
            {
                lock (syncRoot)
                {
                    remove(value);
                }
            }
            else
            {
                remove(value);
            }
        }

        private void removeAt(int index)
        {
            if (index >= InnerList.Count)
            {
                throw new ArgumentOutOfRangeException("Index is not a valid index in the collection.", "index");
            }
            else
            {
                InnerList.RemoveAt(index);
            }
        }

        /// <summary>
        /// Removes the element at the specified index.
        /// An <see cref="ArgumentOutOfRangeException"/> is thrown if the index is not a valid index in the collection.
        /// This is a thread-safe operation when the list is synchronized.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        new public void RemoveAt(int index)
        {
            if (IsSynchronized)
            {
                lock (syncRoot)
                {
                    removeAt(index);
                }
            }
            else
            {
                removeAt(index);
            }
        }

        #endregion Remove from collection

        #region Insert into collection

        private void insert(int index, T value)
        {
            InnerList.Insert(index, value);
        }

        /// <summary>
        /// Inserts an object into the collection at the specified position.
        /// This is a thread-safe operation when the list is synchronized.
        /// </summary>
        /// <param name="index">The zero-based index at which value should be inserted.</param>
        /// <param name="value">The object to insert into the collection.</param>
        public void Insert(int index, T value)
        {
            if (IsSynchronized)
            {
                lock (syncRoot)
                {
                    insert(index, value);
                }
            }
            else
            {
                insert(index, value);
            }
        }

        #endregion Insert into collection

        #region CopyTo

        private void copyTo(Array array, int index)
        {
            if (ReferenceEquals(array, null))
            {
                throw new ArgumentNullException("The destination array is a null reference.", "array");
            }

            if (array.Rank > 1)
            {
                throw new ArgumentException("The destination array is multi-dimensional.", "array");
            }

            if (index < 0 || index > array.GetLength(0) - 1)
            {
                throw new ArgumentException("The starting index is outside the bounds of the destination array.", "index");
            }

            if (array.GetLength(0) < Count)
            {
                throw new ArgumentException("The number of elements in this collection is greater than the size of the destination array.", "array");
            }

            if (array.GetLength(0) < (index + Count))
            {
                throw new ArgumentException("The number of elements in this collection is greater than the available space from index to the end of the destination array.", "index");
            }

            foreach (T item in this)
            {
                array.SetValue(item, index);
                index++;
            }
        }

        /// <summary>
        /// Copies the element of this collection to an array starting at the specified index.
        /// An <see cref="ArgumentNullException"/> is thrown if the array is a null reference.
        /// An <see cref="ArgumentException"/> is thrown if the array is multidimensional, or
        /// the index is equal to or greater than the length of array, or 
        /// the index is less than 0, or
        /// the number of elements in this collection is greater than the size of the destination array, or
        /// the number of elements in this collection is greater than the available space from index to the end of the destination array.
        /// This is a thread-safe operation when the list is synchronized.
        /// </summary>
        /// <param name="array">The one-dimensiional <see cref="System.Array"/> that is the destination of the objects copied from the collection instance. The <see cref="System.Array"/> must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in array at which the copying begins.</param>
        public virtual void CopyTo(Array array, int index)
        {
            if (IsSynchronized)
            {
                lock (syncRoot)
                {
                    copyTo(array, index);
                }
            }
            else
            {
                copyTo(array, index);
            }
        }

        #endregion CopyTo

        /// <summary>
        /// This <see cref="IDisposable"/> implementation follows Microsoft coding best practices.
        /// Dispose is also initiated by the destructor of this class.
        /// </summary>

        #region Dispose

        private bool disposed = false;

        /// <summary>
        /// Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// Because the Dispose is implemented, this class can be used in a 'using' statement.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the 
        // runtime from inside the finalizer and you should not reference 
        // other objects. Only unmanaged resources can be disposed.
        private void Dispose(bool disposing)
        {
            // check to see if Dispose has already been called.
            if (!disposed)
            {
                // if displosing equals true, dispose all managed and unmanaged resources
                if (disposing)
                {
                    // clear managed resources
                    InnerList.Clear();
                }
            }

            disposed = true;
        }

        #endregion Dispose
    }
    

    /// <summary>
    /// A generic <see cref="System.Collections.IComparer"/> implementation. 
    /// Implements <see cref="IGenericComparer"/>.
    /// </summary>
    [Serializable]
    public sealed class GenericComparer : IGenericComparer, IDisposable
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GenericComparer()
        {
        }

        #endregion Constructors

        #region Destructor

        ~GenericComparer()
        {
            Dispose(false);
        }

        #endregion Destructor

        private Type _SortObjectType = null;

        /// <summary>
        /// Object type of the host elements within a strongly typed list. Used to facilitate dynamic sorting of the strongly typed list.
        /// </summary>
        public Type SortObjectType
        {
            get
            {
                return _SortObjectType;
            }
            set
            {
                _SortObjectType = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private SortInfo[] setSortInfo()
        {
            ArrayList array = new ArrayList();
            foreach (SortDirective item in SortDirectives)
            {
                SortInfo info = new SortInfo();
                info.propertyInfo = SortObjectType.GetProperty(item.SortColumns[0]);
                info.sortDirective = item;
                array.Add(info);
            }
            return (SortInfo[]) array.ToArray(typeof (SortInfo));
        }

        private SortInfo[] _sortInfo = new SortInfo[0];

        /// <summary>
        /// 
        /// </summary>
        private SortInfo[] sortInfo
        {
            get
            {
                return _sortInfo;
            }
            set
            {
                _sortInfo = value;
            }
        }

        private SortDirective[] _SortDirectives = new SortDirective[0];

        /// <summary>
        /// 
        /// </summary>
        public SortDirective[] SortDirectives
        {
            get
            {
                return _SortDirectives;
            }
            set
            {
                _SortDirectives = value;
                sortInfo = setSortInfo();
            }
        }

        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to or greater than the other.
        /// </summary>
        /// <param name="x">First object to compare.</param>
        /// <param name="y">Second object to compare.</param>
        /// <returns>Value Condition  Less than zero: x is less than y. Zero: x equal y. Greater than zero: x is greater than y.</returns>
        /// <exception cref="ArgumentException">Object x is not of type '<see cref="SortObjectType"/>'.</exception>
        /// <exception cref="ArgumentException">Object y is not of type '<see cref="SortObjectType"/>'.</exception>
        public int Compare(Object x, Object y)
        {
            return compare(x, y);
        }

        #region Is implementation of or inherited from

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="baseType"></param>
        /// <returns></returns>
        private bool isImplementationOf(Type type, Type baseType)
        {
            TypeFilter filter = new TypeFilter(delegate(Type typeObj, Object criteriaObj)
                {
                    if (typeObj.ToString() == criteriaObj.ToString())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });

            Type[] interfaces = type.FindInterfaces(filter, baseType.ToString());

            foreach (Type t in interfaces)
            {
                if (t.ToString() == baseType.ToString())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="baseType"></param>
        /// <returns></returns>
        private bool isInheritedFrom(Type type, Type baseType)
        {
            Type endOfInheritance = new object().GetType();
            Type a = type;
            Type b = baseType;
            do
            {
                if (a.ToString() == b.ToString())
                {
                    return true;
                }
                else if (a.ToString() == endOfInheritance.ToString())
                {
                    return false;
                }
                else
                {
                    a = a.BaseType;
                }
            }
            while (0 == 0);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int compare(Object x, Object y)
        {
            StringBuilder sb = new StringBuilder();
            if (x.GetType() != SortObjectType && !isImplementationOf(x.GetType(), SortObjectType) && !isInheritedFrom(x.GetType(), SortObjectType))
            {
                throw new ArgumentException(string.Format("Object x is not of type '{0}' and is not an implementation of type '{0}'.", SortObjectType.ToString()));
            }

            if (y.GetType() != SortObjectType && !isImplementationOf(y.GetType(), SortObjectType) && !isInheritedFrom(y.GetType(), SortObjectType))
            {
                throw new ArgumentException(string.Format("Object y is not of type '{0}' and is not an implementation of type '{0}'.", SortObjectType.ToString()));
            }

            int result = 0;

            foreach (SortInfo info in sortInfo)
            {
                Object test;

                IComparable X = null;
                test = getValue(info, x);

                if (test == null && getValue(info, y) == null)
                {
                    return 0;
                }
                else if (test == null)
                {
                    return (1);
                }
                else if (getValue(info, y) == null)
                {
                    return (-1);
                }
                else if (test is IComparable)
                {
                    X = (IComparable)test;

                    IComparable Y = null;

                    test = getValue(info, y);

                    if (test is IComparable)
                    {
                        Y = (IComparable)test;

                        if (info.sortDirective.SortOrder == 0)
                        {
                            result = (X.CompareTo(Y));
                        }
                        else
                        {
                            result = (Y.CompareTo(X));
                        }
                    }
                    else
                    {
                        result = -1; // x does not equal y
                    }
                }
                else
                {
                    throw new InvalidOperationException("When sorting items of aggregated objects, those objects must implement IComparable. This operation requires only a simple implementation of IComparable. For example the following sample method implementation is consistent with the requirements of this operation. Example: public int CompareTo(object obj){return 0;} The last sort column specified in this nested sort should be of a type that is sortable, i.e. a type that fully implements IComparable.");
                }

                if (result != 0) // x does not equal y
                {
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortNfo"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private Object getValue(SortInfo sortNfo, Object obj)
        {
            return getValue(sortNfo.propertyInfo, obj, sortNfo.sortDirective.SortColumns, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="obj"></param>
        /// <param name="sortColumns"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        private Object getValue(PropertyInfo propertyInfo, Object obj, string[] sortColumns, int x)
        {
            try
            {
                Object test = propertyInfo.GetValue(obj, null);
                if (test is IComparable)
                {
                    if (sortColumns.GetLength(0) - 1 > (x))
                    {
                        try
                        {
                            test = getValue(test.GetType().GetProperty(sortColumns[x + 1]), test, sortColumns, x + 1);
                        }
                        catch
                        {
                            inError(sortColumns, x + 1);

                            return null;
                        }
                    }
                }

                return test;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                inError(sortColumns, x);

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortColumns"></param>
        /// <param name="x"></param>
        private void inError(string[] sortColumns, int x)
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y <= x; y++)
            {
                sb.AppendFormat("{0}{1}{0}, ", '"', sortColumns[y]);
            }
            string key = sb.ToString().Trim().TrimEnd(',');
            sb = null;

            if (!Errors.ContainsKey(key))
            {
                Errors.Add(key, string.Format("Sort directive specifying column(s) {0} is not correct.", key));
            }
        }

        private Hashtable _Errors = new Hashtable();

        /// <summary>
        /// 
        /// </summary>
        public Hashtable Errors
        {
            get
            {
                return _Errors;
            }
            set
            {
                _Errors = value;
            }
        }

        /// <summary>
        /// This <see cref="IDisposable"/> implementation follows Microsoft coding best practices.
        /// Dispose is also initiated by the destructor of this class.
        /// </summary>

        #region Dispose

        private bool disposed = false;

        /// <summary>
        /// Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// Because the Dispose is implemented, this class can be used in a 'using' statement.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the 
        // runtime from inside the finalizer and you should not reference 
        // other objects. Only unmanaged resources can be disposed.
        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!disposed)
            {
                // if disposing equals true, dispose all managed and unmanaged resources
                if (disposing)
                {
                }
            }

            disposed = true;
        }

        #endregion Dispose

        /// <summary>
        /// 
        /// </summary>
        [Serializable]
        internal class SortInfo
        {
            private PropertyInfo _propertyInfo = null;

            /// <summary>
            /// 
            /// </summary>
            internal PropertyInfo propertyInfo
            {
                get
                {
                    return _propertyInfo;
                }
                set
                {
                    _propertyInfo = value;
                }
            }

            private SortDirective _sortDirective = null;

            /// <summary>
            /// 
            /// </summary>
            internal SortDirective sortDirective
            {
                get
                {
                    return _sortDirective;
                }
                set
                {
                    _sortDirective = value;
                }
            }
        }
    }

    /// <summary>
    /// Defines the sorting criteria used by <see cref="GenericComparer"/>.
    /// </summary>
    [Serializable]
    public sealed class SortDirective : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Creates a sort directive from the specified sort order and column.
        /// </summary>
        /// <param name="sortOrder">Specifies the order elements are sorted.</param>
        /// <param name="sortColumn">Specifies the sort criteria, the column used in sorting elements.</param>
        public SortDirective(SortOrder sortOrder, string sortColumn) : this(sortOrder, new string[] {sortColumn})
        {
        }

        /// <summary>
        /// Creates a sort directive from the specified column with an ascending sort order.
        /// </summary>
        /// <param name="sortColumn">Specifies the sort criteria, the column used in sorting elements.</param>
        public SortDirective(string sortColumn) : this(SortOrder.Ascending, new string[] {sortColumn})
        {
        }

        /// <summary>
        /// Creates a sort directive from the specified columns with an ascending sort order.
        /// </summary>
        /// <param name="sortColumns">Specifies the sort criteria, the columns used in sorting elements.</param>
        public SortDirective(params string[] sortColumns) : this(SortOrder.Ascending, sortColumns)
        {
        }

        /// <summary>
        /// Creates a sort directive from the specified columns with an ascending sort order.
        /// </summary>
        /// <param name="sortOrder">Specifies the order elements are sorted.</param>
        /// <param name="sortColumns">Specifies the sort criteria, the columns used in sorting elements.</param>
        public SortDirective(SortOrder sortOrder, params string[] sortColumns)
        {
            _SortOrder = sortOrder;
            _SortColumns = sortColumns;
        }

        #endregion Constructors

        #region Destructor

        ~SortDirective()
        {
            Dispose(false);
        }

        #endregion Destructor

        #region SortColumns

        private string[] _SortColumns = new string[0];

        /// <summary>
        /// Gets the sort criteria, the columns used in sorting elements.
        /// </summary>
        public string[] SortColumns
        {
            get
            {
                return _SortColumns;
            }
        }

        #endregion SortColumns

        #region SortOrder

        private SortOrder _SortOrder = SortOrder.Ascending;

        /// <summary>
        /// Gets the order in which elements are sorted.
        /// </summary>
        public SortOrder SortOrder
        {
            get
            {
                return _SortOrder;
            }
        }

        #endregion SortOrder

        /// <summary>
        /// This <see cref="IDisposable"/> implementation follows Microsoft coding best practices.
        /// Dispose is also initiated by the destructor of this class.
        /// </summary>

        #region Dispose

        private bool disposed = false;

        /// <summary>
        /// Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// Because the Dispose is implemented, this class can be used in a 'using' statement.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the 
        // runtime from inside the finalizer and you should not reference 
        // other objects. Only unmanaged resources can be disposed.
        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!disposed)
            {
                // if disposing equals true, dispose all managed and unmanaged resources
                if (disposing)
                {
                }
            }

            disposed = true;
        }

        #endregion Dispose
    }

    /// <summary>
    /// ISortable Interface
    /// </summary>
    public interface ISortable
    {
        void Sort(IComparer comparer);

        Object[] Sort(SortOrder sortOrder, params string[] sortColumns);

        Object[] Sort(SortOrder sortOrder, string sortColumn);

        Object[] Sort(params SortDirective[] sortDirectives);

        Object[] Sort(params string[] sortColumns);

        Type SortObjectType { get; }
    }

    /// <summary>
    /// IGenericComparer Interface implements <see cref="IComparer"/>.
    /// </summary>
    public interface IGenericComparer : IComparer
    {
        Type SortObjectType { get; set; }

        SortDirective[] SortDirectives { get; set; }

        Hashtable Errors { get; set; }
    }
    */
}