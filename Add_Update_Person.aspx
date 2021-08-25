<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add_Update_Person.aspx.cs" Inherits="Add_Update_Person" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <script src="http://code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
          function ImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=Image2.ClientID%>').prop('src', e.target.result)
                        .width(100)
                        .height(100);
                };
                reader.readAsDataURL(input.files[0]);
                }
            }

    </script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 293px;
        }
        .auto-style4 {
            width: 121px;
        }
        .auto-style5 {
            width: 196px;
        }
        .auto-style7 {
            width: 91px;
        }
        .auto-style8 {
            width: 201px;
        }
        .auto-style9 {
            width: 181px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>   
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large" Font-Underline="True" Text="Add/Edit Person"></asp:Label>
        <br />
        <br />
        <asp:Panel ID="Panel1" runat="server" Height="184px" Width="1055px" GroupingText="General Information">
            <table class="auto-style1">
                <tr>
                    <td class="auto-style4">
                        <asp:Label ID="lbl_fnm" runat="server" Text="First Name"></asp:Label>
                    </td>
                    <td class="auto-style5">
                        <asp:TextBox ID="txtbox_fnm" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                    </td>
                    <td class="auto-style7">
                        <asp:Label ID="lbl_cty" runat="server" Text="City"></asp:Label>
                    </td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txtbox_cty" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                    </td>
                    <td class="auto-style9">
                        <asp:Label ID="lbl_ise" runat="server" Text="Is Self Employed"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList_semp" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList_semp_SelectedIndexChanged">
                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                            <asp:ListItem Value="N">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">
                        <asp:Label ID="lbl_lnm" runat="server" Text="Last Name"></asp:Label>
                    </td>
                    <td class="auto-style5">
                        <asp:TextBox ID="txtbox_lnm" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                    </td>
                    <td class="auto-style7">
                        <asp:Label ID="lbl_state" runat="server" Text="State"></asp:Label>
                    </td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txtbox_state" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                    </td>
                    <td class="auto-style9">
                        <asp:Label ID="lbl_doj" runat="server" Text="Date Of Joining" TextMode="Date"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtbox_doj" runat="server" TextMode="Date" CausesValidation="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">
                        <asp:Label ID="lbl_design" runat="server" Text="Designation"></asp:Label>
                    </td>
                    <td class="auto-style5">
                        <asp:TextBox ID="txtbox_design" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                    </td>
                    <td class="auto-style7">
                        <asp:Label ID="lbl_cntry" runat="server" Text="Country"></asp:Label>
                    </td>
                    <td class="auto-style8">
                        <asp:DropDownList ID="DDL_cntry" runat="server" AutoPostBack="True" BackColor="#CCCCCC" ForeColor="#003366">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style9">
                        <asp:Label ID="lbl_dob" runat="server" Text="Date Of Birth"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtbox_dob" runat="server" TextMode="Date"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">
                        <asp:Label ID="lbl_org" runat="server" Text="Organization"></asp:Label>
                    </td>
                    <td class="auto-style5">
                        <asp:TextBox ID="txtbox_orgazn" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                    </td>
                    <td class="auto-style7">
                        <asp:Label ID="lbl_zip" runat="server" Text="Zip"></asp:Label>
                    </td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txtbox_zip" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                    </td>
                    <td colspan="2">&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>  
        <br />
    </div>      
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">
                    <asp:Panel ID="Panel2" runat="server" GroupingText="Photograph" Height="224px" Width="261px">
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="251px" onchange="ImagePreview(this);" />
                        <br />
                        <br />
                        <asp:Image ID="Image2" runat="server" Height="90px" ImageUrl="~/images/imgupload.png" Width="80px" />
                        <br />
                        <br />
                        <asp:Button ID="Btn_imguplod" runat="server" Text="Upload" Width="125px" OnClick="Btn_imguplod_Click" />
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/delete.png" Width="20px" />
                        <br />
                    </asp:Panel>
                </td>
                <td>
                    <asp:Panel ID="Panel3" runat="server" GroupingText="Contact Details ">
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>       
    
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table class="auto-style1">
                            <tr>
                                 <td>
                                    <asp:Label ID="lbl_cnttyp" runat="server" Text="Contact Type"></asp:Label>
                                    &nbsp;<asp:DropDownList ID="DDL_cnttyp" runat="server" AutoPostBack="True" BackColor="#CCCCCC" ForeColor="#003366">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_cntdetls" runat="server" Text="Contact Details"></asp:Label>
                                    &nbsp;<asp:TextBox ID="txtbox_cnt" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btn_add" runat="server" OnClick="btn_add_Click" Text="Add" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <br />
                                    <asp:GridView ID="GridView_cntdetails" runat="server" AutoGenerateColumns="False"  OnRowDataBound="GridView_cntdetails_RowDataBound" OnRowDeleting="GridView_cntdetails_RowDeleting" OnRowEditing="GridView_cntdetails_RowEditing" >
                                        <Columns>
                                            <asp:BoundField DataField="ContactType" HeaderText="Contact Type" SortExpression="ContactType" />
                                            <asp:BoundField DataField="Contactdetails" HeaderText="Contact Details" SortExpression="Contactdetails" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtn_delete" runat="server" CommandName="Delete" >Delete</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkbtn_edit" runat="server" CommandName="Edit" >Edit</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton Text="Update" runat="server" OnClick="OnUpdate" />
                                                        <asp:LinkButton Text="Cancel" runat="server" OnClick="OnCancel" />
                                                    </EditItemTemplate>                                                
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                               
                            </tr>
                        </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </td>
                <td>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" />&nbsp;
                    <asp:Button ID="btn_cancel" runat="server" Text="Cancel" />
                </td>
            </tr>
        </table>     
    </form>
</body>
</html>
