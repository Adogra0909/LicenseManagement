<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

      <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link rel="stylesheet" href="dist/css/adminlte.min.css" />

    <script type="text/javascript" src='../js/jquery-1.8.3.min.js'></script>
    <script type="text/javascript" src="../js/bootstrap.min.js"></script>



    <style>
        .btn {
            margin-top: 0px;
        }

        .info-box-text1 {
            margin-bottom: 10%;
            font-size: small;
            white-space: nowrap
        }

        .vl {
            border-left: 6px solid green;
            height: 500px;
            position: absolute;
            left: 50%;
            margin-left: -3px;
            top: 0;
        }
    </style>



       
  <style>
      .zoomd
            {

                zoom:75%
            }
       .c {
            color: white;
          /*  //background: #698DF2;*/
          background:transparent linear-gradient(180deg, #5D7FA7 0%, #2E4E74 100%) 0% 0% no-repeat padding-box;
            border: none;
        }
         .chart_label{
            font-size:medium;
            font-weight:500;
            letter-spacing:0px;
            opacity:1;
            text-align:left;
            
        }
    .chart_label1{
            font-size:medium;
            font-family:Roboto;
            
        }

        .chart-canvas {
            border-radius: 10px;
        }

        .labelfont {
            font-family: Roboto;
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
            <div class="zoomd">
            <div class="row">
            <div class="col-md-12 ">
                <div class="card">
                    <div class="card-body">
                        <div class="row" style="margin-bottom:-18px">
                            <div class="col-md-1">
                                   <label class="control-label ">
                                    Region :
                                        <asp:RequiredFieldValidator ID="rfvddlRegion" runat="server" InitialValue="0" ControlToValidate="ddlRegion" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SaveForm"></asp:RequiredFieldValidator>
                                </label>
                                </div>
                            <div class="col-md-4 ">
                             
                                <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                    <asp:ListItem Value="">---------Select---------</asp:ListItem>
                                    <asp:ListItem Text="North" Value="North"></asp:ListItem>
                                    <asp:ListItem Text="South" Value="South"></asp:ListItem>
                                 
                                    <asp:ListItem Text="West" Value="West"></asp:ListItem>
                                    <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
                </div>
            <div class="row">
            <div class="col-md-12 ">
                <div class="card ">
                    <div class="card-header c ">
                 <asp:label runat="server" CssClass="chart_label1">       PcVisor Licenses</asp:label> <img src="Images/New folder/users.png" class="ml-4" width="30" ></img>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-4 " style="border-right:solid 1px">
                               <%-- <label class="control-label ">
                                    Purchased  License :
                                        
                                </label>--%>
                                  <asp:Label ID="lblname" runat="server" Text="Purchased  License :" ForeColor="Black" Font-Size="Large"  CssClass="lblh labelfont mr-5 ml-4"></asp:Label>
                                <asp:Label ID="lblvsapurchased" runat="server" Text="0" Font-Size="Large" ForeColor="Black" CssClass="lblh ml-5 labelfont"></asp:Label>
                             <%--  <asp:LinkButton ID="lnkpurchasedlicense" runat="server" Font-Size="Large" OnClick="lnkpurchasedlicense_Click"><i class="fa fa-2x fa-arrow-up" style="font-size:15px"></i></asp:LinkButton>--%>

                            </div>
                            <div class="col-md-4 " style="border-right: solid 1px">
                                 <asp:Label ID="Label1" runat="server" Text="Consumed  License :" ForeColor="Black" Font-Size="Large"  CssClass="lblh labelfont mr-5 ml-4"></asp:Label>
                                 <asp:Label ID="lblvsaconsumed" runat="server" CssClass="lblh  ml-5 labelfont" Font-Size="Large" ForeColor="Black" Text="0"></asp:Label>
                           <%--     <asp:LinkButton ID="linkconsumed" runat="server" OnClick="linktotal_Click"><i class="fa fa-arrow-circle-up" style="font-size:15px"></i></asp:LinkButton>--%>

                            </div>
                            <div class="col-md-4 ">
                                 <asp:Label ID="Label3" runat="server" Text="Available  License :" ForeColor="Black" Font-Size="Large"  CssClass="lblh labelfont mr-5 ml-4"></asp:Label>
                                <asp:Label ID="lblavailable" runat="server" CssClass="lblh ml-5 labelfont" ForeColor="Black"  Font-Size="Large"   Text="0"></asp:Label>
                               <%-- <asp:LinkButton ID="LinkButton1" runat="server"  OnClick="linktotal_Click"><i class="fa fa-arrow-circle-up" style="font-size:15px"></i></asp:LinkButton>--%>
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
                            <div class="row">
                                <div class="col-md-3" hidden>
                                    <div class="card">
                                        <div class="card-header c">
                                            <asp:Label runat="server" CssClass="chart_label1">  </asp:Label>
                                            PCVisor License
                                            <img src="Images/New folder/docs.png" class="fa-pull-right" width="30"></img>
                                        </div>
                                        <div class="card-body">
                                            <asp:Label ID="Label4" runat="server" Text="Used License :" ForeColor="Black" Font-Size="Large" CssClass="lblh labelfont mr-5 ml-4"></asp:Label>
                                            <asp:LinkButton ID="lbllicense" runat="server" Text="0" CssClass="lblh" Font-Size="Large" OnClick="lbllicense_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="card-header c">
                                              Customers  
                                            <img src="Images/New folder/users.png" class="fa-pull-right" width="30"></img>
                                        </div>
                                        <div class="card-body">
                                            <asp:Label ID="Label6" runat="server" Text="Total Customers :" ForeColor="Black" Font-Size="Large" CssClass="lblh labelfont mr-5 ml-4"></asp:Label>
                                            <asp:LinkButton ID="lnktotalCustomers" runat="server" Text="0" CssClass="lblh" Font-Size="Large" OnClick="lnktotalCustomers_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="card-header c">
                                            <asp:Label runat="server" CssClass="chart_label1">     Renewal</asp:Label>
                                            <img src="Images/New folder/exclamation.png" class="fa-pull-right" width="30"></img>
                                        </div>
                                        <div class="card-body">
                                            <asp:Label ID="Label5" runat="server" Text="Renewal Pending :" ForeColor="Black" Font-Size="Large" CssClass="lblh labelfont mr-5 ml-4"></asp:Label>
                                            <asp:LinkButton ID="lnktotoalrenewal" runat="server" Text="0" CssClass="lblh" Font-Size="Large" OnClick="lnktotoalrenewal_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="card">
                                       <div class="card-header c">
                                                            <asp:Label runat="server" CssClass="chart_label1">       Renewal Pending Amount</asp:Label><img src="Images/New folder/rupee.png" class="fa-pull-right" width="30"></img>
                                                        </div>
                                                        <div class="card-body">
                                                            <asp:Label ID="Label8" runat="server" Text="Total Renewal Amount :" ForeColor="Black" Font-Size="Large" CssClass="lblh labelfont mr-5 ml-4"></asp:Label>
                                                            <%--  <asp:LinkButton ID="lbltotalrevenue" runat="server" Text="0" CssClass="lblh" OnClick="lbltotalrevenue_Click"></asp:LinkButton>--%>
                                                            <asp:LinkButton ID="lnkrenwealpend" runat="server" Text="0" CssClass="lblh" Font-Size="Large" OnClick="lnkrenwealpend_Click"></asp:LinkButton>
                                                        </div>
                                    </div>
                                </div>
                                
                                
                                
                                
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="card-header c">
                                            <asp:Label runat="server" CssClass="chart_label1">     Revenue Amount (in Rs) </asp:Label>
                                            <img src="Images/New folder/rupee.png" class="fa-pull-right" width="30"></img>
                                        </div>
                                        <div class="card-body">
        <asp:Label ID="Label7" runat="server" Text="Revenue Amount :" ForeColor="Black"  Font-Size="Large" CssClass="lblh labelfont mr-5 ml-4"></asp:Label>
 <asp:LinkButton ID="lbltotalrevenue" runat="server" Text="0" CssClass="lblh" Font-Size="Large"  OnClick="lbltotalrevenue_Click"></asp:LinkButton>

                                    
                                           

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            

            <div class="row">
                <div class="col-md-4 graphs">
                                <div class="well1 white">
                                    <div class="card card-default">
                                        <div class="card-header c">
                           
                                            <asp:Label ID="lblchasiis" Text="Product Wise License" runat="server" class="size1of4" CssClass="questiontext" />

                                        </div>
                                        <div style="overflow-y:scroll;height:250px">
                                        <asp:GridView GridLines="None" ID="GridAssetDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" Width="100%" DataKeyNames="Product" >
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No." ControlStyle-Font-Size="Medium" ItemStyle-Width="20">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Product" ControlStyle-Font-Size="Medium" HeaderText="Product" ItemStyle-Width="150px" />
                                                <asp:TemplateField HeaderText="Count" ControlStyle-Font-Size="Medium" ItemStyle-Width="20">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkprodcount"  runat="server" ForeColor="Blue"  CommandName="Select" Text='<%# Eval("Count") %>' OnClick="lnkprodcount_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="White" BorderColor="#e3e4e6" CssClass="text-center" BorderWidth="1px" Height="20px" Font-Size="Medium" />
                                            <FooterStyle BackColor="#EDEDED" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#EDEDED" ForeColor="#000000" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="White" Font-Bold="True" Height="20px" ForeColor="#000000" />
                                            <HeaderStyle Font-Bold="True" CssClass="text-center" BackColor="LightGray" ForeColor="Black" Height="20px" Font-Size="Medium" />
                                            <EditRowStyle BackColor="#e9edf2" BorderColor="#e3e4e6" BorderStyle="Solid" Height="20px" BorderWidth="1px" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#EDEDED" />
                                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                                            <AlternatingRowStyle BackColor="White" BorderColor="#e3e4e6" Font-Size="Small" Height="20px" BorderStyle="Solid" BorderWidth="1px" />
                                        </asp:GridView>
                                       </div>
                                    </div>
                                </div>
                            </div>
                <div class="col-md-4 graphs ">

                    <div class="card card-default">
                          <div class="card-header c">
                         Customer Region Wise 
                        <asp:Button ID="btncustomerregion" runat="server" OnClick="btncustomerregion_Click" hidden />
                    </div>
                    <div class="card-body">

                        <!-- Sales Chart Canvas -->
                        <canvas id="chartcustregion" height="220" width="220" class="chartjs-render-monitor"></canvas>
               
                    </div>
                        
                    </div>

                </div>

                <div class="col-md-4 graphs ">
                    <div class="well1 white">
                        <div class="card card-default">
                             <div class="card-header c">
                Revenue Region Wise 
                        <asp:Button ID="btnrevenue" runat="server" OnClick="btnrevenue_Click" hidden />
                    </div>
                    <div class="card-body">

                        <!-- Sales Chart Canvas -->
                        <canvas id="chatrevenue" height="220" width="220" class="chartjs-render-monitor"></canvas>
               
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
                      <%--  <div class="modal-header c">
                               <asp:label runat="server" Text="Customer Details" CssClass="fa-pull-left" />
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        
                        </div>--%>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="col-md-12 graphs" style="padding: 0px">
                                    <div class="xs">
                                        <div class="well1 white" style="padding: 0px">
                                            <div class="card card-default">

                                                <div class="card-header c">
                                                          <asp:Label runat="server" Text="Customer Details" ForeColor="White" CssClass="fa-pull-left" />
                                                    <button type="button" class="close fa-pull-right btn-outline-danger mb-1"   style="color:white" onclick="javascript:window.location.reload()" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                    <%--    <asp:Label ID="lblsofname" runat="server" Text=""></asp:Label>
                                                    <asp:Label ID="lblTotal" runat="server" CssClass="pull-right control-label" Text="0"></asp:Label>
                                                    <asp:Label ID="Label2" runat="server" CssClass="pull-right control-label" Text="Total: "></asp:Label>
                                                    <asp:ImageButton ID="ImgbtnExport" runat="server" ImageUrl="~/images/excel.png" CssClass="pull-right" OnClick="ImgbtnExport_Click" />--%>
                                                </div>
                                                <div class="card-body">
                                                    <div class="row ">
                                                        <div class="col-md-4">

                                                            <asp:Label ID="lblsofname" runat="server" Text="" Font-Size="Larger" ForeColor="Black"></asp:Label>

                                                        </div>
                                                        <div class="col-md-6">
                                                        </div>
                                                        <div class="col-md-2 ">
                                                            <div class="btn btn-sm elevation-1 ml-3 " style="padding: 0px; margin-bottom: 10px; padding-top: 1px">
                                                                <label class="mr-3  mb-0">Export</label>
                                                                <asp:ImageButton ID="ImgbtnExport" runat="server" ImageUrl="~/Images/New folder/excelnew.png" CssClass="fa-pull-right btn-outline-success" OnClick="ImgbtnExport_Click" />
                                                            </div>
                                                      <%--  </div>
                                                        <div class="col-md-1">--%>
                                                             <asp:Label ID="Label2" runat="server" CssClass=" ml-1 control-label" Font-Size="Large" Text="Total: "></asp:Label>
                                                            <asp:Label ID="lblTotal" runat="server" CssClass=" ml-1 control-label" Font-Size="Large" Text="0"></asp:Label>
                                                           
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
                                
                                <asp:AsyncPostBackTrigger ControlID="lbllicense" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnktotoalrenewal" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnktotalCustomers" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lbltotalrevenue" EventName="Click" />
                              <%--  <asp:AsyncPostBackTrigger ControlID="ddlRegion" EventName="SelectedIndexChanged" />--%>
                                 <asp:PostBackTrigger ControlID="ddlRegion" />
                                <asp:AsyncPostBackTrigger ControlID="btnrevenue" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btncustomerregion" EventName="Click" />
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
               <script src="chartjs/js/chart.js"></script>
            <script src="chartjs/js/chart.min.js"></script>
            <script type="text/javascript" src="../Script/Scroll/1.8.2.jquery.min.js"></script>
            <script type="text/javascript" src="../Script/Scroll/1.9.1.jquery-ui.min.js"></script>
            <script type="text/javascript" src="../Script/Scroll/gridviewScroll.min.js"></script>
                 <asp:Literal ID="ltrCustomerregion" runat="server"></asp:Literal>
                 <asp:Literal ID="ltrevenue" runat="server"></asp:Literal>
                <asp:HiddenField ID="hdnfldVariable" runat="server"/>
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
            <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
            <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
            <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
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
      <script type="text/javascript">
          var charOffmachdata;
          var chartOffmachlabel;

          function clickchartOfflineMachines(e) {

              //console.log("clicked!!!!!!!!!!!!!!!", e, item);
              //console.log(item[0].index)

              let label;
              let value;

              const points = myChartoff.getElementsAtEventForMode(e, 'nearest', {
                  intersect: true
              }, false)
              console.log(points);

              if (points.length) {
                  //console.log("!!!!!!!!!!!!!!!", label, value);

                  let firstPoint = points[0];
                  // console.log("points", points);
                  //  console.log("myChart3.data", mychartchasis.data)
                  label = myChartoff.data.labels[firstPoint.index];
                  value = myChartoff.data.datasets[firstPoint.datasetIndex].data[firstPoint.index];

                  // alert(label);
                  document.getElementById('<%=hdnfldVariable.ClientID %>').value =label;
                 document.getElementById('<%=btncustomerregion.ClientID %>').click();

              }

          }

          var ctx = document.getElementById('chartcustregion').getContext('2d');
          var myChartoff = new Chart(ctx, {
              type: 'doughnut',

              data: {
                  labels: chartOffmachlabel,

                  datasets: [{
                      label: 'MS Office Flavors',
                      data: charOffmachdata,
                      tension: 0.4,
                      borderWidth: 0,

                      borderRadius: 4,
                      maxBarThickness: 30,
                      backgroundColor: [
                          '#F34F4F',
                          '#ACC1FE',
                          '#FFAF10',
                          '#3AC93A',
                          '#41415C',
                          '#3DFAB5',

                          '#F5687B',
                          '#10BFFF',
                          '#8FFF8F',
                          '#B8B8B8',
                          '#BA43FF',
                          '#4875F3',

                          '#FFAF1087',
                          '#5FB05F',
                          '#666693',
                          '#71B9B0',

                          '#4E83D3',
                          '#FFC85A',
                          '#CE853E',
                          '#B7B7DD'
                      ],
                      borderColor: [
                          '#F34F4F',
                          '#ACC1FE',
                          '#FFAF10',
                          '#3AC93A',
                          '#41415C',
                          '#3DFAB5',

                          '#F5687B',
                          '#10BFFF',
                          '#8FFF8F',
                          '#B8B8B8',
                          '#BA43FF',
                          '#4875F3',

                          '#FFAF1087',
                          '#5FB05F',
                          '#666693',
                          '#71B9B0',

                          '#4E83D3',
                          '#FFC85A',
                          '#CE853E',
                          '#B7B7DD'
                      ],


                      borderWidth: 1
                  }]

              },
              options: {
                  responsive: true,
                  maintainAspectRatio: false,
                  cutout: 80,
                  plugins: {
                      legend: {
                          display: true,
                          position: 'bottom',
                          labels: {
                              fontColor: [

                                  'rgb(255,0,0, 1)',
                                  'rgb(50,205,50,1)',


                              ],
                              usePointStyle: true,
                              boxWidth: 10,
                              maxHeight: 10,
                              pointStyle: 'rectRounded',



                          }
                      }
                  },
                  interaction: {
                      intersect: false,
                      mode: 'index',
                  },
                  onClick: clickchartOfflineMachines

              },
          });

      </script>
      <script type="text/javascript">
        var chartrevenuecount;
        var chartrevenuelabel;

        function clickchartrevenue(e) {

            //console.log("clicked!!!!!!!!!!!!!!!", e, item);
            //console.log(item[0].index)

            let label;
            let value;

            const points = myChartrevenue.getElementsAtEventForMode(e, 'nearest', {
                intersect: true
            }, false)
            console.log(points);

            if (points.length) {
                //console.log("!!!!!!!!!!!!!!!", label, value);

                let firstPoint = points[0];
                // console.log("points", points);
                //  console.log("myChart3.data", mychartchasis.data)
                label = myChartrevenue.data.labels[firstPoint.index];
                value = myChartrevenue.data.datasets[firstPoint.datasetIndex].data[firstPoint.index];

                // alert(label);
                document.getElementById('<%=hdnfldVariable.ClientID %>').value = label;
                document.getElementById('<%=btnrevenue.ClientID %>').click();

            }

        }

        var ctx1 = document.getElementById('chatrevenue').getContext('2d');
        var myChartrevenue = new Chart(ctx1, {
            type: 'doughnut',

            data: {
                labels: chartrevenuelabel,

                datasets: [{
                    label: 'MS Office Flavors',
                    data: chartrevenuecount,
                    tension: 0.4,
                    borderWidth: 0,

                    borderRadius: 4,
                    maxBarThickness: 20,
                    backgroundColor: [
                        '#F34F4F',
                        '#ACC1FE',
                        '#FFAF10',
                        '#3AC93A',
                        '#41415C',
                        '#3DFAB5',

                        '#F5687B',
                        '#10BFFF',
                        '#8FFF8F',
                        '#B8B8B8',
                        '#BA43FF',
                        '#4875F3',

                        '#FFAF1087',
                        '#5FB05F',
                        '#666693',
                        '#71B9B0',

                        '#4E83D3',
                        '#FFC85A',
                        '#CE853E',
                        '#B7B7DD'
                    ],
                    borderColor: [
                        '#F34F4F',
                        '#ACC1FE',
                        '#FFAF10',
                        '#3AC93A',
                        '#41415C',
                        '#3DFAB5',

                        '#F5687B',
                        '#10BFFF',
                        '#8FFF8F',
                        '#B8B8B8',
                        '#BA43FF',
                        '#4875F3',

                        '#FFAF1087',
                        '#5FB05F',
                        '#666693',
                        '#71B9B0',

                        '#4E83D3',
                        '#FFC85A',
                        '#CE853E',
                        '#B7B7DD'
                    ],


                    borderWidth: 1
                }]

            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                cutout: 85,
                plugins: {
                    legend: {
                        display: true,
                        position: 'bottom',
                        labels: {
                            fontColor: [

                                'rgb(255,0,0, 1)',
                                'rgb(50,205,50,1)',


                            ],
                            usePointStyle: true,
                            boxWidth: 10,
                            maxHeight: 10,
                            pointStyle: 'rectRounded',



                        }
                    }
                },
                interaction: {
                    intersect: false,
                    mode: 'index',
                },
                onClick: clickchartrevenue

            },
        });

    </script>
</asp:Content>



