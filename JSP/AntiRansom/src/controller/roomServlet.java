package controller;

import java.io.IOException;
import java.io.PrintWriter;
import java.sql.Connection;
import java.sql.PreparedStatement;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import model.DBConn;

/**
 * Servlet implementation class roomServlet
 */
@WebServlet("/roomServlet")
public class roomServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public roomServlet() {
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
		String choice = request.getParameter("btnSubmit");
		String id = request.getParameter("currentid");
		String roomname = request.getParameter("name");
		String ip = request.getParameter("ip");
		String sshfingerprint = request.getParameter("sshfingerprint");
		
		if (choice.equals("Add")){
			
			try{
				Connection conn =DBConn.getConnection();
				PreparedStatement pstmt = conn.prepareStatement(
						"Insert into t_rooms (c_roomname, c_ip, c_sshfingerprint) values(?,?,?)");
				pstmt.setString(1, roomname);
				pstmt.setString(2, ip);
				pstmt.setString(3, sshfingerprint);
				
				pstmt.executeUpdate();
				conn.close();

				}catch(Exception ex){
					System.err.println(ex.getMessage());
				}
				
		}
		else if (choice.equals("Update")){
			
			try{
				Connection conn =DBConn.getConnection();
				PreparedStatement pstmt = conn.prepareStatement(
					"Update t_rooms set c_roomname=?, c_ip=?,c_sshfingerprint=?,c_timestamp=now() where c_id=?");
				pstmt.setString(1, roomname);
				pstmt.setString(2, ip);
				pstmt.setString(3, sshfingerprint);
				pstmt.setString(4, id);
				
				pstmt.executeUpdate();
				
				conn.close();
				}catch(Exception ex){
					System.err.println(ex.getMessage());
				}
		}
		else if (choice.equals("Delete")){
			
			try{

				Connection conn =DBConn.getConnection();
	
				PreparedStatement pstmt = conn.prepareStatement(
					"delete from t_rooms where c_id=?");
				pstmt.setString(1, id);
			
				pstmt.executeUpdate();
				
				PreparedStatement pstmt2 = conn.prepareStatement(
						"update t_rooms set c_timestamp=now()");
				pstmt2.executeUpdate();
				conn.close();
			}catch(Exception ex){
				System.err.println(ex.getMessage());
			}
		}
		response.sendRedirect("adminRoom.jsp");  
	}

}
