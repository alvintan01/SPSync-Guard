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

import org.json.JSONArray;
import org.json.JSONObject;


/**
 * Servlet implementation class getNotification
 */
@WebServlet("/getNotification")
public class getNotification extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public getNotification() {
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
					"select * from t_notification where c_timestamp > '"+timestamp+"'");
				ResultSet rs=pstmt.executeQuery();
				PrintWriter out = response.getWriter();
				while(rs.next()){
					JSONObject json = new JSONObject();
					json.put("ID", rs.getInt("c_id"));
					json.put("Message", rs.getString("c_message"));
					json.put("TimeStamp", rs.getString("c_messagetimestamp"));
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
