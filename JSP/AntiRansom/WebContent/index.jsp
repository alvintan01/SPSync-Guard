<%@ page language="java" contentType="text/html; charset=BIG5"
    pageEncoding="BIG5"%>
<!DOCTYPE html>
<html >
<head>
  <meta charset="UTF-8">
  <title>Login</title>
<%@ page import="java.sql.*,model.*,controller.*" %>
<%@include  file="header.jsp" %>
<%
String user=(String)session.getAttribute("user");
String admin = (String)session.getAttribute("admin");
if(user!=null){
	RequestDispatcher dd = request.getRequestDispatcher("userBrowse.jsp");
	dd.forward(request,response);
}else if(admin!=null){
	RequestDispatcher dd = request.getRequestDispatcher("adminIndex.jsp");
	dd.forward(request,response);
}
%>
</head>
<body>
<div class="container">
<div class="text-center">
<br><br>
<img src="sync.ico" width=150px; height=150px>
<h2>Login</h2>
</div>
<%String info=(String)request.getAttribute("information");
	if(info!=null){
		%>
		<script>
		alert("<%=info%>");
		</script>
		<%
	}
%>

  <form id="login" action="login" method="post">
    <div class="form-group row">
    <div class="col-xs-4"></div>
      <div class="col-xs-4">
      <label for="text">Username:</label>
      <input type="text" class="form-control" id="email" placeholder="username" name="loginID">
      </div>
    </div>
    <div class="form-group row">
    <div class="col-xs-4"></div>
      <div class="col-xs-4">
      <label for="pwd">Password:</label>
      <input type="password" class="form-control" id="pwd" placeholder="password" name="password">
      </div>
    </div>
    <div class="text-center">
    <button type="submit" class="btn btn-default">Login</button>
    </div>
  </form>
  <div class="text-center">
  <%
		String login_msg=(String)request.getAttribute("error");  
		if(login_msg!=null){
	%>
<p style="color: red; font-weight: bold;"> ! <%=login_msg%></p><%}%>
<a href="forgotPassword.jsp">Forgot Password?</a>
</div>
</div>

  <script src='http://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js'></script>

    <script src="js/index.js"></script>

</body>
</html>
