<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmChgPass.aspx.cs" Inherits="frmChgPass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col-md-6 graphs">
        <div class="card">
            <div class="card-header c">
                Change Password
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-10">
                <label class="control-label ">Enter New Password :
                       <asp:RequiredFieldValidator ID="rfvchagpass" runat="server" ControlToValidate="txtpassword" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ValidationGroup="chgpass"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox ID="txtpassword" runat="server" CssClass=" form-control form-control-sm" MaxLength="50" ValidationGroup="chgpass" TextMode="Password" placeholder="Password"></asp:TextBox>
             
              </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                    <asp:Button ID="btnChgpass" runat="server" Text="Change Password"  CssClass="btn btn-sm c" OnClick="btnChgpass_Click" ValidationGroup="chgpass" />
                <asp:Label ID="lblmessge" runat="server" Text=""></asp:Label>
            </div>
                        </div>
                </div>
        </div>
    </div>
</asp:Content>

