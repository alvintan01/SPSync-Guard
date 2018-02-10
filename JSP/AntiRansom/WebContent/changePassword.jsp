<%@ page language="java" contentType="text/html; charset=BIG5"
    pageEncoding="BIG5"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=BIG5">
<title>Change Password</title>
<%@include  file="header.jsp" %>
<%
String user=(String)session.getAttribute("user");
String admin = (String)session.getAttribute("admin");
if(user==null && admin==null){
	RequestDispatcher dd = request.getRequestDispatcher("index.jsp");
	dd.forward(request,response);
}
%>
<script type="text/javascript">

function checkPasswordMatch() {
    var password = document.forms["changepassword"]["password"].value;
    var confirmPassword = document.forms["changepassword"]["password2"].value;

    if(password.length<8){
        $("#divCheckPasswordMatch").html("Password needs to be at least 8 characters long.");
        document.getElementById("divCheckPasswordMatch").style.color = "red";
        return false;
    }
    else if (password != confirmPassword){
        $("#divCheckPasswordMatch").html("Passwords do not match!");
        document.getElementById("divCheckPasswordMatch").style.color = "red";
        return false;
    }
    else{
        $("#divCheckPasswordMatch").html("Passwords match.");
        document.getElementById("divCheckPasswordMatch").style.color = "green";
        return true;
    }
}
</script>
</head>
<body>
<div class="container">
<form action="changePassword" method="post" id="changepassword" onsubmit="return checkPasswordMatch()">
<h1>Change Password</h1>
Enter your new password:
<input type= "password" class="form-control"  name="password" onkeyup="checkPasswordMatch();">
Renter your new password:
<input type= "password" class="form-control"  name="password2" onkeyup="checkPasswordMatch();">
<div id="divCheckPasswordMatch" style="font-weight: bold;"></div>
<input type="submit" class="btn btn-default" value="Change Password" /> 
</form>
</div>
</body>
</html>