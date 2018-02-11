<%@ page language="java" contentType="text/html; charset=BIG5"
    pageEncoding="BIG5"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=BIG5">
<%@include  file="header.jsp" %>
<%@include file="validate.jsp"%>
<title>Directory Listing</title> 
<%@ page import="java.util.regex.*,java.io.File,java.util.ArrayList, java.util.Arrays" %>
<%@page import="java.text.SimpleDateFormat,java.util.Calendar, model.MemberUtility" %>

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
<%
Calendar cal = Calendar.getInstance();
SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm");
%>
<script>
function setDate() {
	var x = document.forms["datetimeform"]["date"];
    x.value = "<%=sdf.format(cal.getTime())%>";
}
window.onload = setDate;
</script>


<div class="container">
<%
String hardcodedpath = "/home/p1529364/attachments/";
String username = (String)session.getAttribute("user");

%>
<h2>View Files</h2>

<%
String downloadpath = request.getParameter("downloadpath");
if(downloadpath==null){
	downloadpath = (String)request.getAttribute("downloadpath");//this case is for no files to download and will redirect the user back to the page
}
	
String path = hardcodedpath+username;
String downloadfilepath = request.getParameter("downloadfilepath");
if (downloadpath!=null){
	%>
	<script type="text/javascript">
    $(window).on('load',function(){
        $('#myModal').modal('show');
    });
	</script>
	<%
	int index=downloadpath.lastIndexOf('/');
	path = downloadpath.substring(0,index);
}else if(downloadfilepath!=null){
	%>
	<script type="text/javascript">
    $(window).on('load',function(){
        $('#filedownload').modal('show');
    });
	</script>
	<%
	int index=downloadfilepath.lastIndexOf('/');
	path = downloadfilepath.substring(0,index);
}
String userpath = request.getParameter("path");
if(userpath!=null){
	path = request.getParameter("path");
}
String[] pathsplit = path.split("/");

String tmppath="";
	File folder = new File(path);
	File[] listOfFiles = null;
try{
listOfFiles = folder.listFiles();
Arrays.sort(listOfFiles);
}catch(Exception e){
	
}
if(listOfFiles==null){
	%><h2>You have not backed up any files yet.</h2>
	<%
}else{
%>
<form id="restoreall" method="post" action="userBrowse.jsp">
	<input type="hidden" name="downloadpath" value="<%=hardcodedpath%><%=username%>"/>
	<input type="hidden" name="path" value="<%=hardcodedpath%><%=username%>"/>
</form>	
<button type="submit" class="btn btn-default" form="restoreall"><i class="glyphicon glyphicon-download-alt"></i> Restore All</button>
<p></p>
<br>
<ul class = "breadcrumb">
	<%for (int i=1; i<pathsplit.length; i++){//start with 1 as the first element is always blank due the the starting /
		tmppath+="/"+pathsplit[i];
		if(i>3){//3 as /home/server/attachments/<user>/...
			if(i==4){pathsplit[i]="C";}//replace fake with C if UI
	%>
	<li>
		<%if(i!=pathsplit.length-1){%>
		<form id="breadcrumb<%=pathsplit[i]%>"  action="userBrowse.jsp" method="post" style="display:inline;">
    	<a href="javascript:;" onclick="document.getElementById('breadcrumb<%=pathsplit[i]%>').submit();"><%=pathsplit[i]%></a>
    	<input type="hidden" name="path" value="<%=tmppath%>" />
		</form>
		<%}else{%>
			<%=pathsplit[i]%>
		<%}%>
	</li>
	<%	}
	}
	%>
</ul>	    		
<table class="table" id="example">
    <thead>
      <tr>
        <th>Name</th>
        <th>Type</th>
        <th>Size (Bytes)</th>
        <th>Options</th>
      </tr>
    </thead>
    <tbody>
    <%
   
	ArrayList<String> additionalcopies = new ArrayList();
	ArrayList<File> additionalcopiespath = new ArrayList();
	for (int i = 0; i < listOfFiles.length; i++) {
	    if(listOfFiles[i].isFile()){
	        Pattern pattern = Pattern.compile("(.+) [0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9](?:(\\.[^.]+$)?)");
		Matcher matcher = pattern.matcher(listOfFiles[i].getName());
		String filename = "";
		String fileextension = "";
		while (matcher.find()) {
		    filename= matcher.group(1);//name
		    fileextension= ((matcher.group(2) == null) ? "" : matcher.group(2));//extension
		} 
	        if(additionalcopies.contains(filename+fileextension)){
	            additionalcopiespath.add(listOfFiles[i]);
	        }else{
		        additionalcopies.add(filename+fileextension);
		}
	    }
    }
    
    for (int i = 0; i < listOfFiles.length; i++) {%>
	    <tr>
	    	<%if(listOfFiles[i].isFile() && !additionalcopiespath.contains(listOfFiles[i])){
			Pattern pattern = Pattern.compile("(.+) [0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9](?:(\\.[^.]+$)?)");
			Matcher matcher = pattern.matcher(listOfFiles[i].getName());
			String fullfilename = "";
			while (matcher.find()) {
			    fullfilename = matcher.group(1) + ((matcher.group(2) == null) ? "" : matcher.group(2));
			} 
			
			%>
	    		<td><i class="glyphicon glyphicon glyphicon-file"></i><%=fullfilename%></td>
	    		<td>File</td>
	    		<td><%=listOfFiles[i].length() %></td>
	    		<td>
				<form id="<%=path+"/"+listOfFiles[i].getName().replaceAll("[^A-Za-z0-9\\-]", "")+"download"%>" action="userBrowse.jsp" method="post">
	    			<input type="hidden" name="downloadfilepath" value="<%=path+"/"+listOfFiles[i].getName()%>"/>
				</form>	
				<button type="submit" class="btn btn-default" form="<%=path+"/"+listOfFiles[i].getName().replaceAll("[^A-Za-z0-9\\-]", "")+"download"%>"><i class="glyphicon glyphicon-download-alt"></i> Download</button>
	 		</td>
	    	<%}
		if(listOfFiles[i].isDirectory()){%>
	    		<td><i class="glyphicon glyphicon-folder-close"></i>
	    		<form id="<%=path+"/"+listOfFiles[i].getName()%>"  action="userBrowse.jsp" method="post" style="display:inline">
    				<a href="javascript:;" onclick="document.getElementById('<%=path+"/"+listOfFiles[i].getName()%>').submit();"><%=listOfFiles[i].getName()%></a>
	    			<input type="hidden" name="path" value="<%=path+"/"+listOfFiles[i].getName()%>"/>
			</form>
	    		</td>
	    		<td>Folder</td>
	    		<td><%=MemberUtility.folderSize(listOfFiles[i])%></td>
	    		<td>
	    		<form id="<%=path+"/"+listOfFiles[i].getName()+"download"%>" method="post" action="userBrowse.jsp">
	    			<input type="hidden" name="downloadpath" value="<%=path+"/"+listOfFiles[i].getName()%>"/>
	    		</form>	
	    		<button type="submit" class="btn btn-default" form="<%=path+"/"+listOfFiles[i].getName()+"download"%>"><i class="glyphicon glyphicon-download-alt"></i> Download</button>
			</td>
	    	<%}%>
	 	</tr>
	 <%} %>
 	</tbody>
</table>
</div>

<div class="container">
  <!-- Modal -->
  <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
      <%String folderpath= request.getParameter("downloadpath"); 
      if(folderpath==null){
    	  folderpath = (String)request.getAttribute("downloadpath");//this case is for no files to download and will redirect the user back to the page
    	}
      String actualdirectorypath = "";
      if(folderpath!=null){
    	  actualdirectorypath = folderpath.replaceFirst("\\/home\\/p1529364\\/attachments\\/[^\\/]+\\/", "C:\\/");
    	  if (actualdirectorypath.startsWith("/home")){
        	  actualdirectorypath = folderpath.replaceFirst("\\/home\\/p1529364\\/attachments\\/[^\\/]+", "C:\\/");
    	  }
      }
      
      %>
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%=actualdirectorypath%></h4>
        </div>
        <div class="modal-body">
     
        <form action="zipServlet" method="post" name="datetimeform">
			<input type="datetime-local" max="9999-12-31T23:59" name="date">
			<input type="hidden" name="username" value="<%=username%>">
			<input type="hidden" name="folderpath" value="<%=folderpath%>">
			<input type="submit" value="submit" class="btn btn-default">
		</form>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
      
    </div>
  </div>
  
</div>


<div class="container">
  <!-- Modal -->
  <div class="modal fade" id="filedownload" role="dialog">
    <div class="modal-dialog">
      <%
      String filename="";
      String fileextension="";
      String actualfilepath = "";
      if(downloadfilepath!=null){
	  Pattern pattern = Pattern.compile("\\/home\\/p1529364\\/attachments\\/[^\\/]+(?:\\/(.+))?\\/(.+) [0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9](?:(\\.[^.]+$)?)");
	  Matcher matcher = pattern.matcher(downloadfilepath);
	  while (matcher.find()) {
		if(matcher.group(1)!=null){//null could happen for files under C root
		    actualfilepath = "C:/" + matcher.group(1) +"/"+ matcher.group(2) + ((matcher.group(3) == null) ? "" : matcher.group(3));
		}else{
		    actualfilepath = "C:/" + matcher.group(2) + ((matcher.group(3) == null) ? "" : matcher.group(3));
		}
                filename = matcher.group(2);
		fileextension = ((matcher.group(3) == null) ? "" : matcher.group(3));
	  } 
      }
      %>
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%=actualfilepath%></h4>
        </div>
        <div class="modal-body">
			<p>Available Files  <i class="glyphicon glyphicon-download-alt"></i></p>
			<%
			if(downloadfilepath!=null){
				int index=downloadfilepath.lastIndexOf('/');
				String rootpath=downloadfilepath.substring(0,index);
				java.io.File file;
				java.io.File dir = new java.io.File(rootpath);
				
				String[] list = dir.list();
				Arrays.sort(list);
				%>
			
				 <%if (list.length > 0) {
				for (int i = 0; i < list.length; i++) {
				file = new java.io.File(rootpath +'/'+ list[i]);
				if (file!= null && !file.isDirectory() && list[i].matches(filename.replaceAll("\\(","\\\\(").replaceAll("\\)","\\\\)")+" [0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9]"+fileextension) ) {
				%> 
							
				 <form action="DownloadFile" method="post">
				<input type="hidden" name="fileName" value="<%=list[i]%>">
				<input type="hidden" name="filePath" value="<%=rootpath%>">
				<input type="hidden" name="username" value="<%=username%>">
				 <div id="user">
				<input type="submit" name="download" class="btn btn-default" value="<%=list[i]%>"/></div></form>
				<%
				 }
				}
				} 
			}
			%> 

        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
      
    </div>
  </div>
  <%} %>
</div>
	
</body>
</html>