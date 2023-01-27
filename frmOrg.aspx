<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmOrg.aspx.cs" Inherits="frmOrg" %>

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

                zoom:80%
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="zoomd">
    <div class="col-md-12 graphs">
        <div class="xs">
            <div class="well1 white">
                <div class="Compose-Message">
                    <div class="card card-default">
                        <div class="card-header c">
                            Add Organization         
                        </div>
                        <div class="card-body">

                            <div class="form-group">
                                <asp:Label ID="lblerrorMsg" runat="server" Text="" CssClass="label" ForeColor="#ff0000"></asp:Label>
                                <div class="row">
                                    <div class="col-md-3">
                                        <label class="control-label ">
                                            ERP ID :
                                        </label>
                                        <asp:TextBox ID="txtERPID" runat="server" CssClass="form-control form-control-sm" ReadOnly="True" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label ">
                                            Customer Name :
                                                   <asp:RequiredFieldValidator ID="rfvtxtCustomerName" runat="server" ControlToValidate="txtCustomerName" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SU"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label class="control-label ">
                                            Address :
                                        </label>
                                        <asp:TextBox ID="txtCustomerAddress" runat="server" CssClass="form-control form-control-sm" ></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label ">
                                            Region:
                                        </label>
                                        <asp:DropDownList ID="ddlregion" runat="server" CssClass="form-control form-control-sm chzn-select">
                                            <asp:ListItem Selected="True" Value="West">West</asp:ListItem>
                                            <asp:ListItem Value="East">East</asp:ListItem>
                                            <asp:ListItem Value="North">North</asp:ListItem>
                                            <asp:ListItem Value="South">South</asp:ListItem>
                                            <asp:ListItem Value="Other">Other</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    <div class="col-md-3">
                                        <label class="control-label ">
                                            Contact Person :
                                        </label>
                                        <asp:TextBox ID="txtContactPerson_Name" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label ">
                                            Contact No. :
                                        </label>
                                        <asp:TextBox ID="txtContactno" runat="server" TextMode="Number" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                    </div>
                                     <div class="col-md-3">
                                         <label class="control-label ">
                                             Customer Email. :
                                            <asp:RegularExpressionValidator ID="rgxtxtCustomerEmail" runat="server" ControlToValidate="txtCustomerEmail" ValidationGroup="SU"
                                                ErrorMessage="No Valid Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">  
                                            </asp:RegularExpressionValidator>
                                         </label>
                                        <asp:TextBox ID="txtCustomerEmail" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label">
                                            Location :
                                        </label>
                                        <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control form-control-sm" MaxLength="250"></asp:TextBox>
                                    </div>
                                   <%-- <div class="col-md-3">
                                        <label class="control-label ">Status : </label>
                                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control form-control-sm chzn-select">
                                            <asp:ListItem Value="1">Active</asp:ListItem>
                                            <asp:ListItem Value="0">Inactive</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>--%>
                                </div>
                                <div class="row mt-2">
                                    <div class="col-md-3">
                                        <label class="control-label ">
                                            Contact Person II :
                                        </label>
                                        <asp:TextBox ID="txtcontactpersonII" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label ">
                                            Contact Person No.II :
                                        </label>
                                        <asp:TextBox ID="txtContPerII" runat="server" TextMode="Number" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                    </div>
                                     <div class="col-md-3">
                                        <label class="control-label ">
                                            Contact Person  Email. II :
                                                <asp:RegularExpressionValidator ID="rgxtxtContPersEmailI" runat="server" ControlToValidate="txtContPersEmailI" ValidationGroup="SU"
                                                ErrorMessage="No Valid Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">  
                                            </asp:RegularExpressionValidator>
                                        </label>
                                        <asp:TextBox ID="txtContPersEmailI" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                          <label class="control-label">
                                            Account Manager :
                                               <asp:RequiredFieldValidator ID="RequiredtxtAccount_Manager" runat="server" ControlToValidate="txtAccount_Manager" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SU"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox ID="txtAccount_Manager" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                       

                                    </div>
                                   
                                  
                                </div>
                                <div class="row mt-2">
                                   
                                    
                                    <div class="col-md-3">
                                        <label class="control-label">
                                            Account Manager Email :
                                            <asp:RequiredFieldValidator ID="rgxtxtAccountManager_Email" runat="server" ControlToValidate="txtAccountManager_Email" ErrorMessage="*" ForeColor="Red" ValidationGroup="SU"></asp:RequiredFieldValidator>
                                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAccountManager_Email" ValidationGroup="SU"
                                                ErrorMessage="No Valid Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">  
                                            </asp:RegularExpressionValidator>
                                        </label>
                                        <asp:TextBox ID="txtAccountManager_Email" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                        

                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label">
                                            Search Org :
                                        </label>
                                        <div class="input-group">


                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                             <div class="input-group-append">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn ml-2 mt-0 btn-sm btn-success warning_3" />

</div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" Font-Size="Medium" Width="20%" ValidationGroup="SU" CssClass="btn btn-sm c" />
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" Font-Size="Medium" Width="20%" ValidationGroup="SUs" CssClass="btn btn-sm c" />
  <asp:Label ID="lblsuccess" runat="server" Text=""></asp:Label>

                                    </div>
                                </div>
                                
                            </div>

                        </div>
                    </div>
                </div>
            </div>
          
    <div class="row">
    <div class="col-md-12 ">
        <div class="card">
            <div class="card-body">
                <div class="form-group">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label><br />
    <div style="overflow-x:scroll">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowFooter="true" DataKeyNames="ID" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging"
                        OnRowCommand="GridView1_RowCommand" CssClass="table table-bordered" ShowHeaderWhenEmpty="true">
                        <HeaderStyle BackColor="Green" ForeColor="Yellow" />
                        <RowStyle BackColor="LightGreen" />
                        <FooterStyle BackColor="Green" ForeColor="Yellow" />
                      <Columns>
                            <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="20">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="OrgId" HeaderText="OrgId" NullDisplayText="0" />
                            <asp:BoundField DataField="OrgName" HeaderText="Organization Name" NullDisplayText="0" />
                         
                            <asp:BoundField DataField="OrgAddress" HeaderText="Organization Address" NullDisplayText="0" />
                                <asp:BoundField DataField="OrgLocation" HeaderText="Organization Location" NullDisplayText="0" />
                            <asp:BoundField DataField="ContactPerson" HeaderText="Contact PersonI" NullDisplayText="0" />

                                <asp:BoundField DataField="ContactNo" HeaderText="Contact No I" NullDisplayText="0" />
                            <asp:BoundField DataField="ContactPrsnEmail" HeaderText="Contact Person Email" NullDisplayText="0" />
                               <asp:BoundField DataField="ContactPersonII" HeaderText="Contact Person Name II" NullDisplayText="0" />
                            <asp:BoundField DataField="ContactPersNoII" HeaderText="ContactPerson No II" NullDisplayText="0" />
                            <asp:BoundField DataField="ContactPerEmailII" HeaderText="Contact Person Email II" NullDisplayText="0" />
                               <asp:BoundField DataField="AccntManager" HeaderText="Account Manager" NullDisplayText="0" />
                            <asp:BoundField DataField="AccManagerEmail" HeaderText="Account Manager Email" NullDisplayText="0" />
<asp:BoundField DataField="Region" HeaderText="Region" NullDisplayText="0" />
                            <asp:ButtonField ButtonType="Image" CommandName="Select" HeaderText="Edit" ImageUrl="~/images/edit23.png" ItemStyle-Width="20px" />
                              <asp:ButtonField HeaderText="Delete" ButtonType="Image" ImageUrl="~/Images/New folder/delnew.png" CommandName="DeleteEx" ItemStyle-Width="20px" ItemStyle-Height="5px" />
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
        <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    <link rel="stylesheet" href="../Scripts/chosen.css" />
</asp:Content>

