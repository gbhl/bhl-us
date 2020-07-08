using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

namespace MOBOT.BHL.AdminWeb
{
    public partial class TitleSearch : System.Web.UI.Page
	{
		private bool _refreshSearch = false;
		private SortOrder _sortOrder = SortOrder.Ascending;
		private TitleSearchOrderBy _orderBy = TitleSearchOrderBy.Title;
		public TitleSearchCriteria _searchCriteria;
		private int _sortColumnIndex = 1;
        private String _redirectUrl = "/TitleEdit.aspx?id={0}";

		protected void Page_Load( object sender, EventArgs e )
		{
            HtmlLink cssLnk = new HtmlLink();
            cssLnk.Attributes.Add("rel", "stylesheet");
            cssLnk.Attributes.Add("type", "text/css");
            cssLnk.Href = ConfigurationManager.AppSettings["jQueryUICSSPath"];
            Page.Header.Controls.Add(cssLnk);
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, ConfigurationManager.AppSettings["jQueryPath"]);
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, ConfigurationManager.AppSettings["jQueryUIPath"]);

            if (IsPostBack)
			{
				if ( ViewState[ "SearchCriteria" ] != null )
				{
					_searchCriteria = (TitleSearchCriteria)ViewState[ "SearchCriteria" ];
					_orderBy = (TitleSearchOrderBy)ViewState[ "OrderBy" ];
					_sortOrder = (SortOrder)ViewState[ "SortOrder" ];
                    _searchCriteria.ItemSearch = rdoSearchTypeItem.Checked;
                }
                pagingUserControl.Visible = true;
			}
			else
			{
				pagingUserControl.Visible = false;

                BHLProvider bp = new BHLProvider();

                List<Institution> institutions = bp.InstituationSelectAll();

                listInstitutions.DataSource = institutions;
                listInstitutions.DataBind();
            }

            String redirect = Request.QueryString["redir"] as String;
            if (redirect != null)
            {
                if (redirect.ToLower() == "p")
                {
                    litHeader.Text = "Pagination";
                    liImport.Visible = false;
                    divImport.Visible = false;
                    divSearchType.Visible = true;
                    HyperLinkField linkField = (HyperLinkField)gvwResults.Columns[1];
                    linkField.NavigateUrl = "/Paginator.aspx";
                    linkField.DataNavigateUrlFormatString = "/Paginator.aspx?TitleID={0}";
                    _redirectUrl = "/Paginator.aspx?TitleID={0}&ItemID={1}";
                }
            }

            errorControl.Visible = false;
			Page.SetFocus( titleTextBox );
			Page.Title = "BHL Admin - Title Search";
		}

		private void search()
		{
			if ( rdoSearchTypeTitle.Checked & titleidTextBox.Text.Trim().Length == 0 && titleTextBox.Text.Trim().Length == 0  ) return;
            if ( rdoSearchTypeItem.Checked & itemidTextBox.Text.Trim().Length == 0 ) return;

			BHLProvider bp = new BHLProvider();
			buildSearchCriteria();
			List<Title> results = bp.TitleSearchPaging( _searchCriteria );
			if ( results.Count == 1 )
			{
                string itemID = (results[0].Items.Count > 0) ? results[0].Items[0].ItemID.ToString() : string.Empty;
				Response.Redirect( string.Format(_redirectUrl, results[ 0 ].TitleID.ToString(), itemID));
			}
			else
			{
				pagingUserControl.TotalRecords = (results.Count <= 1) ? results.Count : bp.TitleSearchCount( _searchCriteria );
				pagingUserControl.UpdateDisplay();

				ViewState[ "SearchCriteria" ] = _searchCriteria;
				ViewState[ "OrderBy" ] = _orderBy;
				ViewState[ "SortOrder" ] = _sortOrder;

				gvwResults.DataSource = results;
				gvwResults.DataBind();
			}
		}

		private void buildSearchCriteria()
		{
			if ( _searchCriteria == null )
			{
				_searchCriteria = new TitleSearchCriteria();
			}

			if ( _refreshSearch )
			{
				_searchCriteria = new TitleSearchCriteria();
                _searchCriteria.TitleID = null;
                _searchCriteria.ItemID = null;

                int id;
                if (rdoSearchTypeTitle.Checked)
                {
                    _searchCriteria.ItemSearch = false;
                    _searchCriteria.Title = getNullableString(titleTextBox.Text.Trim());
                    if (titleidTextBox.Text.Trim().Length > 0)
                    {
                        if (int.TryParse(titleidTextBox.Text.Trim(), out id)) _searchCriteria.TitleID = id;
                    }
                }
                else
                {
                    _searchCriteria.ItemSearch = true;
                    if (itemidTextBox.Text.Trim().Length > 0)
                    {
                        if (int.TryParse(itemidTextBox.Text.Trim(), out id)) _searchCriteria.ItemID = id;
                    }
                }

			}

			_searchCriteria.OrderBy = _orderBy;
			_searchCriteria.SortOrder = _sortOrder;
			_searchCriteria.PageSize = Math.Abs( pagingUserControl.StartIndex - pagingUserControl.EndIndex ) + 1;
			_searchCriteria.StartRow = pagingUserControl.StartIndex;
		}

		private string getNullableString( string input )
		{
			if ( input != null && input.Trim().Length > 0 ) return '%' + input.Trim() + "%";
			return null;
		}

        private void HarvestMarcData(String marcFile, int batchID, String institutionCode)
        {
            BHLProvider provider = new BHLProvider();

            // Open the file and parse the data within it
            XmlDocument xml = new XmlDocument();
            xml.Load(marcFile);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
            nsmgr.AddNamespace("ns", "http://www.loc.gov/MARC21/slim");

            // update the root Marc information
            String leader = String.Empty;
            XmlNode marcNode = xml.SelectSingleNode("ns:record/ns:leader", nsmgr);
            if (marcNode != null) leader = marcNode.InnerText;
            Marc marc = provider.MarcInsertAuto(batchID, 
                marcFile.Replace(ConfigurationManager.AppSettings["MarcUploadDrive"], ConfigurationManager.AppSettings["MarcUploadServer"]), 
                institutionCode, leader, null);

            // Insert the new Marc control information
            XmlNodeList controlFields = xml.SelectNodes("ns:record/ns:controlfield", nsmgr);
            foreach (XmlNode controlField in controlFields)
            {
                String tag = (controlField.Attributes["tag"] == null) ? String.Empty : controlField.Attributes["tag"].Value;
                String value = controlField.InnerText;
                provider.MarcControlInsertAuto(marc.MarcID, tag, value);
            }

            // Insert the new Marc data field and subfield information
            XmlNodeList dataFields = xml.SelectNodes("ns:record/ns:datafield", nsmgr);
            foreach (XmlNode dataField in dataFields)
            {
                String tag = (dataField.Attributes["tag"] == null) ? String.Empty : dataField.Attributes["tag"].Value;
                String indicator1 = (dataField.Attributes["ind1"] == null) ? String.Empty : dataField.Attributes["ind1"].Value;
                String indicator2 = (dataField.Attributes["ind2"] == null) ? String.Empty : dataField.Attributes["ind2"].Value;
                MarcDataField marcDataField = provider.MarcDataFieldInsertAuto(marc.MarcID, tag, indicator1, indicator2);

                XmlNodeList subFields = dataField.SelectNodes("ns:subfield", nsmgr);
                foreach (XmlNode subField in subFields)
                {
                    String code = (subField.Attributes["code"] == null) ? String.Empty : subField.Attributes["code"].Value;
                    String value = subField.InnerText;
                    provider.MarcSubFieldInsertAuto(marcDataField.MarcDataFieldID, code, value);
                }
            }
        }

		#region Event handlers

        protected void importButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileUpload.HasFile)
                {
                    BHLProvider provider = new BHLProvider();
                    List<String> files = new List<String>();

                    // Get the identifier for this import batch
                    MarcImportBatch batch = provider.MarcImportBatchInsertAuto(fileUpload.FileName, listInstitutions.SelectedValue);

                    String filePath = String.Format(ConfigurationManager.AppSettings["MarcUploadPath"], batch.MarcImportBatchID.ToString(), fileUpload.FileName);
                    String xmlFilePath = filePath + ".xml";

                    // Save the file
                    fileUpload.SaveAs(filePath);

                    // --- Process the file ---

                    int result = 0;
                    if (rdoType.SelectedValue == "1")   // MRC file
                    {
                        // Convert MARC21 to MARCXML
                        throw new NotImplementedException();
                    }
                    else // MARCXML file
                    {
                        xmlFilePath = filePath;
                    }

                    if (result >= 0)
                    {
                        // Marc conversion OK

                        // Extract the MARC record(s) from the MARCXML file into individual MARCXML
                        // files.  Even if the original fine contains only a single MARC record,
                        // this action will remove any extra headers (OAI, etc) that might be
                        // contained in the file.
                        String xmlString = File.ReadAllText(xmlFilePath);
                        String recordStartTag = String.Empty;
                        String recordEndTag = String.Empty;
                        if (xmlString.Contains("<marc:record"))
                        {
                            recordStartTag = "<marc:record";
                            recordEndTag = "</marc:record>";
                        }
                        else
                        {
                            recordStartTag = "<record";
                            recordEndTag = "</record>";
                        }

                        // Get the endpoints of the first record
                        int recordCount = 1;
                        int startPos = xmlString.IndexOf(recordStartTag, StringComparison.InvariantCultureIgnoreCase);
                        int endPos = xmlString.IndexOf(recordEndTag, StringComparison.InvariantCultureIgnoreCase);
                        while (startPos != -1)
                        {
                            // Get the record
                            String recordString = xmlString.Substring(startPos, endPos + recordEndTag.Length - startPos);

                            // Add namespace declarations
                            recordString = recordString.Replace("<record>", "<record xmlns=\"http://www.loc.gov/MARC21/slim\">");
                            recordString = recordString.Replace("<marc:record>", "<marc:record xmlns:marc=\"http://www.loc.gov/MARC21/slim\">");

                            // Write record to a file
                            File.WriteAllText(filePath + "." + recordCount.ToString() + ".xml", recordString);
                            files.Add(filePath + "." + recordCount.ToString() + ".xml");
                            recordCount++;

                            // Find the endpoints of the next record
                            startPos = xmlString.IndexOf(recordStartTag, endPos + recordEndTag.Length, StringComparison.InvariantCultureIgnoreCase);
                            endPos = xmlString.IndexOf(recordEndTag, endPos + recordEndTag.Length, StringComparison.InvariantCultureIgnoreCase);
                        }
                    }
                    else
                    {
                        // Present error message to user
                        errorControl.AddErrorText("Error converting MARC: " + result.ToString());
                        errorControl.Visible = true;
                    }

                    // For each of the files in the "files" list, parse the MARCXML
                    // into database records.
                    foreach (String file in files)
                    {
                        this.HarvestMarcData(file, batch.MarcImportBatchID, listInstitutions.SelectedValue);
                    }

                    // Try to match the just imported MARC records to existing BHL titles
                    if (provider.MarcResolveTitles(batch.MarcImportBatchID))
                    {
                        // Redirect to page that summarizes the records being imported
                        Response.Redirect("TitleImport.aspx?id=" + batch.MarcImportBatchID.ToString());
                    }
                    else
                    {
                        // Present error message to user
                        errorControl.AddErrorText("Error resolving uploaded titles");
                        errorControl.Visible = true;
                    }
                }
                else
                {
                    // Present error message to user
                    errorControl.AddErrorText("Please select a file to import");
                    errorControl.Visible = true;
                }
            }
            catch (Exception ex)
            {
                // Present error message to user
                errorControl.AddErrorText("Error importing MARC: " + ex.Message);
                errorControl.Visible = true;
            }
        }

		protected void searchButton_Click( object sender, EventArgs e )
		{
			_refreshSearch = true;
			pagingUserControl.PageNumber = 1;
			search();
		}

		protected void pagingUserControl_Search( object sender, EventArgs e )
		{
			_refreshSearch = false;
			search();
		}

		protected void gvwResults_Sorting( object sender, GridViewSortEventArgs e )
		{
			TitleSearchOrderBy origOrderBy = _orderBy;
			if ( e.SortExpression.Equals( "TitleID" ) )
			{
				_orderBy = TitleSearchOrderBy.TitleID;
				_sortColumnIndex = 0;
			}
			else if ( e.SortExpression.Equals( "SortTitle" ) )
			{
				_orderBy = TitleSearchOrderBy.Title;
				_sortColumnIndex = 1;
			}

			if ( origOrderBy == _orderBy )
			{
				if ( _sortOrder == SortOrder.Descending )
				{
					_sortOrder = SortOrder.Ascending;
				}
				else
				{
					_sortOrder = SortOrder.Descending;
				}
			}
			else
			{
				_sortOrder = SortOrder.Ascending;
			}

			_refreshSearch = true;
			pagingUserControl.PageNumber = 1;
			search();
		}

		protected void gvwResults_RowDataBound( object sender, GridViewRowEventArgs e )
		{
			if ( e.Row.RowType == DataControlRowType.Header )
			{
				Image img = new Image();
				img.Attributes[ "style" ] = "padding-bottom:2px";
				if ( _sortOrder == SortOrder.Ascending )
				{
					img.ImageUrl = "/images/up.gif";
				}
				else
				{
					img.ImageUrl = "/images/down.gif";
				}

				e.Row.Cells[ _sortColumnIndex ].Controls.Add( new LiteralControl( " " ) );
				e.Row.Cells[ _sortColumnIndex ].Controls.Add( img );
				e.Row.Cells[ _sortColumnIndex ].Wrap = false;
			}
		}

		#endregion

	}
}
