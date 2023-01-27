<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmdashboardII.aspx.cs" Inherits="frmdashboardII" EnableEventValidation="false" %>

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
            <div class="col-md-12 graphs">
                <div class="card  " >
                 <%--   <div class="card-header c ">
                 <asp:label runat="server" CssClass="chart_label1">       PcVisor Licenses</asp:label>
                    </div>--%>
                    <div class="card-body">
                        <%--<div class="row">
                            <div class="col-md-4 " style="border-right:solid 1px">
                           
                                  <asp:Label ID="lblname" runat="server" Text="Purchased  License :" ForeColor="Black" Font-Size="Large"  CssClass="lblh labelfont mr-5 ml-4"></asp:Label>
                                <asp:Label ID="lblvsapurchased" runat="server" Text="0" Font-Size="Large" ForeColor="Black" CssClass="lblh ml-5 labelfont"></asp:Label>
                               <asp:LinkButton ID="lnkpurchasedlicense" runat="server" Font-Size="Large" OnClick="lnkpurchasedlicense_Click"><i class="fa fa-2x fa-arrow-up" style="font-size:15px"></i></asp:LinkButton>

                            </div>
                            <div class="col-md-4 " style="border-right: solid 1px">
                                 <asp:Label ID="Label1" runat="server" Text="Consumed  License :" ForeColor="Black" Font-Size="Large"  CssClass="lblh labelfont mr-5 ml-4"></asp:Label>
                                 <asp:Label ID="lblvsaconsumed" runat="server" CssClass="lblh  ml-5 labelfont" Font-Size="Large" ForeColor="Black" Text="0"></asp:Label>
                                <asp:LinkButton ID="linkconsumed" runat="server" OnClick="linkconsumed_Click"><i class="fa fa-arrow-circle-up" style="font-size:15px"></i></asp:LinkButton>

                            </div>
                            <div class="col-md-4 ">
                                 <asp:Label ID="Label3" runat="server" Text="Available  License :" ForeColor="Black" Font-Size="Large"  CssClass="lblh labelfont mr-5 ml-4"></asp:Label>
                                <asp:Label ID="lblavailable" runat="server" CssClass="lblh ml-5 labelfont" ForeColor="Black"  Font-Size="Large"   Text="0"></asp:Label>
                                <asp:LinkButton ID="LinkButton1" runat="server"  OnClick="lnlavailable_Click"><i class="fa fa-arrow-circle-up" style="font-size:15px"></i></asp:LinkButton>
                            </div>
                        </div>--%>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="card" style="background: #F2F2F25C 0% 0% no-repeat padding-box">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col">
                                                <div class="card">
                                                    <div class="card-header c">
                                                        <asp:Label runat="server" CssClass="chart_label1">VSA Licenses</asp:Label><img src="Images/New folder/rupee.png" class="fa-pull-right" width="30"></img>
                                                    </div>
                                                    <div class="card-body">
                                                        <asp:Label ID="Label8" runat="server" Text="Total Renewal Amount :" ForeColor="Black" Font-Size="Large" CssClass="lblh labelfont "></asp:Label>

                                                        <asp:LinkButton ID="lnkVSAConumedDetails" runat="server" Text="0" CssClass="lblh" Font-Size="Large"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <div class="card">
                                                    <div class="card-header c">
                                                        <asp:Label runat="server" CssClass="chart_label1">SD Licenses</asp:Label><img src="Images/New folder/rupee.png" class="fa-pull-right" width="30"></img>
                                                    </div>
                                                    <div class="card-body">
                                                        <asp:Label ID="Label1" runat="server" Text="Total Renewal Amount :" ForeColor="Black" Font-Size="Large" CssClass="lblh labelfont "></asp:Label>

                                                        <asp:LinkButton ID="lnkSDLicenseDetails" runat="server" Text="0" CssClass="lblh" Font-Size="Large"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card" style="background: #F2F2F25C 0% 0% no-repeat padding-box">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col">
                                                <div class="card">
                                                    <div class="card-header c">
                                                        <asp:Label runat="server" CssClass="chart_label1">VSA Licenses</asp:Label><img src="Images/New folder/rupee.png" class="fa-pull-right" width="30"></img>
                                                    </div>
                                                    <div class="card-body">
                                                        <asp:Label ID="Label3" runat="server" Text="Total Renewal Amount :" ForeColor="Black" Font-Size="Large" CssClass="lblh labelfont "></asp:Label>

                                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="0" CssClass="lblh" Font-Size="Large"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <div class="card">
                                                    <div class="card-header c">
                                                        <asp:Label runat="server" CssClass="chart_label1">SD Licenses</asp:Label><img src="Images/New folder/rupee.png" class="fa-pull-right" width="30"></img>
                                                    </div>
                                                    <div class="card-body">
                                                        <asp:Label ID="Label4" runat="server" Text="Total Renewal Amount :" ForeColor="Black" Font-Size="Large" CssClass="lblh labelfont "></asp:Label>

                                                        <asp:LinkButton ID="LinkButton2" runat="server" Text="0" CssClass="lblh" Font-Size="Large"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card" style="background: #F2F2F25C 0% 0% no-repeat padding-box">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col">
                                                <div class="card">
                                                    <div class="card-header c">
                                                        <asp:Label runat="server" CssClass="chart_label1">VSA Licenses</asp:Label><img src="Images/New folder/rupee.png" class="fa-pull-right" width="30"></img>
                                                    </div>
                                                    <div class="card-body">
                                                        <asp:Label ID="Label5" runat="server" Text="Total Renewal Amount :" ForeColor="Black" Font-Size="Large" CssClass="lblh labelfont "></asp:Label>

                                                        <asp:LinkButton ID="LinkButton3" runat="server" Text="0" CssClass="lblh" Font-Size="Large"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <div class="card">
                                                    <div class="card-header c">
                                                        <asp:Label runat="server" CssClass="chart_label1">SD Licenses</asp:Label><img src="Images/New folder/rupee.png" class="fa-pull-right" width="30"></img>
                                                    </div>
                                                    <div class="card-body">
                                                        <asp:Label ID="Label6" runat="server" Text="Total Renewal Amount :" ForeColor="Black" Font-Size="Large" CssClass="lblh labelfont "></asp:Label>

                                                        <asp:LinkButton ID="LinkButton4" runat="server" Text="0" CssClass="lblh" Font-Size="Large"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            
            <link rel="stylesheet" href="css/clndr.css" type="text/css" />
            <div class="modal fade" id="basicModal" <%-- tabindex="-1"--%> role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg" style="width: auto; max-width: 98%">
                    <div class="modal-content">
                    
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="col-md-12 graphs" style="padding: 0px">
                                    <div class="xs">
                                        <div class="well1 white" style="padding: 0px">
                                            <div class="card card-default">

                                                <div class="card-header c">
                                                    <asp:Label runat="server" Text="Customer Details" CssClass="fa-pull-left" />
                                                    <button type="button" class="close fa-pull-right btn-outline-danger mb-1" onclick="javascript:window.location.reload()" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                
                                                </div>
                                                <div class="card-body">
                                                    <div class="row ">
                                                        <div class="col-md-4">

                                                            <asp:Label ID="lblsofname" runat="server" Text="" Font-Size="Larger" ForeColor="Black"></asp:Label>

                                                        </div>
                                                        <div class="col-md-5">
                                                        </div>
                                                        <div class="col-md-2 ">
                                                            <div class="btn btn-sm elevation-1 " style="padding: 0px; margin-bottom: 10px; padding-top: 1px">
                                                                <label class="mr-3  mb-0">Export</label>
                                                                <asp:ImageButton ID="ImgbtnExport" runat="server" ImageUrl="~/Images/New folder/excelnew.png" CssClass="fa-pull-right btn-outline-success" OnClick="ImgbtnExport_Click" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:Label ID="lblTotal" runat="server" CssClass="pull-right control-label" Font-Size="Large" Text="0"></asp:Label>
                                                            <asp:Label ID="Label2" runat="server" CssClass="pull-right control-label" Font-Size="Large" Text="Total: "></asp:Label>
                                                        </div>
                                                        <%--      <div class="col-md-1 ml-0">
                                                  <div class="btn btn-sm ml-2 ">
                                                        <label class="mr-3 mb-0">CLOSE</label>
                                                        <button type="button" class="close fa-pull-right btn-outline-danger mb-1" onclick="javascript:window.location.reload()" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                    </div>--%>
                                                    </div>
                                                </div>
                                                <div style="overflow-x: scroll; overflow-y: scroll">
                                                    <asp:GridView GridLines="None" ID="GridDashboardDetails" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered"
                                                        ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" Width="100%">
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
                                
                               
                                <asp:PostBackTrigger ControlID="ImgbtnExport" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <%-- <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>--%>
                    </div>
                </div>
            </div>
            <!-- Placed at the end of the document so the pages load faster -->
            <script type="text/javascript" src="../Script/Scroll/1.8.2.jquery.min.js"></script>
            <script type="text/javascript" src="../Script/Scroll/1.9.1.jquery-ui.min.js"></script>
            <script type="text/javascript" src="../Script/Scroll/gridviewScroll.min.js"></script>
            <script type="text/javascript">
                $(document).ready(function () {
                    gridviewScroll();
                });
                function gridviewScroll() {
                    $('#<%=GridDashboardDetails.ClientID%>').gridviewScroll({
                        width: "99.9%",
                        height: 190,
                        barhovercolor: "#e3e4e6",
                        barcolor: "#e3e4e6"
                    });
                }
            </script>
            <%--   <script type="text/javascript">
                function showProgress() {
                    var updateProgress = $get("<%= UpdateProgress1.ClientID %>");
                    updateProgress.style.display = "block";
                }
            </script>--%>
            <script src="js/jquery.easing.1.3.js"></script>
            <script src="js/bootstrap.min.js"></script>
            <script src="js/jquery.fancybox.pack.js"></script>
            <script src="js/jquery.fancybox-media.js"></script>
            <script src="js/animate.js"></script>
            <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
            <script type="text/javascript">
                $(".chzn-select>select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
            <link rel="stylesheet" href="../Scripts/chosen.css" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <style>
        .modal-header {
            background-color: #337AB7;
            padding: 10px;
            color: #FFF;
            border-bottom: 2px dashed #337AB7;
        }
    </style>

</asp:Content>



