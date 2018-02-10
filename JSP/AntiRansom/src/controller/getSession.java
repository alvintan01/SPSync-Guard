package controller;

import java.io.IOException;
import java.io.PrintWriter;
import java.security.SecureRandom;
import java.util.Random;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import model.MemberUtility;

/**
 * Servlet implementation class getSession
 */
@WebServlet("/getSession")
public class getSession extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public getSession() {
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
		 // TODO Auto-generated method stub
		 PrintWriter out= response.getWriter();
		 // Pick from some letters that won't be easily mistaken for each
	     // other. So, for example, omit o O and 0, 1 l and L.
	     String letters = "abcdefghjkmnpqrstuvwxyzABCDEFGHJKMNPQRSTUVWXYZ23456789+@";
	     Random RANDOM = new SecureRandom();
	     String pw = "";
	     for (int i=0; i<16; i++)
	     {
         int index = (int)(RANDOM.nextDouble()*letters.length());	      
         pw += letters.substring(index, index+1);
	     }
	     MemberUtility.insertsessionkey(pw);
	     out.println(pw);
	}

}
