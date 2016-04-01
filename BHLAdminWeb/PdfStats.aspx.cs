using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Server;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.AdminWeb
{
    public partial class PdfStats : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BHLProvider provider = new BHLProvider();
                gvSummary.DataSource = provider.PDFStatsSelectOverview();
                gvSummary.DataBind();
                gvExpanded.DataSource = provider.PDFStatsSelectExpanded();
                gvExpanded.DataBind();
            }
        }

        private int _totalSummaryPdfs = 0;
        private int _totalSummaryPdfsWithOcr = 0;
        private int _totalSummaryPdfsWithArticleInfo = 0;
        private int _totalSummaryPdfsMissingImages = 0;
        private int _totalSummaryPdfsMissingOcr = 0;

        private int _totalExpandedPdfs = 0;
        private int _totalExpandedPdfsWithOcr = 0;
        private int _totalExpandedPdfsWithArticleInfo = 0;
        private int _totalExpandedPdfsMissingImages = 0;
        private int _totalExpandedPdfsMissingOcr = 0;
        private int _totalExpandedMissingImages = 0;
        private int _totalExpandedMissingOcr = 0;
        private int? _totalExpandedMinutes = 0;

        protected void gv_SummaryRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PDFStats stats = (PDFStats)e.Row.DataItem;
                _totalSummaryPdfs += stats.NumberofPdfs;
                _totalSummaryPdfsWithOcr += stats.PdfsWithOcr;
                _totalSummaryPdfsWithArticleInfo += stats.PdfsWithArticleMetadata;
                _totalSummaryPdfsMissingImages += stats.PdfsWithMissingImages;
                _totalSummaryPdfsMissingOcr += stats.PdfsWithMissingOcr;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[1].Text = _totalSummaryPdfs.ToString();
                e.Row.Cells[2].Text = _totalSummaryPdfsWithOcr.ToString();
                e.Row.Cells[3].Text = _totalSummaryPdfsWithArticleInfo.ToString();
                e.Row.Cells[4].Text = _totalSummaryPdfsMissingImages.ToString();
                e.Row.Cells[5].Text = _totalSummaryPdfsMissingOcr.ToString();
            }
        }

        protected void gv_ExpandedRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PDFStats stats = (PDFStats)e.Row.DataItem;
                _totalExpandedPdfs += stats.NumberofPdfs;
                _totalExpandedPdfsWithOcr += stats.PdfsWithOcr;
                _totalExpandedPdfsWithArticleInfo += stats.PdfsWithArticleMetadata;
                _totalExpandedPdfsMissingImages += stats.PdfsWithMissingImages;
                _totalExpandedPdfsMissingOcr += stats.PdfsWithMissingOcr;
                _totalExpandedMissingImages += stats.TotalMissingImages;
                _totalExpandedMissingOcr += stats.TotalMissingOcr;
                _totalExpandedMinutes += (stats.TotalMinutesToGenerate == null ? 0 : stats.TotalMinutesToGenerate);

                if (stats.PdfStatusID == 40) e.Row.BackColor=System.Drawing.Color.MistyRose;
                //if (stats.PdfsWithMissingImages > 0) e.Row.Cells[6].BackColor = System.Drawing.Color.MistyRose;
                //if (stats.PdfsWithMissingOcr > 0) e.Row.Cells[7].BackColor = System.Drawing.Color.MistyRose;
                //if (stats.TotalMissingImages > 0) e.Row.Cells[8].BackColor = System.Drawing.Color.MistyRose;
                //if (stats.TotalMissingOcr > 0) e.Row.Cells[9].BackColor = System.Drawing.Color.MistyRose;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[4].Text = _totalExpandedPdfs.ToString();
                e.Row.Cells[5].Text = _totalExpandedPdfsWithOcr.ToString();
                e.Row.Cells[6].Text = _totalExpandedPdfsWithArticleInfo.ToString();
                e.Row.Cells[7].Text = _totalExpandedPdfsMissingImages.ToString();
                e.Row.Cells[8].Text = _totalExpandedPdfsMissingOcr.ToString();
                e.Row.Cells[9].Text = _totalExpandedMissingImages.ToString();
                e.Row.Cells[10].Text = _totalExpandedMissingOcr.ToString();
                e.Row.Cells[11].Text = ((double)_totalExpandedMinutes / (double)_totalExpandedPdfs).ToString("#.00");
            }
        }

    }
}
