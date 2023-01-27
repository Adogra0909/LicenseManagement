<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmcloudpremises.aspx.cs" Inherits="frmcloudpremises" %>

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

                zoom:75%
            }
    </style>
    <script type="text/javascript">

        function ToggleVisible(ddl) {
            var div = document.getElementById('div_shared');
            var divnotshared = document.getElementById('div_notshared');
            var value = ddl.options[ddl.selectedIndex].value;
            if (value == "SharedCloud") {
                div.style.display = "block";
                divnotshared.style.display="none"
            }
            else {
                div.style.display = "none";
                divnotshared.style.display = "block"
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                            Add Customer Server Details         
                        </div>
                        <div class="card-body">

                            <div class="form-group">
                                <asp:Label ID="lblerrorMsg" runat="server" Text="" CssClass="label" ForeColor="#ff0000"></asp:Label>
                                <div class="row">
                                    <div class="col-md-3 ">
                                        <label class="control-label ">
                                            Organisation :
                                        <asp:RequiredFieldValidator ID="RfvOrganisation" runat="server" InitialValue="0" ControlToValidate="ddlOrg" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SaveForm"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:DropDownList ID="ddlOrg" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlOrg_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                       
                                        <label class="control-label ">
                                          <asp:Label ID="Label10" runat="server" Text="Contact Person :"></asp:Label>
                                                   <asp:RequiredFieldValidator ID="rfvtxtCustomerName" runat="server" ControlToValidate="txtContactPerson_Name" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SaveForm"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox ID="txtContactPerson_Name" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                    </div>
                                   <div class="col-md-3 ">
                                        <label class="control-label ">
                                            Cloud Type :
                                        <asp:RequiredFieldValidator ID="rftvddlcloud" runat="server" InitialValue="0" ControlToValidate="ddlcloud" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SaveForm"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:DropDownList ID="ddlcloud" runat="server" CssClass="form-control form-control-sm chzn-select"  onchange="ToggleVisible(this);">
                                          <%--  <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                             <asp:ListItem Text="Our Cloud" Value="OurCloud"></asp:ListItem>
                                             <asp:ListItem Text="On Premise" Value="OnPremise"></asp:ListItem>
                                             <asp:ListItem Text="Shared Cloud" Value="Sharedcloud"></asp:ListItem>
                                             <asp:ListItem Text="AWS" Value="AWS"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label ">
                                            Account Manager Name :
                                        </label>
                                        <asp:TextBox ID="txtAccountManager" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    <div class="col-md-3">
                                        <label class="control-label ">
                                           Contact Person Number :
                                        </label>
                                        <asp:TextBox ID="txtnumber" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label ">
                                            Customer Email Id  :
                                        </label>
                                        <asp:TextBox ID="txtemailid" runat="server" CssClass="form-control form-control-sm" MaxLength="50" ></asp:TextBox>
                                    </div>
                                    
                                    <div class="col-md-3">
                                        <label class="control-label ">
                                            Region:
                                             <asp:RequiredFieldValidator ID="rfvregion" runat="server" InitialValue="0" ControlToValidate="ddlregion" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SaveForm"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:DropDownList ID="ddlregion" runat="server" CssClass="form-control form-control-sm chzn-select">
                                            <asp:ListItem Selected="True" Value="West">West</asp:ListItem>
                                            <asp:ListItem Value="East">East</asp:ListItem>
                                            <asp:ListItem Value="North">North</asp:ListItem>
                                            <asp:ListItem Value="South">South</asp:ListItem>
                                            <asp:ListItem Value="Other">Other</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label">
                                                 
                                            Location :
                                             <asp:RequiredFieldValidator ID="rfvLocation" runat="server" InitialValue="0" ControlToValidate="txtLocation" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SaveForm"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control form-control-sm" MaxLength="250"></asp:TextBox>
                                    </div>
                                  
                                </div>
                                <div class="row mt-2" id="div_notshared" style="display:none">
                                    <div class="col-md-3">
                                        <label class="control-label ">
                                          Server Local IP :
                                        
                                        </label>
                                        <asp:TextBox ID="txtlocalip" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label ">
                                          Server Public IP  :
                                        </label>
                                        <asp:TextBox ID="txtpublicip" runat="server" CssClass="form-control form-control-sm" MaxLength="50" ></asp:TextBox>
                                    </div>
                                          <div class="col-md-3">
                                        <label class="control-label ">
                                          Window UserName :
                                        </label>
                                        <asp:TextBox ID="txtwindusername" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label ">
                                          Window Password :
                                        </label>
                                        <asp:TextBox ID="txtwindpassword" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 mt-3">
                                        <label class="control-label ">
                                           Portal UserName  :
                                        </label>
                                        <asp:TextBox ID="txtportalusername" runat="server" CssClass="form-control form-control-sm" MaxLength="50" ></asp:TextBox>
                                    </div>
                                    
                                    <div class="col-md-3 mt-3">
                                        <label class="control-label ">
                                            Portal Password . :
                                        </label>
                                        <asp:TextBox ID="txtportalpassword" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 mt-3">
                                        <label class="control-label">
                                            Owner :
                                        </label>
                                        <asp:TextBox ID="txtowner" runat="server" CssClass="form-control form-control-sm" MaxLength="250"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 mt-3">
                                        <label class="control-label">
                                            SA UserName :
                                        </label>
                                        <asp:TextBox ID="txtsausername" runat="server" CssClass="form-control form-control-sm" MaxLength="250"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 mt-3">
                                        <label class="control-label">
                                            SA Password :
                                        </label>
                                        <asp:TextBox ID="txtsaPassword" runat="server" CssClass="form-control form-control-sm" MaxLength="250"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row mt-2" id="div_shared" style="display:none">
                                    <div class="col-md-3">
                                        <label class="control-label">
                                            URL :
                                        </label>
                                        <asp:TextBox ID="txturl" runat="server" CssClass="form-control form-control-sm" MaxLength="250"></asp:TextBox>
                                    </div>
                                    </div>
                                <div class="row mt-3">
                                    <div class="col-md-3 mt-4">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" ValidationGroup="SaveForm" CssClass="btn btn-sm c mt-4" />
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" ValidationGroup="SaveForm" CssClass="btn btn-sm c mt-4" />


                                    </div>
                                   <%-- <div class="col-md-3">

                                        <asp:TextBox ID="txtAccount_Manager" runat="server" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredtxtAccount_Manager" runat="server" ControlToValidate="txtAccount_Manager" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SU"></asp:RequiredFieldValidator>

                                    </div>--%>
                                    <div class="col-md-3" hidden>

                                        <asp:Label ID="Label9" runat="server" Text="Search Device :"></asp:Label>


                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="input_field270"></asp:TextBox>
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-sm btn-success warning_3" />


                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
   
        <div class="row">

            <div class="col-md-12 graphs">
                <div class="card">
                    <div class="card-body">
                        <div class="form-group">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label><br />
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowFooter="true" DataKeyNames="id"
                                OnRowDeleting="GridView1_RowDeleting" OnRowCommand="GridView1_RowCommand" CssClass="table table-bordered" ShowHeaderWhenEmpty="true">
                                <HeaderStyle BackColor="Green" ForeColor="Yellow" />
                                <RowStyle BackColor="LightGreen" />
                                <FooterStyle BackColor="Green" ForeColor="Yellow" />
                                <Columns>

                                    <asp:TemplateField HeaderText="SrNo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSrNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="OrgID" ConvertEmptyStringToNull="true" HeaderText="OrgID" />
                                    <asp:BoundField DataField="Hosting" ConvertEmptyStringToNull="true" HeaderText="Hosting" />
                                    <asp:BoundField DataField="WindowUserName" ConvertEmptyStringToNull="true" HeaderText="Window User Name" />
                                    <asp:BoundField DataField="WindowPassword" ConvertEmptyStringToNull="true" HeaderText="Window Password" />
                                    <asp:BoundField DataField="PortalUserName" ConvertEmptyStringToNull="true" HeaderText="Portal User Name" />
                                    <asp:BoundField DataField="PortalPassword" ConvertEmptyStringToNull="true" HeaderText="Portal Password" />
                                    <asp:BoundField DataField="URL" ConvertEmptyStringToNull="true" HeaderText="URL" />

                                    <asp:BoundField DataField="LocalIP" ConvertEmptyStringToNull="true" HeaderText="Local IP" />
                                    <asp:BoundField DataField="PublicIP" ConvertEmptyStringToNull="true" HeaderText="Public IP" />
                                    <asp:BoundField DataField="Owner" ConvertEmptyStringToNull="true" HeaderText="Owner" />

                                    <asp:BoundField DataField="SAUserName" ConvertEmptyStringToNull="true" HeaderText="SA User Name" />
                                    <asp:BoundField DataField="SAPassword" ConvertEmptyStringToNull="true" HeaderText="SA Password" />





                                    <asp:TemplateField ItemStyle-Width="50px">
                                        <HeaderTemplate>Delete </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:Button ID="ButtonDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?')" Text="Delete" CssClass="btn btn-outline-danger" />

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
          </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlOrg" />
              <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowDeleting" />
            </Triggers>
    </asp:UpdatePanel>
     <link rel="stylesheet" type="text/css" href="../Script/build/jquery.datetimepicker.css" />
    <script src="../Script/build2/jquery.js"></script>
    <script src="../Script/build2/jquery.datetimepicker.full.js"></script>
            <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    <link rel="stylesheet" href="../Scripts/chosen.css" /> 
</asp:Content>

