using System;
using System.Collections.Generic;
using CustomDataAccess;

namespace MOBOT.BHL.Web2
{
    public static class CustomGenericListExtensions
    {
        public static List<T> ToList<T>(this CustomGenericList<T> list)
        {
            List<T> newList = new List<T>();
            foreach (T item in list)
            {
                newList.Add(item);
            }
            
            return newList;
        }
    }
}