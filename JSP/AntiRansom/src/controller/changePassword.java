package controller;

import java.io.IOException;
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

import org.apache.commons.codec.digest.DigestUtils;

import model.MemberUtility;

/**
 * Servlet implementation class changePassword
 */
@WebServlet("/changePassword")
public class changePassword extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public changePassword() {
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
				HttpSession session = request.getSession();
				String email= (String)session.getAttribute("user");
				
				if(email!=null){
					String password = request.getParameter("password");
			        MemberUtility.updatePassword(email, DigestUtils.sha512Hex(password));
			        request.setAttribute("message", "Password changed.");	        
					RequestDispatcher lm=request.getRequestDispatcher("userBrowse.jsp");
					lm.forward(request, response);					
				}else{
					email = (String)session.getAttribute("admin");
					String password = request.getParameter("password");
			        MemberUtility.updatePassword(email, DigestUtils.sha512Hex(password));
			        request.setAttribute("message", "Password changed.");	        
					RequestDispatcher lm=request.getRequestDispatcher("adminIndex.jsp");
					lm.forward(request, response);
				}

	}

}
