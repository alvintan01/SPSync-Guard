package controller;

import java.io.IOException;
import java.util.Properties;

import javax.mail.BodyPart;
import javax.mail.Message;
import javax.mail.MessagingException;
import javax.mail.Multipart;
import javax.mail.PasswordAuthentication;
import javax.mail.Session;
import javax.mail.Transport;
import javax.mail.internet.AddressException;
import javax.mail.internet.InternetAddress;
import javax.mail.internet.MimeBodyPart;
import javax.mail.internet.MimeMessage;
import javax.mail.internet.MimeMultipart;
import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.commons.codec.digest.DigestUtils;

import model.MemberUtility;
import controller.settings;


/**
 * Servlet implementation class forgotPassword
 */
@WebServlet("/alertuser")
public class alertuser extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public alertuser() {
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
		String email= request.getParameter("email");
		Boolean userexist=MemberUtility.checkEmail(email);
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
		        MemberUtility.updateSession(email, sessionkey);
		        Message message = new MimeMessage(session);
		        message.setFrom(new InternetAddress(settings.emailloginid));
		        message.setRecipients(Message.RecipientType.TO,
		                InternetAddress.parse(email));
		        
		        message.setSubject("Alert about login attempt");
		        message.setText("Your SPSync account has been locked out due to too many login attempts. Please click this link (https://<yourserverip>/unlock?username="+email+"&sessionkey="+sessionkey+")to unlock your account. If this was not done by you, please change your password immediately after unlocking your account. Thank you.");
		        
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
	}

