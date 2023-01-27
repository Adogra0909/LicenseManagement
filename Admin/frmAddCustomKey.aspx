<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmAddCustomKey.aspx.cs" Inherits="frmAddCustomKey" EnableEventValidation="false" %>

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
    <script type="text/javascript">
        function allowOnlyNumber(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>

    <script src="js/GScripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="js/GScripts/ScrollableGridViewPlugin_ASP.NetAJAXmin.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=gvstate.ClientID %>').Scrollable({
                ScrollHeight: 440,
                IsInUpdatePanel: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-md-12 graphs  ">
        <div class="xs">
            <div class="well1 white">
                <div class="Compose-Message">
                    <div class="card card-default">
                        <div class="card-header c">
                            Add Customer Key Count
                        </div>
                        <div class="card-body">
                            <div class="row">
                               

                                <div class="col-md-4">
                                    <label class="control-label ">
                                       Master License Key :
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" ControlToValidate="ddlkey" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SaveForm"></asp:RequiredFieldValidator>
                                    </label>
                                    <asp:DropDownList ID="ddlkey" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlkey_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-2">
                                    <label class="control-label ">
                                        Quantity :
              
                                    </label>
                                    <asp:TextBox ID="txtQuantity" runat="server" ReadOnly="true" CssClass="form-control form-control-sm" AutoComplete="off"></asp:TextBox>
                                </div>
                                
                                <div class="col-md-2">
                                    <label class="control-label ">
                                        Child  License Key :
              
                                    </label>
                                    <asp:TextBox ID="txtchildlicKey" runat="server" CssClass="form-control form-control-sm" AutoComplete="off"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                    <label class="control-label ">
                                       Product Type :
            
                                    </label>
                                    <asp:DropDownList ID="ddlproducttype" runat="server" CssClass="form-control form-control-sm chzn-select">
                                        <asp:ListItem Text="VSA+SD" Value="VSA+SD"></asp:ListItem>
                                         <asp:ListItem Text="VSA" Value="VSA"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label class="control-label ">
                                        Assigned Qty :
              
                                    </label>
                                    <asp:TextBox ID="txtAssigned" runat="server"  CssClass="form-control form-control-sm" AutoComplete="off"></asp:TextBox>
                                </div>

                            </div>
                            
                            <div class="form-group">
                                <asp:Button ID="btnTypeAdd" runat="server" CssClass="btn btn-sm c" Text="Save" ValidationGroup="btnSave" OnClick="btnTypeAdd_Click" />
                                <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-sm c" Text="Update" Visible="false"  ValidationGroup="btnSave" OnClick="btnUpdate_Click" />
                                <asp:Label ID="lblsuccess" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
   </div>
    <div class="col-md-12 graphs" >
        <div class="xs">
            <div class="well1 white">
                <div class="card card-default">
                <%--    <div class="card-header ">
                        Customer Wise Key Details
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                        <asp:Label ID="Label3" runat="server"></asp:Label>
                        <asp:ImageButton ID="ImgBtnExport" runat="server" ImageUrl="~/images/excel.png" OnClick="ImgBtnExport_Click" CssClass="pull-right" />
                    </div>--%>
                    <div class="card-body ">
                        <div class="row ">
                            <div class="col-md-4">

                                <asp:Label ID="lblsofname" runat="server" Text="  Customer Wise Key Details" Font-Size="Larger" ForeColor="Black"></asp:Label>

                            </div>
                            <div class="col-md-7">
                            </div>
                            <div class="col-md-1 ">
                                <div class="btn btn-sm elevation-1 ml-3" style="padding: 0px; margin-bottom: 10px; padding-top: 1px">
                                    <label class="mr-3 ml-1  mb-0">Export</label>
                                    <asp:ImageButton ID="ImgBtnExport" runat="server" ImageUrl="~/Images/New folder/excelnew.png" CssClass="fa-pull-right btn-outline-success" OnClick="ImgBtnExport_Click" />
                                </div>
                            </div>
                           <%-- <div class="col-md-1">
                                <label class="control-label pull-right ">
                                    Total Assets : 
                            <asp:Label ID="lblTotalCount" runat="server" Text="0"></asp:Label>
                                </label>
                            </div>--%>
                        </div>
                        <asp:GridView GridLines="None" ID="gvstate" runat="server" DataKeyNames="ChildLicKey" AutoGenerateColumns="false" CssClass="table table-bordered"
                            Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="20">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="MasterLicenseKey" HeaderText="MasterLicenseKey" NullDisplayText="0" />

                                <asp:BoundField DataField="ChildLicKey" HeaderText="Child License Key" NullDisplayText="0" />
                                <asp:BoundField DataField="ProductType" HeaderText="Product Type" NullDisplayText="0" />
                                <asp:BoundField DataField="Assigned" HeaderText="Assigned" NullDisplayText="0" />

                                <%-- <asp:ButtonField ButtonType="Image" CommandName="SelectState" HeaderText="Edit" ImageUrl="~/images/edit.png" ItemStyle-Width="20px" />--%>
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
       <link rel="stylesheet" type="text/css" href="../Script/build/jquery.datetimepicker.css" />
    <script src="../Script/build2/jquery.js"></script>
    <script src="../Script/build2/jquery.datetimepicker.full.js"></script>
            <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    <link rel="stylesheet" href="../Scripts/chosen.css" /> 
</asp:Content>

