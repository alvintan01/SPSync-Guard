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
 * Servlet implementation class extensionServlet
 */
@WebServlet("/extensionServlet")
public class extensionServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public extensionServlet() {
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
				String extenadd = request.getParameter("extension");
				String infoadd = request.getParameter("info");
				if (extenadd != null && !extenadd.isEmpty()) {
					  
					Connection conn =DBConn.getConnection();
					PreparedStatement pstmt = conn.prepareStatement(
							"Select * from t_ransomwareextension where c_extension=?");
					pstmt.setString(1, extenadd);
					ResultSet rs=pstmt.executeQuery();
					if(!rs.next()){	
						PreparedStatement pstmt2 = conn.prepareStatement(
							"Insert into t_ransomwareextension (c_extension, c_info) values(?,?)");
						pstmt2.setString(1, extenadd);
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
				String extenup = request.getParameter("extension");
				String infoup = request.getParameter("info");
				if (extenup != null && !extenup.isEmpty()) {
				String originId = request.getParameter("originalid");
				Connection conn =DBConn.getConnection();
				PreparedStatement pstmt = conn.prepareStatement(
					"update t_ransomwareextension set c_extension=?,c_info=?, c_timestamp=now() where c_id=?");
				pstmt.setString(1, extenup);
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
					"delete from t_ransomwareextension where c_id=?");
				pstmt.setString(1, delName);
			
				pstmt.executeUpdate();
				PreparedStatement pstmt2 = conn.prepareStatement(
						"update t_ransomwareextension set c_timestamp=now()");
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
