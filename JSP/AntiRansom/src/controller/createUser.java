package controller;

import java.io.IOException;
import java.io.PrintWriter;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.json.JSONArray;
import org.json.JSONObject;

import model.DBConn;
import model.SecretKey;

/**
 * Servlet implementation class createUser
 */
@WebServlet("/createUser")
public class createUser extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public createUser() {
        super();
        // TODO Auto-generated constructor stub
    }

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
		RequestDispatcher lm=request.getRequestDispatcher("index.jsp");
		lm.include(request, response);
	}

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
		String username= request.getParameter("username");
		String password= request.getParameter("password");
		String hash= request.getParameter("hash");
		String key= request.getParameter("key");
		String salt= request.getParameter("salt");
		String roomname= request.getParameter("roomname");

		int numberofmonths=Integer.parseInt(request.getParameter("noofmonths"));
		PrintWriter out= response.getWriter();
		if(key.equals(SecretKey.getKey())){
			try{			
				Connection conn =DBConn.getConnection();
				PreparedStatement pstmt = conn.prepareStatement(
					"select * from t_users where c_username = ?");
				pstmt.setString(1, username);
				ResultSet rs = pstmt.executeQuery();
				if(rs.next()){
					out.println("User "+rs.getString("c_username")+" already exists Salt:" + rs.getString("c_salt"));
				}else{
					PreparedStatement pstmt2 = conn.prepareStatement(
							"insert into t_users (c_username, c_password, c_role, c_noofmonths, c_salt, c_hash, c_roomname) values(?,?,'user',?,?,?,?)");
						pstmt2.setString(1, username);
						pstmt2.setString(2, password);
						pstmt2.setInt(3, numberofmonths);
						pstmt2.setString(4, salt);
						pstmt2.setString(5, hash);
						pstmt2.setString(6, roomname);
						pstmt2.executeUpdate();
						out.println("User created");
				}
				
				conn.close();
				
				}catch(Exception ex){
					System.err.println(ex.getMessage());
				}	
			}
		}

}
