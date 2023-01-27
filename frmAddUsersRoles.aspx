<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmAddUsersRoles.aspx.cs" Inherits="frmAddUsersRoles" %>

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
    <link rel="stylesheet" href="../plugins/icheck-bootstrap/icheck-bootstrap.min.css" />
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

<div class="zoomd">
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header c">

                    <label class="chart_label mb-0">Add User Roles</label>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <label class="control-label">
                                Enter Role :
        <asp:RequiredFieldValidator ID="RfvtxtRoleName" runat="server" ErrorMessage="*" ForeColor="Red" Font-Bold="true" ControlToValidate="txtRoleName" ValidationGroup="Role"></asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox ID="txtRoleName" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                        </div>
                        <div class="col-md-1 mt-2  mr-5">
                            <asp:Button ID="btnSaveRole" runat="server" Text="SUBMIT" CssClass="btn btn-sm c" OnClick="btnSaveRole_Click" Style="margin-top: 25px" ValidationGroup="Role" />
                        </div>
                        <div class="col-md-3 ">
                            <div class="well1 white">
                                <label class="control-label">
                                    Select User :
                    <asp:RequiredFieldValidator ID="RfvddlUsers" runat="server" ErrorMessage="*" InitialValue="0" ForeColor="Red" Font-Bold="true" ControlToValidate="ddlUsers" ValidationGroup="RoleA"></asp:RequiredFieldValidator>
                                </label>
                                <asp:DropDownList ID="ddlUsers" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>




                    </div>
                    <div class="row mt-2">
                        <div class="col-md-4 mr-5">
                            <div class="card">
                                <div class="card-header c">
                                    <label class="chart_label mb-0">Menu List</label>
                                </div>
                                <div class="card-body">

                                    <div style="overflow-y: scroll; height: 500px;">
                                        <asp:GridView ID="gvMasterRoles" DataKeyNames="MenuID" AutoGenerateColumns="false" runat="server" CssClass="table  table-bordered">

                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField HeaderText="MenuName" DataField="MenuName" ItemStyle-Font-Size="Medium" />
                                                <asp:BoundField HeaderText="MenuID" DataField="MenuID" ItemStyle-Font-Size="Medium" />
                                            </Columns>
                                            <RowStyle BackColor="White" BorderColor="#e3e4e6" BorderWidth="1px" Height="10px" Font-Size="Medium" />
                                            <FooterStyle BackColor="#EDEDED" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#EDEDED" ForeColor="#000000" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#688df5" Font-Bold="True" Height="10px" ForeColor="#000000" />
                                            <HeaderStyle BackColor="#F5F5F5 " Font-Bold="True" ForeColor="Gray" Height="10px" Font-Size="Medium" />
                                            <EditRowStyle BackColor="White" BorderColor="#e3e4e6" BorderStyle="Solid" Height="10px" BorderWidth="1px" />
                                            <AlternatingRowStyle BackColor="White" BorderColor="#e3e4e6" Font-Size="Medium" Height="10px" BorderStyle="Solid" BorderWidth="1px" />
                                        </asp:GridView>
                                    </div>
                                    <asp:Button ID="btnMasterRoleApply" runat="server" Text="Apply" OnClick="btnMasterRoleApply_Click" CssClass="btn btn-sm c col-md-3 offset-9 fa-pull-right" ValidationGroup="RoleA" />
                                </div>

                            </div>
                        </div>

                        <div class="col-md-7 ">
                            <div class="card">
                                <div class="card-header c">
                                    <label class="chart_label mb-0">Role Wise Menu List</label>
                                </div>
                                <div class="card-body">
                                    <div style="overflow-y: scroll; height: 500px">
                                        <asp:GridView ID="gvAllRoles" DataKeyNames="ID" AutoGenerateColumns="false" CssClass="table  table-bordered" runat="server" OnRowCommand="gvAllRoles_RowCommand">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" Checked='<%# Eval("MenuStatus").ToString().Equals("Active")  %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField HeaderText="Menu Name" DataField="MenuName" ItemStyle-Font-Size="Medium" />
                                                <asp:BoundField HeaderText="User Name" DataField="UserName" ItemStyle-Font-Size="Medium" />
                                                <asp:BoundField HeaderText="Menu Status" DataField="MenuStatus" ItemStyle-Font-Size="Medium" />
                                                <asp:BoundField HeaderText="MenuID" DataField="MenuID" ItemStyle-Font-Size="Medium" />
                                                <%-- <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="c" CommandName="DeleterRole" CommandArgument='<%#Eval("ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Remove">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgdel" runat="server" CommandName="DeleterRole" CommandArgument='<%#Eval("ID") %>' ImageUrl="~/Images/New folder/delnew.png" ToolTip="Delete" CausesValidation="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="White" BorderColor="#e3e4e6" BorderWidth="1px" Height="10px" Font-Size="Medium" />
                                            <FooterStyle BackColor="#EDEDED" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#EDEDED" ForeColor="#000000" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#688df5" Font-Bold="True" Height="10px" ForeColor="#000000" />
                                            <HeaderStyle BackColor="#F5F5F5 " Font-Bold="True" ForeColor="Gray" Height="10px" Font-Size="Medium" />
                                            <EditRowStyle BackColor="White" BorderColor="#e3e4e6" BorderStyle="Solid" Height="10px" BorderWidth="1px" />
                                            <AlternatingRowStyle BackColor="White" BorderColor="#e3e4e6" Font-Size="Medium" Height="10px" BorderStyle="Solid" BorderWidth="1px" />
                                        </asp:GridView>
                                    </div>
                                    <asp:Button ID="btnUpdateRoles" runat="server" Text="Apply" CssClass="btn btn-sm btn-success warning_3" OnClick="btnUpdateRoles_Click" Visible="false" />
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</div>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    <link rel="stylesheet" href="../Scripts/chosen.css" />
</asp:Content>

<%-- checked='<%# Eval("MenuStatus").ToString().Equals("Active")  %>'--%>