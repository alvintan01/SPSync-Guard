# SPSync-Guard
A Ransomware detection and backup system that utilities the 3-2-1 backup approach.

## Hardware Requirements
-Raspberry Pi 3<br />
-Wireless Router<br />
-SSD<br />
-Ubuntu Cloud Server<br />

## Threat Detection Techniques
-Process Monitoring<br />
-“Tripwire-Like” detection<br />
-Ransomware files and extensions<br />
-No. of file changes<br />

## Prerequisites
You will need 2 Gmail accounts, one to act as the server and the other to act as the client.


## Configurations
The JSP site is meant to be running on the cloud server. The Django site is meant to be running on the Raspberry Pi. The Python scripts is meant to download the email attachments from Gmail's server and should be placed in a cron job on both the cloud server and Raspberry Pi.<br />
`decrypt.py` is meant to be running on the Raspberry Pi to decrypt files that are sent to it via SFTP at a faster rate. <br />
`downloadattachmentpi3.py` is meant to be running on the Raspberry Pi download the email from Gmail's server. <br />
`downloadattachmentserver3.py` is meant to be running on the cloud server download the email from Gmail's server. <br />
`randomkeygenerator.py` is meant to be running on the cloud server to update the secret key that is used to secure the communication process.<br />

The Sync folder contains the code for the Sync App which runs on the client's Windows computer. <br />
The database dump file is meant to be imported to the cloud server's MySQL's database.<br />
Do note that you will need to enter your Gmail's email address and password in the `settings.java`, `decrypt.py`, `downloadattachmentpi3.py` and `downloadattachmentserver3.py`. You will also need to change `<yourserverip>` to the IP of your cloud server in some of the files.
