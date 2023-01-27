<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmCustomerReportAdmin.aspx.cs" Inherits="frmCustomerReportAdmin" EnableEventValidation="false" %>

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
            font-weight:400;
              letter-spacing:0px;
            opacity:1;
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
    <%--<script>
        function getxtValue(that) {
            document.getElementById("lable").innerHTML = that.value;
        }
    </script>--%>
    <script>
        function pageLoad() {
            jQuery(".chzn-select").data("placeholder", "Select Frameworks...").chosen();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <%--   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
        <ContentTemplate>--%>
            <div class="col-md-12 graphs">
                <div class="card">
                    <div class="card-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-3">
                                    <label class="control-label ">Select Column : </label>
                                    <asp:DropDownList ID="ddlSearchItems" runat="server" CssClass="form-control form-control-sm chzn-select" >
                                        <asp:ListItem Value="Customer_Name">Customer Name</asp:ListItem>
                                           <asp:ListItem Value="ContactPerson">Customer Contact Person Name</asp:ListItem>
                                              <asp:ListItem Value="OrgLocation">Location</asp:ListItem>
                                        <asp:ListItem Value="Region">Region</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label class="control-label ">
                                        Enter Search Value : 
                            <asp:RequiredFieldValidator ID="RequiredtxtSearch" runat="server" ControlToValidate="txtSearch" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Search"></asp:RequiredFieldValidator>
                                    </label>
                                    <div class="input-group" style="margin-top:0px">
                                        <asp:TextBox ID="txtSearch" runat="server" class="form-control form-control-sm input-search" placeholder="Search..."></asp:TextBox>
                                        <span class="input-group-append" >
                                            <asp:ImageButton ID="btnSearch" runat="server" class="btn btn-sm c" style="margin-top:0px" ImageUrl="~/Images/search_iconnew.png" OnClick="btnSearch_Click" ValidationGroup="Search" />
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <label class="control-label ">
                                        <asp:Label ID="lblerrorMsg" runat="server" Text=""></asp:Label></label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row zoomd">
                <div class="col-md-12 graphs">
                    <div class="xs">
                        <div class="well1 white">
                            <div class="card card-default">
                                <%--   <div class="card-header">
                             All Customer Details         
                        <label class="control-label pull-right ">
                            Total Assets : 
                            <asp:Label ID="lblTotalCount" runat="server" Text="0"></asp:Label>
                        </label>
                                <asp:ImageButton ID="ImageBtnExport" runat="server" ImageUrl="~/images/excel.png" CssClass="float-right" Height="24" Width="24" OnClick="ImageBtnExport_Click" />
                            </div>--%>
                                <div class="card-body">
                                    <div class="row ">
                                        <div class="col-md-4">

                                            <asp:Label ID="lblsofname" runat="server" Text=" All Customer Details " Font-Size="Larger" ForeColor="Black"></asp:Label>

                                        </div>
                                        <div class="col-md-6">
                                        </div>
                                        <div class="col-md-2 ">
                                            <div class="btn btn-sm elevation-1 ml-4 " style="padding: 0px; margin-bottom: 10px; padding-top: 1px">
                                                <label class="mr-3  mb-0">Export</label>
                                                <asp:ImageButton ID="ImageBtnExport" runat="server" ImageUrl="~/Images/New folder/excelnew.png" CssClass="fa-pull-right btn-outline-success" OnClick="ImageBtnExport_Click" />
                                            </div>
                                       <%-- </div>
                                        <div class="col-md-1">--%>
                                            <label class="control-label  ml-2 ">
                                                Total  : 
                            <asp:Label ID="lblTotalCount" runat="server" Text="0"></asp:Label>
                                        </div>
                                    </div>
                                    <div style="overflow-x: scroll">
                                        <asp:GridView ID="gvAllAssets" runat="server" CssClass="table table-bordered" AutoGenerateColumns="true"
                                            AllowPaging="true" OnPageIndexChanging="gvAllAssets_PageIndexChanging" PageSize="5">
                                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                                            <%--   <Columns>
                                    <asp:BoundField DataField="Customer_Name" HeaderText="Customer Name" />
                                    <asp:BoundField DataField="ContactPerson_Name" HeaderText=" Customer ContactPerson Name" />
                                    <asp:BoundField DataField="Number" HeaderText="Number" />
                                    <asp:BoundField DataField="CustomerAddress" HeaderText="Customer Address" />
                                    <asp:BoundField DataField="Location" HeaderText="Location" />
                                    <asp:BoundField DataField="Cloud_OnPremesis" HeaderText="Hosting" />
                                    <asp:BoundField DataField="License_Type" HeaderText="License Type " />
                                    <asp:BoundField DataField="Product" HeaderText="Product" />
                                    <asp:BoundField DataField="Qty" HeaderText="Quantity" />
                                    <asp:BoundField DataField="Our_Buying_Prices_for_sales" HeaderText="Our_Buying_Prices_for_sales" />
                                    <asp:BoundField DataField="Unit_Prices" HeaderText="Unit_Prices" />
                                    <asp:BoundField DataField="Total_Price" HeaderText="Total_Price" />
                                    <asp:BoundField DataField="Start" HeaderText="Start" DataFormatString="{0:yyyy-MM-dd}"/>
                                    <asp:BoundField DataField="Expiry" HeaderText="Expiry" DataFormatString="{0:yyyy-MM-dd}" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                    <asp:BoundField DataField="Account_Manager" HeaderText="Account_Manager" />
                                    <asp:BoundField DataField="IsActive" HeaderText="IsActive" />
                                    <asp:BoundField DataField="Region" HeaderText="Region" />
                                   
                                    
                                </Columns>--%>
                                             <RowStyle BackColor="White" BorderColor="#e3e4e6" CssClass="text-center" BorderWidth="1px" Height="20px" Font-Size="Small" />
                                        <FooterStyle BackColor="#EDEDED" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#EDEDED" ForeColor="#000000" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="White" Font-Bold="True" Height="20px" ForeColor="#000000" />
                                        <HeaderStyle  CssClass="text-center c" ForeColor="White" Height="20px" Font-Size="Small" />
                                        <EditRowStyle BackColor="#e9edf2" BorderColor="#e3e4e6" BorderStyle="Solid" Height="20px" BorderWidth="1px" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#EDEDED" />
                                        <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                                        <AlternatingRowStyle BackColor="White" BorderColor="#e3e4e6" Font-Size="Small" Height="20px" BorderStyle="Solid" BorderWidth="1px" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        <%--</ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ImageBtnExport" />
            <asp:PostBackTrigger ControlID="btnSearch" />
              <asp:AsyncPostBackTrigger ControlID="gvAllAssets" EventName="PageIndexChanging" />
        </Triggers>
    </asp:UpdatePanel>--%>
    <script type="text/javascript" src="../Script/Scroll/1.8.2.jquery.min.js"></script>
    <script type="text/javascript" src="../Script/Scroll/1.9.1.jquery-ui.min.js"></script>
    <script type="text/javascript" src="../Script/Scroll/gridviewScroll.min.js"></script>
        <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    <link rel="stylesheet" href="../Scripts/chosen.css" />
   <%-- <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });
        function gridviewScroll() {
            $('#<%=gvAllAssets.ClientID%>').gridviewScroll({
                width: "99.9%",
                height: 400,
                // freezesize: 2, // Freeze Number of Columns.
                headerrowcount: 1, //Freeze Number of Rows with Header.
                arrowsize: 30,
                varrowtopimg: "../images/arrowvt.png",
                varrowbottomimg: "../images/arrowvb.png",
                harrowleftimg: "../images/arrowhl.png",
                freezeFooter: true,
                harrowrightimg: "../images/arrowhr.png",
                freezeFooter: true,
                freezeFooterCssClass: "GridViewScrollFooterFreeze",
            });
        }
    </script>--%>
     
</asp:Content>

