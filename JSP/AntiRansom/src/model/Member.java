package model;

public class Member {
	private int memberid=0;
	private String name="";
	private String password="";
	private String address="";
	private String email="";
	private String contact="";
	public int getMemberid() {
		return memberid;
	}
	public Member(){
		
	}
	public Member(String name, String password, String address,String email, String contact) {
		super();
		this.name = name;
		this.password = password;
		this.address = address;
		this.email = email;
		this.contact = contact;
	}
	public void setMemberid(int memberid) {
		this.memberid = memberid;
	}
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public String getPassword() {
		return password;
	}
	public void setPassword(String password) {
		this.password = password;
	}
	public String getAddress() {
		return address;
	}
	public void setAddress(String address) {
		this.address = address;
	}
	public String getEmail() {
		return email;
	}
	public void setEmail(String email) {
		this.email = email;
	}
	public String getContact() {
		return contact;
	}
	public void setContact(String contact) {
		this.contact = contact;
	}

}
