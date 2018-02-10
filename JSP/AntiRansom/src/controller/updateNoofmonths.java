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

import model.DBConn;
import model.MemberUtility;
import model.SecretKey;

/**
 * Servlet implementation class updateNoofmonths
 */
@WebServlet("/updateNoofmonths")
public class updateNoofmonths extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public updateNoofmonths() {
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
		int numberofmonths=Integer.parseInt(request.getParameter("noofmonths"));
		String sessionkey= request.getParameter("sessionkey");
		String auth= request.getParameter("auth");
		
		if(MemberUtility.authenticate(sessionkey,auth)){
			try{			
				Connection conn =DBConn.getConnection();
				PreparedStatement pstmt = conn.prepareStatement(
					"update t_users set c_noofmonths =? where c_username=?");
				pstmt.setInt(1, numberofmonths);
				pstmt.setString(2, username);
				pstmt.executeUpdate();
				
				conn.close();
				
				}catch(Exception ex){
					System.err.println(ex.getMessage());
				}	
			}
	}

}
