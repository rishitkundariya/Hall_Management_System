using HallManagementSystem.BAL;
using HallManagementSystem.ENT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrontPanel_Login_UserLogin : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["UserID"]!= null && Session["AdminID"] == null )
        {
           Response.Redirect("~/FrontPanel/Hall/HallList.aspx");
        }
    }
    #endregion

    #region Button Login Click
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        UserDetailsBAL balUserDetails = new UserDetailsBAL();
        UserDetailsENT entUserDetails = new UserDetailsENT();

        entUserDetails = balUserDetails.SelectForUsernamePassword(txtUsername.Text.Trim(), txtPassword.Text.Trim());

        if (balUserDetails.Message == null)
        {
            if (Session["UserID"] == null)
            {
                if (!entUserDetails.UserID.IsNull)
                    Session["UserID"] = entUserDetails.UserID.Value.ToString();

                if (!entUserDetails.DisplayName.IsNull)
                    Session["UserDisplayName"] = entUserDetails.DisplayName.Value.ToString();
                else
                    Session["UserDisplayName"] = entUserDetails.Username.Value.ToString();

                if (!entUserDetails.PhotoPath.IsNull)
                    Session["UserPhotoPath"] = entUserDetails.PhotoPath.Value.ToString();

                Response.Redirect("~/FrontPanel/Hall/HallList.aspx");
            }
            else
            {
                lblErrorMessage.Text = "Another User is already logged In, kindly close the current session or use another browser";
            }
        }
        else
        {
            lblErrorMessage.Text = balUserDetails.Message;
        }
    }
    #endregion
}
