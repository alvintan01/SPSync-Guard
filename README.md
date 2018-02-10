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

## Configurations<br />
The JSP site is meant to be running on the cloud server. The Django site is meant to be running on the Raspberry Pi. The python scripts is meant to download the email attachments from Gmail's server and should be placed in a cron job on both the cloud server and Raspberry Pi. The database dump file is meant to be imported to the cloud server's MySQL's database.
