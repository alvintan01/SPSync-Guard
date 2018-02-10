package controller;

import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Base64;
import java.util.Properties;

import javax.mail.Message;
import javax.mail.MessagingException;
import javax.mail.PasswordAuthentication;
import javax.mail.Session;
import javax.mail.Transport;
import javax.mail.internet.InternetAddress;
import javax.mail.internet.MimeMessage;
import javax.servlet.RequestDispatcher;
import javax.servlet.ServletContext;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.apache.commons.codec.digest.DigestUtils;

import model.MemberUtility;

/**
 * Servlet implementation class loginServlet
 */
@WebServlet("/login")
public class login extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public login() {
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
		//doGet(request, response);
		String login = request.getParameter("loginID");
		String password = request.getParameter("password");
		
			
		String sha512password = DigestUtils.sha512Hex(password);
		String role;
		
		role=MemberUtility.serverloginMember(login, sha512password);
		
		System.out.println(sha512password);
		
		if(role.equals("admin")){
			HttpSession session =request.getSession();
			session.setAttribute("admin", login);
			RequestDispatcher lm=request.getRequestDispatcher("adminIndex.jsp");
			lm.forward(request, response);
		}
		else if(role.equals("user")){
			HttpSession session =request.getSession();
			session.setAttribute("user", login);
			MemberUtility.unlockAccount(login);//reset attempts counter
			RequestDispatcher lm=request.getRequestDispatcher("userBrowse.jsp");
			lm.forward(request, response);
			
		}else if(role.equals("exceeded") && MemberUtility.isNull(login)){
			Boolean userexist=MemberUtility.checkEmail(login);
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
			        MemberUtility.updateSession(login, sessionkey);
			        Message message = new MimeMessage(session);
			        message.setFrom(new InternetAddress(settings.emailloginid));
			        message.setRecipients(Message.RecipientType.TO,
			                InternetAddress.parse(login));
			        
			        message.setSubject("Alert about login attempt");
			        message.setText("Your SPSync account has been locked out due to too many login attempts. Please click this link (https://<yourserverip>/unlock?username="+login+"&sessionkey="+sessionkey+") to unlock your account. If this was not done by you, please change your password immediately after unlocking your account. Thank you.");
			        
			        Transport.send(message);

			        System.out.println("Done");

			    } catch (MessagingException e) {
			        throw new RuntimeException(e);
			    }
				request.setAttribute("error","You have exceeded your login attempts");
				request.setAttribute("information","Please check your email for help.");
				RequestDispatcher lm=request.getRequestDispatcher("index.jsp");            
				lm.include(request, response);
				}
		}
		else{
			request.setAttribute("error","Invalid Username or Password");
			RequestDispatcher lm=request.getRequestDispatcher("index.jsp");            
			lm.include(request, response);
		}
	}

}
