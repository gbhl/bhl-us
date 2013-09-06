using System;

namespace CustomDataAccess
{
    public enum CustomDataAccessContext
    {
        Insert,
        Update,
        Delete,
        NA
    }

    [Serializable]
    public sealed class CustomDataAccessStatus<T>
    {
        public CustomDataAccessStatus(CustomDataAccessContext context, bool isSuccessful, T returnObject)
        {
            Context = context;
            IsSuccessful = isSuccessful;
            ReturnObject = returnObject;
        }

        public readonly CustomDataAccessContext Context = CustomDataAccessContext.NA;
        public readonly bool IsSuccessful = false;
        public readonly T ReturnObject = default(T);

        public bool IsInserted
        {
            get
            {
                if (Context == CustomDataAccessContext.Insert)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsUpdated
        {
            get
            {
                if (Context == CustomDataAccessContext.Update)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsDeleted
        {
            get
            {
                if (Context == CustomDataAccessContext.Delete)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsNA
        {
            get
            {
                if (Context == CustomDataAccessContext.NA)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}