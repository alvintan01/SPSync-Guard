<%@ page language="java" contentType="text/html; charset=BIG5"
    pageEncoding="BIG5"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=BIG5">
<title>Insert title here</title>
<%@page import="java.text.SimpleDateFormat,java.util.Calendar" %>
</head>
<body>
<%
Calendar cal = Calendar.getInstance();
SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm");
%>
<script>
function setDate() {
	var x = document.forms["form"]["date"];
    x.value = "<%=sdf.format(cal.getTime())%>";
}
window.onload = setDate;
</script>


        <form action="zipServlet" method="post" name="form">
			<input type="datetime-local" name="date">
			</form>

</body>
</html>