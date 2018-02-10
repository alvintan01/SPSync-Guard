<%@ page language="java" contentType="text/html; charset=BIG5"
    pageEncoding="BIG5"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=BIG5">
<title>Ransomware Files & Extensions</title>
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
function validatefile(){
	var x = document.forms["fileForm"]["filename"].value.toLowerCase();
	var y = document.forms["fileForm"]["info"].value.toLowerCase();
	if(x == null || x== ""){
		alert("File name cannot be empty!");
		return false;
	}
	if(x.indexOf("<")!=-1 || x.indexOf(">")!=-1){
		alert("File name cannot contain tags!");
		return false;		
	}
	if(x.indexOf("#")!=-1){
		alert("File name cannot contain pound!");
		return false;		
	}
	if(x.indexOf("\"")!=-1){
		alert("File name cannot contain double quotes! Use single quotes instead.");
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

function validateextension(){
	var x = document.forms["extensionForm"]["extension"].value.toLowerCase();
	var y = document.forms["extensionForm"]["info"].value.toLowerCase();
	if(x == null || x== ""){
		alert("Extension cannot be empty!");
		return false;
	}
	if(x.indexOf("<")!=-1 || x.indexOf(">")!=-1){
		alert("Extension cannot contain tags!");
		return false;		
	}
	if(x.indexOf("#")!=-1){
		alert("Extension cannot contain pound!");
		return false;		
	}
	if(x.indexOf("\"")!=-1){
		alert("Extension cannot contain double quotes! Use single quotes instead.");
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
<h2>Ransomware Files & Extensions</h2>
<%
String operation = request.getParameter("operation");
String table = request.getParameter("table");
String currentvalue = request.getParameter("currentvalue");
String infos=request.getParameter("Info");
String id=request.getParameter("currentid");

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
        <th>Ransomware File</th>
         <th>Action   <table>
          <form action="adminFileList.jsp" method="post">
    			<input type="hidden" name="operation" value="Add"/>
    			<input type="hidden" name="table" value="Ransomware File"/>
    			<input type="submit" class="btn btn-default" value="Add" /> 
		   </form>
		   </table>
          </th>
      </tr>
    </thead>
    <tbody>
<%

Connection conn =DBConn.getConnection();

String sql1="select * from t_ransomwarefile order by c_file";
PreparedStatement pstmt1 = conn.prepareStatement(sql1);
ResultSet rs1 = pstmt1.executeQuery();

String sql3="select * from t_ransomwareextension order by c_extension";
PreparedStatement pstmt3 = conn.prepareStatement(sql3);
ResultSet rs3 = pstmt3.executeQuery();

      
    	  while (rs1.next()) {
  %>
  		<tr>
  			<%String filename= rs1.getString("c_file");
  			String info=rs1.getString("c_info");
  			%>
          	<td><%= filename%></td>
          	<td>
          	<table>
          	<form method="post" action="adminFileList.jsp" >
          	<input type="hidden" name="operation" value="Update"/>
    		<input type="hidden" name="table" value="Ransomware File"/>
  			<input type="hidden" name="currentvalue" value="<%=filename%>" />
  			<input type="hidden" name="currentid" value="<%=rs1.getInt("c_id")%>" />
  			<input type="hidden" name="Info" value="<%=info%>" />
  			<input type="submit" id ="button4" name="Update" class="btn btn-default" value="Update" /> 
			</form> 
			<form action = "fileServlet" method="POST">
			<input type= "submit" name="btnSubmit" id="button4" class="btn btn-default" value="Delete" >
			<input type= "hidden" name="hiddenID" id="button4" value="<%=rs1.getString("c_id")%>">
			</form>
			<%if(!rs1.getString("c_info").equals("")){ %>
			<form action="adminFileList.jsp" method="post" >
			<input type="hidden" name="operation" value="Info"/>
			<input type="hidden" name="table" value="Ransomware Filet"/>
			<input type="hidden" name="currentvalue" value="<%=filename%>" />
			<input type="hidden" name="currentid" value="<%=rs1.getInt("c_id")%>" />
			<label for="Filebutton<%=rs1.getInt("c_id")%>" class="btn"><span class="glyphicon glyphicon-info-sign"></span></label>
			<input type="hidden" name="Info" value="<%=info%>" />
			<input type= "submit" id="Filebutton<%=rs1.getInt("c_id")%>" name="btnSubmit" value="Info" class="hidden">
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
        <th>Ransomware Extension</th>
          <th>Action   <table>
          <form action="adminFileList.jsp" method="post">
    			<input type="hidden" name="operation" value="Add"/>
    			<input type="hidden" name="table" value="Ransomware Extension"/>
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
          <%String extension= rs3.getString("c_extension");
          String info=rs3.getString("c_info");
          %>
            <td><%= extension %></td>
            <td>    		
          	<table>	
			<form method="post" action="adminFileList.jsp">
          	<input type="hidden" name="operation" value="Update"/>
    		<input type="hidden" name="table" value="Ransomware Extension"/>
  			<input type="hidden" name="currentvalue" value="<%=extension%>" />
  			<input type="hidden" name="currentid" value="<%=rs3.getInt("c_id")%>" />
  			<input type="hidden" name="Info" value="<%=info%>" />
  			<input type="submit" id ="button4" name="Update" class="btn btn-default" value="Update" /> 
			</form> 
			<form action = "extensionServlet" method="POST">
			<input type= "submit" name="btnSubmit" id="button4" class="btn btn-default" value="Delete" >
			<input type= "hidden" name="hiddenID" id="button4" value="<%=rs3.getString("c_id")%>">
			</form>
			<%if(!rs3.getString("c_info").equals("")){ %>
			<form action="adminFileList.jsp" method="post" >
			<input type="hidden" name="operation" value="Info"/>
			<input type="hidden" name="table" value="Black List"/>
			<input type="hidden" name="currentvalue" value="<%=extension%>" />
			<input type="hidden" name="currentid" value="<%=rs3.getInt("c_id")%>" />
			<label for="Extbutton<%=rs3.getInt("c_id")%>" class="btn"><span class="glyphicon glyphicon-info-sign"></span></label>
			<input type="hidden" name="Info" value="<%=info%>" />
			<input type= "submit" id="Extbutton<%=rs3.getInt("c_id")%>" name="btnSubmit" value="Info" class="hidden">
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
  <%if(!operation.equals("Info")){
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
        
        <%if(table.equals("Ransomware File")){ %> 

        <form action="fileServlet" method="post" name="fileForm" onsubmit="return validatefile()">
        	<%if(operation.equals("Update")){ %>
        	File Name:
			<input type="text" class="form-control" name="filename" value="<%=currentvalue%>" placeholder="File name">
			Info:
			<textarea class="form-control" name="info" cols="40" rows="3" placeholder="Info" ><%=(infos==null) ? "" : infos%></textarea>
			<input type="hidden" class="form-control" name="originalid" value="<%=id%>">
			<%} else{ %>
			File Name:
			<input type="text" class="form-control" name="filename" placeholder="File name">
			Info:
			<textarea class="form-control" name="info" cols="40" rows="3" placeholder="Info" ></textarea>
			<%} %>
			<input type= "submit" name="btnSubmit" class="btn btn-default" value="<%=operation%>" >
		</form>
		<%} else{ %>
		     <form action="extensionServlet" method="post" name="extensionForm" onsubmit="return validateextension()">
	        	<%if(operation.equals("Update")){ %>
	        	Extension Name:
				<input type="text" class="form-control" name="extension" value="<%=currentvalue%>" placeholder="Extension name">
				Info:
				<textarea class="form-control" name="info" cols="40" rows="3" placeholder="Info" ><%=(infos==null) ? "" : infos%></textarea>
				<input type="hidden" class="form-control" name="originalid" value="<%=id%>">
				<input type= "submit" name="btnSubmit" class="btn btn-default" value="<%=operation%>" >
				<%} else{ %>
				Extension Name:
				<input type="text" class="form-control" name="extension" placeholder="Extension name">
				Info:
				<textarea class="form-control" name="info" cols="40" rows="3" placeholder="Info" ></textarea>
				<input type= "submit" name="btnSubmit" class="btn btn-default" value="<%=operation%>" >
				<%} %>
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