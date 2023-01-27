<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Custom Portal--Hitachi Systems India Pvt Ltd.</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta name="viewport" content="width=device-width, initial-scale=0" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../../plugins/fontawesome-free/css/all.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />
    <!-- icheck bootstrap -->
    <link rel="stylesheet" href="../../plugins/icheck-bootstrap/icheck-bootstrap.min.css" />
    <!-- Theme style -->
    <link href="LoginCss/css/style.css" rel="stylesheet" />
    <!-- Google Font: Source Sans Pro -->
<style type="text/css">
    .img {
        background-image: url('Images/New folder/defaultpage.png');
        background-size: cover;
        /* // background-position: center center;*/
        background-repeat: no-repeat;
   
        background-size: cover;
    }
     .atag{
          font-size:x-large;
          font-weight:600;
          font-family:serif;

      }
     .crdcurve{
         border-radius:5px;
           box-shadow:
       inset 0 -3em 3em rgba(0,0,0,0.1),
           
             0.3em 0.3em 1em rgba(0,0,0,0.3);
     }
     .font_label{
         font-family:'Roboto';
         font-size:medium;
     }
       .font_fam{
        font-family:'Roboto';
        font-weight:500
     }
     .c{
         background:#BFCED8;
    opacity:77%;
     }
    .btncol{
        background: linear-gradient(180deg, #3877C2 0%, #235A9D 100%) 0% 0% no-repeat padding-box;
        color:white;
    }
    .no_Opac{
        opacity:0%
    }
    .zoomd{
        zoom:75%;
    }
      </style>
    <style>
        #landing-area{
		width: 100vw;
		height: 100vh;
		display: flex; 
		background-color: #F46835;
	}

	#box-left{
		width: 50%;
		clip-path: polygon(0 0, calc(100% - 10vh) 0, 100% 100%, 0 100%);
		-webkit-clip-path: polygon(0 0, calc(100% - 10vh) 0, 100% 100%, 0 100%);
		margin-right: -4.2vh;
		padding: 5px 11vh 5px 5px;
		background-color: white;
		text-align: center;
	}
	#box-right{
		width: 50%;
		clip-path: polygon(0 0, 100% 0, 100% 100%, calc(0% + 10vh) 100%);
		-webkit-clip-path: polygon(0 0, 100% 0, 100% 100%, calc(0% + 10vh) 100%);
		/*margin-left: -4.2vh;*/
		padding: 5px 5px 5px 11vh;
		background-color: #3877C2 ;
		text-align: center;
	}
        </style>
    <style>
* {box-sizing: border-box;}
body {font-family: Verdana, sans-serif;}
.mySlides {display: none;}
.mySlides1 {display: none;}
img {vertical-align: middle;}

/* Slideshow container */
.slideshow-container {
  max-width: 1000px;
  position: relative;
  margin: auto;
}

/* Caption text */
.text {
  color: #f2f2f2;
  font-size: 15px;
  padding: 8px 12px;
  position: absolute;
  bottom: 8px;
  width: 100%;
  text-align: center;
}

/* Number text (1/3 etc) */
.numbertext {
  color: #f2f2f2;
  font-size: 12px;
  padding: 8px 12px;
  position: absolute;
  top: 0;
}

/* The dots/bullets/indicators */
.dot {
  height: 15px;
  width: 15px;
  margin: 0 2px;
  background-color: #bbb;
  border-radius: 50%;
  display: inline-block;
  transition: background-color 0.6s ease;
}

.active {
  background-color: #717171;
}

/* Fading animation */
.fade {
  animation-name: fade;
  animation-duration: 1.5s;
}

@keyframes fade {
  from {opacity: .4} 
  to {opacity: 1}
}

/* On smaller screens, decrease text size */
@media only screen and (max-width: 300px) {
  .text {font-size: 11px}
}
.textslide{
    font:400;
    font-family:'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
    font-size:large;
    color:white;
}
.backcolor{
  background:  transparent linear-gradient(215deg, #F6FAFF 0%, #E0EFFF 20%, #C8E2FF 58%, #8FC4FF 100%) 0% 0% no-repeat padding-box;
}
</style>
</head>
<%--<body class="hold-transition login-page img">--%>
    <body class="backcolor" >
        <asp:Label ID="lblsn" ForeColor="Black" Text="" runat="server" />
    <%--<div class="login-box">
        <div class="login-logo">
          
            <img src="Images/Hitachi-Logo-White.png" width="200" />
        </div>--%>
        <!-- /.login-logo -->
    <div class="container-fluid">
        <div class="row backcolor" >


            <div class="col-md-4 " style="margin-bottom:1%">




                <form id="form1" runat="server" >
                    <%--    <div class="card  ">--%>
                    <asp:Label ID="lblerrorMsg" Text="" runat="server" />
                    <%--    <div class="card-body ">--%>
                    <br />
                    <br />
                    <div class="row ">
                        <div class="col-md-12">
                            <center>
                                <div class="login-logo">

                                    <img src="Images/pcvlogo.png" width="200" />
                                </div>

                                <br />

                                <asp:Label ID="lbl" runat="server" CssClass=" col-md-1 ml-5  font_fam offset-1" Font-Size="XX-Large" Text="Sign In"></asp:Label>

                                <br />
                            </center>
                        </div>
                    </div>
                    <div class="row mt-2 ml-2">

                        <div class="col-md-9 form-group offset-2 mt-2 mb-3">
                            <label class="font_label fa-pull-left">User Name</label>
                            <asp:TextBox ID="txtuser" runat="server" class="form-control" Font-Size="Large" placeholder="User Name"></asp:TextBox>


                        </div>
                    </div>
                    <div class="row ml-2">
                        <div class="form-group offset-2 col-md-9 mb-3">
                            <label class="font_label fa-pull-left">Password</label>
                            <asp:TextBox ID="txtpassword" runat="server" class="form-control" Font-Size="Large" placeholder="Password" TextMode="Password"></asp:TextBox>

                        </div>
                    </div>
                    <div class="row ml-2">

                        <p class="mb-1 fa-pull-left offset-2 font_label" style="padding: 2px">
                            <a href="forgot-password.html" style="color: cornflowerblue">Forgot Password</a>
                        </p>
                    </div>
                    <div class="row ml-2">

                        <!-- /.col -->
                        <div class="col-9 offset-2">
                            <asp:Button ID="btnLogin" runat="server" Text="Sign In" class="btn  btn-block btncol font_label" OnClick="btnLogin_Click" />
                        </div>
                        <!-- /.col -->
                    </div>

                    <br />
                    <br />
                    <br />
                    <br />
                         <br />
                   
                </form>
            </div>

            <div class="col-md-8 img">
                <br />
                <br />
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <center>
                            <div class="slideshow-container">

                                <div class="mySlides fade">
                                    <div class="numbertext">1 / 3</div>
                                    <img src="Images/homeimg/ist.png" style="width: 50%" />

                                </div>

                                <div class="mySlides fade">
                                    <div class="numbertext">2 / 3</div>
                                    <img src="Images/homeimg/2nd.png" style="width: 50%" />

                                </div>

                                <div class="mySlides fade">
                                    <div class="numbertext">3 / 3</div>
                                    <img src="Images/homeimg/3rd.png" style="width: 50%" />

                                </div>

                            </div>


                            <div style="text-align: center">
                                <span class="dot"></span>
                                <span class="dot"></span>
                                <span class="dot"></span>
                            </div>
                        </center>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <center>
                            <div class="mySlides1 fade">

                                <div class="textslide">PCVisor Manged Services</div>

                            </div>
                            <div class="mySlides1 fade">

                                <div class="textslide">ITCM</div>

                            </div>
                            <div class="mySlides1 fade">

                                <div class="textslide">ITSM</div>

                            </div>
                        </center>
                    </div>
                </div>
            </div>
        </div>
  <%--  </div>--%>
    <!-- /.login-box -->
        </div>
    <!-- jQuery -->
    <script src="../../plugins/jquery/jquery.min.js"></script>
    <!-- Bootstrap 4 -->
    <script src="../../plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
    <!-- AdminLTE App -->
    <script src="../../dist/js/adminlte.min.js"></script>
        <script>
            let slideIndex = 0;
            let textIndex = 0;
            showSlides();
            textSlides();

function showSlides() {
  let i;
  let slides = document.getElementsByClassName("mySlides");
  let dots = document.getElementsByClassName("dot");
  for (i = 0; i < slides.length; i++) {
    slides[i].style.display = "none";  
  }
  slideIndex++;
  if (slideIndex > slides.length) {slideIndex = 1}    
  for (i = 0; i < dots.length; i++) {
    dots[i].className = dots[i].className.replace(" active", "");
  }
  slides[slideIndex-1].style.display = "block";  
  dots[slideIndex-1].className += " active";
  setTimeout(showSlides, 2000); // Change image every 2 seconds
            }
 function textSlides() {
                let i;
                let slides = document.getElementsByClassName("mySlides1");
          
                for (i = 0; i < slides.length; i++) {
                    slides[i].style.display = "none";
                }
                textIndex++;
                if (textIndex > slides.length) { textIndex = 1 }
             
                slides[textIndex - 1].style.display = "block";
               
                setTimeout(textSlides, 2000); // Change image every 2 seconds
            }
        </script>
</body>
</html>
