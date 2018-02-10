<%@ page language="java" contentType="text/html; charset=BIG5"
    pageEncoding="BIG5"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=BIG5">
<%@include file="header.jsp" %>
<%@include file="validateAdmin.jsp"%>
<%@ page import="java.sql.*,model.*,controller.*" %>
<title>Unlock User Accounts</title>
</head>
<body>
<div class="container">
<a href="adminIndex.jsp" class="logout">Back</a>
  <h1>Unlock User Accounts</h1>
  <table class="table table-hover">
  <thead><tr><th>Username</th><th>Action</th></tr></thead>
  <%
	ResultSet rs = MemberUtility.getLockedUsers();
    while(rs.next()){
    	%>
    	<tr>
    	<td>
    	<%=rs.getString("c_username") %>
    	</td>
    	<td>
    	<form action="unlockAccount" method="post">
    	<input type="hidden" name="username" value="<%=rs.getString("c_username")%>">
    	<input type="submit" class="btn btn-default" value="Unlock">
    	</form>
    	</td>
    	</tr>
    	<%
    }
  %>
  </table>
 
 </div>
</body>
</html>