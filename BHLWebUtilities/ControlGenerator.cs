using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace MOBOT.BHL.Web.Utilities
{
    public class ControlGenerator
    {
        protected ControlGenerator()
        {
        }

        public static HtmlGenericControl GetScriptControl(string scriptLocation)
        {
            HtmlGenericControl control = new HtmlGenericControl();
            control.TagName = "script";
            control.Attributes.Add("language", "javascript");
            control.Attributes.Add("src", scriptLocation);
            control.Attributes.Add("type", "text/javascript");
            return control;
        }

        public static HtmlGenericControl GetLinkControl(string linkLocation)
        {
            HtmlGenericControl control = new HtmlGenericControl();
            control.TagName = "link";
            control.Attributes.Add("rel", "stylesheet");
            control.Attributes.Add("type", "text/css");
            control.Attributes.Add("href", linkLocation);
            return control;
        }

        public static void AddScriptControl(ControlCollection container, string scriptLocation)
        {
            bool scriptControlFound = false;
            foreach (Control control in container)
            {
                HtmlGenericControl htmlControl = null;
                if (control is HtmlGenericControl)
                    htmlControl = (HtmlGenericControl)control;
                if (htmlControl != null && htmlControl.Attributes["src"] != null && htmlControl.Attributes["src"].Trim().ToLower().IndexOf(scriptLocation.ToLower()) >= 0)
                {
                    scriptControlFound = true;
                    break;
                }
            }
            if (!scriptControlFound)
                container.Add(GetScriptControl(scriptLocation));
        }

        public static void AddLinkControl(ControlCollection container, string linkLocation)
        {
            bool linkControlFound = false;
            foreach (Control control in container)
            {
                HtmlGenericControl htmlControl = null;
                if (control is HtmlGenericControl)
                    htmlControl = (HtmlGenericControl)control;
                if (htmlControl != null && htmlControl.Attributes["href"] != null && htmlControl.Attributes["href"].Trim().ToLower().IndexOf(linkLocation.ToLower()) >= 0)
                {
                    linkControlFound = true;
                    break;
                }
            }
            if (!linkControlFound)
                container.Add(GetLinkControl(linkLocation));
        }

        public static void AddAttributesAndPreserveExisting(HtmlGenericControl control, string key, string value)
        {
            string existingValue = control.Attributes[key];
            if (existingValue != null && existingValue.Trim().Length > 0 && !existingValue.EndsWith(";"))
                existingValue += ";";

            //get the existing value before adding on to it so we don't wipe anything out.
            control.Attributes.Remove(key);
            control.Attributes.Add(key, existingValue + value);
        }
    }
}
