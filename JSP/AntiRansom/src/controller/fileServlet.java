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
 * Servlet implementation class nameServlet
 */
@WebServlet("/fileServlet")
public class fileServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public fileServlet() {
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
		Boolean exist = false;
		
		if (choice.equals("Add")){
			
			try{
				String fileadd = request.getParameter("filename");
				String infoadd = request.getParameter("info");
				if (fileadd != null && !fileadd.isEmpty()) {					  
					Connection conn =DBConn.getConnection();
					PreparedStatement pstmt = conn.prepareStatement(
							"Select * from t_ransomwarefile where c_file=?");
					pstmt.setString(1, fileadd);
					ResultSet rs=pstmt.executeQuery();
					if(!rs.next()){	
						PreparedStatement pstmt2 = conn.prepareStatement(
							"Insert into t_ransomwarefile (c_file,c_info) values(?,?)");
						pstmt2.setString(1, fileadd);
						pstmt2.setString(2, infoadd);
					
						pstmt2.executeUpdate();
						conn.close();
					}else{
						exist = true;
					}
				}else {
					response.sendRedirect("adminFileList.jsp");
				}
				}catch(Exception ex){
					System.err.println(ex.getMessage());
				}
				
		}
		else if (choice.equals("Update")){
			
			try{
				String fileup = request.getParameter("filename");
				String infoup = request.getParameter("info");
				if (fileup != null && !fileup.isEmpty()) {
				String originId = request.getParameter("originalid");
				Connection conn =DBConn.getConnection();
				PreparedStatement pstmt = conn.prepareStatement(
						"update t_ransomwarefile set c_file=?,c_info=?, c_timestamp=now() where c_id=?");
				pstmt.setString(1, fileup);
				pstmt.setString(2, infoup);
				pstmt.setString(3, originId);
			
				pstmt.executeUpdate();
				
				conn.close();
				}else {
					response.sendRedirect("adminFileList.jsp");
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
					"delete from t_ransomwarefile where c_id=?");
				pstmt.setString(1, delName);
			
				pstmt.executeUpdate();
				PreparedStatement pstmt2 = conn.prepareStatement(
						"update t_ransomwarefile set c_timestamp=now()");
				pstmt2.executeUpdate();
				conn.close();
			}catch(Exception ex){
				System.err.println(ex.getMessage());
			}
		}
		if(exist){
			request.setAttribute("exist","Entry already exists.");
		}
		RequestDispatcher lm=request.getRequestDispatcher("adminFileList.jsp");            
		lm.include(request, response); 
		
	}

}
