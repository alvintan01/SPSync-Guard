<%@ page language="java" contentType="text/html; charset=BIG5"
    pageEncoding="BIG5"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=BIG5">
<title>Admin Menu</title>
<%@ page import="java.sql.*,model.*,controller.*" %>
<%@include  file="header.jsp" %>
<%@include file="validateAdmin.jsp"%>
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
</head>
<body>
<div class="container">
	<div class="row">
		<div class="col-sm-3">	
			<br>
				<div class="text-center">		
				<a href="adminAnnounce.jsp"><img src="image/announce.png" width=30%; height=30%/> 
				<h2>Send Announcements</h2></a>
		</div>
	</div>	
		
		<div class="col-sm-3">
			<br>
			<div class="text-center">		
				<a href="adminFileList.jsp"><img src="image/adddelFE.png" width=30%; height=30%/>
				<h2>Add/Delete/Update</h2>
				<h3>File names & extensions  </h3></a>
			</div>
		</div>
		
		<div class="col-sm-3">
			<br>			
			<div class="text-center">		
				<a href="adminTaskList.jsp"><img src="image/adddelWB.png" width=30%; height=30%/>
				<h2>Add/Delete/Update</h2>
				<h3>Whitelist & Blacklist processes  </h3></a>
			</div>
		</div>
		
		<div class="col-sm-3">
			<br>		
			<div class="text-center">			
				<a href="adminApplicationList.jsp"><img src="image/adddelWB.png" width=30%; height=30%/>
				<h2>View User Processes</h2></a>
			</div>
			</div>
		</div>
		<div class="row">
		<div class="col-sm-3">
			<br>		
			<div class="text-center">			
				<a href="unlockAccount.jsp"><img src="image/unlock.png" width=30%; height=30%/>
				<h2>Unlock User Account</h2></a>
			</div>
		</div>
		<div class="col-sm-3">
			<br>		
			<div class="text-center">			
				<a href="adminRoom.jsp"><img src="image/room.png" width=30%; height=30%/>
				<h2>Room Settings</h2></a>
			</div>
		</div>
	</div>
</div>
</body>
</html>