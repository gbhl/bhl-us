using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MOBOT.BHL.AdminWeb
{
    public partial class DOIList : System.Web.UI.Page
    {
        protected string userId = "0";
        protected string statusId = "-1";
        protected string typeId = "0";
        protected string entityId = "";
        protected string startDate = "";
        protected string endDate = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            // The jQuery UI theme that will be used by the grid
            HtmlLink cssLnk = new HtmlLink();
            cssLnk.Attributes.Add("rel", "stylesheet");
            cssLnk.Attributes.Add("type", "text/css");
            cssLnk.Href = ConfigurationManager.AppSettings["jQueryUICSSPath"];
            Page.Header.Controls.Add(cssLnk);

            // The jQuery UI theme extension jqGrid needs
            HtmlLink cssLnk2 = new HtmlLink();
            cssLnk2.Attributes.Add("rel", "stylesheet");
            cssLnk2.Attributes.Add("type", "text/css");
            cssLnk2.Href = ConfigurationManager.AppSettings["jqGridCSSPath"];
            Page.Header.Controls.Add(cssLnk2);

            // jQuery runtime
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, ConfigurationManager.AppSettings["jQueryPath"]);
            // The localization file we need (English)
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, ConfigurationManager.AppSettings["jqGridLocalePath"]);
            // jqGrid runtime
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, ConfigurationManager.AppSettings["jqGridPath"]);
            // jQuery UI library
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, ConfigurationManager.AppSettings["jQueryUIPath"]);

            if (!IsPostBack)
            {
                txtStartDate.Text = "1/1/1980";
                txtEndDate.Text = DateTime.Now.AddDays(1).ToShortDateString();

                Dictionary<int, string> users = new BHLProvider().AspNetUserSelectWithDoi();
                ddlQueuedBy.Items.Add(new ListItem("(Anyone)", "0"));
                foreach(KeyValuePair<int, string> user in users)
                {
                    ddlQueuedBy.Items.Add(new ListItem(user.Value, user.Key.ToString()));
                }

                List<DOIStatus> statuses = new BHLProvider().DOIStatusSelectAll();
                ddlStatusView.Items.Add(new ListItem("(All Statuses)", "0"));
                foreach (DOIStatus status in statuses)
                {
                    if (status.DOIStatusID.ToString() != DOISTATUS_EXTERNAL)
                    {
                        ListItem li = new ListItem(status.DOIStatusName, status.DOIStatusID.ToString());
                        ddlStatusView.Items.Add(li);
                    }
                }

                List<DOIEntityType> types = new BHLProvider().DOIEntityTypeSelectWithDoi();
                ddlEntityType.Items.Add(new ListItem("(All Types)", "0"));
                foreach(DOIEntityType type in types)
                {
                    ddlEntityType.Items.Add(new ListItem(type.DOIEntityTypeName, type.DOIEntityTypeID.ToString()));
                }
            }
        }

        /// <summary>
        /// Display the DOIs that are in the selected status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView_Click(object sender, EventArgs e)
        {
            userId = ddlQueuedBy.SelectedValue;
            statusId = ddlStatusView.SelectedValue;
            typeId = ddlEntityType.SelectedValue;
            entityId = txtEntityID.Text;
            startDate = txtStartDate.Text;
            endDate = txtEndDate.Text;

            lnkDownloadResults.Visible = true;
            lnkDownloadResults.HRef = String.Format(
                "/services/doiservice.ashx?uid={0}&sid={1}&tid={2}&eid={3}&sdate={4}&edate={5}&dl=1&rows=100000000",
                userId, statusId, typeId, entityId, startDate, endDate);

            string userName = (userId == "0") ? "Anyone" : ddlQueuedBy.SelectedItem.Text;
            string statusName = (statusId == DOISTATUS_ALLSELECTED) ? "Any" : ddlStatusView.SelectedItem.Text;
            string entityName = (typeId == "0") ? "Any" : string.Empty;
            string entityDetail = (typeId== "0") ? string.Empty : ddlEntityType.SelectedItem.Text + 
                (entityId == string.Empty ? string.Empty : " (" + entityId + ")");

            litDisplayed.Text = (statusId == DOISTATUS_NONESELECTED)
                ? string.Empty
                : string.Format("DOIs queued between <b>{0}</b> and <b>{1}</b> by <b>{2}</b> in <b>{3}</b> status and <b>{4}</b> entity type <b>{5}</b>",
                    startDate, endDate, userName, statusName, entityName, entityDetail);
        }

        private const string DOISTATUS_NONESELECTED = "-1";
        private const string DOISTATUS_ALLSELECTED = "0";
        private const string DOISTATUS_EXTERNAL = "200";
    }
}