<%@ page language="java" contentType="text/html; charset=BIG5"
    pageEncoding="BIG5"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=BIG5">
<title>Process Blacklist & Whitelist</title>
<%@ page import="java.sql.*,model.*,controller.*" %>
<%@include  file="header.jsp" %>
<%@include file="validateAdmin.jsp"%>
<%
String message = (String)request.getAttribute("exist");
if(message!=null){	
	%>
	<script>
	alert("<%=message%>");
	</script>
<%
}
%>
<script>
function validatewhite(){
	var x = document.forms["whiteForm"]["whitelist"].value.toLowerCase();
	var y = document.forms["whiteForm"]["info"].value.toLowerCase();
	if(x == null || x== ""){
		alert("Process name cannot be empty!");
		return false;
	}
	if(x.indexOf("<")!=-1 || x.indexOf(">")!=-1){
		alert("Process name cannot contain tags!");
		return false;		
	}
	if(x.indexOf("#")!=-1){
		alert("Process name cannot contain pound!");
		return false;		
	}
	if(x.indexOf("\"")!=-1){
		alert("Process name cannot contain double quotes! Use single quotes instead.");
		return false;		
	}
	if(y.indexOf("<")!=-1 || y.indexOf(">")!=-1){
		alert("Info cannot contain tags!");
		return false;		
	}
	if(y.indexOf("#")!=-1){
		alert("Info cannot contain pound!");
		return false;		
	}
	if(y.indexOf("\"")!=-1){
		alert("Info cannot contain double quotes! Use single quotes instead.");
		return false;		
	}
	
}

function validateblack(){
	var x = document.forms["blackForm"]["blacklist"].value.toLowerCase();
	var y = document.forms["blackForm"]["info"].value.toLowerCase();;
	if(x == null || x== ""){
		alert("Process name cannot be empty!");
		return false;
	}
	if(x.indexOf("<")!=-1 || x.indexOf(">")!=-1){
		alert("Process name cannot contain tags!");
		return false;		
	}
	if(x.indexOf("#")!=-1){
		alert("Process name cannot contain pound!");
		return false;		
	}
	if(x.indexOf("\"")!=-1){
		alert("Process name cannot contain double quotes! Use single quotes instead.");
		return false;		
	}
	if(y.indexOf("<")!=-1 || y.indexOf(">")!=-1){
		alert("Info cannot contain tags!");
		return false;		
	}
	if(y.indexOf("#")!=-1){
		alert("Info cannot contain pound!");
		return false;		
	}
	if(y.indexOf("\"")!=-1){
		alert("Info cannot contain double quotes! Use single quotes instead.");
		return false;		
	}
}
</script>
</head>
<body>
<div class="container">
<a href="adminIndex.jsp" class="logout">Back</a>
<h2>Process Whitelist & Blacklist</h2>
<%
String operation = request.getParameter("operation");
String table = request.getParameter("table");
String currentvalue = request.getParameter("currentvalue");
String infos=request.getParameter("Info");
String id=request.getParameter("id");

if(operation!=null){
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
<div class="col-sm-6">
  <table class="table table-hover">
    <thead>
      <tr>
        <th>White List</th>
         <th>Action
         <table>
          <form action="adminTaskList.jsp" method="post">
    			<input type="hidden" name="operation" value="Add"/>
    			<input type="hidden" name="table" value="White List"/>
    			<input type="submit" class="btn btn-default" value="Add" /> 
		   </form>
		   </table>
          </th>
      </tr>
    </thead>
    <tbody>
<%

Connection conn =DBConn.getConnection();

String sql1="select * from t_trustedprocess order by c_processname";
PreparedStatement pstmt1 = conn.prepareStatement(sql1);
ResultSet rs1 = pstmt1.executeQuery();

String sql3="select * from t_ransomprocess order by c_processname";
PreparedStatement pstmt3 = conn.prepareStatement(sql3);
ResultSet rs3 = pstmt3.executeQuery();

      
    	  while (rs1.next()) {
  %>
  		<tr>
  			<%String processname = rs1.getString("c_processname");
  			String info=rs1.getString("c_info");%>	
          	<td><%= processname%></td>
          	<td>
          	<table>
          	<form method="post" action="adminTaskList.jsp" >
          	<input type="hidden" name="operation" value="Update"/>
    		<input type="hidden" name="table" value="White List"/>
  			<input type="hidden" name="currentvalue" value="<%=processname%>" />
			<input type="hidden" name="Info" value="<%=info%>" />
			<input type="hidden" name="id" value="<%=rs1.getInt("c_id")%>" />
  			<input type="submit" id ="button4" name="Update" class="btn btn-default" value="Update" /> 
			</form> 

			
			<form action = "whitelistServlet" method="POST">
			<input type= "submit" name="btnSubmit" id="button4" class="btn btn-default" value="Delete" >
			<input type= "hidden" name="hiddenID" id="button4" value="<%=rs1.getInt("c_id")%>">
			</form>
			<%if(!rs1.getString("c_info").equals("")){ %>
			<form action="adminTaskList.jsp" method="post" >
			<input type="hidden" name="operation" value="Info"/>
			<input type="hidden" name="table" value="Black List"/>
			<input type="hidden" name="currentvalue" value="<%=processname%>" />
			<label for="Whitebutton<%=rs1.getInt("c_id")%>" class="btn"><span class="glyphicon glyphicon-info-sign"></span></label>
			<input type="hidden" name="Info" value="<%=info%>" />
			<input type="hidden" name="id" value="<%=rs1.getInt("c_id")%>" />
			<input type= "submit" id="Whitebutton<%=rs1.getInt("c_id")%>" name="btnSubmit" value="Info" class="hidden">
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
  <div class="col-sm-6">
  <table class="table table-hover">
    <thead>
      <tr>
        <th>Black List</th>
          <th>Action   <table>
          <form action="adminTaskList.jsp" method="post">
    			<input type="hidden" name="operation" value="Add"/>
    			<input type="hidden" name="table" value="Black List"/>
    			<input type="submit" class="btn btn-default" value="Add" /> 
		   </form>
          </table>
          </th>       
      </tr>
    </thead>
    <tbody>
  
  
<%
    	  while (rs3.next()) {
  %>
          <tr>
          <%String processname= rs3.getString("c_processname");
          String info=rs3.getString("c_info");
          %>
          
            <td><%= processname %></td>
            <td>    		
          	<table>	
			<form method="post" action="adminTaskList.jsp">
          	<input type="hidden" name="operation" value="Update"/>
    		<input type="hidden" name="table" value="Black List"/>
  			<input type="hidden" name="currentvalue" value="<%=processname%>" />
			<input type="hidden" name="Info" value="<%=info%>" />
			<input type="hidden" name="id" value="<%=rs3.getInt("c_id")%>" />
  			<input type="submit" id ="button4" name="Update" class="btn btn-default" value="Update" /> 
			</form> 
			<form action = "blacklistServlet" method="POST">
			<input type= "submit" name="btnSubmit" id="button4" class="btn btn-default" value="Delete" >
			<input type= "hidden" name="hiddenID" id="button4" value="<%=rs3.getInt("c_id")%>">
			</form>
			<%if(!rs3.getString("c_info").equals("")){ %>
			<form action="adminTaskList.jsp" method="post" >
			<input type="hidden" name="operation" value="Info"/>
			<input type="hidden" name="table" value="Black List"/>
			<input type="hidden" name="currentvalue" value="<%=processname%>" />
			<input type="hidden" name="id" value="<%=rs3.getInt("c_id")%>" />
			<label for="Blackbutton<%=rs3.getInt("c_id")%>" class="btn"><span class="glyphicon glyphicon-info-sign"></span></label>
			<input type="hidden" name="Info" value="<%=info%>" />
			<input type= "submit" id="Blackbutton<%=rs3.getInt("c_id")%>" name="btnSubmit" value="Info" class="hidden">
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
 <%if(operation!=null){ %>
 <div class="container">
  <!-- Modal -->
  <%
  if(!operation.equals("Info")){
	  String title = operation+" "+table + ((currentvalue==null) ? "" : " " + currentvalue);%>
  <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%=title%></h4>
        </div>
        <div class="modal-body">    
        
        <%if(table.equals("White List")){ %> 

        <form action="whitelistServlet" method="post" name="whiteForm" onsubmit="return validatewhite()">
        	<%if(operation.equals("Update")){ %>
        	Whitelist:
			<input type="text" class="form-control" name="whitelist" placeholder="Whitelist" value="<%=currentvalue%>">
			Info:
			<textarea class="form-control" name="info" cols="40" rows="3" placeholder="Info" ><%=(infos==null) ? "" : infos%></textarea>
			<input type="hidden" class="form-control" name="originalid" value="<%=id%>">
			<%} else{ %>
			Whitelist:
			<input type="text" class="form-control" name="whitelist" placeholder="Whitelist">
			Info:
			<textarea class="form-control" name="info" cols="40" rows="3" placeholder="Info" ></textarea>
			<%} %>
			<input type= "submit" name="btnSubmit" class="btn btn-default" value="<%=operation%>" >
		</form>
		<%} else{ %>
		     <form action="blacklistServlet" method="post" name="blackForm" onsubmit="return validateblack()">
	        	<%if(operation.equals("Update")){ %>
				Blacklist:
				<input type="text" class="form-control" name="blacklist" placeholder="Blacklist" value="<%=currentvalue%>">
				Info:
				<textarea class="form-control" name="info" cols="40" rows="3" placeholder="Info" ><%=(infos==null) ? "" : infos%></textarea>
				<input type="hidden" class="form-control" name="originalid" value="<%=id%>">
				<%}else{ %>
				Blacklist:
				<input type="text" class="form-control" name="blacklist" placeholder="Blacklist">
				Info:
				<textarea class="form-control" name="info" cols="40" rows="3" placeholder="Info" ></textarea>
				<%} %>
			<input type= "submit" name="btnSubmit" class="btn btn-default" value="<%=operation%>" >
		    </form>
		<%} %>
        </div>
        <% 
        }else{
        	String title = currentvalue+" "+operation;%>
        	  <div class="modal fade" id="myModal" role="dialog">
        	    <div class="modal-dialog">
        	      <!-- Modal content-->
        	      <div class="modal-content">
        	        <div class="modal-header">
        	          <button type="button" class="close" data-dismiss="modal">&times;</button>
        	          <h4 class="modal-title"><%=title%></h4>
        	        </div>
        	        <div class="modal-body">  
		        	<%=infos%>
				<%}
        %>
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