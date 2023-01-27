<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmuser.aspx.cs" Inherits="frmuser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
        <script>
            function getxtValue(that) {
                document.getElementById("lable").innerHTML = that.value;
            }
        </script>
<script>
    function pageLoad() {
        jQuery(".chzn-select").data("placeholder", "Select Frameworks...").chosen();
    }
</script>
        <link rel="stylesheet" href="../plugins/fontawesome-free/css/all.min.css" />
    <!-- overlayScrollbars -->
    <link rel="stylesheet" href="../plugins/overlayScrollbars/css/OverlayScrollbars.min.css" />
    <!-- Theme style -->
      <link rel="stylesheet" href="../Scripts/chosen.css" />
    <link rel="stylesheet" href="../dist/css/adminlte.min.css" />
    <style>
       .c {
            color: white;
          /*  //background: #698DF2;*/
          background:transparent linear-gradient(180deg, #5D7FA7 0%, #2E4E74 100%) 0% 0% no-repeat padding-box;
            border: none;
        }
         .chart_label{
            font-size:larger;
            font-weight:500;
            letter-spacing:0px;
            opacity:1;
            text-align:left;
            
        }
        .zoomd
            {

                zoom:75%
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="messagealert" id="alert_container">
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modall">
                <div class="center">
                    <img alt="" src="../images/loader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section class="content">
                <div class="zoomd">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col">

                            <div class="card">
                                <div class="card-header c">
                                          
                                            <label class="chart_label mb-0">Add User</label>
                                </div>
                                <div class="card-body">

                                    <div class="form-group">
                                        <asp:Label ID="lblerrorMsg" runat="server" Text="" CssClass="label" ForeColor="#ff0000"></asp:Label>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <label class="control-label ">
                                                    Login Name :
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLoginName" ErrorMessage="Required" ForeColor="Red" ValidationGroup="CreateUser"></asp:RequiredFieldValidator>
                                                </label>
                                                <asp:TextBox ID="txtLoginName" runat="server" class="form-control form-control-sm chzn-select-deselect" autocomplete="off"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1 mt-4">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" class="btn btn-sm c" />
                                            </div>
                                            <div class="col-md-3">
                                                <label class="control-label ">Name : </label>
                                                <asp:RequiredFieldValidator ID="RequiredtxtUsername" runat="server" ErrorMessage="Required" ForeColor="Red" ValidationGroup="CreateUser" ControlToValidate="txtUsername" CssClass="bold"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtUsername" runat="server" class="form-control form-control-sm chzn-select-deselect" autocomplete="off"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3">
                                                <label class="control-label ">&nbsp;Emp ID : </label>
                                                <asp:TextBox ID="txtEmpID" runat="server" class="form-control form-control-sm" autocomplete="off"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3">
                                                <label class="control-label ">
                                                    Email ID : 
                                 <asp:RequiredFieldValidator ID="RequiredtxtLoginName" runat="server" ControlToValidate="txtEmail" ErrorMessage="Required" ForeColor="Red" ValidationGroup="CreateUser"></asp:RequiredFieldValidator>
                                                </label>
                                                <asp:TextBox ID="txtEmail" runat="server" class="form-control form-control-sm" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row mt-1">
                                            <div class="col-md-3">
                                                <label class="control-label ">User Password : </label>
                                                <asp:TextBox ID="txtPass" runat="server" class="form-control form-control-sm" autocomplete="off" TextMode="Password"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3">
                                                <label class="control-label ">Confirm Password : </label>
                                                <asp:TextBox ID="txtConPass" runat="server" class="form-control form-control-sm" autocomplete="off" TextMode="Password"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="control-label ">User Role : </label>
                                                <asp:DropDownList ID="ddlUserRoles" runat="server" class="form-control form-control-sm chzn-select">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="control-label ">Status : </label>
                                                <asp:DropDownList ID="ddlStatus" runat="server" class="form-control form-control-sm chzn-select">
                                                    <asp:ListItem Value="Active">Active</asp:ListItem>
                                                    <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row mt-1">
                                            <div class="col-md-3 ">
                                                <label class="control-label ">
                                                    Location : 
                        <asp:RequiredFieldValidator ID="RequiredddlLocation" runat="server" ControlToValidate="ddlLocation" InitialValue="0" ErrorMessage="Required" ForeColor="Red" ValidationGroup="CreateUser"></asp:RequiredFieldValidator>
                                                </label>
                                                <asp:DropDownList ID="ddlLocation" runat="server" class="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 ">
                                                <label class="control-label ">
                                                    Department :
                                                    <asp:RequiredFieldValidator ID="Requiredddldepartment" runat="server" ControlToValidate="ddldepartment" InitialValue="0" ErrorMessage="Required" ForeColor="Red" ValidationGroup="CreateUser"></asp:RequiredFieldValidator>
                                                </label>
                                                <asp:DropDownList ID="ddldepartment" runat="server" class="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="control-label ">Designation : </label>
                                                <asp:TextBox ID="txtDesignation" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 ">
                                                <label class="control-label ">
                                                    Manager :   
                                                </label>
                                                <asp:TextBox ID="txtManager" runat="server" class="form-control form-control-sm" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row mt-1">
                                            <div class="col-md-3">
                                                <label class="control-label ">Contact No : </label>
                                                <asp:TextBox ID="txtContactNo" runat="server" class="form-control form-control-sm" autocomplete="off"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3">
                                                <label class="control-label ">Remarks : </label>
                                                <asp:TextBox ID="txtUserRemarks" runat="server" class="form-control form-control-sm" autocomplete="off"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="control-label ">Domain/Non Domain : </label>
                                                <asp:DropDownList ID="ddlDomain" runat="server" class="form-control form-control-sm chzn-select">
                                                    <asp:ListItem Value="Domain">Domain</asp:ListItem>
                                                    <asp:ListItem Value="NonDomain">Non-Domain</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3 mt-2 ">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-sm c" OnClick="btnSave_Click" ValidationGroup="CreateUser" />
                                                <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn btn-sm c" OnClick="btnUpdate_Click" ValidationGroup="CreateUser" Visible="False" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
          

                <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="form-group">
                                    <div class="row">

                                        <div class="col-md-3">
                                            <label class="control-label ">
                                                Enter User Name : 
                                   <asp:RequiredFieldValidator ID="RequiredtxtSearch" runat="server" ControlToValidate="txtSearch" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Search"></asp:RequiredFieldValidator>
                                            </label>
                                            <div class="input-group">
                                                <asp:TextBox ID="txtSearch" runat="server" class="form-control form-control-sm input-search" placeholder="Search..."></asp:TextBox>
                                                <span class="input-group-append mt-0 ">
                                                    <asp:ImageButton ID="ImgbtnSearchUser" runat="server" class="btn c mt-0 btn-outline-success" ImageUrl="~/Images/search_iconnew.png" ValidationGroup="Search" OnClick="ImgbtnSearchUser_Click" />
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>

                                            <label class="control-label float-right ">
                                                Total : 
                                <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>
                                            </label>

                                        </div>
                                    </div>
                                    <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ShowHeader="true" AllowPaging="true"
                                        EmptyDataText="No Records Found" OnRowCommand="gvUsers_RowCommand" DataKeyNames="UserID" OnPageIndexChanging="gvUsers_PageIndexChanging" OnSelectedIndexChanging="gvUsers_SelectedIndexChanging" PageSize="100">
                                        <Columns>
                                            <asp:BoundField HeaderText="User Emp. ID" DataField="EmpID" />
                                            <asp:BoundField HeaderText="User Name" DataField="UserName" />
                                            <asp:BoundField HeaderText="Login Name" DataField="Frm_UID" />
                                            <asp:BoundField HeaderText="Email ID" DataField="EmailID" />
                                            <asp:BoundField HeaderText="UserType" DataField="UserType" />
                                            <asp:BoundField HeaderText="Location" DataField="Location" />
                                            <asp:BoundField HeaderText="Department" DataField="DepName" />
                                            <asp:BoundField HeaderText="Contact.No" DataField="ContactNo" />
                                            <asp:BoundField HeaderText="Status" DataField="Status" />
                                            <asp:TemplateField>
                                                <HeaderTemplate></HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEdit" runat="server" CommandName="SelectUser" CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/images/edit23.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <HeaderTemplate></HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnGetDetails" runat="server" CommandName="getDetials" CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/images/Details.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle BackColor="White" BorderColor="#e3e4e6" CssClass="text-center" BorderWidth="1px" Height="20px" Font-Size="Small" />
                                        <FooterStyle BackColor="#EDEDED" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#EDEDED" ForeColor="#000000" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="White" Font-Bold="True" Height="20px" ForeColor="#000000" />
                                        <HeaderStyle Font-Bold="True" CssClass="text-center c" ForeColor="White" Height="20px" Font-Size="Small" />
                                        <EditRowStyle BackColor="#e9edf2" BorderColor="#e3e4e6" BorderStyle="Solid" Height="20px" BorderWidth="1px" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#EDEDED" />
                                        <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                                        <AlternatingRowStyle BackColor="White" BorderColor="#e3e4e6" Font-Size="Small" Height="20px" BorderStyle="Solid" BorderWidth="1px" />

                                    </asp:GridView>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
 <Triggers>
           <asp:PostBackTrigger ControlID="btnSearch" />
           <asp:PostBackTrigger ControlID="ImgbtnSearchUser" />
       </Triggers>
    </asp:UpdatePanel>
          <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
   <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    <link rel="stylesheet" href="../Scripts/chosen.css" />
        <link rel="stylesheet" href="../css/modal.css" />
</asp:Content>

