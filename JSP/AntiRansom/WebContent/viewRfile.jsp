<%@ page language="java" contentType="text/html; charset=BIG5"
    pageEncoding="BIG5"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=BIG5">
<title>Ransomware Threats</title>
<%@ page import="java.sql.*,model.*,controller.*" %>
<%@include  file="header.jsp" %>

</head>
<body>
<div class="container">
<h2>Ransomware Threats</h2>
<%
String value = request.getParameter("value");
String info = request.getParameter("info");
String lastupdatedtime = request.getParameter("timestamp");
if(lastupdatedtime!=null){
	lastupdatedtime = lastupdatedtime.substring(0, lastupdatedtime.length()-2);//to remove the miliseconds
}
if(value!=null){
	%>
	<script type="text/javascript">
    $(window).on('load',function(){
        $('#myModal').modal('show');
    });
	</script>
	<%
}

%>
<div class="row">
<div class="col-sm-4">
  <table class="table table-hover">
    <thead>
      <tr>
        <th>Ransomware File Name</th>
      </tr>
    </thead>
    <tbody>
<%

Connection conn =DBConn.getConnection();

String sql1="select * from t_ransomwarefile order by c_file";
PreparedStatement pstmt1 = conn.prepareStatement(sql1);
ResultSet rs1 = pstmt1.executeQuery();

      
    	  while (rs1.next()) {
  %>
  		<tr>
  			<%String filename= rs1.getString("c_file");
  			String infomation=rs1.getString("c_info");
  			String timestamp=rs1.getString("c_timestamp");
  			%>
  			<td>
  			<table>
          	<%=filename%>
          	<%if(!infomation.equals("") || infomation==null){ %>
          	<form action="viewRfile.jsp" method="post" >
			<input type="hidden" name="value" value="Ransomware File: <%=filename%>"/>
			<input type="hidden" name="info" value="<%=infomation%>"/>
			<input type="hidden" name="timestamp" value="<%=timestamp%>"/>
			<label for="button<%=filename%>" class="btn"><span class="glyphicon glyphicon-info-sign"></span></label>
			<input type= "submit" id="button<%=filename%>" name="btnSubmit" value="Info" class="hidden">
			</form>
			<%} %>
          	</table>
          	
          	</td>
      	</tr>
      	
        
  <%    }
  %>
  </tbody>
  </table>
  </div>
  <div class="col-sm-4">
  <table class="table table-hover">
    <thead>
      <tr>
        <th>Ransomware Extension</th>     
      </tr>
    </thead>
    <tbody>
  
  
<%
String sql2="select * from t_ransomwareextension order by c_extension";
PreparedStatement pstmt2 = conn.prepareStatement(sql2);
ResultSet rs2 = pstmt2.executeQuery();
    	  while (rs2.next()) {
  %>
          <tr>
          <%String extension= rs2.getString("c_extension");
          String infomation=rs2.getString("c_info");
          String timestamp=rs2.getString("c_timestamp");
          %>
            <td>
  			<table>
          	<%= extension%>
          	<%if(!infomation.equals("") || infomation==null){ %>
          	<form action="viewRfile.jsp" method="post" >
			<input type="hidden" name="value" value="Ransomware Extension: <%=extension%>"/>
			<input type="hidden" name="info" value="<%=infomation%>"/>
			<input type="hidden" name="timestamp" value="<%=timestamp%>"/>
			<label for="button<%=extension%>" class="btn"><span class="glyphicon glyphicon-info-sign"></span></label>
			<input type= "submit" id="button<%=extension%>" name="btnSubmit" value="Info" class="hidden">
			</form>
			<%} %>
          	</table>
          	
          	</td>		
          </tr>
  <%    }
  %>
 </tbody>
  </table>
 </div>
 <div class="col-sm-4">
  <table class="table table-hover">
    <thead>
      <tr>
        <th>Ransomware Processes</th>     
      </tr>
    </thead>
    <tbody>
  
  
<%
String sql3="select * from t_ransomprocess order by c_processname";
PreparedStatement pstmt3 = conn.prepareStatement(sql3);
ResultSet rs3 = pstmt3.executeQuery();
    	  while (rs3.next()) {
  %>
          <tr>
          <%String processname= rs3.getString("c_processname");
            String infomation=rs3.getString("c_info");
            String timestamp=rs3.getString("c_timestamp");
          %>
            <td>
  			<table>
          	<%= processname%>
          	<%if(!infomation.equals("") || infomation==null){ %>
          	<form action="viewRfile.jsp" method="post" >
			<input type="hidden" name="value" value="Ransomware Process: <%=processname%>"/>
			<input type="hidden" name="info" value="<%=infomation%>"/>
			<input type="hidden" name="timestamp" value="<%=timestamp%>"/>
			<label for="button<%=processname%>" class="btn"><span class="glyphicon glyphicon-info-sign"></span></label>
			<input type= "submit" id="button<%=processname%>" name="btnSubmit" value="Info" class="hidden">
			</form>
			<%} %>
          	</table>
          	
          	</td>			
          </tr>
  <%    }
   
       conn.close();
  %>
 </tbody>
  </table>
 </div>
 </div> 
 </div>
 <%if(value!=null){ 
 
 %>
  <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%=value%></h4>
        </div>
        <div class="modal-body">
          <p><%=info%></p>
        </div>
        <div class="modal-footer">
        Last updated on: <%=lastupdatedtime%>
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
      
    </div>
  </div>
  <%} %> 
</body>
</html>