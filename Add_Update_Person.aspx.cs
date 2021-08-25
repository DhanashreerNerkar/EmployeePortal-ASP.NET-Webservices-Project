using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ServiceReference1_ValidateDOJ;
using ServiceReference2_ValidateDOB;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing.Imaging;
using System.Xml.Linq;
using System.Xml;

public partial class Add_Update_Person : System.Web.UI.Page
{
    public string strConnectionString = "Data Source=USER-PC; Integrated Security=True; Initial Catalog=SC_14;";
    private SqlCommand _sqlCommand;
    private SqlDataReader _sqlDataReader;
    private SqlDataAdapter _sqlDataAdapter;
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindCountryDropDown();
            BindContactTypeDropDown();

            dt = new DataTable();
            if (ViewState["Records"] == null)
            {          
                dt.Columns.Add("ContactType");
                dt.Columns.Add("Contactdetails");
                ViewState["Records"] = dt;
            }
        }
    }
    public void CreateConnection()
    {
        SqlConnection _sqlConnection = new SqlConnection(strConnectionString);
        _sqlCommand = new SqlCommand();
        _sqlCommand.Connection = _sqlConnection;
    }
    public void OpenConnection()
    {
        _sqlCommand.Connection.Open();
    }
    public void CloseConnection()
    {
        _sqlCommand.Connection.Close();
    }
    public void DisposeConnection()
    {
        _sqlCommand.Connection.Dispose();
    }
    public void BindContactTypeDropDown()
    {
        try
        {
            CreateConnection();
            OpenConnection();

            _sqlCommand.CommandText = "Sp_selectcontacttype";
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
            dt = new DataTable();
            _sqlDataAdapter.Fill(dt);
            _sqlCommand.ExecuteReader();

            //DDL_cnttyp.Items.Insert(0, new ListItem("select", "select"));
            DDL_cnttyp.DataTextField = "Contacttype";
            DDL_cnttyp.DataValueField = "ID";
            DDL_cnttyp.DataSource = dt;
            DDL_cnttyp.DataBind();
        }
        catch (Exception exp)
        { }
        finally
        {
            CloseConnection();
            DisposeConnection();
        }      
    }
    public void BindCountryDropDown()
    {
        try
        {
            CreateConnection();
            OpenConnection();

            _sqlCommand.CommandText = "Sp_selectcountry";
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
            dt = new DataTable();
            _sqlDataAdapter.Fill(dt);
            _sqlCommand.ExecuteReader();
            DDL_cntry.DataTextField = "Name";
            DDL_cntry.DataValueField = "ID";
            DDL_cntry.DataSource = dt;
            DDL_cntry.DataBind();
        }
        catch (Exception exp)
        { }
        finally
        {
            CloseConnection();
            DisposeConnection();
        }
    }
    static Boolean x;
    protected void RadioButtonList_semp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList_semp.SelectedValue == "Y")
        {
            txtbox_doj.Enabled = false;
            x = true;
        }
        else
        {
            txtbox_doj.Enabled = true;
            x = false;
        }
    }  
    protected void btn_save_Click(object sender, EventArgs e)
    {
        //WebService_ValidateDOJSoapClient doj = new WebService_ValidateDOJSoapClient();
        //Int32 ok1 = doj.CheckDateOfJoining(txtbox_doj.Text);
        //if (ok1 == 0)
        //{
        //    txtbox_doj.Text = "";
        //    txtbox_doj.ForeColor = System.Drawing.Color.Red;
        //    txtbox_doj.Focus();
        //}

        //WebService_ValidateDOBSoapClient dob = new WebService_ValidateDOBSoapClient();
        //Int32 ok2 = dob.CheckDateOfBirth(txtbox_dob.Text, txtbox_doj.Text);
        //if (ok2 == 0)
        //{
        //    txtbox_dob.Text = "";
        //    txtbox_dob.ForeColor = System.Drawing.Color.Red;
        //    txtbox_dob.Focus();
        //}

        //**xml file**
        dt = (DataTable)ViewState["Records"];
        string xmfilename = "PersonContacts.xml";
        dt.WriteXml("C:/" + xmfilename);
        var xmlcontacts = File.ReadAllText("C:/PersonContacts.xml");
        try
        {
            CreateConnection();
            OpenConnection();

            _sqlCommand.CommandText = "Sp_InsertPerson";
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            

            _sqlCommand.Parameters.AddWithValue("@param_fnm", txtbox_fnm.Text);                                     //First Name 
            _sqlCommand.Parameters.AddWithValue("@param_lnm", txtbox_lnm.Text);                                      //Last Name
            _sqlCommand.Parameters.AddWithValue("@param_dob", Convert.ToDateTime(txtbox_dob.Text));                       //DOB
            _sqlCommand.Parameters.AddWithValue("@param_organization", txtbox_orgazn.Text);                               //Organization
            _sqlCommand.Parameters.AddWithValue("@param_designation", txtbox_design.Text);                                //Designation 
            //_sqlCommand.Parameters.AddWithValue("@param_country", Convert.ToInt32(DDL_cntry.SelectedIndex));
            //_sqlCommand.Parameters.Add("@param_country", SqlDbType.Int).Value = DDL_cntry.SelectedIndex;//Country
            _sqlCommand.Parameters.AddWithValue("@param_state", txtbox_state.Text);                                       //State
            _sqlCommand.Parameters.AddWithValue("@param_city", txtbox_cty.Text);                                          //City
            _sqlCommand.Parameters.AddWithValue("@param_zip", txtbox_zip.Text);                                       //Zip Code            
            _sqlCommand.Parameters.AddWithValue("@param_photoname", img);  //image file
            _sqlCommand.Parameters.AddWithValue("@param_photocontent", bytes);                                           //image
            //_sqlCommand.Parameters.AddWithValue("@param_isselfemp", x);//is self emp ?
            _sqlCommand.Parameters.AddWithValue("@param_doj", Convert.ToDateTime(txtbox_doj.Text));                       //DOJ
            _sqlCommand.Parameters.AddWithValue("@param_contacts", xmlcontacts);                                           //contact details

            _sqlCommand.ExecuteNonQuery();

        }
        catch (Exception exp)
        { }
        finally
        {
            CloseConnection();
            DisposeConnection();
        }

        

        //string constr = strConnectionString;
        //using (SqlConnection conn = new SqlConnection(constr))
        //{
        //    string sql = "INSERT INTO tblxml VALUES(@xmldata)";
        //    using (SqlCommand cmd = new SqlCommand(sql, conn))
        //    {
        //        cmd.Parameters.AddWithValue("@xmldata", xmlcontacts);
        //        conn.Open();
        //        cmd.ExecuteNonQuery();
        //        conn.Close();
        //    }
        //}
        //dt = (DataTable)ViewState["Records"];
        //int metaIndex = 0;
        //string xmlcontacts = ToXml(dt, metaIndex);
        //XmlDocument xDoc = new XmlDocument();
        //xDoc.LoadXml(xmlcontacts);
        


    }
    //public string ToXml(DataTable table, int metaIndex)
    //{
    //    XDocument xdoc = new XDocument(
    //        new XElement(table.TableName,
    //            from column in table.Columns.Cast<DataColumn>()
    //            where column != table.Columns[metaIndex]
    //            select new XElement(column.ColumnName,
    //                from row in table.AsEnumerable()
    //                select new XElement(row.Field<string>(metaIndex), row[column])
    //                )
    //            )
    //        );
    //    return xdoc.ToString();
    //}
    protected void btn_add_Click(object sender, EventArgs e)
    {
        dt = (DataTable)ViewState["Records"];
        dt.Rows.Add(DDL_cnttyp.SelectedItem.ToString(), txtbox_cnt.Text);
        GridView_cntdetails.DataSource = dt;
        GridView_cntdetails.DataBind();
    }
    protected void GridView_cntdetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);
        dt = (DataTable)ViewState["Records"];
        dt.Rows[index].Delete();
        ViewState["Records"] = dt;
        GridView_cntdetails.DataSource = dt;
        GridView_cntdetails.DataBind();
    }
    protected void GridView_cntdetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string item = e.Row.Cells[0].Text;
            foreach (LinkButton lnkbtn_del in e.Row.Cells[2].Controls.OfType<LinkButton>())
            {
                if (lnkbtn_del.CommandName == "Delete")
                {
                    lnkbtn_del.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){ return false; };";
                }
                if (lnkbtn_del.CommandName == "Edit")
                {
                    txtbox_cnt.Text = GridView_cntdetails.Rows[GridView_cntdetails.SelectedIndex].Cells[1].Text;
                }
            }
        }        
    }
    protected void GridView_cntdetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView_cntdetails.EditIndex = e.NewEditIndex;
        dt = (DataTable)ViewState["Records"];
        ViewState["Records"] = dt;
        GridView_cntdetails.DataSource = dt;
        GridView_cntdetails.DataBind();
    }
    protected void OnUpdate(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
        string cntyp = (row.Cells[0].Controls[0] as TextBox).Text;
        string cntdel = (row.Cells[1].Controls[0] as TextBox).Text;
        DataTable dt = (DataTable)ViewState["Records"];
        dt.Rows[row.RowIndex]["ContactType"] = cntyp;
        dt.Rows[row.RowIndex]["Contactdetails"] = cntdel;
        ViewState["Records"] = dt;
        GridView_cntdetails.EditIndex = -1;
        GridView_cntdetails.DataSource = dt;
        GridView_cntdetails.DataBind();
    }
    protected void OnCancel(object sender, EventArgs e)
    {
        GridView_cntdetails.EditIndex = -1;
        GridView_cntdetails.DataSource = dt;
        GridView_cntdetails.DataBind();
    }
   
    public static string img = "", imgfilename="";
    static byte[] bytes;
    protected void Btn_imguplod_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string fname = FileUpload1.FileName;
            FileUpload1.SaveAs(Server.MapPath("~/UploadedImages/" + fname));
            img = "~/UploadedImages/" + fname;
        }

        using (BinaryReader br = new BinaryReader(FileUpload1.PostedFile.InputStream))
        { bytes = br.ReadBytes(FileUpload1.PostedFile.ContentLength); }
        imgfilename = Path.GetFileName(FileUpload1.PostedFile.FileName);
    }
}