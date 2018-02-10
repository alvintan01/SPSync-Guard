<%@ page language="java" contentType="text/html; charset=BIG5"
    pageEncoding="BIG5"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=BIG5">
<title>User Processes</title>
<%@ page import="java.sql.*,model.*,controller.*" %>
<%@include  file="header.jsp" %>
<%@include file="validateAdmin.jsp"%>
</head>
<body>
<%
Connection conn =DBConn.getConnection();

String sql1="select * from t_userprocess order by c_approvedcount desc";
PreparedStatement pstmt1 = conn.prepareStatement(sql1);
ResultSet rs1 = pstmt1.executeQuery();


%>
<div class="container">
<a href="adminIndex.jsp" class="logout">Back</a>

  <h2>User Processes</h2>
  <p>Text in <strong>black</strong>: Processes that are not in white or black list. Text in <strong style="color: red;">red</strong>: Processes in the blacklist. Text in <strong style="color: blue;">blue</strong>: Processes in the white list.</p>            
  <table class="table table-hover">
    <thead>
      <tr>
        <th>Processname</th>
        <th>Approve Count</th>
        <th>Deny Count</th>
        <th>Action</th>
      </tr>
    </thead>
    <tbody>

<%
      
    	  while (rs1.next()) {
  %>
  
  		<tr>
  			<%String processname= rs1.getString("c_processname");%>
  			
  			<%
  			Boolean newprocess = true;
  			String sql2="select * from t_trustedprocess where c_processname=?";
  			PreparedStatement pstmt2 = conn.prepareStatement(sql2);
  			pstmt2.setString(1,processname);
  			ResultSet rs2 = pstmt2.executeQuery();
  			
  			if(rs2.next()){
  				%><td style="color: blue;"><%= processname%>	
					</td><%
					newprocess = false;
  			}else{
  			String sql3="select * from t_ransomprocess where c_processname=?";
  			PreparedStatement pstmt3 = conn.prepareStatement(sql3);
  			pstmt3.setString(1,processname);
  			ResultSet rs3 = pstmt3.executeQuery();
			
  			if(rs3.next()){
  				%><td style="color: red;"><%= processname%>	
					</td><%
					newprocess = false;
  			}
  			
  			else{%>
          	<td><%= processname%>	
			</td>
 			<%
  			}
  		  }%>
  			<%String approvedcount= rs1.getString("c_approvedcount");%>
          	<td><%= approvedcount%>	
			</td>
			<%String denycount= rs1.getString("c_denycount");%>
			<td><%=denycount%>
			</td>
			<td>
			<%if(newprocess){ %>
				<table>
				<form action="whitelistServlet" method="post">
				<input type="hidden" name="whitelist" value="<%=processname%>">
				<input type= "hidden" name="btnSubmit" value="Add" >
				<input type= "submit" name="type" class="btn btn-default" value="Approve" style="color: blue;">				
				</form>
				<form action="blacklistServlet" method="post">
				<input type="hidden" name="blacklist" value="<%=processname%>">
				<input type= "hidden" name="btnSubmit" class="btn btn-default" value="Add" >
				<input type= "submit" name="type" class="btn btn-default" value="Deny" style="color: red;">
				</form>
				</table>
			<%} %>
			</td>
      	</tr>
        
  <%    	
    	  }
  %>
    </tbody>
  </table>
</div>
 
 
  <%    
       conn.close();
  %>
</body>
</html>