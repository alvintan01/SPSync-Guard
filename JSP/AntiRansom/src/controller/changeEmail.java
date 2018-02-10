package controller;

import java.io.IOException;
import java.sql.Connection;
import java.sql.PreparedStatement;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import model.DBConn;
import model.MemberUtility;

/**
 * Servlet implementation class changeEmail
 */
@WebServlet("/changeEmail")
public class changeEmail extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public changeEmail() {
        super();
        // TODO Auto-generated constructor stub
    }

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
		response.getWriter().append("Served at: ").append(request.getContextPath());
	}

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
		String newusername= request.getParameter("newusername");
		String username= request.getParameter("username");
		String sessionkey= request.getParameter("sessionkey");
		String auth= request.getParameter("auth");
		
		if(MemberUtility.authenticate(sessionkey,auth)){
			try{			
				Connection conn =DBConn.getConnection();
				PreparedStatement pstmt = conn.prepareStatement(
					"Update t_users set c_username=? where c_username=?");
				
				pstmt.setString(1, newusername);
				pstmt.setString(2, username);
				
				pstmt.executeUpdate();
				
				conn.close();
				
				}catch(Exception ex){
					System.err.println(ex.getMessage());
				}	
			}
	}

}
