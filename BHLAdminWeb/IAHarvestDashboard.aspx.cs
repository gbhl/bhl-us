using MOBOT.BHLImport.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace MOBOT.BHL.AdminWeb
{
    public partial class IAHarvestDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int numLogsToDisplay = Convert.ToInt32(ConfigurationManager.AppSettings["StatsNumberOfLogItemsToDisplay"]);
            int ageLimit = Convert.ToInt32(ConfigurationManager.AppSettings["StatsPendingApprovalDownloadLimit"]);

            BHLImportProvider service = new BHLImportProvider();
            gvItemCountByStatus.DataSource = service.StatsSelectIAItemGroupByStatus();
            gvItemCountByStatus.DataBind();

            hypNumItems.NavigateUrl += ageLimit.ToString();

            gvIAReadyToPublish.DataSource = service.StatsSelectReadyForProductionBySource(1);
            gvIAReadyToPublish.DataBind();

            List<List<Tuple<string, object>>> rows = service.ImportLogSelectRecent(numLogsToDisplay);
            BuildLogTable(rows);            

            gvLatestPubToProdErrors.DataSource = service.ImportErrorSelectRecent(numLogsToDisplay);
            gvLatestPubToProdErrors.DataBind();

            gvIAItemErrors.DataSource = service.IAItemErrorSelectRecent(numLogsToDisplay);
            gvIAItemErrors.DataBind();
        }

        private void BuildLogTable(List<List<Tuple<string,object>>> rows)
        {
            bool firstRow = true;
            foreach (List<Tuple<string, object>> row in rows)
            {
                HtmlTableRow dataRow = new HtmlTableRow();
                if (firstRow)
                {
                    HtmlTableRow headerRow = new HtmlTableRow();
                    headerRow.Cells.Add(GetHeaderCell("Import Date", true, "#FFFFFF"));
                    headerRow.Cells.Add(GetHeaderCell("Identifier", true, "#EEEEEE"));
                    headerRow.Cells.Add(GetHeaderCell("Result", true, "#FFFFFF"));
                    for (int x = 3; x < row.Count; x++)
                    {
                        string bgColor = (x % 2 == 0 ? "#FFFFFF" : "#EEEEEE");
                        headerRow.Cells.Add(GetHeaderCell(row[x].Item1, false, bgColor));
                    }
                    tblImportLog.Rows.Add(headerRow);
                    firstRow = false;
                }

                dataRow.Cells.Add(GetDataCell((row[0].Item2 == null ? "" : row[0].Item2.ToString()), "left", true, "#FFFFFF"));
                dataRow.Cells.Add(GetDataCell((row[1].Item2 == null ? "" : row[1].Item2.ToString()), "left", true, "#EEEEEE"));
                dataRow.Cells.Add(GetDataCell((row[2].Item2 == null ? "" : row[2].Item2.ToString()), "left", true, "#FFFFFF"));
                for (int x = 3; x < row.ToArray().Length; x++)
                {
                    string bgColor = (x % 2 == 0 ? "#FFFFFF" : "#EEEEEE");
                    dataRow.Cells.Add(GetDataCell((row[x].Item2 == null ? "" : row[x].Item2.ToString()), "center", false, bgColor));
                }
                tblImportLog.Rows.Add(dataRow);
            }
        }

        private HtmlTableCell GetHeaderCell(string text, bool noWrap, string bgColor)
        {
            HtmlTableCell headerCell = new HtmlTableCell();
            headerCell.Align = "left";
            headerCell.VAlign = "bottom";
            headerCell.Attributes.Add("scope", "col");
            headerCell.Attributes.Add("style", "font-weight:bold");
            headerCell.BgColor = bgColor;
            headerCell.NoWrap = noWrap;
            headerCell.InnerText = text;
            return headerCell;
        }

        private HtmlTableCell GetDataCell(string text, string align, bool noWrap, string bgColor)
        {
            HtmlTableCell dataCell = new HtmlTableCell();
            dataCell.Align = align;
            dataCell.BgColor = bgColor;
            dataCell.NoWrap = noWrap;
            dataCell.InnerText = text;
            return dataCell;
        }
    }
}
