package model;

import java.io.File;
import java.io.PrintWriter;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import java.security.SecureRandom;
import java.util.Random;

import javax.mail.internet.AddressException;
import javax.mail.internet.InternetAddress;

import controller.Encryptor;

public class MemberUtility {
	
	public static String loginMember(String userid, String password, String roomname){
		String role="";
		try{
			//connect to database
			Connection conn =DBConn.getConnection();
			//prepare sql statement
			PreparedStatement pstmt= conn.prepareStatement(
						"Select c_role, c_attempts from t_users where BINARY c_username=? and c_password=? and c_roomname=?");
			pstmt.setString(1, userid);
			pstmt.setString(2, password);
			pstmt.setString(3, roomname);

			ResultSet rs=pstmt.executeQuery();
			if(rs.next()){
				if(rs.getInt("c_attempts")<5){
					role=rs.getString("c_role");
					MemberUtility.unlockAccount(userid);

				}else{
					role="exceeded";
				}
			}


			if(role.equals("")){
				PreparedStatement pstmt2= conn.prepareStatement(
							"Update t_users set c_attempts=c_attempts+1 where c_username=?");
				pstmt2.setString(1, userid);
				int result = pstmt2.executeUpdate();
				if(result>0){//user exist
					PreparedStatement pstmt3= conn.prepareStatement(
							"Select c_attempts from t_users where c_username=?");
					pstmt3.setString(1, userid);
					ResultSet rs3 = pstmt3.executeQuery();
					if(rs3.next()){
						if(rs3.getInt("c_attempts")>5){
							role="exceeded";
						}
					}
				}
			}

	
		}catch(Exception ex){
			System.err.println(ex.getMessage());
		}return role;
			
	}
	
	public static String serverloginMember(String userid, String password){
		String role="";
		try{
			//connect to database
			Connection conn =DBConn.getConnection();
			//prepare sql statement
			PreparedStatement pstmt= conn.prepareStatement(
						"Select c_role, c_attempts from t_users where BINARY c_username=? and c_password=?");
			pstmt.setString(1, userid);
			pstmt.setString(2, password);

			ResultSet rs=pstmt.executeQuery();
			if(rs.next()){
				if(rs.getInt("c_attempts")<5){
					role=rs.getString("c_role");
					MemberUtility.unlockAccount(userid);

				}else{
					role="exceeded";
				}
			}


			if(role.equals("")){
				PreparedStatement pstmt2= conn.prepareStatement(
							"Update t_users set c_attempts=c_attempts+1 where c_username=?");
				pstmt2.setString(1, userid);
				int result = pstmt2.executeUpdate();
				if(result>0){//user exist
					PreparedStatement pstmt3= conn.prepareStatement(
							"Select c_attempts from t_users where c_username=?");
					pstmt3.setString(1, userid);
					ResultSet rs3 = pstmt3.executeQuery();
					if(rs3.next()){
						if(rs3.getInt("c_attempts")>5){
							role="exceeded";
						}
					}
				}
			}

	
		}catch(Exception ex){
			System.err.println(ex.getMessage());
		}return role;
			
	}
	
	public static void announce(String announce){
		try{
		//connect to database
		Connection conn =DBConn.getConnection();
		//prepare sql statement
		PreparedStatement pstmt= conn.prepareStatement(
					"Insert into t_notification (c_Message) values (?)");
		pstmt.setString(1, announce);
			
		int UpdateQuery=pstmt.executeUpdate();
		
		PreparedStatement pstmt2= conn.prepareStatement(
				"update t_notification set c_timestamp=now()");
			
		pstmt2.executeUpdate();
		
		}catch(Exception ex){
			System.err.println(ex.getMessage());
		}
	}
	
	public static void delete(String delete){
		try{
		//connect to database
		Connection conn =DBConn.getConnection();
		//prepare sql statement
		PreparedStatement pstmt= conn.prepareStatement(
					"Delete from t_notification where c_id=?");
		pstmt.setString(1, delete);
			
		pstmt.executeUpdate();
		
		PreparedStatement pstmt2= conn.prepareStatement(
				"update t_notification set c_timestamp=now()");
			
		pstmt2.executeUpdate();
		
		}catch(Exception ex){
			System.err.println(ex.getMessage());
		}
	}
	
	public static void update(String id, String update){
		try{
			//connect to database
			Connection conn =DBConn.getConnection();
			//prepare sql statement
			PreparedStatement pstmt= conn.prepareStatement(
						"update t_notification set c_message= ?, c_messagetimestamp=now() where c_id=?");
			pstmt.setString(1, update);
			pstmt.setString(2, id);
			
			pstmt.executeUpdate();
			PreparedStatement pstmt2= conn.prepareStatement(
					"update t_notification set c_timestamp=now()");
			pstmt2.executeUpdate();
			
			}catch(Exception ex){
				System.err.println(ex.getMessage());
			}
	}
	
	public static Boolean checkEmail(String email){
		try{
		//connect to database
		Connection conn =DBConn.getConnection();
		//prepare sql statement
		PreparedStatement pstmt= conn.prepareStatement(
					"Select * from t_users where c_username=?");
		pstmt.setString(1, email);
			
		ResultSet rs= pstmt.executeQuery();
		if(rs.next()){
			return true;
		}
		}catch(Exception ex){
			System.err.println(ex.getMessage());
			
		}
		return false;
	}
	
	
	  private static final Random RANDOM = new SecureRandom();
	  /** Length of password. @see #generateRandomPassword() */
	  public static final int PASSWORD_LENGTH = 8;
	  /**
	   * Generate a random String suitable for use as a temporary password.
	   *
	   * @return String suitable for use as a temporary password
	   * @since 2.4
	   */
	  public static String generateRandomPassword()
	  {
	      // Pick from some letters that won't be easily mistaken for each
	      // other. So, for example, omit o O and 0, 1 l and L.
	      String letters = "abcdefghjkmnpqrstuvwxyzABCDEFGHJKMNPQRSTUVWXYZ23456789+@";

	      String pw = "";
	      for (int i=0; i<PASSWORD_LENGTH; i++)
	      {
	          int index = (int)(RANDOM.nextDouble()*letters.length());
	          pw += letters.substring(index, index+1);
	      }
	      return pw;
	  }
	  
	  public static String generateSessionKey()
	  {
	      // Pick from some letters that won't be easily mistaken for each
	      // other. So, for example, omit o O and 0, 1 l and L.
	      String letters = "abcdefghjkmnpqrstuvwxyzABCDEFGHJKMNPQRSTUVWXYZ23456789";

	      String pw = "";
	      for (int i=0; i<30; i++)
	      {
	          int index = (int)(RANDOM.nextDouble()*letters.length());
	          pw += letters.substring(index, index+1);
	      }
	      return pw;
	  }
	  
	  public static void updatePassword(String username, String password){
		  try{
				//connect to database
				Connection conn =DBConn.getConnection();
				//prepare sql statement
				
					PreparedStatement pstmt = conn.prepareStatement(
						"update t_users set c_password=? where c_username=?");
					pstmt.setString(1, password);
					pstmt.setString(2, username);
				
					pstmt.executeUpdate();
			}catch(Exception ex){
				System.err.println(ex.getMessage());
			}
	  }
	  
	  
	  public static ResultSet getLockedUsers(){
		  try{
				//connect to database
				Connection conn =DBConn.getConnection();
				//prepare sql statement
				
					PreparedStatement pstmt = conn.prepareStatement(
						"Select * from t_users where c_attempts>=5");				
					ResultSet rs = pstmt.executeQuery();
					return rs;
			}catch(Exception ex){
				System.err.println(ex.getMessage());
			}
		  return null;
	  }
	  
	  public static void unlockAccount(String username){
		  try{
				//connect to database
				Connection conn =DBConn.getConnection();
				//prepare sql statement
				
					PreparedStatement pstmt = conn.prepareStatement(
						"update t_users set c_attempts=0, c_sessionkey=null where c_username=?");
					pstmt.setString(1, username);
				
					pstmt.executeUpdate();
			}catch(Exception ex){
				System.err.println(ex.getMessage());
			}
	  }
	  
	  public static Boolean checkSession(String username, String sessionkey){
		  try{
				//connect to database
				Connection conn =DBConn.getConnection();
				//prepare sql statement
				
					PreparedStatement pstmt = conn.prepareStatement(
						"select * from t_users where c_username=? and c_sessionkey=?");
					pstmt.setString(1, username);
					pstmt.setString(2, sessionkey);
					ResultSet rs = pstmt.executeQuery();
					
					if(rs.next()){
						PreparedStatement pstmt2 = conn.prepareStatement(
								"update t_users set c_sessionkey=null  where c_username=?");
							pstmt2.setString(1, username);
							pstmt2.executeUpdate();						
						return true;
					}
			}catch(Exception ex){
				System.err.println(ex.getMessage());
			}
		  return false;
	  }
	  
	  public static void updateSession(String username, String sessionkey){
		  try{
				//connect to database
				Connection conn =DBConn.getConnection();
				//prepare sql statement
				
					PreparedStatement pstmt = conn.prepareStatement(
						"update t_users set c_sessionkey=? where c_username=?");
					pstmt.setString(1, sessionkey);
					pstmt.setString(2, username);
					pstmt.executeUpdate();
			}catch(Exception ex){
				System.err.println(ex.getMessage());
			}
	  }
	  
	  public static long folderSize(File directory) {
		    long length = 0;
		    for (File file : directory.listFiles()) {
		        if (file.isFile())
		            length += file.length();
		        else
		            length += folderSize(file);
		    }
		    return length;
		}
	  
	  public static boolean authenticate(String sessionkey, String auth) {
		  try{
				//connect to database
				Connection conn =DBConn.getConnection();
				//prepare sql statement
				
					PreparedStatement pstmt = conn.prepareStatement(
						"Select * from t_session where c_sessionkey=?");
					pstmt.setString(1, sessionkey);
					ResultSet rs= pstmt.executeQuery();
					if (rs.next()){
						if(auth.equals(Encryptor.encrypt(SecretKey.getKey(), sessionkey))){
							PreparedStatement pstmt1 = conn.prepareStatement(
									"Delete from t_session where c_sessionkey=?");
								pstmt1.setString(1, sessionkey);
								pstmt1.executeUpdate();
							return true;
						}
					}
					
			}catch(Exception ex){
				System.err.println(ex.getMessage());
			}
		  return false;
	  }
	  
	  public static String authenticatekey(String sessionkey, String auth) {
		  try{
				//connect to database
				Connection conn =DBConn.getConnection();
				//prepare sql statement
				
					PreparedStatement pstmt = conn.prepareStatement(
						"Select * from t_session where c_sessionkey=?");
					pstmt.setString(1, sessionkey);
					ResultSet rs= pstmt.executeQuery();					
		 
					String previouskey="";
					String defaultkey="";
					PreparedStatement pstmt2 = conn.prepareStatement(
							"select c_secretkey from t_secretkey order by c_id desc limit 1,1");
						
					ResultSet rs2=pstmt2.executeQuery();
					while(rs2.next()){
						previouskey = rs2.getString("c_secretkey");
					}			
				
					PreparedStatement pstmt3 = conn.prepareStatement(
							"Delete from t_session where c_sessionkey=?");
						pstmt3.setString(1, sessionkey);
						pstmt3.executeUpdate();
						
					PreparedStatement pstmt4 = conn.prepareStatement(
							"Select * from t_secretkey limit 1");
					ResultSet rs3 = pstmt4.executeQuery();
					while(rs3.next()){
						defaultkey = rs3.getString("c_secretkey");
					}	
							
					if (rs.next()){
						if(auth.equals(Encryptor.encrypt(SecretKey.getKey(), sessionkey))){
							return "No changes";
						}else if(auth.equals(Encryptor.encrypt(previouskey, sessionkey))){
							return Encryptor.encrypt(previouskey, SecretKey.getKey());
						}else if(auth.equals(Encryptor.encrypt(defaultkey, sessionkey))){
							return Encryptor.encrypt(defaultkey, SecretKey.getKey());
						}
					}
					conn.close();
			}catch(Exception ex){
				System.err.println(ex.getMessage());
				return ex.getMessage();
			}
		  return "";
	  }
	
	  
	  public static void insertsessionkey(String sessionkey) {
		  try{
				//connect to database
				Connection conn =DBConn.getConnection();
				//prepare sql statement
				
					PreparedStatement pstmt = conn.prepareStatement(
						"insert into t_session (c_sessionkey) values (?)");
					pstmt.setString(1, sessionkey);
					pstmt.executeUpdate();
					
			}catch(Exception ex){
				System.err.println(ex.getMessage());
			}
	  }
	  public static boolean isNull(String username){
		  try{
				//connect to database
				Connection conn =DBConn.getConnection();
				//prepare sql statement
				
					PreparedStatement pstmt = conn.prepareStatement(
						"Select * from t_users where c_username=?");
					pstmt.setString(1, username);
					ResultSet rs= pstmt.executeQuery();
					rs.next();
					if(rs.getString("c_sessionkey")!=null){
						return false;
					}
					
			}catch(Exception ex){
				System.err.println(ex.getMessage());
			}
		  return true;
	  }
	  public static boolean isValidEmailAddress(String email) {
		   boolean result = true;
		   try {
		      InternetAddress emailAddr = new InternetAddress(email);
		      emailAddr.validate();
		   } catch (AddressException ex) {
		      result = false;
		   }
		   return result;
		}
}