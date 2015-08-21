using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MOBOT.BHL.AdminWeb
{
	public partial class ErrorControl : System.Web.UI.UserControl
	{
		protected void Page_Load( object sender, EventArgs e )
		{

		}

		public void AddErrorText( string text )
		{
			errorCell.Controls.Add( new LiteralControl( "* " + text + "<br>" ) );
		}
	}
}