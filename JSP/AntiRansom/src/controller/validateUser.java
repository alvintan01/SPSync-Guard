package controller;

import java.io.IOException;
import java.io.PrintWriter;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.util.Properties;

import javax.mail.Message;
import javax.mail.MessagingException;
import javax.mail.PasswordAuthentication;
import javax.mail.Session;
import javax.mail.Transport;
import javax.mail.internet.InternetAddress;
import javax.mail.internet.MimeMessage;
import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import model.DBConn;
import model.MemberUtility;
import model.SecretKey;

/**
 * Servlet implementation class validateUser
 */
@WebServlet("/validateUser")
public class validateUser extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public validateUser() {
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
		String validatesessionkey= request.getParameter("sessionkey");
		String auth= request.getParameter("auth");
		String roomname= request.getParameter("roomname");
		PrintWriter out= response.getWriter();
		String role = "";
		if(MemberUtility.authenticate(validatesessionkey,auth)){
			role=MemberUtility.loginMember(username, hash, roomname);
			
			if(role.equals("user")){
				out.println("Success");
				
			}else if(role.equals("exceeded") && MemberUtility.isNull(username)){
				Boolean userexist=MemberUtility.checkEmail(username);
				if(userexist){
					Properties props = new Properties();
				    props.put("mail.smtp.host", "smtp.gmail.com");
				    props.put("mail.smtp.socketFactory.port", "465");
				    props.put("mail.smtp.socketFactory.class",
				            "javax.net.ssl.SSLSocketFactory");
				    props.put("mail.smtp.auth", "true");
				    props.put("mail.smtp.port", "465"); 
				    Session session = Session.getDefaultInstance(props,
				        new javax.mail.Authenticator() {
				                            @Override
				            protected PasswordAuthentication getPasswordAuthentication() {
				                return new PasswordAuthentication(settings.emailloginid,settings.emailpassword);
				            }
				        });

				    try {
				    	String sessionkey= MemberUtility.generateSessionKey();
				        MemberUtility.updateSession(username, sessionkey);
				        Message message = new MimeMessage(session);
				        message.setFrom(new InternetAddress(settings.emailloginid));
				        message.setRecipients(Message.RecipientType.TO,
				                InternetAddress.parse(username));
				        
				        message.setSubject("Alert about login attempt");
				        message.setText("Your SPSync account has been locked out due to too many login attempts. Please click this link (https://<yourserverip>/unlock?username="+username+"&sessionkey="+sessionkey+") to unlock your account. If this was not done by you, please change your password immediately after unlocking your account. Thank you.");
				        
				        Transport.send(message);

				        System.out.println("Done");

				    } catch (MessagingException e) {
				        throw new RuntimeException(e);
				    }
					out.println("Exceeded");
					}
			}
			else{
				out.println("Failed");
			}
		}
	}


}
