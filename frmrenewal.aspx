<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmrenewal.aspx.cs" Inherits="frmrenewal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="js/GScripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="js/GScripts/ScrollableGridViewPlugin_ASP.NetAJAXmin.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=gvUsers.ClientID %>').Scrollable({
                ScrollHeight: 500,
                IsInUpdatePanel: true
            });
        });
    </script>

    <link rel="stylesheet" href="../css/bootstrap.min.css" type="text/css" />
    <script src="../js/jquery.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
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
    <style type="text/css">
        .messagealert {
            width: 100%;
            position: fixed;
            top: 0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }
    </style>
    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="www.google.com" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');
        }
    </script>

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
            <div class="col-md-12 graphs"hidden>
                <div class="xs">
                    <div class="well1 white">
                        <div class="Compose-Message">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Renewal        
                                </div>
                                <div class="panel-body">

                                    <div class="form-group">
                                        <asp:Label ID="lblerrorMsg" runat="server" Text="" CssClass="label" ForeColor="#ff0000"></asp:Label>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <label class="control-label ">
                                                    Customer Name :
                                                   <asp:RequiredFieldValidator ID="rfvtxtCustomerName" runat="server" ControlToValidate="txtCustomerName" ErrorMessage="Required" ForeColor="Red" ValidationGroup="ss"></asp:RequiredFieldValidator>
                                                </label>
                                                <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control1" MaxLength="50"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">
                                                    Contact Person Name :
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtContctPrnsName" ErrorMessage="Required" ForeColor="Red" ValidationGroup="ss"></asp:RequiredFieldValidator>
                                                </label>
                                                <asp:TextBox ID="txtContctPrnsName" runat="server" CssClass="form-control1" MaxLength="50"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">
                                                    Number :
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtnumber" ErrorMessage="Required" ForeColor="Red" ValidationGroup="ss"></asp:RequiredFieldValidator>
                                                </label>
                                                <asp:TextBox ID="txtnumber" runat="server" CssClass="form-control1" MaxLength="50"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">
                                                    Address :
                                                </label>
                                                <asp:TextBox ID="txtCustomerAddress" runat="server" CssClass="form-control1" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label">
                                                    Account Manager :
                                                </label>
                                                <asp:TextBox ID="txtAccManager" runat="server" CssClass="form-control1" MaxLength="50"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">Account Manager Email : </label>
                                                <asp:TextBox ID="txtacManagerEmail" runat="server" CssClass="form-control1" MaxLength="50"></asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <label class="control-label ">
                                                    PO No.
                                                </label>
                                                 <asp:RequiredFieldValidator ID="RequiredTextBox1" runat="server" ControlToValidate="txtpono" ErrorMessage="Required" ForeColor="Red" ValidationGroup="ss"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtpono" runat="server" CssClass="form-control1" MaxLength="50"></asp:TextBox>
                                            </div>

                                            <div class="col-md-2">
                                                <label class="control-label ">
                                                    Po. Type :
                                    
                                                </label>
                                                <asp:DropDownList ID="ddlPOType" runat="server" CssClass="form-control1 chzn-select">
                                                    <asp:ListItem Value="PO"></asp:ListItem>
                                                    <asp:ListItem Value="FMS">FMS</asp:ListItem>
                                                    <asp:ListItem Value="AMC">AMC</asp:ListItem>
                                                    <asp:ListItem Value="NewSale">New Sale</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label">
                                                    Payment Term :
                                                </label>
                                                <asp:TextBox ID="txtpaymentterm" runat="server" CssClass="form-control1" MaxLength="50"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label">
                                                    Start Date :
                                             <asp:RequiredFieldValidator ID="RequiredtxtStart" runat="server" ControlToValidate="txtStart" ErrorMessage="Required" ForeColor="Red" ValidationGroup="ss"></asp:RequiredFieldValidator>
                                                </label>
                                                <asp:TextBox ID="txtStart" runat="server" CssClass="form-control1" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label">
                                                    Expiry Date :
                                            <asp:RequiredFieldValidator ID="rqrdtxtExpiry" runat="server" ControlToValidate="txtExpiry" ErrorMessage="Required" ForeColor="Red" ValidationGroup="ss" InitialValue="0"></asp:RequiredFieldValidator>
                                                </label>
                                                <asp:TextBox ID="txtExpiry" runat="server" CssClass="form-control1" ClientIDMode="Static" MaxLength="10"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">
                                                    Region:
                                                </label>
                                                <asp:DropDownList ID="ddlregion" runat="server" CssClass="form-control1 chzn-select">
                                                    <asp:ListItem Selected="True" Value="West">West</asp:ListItem>
                                                    <asp:ListItem Value="East">East</asp:ListItem>
                                                    <asp:ListItem Value="North">North</asp:ListItem>
                                                    <asp:ListItem Value="South">South</asp:ListItem>
                                                    <asp:ListItem Value="Other">Other</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                       
                                    <div class="col-md-2">
                                        <label class="control-label ">
                                           Location:
                                        </label>
                                        <asp:TextBox ID="txtlocation" runat="server" CssClass="form-control1" MaxLength="50"></asp:TextBox>
                                    </div>


                                    <div class="col-md-2">
                                        <label class="control-label ">
                                            Cloud Type:
                                        </label>
                                        <asp:DropDownList ID="ddlCldtype" runat="server" CssClass="form-control1 chzn-select" MaxLength="50">
                                            <asp:ListItem Value="OnPremise">On Premise</asp:ListItem>
                                              <asp:ListItem Value="Cloud">Cloud</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>


                                    <div class="col-md-2">
                                        <label class="control-label">
                                            License Type :
                                        </label>
                                       <asp:DropDownList ID="ddlLicenseType" runat="server" CssClass="form-control1 chzn-select" MaxLength="50">
                                            <asp:ListItem Value="Subscription">Subscription</asp:ListItem>
                                              <asp:ListItem Value="Cloud">Cloud</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label ">Product : </label>
                                        <asp:DropDownList ID="ddlproduct" runat="server" class="form-control1 chzn-select">
                                            <asp:ListItem Value="1">VSA</asp:ListItem>
                                            <asp:ListItem Value="0">SD-Desk</asp:ListItem>
                                            <asp:ListItem Value="0">SD-Tech</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label ">
                                           Quantity:
                                        </label>
                                        <asp:TextBox ID="txtqnty" runat="server" Text="0" CssClass="form-control1" MaxLength="50"></asp:TextBox>
                                    </div>
                                    
                                    <div class="col-md-2">
                                        <label class="control-label ">
                                           Technician:
                                        </label>
                                        <asp:TextBox ID="txtTechnician" runat="server"  Text="0" CssClass="form-control1" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <label class="control-label ">
                                                    OEM Price:
                                                </label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtoemprice" ErrorMessage=" Numbers Only" ForeColor="Red" ValidationExpression="^[1-9]\d*(\.\d+)?$" ValidationGroup="ss"></asp:RegularExpressionValidator>
                                                <asp:TextBox ID="txtoemprice" runat="server" Text="0" CssClass="form-control1" MaxLength="50"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">
                                                    Buying Price For Sale:
                                                </label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtsaleprice" ErrorMessage=" Numbers Only" ForeColor="Red" ValidationExpression="^[1-9]\d*(\.\d+)?$" ValidationGroup="ss"></asp:RegularExpressionValidator>
                                                <asp:TextBox ID="txtsaleprice" runat="server" Text="0" CssClass="form-control1" MaxLength="50"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">
                                                    Unit Price:
                                                </label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtunitprice" ErrorMessage=" Numbers Only" ForeColor="Red" ValidationExpression="^[1-9]\d*(\.\d+)?$" ValidationGroup="ss"></asp:RegularExpressionValidator>
                                                <asp:TextBox ID="txtunitprice" runat="server" Text="0" CssClass="form-control1" MaxLength="50"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label">
                                                    Total Price :
                                                </label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txttotalPrice" ErrorMessage=" Numbers Only" ForeColor="Red" ValidationExpression="^[1-9]\d*(\.\d+)?$" ValidationGroup="ss"></asp:RegularExpressionValidator>
                                                <asp:TextBox ID="txttotalPrice" runat="server" Text="0" CssClass="form-control1" MaxLength="50"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">
                                                    Is Active:
                                                </label>
                              
                                                <asp:DropDownList ID="ddlactive" runat="server" CssClass="form-control1 chzn-select">
                                                    <asp:ListItem Selected="True" Value="Active">Active</asp:ListItem>
                                                    <asp:ListItem Value="Expired">Expired</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-3 ">
                                  
                                                <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn btn-sm btn-success warning_3" OnClick="btnUpdate_Click" ValidationGroup="ss"  />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="col-md-12 graphs">
                <div class="xs">
                    <div class="well1 white">
                        <div class="form-group">
                            <div class="row">

                                <div class="col-md-3">
                                    <label class="control-label ">
                                        Enter Customer Name : 
                                   <asp:RequiredFieldValidator ID="RequiredtxtSearch" runat="server" ControlToValidate="txtSearch" ErrorMessage="Required" ForeColor="Red" ValidationGroup="search"></asp:RequiredFieldValidator>
                                    </label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtSearch" runat="server" class="form-control1 input-search" placeholder="Search..."></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:ImageButton ID="ImgbtnSearchUser" runat="server" class="btn btn-success" ImageUrl="~/images/icons_search.png"  ValidationGroup="search" OnClick="ImgbtnSearchUser_Click" />
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
                            <div style="overflow:scroll">
                            <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ShowHeader="true" AllowPaging="true"
                                EmptyDataText="No Records Found"   OnPageIndexChanging="gvUsers_PageIndexChanging" OnSelectedIndexChanging="gvUsers_SelectedIndexChanging" PageSize="50">
                           <%--     <Columns>
                                         <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgEdit" runat="server" CommandName="SelectUser" ImageUrl="~/Images/Edit.png" ToolTip="Edit" CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                
                                    <asp:BoundField HeaderText="Customer_Name" DataField="Customer_Name" />
                                    <asp:BoundField HeaderText="Number" DataField="Number" />
                                    <asp:BoundField HeaderText="ContactPerson Name" DataField="ContactPerson_Name" />
                                    <asp:BoundField HeaderText="Location" DataField="Location" />
                                    <asp:BoundField HeaderText="CustomerAddress" DataField="CustomerAddress" />

                                    <asp:BoundField HeaderText="Cloud_OnPremesis" DataField="Cloud_OnPremesis" />
                                    <asp:BoundField HeaderText="License_Type" DataField="License_Type" />
                                    <asp:BoundField HeaderText="Product" DataField="Product" />
                          


                                    <asp:BoundField HeaderText="Quantity" DataField="Qty" />
                      
                                    <asp:BoundField HeaderText="Sales Price" DataField="Our_Buying_Prices_for_sales" />
                                    <asp:BoundField HeaderText="Unit_Prices" DataField="Unit_Prices" />
                                    <asp:BoundField HeaderText="Total_Price" DataField="Total_Price" />
                                    <asp:BoundField HeaderText="Start" DataField="Start" />
                                    <asp:BoundField HeaderText="Expiry" DataField="Expiry" />
                               
                                    <asp:BoundField HeaderText="AccountMangerEmail" DataField="AccountMangerEmail" />
                                    <asp:BoundField HeaderText="PO_No" DataField="PO_No" />
                                    <asp:BoundField HeaderText="PO_Type" DataField="PO_Type" />
                                    <asp:BoundField HeaderText="Payment_Term" DataField="Payment_Term" />
                                    <asp:BoundField HeaderText="Account_Manager" DataField="Account_Manager" />
                                    <asp:BoundField HeaderText="Region" DataField="Region" />
                                    <asp:BoundField HeaderText="Status" DataField="IsActive" />
                                     <asp:BoundField HeaderText="ID" DataField="ID" />
                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnGetDetails" runat="server" CommandName="getDetials" CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/images/Details.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>--%>
                                <RowStyle BackColor="#fafafa" BorderColor="#e3e4e6" BorderWidth="1px" />
                                <FooterStyle BackColor="#EDEDED" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#EDEDED" ForeColor="#000000" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E3E4E6" Font-Bold="True" ForeColor="#000000" />
                                <HeaderStyle BackColor="#e3e4e6" Font-Bold="True" ForeColor="#000000" Height="30px" Font-Size="Small" />
                                <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#EDEDED" />
                                <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                                <EditRowStyle BackColor="#EDEDED" BorderColor="#e3e4e6" BorderStyle="Solid" BorderWidth="1px" />
                                <AlternatingRowStyle BackColor="White" BorderColor="#e3e4e6" BorderStyle="Solid" BorderWidth="1px" />
                            </asp:GridView>
</div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
         <link rel="stylesheet" type="text/css" href="../Script/build/jquery.datetimepicker.css" />
    <script src="../Script/build2/jquery.js"></script>
    <script src="../Script/build2/jquery.datetimepicker.full.js"></script>
     <script>
         $.datetimepicker.setLocale('en');
         $('#txtStart').datetimepicker({
             dayOfWeekStart: 1,
             lang: 'en',
             startDate: '+1d'
         });
     </script>
    <script>
        $.datetimepicker.setLocale('en');
        $('#txtExpiry').datetimepicker({
            dayOfWeekStart: 1,
            lang: 'en',
            startDate: '+1d'
        });
    </script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    <link rel="stylesheet" href="../Scripts/chosen.css" />
</asp:Content>

