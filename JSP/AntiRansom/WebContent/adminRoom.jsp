<%@ page language="java" contentType="text/html; charset=BIG5"
    pageEncoding="BIG5"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=BIG5">
<title>Room settings</title>
<%@ page import="java.sql.*,model.*,controller.*" %>
<%@include  file="header.jsp" %>
<%@include file="validateAdmin.jsp"%>
<script>
function validateRoom(){
	var x = document.forms["RoomForm"]["name"].value.toLowerCase();
	var y = document.forms["RoomForm"]["ip"].value.toLowerCase();
	var z = document.forms["RoomForm"]["sshfingerprint"].value.toUpperCase();
	
	if(x == null || x== ""){
		alert("Room name cannot be empty!");
		return false;
	}
	if(x.indexOf("<")!=-1 || x.indexOf(">")!=-1){
		alert("Room name cannot contain tags!");
		return false;		
	}
	if(x.indexOf("#")!=-1){
		alert("Room name cannot contain pound!");
		return false;		
	}
	if(y == null || y== ""){
		alert("IP cannot be empty!");
		return false;
	}
	if (!(/^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/).test(y))
	  {
		alert("Invalid IP address");
	    return false;
	  }
	if(z == null || z== ""){
		alert("SSH fingerprint cannot be empty!");
		return false;
	}
	if (!(/^((([A-F0-9]{2}):){15}[A-F0-9]{2})$/).test(z)){
		alert("Invalid SSH fingerprint!");
		return false;
	}

	
}
</script>
</head>
<body>
<div class="container">
<a href="adminIndex.jsp" class="logout">Back</a>
<h2>Room settings</h2>
<%
String operation = request.getParameter("operation");
String currentvalue = request.getParameter("currentvalue");
String currentvalue1 = request.getParameter("currentvalue1");
String currentvalue2 = request.getParameter("currentvalue2");
String currentid=request.getParameter("currentid");

if(operation!=null){
	%>
	<script type="text/javascript">
    $(window).on('load',function(){
        $('#myModal').modal('show');
    });
	</script>
	<%
}
if(currentvalue==null){//add operation
	currentvalue=" Room";
}

%>
<div class="row">
<div class="col-sm-8">
  <table class="table table-hover">
    <thead>
      <tr>
        <th>Rooms</th>
         <th>IP</th>
         <th>SSH fingerprint</th>
         <th>Action
         
         <table>
          <form action="adminRoom.jsp" method="post">
    			<input type="hidden" name="operation" value="Add"/>
    			<input type="submit" class="btn btn-default" value="Add" /> 
		   </form>
		   </table>
		</th>
      </tr>
    </thead>
    <tbody>
<%

Connection conn =DBConn.getConnection();

String sql1="select * from t_rooms";
PreparedStatement pstmt1 = conn.prepareStatement(sql1);
ResultSet rs1 = pstmt1.executeQuery();

      
    	  while (rs1.next()) {
  %>
  		<tr>
  			<%String roomname= rs1.getString("c_roomname");
  			String ipaddr=rs1.getString("c_ip");
  			String sshfingerprint=rs1.getString("c_sshfingerprint");
  			%>
          	<td><%=roomname%></td>
          	<td><%=ipaddr%></td>
          	<td><%=sshfingerprint%></td>
          	<td>
          	<table>
          	<form method="post" action="adminRoom.jsp" >
          	<input type="hidden" name="operation" value="Update"/>
  			<input type="hidden" name="currentvalue" value="<%=roomname%>" />
  			<input type="hidden" name="currentvalue1" value="<%=ipaddr%>" />
  			<input type="hidden" name="currentvalue2" value="<%=sshfingerprint%>" />
  			<input type="hidden" name="currentid" value="<%=rs1.getInt("c_id")%>" />
  			<input type="submit" id ="button4" name="Update" class="btn btn-default" value="Update" /> 
			</form> 
			<form action = "roomServlet" method="POST">
			<input type= "submit" name="btnSubmit" id="button4" class="btn btn-default" value="Delete" >
			<input type= "hidden" name="currentid" id="button4" value="<%=rs1.getString("c_id")%>">
			</form>
			</table>
			</td>
      		</tr>
			<%} %>
			
			

      	
        
  <%    
    conn.close();
  %>
  </tbody>
  </table>
  </div>
  <div class="col-sm-4"></div>
 </div> 

 <%if(operation!=null){ %>
 <div class="container">
  <!-- Modal -->
<% 
  String title = operation+" "+currentvalue ;%>
  <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%=title%></h4>
        </div>
        <div class="modal-body">    

        <form action="roomServlet" method="post" name="RoomForm" onsubmit="return validateRoom()">
        	<%if(operation.equals("Update")){ %>
        	Name:
			<input type="text" class="form-control" name="name" value="<%=currentvalue%>" placeholder="Room Name">
			IP:
			<input type="text" class="form-control" name="ip" value="<%=currentvalue1%>" placeholder="IP Address">
			SSH Fingerprint:
			<input type="text" class="form-control" name="sshfingerprint" value="<%=currentvalue2%>" placeholder="SSH Fingerprint">
			<input type="hidden" class="form-control" name="currentid" value="<%=currentid%>">
			<%} else{ %>
			Name:
			<input type="text" class="form-control" name="name"  placeholder="Room Name">
			IP:
			<input type="text" class="form-control" name="ip"  placeholder="IP Address">
			SSH fingerprint:
			<input type="text" class="form-control" name="sshfingerprint" placeholder="SSH Fingerprint">
			<%} %>
			<input type= "submit" name="btnSubmit" class="btn btn-default" value="<%=operation%>" >
		</form>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
      
    </div>
  </div>
</div>
<%} %>
 </div>
</body>
</html>

