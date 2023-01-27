<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmAlmkeydetails.aspx.cs" Inherits="frmAlmkeydetails" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="../css/modal.css" />
 <%--   <link rel="stylesheet" href="../css/bootstrap.min.css" />--%>
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
        <link rel="stylesheet" href="../../plugins/fontawesome-free/css/all.min.css" />
    <!-- overlayScrollbars -->
    <link rel="stylesheet" href="../../plugins/overlayScrollbars/css/OverlayScrollbars.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="../../Scripts/chosen.css" />
    <link rel="stylesheet" href="../../dist/css/adminlte.min.css"/>
      <style>
            .c {
            color: white;
          /*  //background: #698DF2;*/
          background:transparent linear-gradient(180deg, #5D7FA7 0%, #2E4E74 100%) 0% 0% no-repeat padding-box;
            border: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <label class="control-label ">Select Column : </label>
                                    <asp:DropDownList ID="ddlSearchItems" runat="server" CssClass="form-control form-control-sm chzn-select">
                                        <asp:ListItem Value="CustomerCode">CustomerCode</asp:ListItem>
                                        <asp:ListItem Value="CustomerName">Customer Name</asp:ListItem>
                                        <asp:ListItem Value="Quantity">Quantity</asp:ListItem>


                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label class="control-label ">
                                        Enter Search Value : 
                            <asp:RequiredFieldValidator ID="RequiredtxtSearch" runat="server" ControlToValidate="txtSearch" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Search"></asp:RequiredFieldValidator>
                                    </label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtSearch" runat="server" class="form-control form-control-sm input-search" placeholder="Search..."></asp:TextBox>
                                        <span class="input-group-append" style="margin-top: 0px; top: 0px">
                                            <asp:ImageButton ID="btnSearch" runat="server" class="btn c btn-outline-info" Style="margin-top: 0px; padding: 8px" ImageUrl="~/images/icons_search.png" OnClick="btnSearch_Click" ValidationGroup="Search" />
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
            <div class="row">
                <div class="col-md-12 graphs">
                    <div class="card">
                      <%--  <div class="card-header c">
                            All Key Details  
                                    <asp:ImageButton ID="ImageBtnExport" runat="server" ImageUrl="~/Images/New folder/excelnew.png" CssClass="float-right btn-outline-success ml-3" OnClick="ImageBtnExport_Click" />
                            <label class="control-label pull-right ">
                                Total  : 
                            <asp:Label ID="lblTotalCount" runat="server" Text="0"></asp:Label>
                            </label>

                        </div>--%>
                        <div class="card-body">
                            <div class="row ">
                                <div class="col-md-4">

                                    <asp:Label ID="lblsofname" runat="server" Text="  All Key Details  " Font-Size="Larger" ForeColor="Black"></asp:Label>

                                </div>
                                <div class="col-md-6">
                                </div>
                                <div class="col-md-2 ">
                                    <div class="btn btn-sm elevation-1 ml-5 " style="padding: 0px; margin-bottom: 10px; padding-top: 1px">
                                        <label class="mr-1 ml-1 mb-0">Export</label>
                                        <asp:ImageButton ID="ImageBtnExport" runat="server" ImageUrl="~/Images/New folder/excelnew.png" CssClass="float-right btn-outline-success ml-2" OnClick="ImageBtnExport_Click" />
                                    </div>
                                    <%--  </div>
                                        <div class="col-md-1">--%>
                                    <label class="control-label  ml-4 ">
                                        Total  : 
                                       
                           <asp:Label ID="lblTotalCount" runat="server" CssClass="control-label" Text="0"></asp:Label>
                                         </label>
                                </div>
                            </div>


                            <div style="overflow: scroll">
                                <asp:GridView ID="gvAllAssets" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                                    OnRowCommand="gvAllAssets_RowCommand" AllowPaging="true" PageIndex="10">
                                    <Columns>

                                        <asp:BoundField DataField="CustomerCode" HeaderText="Customer Code" />
                                        <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                        <asp:BoundField DataField="SerialNo" HeaderText="Serial No" />
                                        <asp:BoundField DataField="StartDate" HeaderText="StartDate" DataFormatString="{0:yyyy-MM-dd}" />
                                        <asp:BoundField DataField="SerialNo" HeaderText="Serial No." />
                                        <asp:BoundField DataField="EndDate" HeaderText="EndDate" DataFormatString="{0:yyyy-MM-dd}" />
                                        <asp:BoundField DataField="LicenceKey" HeaderText="LicenceKey" />
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />


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
            
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ImageBtnExport" />
            <asp:PostBackTrigger ControlID="btnSearch" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript" src="../Script/Scroll/1.8.2.jquery.min.js"></script>
    <script type="text/javascript" src="../Script/Scroll/1.9.1.jquery-ui.min.js"></script>
    <script type="text/javascript" src="../Script/Scroll/gridviewScroll.min.js"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    <link rel="stylesheet" href="../Scripts/chosen.css" />
    <script type="text/javascript">
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
    </script>

</asp:Content>

