using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;

namespace MOBOT.BHL.AdminWeb
{
    public partial class SegmentTypeEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BHLProvider bp = new BHLProvider();
                CustomGenericList<SegmentGenre> segmentGenres = bp.SegmentGenreSelectAll();

                segmentGenres.Sort();

                SegmentGenre emptySegmentGenre = new SegmentGenre();
                emptySegmentGenre.SegmentGenreID = -1;
                emptySegmentGenre.GenreName = "";
                segmentGenres.Insert(0, emptySegmentGenre);

                ddlSegmentTypes.DataSource = segmentGenres;
                ddlSegmentTypes.DataTextField = "GenreName";
                ddlSegmentTypes.DataValueField = "SegmentGenreID";
                ddlSegmentTypes.DataBind();
            }

            litMessage.Text = "";
            errorControl.Visible = false;
        }

        private void clearForm(ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c is TextBox)
                {
                    TextBox textBox = (TextBox)c;
                    textBox.Text = "";
                }
                else if (c.HasControls())
                {
                    clearForm(c.Controls);
                }
            }
        }

        private bool validate()
        {
            bool flag = false;
            if (nameTextBox.Text.Trim().Length == 0)
            {
                flag = true;
                errorControl.AddErrorText("Segment type name is missing");
            }

            errorControl.Visible = flag;

            return !flag;
        }

        #region Event handlers

        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                if (idLabel.Text.Length == 0)
                {
                    errorControl.AddErrorText("Please select a segment type before saving");
                    errorControl.Visible = true;
                    return;
                }

                SegmentGenre segmentGenre = new BHLProvider().SegmentGenreSelectAuto(int.Parse(idLabel.Text));
                segmentGenre.GenreName = nameTextBox.Text.Trim();
                segmentGenre.IsNew = false;

                BHLProvider bp = new BHLProvider();
                bp.SaveSegmentGenre(segmentGenre);

                ddlSegmentTypes.SelectedItem.Text = nameTextBox.Text.Trim();
            }
            else
            {
                return;
            }

            //litMessage.Text = "<span class='liveData'>Segment Type Saved.</span>";
            Response.Redirect("/");
        }

        protected void saveAsNewButton_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                SegmentGenre segmentGenre = new SegmentGenre();
                segmentGenre.SegmentGenreID = 0;
                segmentGenre.GenreName = nameTextBox.Text.Trim();
                segmentGenre.IsNew = true;

                BHLProvider bp = new BHLProvider();
                bp.SaveSegmentGenre(segmentGenre);
            }
            else
            {
                return;
            }

            //litMessage.Text = "<span class='liveData'>Segment Type Added.</span>";
            Response.Redirect("/");
        }

        protected void clearButton_Click(object sender, EventArgs e)
        {
            clearForm(this.Controls);
        }

        protected void ddlSegmentTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearForm(this.Controls);
            int segmentGenreId = int.Parse(ddlSegmentTypes.SelectedValue);
            if (segmentGenreId > 0)
            {
                BHLProvider bp = new BHLProvider();
                SegmentGenre segmentGenre = bp.SegmentGenreSelectAuto(segmentGenreId);
                if (segmentGenre != null)
                {
                    idLabel.Text = segmentGenre.SegmentGenreID.ToString();
                    nameTextBox.Text = segmentGenre.GenreName;
                    ddlSegmentTypes.SelectedValue = segmentGenre.SegmentGenreID.ToString();
                }
            }
        }

        #endregion

    }
}