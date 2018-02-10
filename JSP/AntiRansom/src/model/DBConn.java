package model;
import java.sql.Connection;
import java.sql.DriverManager;

public class DBConn {
	public static Connection getConnection() throws Exception{
		Class.forName("com.mysql.jdbc.Driver");
		String connURL = "jdbc:mysql://127.0.0.1:3306/webapp?user=webapp&password=1qwer$#@!";
		//String connURL = "jdbc:mysql://127.0.0.1/assignment2?user=root&password=s9812063g";
		return DriverManager.getConnection(connURL);
	}
}
