using CustomDataAccess;
using System;

namespace MOBOT.BHL.DataObjects
{
    public class EditHistory : ISetValues
    {
        private DateTime? _editDate;
        private string _entityName;
        private string _entityKey;
        private string _entityDetail;
        private string _operation;
        private string _firstName;
        private string _lastName;
        private string _email;

        public DateTime? EditDate { get => _editDate; set => _editDate = value; }
        public string EntityName { get => _entityName; set => _entityName = value; }
        public string EntityKey { get => _entityKey; set => _entityKey = value; }
        public string EntityDetail { get => _entityDetail; set => _entityDetail = value; }

        public string Operation {
            get => _operation;
            set
            {
                switch (value)
                    {
                    case "I":
                        _operation = "Add";
                        break;
                    case "U":
                        _operation = "Update";
                        break;
                    case "D":
                        _operation = "Delete";
                        break;
                }
            }
        }

        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public string Email { get => _email; set => _email = value; }

        public string User { get => _firstName + " " + _lastName + (string.IsNullOrWhiteSpace(_email) ? "" : " (" + _email + ")");  }

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "EditDate":
                        {
                            EditDate = (DateTime)column.Value;
                            break;
                        }
                    case "EntityName":
                        {
                            EntityName = Utility.EmptyIfNull((String)column.Value);
                            break;
                        }
                    case "EntityKey1":
                        {
                            EntityKey = Utility.EmptyIfNull((String)column.Value);
                            break;
                        }
                    case "EntityDetail":
                        {
                            EntityDetail = Utility.EmptyIfNull((String)column.Value);
                            break;
                        }
                    case "Operation":
                        {
                            Operation = Utility.EmptyIfNull((String)column.Value);
                            break;
                        }
                    case "FirstName":
                        {
                            FirstName = Utility.EmptyIfNull((String)column.Value);
                            break;
                        }
                    case "LastName":
                        {
                            LastName = Utility.EmptyIfNull((String)column.Value);
                            break;
                        }
                    case "Email":
                        {
                            Email = Utility.EmptyIfNull((String)column.Value);
                            break;
                        }
                }
            }
        }
    }
}
