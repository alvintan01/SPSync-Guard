<%@ page language="java" contentType="text/html; charset=BIG5"
    pageEncoding="BIG5"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=BIG5">
<title>Password Recovery</title>
<%@include  file="header.jsp" %>
</head>
<body>
<div class="container">
<%
String message = (String)request.getAttribute("message");
if(message!=null){	
	%>
	<script>
	alert("<%=message%>");
	</script>
<%
}
%>
<h1>Forgot Password</h1>
<form action="forgotPassword" method="POST">
Enter your email:
<input type= "email" class="form-control"  name="email" placeholder="example@email.com">
<input type="submit" class="btn btn-default" value="Submit" /> 
</form>
</div>
</body>
</html>