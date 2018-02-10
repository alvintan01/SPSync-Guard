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

/**
 * Servlet implementation class getRoom
 */
@WebServlet("/getRoom")
public class getRoom extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public getRoom() {
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
		String sessionkey= request.getParameter("sessionkey");
		String auth= request.getParameter("auth");
		PrintWriter out = response.getWriter();
		
		if(MemberUtility.authenticate(sessionkey,auth)){
			try{			
				Connection conn =DBConn.getConnection();
			
					PreparedStatement pstmt = conn.prepareStatement(
						"select * from t_rooms");
					ResultSet rs=pstmt.executeQuery();
					while(rs.next()){
						JSONObject json = new JSONObject();
						json.put("RoomID", rs.getInt("c_id"));
						json.put("RoomName", rs.getString("c_roomname"));
						json.put("IP", rs.getString("c_ip"));
						json.put("SSHfingerprint", rs.getString("c_sshfingerprint"));
						jArray.put(json);		
					}
					out.println(jArray.toString());
				
				conn.close();
				
				}catch(Exception ex){
					System.err.println(ex.getMessage());
				}	
		}
	}

}
