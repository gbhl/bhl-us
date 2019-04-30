using System;

namespace MOBOT.BHL.AdminWeb.Controls
{
    public partial class EditHistoryLink : System.Web.UI.UserControl
    {
        private string _entityName = "Unknown";
        private string _entityId = string.Empty;

        public string EntityName { get => _entityName; set { _entityName = value; SetLink(); } }
        public string EntityId { get => _entityId; set { _entityId = value; SetLink(); } }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void SetLink()
        {
            string historyOnClick = "javascript:window.open('EditHistoryList.aspx?name={0}&id={1}', '', 'width=800,height=600,location=0,status=0,scrollbars=1');";
            hypHistory.Attributes["onclick"] = string.Format(historyOnClick, EntityName, EntityId);
        }
    }
}