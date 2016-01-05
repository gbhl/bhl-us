﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Web.Utilities;
using CustomDataAccess;

namespace MOBOT.BHL.AdminWeb
{
    public partial class TitleItemMarc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                try
                {
                    string type = Request.QueryString["type"] as string;
                    string idString = Request.QueryString["id"] as string;
                    type = (type == null) ? string.Empty : type.ToLower();
                    idString = (idString == null) ? string.Empty : idString;
                    int id;

                    if (Int32.TryParse(idString, out id) && (type == "t" || type == "i"))
                    {
                        SiteService.SiteServiceSoapClient service = new SiteService.SiteServiceSoapClient();
                        if (service.MARCFileExists(id, type))
                        {
                            string marcXML = service.MARCGetFileContents(id, type);

                            XmlDocument xml = new XmlDocument();
                            StringReader reader = new StringReader(marcXML);
                            xml.Load(reader);

                            // Set up the XSL resolver that we'll use to extract the text from the xml
                            XmlUrlResolver resolver = new XmlUrlResolver();
                            resolver.Credentials = System.Net.CredentialCache.DefaultCredentials;
                            System.Xml.Xsl.XslTransform xslTransform = new System.Xml.Xsl.XslTransform();

                            // Format the MARC XML into a "flat" MARC format
                            xslTransform.Load(Request.PhysicalApplicationPath + "\\xsl\\MARC21transformMARC.xsl", resolver);
                            StringWriter output = new StringWriter();
                            xslTransform.Transform(xml, null, output, resolver);
                            litMarc.Text = output.ToString();
                        }
                        else
                        {
                            litMarc.Text = "MARC not found.";
                        }

                    }
                }
                catch (Exception ex)
                {
                    string message = "Error retrieving MARC.<br><br>";
                    if (new DebugUtility(ConfigurationManager.AppSettings["DebugValue"]).IsDebugMode(Response, Request))
                    {
                        message += ex.Message + "<br><br>";
                        if (ex.StackTrace != null) message += ex.StackTrace;
                    }
                    litMarc.Text = message;
                }
            }

        }
    }
}