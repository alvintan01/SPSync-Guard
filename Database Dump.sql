CREATE DATABASE  IF NOT EXISTS `webapp` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `webapp`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 192.168.0.60    Database: webapp
-- ------------------------------------------------------
-- Server version	5.7.21-0ubuntu0.16.04.1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `t_notification`
--

DROP TABLE IF EXISTS `t_notification`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `t_notification` (
  `c_id` int(11) NOT NULL AUTO_INCREMENT,
  `c_message` varchar(500) NOT NULL,
  `c_messagetimestamp` datetime DEFAULT CURRENT_TIMESTAMP,
  `c_timestamp` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`c_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_notification`
--

LOCK TABLES `t_notification` WRITE;
/*!40000 ALTER TABLE `t_notification` DISABLE KEYS */;
INSERT INTO `t_notification` VALUES (1,'There are reports of a new ransomware outbreak known as crypt2. Users are advised to backup their files and to verify the sender of emails before downloading the attachments.','2018-01-26 11:51:28','2018-01-30 16:23:01');
/*!40000 ALTER TABLE `t_notification` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `t_ransomprocess`
--

DROP TABLE IF EXISTS `t_ransomprocess`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `t_ransomprocess` (
  `c_id` int(11) NOT NULL AUTO_INCREMENT,
  `c_processname` varchar(100) NOT NULL,
  `c_info` varchar(1000) DEFAULT '',
  `c_timestamp` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`c_id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_ransomprocess`
--

LOCK TABLES `t_ransomprocess` WRITE;
/*!40000 ALTER TABLE `t_ransomprocess` DISABLE KEYS */;
INSERT INTO `t_ransomprocess` VALUES (1,'notepad','Microsoft Notepad.','2018-01-30 09:44:42'),(2,'mssecsvc','This process is created by the Wannacry ransomware upon execution','2018-01-30 09:47:50'),(3,'Tasksche','This process is created by the Wannacry ransomware upon execution','2018-01-30 09:48:01'),(4,'taskdl','This process is created by the Wannacry ransomware upon execution','2018-01-30 09:48:56'),(5,'taskse','This process is created by the Wannacry ransomware upon execution','2018-01-30 09:49:32'),(6,'@WanaDecryptor@','This process is created by the Wannacry ransomware upon execution','2018-01-30 09:50:08'),(7,'dispci','This process is created by the Bad Rabbit ransomware upon execution','2018-01-30 09:55:18'),(8,'cscc','This process is created by the Bad Rabbit ransomware upon execution','2018-01-30 09:56:06'),(9,'Adobe_Flash_Player','This process is created by the Bad Rabbit ransomware upon execution','2018-01-30 09:58:50'),(10,'m1c2','This process is created by the Locky ransomware upon execution','2018-01-30 11:32:16'),(11,'fYWHvhbB','This process is created by the Locky ransomware upon execution','2018-01-30 11:35:21'),(12,'ScCoilJk','This process is created by the Locky ransomware upon execution','2018-01-30 11:35:33');
/*!40000 ALTER TABLE `t_ransomprocess` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `t_ransomwareextension`
--

DROP TABLE IF EXISTS `t_ransomwareextension`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `t_ransomwareextension` (
  `c_id` int(11) NOT NULL AUTO_INCREMENT,
  `c_extension` varchar(100) NOT NULL,
  `c_info` varchar(1000) DEFAULT '',
  `c_timestamp` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`c_id`)
) ENGINE=InnoDB AUTO_INCREMENT=88 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_ransomwareextension`
--

LOCK TABLES `t_ransomwareextension` WRITE;
/*!40000 ALTER TABLE `t_ransomwareextension` DISABLE KEYS */;
INSERT INTO `t_ransomwareextension` VALUES (1,'.epic','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(2,'.ecc','Used by the ransomware known as TeslaCrypt (ver. 1, 2, and 3)','2018-01-23 15:26:56'),(3,'.ezz','Used by the ransomware known as TeslaCrypt 3 and TeslaCrypt 4','2018-01-23 15:26:56'),(4,'.cerber3','The ransomware known as Cerber 3 uses this extension to denote encrypted files or folders which were affected by the ransomware.','2018-01-23 15:26:56'),(5,'.fun','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(6,'.pabluk300CrYpT!','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(7,'.pablukCRYPT','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(8,'.kill','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(9,'.korea','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(10,'.kkk','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(11,'.gws','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(12,'.btc','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(13,'.hush','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(14,'.paytounlock','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(15,'.nemo-hacks.at.sigaint.org','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(16,'.uk-dealer@sigaint.org','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(17,'.gefickt','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(18,'.ghost','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(19,'.pay','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(20,'.payms','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(21,'.paymst','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(22,'.porno','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(23,'.xyz','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(24,'.versiegelt','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(25,'.encrypted','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(26,'.Locked','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(27,'.locked','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(28,'.Contact_TarineOZA@Gmail.com_','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(29,'.tdelf','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(30,'.lost','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(31,'.R3K7M9','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(32,'.rat','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(33,'.jigsaw','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(34,'.pabluklocker','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(35,'.beep','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(36,'.CryptWalker','Used by the ransomware known as Jigsaw','2018-01-23 15:26:56'),(37,'.TTT','Used by the ransomware known as Teslacrypt 3.0','2018-01-23 15:26:56'),(38,'.XXX','Used by the ransomware known as Teslacrypt 3.0','2018-01-23 15:26:56'),(39,'.MICRO','Used by the ransomware known as Teslacrypt 3.0','2018-01-23 15:26:56'),(40,'.exx','Used by the ransomware known as TeslaCrypt 4','2018-01-23 15:26:56'),(41,'.zzz','Used by the ransomware known as TeslaCrypt','2018-01-23 15:26:56'),(42,'.aaa','Used by the ransomware known as TeslaCrypt 5','2018-01-23 15:26:56'),(43,'.abc','Used by the ransomware known as TeslaCrypt 5','2018-01-23 15:26:56'),(44,'.ccc','Used by the ransomware known as TeslaCrypt 5 and TeslaCrypt 7','2018-01-23 15:26:56'),(45,'.vvv','Used by the ransomware known as TeslaCrypt 8','2018-01-23 15:26:56'),(48,'.Lukitus','Used by the ransomware known as Locky','2018-01-23 15:26:56'),(49,'.diablo6','Used by the ransomware known as Locky','2018-01-23 15:26:56'),(50,'.zepto','Used by the ransomware known as Locky','2018-01-23 15:26:56'),(51,'.odin','Used by the ransomware known as Locky','2018-01-23 15:26:56'),(52,'.shit','Used by the ransomware known as Locky','2018-01-23 15:26:56'),(53,'.thor','Used by the ransomware known as Locky','2018-01-23 15:26:56'),(54,'.aesir','Used by the ransomware known as Locky','2018-01-23 15:26:56'),(55,'.zzzzz','Used by the ransomware known as Locky','2018-01-23 15:26:56'),(56,'.osiris','Used by the ransomware known as Locky','2018-01-23 15:26:56'),(57,'.loptr','Used by the ransomware known as Locky','2018-01-23 15:26:56'),(58,'.lukitus','Used by the ransomware known as Locky','2018-01-23 15:26:56'),(59,'.xtbl','Used by the ransomware known as Crysis and Troldesh','2018-01-23 15:26:56'),(60,'.cerber','Used by the ransomware known as Cerber','2018-01-23 15:26:56'),(61,'.cerber2','Used by the ransomware known as Cerber','2018-01-23 15:26:56'),(62,'.crypted','Used by the ransomware known as Nemucod','2018-01-23 15:26:56'),(64,'.AZER','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(65,'.MOLE','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(66,'.rmd','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(67,'.CK','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(68,'.mole00','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(69,'.rscl','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(70,'.code','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(71,'.mole02','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(72,'.CNC','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(73,'.scl','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(74,'.mole03','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(75,'.SHARK','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(76,'.CRYPTOSHIELD','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(77,'.NOOB','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(78,'.WALLET','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(79,'.ERROR','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(80,'.OGONIA','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(81,'.x1881','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(82,'.EXTE','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(83,'.PIRATE','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(84,'.ZAYKA','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(85,'.lesli','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(86,'.rmdk','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56'),(87,'.ZERO','Used by the ransomware known as CryptoMix','2018-01-23 15:26:56');
/*!40000 ALTER TABLE `t_ransomwareextension` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `t_ransomwarefile`
--

DROP TABLE IF EXISTS `t_ransomwarefile`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `t_ransomwarefile` (
  `c_id` int(11) NOT NULL AUTO_INCREMENT,
  `c_file` varchar(100) NOT NULL,
  `c_info` varchar(1000) DEFAULT '',
  `c_timestamp` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`c_id`)
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_ransomwarefile`
--

LOCK TABLES `t_ransomwarefile` WRITE;
/*!40000 ALTER TABLE `t_ransomwarefile` DISABLE KEYS */;
INSERT INTO `t_ransomwarefile` VALUES (1,'HELP_DECRYPT.TXT','The ransomware known as Cerber 3 creates this file to give instructions to the victim on how to pay the ransom and decrypt affected files & folders.','2018-01-23 15:17:03'),(2,'HELP_YOUR_FILES.TXT','The ransomware known as CryptoWall 4.0 creates this file to give instructions to the victim on how to pay the ransom and decrypt affected files & folders.','2018-01-23 15:17:03'),(3,'HELP_TO_DECRYPT_YOUR_FILES.txt','The ransomware known as Cryptolocker & TeslaCrypt creates this file to give instructions to the victim on how to pay the ransom and decrypt affected files & folders.','2018-01-23 15:17:03'),(4,'HELP_TO_DECRYPT_YOUR_FILES.bmp','The ransomware known as TeslaCrypt creates this file to give instructions to the victim on how to pay the ransom and decrypt affected files & folders.','2018-01-23 15:17:03'),(5,'HELP_RESTORE_FILES.txt','The ransomware known as TeslaCrypt creates this file to give instructions to the victim on how to pay the ransom and decrypt affected files & folders.','2018-01-23 15:17:03'),(6,'HELP_RESTORE_FILES.bmp','The ransomware known as TeslaCrypt creates this file to give instructions to the victim on how to pay the ransom and decrypt affected files & folders.','2018-01-23 15:17:03'),(7,'HELP_TO_SAVE_FILES.txt','The ransomware known as AlphaCrypt creates this file to give instructions to the victim on how to pay the ransom and decrypt affected files & folders.','2018-01-23 15:17:03'),(8,'HELP_TO_SAVE_FILES.bmp','The ransomware known as AlphaCrypt creates this file to give instructions to the victim on how to pay the ransom and decrypt affected files & folders.','2018-01-23 15:17:03'),(9,'@Please_Read_Me@.txt','This file is created by the ransomware known as Wannacry to give victims instructions on how to pay the ransom and recover their encrypted files.','2018-01-23 15:17:03'),(10,'@WanaDecryptor@.exe','This file is used by the ransomware known as WannaCry to decrypt files after the ransom has been paid','2018-01-23 15:17:03'),(11,'@WanaDecryptor@.exe.lnk','This file is used by the ransomware known as WannaCry to decrypt files after the ransom has been paid','2018-01-23 15:17:03'),(12,'Please_Read_Me!.txt','This file is created by the ransomware known as Wannacry (old variant) to give victims instructions on how to pay the ransom and recover their encrypted files.','2018-01-23 15:17:03'),(13,'tasksche.exe','This file is used by the ransomware known as WannaCry','2018-01-23 15:17:03'),(14,'131181494299235.bat','This file is used by the ransomware known as WannaCry','2018-01-23 15:17:03'),(15,'176641494574290.bat','This file is used by the ransomware known as WannaCry','2018-01-23 15:17:03'),(16,'217201494590800.bat','This file is used by the ransomware known as WannaCry','2018-01-23 15:17:03'),(17,'00000000.pky','This file is used by the ransomware known as WannaCry','2018-01-23 15:17:03'),(18,'00000000.eky','This file is used by the ransomware known as WannaCry','2018-01-23 15:17:03'),(19,'00000000.res','This file is used by the ransomware known as WannaCry','2018-01-23 15:17:03'),(20,'taskdl.exe','This file is used by the ransomware known as WannaCry','2018-01-23 15:17:03'),(21,'mssecsvc.exe','This file is used by the ransomware known as WannaCry','2018-01-23 15:17:03'),(22,'iylipul.exe','This file is created by the ransomware known as TeslaCrypt','2018-01-23 15:17:03'),(23,'key.dat','This file is created by the ransomware known as TeslaCrypt','2018-01-23 15:17:03'),(24,'log.html','This file is created by the ransomware known as TeslaCrypt','2018-01-23 15:17:03'),(25,'HowTo_Restore_FILES.TXT','This file is created by the ransomware known as TeslaCrypt to provide instructions to the victim on how to pay the ransom and recover his affected files','2018-01-23 15:17:03'),(26,'HowTo_Restore_FILES.HTM','This file is created by the ransomware known as TeslaCrypt to provide instructions to the victim on how to pay the ransom and recover his affected files','2018-01-23 15:17:03'),(27,'HowTo_Restore_FILES.BMP','This file is created by the ransomware known as TeslaCrypt to provide instructions to the victim on how to pay the ransom and recover his affected files','2018-01-23 15:17:03'),(28,'perfc.dat','This file is used by the ransomware known as Petya to initialize the ransomware','2018-01-23 15:17:03'),(29,'perfc','This file is created by the ransomware known as Petya as a mark of infection','2018-01-23 15:17:03'),(30,'infpub.dat','This file is used by the ransomware known as BadRabbit','2018-01-23 15:17:03'),(31,'dispci.exe','This file is used by the ransomware known as BadRabbit to install bootlocker','2018-01-23 15:17:03'),(32,'cscc.dat','This file is created by the ransomware known as BadRabbit','2018-01-23 15:17:03'),(33,'lukitus.bmp','This file is created by the ransomware known as Locky to give the victim instructions on how to pay the ransom and decrypt the affected files','2018-01-23 15:17:03'),(34,'a0.exe','This file is created by the ransomware known as Nemucod to execute the ransomware','2018-01-23 15:17:03'),(35,'a1.exe','This file is created by the ransomware known as Nemucod to execute the ransomware','2018-01-23 15:17:03'),(36,'a2.exe','This file is created by the ransomware known as Nemucod to execute the ransomware','2018-01-23 15:17:03'),(37,'DECRYPT.TXT','This file is created by the ransomware known as Nemucod to give the victim instructions on how to pay the ransom and recover the affected files','2018-01-23 15:17:03'),(38,'wnsrvupd.exe','This file is created by the ransomware known as Jaff to execute the ransomware','2018-01-23 15:17:03'),(39,'ks9a96HHD9g72Zm.exe','This file is created by the ransomware known as Jaff to execute the ransomware','2018-01-23 15:17:03'),(40,'81063163ded.exe.bin','This file is created by the ransomware known as Spora','2018-01-23 15:17:03');
/*!40000 ALTER TABLE `t_ransomwarefile` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `t_rooms`
--

DROP TABLE IF EXISTS `t_rooms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `t_rooms` (
  `c_id` int(11) NOT NULL AUTO_INCREMENT,
  `c_roomname` varchar(100) NOT NULL,
  `c_ip` varchar(100) NOT NULL,
  `c_sshfingerprint` varchar(100) NOT NULL,
  `c_timestamp` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`c_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_rooms`
--

LOCK TABLES `t_rooms` WRITE;
/*!40000 ALTER TABLE `t_rooms` DISABLE KEYS */;
INSERT INTO `t_rooms` VALUES (1,'T2031','172.16.0.100','A4:E7:2D:6D:FF:CD:0A:C3:7B:BF:05:2B:5D:06:53:61','2018-01-23 16:21:08');
/*!40000 ALTER TABLE `t_rooms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `t_secretkey`
--

DROP TABLE IF EXISTS `t_secretkey`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `t_secretkey` (
  `c_id` int(11) NOT NULL AUTO_INCREMENT,
  `c_secretkey` varchar(500) NOT NULL,
  `c_timestamp` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`c_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_secretkey`
--

LOCK TABLES `t_secretkey` WRITE;
/*!40000 ALTER TABLE `t_secretkey` DISABLE KEYS */;
INSERT INTO `t_secretkey` VALUES (1,'defaultkey123456','2017-10-19 10:38:54');
/*!40000 ALTER TABLE `t_secretkey` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `t_session`
--

DROP TABLE IF EXISTS `t_session`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `t_session` (
  `c_sessionkey` varchar(500) NOT NULL,
  `c_timestamp` datetime DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_session`
--

LOCK TABLES `t_session` WRITE;
/*!40000 ALTER TABLE `t_session` DISABLE KEYS */;
/*!40000 ALTER TABLE `t_session` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `t_trustedprocess`
--

DROP TABLE IF EXISTS `t_trustedprocess`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `t_trustedprocess` (
  `c_id` int(11) NOT NULL AUTO_INCREMENT,
  `c_processname` varchar(100) NOT NULL,
  `c_info` varchar(1000) DEFAULT '',
  `c_timestamp` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`c_id`)
) ENGINE=InnoDB AUTO_INCREMENT=302 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_trustedprocess`
--

LOCK TABLES `t_trustedprocess` WRITE;
/*!40000 ALTER TABLE `t_trustedprocess` DISABLE KEYS */;
INSERT INTO `t_trustedprocess` VALUES (1,'AcroRd32','','2018-01-23 16:10:48'),(2,'ShellExperienceHost','','2018-01-23 16:10:48'),(3,'AdobeUpdateService','','2018-01-23 16:10:48'),(4,'iexplore','','2018-01-23 16:10:48'),(5,'MpCmdRun','','2018-01-23 16:10:48'),(6,'sttray64','','2018-01-23 16:10:48'),(7,'prodreg','','2018-01-23 16:10:48'),(8,'SystemSettingsBroker','','2018-01-23 16:10:48'),(9,'RegSrvc','','2018-01-23 16:10:48'),(10,'PeopleExperienceHost','','2018-01-23 16:10:48'),(11,'AsPatchTouchPanel64','','2018-01-23 16:10:48'),(12,'eclipse','','2018-01-23 16:10:48'),(13,'VSSVC','','2018-01-23 16:10:48'),(14,'RuntimeBroker','','2018-01-23 16:10:48'),(15,'chrome','','2018-01-23 16:10:48'),(16,'NvTelemetryContainer','','2018-01-23 16:10:48'),(17,'WZUpdateNotifier','','2018-01-23 16:10:48'),(18,'hpqwmiex','','2018-01-23 16:10:48'),(19,'ibtsiva','','2018-01-23 16:10:48'),(20,'AppleMobileDeviceService','','2018-01-23 16:10:48'),(21,'msoia','','2018-01-23 16:10:48'),(22,'splwow64','','2018-01-23 16:10:48'),(23,'sqlservr','','2018-01-23 16:10:48'),(24,'adb','','2018-01-23 16:10:48'),(25,'audiodg','','2018-01-23 16:10:48'),(26,'devenv','','2018-01-23 16:10:48'),(27,'PtWatchDog','','2018-01-23 16:10:48'),(28,'bcastdvr','','2018-01-23 16:10:48'),(29,'stacsv64','','2018-01-23 16:10:48'),(30,'taskhostw','','2018-01-23 16:10:48'),(31,'csrss','','2018-01-23 16:10:48'),(32,'wfica32','','2018-01-23 16:10:48'),(33,'VBCSCompiler','','2018-01-23 16:10:48'),(34,'Steam','','2018-01-23 16:10:48'),(35,'ChsIME','','2018-01-23 16:10:48'),(36,'ACMON','','2018-01-23 16:10:48'),(37,'SteamService','','2018-01-23 16:10:48'),(38,'OfficeC2RClient','','2018-01-23 16:10:48'),(39,'RAVCpl64','','2018-01-23 16:10:48'),(40,'TrustedInstaller','','2018-01-23 16:10:48'),(41,'GoogleUpdate','','2018-01-23 16:10:48'),(42,'quickset','','2018-01-23 16:10:48'),(43,'ssh','','2018-01-23 16:10:48'),(44,'PwmTower','','2018-01-23 16:10:48'),(45,'msvsmon','','2018-01-23 16:10:48'),(46,'dwm','','2018-01-23 16:10:48'),(47,'nvxdsync','','2018-01-23 16:10:48'),(48,'svchost','','2018-01-23 16:10:48'),(49,'AdobeIPCBroker','','2018-01-23 16:10:48'),(50,'NvNetworkService','','2018-01-23 16:10:48'),(51,'lynchtmlconv','','2018-01-23 16:10:48'),(52,'ServiceHub.Host.CLR.x86','','2018-01-23 16:10:48'),(53,'GoogleCrashHandler64','','2018-01-23 16:10:48'),(54,'WSCStatusController','','2018-01-23 16:10:48'),(55,'igfxEM','','2018-01-23 16:10:48'),(56,'ServiceHub.Host.Node.x86','','2018-01-23 16:10:48'),(57,'AsLdrSrv','','2018-01-23 16:10:48'),(58,'DMedia','','2018-01-23 16:10:48'),(59,'Idle','','2018-01-23 16:10:48'),(60,'ServiceHub.SettingsHost','','2018-01-23 16:10:48'),(61,'vpnui','','2018-01-23 16:10:48'),(62,'jhi_service','','2018-01-23 16:10:48'),(63,'ielowutil','','2018-01-23 16:10:48'),(64,'Hearthstone','','2018-01-23 16:10:48'),(65,'NVIDIA Web Helper','','2018-01-23 16:10:48'),(66,'WinZipCompressionSmartMonitor','','2018-01-23 16:10:48'),(67,'DellDataVault','','2018-01-23 16:10:48'),(68,'DCCService','','2018-01-23 16:10:48'),(69,'steamwebhelper','','2018-01-23 16:10:48'),(70,'AsusSmartGestureDetector64','','2018-01-23 16:10:48'),(71,'PtSvcHost','','2018-01-23 16:10:48'),(72,'Taskmgr','','2018-01-23 16:10:48'),(73,'uiWatchDog','','2018-01-23 16:10:48'),(74,'igfxHK','','2018-01-23 16:10:48'),(75,'IpOverUsbSvc','','2018-01-23 16:10:48'),(76,'WDAppManager','','2018-01-23 16:10:48'),(77,'MSASCuiL','','2018-01-23 16:10:48'),(78,'DellDataVaultWiz','','2018-01-23 16:10:48'),(79,'SynTPEnh','','2018-01-23 16:10:48'),(80,'HPMSGSVC','','2018-01-23 16:10:48'),(81,'AsBhcSrv','','2018-01-23 16:10:48'),(82,'SynTPEnhService','','2018-01-23 16:10:48'),(83,'SpeechRuntime','','2018-01-23 16:10:48'),(84,'WmiApSrv','','2018-01-23 16:10:48'),(85,'DeliveryService','','2018-01-23 16:10:48'),(86,'OneDrive','','2018-01-23 16:10:48'),(87,'smartscreen','','2018-01-23 16:10:48'),(88,'UsoClient','','2018-01-23 16:10:48'),(89,'ServiceHub.DataWarehouseHost','','2018-01-23 16:10:48'),(90,'SearchUI','','2018-01-23 16:10:48'),(91,'uiSeAgnt','','2018-01-23 16:10:48'),(92,'gimp-2.8','','2018-01-23 16:10:48'),(93,'LogonUI','','2018-01-23 16:10:48'),(94,'AsusTPHelper','','2018-01-23 16:10:48'),(95,'ServiceHub.VSDetouredHost','','2018-01-23 16:10:48'),(96,'Sonos','','2018-01-23 16:10:48'),(97,'IntelCpHDCPSvc','','2018-01-23 16:10:48'),(98,'pythonw','','2018-01-23 16:10:48'),(99,'ApplicationFrameHost','','2018-01-23 16:10:48'),(100,'ToolbarNativeMsgHost','','2018-01-23 16:10:48'),(101,'WMIADAP','','2018-01-23 16:10:48'),(102,'LMS','','2018-01-23 16:10:48'),(103,'nvtray','','2018-01-23 16:10:48'),(104,'dasHost','','2018-01-23 16:10:48'),(105,'mbamtray','','2018-01-23 16:10:48'),(106,'RtkNGUI64','','2018-01-23 16:10:48'),(107,'HxTsr','','2018-01-23 16:10:48'),(108,'dllhost','','2018-01-23 16:10:48'),(109,'WinZip Compression Smart Monitor Service','','2018-01-23 16:10:48'),(110,'vmnat','','2018-01-23 16:10:48'),(111,'vmware-hostd','','2018-01-23 16:10:48'),(112,'openvpnserv','','2018-01-23 16:10:48'),(113,'SecHealthUI','','2018-01-23 16:10:48'),(114,'taskhost','','2018-01-23 16:10:48'),(115,'armsvc','','2018-01-23 16:10:48'),(116,'GameScannerService','','2018-01-23 16:10:48'),(117,'concentr','','2018-01-23 16:10:48'),(118,'igfxCUIService','','2018-01-23 16:10:48'),(119,'ScriptedSandbox64','','2018-01-23 16:10:48'),(120,'coreServiceShell','','2018-01-23 16:10:48'),(121,'WinStore.App','','2018-01-23 16:10:48'),(122,'LiveUpdate','','2018-01-23 16:10:48'),(123,'SearchFilterHost','','2018-01-23 16:10:48'),(124,'Sync','','2018-01-23 16:10:48'),(125,'GameOverlayUI','','2018-01-23 16:10:48'),(126,'WINWORD','','2018-01-23 16:10:48'),(127,'RzSDKServer','','2018-01-23 16:10:48'),(128,'POWERPNT','','2018-01-23 16:10:48'),(129,'SupportAssistAgent','','2018-01-23 16:10:48'),(130,'NVDisplay.Container','','2018-01-23 16:10:48'),(131,'Calculator','','2018-01-23 16:10:48'),(132,'MBAMService','','2018-01-23 16:10:48'),(133,'msfeedssync','','2018-01-23 16:10:48'),(134,'wininit','','2018-01-23 16:10:48'),(135,'Video.UI','','2018-01-23 16:10:48'),(136,'rundll32','','2018-01-23 16:10:48'),(137,'Discord','','2018-01-23 16:10:48'),(138,'SynTPHelper','','2018-01-23 16:10:48'),(139,'SearchProtocolHost','','2018-01-23 16:10:48'),(140,'Garena','','2018-01-23 16:10:48'),(141,'SamsungMagician','','2018-01-23 16:10:48'),(142,'dirmngr','','2018-01-23 16:10:48'),(143,'System','','2018-01-23 16:10:48'),(144,'HP3DDGService','','2018-01-23 16:10:48'),(145,'init','','2018-01-23 16:10:48'),(146,'sqlbrowser','','2018-01-23 16:10:48'),(147,'NisSrv','','2018-01-23 16:10:48'),(148,'AGSService','','2018-01-23 16:10:48'),(149,'ThumbnailExtractionHost','','2018-01-23 16:10:48'),(150,'IAStorIconLaunch','','2018-01-23 16:10:48'),(151,'GameBarPresenceWriter','','2018-01-23 16:10:48'),(152,'nvsphelper64','','2018-01-23 16:10:48'),(153,'mDNSResponder','','2018-01-23 16:10:48'),(154,'USBChargerPlus','','2018-01-23 16:10:48'),(155,'valWBFPolicyService','','2018-01-23 16:10:48'),(156,'taskhostex','','2018-01-23 16:10:48'),(157,'vmware','','2018-01-23 16:10:48'),(158,'WerFault','','2018-01-23 16:10:48'),(159,'esif_assist_64','','2018-01-23 16:10:48'),(160,'WmiPrvSE','','2018-01-23 16:10:48'),(161,'IntelCpHeciSvc','','2018-01-23 16:10:48'),(162,'IntelliTrace','','2018-01-23 16:10:48'),(163,'vncviewer','','2018-01-23 16:10:48'),(164,'SettingSyncHost','','2018-01-23 16:10:48'),(165,'TiWorker','','2018-01-23 16:10:48'),(166,'Creative Cloud','','2018-01-23 16:10:48'),(167,'EvtEng','','2018-01-23 16:10:48'),(168,'mfevtps','','2018-01-23 16:10:48'),(169,'imstrayicon','','2018-01-23 16:10:48'),(170,'nvnodejslauncher','','2018-01-23 16:10:48'),(171,'WUDFHost','','2018-01-23 16:10:48'),(172,'vmware-usbarbitrator64','','2018-01-23 16:10:48'),(173,'LockApp','','2018-01-23 16:10:48'),(174,'reg','','2018-01-23 16:10:48'),(175,'MSBuild','','2018-01-23 16:10:48'),(176,'conhost','','2018-01-23 16:10:48'),(177,'spoolsv','','2018-01-23 16:10:48'),(178,'msconfig','','2018-01-23 16:10:48'),(179,'KorIME','','2018-01-23 16:10:48'),(180,'NvOAWrapperCache','','2018-01-23 16:10:48'),(181,'OfficeClickToRun','','2018-01-23 16:10:48'),(182,'script-fu','','2018-01-23 16:10:48'),(183,'NvStreamService','','2018-01-23 16:10:48'),(184,'WDSyncService','','2018-01-23 16:10:48'),(185,'sftp','','2018-01-23 16:10:48'),(186,'gxxsvc','','2018-01-23 16:10:48'),(187,'openvpn-gui','','2018-01-23 16:10:48'),(188,'Battle.net Helper','','2018-01-23 16:10:48'),(189,'HxOutlook','','2018-01-23 16:10:48'),(190,'SteelSeriesEngine3','','2018-01-23 16:10:48'),(191,'wlanext','','2018-01-23 16:10:48'),(192,'RtkAudioService64','','2018-01-23 16:10:48'),(193,'SelfServicePlugin','','2018-01-23 16:10:48'),(194,'unsecapp','','2018-01-23 16:10:48'),(195,'vmware-authd','','2018-01-23 16:10:48'),(196,'wfcrun32','','2018-01-23 16:10:48'),(197,'StandardCollector.Service','','2018-01-23 16:10:48'),(198,'fontdrvhost','','2018-01-23 16:10:48'),(199,'RAVBg64','','2018-01-23 16:10:48'),(200,'Adobe Desktop Service','','2018-01-23 16:10:48'),(201,'ServiceHub.IdentityHost','','2018-01-23 16:10:48'),(202,'RzSDKService','','2018-01-23 16:10:48'),(203,'sqlwriter','','2018-01-23 16:10:48'),(204,'DellUpTray','','2018-01-23 16:10:48'),(205,'SystemSettings','','2018-01-23 16:10:48'),(206,'lsass','','2018-01-23 16:10:48'),(207,'sihost','','2018-01-23 16:10:48'),(208,'Receiver','','2018-01-23 16:10:48'),(209,'SelfService','','2018-01-23 16:10:48'),(210,'uaclauncher','','2018-01-23 16:10:48'),(211,'ServiceHub.RoslynCodeAnalysisService32','','2018-01-23 16:10:48'),(212,'OfficeHubTaskHost','','2018-01-23 16:10:48'),(213,'ATKOSD2','','2018-01-23 16:10:48'),(214,'vpnagent','','2018-01-23 16:10:48'),(215,'AuthManSvr','','2018-01-23 16:10:48'),(216,'Agent','','2018-01-23 16:10:48'),(217,'vmnetdhcp','','2018-01-23 16:10:48'),(218,'sppsvc','','2018-01-23 16:10:48'),(219,'CoolSense','','2018-01-23 16:10:48'),(220,'UMonit64','','2018-01-23 16:10:48'),(221,'explorer','','2018-01-23 16:10:48'),(222,'RdrCEF','','2018-01-23 16:10:48'),(223,'smss','','2018-01-23 16:10:48'),(224,'HPNetworkCommunicator','','2018-01-23 16:10:48'),(225,'jucheck','','2018-01-23 16:10:48'),(226,'SearchIndexer','','2018-01-23 16:10:48'),(227,'nvcontainer','','2018-01-23 16:10:48'),(228,'services','','2018-01-23 16:10:48'),(229,'esif_uf','','2018-01-23 16:10:48'),(230,'XperiaCompanionService','','2018-01-23 16:10:48'),(231,'winlogon','','2018-01-23 16:10:48'),(232,'Microsoft.Photos','','2018-01-23 16:10:48'),(233,'UcMapi','','2018-01-23 16:10:48'),(234,'PwmSvc','','2018-01-23 16:10:48'),(235,'WzPreloader','','2018-01-23 16:10:48'),(236,'HPCustPartic','','2018-01-23 16:10:48'),(237,'notepad','','2018-01-23 16:10:48'),(238,'coreFrameworkHost','','2018-01-23 16:10:48'),(239,'Shadowverse','','2018-01-23 16:10:48'),(240,'PresentationFontCache','','2018-01-23 16:10:48'),(241,'MsMpEng','','2018-01-23 16:10:48'),(242,'gs-server','','2018-01-23 16:10:48'),(243,'HControl','','2018-01-23 16:10:48'),(244,'DbxSvc','','2018-01-23 16:10:48'),(245,'ConfigurationWizard','','2018-01-23 16:10:48'),(246,'ctfmon','','2018-01-23 16:10:48'),(247,'vmware-unity-helper','','2018-01-23 16:10:48'),(248,'AsusTPLoader','','2018-01-23 16:10:48'),(249,'TmsaInstance64','','2018-01-23 16:10:48'),(250,'Memory Compression','','2018-01-23 16:10:48'),(251,'msiexec','','2018-01-23 16:10:48'),(252,'Battle.net','','2018-01-23 16:10:48'),(253,'MySQLNotifier','','2018-01-23 16:10:48'),(254,'mysqld','','2018-01-23 16:10:48'),(255,'wermgr','','2018-01-23 16:10:48'),(256,'vmware-tray','','2018-01-23 16:10:48'),(257,'Carbon','','2018-01-23 16:10:48'),(258,'jusched','','2018-01-23 16:10:48'),(259,'IAStorDataMgrSvc','','2018-01-23 16:10:48'),(260,'lync','','2018-01-23 16:10:48'),(261,'taskeng','','2018-01-23 16:10:48'),(262,'Defrag','','2018-01-23 16:10:48'),(263,'backgroundTaskHost','','2018-01-23 16:10:48'),(264,'ZeroConfigService','','2018-01-23 16:10:48'),(265,'BhcMgr','','2018-01-23 16:10:48'),(266,'SecurityHealthService','','2018-01-23 16:10:48'),(267,'PtSessionAgent','','2018-01-23 16:10:48'),(268,'HPSupportSolutionsFrameworkService','','2018-01-23 16:10:48'),(269,'cmd','','2018-01-23 16:10:48'),(270,'CompatTelRunner','','2018-01-23 16:10:48'),(271,'vmware-vmx','','2018-01-23 16:10:48'),(272,'NvBackend','','2018-01-23 16:10:48'),(273,'ScanToPCActivationApp','','2018-01-23 16:10:48'),(274,'TouchpointAnalyticsClientService','','2018-01-23 16:10:48'),(275,'NVIDIA Share','','2018-01-23 16:10:48'),(276,'CCXProcess','','2018-01-23 16:10:48'),(277,'nvvsvc','','2018-01-23 16:10:48'),(278,'AdobeARM','','2018-01-23 16:10:48'),(279,'node','','2018-01-23 16:10:48'),(280,'DptfParticipantProcessorService','','2018-01-23 16:10:48'),(281,'SpotifyWebHelper','','2018-01-23 16:10:48'),(282,'PerfWatson2','','2018-01-23 16:10:48'),(283,'bash','','2018-01-23 16:10:48'),(284,'provtool','','2018-01-23 16:10:48'),(285,'HPWMISVC','','2018-01-23 16:10:48'),(286,'CoreSync','','2018-01-23 16:10:48'),(287,'DellUpService','','2018-01-23 16:10:48'),(288,'reader_sl','','2018-01-23 16:10:48'),(289,'SnippingTool','','2018-01-23 16:10:48'),(290,'igfxTray','','2018-01-23 16:10:48'),(291,'DropboxUpdate','','2018-01-23 16:10:48'),(292,'IAStorIcon','','2018-01-23 16:10:48'),(293,'SkypeHost','','2018-01-23 16:10:48'),(294,'Microsoft.Notes','','2018-01-23 16:10:48'),(295,'WWAHost','','2018-01-23 16:10:48'),(296,'mspaint','','2018-01-23 16:10:48'),(297,'Adobe CEF Helper','','2018-01-23 16:10:48'),(298,'GoogleCrashHandler','','2018-01-23 16:10:48'),(299,'FAHWindow64','','2018-01-23 16:10:48'),(300,'hpwuschd2','','2018-01-23 16:10:48'),(301,'AsusTPCenter','','2018-01-23 16:10:48');
/*!40000 ALTER TABLE `t_trustedprocess` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `t_userprocess`
--

DROP TABLE IF EXISTS `t_userprocess`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `t_userprocess` (
  `c_id` int(11) NOT NULL AUTO_INCREMENT,
  `c_processname` varchar(100) NOT NULL,
  `c_approvedcount` int(11) DEFAULT '0',
  `c_denycount` int(11) DEFAULT '0',
  PRIMARY KEY (`c_id`)
) ENGINE=InnoDB AUTO_INCREMENT=324 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_userprocess`
--

LOCK TABLES `t_userprocess` WRITE;
/*!40000 ALTER TABLE `t_userprocess` DISABLE KEYS */;
INSERT INTO `t_userprocess` VALUES (1,'ScriptedSandbox64',95,0),(2,'ServiceHub.Host.CLR.x86',97,0),(3,'igfxTray',113,0),(4,'nvsphelper64',1,0),(5,'wininit',121,0),(6,'Win32DiskImager',2,0),(7,'smss',121,0),(8,'Sync',121,0),(9,'ChsIME',120,0),(10,'iprodifx',1,0),(11,'nvvsvc',10,0),(12,'GoogleUpdate',29,0),(13,'coreFrameworkHost',103,0),(14,'AdobeIPCBroker',1,0),(15,'pcdrsysinfosoftware.p5x',1,0),(16,'SynTPEnhService',112,0),(17,'mspaint',16,0),(18,'WMIADAP',26,0),(19,'TouchpointAnalyticsClientService',103,0),(20,'WDSyncService',103,0),(21,'Calculator',27,0),(22,'WZUpdateNotifier',1,0),(23,'hpwuschd2',103,0),(24,'mDNSResponder',9,0),(25,'SelfServicePlugin',82,0),(26,'WSCStatusController',1,0),(27,'SearchUI',114,0),(28,'DellDataVaultWiz',9,0),(29,'IpOverUsbSvc',9,0),(30,'Microsoft.Photos',38,0),(31,'DMedia',1,0),(32,'WerFault',1,0),(33,'PresentationFontCache',120,0),(34,'ScanToPCActivationApp',103,0),(35,'ACMON',7,0),(36,'ApplicationFrameHost',88,0),(37,'taskhostw',114,0),(38,'NVDisplay.Container',104,0),(39,'PtSessionAgent',103,0),(40,'LogonUI',4,0),(41,'smartscreen',90,0),(42,'PerfWatson2',97,0),(43,'IntelCpHeciSvc',104,0),(44,'PeopleExperienceHost',7,0),(45,'IntelCpHDCPSvc',1,0),(46,'WmiApSrv',31,0),(47,'Carbon',1,0),(48,'HPNetworkCommunicator',76,0),(49,'conhost',116,0),(50,'MBAMService',9,0),(51,'iexplore',1,0),(52,'SnippingTool',6,0),(53,'HControl',1,0),(54,'TmsaInstance64',103,0),(55,'vmware-tray',110,0),(56,'EvtEng',1,0),(57,'taskeng',4,0),(58,'SystemSettings',31,0),(59,'InstallPsm',1,0),(60,'winlogon',121,0),(61,'AsBhcSrv',1,0),(62,'WinZipCompressionSmartMonitor',8,0),(63,'uiSeAgnt',103,0),(64,'cmd',104,0),(65,'Sonos',3,0),(66,'PING',2,0),(67,'DellDataVault',9,0),(68,'studio64',1,0),(69,'WDAppManager',103,0),(70,'vpnui',103,0),(71,'ServiceHub.VSDetouredHost',97,0),(72,'NVIDIA Web Helper',104,0),(73,'GameScannerService',1,0),(74,'KorIME',7,0),(75,'UMonit64',7,0),(76,'MSBuild',97,0),(77,'CoreSync',1,0),(78,'VBCSCompiler',95,0),(79,'vmnetdhcp',121,0),(80,'reader_sl',42,0),(81,'sqlbrowser',9,0),(82,'Microsoft.Notes',114,0),(83,'devenv',97,0),(84,'Hearthstone',1,0),(85,'fsnotifier64',1,0),(86,'GamePanel',1,0),(87,'HPMSGSVC',103,0),(88,'AGSService',10,0),(89,'PtSvcHost',103,0),(90,'NvTelemetryContainer',104,0),(91,'VSSVC',18,0),(92,'lynchtmlconv',23,0),(93,'cleanmgr',1,0),(94,'RdrCEF',4,0),(95,'AsusTPCenter',8,0),(96,'TabTip',1,0),(97,'SettingSyncHost',104,0),(98,'SecHealthUI',1,0),(99,'XboxIdp',1,0),(100,'NVIDIA Share',1,0),(101,'certutil',1,0),(102,'flux',9,0),(103,'ServiceHub.DataWarehouseHost',95,0),(104,'ServiceHub.IdentityHost',97,0),(105,'mmc',1,0),(106,'msfeedssync',19,0),(107,'init',19,0),(108,'ServiceHub.Host.Node.x86',97,0),(109,'Creative Cloud',1,0),(110,'sftp',14,0),(111,'jucheck',51,0),(112,'wlanext',8,0),(113,'RAVBg64',10,0),(114,'eclipse',4,0),(115,'AdobeUpdateService',10,0),(116,'WinZip Compression Smart Monitor Service',8,0),(117,'steamwebhelper',4,0),(118,'HPWMISVC',103,0),(119,'SearchIndexer',121,0),(120,'concentr',82,0),(121,'LockApp',44,0),(122,'GoogleCrashHandler64',103,0),(123,'services',121,0),(124,'MpCmdRun',4,0),(125,'ibtsiva',113,0),(126,'AppleMobileDeviceService',9,0),(127,'script-fu',2,0),(128,'adb',1,0),(129,'WiFi_20.10.2_PROSet64_Win10',1,0),(130,'CCXProcess',1,0),(131,'RegSrvc',1,0),(132,'TiWorker',25,0),(133,'RtkAudioService64',9,0),(134,'esif_uf',1,0),(135,'ServiceHub.SettingsHost',97,0),(136,'SpeechRuntime',1,0),(137,'SpotifyWebHelper',1,0),(138,'nvxdsync',10,0),(139,'PtWatchDog',103,0),(140,'TrustedInstaller',25,0),(141,'GoogleCrashHandler',103,0),(142,'Memory Compression',114,0),(143,'jusched',110,0),(144,'RzSDKServer',1,0),(145,'taskhostex',7,0),(146,'NvOAWrapperCache',22,0),(147,'SynTPEnh',112,0),(148,'WUDFHost',113,0),(149,'uiWatchDog',103,0),(150,'Defrag',4,0),(151,'WINWORD',78,0),(152,'armsvc',112,0),(153,'mysqld',9,0),(154,'AuthManSvr',13,0),(155,'stacsv64',121,0),(156,'WmiPrvSE',113,0),(157,'SearchFilterHost',108,0),(158,'WzPreloader',8,0),(159,'Battle.net Helper',1,0),(160,'OneDrive',92,0),(161,'DptfParticipantProcessorService',9,0),(162,'igfxCUIService',121,0),(163,'backgroundTaskHost',64,0),(164,'IAStorIconLaunch',2,0),(165,'Idle',121,0),(166,'LiveUpdate',4,0),(167,'WWAHost',4,0),(168,'provtool',2,0),(169,'ctfmon',112,0),(170,'nvcontainer',104,0),(171,'UsoClient',1,0),(172,'unsecapp',105,0),(173,'bash',19,0),(174,'splwow64',23,0),(175,'sttray64',103,0),(176,'MsMpEng',11,0),(177,'HxTsr',48,0),(178,'HPSupportSolutionsFrameworkService',103,0),(179,'svchost',121,0),(180,'prodreg',4,0),(181,'gxxsvc',17,0),(182,'vmware-authd',114,0),(183,'reg',21,0),(184,'msoia',1,0),(185,'bcastdvr',16,0),(186,'explorer',121,0),(187,'ielowutil',1,0),(188,'Steam',4,0),(189,'IAStorDataMgrSvc',9,0),(190,'ThumbnailExtractionHost',2,0),(191,'igfxHK',120,0),(192,'Shadowverse',2,0),(193,'ATKOSD2',1,0),(194,'DellUpService',9,0),(195,'lync',23,0),(196,'LMS',10,0),(197,'uaclauncher',2,0),(198,'FAHWindow64',8,0),(199,'hpqwmiex',103,0),(200,'iWrap',1,0),(201,'GameBarPresenceWriter',2,0),(202,'MSASCuiL',114,0),(203,'mbamtray',9,0),(204,'DbxSvc',9,0),(205,'ServiceHub.RoslynCodeAnalysisService32',97,0),(206,'gs-server',103,0),(207,'vmware-usbarbitrator64',114,0),(208,'BhcMgr',1,0),(209,'dirmngr',9,0),(210,'XperiaCompanionService',9,0),(211,'lsass',121,0),(212,'wermgr',22,0),(213,'SecurityHealthService',114,0),(214,'SelfService',68,0),(215,'vmware-unity-helper',3,0),(216,'DismHost',1,0),(217,'vncviewer',2,0),(218,'openvpn-gui',103,0),(219,'vmnat',121,0),(220,'AsPatchTouchPanel64',7,0),(221,'HxOutlook',47,0),(222,'Battle.net',1,0),(223,'UcMapi',23,0),(224,'ZeroConfigService',1,0),(225,'rundll32',6,0),(226,'Adobe Desktop Service',1,0),(227,'Agent',1,0),(228,'sqlservr',8,0),(229,'SkypeHost',113,0),(230,'OfficeHubTaskHost',21,0),(231,'CoolSense',103,0),(232,'igfxEM',121,0),(233,'AsusTPHelper',8,0),(234,'pcdrcui',1,0),(235,'SamsungMagician',103,0),(236,'DellUpTray',9,0),(237,'Adobe CEF Helper',1,0),(238,'mbam',1,0),(239,'TabTip32',1,0),(240,'msvsmon',85,0),(241,'DropboxUpdate',9,0),(242,'WinStore.App',37,0),(243,'nvnodejslauncher',20,0),(244,'vmware-vmx',3,0),(245,'MySQLNotifier',1,0),(246,'wfica32',82,0),(247,'Video.UI',25,0),(248,'NvStreamService',1,0),(249,'RAVCpl64',1,0),(250,'taskhost',7,0),(251,'dllhost',115,0),(252,'jhi_service',10,0),(253,'Garena',1,0),(254,'GameOverlayUI',2,0),(255,'Receiver',82,0),(256,'SystemSettingsBroker',13,0),(257,'SteelSeriesEngine3',9,0),(258,'ToolbarNativeMsgHost',101,0),(259,'IntelliTrace',86,0),(260,'RtkNGUI64',9,0),(261,'imstrayicon',6,0),(262,'wfcrun32',82,0),(263,'esif_assist_64',1,0),(264,'sppsvc',27,0),(265,'USBChargerPlus',8,0),(266,'ShellExperienceHost',114,0),(267,'NvBackend',17,0),(268,'StandardCollector.Service',94,0),(269,'CompatTelRunner',19,0),(270,'audiodg',121,0),(271,'valWBFPolicyService',103,0),(272,'OfficeClickToRun',121,0),(273,'node',1,0),(274,'dwm',121,0),(275,'Setup',1,0),(276,'SteamService',4,0),(277,'DCCService',9,0),(278,'openvpnserv',103,0),(279,'fontdrvhost',114,0),(280,'IAStorIcon',9,0),(281,'AcroRd32',4,0),(282,'PwmSvc',103,0),(283,'SynTPHelper',112,0),(284,'coreServiceShell',103,0),(285,'SearchProtocolHost',111,0),(286,'ConfigurationWizard',6,0),(287,'SupportAssistAgent',9,0),(288,'AsLdrSrv',1,0),(289,'spoolsv',121,0),(290,'csrss',121,0),(291,'RzSDKService',1,0),(292,'wifitask',1,0),(293,'NvNetworkService',9,0),(294,'sqlwriter',9,0),(295,'AsusSmartGestureDetector64',7,0),(296,'notepad',22,0),(297,'msconfig',0,0),(298,'HPCustPartic',17,0),(299,'gimp-2.8',2,0),(300,'vpnagent',112,0),(301,'mfevtps',7,0),(302,'RuntimeBroker',118,0),(303,'pythonw',25,0),(304,'HP3DDGService',103,0),(305,'DeliveryService',1,0),(306,'AsusTPLoader',8,0),(307,'chrome',118,0),(308,'msiexec',22,0),(309,'AdobeARM',1,0),(310,'System',121,0),(311,'NisSrv',11,0),(312,'POWERPNT',33,0),(313,'vmware-hostd',132,0),(314,'nvtray',10,0),(315,'OfficeC2RClient',2,0),(316,'quickset',9,0),(317,'Discord',1,0),(318,'Taskmgr',8,0),(319,'vmware',3,0),(320,'sihost',114,0),(321,'ssh',19,0);
/*!40000 ALTER TABLE `t_userprocess` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `t_users`
--

DROP TABLE IF EXISTS `t_users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `t_users` (
  `c_id` int(11) NOT NULL AUTO_INCREMENT,
  `c_username` varchar(100) NOT NULL,
  `c_password` varchar(128) NOT NULL,
  `c_role` varchar(10) DEFAULT 'user',
  `c_noofmonths` int(11) DEFAULT '6',
  `c_directorysize` bigint(20) DEFAULT '0',
  `c_salt` varchar(50) DEFAULT NULL,
  `c_hash` varchar(255) DEFAULT NULL,
  `c_attempts` int(11) DEFAULT '0',
  `c_sessionkey` varchar(50) DEFAULT NULL,
  `c_roomname` varchar(100) NOT NULL,
  PRIMARY KEY (`c_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_users`
--

LOCK TABLES `t_users` WRITE;
/*!40000 ALTER TABLE `t_users` DISABLE KEYS */;
INSERT INTO `t_users` VALUES (1, 'admin','C7AD44CBAD762A5DA0A452F9E854FDC1E0E7A52A38015F23F3EAB1D80B931DD472634DFAC71CD34EBC35D16AB7FB8A90C81F975113D6C7538DC69DD8DE9077EC','admin',6,0,NULL,NULL,0,NULL,'');
/*!40000 ALTER TABLE `t_users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-02-03 13:13:05
