using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;

namespace MOBOT.BHL.Web2
{
    public partial class dlconcepts : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = String.Format(AppConfig.PageTitle, "Darwin's Library Concepts");

            // Get the concepts for the Darwin's Library annotations
            List<AnnotationConcept> concepts = bhlProvider.AnnotationConceptSelectAll(1);
            //split into three columns
            List<AnnotationConcept> concepts1 = new List<AnnotationConcept>();
            List<AnnotationConcept> concepts2 = new List<AnnotationConcept>();
            List<AnnotationConcept> concepts3 = new List<AnnotationConcept>();
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