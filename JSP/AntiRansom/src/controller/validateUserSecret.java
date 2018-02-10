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
 * Servlet implementation class validateUserSecret
 */
@WebServlet("/validateUserSecret")
public class validateUserSecret extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public validateUserSecret() {
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
		String hash= request.getParameter("hash");
		String sessionkey= request.getParameter("sessionkey");
		String auth= request.getParameter("auth");
		PrintWriter out= response.getWriter();
		
		if(MemberUtility.authenticate(sessionkey,auth)){
			try{			
				Connection conn =DBConn.getConnection();
				PreparedStatement pstmt = conn.prepareStatement(
					"select * from t_users where BINARY c_username =? and c_hash=?");
				pstmt.setString(1, username);
				pstmt.setString(2, hash);
				ResultSet rs = pstmt.executeQuery();
				if(rs.next()){
					out.println("Success");
				}else{
					out.println("Failed");
				}
				conn.close();
				
				}catch(Exception ex){
					System.err.println(ex.getMessage());
				}	
			}
		}

}
