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

/**
 * Servlet implementation class whitelistServlet
 */
@WebServlet("/whitelistServlet")
public class whitelistServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public whitelistServlet() {
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
		String choice = request.getParameter("btnSubmit");
		String type = request.getParameter("type");//to know which page to redirect
		Boolean exist = false;
		
		if (choice.equals("Add")){
			
			try{
				String whitelistadd = request.getParameter("whitelist");
				String infoadd = request.getParameter("info");
				if (whitelistadd != null && !whitelistadd.isEmpty()) {
					if(infoadd==null || infoadd.isEmpty()){
						infoadd="";
					}
					Connection conn =DBConn.getConnection();
					PreparedStatement pstmt = conn.prepareStatement(
							"Select * from t_trustedprocess where c_processname=?");
					pstmt.setString(1, whitelistadd);
					ResultSet rs=pstmt.executeQuery();
					if(!rs.next()){	
						PreparedStatement pstmt2 = conn.prepareStatement(
							"Insert into t_trustedprocess (c_processname, c_info) values(?,?)");
						pstmt2.setString(1, whitelistadd);
						pstmt2.setString(2, infoadd);
					
						pstmt2.executeUpdate();
						conn.close();
					}else{
						exist = true;
					}
				}
				}catch(Exception ex){
					System.err.println(ex.getMessage());
				}
				
		}
		else if (choice.equals("Update")){
			
			try{
				String processup = request.getParameter("whitelist");
				String infoup = request.getParameter("info");
				if (processup != null && !processup.isEmpty()) {
				String originId = request.getParameter("originalid");
				Connection conn =DBConn.getConnection();
				PreparedStatement pstmt = conn.prepareStatement(
					"Update t_trustedprocess set c_processname=?, c_info=?, c_timestamp=now() where c_id=?");
				pstmt.setString(1, processup);
				pstmt.setString(2, infoup);
				pstmt.setString(3, originId);

				pstmt.executeUpdate();
				
				conn.close();
				}
				}catch(Exception ex){
					System.err.println(ex.getMessage());
				}
		}
		else if (choice.equals("Delete")){
			
			try{
				String delName = request.getParameter("hiddenID");
				Connection conn =DBConn.getConnection();
	
				PreparedStatement pstmt = conn.prepareStatement(
					"delete from t_trustedprocess where c_id=?");
				pstmt.setString(1, delName);
			
				pstmt.executeUpdate();
				PreparedStatement pstmt2 = conn.prepareStatement(
						"update t_trustedprocess set c_timestamp=now()");
				pstmt2.executeUpdate();
				conn.close();
			}catch(Exception ex){
				System.err.println(ex.getMessage());
			}
		}
		if(type==null){
			if(exist){
				request.setAttribute("exist","Entry already exists.");
			}
			RequestDispatcher lm=request.getRequestDispatcher("adminTaskList.jsp");            
			lm.include(request, response); 
		}else{
			response.sendRedirect("adminApplicationList.jsp");  
		}
	}

}
