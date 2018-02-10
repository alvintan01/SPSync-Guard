package controller;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.zip.ZipEntry;
import java.util.zip.ZipOutputStream;

import javax.servlet.ServletException;
import javax.servlet.ServletOutputStream;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import model.GetFiles;
import model.MemberUtility;

/**
 * Servlet implementation class downloadClient
 */
@WebServlet("/downloadClient")
public class downloadClient extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public downloadClient() {
        super();
        // TODO Auto-generated constructor stub
    }

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
		doPost(request, response);
	}

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
		response.setContentType("application/octet-steam");//("Content-type: text/zip");
		response.setHeader("Content-Disposition","attachment; filename=SyncSetup.zip");
		//ServletOutputStream out = response.getOutputStream();
		String zipfilepath = "/home/p1529364/attachments/installer"+MemberUtility.generateRandomPassword()+".zip";
		FileOutputStream out = new FileOutputStream(zipfilepath);
		ZipOutputStream zos = new ZipOutputStream(new BufferedOutputStream(out));
		ArrayList<File> files = new ArrayList<File>();
		GetFiles.listf("/home/p1529364/installer", files);

		for (File file : files) {

			System.out.println("Adding " + file.getName());
			zos.putNextEntry(new ZipEntry(file.getName()));

			// Get the file
			FileInputStream fis = null;
			try {
				fis = new FileInputStream(file);

			} catch (FileNotFoundException fnfe) {
				// If the file does not exists, write an error entry instead of
				// file
				// contents
				zos.write(("ERRORld not find file " + file.getName())
						.getBytes());
				zos.closeEntry();
				System.out.println("Couldfind file "
						+ file.getAbsolutePath());
				continue;
			}

			BufferedInputStream fif = new BufferedInputStream(fis);

			// Write the contents of the file
			int data = 0;
			while ((data = fif.read()) != -1) {
				zos.write(data);
			}
			fif.close();

			zos.closeEntry();
			System.out.println("Finished adding file " + file.getName());
		}

		zos.close();
		
		InputStream fileIn = new BufferedInputStream(new FileInputStream(zipfilepath));
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
		File zipfile = new File(zipfilepath);
		zipfile.delete();
	}
}
