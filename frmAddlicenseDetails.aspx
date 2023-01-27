<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmAddlicenseDetails.aspx.cs" Inherits="frmAddlicenseDetails" %>

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
                            Add License         
                        </div>
                        <div class="card-body">

                            <div class="form-group">
                                <asp:Label ID="lblerrorMsg" runat="server" Text="" CssClass="label" ForeColor="#ff0000"></asp:Label>
                                <div class="row">
                                    <div class="col-md-3">
                                        <label class="control-label ">
                                         PO No :
                                        </label>
                                        <asp:TextBox ID="txtPono" runat="server" CssClass="form-control form-control-sm " MaxLength="50">
                                     
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label ">
                                            Licenses :
                                             <asp:RequiredFieldValidator ID="rfvtxtLicense" runat="server" ControlToValidate="txtLicense" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SU"></asp:RequiredFieldValidator>
                                                   </label>
                                        <asp:TextBox ID="txtLicense" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label ">
                                            VSA Details:
                                                   <asp:RequiredFieldValidator ID="rfvtxtVSACount" runat="server" ControlToValidate="txtVSACount" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SU"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox ID="txtVSACount" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label ">
                                            Service Desk Details :
                                             <asp:RequiredFieldValidator ID="rfvttxtSdcount" runat="server" ControlToValidate="txtSdcount" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SU"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox ID="txtSdcount" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                    </div>
                                    
                                </div>
                                <div class="row mt-2">
                                    <div class="col-md-3">
                                        <label class="control-label ">
                                            SD Admin Count :
                                        </label>
                                        <asp:TextBox ID="txtSDadmin" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label ">
                                            SD- Technician :
                                        </label>
                                        <asp:TextBox ID="txtsdtech" runat="server" autocomplete="off" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label">
                                            Start Date :
                                             <asp:RequiredFieldValidator ID="RequiredtxtStart" runat="server" ControlToValidate="txtStart" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SU"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox ID="txtStart" runat="server" CssClass="form-control form-control-sm" autocomplete="off" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3">
                                        <label class="control-label">
                                            Expiry Date :
                                            <asp:RequiredFieldValidator ID="RequiredddlLicense_Type" runat="server" ControlToValidate="txtExpiry" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SU" InitialValue="0"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox ID="txtExpiry" runat="server" CssClass="form-control form-control-sm" autocomplete="off" ClientIDMode="Static" MaxLength="10"></asp:TextBox>
                                    </div>
                                    

                                </div>
                                <div class="row mt-2">
                                       <div class="col-md-3">
                                                <label class="control-label ">
                                                    Product :
                                        <asp:RequiredFieldValidator ID="rfvddlProductName" runat="server" InitialValue="0" ControlToValidate="ddlProductName" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </label>
                                                <asp:DropDownList ID="ddlProductName" runat="server"  CssClass="form-control form-control-sm chzn-select" ></asp:DropDownList>
                                              
                                            </div>
                                    <div class="col-md-3">
                                        <label class="control-label">
                                            Quote No :
                                            <asp:RequiredFieldValidator ID="rfvtxtQuoteNo" runat="server" ControlToValidate="txtQuoteNo" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SU" InitialValue="0"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox ID="txtQuoteNo" runat="server" CssClass="form-control form-control-sm" autocomplete="off" ClientIDMode="Static" MaxLength="20"></asp:TextBox>
                                    </div>
                                 
                                    <div class="col-md-3">
                                        <label class="control-label">
                                            Invoice No :
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtinvoice" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SU" InitialValue="0"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox ID="txtinvoice" runat="server" CssClass="form-control form-control-sm" autocomplete="off" ClientIDMode="Static" MaxLength="20"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label">
                                            Quantity :
                                            <asp:RequiredFieldValidator ID="rfvtxtquantity" runat="server" ControlToValidate="txtquantity" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SU" InitialValue="0"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox ID="txtquantity" runat="server" CssClass="form-control form-control-sm" autocomplete="off" ClientIDMode="Static" MaxLength="10"></asp:TextBox>
                                    </div>
                                    
                                    
                                </div>
                                <div class="row mt-2">
                                    <div class="col-md-3  ">
                                        <label class="control-label ">
                                            Upload PO Doc : 
                                       <asp:Label ID="Label21" runat="server"></asp:Label>
                                        </label>
                                        <div class="input-group mb-3">
                                            <div class="custom-file">
                                                <asp:FileUpload ID="FileUploadPO" runat="server" CssClass="form-control form-control-sm" ToolTip="Select Only Pdf,Excel File" />
                                           
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3  ">
                                        <label class="control-label ">
                                            Invoice :
                                            <asp:Label ID="lblinvoiceupload" runat="server" Text=""></asp:Label>
                                        </label>
                                        <div class="input-group mb-3">
                                            <div class="custom-file">
                                                <asp:FileUpload ID="FileUploadInvoice" runat="server" CssClass="form-control form-control-sm" ToolTip="Select Only Pdf,Excel File" />
                                             
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3  ">
                                        <label class="control-label ">
                                            Quote : 
                                       <asp:Label ID="Label2" runat="server"></asp:Label>
                                        </label>
                                        <div class="input-group mb-3">
                                            <div class="custom-file">
                                                <asp:FileUpload ID="FileUploadQuote" runat="server" CssClass="form-control form-control-sm" ToolTip="Select Only Pdf,Excel File" />
                                                
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label">
                                            Amount :
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAmount" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SU" InitialValue="0"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control form-control-sm" autocomplete="off" Text="0" ClientIDMode="Static" MaxLength="10"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label class="control-label">
                                            Remarks :
                                            <asp:RequiredFieldValidator ID="rfvtxtRemarks" runat="server" ControlToValidate="txtRemarks" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SU" InitialValue="0"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control form-control-sm" autocomplete="off" ClientIDMode="Static" ></asp:TextBox>
                                    </div>
                                    <%--<div class="col-md-3 ">
   <label class="custom-file-label" for="inputGroupFile01">Choose file</label>
     <label class="custom-file-label" for="inputGroupFile01">Choose file</label>
                                        <label class="control-label">
                                            PO :
                                             <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                        </label>

                                        <div class="input-group">
                                            <asp:FileUpload ID="FileUploadPO" runat="server" CssClass="form-control form-control-sm" ToolTip="Select Only Pdf,Excel File" />
                                          <%--  <span class="input-group-btn" hidden>
                                                <asp:ImageButton ID="ImgbtnSearchUser" runat="server" class="btn btn-success" ImageUrl="~/images/arrow_up.png" ValidationGroup="Search" OnClick="ImgbtnSearchUser_Click" />
                                            </span>--%>
                                    <%-- </div>
                                    </div>--%>
                                    <%--<div class="col-md-3">
                                        <label class="control-label">
                                            Invoice :
                                            <asp:Label ID="lblinvoiceupload" runat="server" Text=""></asp:Label>
                                        </label>
                                         <div class="input-group">
                                            <asp:FileUpload ID="FileUploadInvoice" runat="server" CssClass="form-control form-control-sm" ToolTip="Select Only Pdf,Excel File" />
                                          
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label">
                                            Quote :
                                        </label>
                                        <div class="input-group">
                                            <asp:FileUpload ID="FileUploadQuote" runat="server" CssClass="form-control form-control-sm" ToolTip="Select Only Pdf,Excel File" />
                                        
                                        </div>
                                    </div>--%>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" Visible="false" ValidationGroup="SU" CssClass="btn btn-sm c" />
                                         <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"  CssClass="btn btn-sm c" />
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" Visible="false" ValidationGroup="SU" CssClass="btn btn-sm c" />


                                    </div>
                                  
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
     
    <div class="row">
    <div class="col-md-12 "  >
        <div class="card">
            <div class="card-body">
                <div class="form-group">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label><br />
                    <div style="overflow: scroll">
                        <asp:GridView ID="gvFiles" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
                            OnRowCommand="GridView1_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="PONo" HeaderText="PONo" />
                                 <asp:BoundField DataField="License" HeaderText="License" />
                                <asp:BoundField DataField="ProductType" HeaderText="ProductType" />
                                <asp:BoundField DataField="VSA" HeaderText="VSA" />
                                <asp:BoundField DataField="SD" HeaderText="SD" />
                                <asp:BoundField DataField="SD_Admin" HeaderText="SD_Admin" />
                                <asp:BoundField DataField="SD_Tech" HeaderText="SD_Tech" />

                                <asp:BoundField DataField="Start" HeaderText="Start" />
                                <asp:BoundField DataField="Expiry_date" HeaderText="Expiry_date" />
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />

                                <asp:BoundField DataField="InvoiceNo" HeaderText="InvoiceNo" />
                                <asp:BoundField DataField="QuoteNo" HeaderText="QuoteNo" />
                                <asp:TemplateField HeaderText="PO File Download">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDownloadPO" runat="server" CausesValidation="False" CommandArgument='<%# Eval("PoDocLoctn") %>'
                                            CommandName="Download" Text='<%# Eval("PoDocLoctn") %>' ForeColor="Blue" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoice File Download">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDownloadInvoice" runat="server" CausesValidation="False" CommandArgument='<%# Eval("InvoiceDocLoctn") %>'
                                            CommandName="Download" Text='<%# Eval("InvoiceDocLoctn") %>' ForeColor="Blue" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quote File Download">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDownloadQuote" runat="server" CausesValidation="False" CommandArgument='<%# Eval("QuoteDocLoctn") %>'
                                            CommandName="Download" Text='<%# Eval("QuoteDocLoctn") %>' ForeColor="Blue" />
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
    <asp:GridView ID="GridView3" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
    RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
    AutoGenerateColumns="false">
    <Columns>
        <asp:BoundField DataField="Name" HeaderText="File Name"/>
        <asp:TemplateField ItemStyle-HorizontalAlign = "Center">
            <ItemTemplate>
                <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="lnkDownload_Click"
                    CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
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

