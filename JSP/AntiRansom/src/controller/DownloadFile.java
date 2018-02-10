package controller;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.DataInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.zip.ZipEntry;
import java.util.zip.ZipOutputStream;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletContext;
import javax.servlet.ServletException;
import javax.servlet.ServletOutputStream;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import model.MemberUtility;
import model.SecretKey;


/**
 * Servlet implementation class DownloadFile
 */
@WebServlet("/DownloadFile")
public class DownloadFile extends HttpServlet implements
javax.servlet.Servlet{
	private static final long serialVersionUID = 1L;
	private static final int BUFSIZE = 4096;
	
    /**
     * @see HttpServlet#HttpServlet()
     */
    public DownloadFile() {
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
		String name = request.getParameter("fileName");
		String actualpath = request.getParameter("filePath");
		String path = actualpath.replaceFirst("\\/home\\/p1529364\\/attachments\\/", "");
		String username= request.getParameter("username");
	
		response.setContentType("application/octet-steam");//("Content-type: text/zip");
		response.setHeader("Content-Disposition","attachment; filename=SPSync.sync");
		//ServletOutputStream out = response.getOutputStream();
		String zipfilepath = "/home/p1529364/attachments/"+username+MemberUtility.generateRandomPassword()+".zip";
		FileOutputStream out = new FileOutputStream(zipfilepath);
		ZipOutputStream zos = new ZipOutputStream(new BufferedOutputStream(out));

		if(path.startsWith(username)){
			path = "C"+path.substring(username.length(),path.length());
		}
		//path = path.replaceFirst("\\/home\\/server\\/attachments\\/[^\\/]+\\/Real\\/", "");

		//zos.putNextEntry(new ZipEntry("abc/"+name));
		zos.putNextEntry(new ZipEntry(path+"/"+name));
		// Get the file
		FileInputStream fis = null;
		try {
			fis = new FileInputStream(actualpath+"/"+name);

		} catch (FileNotFoundException fnfe) {
			// If the file does not exists, write an error entry instead of
			// file
			// contents
			zos.write(("ERRORld not find file " + name)
					.getBytes());
			zos.closeEntry();
			System.out.println("Couldfind file "
					+ actualpath);
		}

		BufferedInputStream fif = new BufferedInputStream(fis);

		// Write the contents of the file
		int data = 0;
		while ((data = fif.read()) != -1) {
			zos.write(data);
		}
		fif.close();

		zos.closeEntry();
		System.out.println("Finished adding file " + name);
	

	zos.close();
	try{
	AESCrypt aes = new AESCrypt(true, SecretKey.getKey());
	String randompath="/home/p1529364/attachments/"+username+MemberUtility.generateRandomPassword()+".sync";//just to append random string at the end of username
	aes.encrypt(2, zipfilepath,randompath);
	File encryptedfile = new File(randompath);
	//FileInputStream fileIn = new FileInputStream(encryptedfile);
	InputStream fileIn = new BufferedInputStream(new FileInputStream(encryptedfile));
	ServletOutputStream servletout = response.getOutputStream();
	try {
		int ch;
		while ((ch = fileIn.read()) != -1) {
		servletout.print((char) ch);
		}
	}
	finally {
		if (fileIn != null) fileIn.close(); // very important
	}

	servletout.flush();
	servletout.close();
	fileIn.close(); 
	encryptedfile.delete();
	File zipfile = new File(zipfilepath);
	zipfile.delete();
	}catch(Exception e){
	e.printStackTrace();
	}

		
	}	  

}
