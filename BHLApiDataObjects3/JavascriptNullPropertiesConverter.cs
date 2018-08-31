using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    /// <summary>
    /// Custom javascript serializer that ignores null values when serializing a class into JSON.  
    /// The default .NET serializer includes "propertyname" : null in the JSON output; this 
    /// suppresses such elements.
    /// </summary>
    public class JavascriptNullPropertiesConverter : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var json = new Dictionary<string, object>();
            foreach (var prop in obj.GetType().GetProperties())
            {
                //check if decorated with ScriptIgnore attribute
                bool ignoreProp = prop.IsDefined(typeof(ScriptIgnoreAttribute), true);

                var value = prop.GetValue(obj, BindingFlags.Public, null, null, null);

                // If the value of the property is null, or property has a ScriptIgnore attribute, skip it.
                if (value != null && !ignoreProp)
                    json.Add(prop.Name, value);
            }

            return json;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            // Apply custom serialization to all types in this assembly
            get { return GetType().Assembly.GetTypes(); }
            //get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(BHLApiDataObjects3.Part) })); }
        }
    }
}
