<%@ page language="java" contentType="text/html; charset=BIG5"
    pageEncoding="BIG5"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=BIG5">
<%@include file="header.jsp" %>
<%@include file="validateAdmin.jsp"%>
<%@ page import="java.sql.*,model.*,controller.*" %>
<title>Announcement</title>
<script>
function validate(){
	var x = document.forms["announcementForm"]["announcement"].value;
	if(x == null || x== ""){
		alert("Input cannot be empty!");
		return false;
	}
	if(x.indexOf("<")!=-1 || x.indexOf(">")!=-1){
		alert("Accouncement cannot contain tags!");
		return false;		
	}
	if(x.indexOf("#")!=-1){
		alert("Accouncement cannot contain pound!");
		return false;		
	}
	if(x.indexOf("\"")!=-1){
		alert("Accouncement cannot contain double quotes! Use single quotes instead.");
		return false;		
	}
}

function updatevalidate(){
	var x = document.forms["updateannouncement"]["announcement"].value;
	if(x == null || x== ""){
		alert("Input cannot be empty!");
		return false;
	}
	if(x.indexOf("<")!=-1 || x.indexOf(">")!=-1){
		alert("Accouncement cannot contain tags!");
		return false;		
	}
	if(x.indexOf("#")!=-1){
		alert("Accouncement cannot contain pound!");
		return false;		
	}
	if(x.indexOf("\"")!=-1){
		alert("Accouncement cannot contain double quotes! Use single quotes instead.");
		return false;		
	}
}
</script>
</head>
<body>
<%
String update = request.getParameter("currentvalue");
String id=request.getParameter("id");

if(update!=null){
	%>
	<script type="text/javascript">
    $(window).on('load',function(){
        $('#myModal').modal('show');
    });
	</script>
	<%
}

%>
<div class="container">
<a href="adminIndex.jsp" class="logout">Back</a>
<div class="text-center">
	<br></br>
	<img src="image/announce.png" width=12%; height=12%/>
	<h2>Send Announcement</h2>
</div>



  <p>Send Announcement to all users:</p>
  <form action="announceServlet" name ="announcementForm" method="post" onsubmit="return validate()">
    <div class="form-group">
      <label for="comment">Announcement:</label>
      <textarea class="form-control" rows="5" name="announcement" placeholder="Enter announcement here"></textarea>
    </div>
      <button type="submit" value="Send Announcement" class="btn btn-default">Submit</button>
  </form>



<div class="col-sm-16">
  <table class="table table-hover">
    <thead>
      <tr>
        <th><h2>Past Announcements</h2></th><th>Action</th>
      </tr>
    </thead>
    <tbody>
<%

Connection conn =DBConn.getConnection();

String sql1="select * from t_notification";
PreparedStatement pstmt1 = conn.prepareStatement(sql1);
ResultSet rs1 = pstmt1.executeQuery();

      
    	  while (rs1.next()) {
  %>
  		<tr>
  			<%String message= rs1.getString("c_message");%>
          	<td><%= message%></td>
          	<td>
          	<table>
          	<form method="post" action="adminAnnounce.jsp" >
          	<input type="hidden" name="currentvalue" value="<%=message%>">
          	<input type= "hidden" name="id" value="<%=rs1.getInt("c_id")%>">
          	<input type= "submit" name="btnSubmit" id="button4" class="btn btn-default" value="Update" >
          	</form>
          	<form action = "announceServlet" method="POST">
			<input type= "submit" name="btnSubmit" id="button4" class="btn btn-default" value="Delete" >
			<input type= "hidden" name="id" id="button4" value="<%=rs1.getInt("c_id")%>">
			</form>
			</table>
			</td>
      	</tr>
      	
        
  <%    }
  %>
  </tbody>
  </table>
  </div>
  </div>
  <%if(update!=null){ 
  String title = "Update Announcement";%>
 <div class="container">
  <!-- Modal -->
   <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%=title%></h4>
        </div>
        <div class="modal-body">   
  <form action="announceServlet" name="updateannouncement" method="post" onsubmit="return updatevalidate()">
	Announcement:
	<textarea class="form-control" rows="5" name="announcement"><%=update%></textarea>
	<input type="hidden" class="form-control" name="id" value="<%=id%>">
	<input type= "submit" name="btnSubmit" class="btn btn-default" value="Update"> 
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
</body>
</html>