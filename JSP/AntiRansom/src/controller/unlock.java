package controller;

import java.io.IOException;
import java.io.PrintWriter;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import model.MemberUtility;

/**
 * Servlet implementation class unlock
 */
@WebServlet("/unlock")
public class unlock extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public unlock() {
        super();
        // TODO Auto-generated constructor stub
    }

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
		doPost(request,response);
	}

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
		String username = request.getParameter("username");
		String sessionkey = request.getParameter("sessionkey");
		RequestDispatcher lm=request.getRequestDispatcher("index.jsp");
		if(sessionkey!=null){
			if(MemberUtility.checkSession(username, sessionkey)){
				MemberUtility.unlockAccount(username);
				request.setAttribute("information","Your account has been unlocked. If you still can't remember your password, click on forgot password to reset your password.");
			}else{			
				request.setAttribute("information","There was an error unlocking your account.");
			}
			
		}else{
			request.setAttribute("information","There was an error unlocking your account.");
		}
		lm.include(request, response);
	}

}
