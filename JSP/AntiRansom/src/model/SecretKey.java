package model;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

public class SecretKey {
	public static String getKey(){
		String key = "";
		try{
			//connect to database
			Connection conn =DBConn.getConnection();
			//prepare sql statement
			PreparedStatement pstmt= conn.prepareStatement(
						"Select c_secretkey from t_secretkey order by c_id desc limit 1");
				
			ResultSet rs=pstmt.executeQuery();
			if(rs.next()){
				key = rs.getString("c_secretkey");
			}	
			
		}catch(Exception ex){
			System.err.println(ex.getMessage());
		}
		return key;
	}
		
	
}