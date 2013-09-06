using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Web.Utilities;
using MOBOT.BHL.Server;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Web2
{
    public partial class dlconcepts : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = String.Format(ConfigurationManager.AppSettings["PageTitle"], "Darwin's Library Concepts");
          //  ((Main)Page.Master).SetTweetMessage(String.Format(ConfigurationManager.AppSettings["TweetMessage"], "Darwin's Library Concepts"));

            // Get the concepts for the Darwin's Library annotations
            CustomGenericList<AnnotationConcept> concepts = bhlProvider.AnnotationConceptSelectAll(1);
            //split into three columns
            CustomGenericList<AnnotationConcept> concepts1 = new CustomGenericList<AnnotationConcept>();
            CustomGenericList<AnnotationConcept> concepts2 = new CustomGenericList<AnnotationConcept>();
            CustomGenericList<AnnotationConcept> concepts3 = new CustomGenericList<AnnotationConcept>();
            float totalConceptCount = concepts.Count;
            int columncount = Convert.ToInt32(Math.Ceiling(totalConceptCount/3));
            for(int i = 0; i<=columncount-1; i++){
                concepts1.Add(concepts[i]);
            }
            for(int j = columncount; j<=(columncount*2)-1; j++){
                concepts2.Add(concepts[j]);
            }
            for(int k = columncount*2; k<=totalConceptCount-1; k++){
                concepts3.Add(concepts[k]);
            }
            rptConcepts1.DataSource = concepts1;
            rptConcepts2.DataSource = concepts2;
            rptConcepts3.DataSource = concepts3;
            rptConcepts1.DataBind();
            rptConcepts2.DataBind();
            rptConcepts3.DataBind();
     
        }
    }
}