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
import model.MemberUtility;
import model.SecretKey;

/**
 * Servlet implementation class getWhitelist
 */
@WebServlet("/getTrustedProcess")
public class getTrustedProcess extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public getTrustedProcess() {
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
		JSONArray jArray = new JSONArray();
		String timestamp= request.getParameter("timestamp");
		String sessionkey= request.getParameter("sessionkey");
		String auth= request.getParameter("auth");
		
		if(MemberUtility.authenticate(sessionkey,auth)){
			try{			
				Connection conn =DBConn.getConnection();
				PreparedStatement pstmt = conn.prepareStatement(
					"select * from t_trustedprocess where c_timestamp > '"+timestamp+"'");
				ResultSet rs=pstmt.executeQuery();
				PrintWriter out = response.getWriter();
				if(rs.next()){
					PreparedStatement pstmt2 = conn.prepareStatement(
						"select * from t_trustedprocess");
					ResultSet rs2=pstmt2.executeQuery();
					while(rs2.next()){
						JSONObject json = new JSONObject();
						json.put("ID", rs2.getInt("c_id"));
						json.put("ProcessName", rs2.getString("c_processname"));
						jArray.put(json);		
					}
					out.println(jArray.toString());
				}else{
					out.println("No changes");
				}
				conn.close();
				
				}catch(Exception ex){
					System.err.println(ex.getMessage());
				}	
			}
		}

}
