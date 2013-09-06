using System;
using System.Data;

namespace CustomDataAccess 
{
    /// <summary>
    /// Summary description for ColumnDefinition.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    [Serializable] 
    public class ColumnDefinitionAttribute : Attribute
    {
        public ColumnDefinitionAttribute(string columnName)
        {
            ColumnName = columnName;
        }
        public ColumnDefinitionAttribute(string columnName, SqlDbType dataType, int ordinal, string description, int charMaxLength, int numericPrecision, int numericScale, bool isAutoKey, bool isComputed, bool isInForeignKey, bool isInPrimaryKey, bool isNullable)
        {
            ColumnName = columnName;
            DbTargetType = dataType;
            Ordinal = ordinal;
            Description = description;
            CharacterMaxLength = charMaxLength;
            NumericPrecision = numericPrecision;
            NumericScale = numericScale;
            IsAutoKey = isAutoKey;
            IsComputed = isComputed;
            IsInForeignKey = isInForeignKey;
            IsInPrimaryKey = isInPrimaryKey;
            IsNullable = isNullable;
        }

        private string _ColumnName = string.Empty;

        public string ColumnName
        {
            get
            {
                return _ColumnName;
            }
            set
            {
                _ColumnName = value;
            }
        }

        private object _DbTargetType;

        public object DbTargetType
        {
            get
            {
                return _DbTargetType;
            }
            set
            {
                _DbTargetType = value;
            }
        }

        private int _Ordinal = -1;

        public int Ordinal
        {
            get
            {
                return _Ordinal;
            }
            set
            {
                _Ordinal = value;
            }
        }

        private int _CharacterMaxLength = 0;

        public int CharacterMaxLength
        {
            get
            {
                return _CharacterMaxLength;
            }
            set
            {
                _CharacterMaxLength = value;
            }
        }

        private int _NumericPrecision = 0;

        public int NumericPrecision
        {
            get
            {
                return _NumericPrecision;
            }
            set
            {
                _NumericPrecision = value;
            }
        }

        private int _NumericScale = 0;

        public int NumericScale
        {
            get
            {
                return _NumericScale;
            }
            set
            {
                _NumericScale = value;
            }
        }

        private bool _IsAutoKey = false;

        public bool IsAutoKey
        {
            get
            {
                return _IsAutoKey;
            }
            set
            {
                _IsAutoKey = value;
            }
        }

        private bool _IsComputed = false;

        public bool IsComputed
        {
            get
            {
                return _IsComputed;
            }
            set
            {
                _IsComputed = value;
            }
        }

        private bool _IsInForeignKey = false;

        public bool IsInForeignKey
        {
            get
            {
                return _IsInForeignKey;
            }
            set
            {
                _IsInForeignKey = value;
            }
        }

        private bool _IsInPrimaryKey = false;

        public bool IsInPrimaryKey
        {
            get
            {
                return _IsInPrimaryKey;
            }
            set
            {
                _IsInPrimaryKey = value;
            }
        }

        private bool _IsNullable = false;

        public bool IsNullable
        {
            get
            {
                return _IsNullable;
            }
            set
            {
                _IsNullable = value;
            }
        }

        private string _Description = string.Empty;

        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }
    }
}