<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmCustomers.aspx.cs" Inherits="frmCustomers" %>

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
            background: transparent linear-gradient(180deg, #5D7FA7 0%, #2E4E74 100%) 0% 0% no-repeat padding-box;
            border: none;
        }

        .chart_label {
            font-size: larger;
            font-weight: 500;
            letter-spacing: 0px;
            opacity: 1;
            text-align: left;
        }
        .zoomd
            {

                zoom:90%
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
    <script type="text/javascript">
        function multiplyBy() {
            num1 = document.getElementById("<%=txtAnnualBuyPrice.ClientID%>").value;
            num2 = document.getElementById("<%=txtContractPeriod.ClientID%>").value;
            num3 = document.getElementById("<%=txtTaxamount.ClientID%>").value;
            num4 = parseFloat(num1 * num2) + parseFloat(num3);
            document.getElementById("<%=txtcontractAmt.ClientID%>").value = num4;
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="zoomd">
                <div class="col-md-12 graphs">
                    <div class="xs">
                        <div class="well1 white">
                            <div class="Compose-Message">
                                <div class="card card-default">
                                    <div class="card-header c">
                                        Add Order         
                                <asp:Label ID="lblID" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lblerrorMsg" runat="server" Text="" CssClass="label" ForeColor="#ff0000"></asp:Label>
                                    </div>
                                    <div class="card-body">

                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label class="control-label ">
                                                        Organisation :
                                        <asp:RequiredFieldValidator ID="RfvOrganisation" runat="server" InitialValue="0" ControlToValidate="ddlOrg" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SaveForm"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:DropDownList ID="ddlOrg" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlOrg_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label ">
                                                        Branch :
                            <%--            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" ControlToValidate="txtLocation" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SaveForm"></asp:RequiredFieldValidator>--%>
                                                    </label>
                                                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control form-control-sm chzn-select">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label ">
                                                        <asp:Label ID="Label4" runat="server" Text="Address :"></asp:Label>
                                                    </label>
                                                    <asp:TextBox ID="txtCustomerAddress" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label ">
                                                        Region :
                                        <asp:RequiredFieldValidator ID="rfvddlRegion" runat="server" InitialValue="0" ControlToValidate="ddlRegion" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SaveForm"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control form-control-sm chzn-select">
                                                        <asp:ListItem Text="North" Value="North"></asp:ListItem>
                                                        <asp:ListItem Text="South" Value="South"></asp:ListItem>

                                                        <asp:ListItem Text="West" Value="West"></asp:ListItem>
                                                        <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3" hidden>
                                                    <label class="control-label">
                                                        <asp:Label ID="Label12" runat="server" Text="Location :"></asp:Label>
                                                    </label>
                                                    <asp:TextBox ID="txtLocation1" runat="server" CssClass="form-control form-control-sm" MaxLength="250"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row mt-2">
                                                <div class="col-md-3">
                                                    <label class="control-label ">
                                                        <asp:Label ID="Label10" runat="server" Text="Contact Person :"></asp:Label>
                                                    </label>
                                                    <asp:TextBox ID="txtContactPerson_Name" runat="server" CssClass="form-control form-control-sm" ReadOnly="true" MaxLength="50"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label ">
                                                        <asp:Label ID="Label3" runat="server" Text="Number :"></asp:Label>

                                                    </label>
                                                    <asp:TextBox ID="txtNumber" runat="server" CssClass="form-control form-control-sm" ReadOnly="true" MaxLength="50"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label ">
                                                        PO No.
                                                    </label>
                                                    <asp:RequiredFieldValidator ID="rfvtxtpono" runat="server" ControlToValidate="txtpono" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SU"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtpono" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label ">
                                                        Contract Type :
                                    
                                                    </label>
                                                    <asp:DropDownList ID="ddlContractType" runat="server" CssClass="form-control form-control-sm chzn-select">
                                                        <%--  <asp:ListItem Value="PO"></asp:ListItem>
                                                    <asp:ListItem Value="FMS">FMS</asp:ListItem>
                                                    <asp:ListItem Value="AMC">AMC</asp:ListItem>
                                                    <asp:ListItem Value="NewSale">New Sale</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="row mt-2">
                                                <div class="col-md-3">
                                                    <label class="control-label ">
                                                        Payment Term :
                                        <asp:RequiredFieldValidator ID="Rfv" runat="server" InitialValue="0" ControlToValidate="ddlPaymentTerm" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:DropDownList ID="ddlPaymentTerm" runat="server" CssClass="form-control form-control-sm chzn-select">
                                                        <asp:ListItem Text="Na" Value="NA"> </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-md-3">
                                                    <label class="control-label">
                                                        <asp:Label ID="Label5" runat="server" Text="Server Location"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredtddlCloud_OnPremesis" runat="server" ControlToValidate="ddlCloud_OnPremesis" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SU" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:DropDownList ID="ddlCloud_OnPremesis" runat="server" CssClass="form-control form-control-sm chzn-select">
                                                        <%--  <asp:ListItem Value="0">----------Select One-----------</asp:ListItem>
                                                    <asp:ListItem Value="OurCloud">Our Cloud</asp:ListItem>
                                                    <asp:ListItem Value="OnPremises">On Premises</asp:ListItem>
                                                      <asp:ListItem Value="Sharedcloud">Sharedcloud</asp:ListItem>
                                                    <asp:ListItem Value="AWS">AWS</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-md-3">
                                                    <label class="control-label">
                                                        License Type :
                                           
                                                    </label>
                                                    <asp:DropDownList ID="ddlLicense_Type" runat="server" CssClass="form-control form-control-sm chzn-select">
                                                        <asp:ListItem Value="0">----------Select One-----------</asp:ListItem>
                                                        <asp:ListItem Value="Subscription">Subscription</asp:ListItem>
                                                        <asp:ListItem Value="Perpetual">Perpetual</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3" hidden>
                                                    <label class="control-label">
                                                        License Key :
                                           <%-- <asp:RequiredFieldValidator ID="rfvtxtLicenceKey" runat="server" ControlToValidate="txtLicenceKey" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SU"></asp:RequiredFieldValidator>--%>
                                                    </label>
                                                    <asp:TextBox ID="txtLicenceKey" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row mt-2">

                                                <div class="col-md-3">
                                                    <label class="control-label">
                                                        Start Date :
                                             <asp:RequiredFieldValidator ID="RequiredtxtStart" runat="server" ControlToValidate="txtStart" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SU"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:TextBox ID="txtStart" runat="server" CssClass="form-control form-control-sm" MaxLength="10" autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                                                </div>

                                                <div class="col-md-3">
                                                    <label class="control-label">
                                                        Expiry Date :
                                            <asp:RequiredFieldValidator ID="RequiredtxtExpiry" runat="server" ControlToValidate="txtExpiry" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SU"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:TextBox ID="txtExpiry" runat="server" CssClass="form-control form-control-sm" autocomplete="off" ClientIDMode="Static" MaxLength="10"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label">
                                                        Account Manager :
                                            <asp:RequiredFieldValidator ID="RequiredtxtAccount_Manager" runat="server" ControlToValidate="txtAccount_Manager" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SU"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:TextBox ID="txtAccount_Manager" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label">
                                                        Account Manager Email :
                                            <asp:RequiredFieldValidator ID="RfvtxtAccountManagerEmail" runat="server" ControlToValidate="txtAccountManagerEmail" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SU"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:TextBox ID="txtAccountManagerEmail" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                                </div>

                                            </div>
                                            <%--                                        <div class="row">
                                          
                                            <div class="col-md-9">
                                                <label class="control-label">
                                                    <asp:Label ID="Label19" runat="server" Text="Remarks :"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredtxtExpiry" runat="server" ControlToValidate="txtExpiry" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SU"></asp:RequiredFieldValidator>
                                                </label>
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control form-control-sm" MaxLength="250"></asp:TextBox>
                                            </div>
                                        </div>--%>
                                            <div class="row mt-3">
                                                <div class="col-md-2">
                                                    <label class="control-label ">
                                                        Product :
                                        <asp:RequiredFieldValidator ID="rfvddlProductName" runat="server" InitialValue="0" ControlToValidate="ddlProductName" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:DropDownList ID="ddlProductName" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProductName_SelectedIndexChanged"></asp:DropDownList>

                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">
                                                        Category ALM :
                                                    </label>
                                                    <asp:TextBox ID="txtcategoryALM" runat="server" CssClass="form-control form-control-sm" Text="0"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">
                                                        Unit Price :
                                                    </label>
                                                    <asp:TextBox ID="txtunitprice" runat="server" CssClass="form-control form-control-sm" Text="0" AutoPostBack="True" OnTextChanged="txtunitprice_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">
                                                        <asp:Label ID="Label14" runat="server" Text="Qty :"></asp:Label>
                                                    </label>
                                                    <asp:TextBox ID="txtQty" runat="server" CssClass="form-control form-control-sm" Text="0" AutoPostBack="true" OnTextChanged="txtQty_TextChanged1"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">
                                                        Annual  Price :
                                                    </label>
                                                    <asp:TextBox ID="txtAnnualBuyPrice" runat="server" CssClass="form-control form-control-sm" Text="0" Enabled="False"></asp:TextBox>
                                                </div>


                                                <div class="col-md-2">
                                                    <label class="control-label">
                                                        <asp:Label ID="Label15" runat="server" Text="Contract Period(in Months) :"></asp:Label>
                                                    </label>
                                                    <asp:TextBox ID="txtContractPeriod" runat="server" CssClass="form-control form-control-sm" Text="1" AutoPostBack="true" OnTextChanged="txtContractPeriod_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row mt-2">
                                                <div class="col-md-2">
                                                    <label class="control-label">
                                                        Tax %  :
                                                    </label>
                                                    <asp:TextBox ID="txttaxper" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnTextChanged="txttaxper_TextChanged">0</asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label">
                                                        Tax Amount  :
                                                    </label>
                                                    <asp:TextBox ID="txtTaxamount" runat="server" CssClass="form-control form-control-sm" Enabled="False">0</asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label">
                                                        Total Contract Price  :
                                                    </label>
                                                    <asp:TextBox ID="txtcontractAmt" runat="server" CssClass="form-control form-control-sm" Text="0" Enabled="False"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label">
                                                        Technician  :
                                                    </label>
                                                    <asp:TextBox ID="txtTechnician" runat="server" CssClass="form-control form-control-sm" MaxLength="9" Text="0"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label">
                                                        <asp:Label ID="Label16" runat="server" Text="Monthly Revenue :"></asp:Label>
                                                    </label>
                                                    <asp:TextBox ID="txtmonthlyRev" runat="server" CssClass="form-control form-control-sm" Text="0" Enabled="False"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4 mt-3">
                                                    <label class="control-label">
                                                        <asp:Label ID="Label1" runat="server" Text="Remarks :"></asp:Label>
                                                    </label>
                                                    <asp:TextBox ID="txtremarks" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">


                                            <div class="col-md-3">
                                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" Font-Size="Medium" Width="20%" ValidationGroup="SU" CssClass="btn btn-sm c" />
                                                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" ValidationGroup="SU" Visible="false" CssClass="btn btn-sm c" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row mt-1">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="form-group">


                                <div class="col-md-1">
                                    <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>

                                    <label class="control-label float-right font-label">
                                        Total : 
                                <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>
                                    </label>

                                </div>
                            </div>
                            <div style="overflow-x: scroll; width: 100%">
                                <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ShowHeader="true" AllowPaging="true"
                                    EmptyDataText="No Records Found" OnRowCommand="gvUsers_RowCommand" DataKeyNames="ID" OnPageIndexChanging="gvUsers_PageIndexChanging" PageSize="100">
                                    <Columns>
       
                                        <asp:BoundField HeaderText="Customer ID" DataField="Customer_Id" />
                                        <asp:BoundField HeaderText="Customer Name" DataField="Customer_Name" />
                                        <asp:BoundField HeaderText="Cloud OnPremesis" DataField="Cloud_OnPremesis" />
                                        <asp:BoundField HeaderText="License Type" DataField="License_Type" />
                                          <asp:BoundField HeaderText="Contract Type" DataField="Contract_Type" />
                                        <asp:BoundField HeaderText="Product" DataField="Product" />
                                        <asp:BoundField HeaderText="Technician" DataField="Technician" />
                                            <asp:BoundField HeaderText="Qty" DataField="Qty" />
                                        <asp:BoundField HeaderText="Unit Prices" DataField="Unit_Prices" />
                                        <asp:BoundField HeaderText="Annual Buy Price" DataField="Annual_Buy_Price" />

                                        <asp:BoundField HeaderText="Contract Amount" DataField="Contract_Amount" />
                                        <asp:BoundField HeaderText="Monthly Revenue" DataField="Monthly_Revenue" />
                                         <asp:BoundField HeaderText="TaxAmount" DataField="TaxAmount" />
                                        <asp:BoundField HeaderText="Start" DataField="Start" />
                                        <asp:BoundField HeaderText="Expiry" DataField="Expiry" />
                                        <asp:BoundField HeaderText="Contract years" DataField="Contract_years" />

                                        <asp:BoundField HeaderText="Account Manager" DataField="Account_Manager" />
                                        <asp:BoundField HeaderText="Account Manger Email" DataField="AccountMangerEmail" />
                                        <asp:BoundField HeaderText="Region" DataField="Region" />
                                         <asp:BoundField HeaderText="Payment Term" DataField="Payment_Term" />
                                        <asp:BoundField HeaderText="PO_No" DataField="PO_No" />
                                        <asp:BoundField HeaderText="Category Alm" DataField="Category_Alm" />
                                        <asp:BoundField HeaderText="Remarks" DataField="Remarks" />
                                        <asp:TemplateField>
                                            <HeaderTemplate></HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" CommandName="SelectUser" CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/images/edit23.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <RowStyle BackColor="White" BorderColor="#e3e4e6" BorderWidth="1px" CssClass="font-label" Height="20px" HorizontalAlign="Center" Font-Size="Small" />
                                    <FooterStyle BackColor="#EDEDED" Font-Bold="True" CssClass="font-label" ForeColor="White" />
                                    <PagerStyle BackColor="#EDEDED" ForeColor="#000000" CssClass="font-label" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#688df5" Font-Bold="True" CssClass="font-label" HorizontalAlign="Center" Height="20px" Font-Size="Small" ForeColor="#000000" />
                                    <HeaderStyle BackColor=" #698DF2 " CssClass="font-label" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" Height="20px" Font-Size="Small" />
                                    <EditRowStyle BackColor=" #698DF2" BorderColor="#e3e4e6" CssClass="font-label" HorizontalAlign="Center" BorderStyle="Solid" Font-Size="Small" Height="20px" BorderWidth="1px" />
                                    <AlternatingRowStyle BackColor="White" BorderColor="#e3e4e6" CssClass="font-label" HorizontalAlign="Center" Font-Size="Small" Height="20px" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlOrg" />
            <asp:PostBackTrigger ControlID="ddlProductName" />
            <asp:PostBackTrigger ControlID="txtunitprice" />
            <asp:PostBackTrigger ControlID="txttaxper" />
            <asp:PostBackTrigger ControlID="txtQty" />
            <asp:PostBackTrigger ControlID="txtContractPeriod" />
             <asp:PostBackTrigger ControlID="gvUsers"  />
            <%--  <asp:AsyncPostBackTrigger ControlID ="txtContractPeriod" EventName ="TextChanged" />
             <asp:AsyncPostBackTrigger ControlID ="txtQty" EventName ="TextChanged" />--%>
        </Triggers>
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
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    <link rel="stylesheet" href="../Scripts/chosen.css" />
</asp:Content>

