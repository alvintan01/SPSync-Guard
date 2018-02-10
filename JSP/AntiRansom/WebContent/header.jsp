<%@ page language="java" contentType="text/html; charset=BIG5"
    pageEncoding="BIG5"%>
<!DOCTYPE html>
<html>
<head>
<meta charset="BIG5">
<link rel="shortcut icon" href="sync.ico" />
<script src="jquery.js"></script>
<script type="text/javascript">
$(document).ready(function() {
    $("body").css("display", "none");
 
    $("body").fadeIn(500);
 
    $("a.transition").click(function(event){
        event.preventDefault();
        linkLocation = this.href;
        $("body").fadeOut(500, redirectPage);      
    });
         
    function redirectPage() {
        window.location = linkLocation;
    }
});
</script>
<script src="bootstrap.js"></script>
<link rel='stylesheet' href='css/bootstrap.css' type='text/css'/>
  <style>
    /* Remove the navbar's default margin-bottom and rounded borders */ 
    .navbar {
      margin-bottom: 0;
      border-radius: 0;
    }
    
    /* Set height of the grid so .sidenav can be 100% (adjust as needed) */
    .row.content {height: 450px}
    
    /* Set gray background color and 100% height */
    .sidenav {
      padding-top: 20px;
      background-color: #f1f1f1;
      height: 100%;
    }
    
    /* Set black background color, white text and some padding */
    footer {
      background-color: #555;
      color: white;
      padding: 15px;
    }
    
    /* On small screens, set height to 'auto' for sidenav and grid */
    @media screen and (max-width: 767px) {
      .sidenav {
        height: auto;
        padding: 15px;
      }
      .row.content {height:auto;} 
    }
  </style>
</head>
<body>
<nav class="navbar navbar-default">
  <div class="container-fluid">
    <!-- Brand and toggle get grouped for better mobile display -->
    <div class="navbar-header">
      <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
        <span class="sr-only">Toggle navigation</span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
      </button>
      <a class="navbar-brand" href="index.jsp">SyncUI</a>
      <a class="navbar-brand">|</a>
      <a class="navbar-brand" href="viewRfile.jsp">Ransomware Threats</a>
      <a class="navbar-brand">|</a>
      <a class="navbar-brand" href="downloadClient">Download Client</a>
    </div>

	
	<%String adminlogin=(String)session.getAttribute("admin");
      String userlogin=(String)session.getAttribute("user");
      
	  if(userlogin!=null || adminlogin!=null){
      %>
      <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
      <ul class="nav navbar-nav navbar-right">
        <li><a href="changePassword.jsp">Change Password</a></li>
        <li><a href="logout.jsp">Logout</a></li>
      </ul>
     </div><!-- /.navbar-collapse -->
	<% }%>
  </div><!-- /.container-fluid -->
</nav>
</body>
</html>