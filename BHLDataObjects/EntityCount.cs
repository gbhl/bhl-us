#region Using

using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
    [Serializable]
    public class EntityCount
    {
        private EntityType _entityCountTypeID = 0;

        public EntityType EntityCountTypeID
        {
            get { return _entityCountTypeID; }
            set { _entityCountTypeID = value; }
        }

        private string _fullName = string.Empty;

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        private string _displayName = string.Empty;

        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        private int _countValue = 0;

        public int CountValue
        {
            get { return _countValue; }
            set { _countValue = value; }
        }

        public enum EntityType
        {
            ActiveTitles = 1,
            ActiveItems = 2,
            ActivePages = 3,
            ActiveSegments = 4,
            ActiveNames = 5
        }
    }
}
