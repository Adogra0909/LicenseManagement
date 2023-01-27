<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmAddCustomKey.aspx.cs" Inherits="frmAddCustomKey" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="pardeep.css" rel="stylesheet" type="text/css" />
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
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Add Customer Key Count
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <label class="control-label ">
                                        Organisation :
                                        <asp:RequiredFieldValidator ID="RfvOrganisation" runat="server" InitialValue="0" ControlToValidate="ddlOrg" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SaveForm"></asp:RequiredFieldValidator>
                                    </label>
                                    <asp:DropDownList ID="ddlOrg" runat="server" CssClass="form-control1 chzn-select">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-3">
                                    <label class="control-label ">
                                        License Key :
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" ControlToValidate="ddlkey" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SaveForm"></asp:RequiredFieldValidator>
                                    </label>
                                    <asp:DropDownList ID="ddlkey" runat="server" CssClass="form-control1 chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlkey_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-3">
                                    <label class="control-label ">
                                        Quantity :
              
                                    </label>
                                    <asp:TextBox ID="txtQuantity" runat="server" ReadOnly="true" CssClass="form-control1" AutoComplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label class="control-label ">
                                        Assigned Qty :
              
                                    </label>
                                    <asp:TextBox ID="txtAssigned" runat="server"  CssClass="form-control1" AutoComplete="off"></asp:TextBox>
                                </div>

                            </div>
                            
                            <div class="form-group">
                                <asp:Button ID="btnTypeAdd" runat="server" CssClass="btn btn-sm btn-success warning_3" Text="Save" ValidationGroup="btnSave" OnClick="btnTypeAdd_Click" />
                                <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-sm btn-success warning_3" Text="Update" Visible="false"  ValidationGroup="btnSave" OnClick="btnUpdate_Click" />
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
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Customer Wise Key Details
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                        <asp:Label ID="Label3" runat="server"></asp:Label>
                        <asp:ImageButton ID="ImgBtnExport" runat="server" ImageUrl="~/images/excel.png" OnClick="ImgBtnExport_Click" CssClass="pull-right" />
                    </div>
                    <asp:GridView GridLines="None" ID="gvstate" runat="server" DataKeyNames="LicKeyCode" AutoGenerateColumns="false" CssClass="table table-bordered"
                        Width="100%" >
                        <Columns>
                               <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="20">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                          <asp:BoundField DataField="OrgID" HeaderText="OrgID" NullDisplayText="0" />
                            <asp:BoundField DataField="LicKeyCode" HeaderText="License Code" NullDisplayText="0" />
                            <asp:BoundField DataField="Assigned" HeaderText="Assigned" NullDisplayText="0" />
                          
                           <%-- <asp:ButtonField ButtonType="Image" CommandName="SelectState" HeaderText="Edit" ImageUrl="~/images/edit.png" ItemStyle-Width="20px" />--%>
                        </Columns>
                        <RowStyle BackColor="#fafafa" BorderColor="#e3e4e6" BorderWidth="1px" />
                        <FooterStyle BackColor="#EDEDED" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#EDEDED" ForeColor="#000000" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E3E4E6" Font-Bold="True" ForeColor="#000000" />
                        <HeaderStyle BackColor="#e3e4e6" Font-Bold="True" Font-Size="Small" ForeColor="#000000" Height="30px" />
                        <EditRowStyle BackColor="#EDEDED" BorderColor="#e3e4e6" BorderStyle="Solid" BorderWidth="1px" />
                        <AlternatingRowStyle BackColor="White" BorderColor="#e3e4e6" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:GridView>
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

