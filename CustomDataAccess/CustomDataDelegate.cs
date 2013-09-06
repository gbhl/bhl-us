namespace CustomDataAccess
{
    public class CustomDataDelegate<T>
    {
        public delegate void TransporterCallBack(CustomDataTransporter<T> obj);

        /// <summary>
        /// Load object call back defines the signature for a function call back to execute a custom load of the object.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public delegate T LoadObjectCallBack(CustomDataRow row);

    }
}
