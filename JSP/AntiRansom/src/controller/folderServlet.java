package controller;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.PrintWriter;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import java.util.zip.ZipEntry;
import java.util.zip.ZipOutputStream;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.ServletOutputStream;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import model.GetFiles;

/**
 * Servlet implementation class folderServlet
 */
@WebServlet("/folderServlet")
public class folderServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public folderServlet() {
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
		ArrayList<File> files = new ArrayList<File>();
		ArrayList<File> filestodownload = new ArrayList<File>();
		String chosenpath=request.getParameter("chosenpath");
		GetFiles.listf(chosenpath, files);
		//GetFiles.listf("C:/testing", files);
		DateFormat df = new SimpleDateFormat("dd-MM-yyyy hh-mm-ss");
		DateFormat df2 = new SimpleDateFormat("yyyy-MM-dd'T'hh:mm"); 
		Date selecteddate = new Date();
		Date filedat = null;
		Date oldfiledat = null;
		String filename="";
		String filedate="";
		String fileextension="";
		String path="";
		String oldfilename="";
		String oldfiledate="";
		String oldfileextension="";
		String oldpath="";
		Pattern pattern = Pattern.compile("(.+\\/)(.+) ([0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9])(\\.[^\\.]+$)");
		//Pattern pattern = Pattern.compile("(.+\\\\)(.+) ([0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9])(\\.[^\\.]+$)");
		Matcher matcher = null;
		ArrayList<File> filetoremove = new ArrayList<File>();

		
		try {
			selecteddate = df2.parse(request.getParameter("date"));
			
		} catch (ParseException e) {
		    e.printStackTrace();
		}


		for(File f: files) {		
			matcher = pattern.matcher(f.getPath());

			while (matcher.find()) {
				path= matcher.group(1);//path
			    filename= matcher.group(2);//name
			    filedate = matcher.group(3);//filedate
			    fileextension= matcher.group(4);//extension
			}
					
			try {
			    filedat = df.parse(filedate);
			} catch (ParseException e) {
			    e.printStackTrace();
			}

			if (filedat.compareTo(selecteddate)<=0){//!filedat.after(selecteddate)){
				Boolean added = false;
				for(File g: filestodownload) {
					matcher = pattern.matcher(g.getPath());

					while (matcher.find()) {
						oldpath= matcher.group(1);//path
						oldfilename= matcher.group(2);//name
						oldfiledate = matcher.group(3);//filedate
						oldfileextension= matcher.group(4);//extension
						try {
							oldfiledat = df.parse(oldfiledate);
						} catch (ParseException e) {
						    e.printStackTrace();
						}

						
						if(oldpath.equals(path) && oldfilename.equals(filename) && fileextension.equals(oldfileextension)){
							if(filedat.compareTo(oldfiledat)>0){//if newer file								
								filetoremove.add(g);							
							}else{//if older file
								added=true;
							}
							break;
						}
					}										
				}
				if(!added){
					filestodownload.add(f);
				}
				filestodownload.removeAll(filetoremove);	
				filetoremove.clear();
				
			}
		}

		response.setContentType("Content-type: text/zip");
		response.setHeader("Content-Disposition","attachment; filename=mytest.zip");
		ServletOutputStream out = response.getOutputStream();
		ZipOutputStream zos = new ZipOutputStream(new BufferedOutputStream(out));

		for (File file : filestodownload) {

			System.out.println("Adding " + file.getName());
			path = file.getParent();
			
			path = path.replace("/home/server/attachments/alvin/Real/", "");
			zos.putNextEntry(new ZipEntry(path+"/" +file.getName()));

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
			System.out.println("Finishedng file " + file.getName());
		}

		zos.close();
	}

}
