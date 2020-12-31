using System;
using System.Collections.Generic;

namespace CustomDataAccess
{
    public interface ISetValues
    {
        void SetValues(CustomDataRow row);
    }

    [Serializable]
    public class CustomDataRow : List<CustomDataColumn>
    {
        public CustomDataColumn this[string name]
        {
            get
            {
                foreach (CustomDataColumn column in this)
                {
                    if (column.Name == name)
                    {
                        return column;
                    }
                }

                return null;
            }
        }
    }

    [Serializable]
    public class CustomDataColumn
    {
        internal CustomDataColumn()
        {
        }

        public CustomDataColumn(int ordinal, string name, Type dataType, object value, bool isDbNull) : this()
        {
            _Ordinal = ordinal;
            _Name = name;
            _DataType = dataType;
            if (isDbNull)
            {
                _Value = null;
            }
            else
            {
                _Value = value;
            }
            _IsDbNull = isDbNull;
        }

        private int _Ordinal = -1;

        public int Ordinal
        {
            get
            {
                return _Ordinal;
            }
        }

        private string _Name = string.Empty;

        public string Name
        {
            get
            {
                return _Name;
            }
        }

        private Type _DataType = default(Type);

        public Type DataType
        {
            get
            {
                return _DataType;
            }
        }

        private object _Value = null;

        public object Value
        {
            get
            {
                return _Value;
            }
        }

        private bool _IsDbNull = false;

        public bool IsDbNull
        {
            get
            {
                return _IsDbNull;
            }
        }
    }
}