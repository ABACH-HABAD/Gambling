-- MySQL dump 10.13  Distrib 8.0.44, for Win64 (x86_64)
--
-- Host: localhost    Database: gambling
-- ------------------------------------------------------
-- Server version	8.2.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `blackjack_cards`
--

DROP TABLE IF EXISTS `blackjack_cards`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `blackjack_cards` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `GameId` int NOT NULL,
  `Denomination` longtext NOT NULL,
  `Suit` varchar(1) NOT NULL,
  `InPlayerHand` tinyint(1) NOT NULL,
  `IsOpen` tinyint(1) NOT NULL,
  `BlackjackGameId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Blackjack_cards_BlackjackGameId` (`BlackjackGameId`),
  CONSTRAINT `FK_Blackjack_cards_Blackjack_games_BlackjackGameId` FOREIGN KEY (`BlackjackGameId`) REFERENCES `blackjack_games` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `blackjack_cards`
--

LOCK TABLES `blackjack_cards` WRITE;
/*!40000 ALTER TABLE `blackjack_cards` DISABLE KEYS */;
/*!40000 ALTER TABLE `blackjack_cards` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `blackjack_games`
--

DROP TABLE IF EXISTS `blackjack_games`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `blackjack_games` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `Bet` double NOT NULL,
  `CanPlayerMove` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Blackjack_games_UserId` (`UserId`),
  CONSTRAINT `FK_Blackjack_games_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `blackjack_games`
--

LOCK TABLES `blackjack_games` WRITE;
/*!40000 ALTER TABLE `blackjack_games` DISABLE KEYS */;
/*!40000 ALTER TABLE `blackjack_games` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `device_types`
--

DROP TABLE IF EXISTS `device_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `device_types` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `device_types`
--

LOCK TABLES `device_types` WRITE;
/*!40000 ALTER TABLE `device_types` DISABLE KEYS */;
INSERT INTO `device_types` VALUES (1,'Windows'),(2,'Приложение администратора'),(3,'Android'),(4,'Ios'),(5,'Браузер');
/*!40000 ALTER TABLE `device_types` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `game_types`
--

DROP TABLE IF EXISTS `game_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `game_types` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `game_types`
--

LOCK TABLES `game_types` WRITE;
/*!40000 ALTER TABLE `game_types` DISABLE KEYS */;
INSERT INTO `game_types` VALUES (1,'слоты'),(2,'блекджек'),(3,'рулетка');
/*!40000 ALTER TABLE `game_types` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `games`
--

DROP TABLE IF EXISTS `games`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `games` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PlayerId` int NOT NULL,
  `GameTypeId` int NOT NULL,
  `Bet` double NOT NULL,
  `IsWin` tinyint(1) NOT NULL,
  `WinAmount` double NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Games_GameTypeId` (`GameTypeId`),
  KEY `FK_Games_Games_PlayerId_idx` (`PlayerId`),
  CONSTRAINT `FK_Games_Game_types_GameTypeId` FOREIGN KEY (`GameTypeId`) REFERENCES `game_types` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Games_Games_PlayerId` FOREIGN KEY (`PlayerId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=389 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `games`
--

LOCK TABLES `games` WRITE;
/*!40000 ALTER TABLE `games` DISABLE KEYS */;
INSERT INTO `games` VALUES (1,12,1,50,0,0),(2,12,3,0,0,0),(3,12,3,0,0,0),(4,12,3,0,0,0),(5,12,3,0,0,0),(6,12,3,0,0,0),(7,12,3,0,0,0),(8,12,3,0,0,0),(9,12,3,0,0,0),(10,12,3,0,0,0),(11,12,3,0,0,0),(12,12,3,0,0,0),(13,12,3,0,0,0),(14,12,3,0,0,0),(15,12,3,0,0,0),(16,12,3,0,0,0),(17,12,3,0,0,0),(18,12,3,0,0,0),(19,12,3,0,0,0),(20,12,3,0,0,0),(21,12,3,0,0,0),(22,12,3,0,0,0),(23,12,3,0,0,0),(24,12,3,0,0,0),(25,12,3,0,0,0),(26,12,3,0,0,0),(27,12,3,0,0,0),(28,12,3,0,0,0),(29,12,3,0,0,0),(30,12,3,0,0,0),(31,12,3,0,0,0),(32,12,3,0,0,0),(33,12,3,0,0,0),(34,12,3,0,0,0),(35,12,3,0,0,0),(36,12,3,0,0,0),(37,12,3,0,0,0),(38,12,3,0,0,0),(39,12,3,0,0,0),(40,12,3,0,0,0),(41,12,3,0,0,0),(42,12,3,0,0,0),(43,12,3,0,0,0),(44,12,3,0,0,0),(45,12,3,0,0,0),(46,12,3,0,0,0),(47,12,3,0,0,0),(48,12,3,0,0,0),(49,12,3,0,0,0),(50,12,3,0,0,0),(51,12,3,0,0,0),(52,12,3,0,0,0),(53,12,3,0,0,0),(54,12,3,0,0,0),(55,12,3,0,0,0),(56,12,3,0,0,0),(57,12,3,0,0,0),(58,12,3,0,0,0),(59,12,3,0,0,0),(60,12,3,0,0,0),(61,12,3,0,0,0),(62,12,3,0,0,0),(63,12,3,0,0,0),(64,12,3,0,0,0),(65,12,3,0,0,0),(66,12,3,0,0,0),(67,12,3,0,0,0),(68,12,3,0,0,0),(69,12,3,0,0,0),(70,12,3,0,0,0),(71,12,3,0,0,0),(72,12,3,0,0,0),(73,12,3,0,0,0),(74,12,3,0,0,0),(75,12,3,0,0,0),(76,12,3,0,0,0),(77,12,3,0,0,0),(78,12,3,0,0,0),(79,12,3,0,0,0),(80,12,3,0,0,0),(81,12,3,0,0,0),(82,12,3,0,0,0),(83,12,3,0,0,0),(84,12,3,0,0,0),(85,12,3,0,0,0),(86,12,3,0,0,0),(87,12,3,0,0,0),(88,12,3,0,0,0),(89,12,3,0,0,0),(90,12,3,0,0,0),(91,12,3,0,0,0),(92,12,3,0,0,0),(93,12,3,0,0,0),(94,12,3,0,0,0),(95,12,3,0,0,0),(96,12,3,0,0,0),(97,12,3,0,0,0),(98,12,3,0,0,0),(99,12,3,0,0,0),(100,12,3,0,0,0),(101,12,3,0,0,0),(102,12,3,0,0,0),(103,12,3,0,0,0),(104,12,3,0,0,0),(105,12,3,0,0,0),(106,12,3,0,0,0),(107,12,3,0,0,0),(108,12,3,0,0,0),(109,12,3,0,0,0),(110,12,3,0,0,0),(111,12,3,0,0,0),(112,12,3,0,0,0),(113,12,3,0,0,0),(114,12,3,0,0,0),(115,12,3,0,0,0),(116,12,3,0,0,0),(117,12,3,0,0,0),(118,12,3,0,0,0),(119,12,3,0,0,0),(120,12,3,0,0,0),(121,12,3,0,0,0),(122,12,3,0,0,0),(123,12,3,0,0,0),(124,12,3,0,0,0),(125,12,3,0,0,0),(126,12,3,0,0,0),(127,12,3,0,0,0),(128,12,3,0,0,0),(129,12,3,0,0,0),(130,12,3,0,0,0),(131,12,3,0,0,0),(132,12,3,0,0,0),(133,12,3,5,0,0),(134,12,3,0,0,0),(135,12,3,5,0,0),(136,12,3,0,0,0),(137,12,3,5,0,0),(138,12,3,0,0,0),(139,12,3,5,0,0),(140,12,3,5,0,0),(141,12,3,5,0,0),(142,12,3,5,0,0),(143,12,3,5,0,0),(144,12,3,5,0,0),(145,12,3,5,0,0),(146,12,3,5,0,0),(147,12,3,5,0,0),(148,12,3,5,1,10),(149,12,3,5,0,0),(150,12,3,50,0,0),(151,12,3,50,0,0),(152,12,3,50,0,0),(153,12,3,50,0,0),(154,12,3,50,0,0),(155,12,3,50,0,0),(156,12,3,50,1,100),(157,12,3,50,0,0),(158,12,3,0,0,0),(159,12,3,50,0,0),(160,12,3,50,1,100),(161,12,3,50,0,0),(162,12,3,50,0,0),(163,12,3,50,1,100),(164,12,3,200,1,300),(165,12,3,50,1,100),(166,12,3,50,0,0),(167,12,3,50,0,0),(168,12,3,720,0,0),(169,12,3,50,0,0),(170,12,3,50,0,0),(171,12,3,50,1,100),(172,12,3,50,1,100),(173,12,3,50,0,0),(174,12,3,50,0,0),(175,12,3,50,1,100),(176,12,3,50,1,100),(177,12,3,50,1,100),(178,12,3,50,0,0),(179,12,3,50,0,0),(180,12,3,80,1,160),(181,12,3,100,1,200),(182,12,3,80,1,160),(183,12,3,10000,0,0),(184,12,3,50,0,0),(185,12,3,50,0,0),(186,12,3,50,0,0),(187,12,3,50,0,0),(188,12,3,50,0,0),(189,12,3,50,0,0),(190,12,3,50,1,100),(191,12,3,100,0,0),(192,12,3,500,0,0),(193,12,3,5,1,10),(194,12,1,50,0,0),(195,12,3,50,1,100),(196,12,1,50,0,0),(197,12,1,50,0,0),(198,12,1,50,0,0),(199,12,1,50,0,0),(200,12,1,50,0,0),(201,12,1,50,0,0),(202,12,1,50,0,0),(203,12,1,50,0,0),(204,12,1,50,0,0),(205,12,1,50,0,0),(206,12,1,50,0,0),(207,12,1,50,0,0),(208,12,1,50,0,0),(209,12,1,50,1,750),(210,12,3,250,1,100),(211,12,3,100,1,200),(212,12,3,50,0,0),(213,12,1,500,0,0),(214,12,1,500,0,0),(215,12,1,500,0,0),(216,12,1,500,0,0),(217,12,1,500,0,0),(218,12,1,500,0,0),(219,12,3,50,0,0),(220,12,1,50,0,0),(221,12,1,50,0,0),(222,12,1,50,1,750),(223,12,1,50,0,0),(224,12,1,50,0,0),(225,12,3,500,0,0),(226,12,3,20,1,40),(227,12,3,10000,1,10000),(228,12,3,10000,1,10000),(229,12,3,56,0,0),(230,12,3,50,0,0),(231,12,3,50,1,100),(232,12,3,50,0,0),(233,12,3,20,0,0),(234,12,3,30,1,190),(235,12,3,100,1,100),(236,12,3,100,1,100),(237,12,3,1000,1,1000),(238,12,3,100,1,100),(239,12,3,0,0,0),(240,12,1,50,0,0),(241,12,1,50,1,750),(242,12,3,50,1,150),(243,12,3,1000,1,1000),(244,12,3,1000,1,1000),(245,12,3,1000,1,1000),(246,12,3,500,1,1000),(247,12,3,500,1,1000),(248,12,3,0,0,0),(249,12,3,0,0,0),(250,12,1,5000,0,0),(251,12,1,5000,1,75000),(252,12,1,5000,0,0),(253,12,1,500,0,0),(254,12,1,500,0,0),(255,12,1,500,0,0),(256,12,1,500,0,0),(257,12,1,500,0,0),(258,12,1,500,0,0),(259,12,1,500,0,0),(260,12,1,500,0,0),(261,12,1,500,0,0),(262,12,1,500,0,0),(263,12,1,500,0,0),(264,12,1,500,0,0),(265,12,1,500,0,0),(266,12,1,500,0,0),(267,12,1,500,0,0),(268,12,1,500,0,0),(269,12,3,50,0,0),(270,12,3,10,0,0),(271,12,1,50,0,0),(272,12,1,50,0,0),(273,12,1,50,0,0),(274,12,1,50,0,0),(275,12,3,5000,0,0),(276,12,3,50,0,0),(277,12,3,0,0,0),(278,12,1,50,0,0),(279,12,1,50,0,0),(280,12,1,500,1,7500),(281,12,3,500,0,0),(282,12,3,500,0,0),(283,12,3,500,0,0),(284,12,3,500,0,0),(285,12,3,500,1,1000),(286,12,1,5000,0,0),(287,12,1,5000,0,0),(288,12,1,5000,0,0),(289,12,3,1000,1,1000),(290,12,3,500,0,0),(291,12,3,500,0,0),(292,12,3,0,0,0),(293,12,1,50,0,0),(294,12,1,50,0,0),(295,12,1,50,0,0),(296,12,1,3000,0,0),(297,12,1,3000,1,45000),(298,12,3,2000,0,0),(299,12,1,1000,0,0),(300,12,1,100,0,0),(301,12,1,100,1,1500),(302,12,3,1000,1,2000),(303,12,1,586935,0,0),(304,12,1,293468,0,0),(305,12,1,5000,0,0),(306,12,1,5000,0,0),(307,12,1,5000,1,75000),(308,12,1,5000,0,0),(309,12,1,5000,1,75000),(310,12,1,423517,0,0),(311,12,1,423517,0,0),(312,12,1,423517,0,0),(313,12,3,0,0,0),(314,12,1,1000,0,0),(315,12,1,1000,1,30000),(316,12,3,500,0,0),(317,12,1,16704,1,250560),(318,12,3,5,0,0),(319,12,3,5,0,0),(320,12,3,5,0,0),(321,12,3,5,0,0),(322,12,3,1000,0,0),(323,12,3,1000,0,0),(324,12,3,1000,0,0),(325,12,3,2000,1,2000),(326,12,1,1000,1,15000),(327,12,1,1000,0,0),(328,12,1,1000,0,0),(329,12,1,500,0,0),(330,12,1,500,0,0),(331,12,3,1000,0,0),(332,12,3,200,0,0),(333,12,3,100,0,0),(334,12,3,100,1,200),(335,12,1,500,0,0),(336,12,1,315144,0,0),(337,12,1,315144,0,0),(338,12,1,315144,0,0),(339,12,1,315144,0,0),(340,12,1,315144,1,4727160),(341,12,1,315144,0,0),(342,12,1,315144,1,9454320),(343,12,1,315144,0,0),(344,12,1,315144,0,0),(345,12,1,500,0,0),(346,12,1,100,0,0),(347,10,1,1,0,0),(348,10,1,1,0,0),(349,10,3,1,0,0),(350,10,1,1,0,0),(351,10,3,1,0,0),(352,10,1,100,0,0),(353,10,1,100,0,0),(354,10,3,100,0,0),(355,10,3,100,1,200),(356,10,3,100,0,0),(357,10,3,100,0,0),(358,10,3,100,1,200),(359,10,3,100,0,0),(360,10,3,100,1,300),(361,10,3,100,0,0),(362,10,3,100,1,200),(363,10,3,100,0,0),(364,10,3,100,0,0),(365,10,3,100,0,0),(366,12,2,500,0,0),(367,12,2,500,1,1250),(368,12,2,500,0,0),(369,12,2,500,1,1250),(370,12,2,500,0,0),(371,12,2,500,0,0),(372,12,2,500,0,0),(373,12,2,500,0,0),(374,12,2,500,1,1250),(375,12,2,500,1,500),(376,12,2,500,1,1000),(377,12,1,100,0,0),(378,12,2,100,0,0),(379,12,3,100,0,0),(380,12,1,100,0,0),(381,12,2,100,1,200),(382,12,3,100,1,200),(383,12,3,900,0,0),(384,12,3,900,0,0),(385,12,3,400,0,0),(386,12,1,500,0,0),(387,12,3,400,0,0),(388,12,1,100,0,0);
/*!40000 ALTER TABLE `games` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `promotional_codes`
--

DROP TABLE IF EXISTS `promotional_codes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `promotional_codes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Code` longtext NOT NULL,
  `Use` int NOT NULL,
  `InterestBonus` int NOT NULL,
  `QuantitativeBonus` int NOT NULL,
  `FreeSpins` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `promotional_codes`
--

LOCK TABLES `promotional_codes` WRITE;
/*!40000 ALTER TABLE `promotional_codes` DISABLE KEYS */;
INSERT INTO `promotional_codes` VALUES (1,'AAAA',2,3,0,0),(2,'AAAB',4,0,100,0),(3,'AAAC',4,0,0,5),(4,'AAAD',5,3,100,0),(5,'AAAE',5,3,0,5),(6,'AAAF',5,0,100,5),(7,'AAAG',5,3,100,5),(8,'AAAH',0,3,5,5),(9,'AAAI',5,0,0,0);
/*!40000 ALTER TABLE `promotional_codes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `promotional_codes_activates`
--

DROP TABLE IF EXISTS `promotional_codes_activates`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `promotional_codes_activates` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PromotionalCodeId` int NOT NULL,
  `UserId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Promotional_codes_activates_PromotionalCodeId` (`PromotionalCodeId`),
  KEY `IX_Promotional_codes_activates_UserId` (`UserId`),
  CONSTRAINT `FK_Promotional_codes_activates_Promotional_codes_PromotionalCod~` FOREIGN KEY (`PromotionalCodeId`) REFERENCES `promotional_codes` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Promotional_codes_activates_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `promotional_codes_activates`
--

LOCK TABLES `promotional_codes_activates` WRITE;
/*!40000 ALTER TABLE `promotional_codes_activates` DISABLE KEYS */;
INSERT INTO `promotional_codes_activates` VALUES (1,1,12),(2,2,12),(3,1,12),(4,1,12),(5,3,12);
/*!40000 ALTER TABLE `promotional_codes_activates` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sessions`
--

DROP TABLE IF EXISTS `sessions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sessions` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `Time` datetime(6) NOT NULL,
  `DeviceTypeId` int NOT NULL,
  `Ip` longtext NOT NULL,
  `RefreshToken` longtext NOT NULL,
  `IsComplete` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Sessions_DeviceTypeId` (`DeviceTypeId`),
  KEY `IX_Sessions_UserId` (`UserId`),
  CONSTRAINT `FK_Sessions_Device_types_DeviceTypeId` FOREIGN KEY (`DeviceTypeId`) REFERENCES `device_types` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Sessions_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=204 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sessions`
--

LOCK TABLES `sessions` WRITE;
/*!40000 ALTER TABLE `sessions` DISABLE KEYS */;
INSERT INTO `sessions` VALUES (54,12,'2026-04-29 16:29:03.000000',1,'::1','+p+ChVLD103RPyAdvZLv6qGYEbfj9sZ30TyaUYZagtK0TxGayfEpM8jZjewfN0XPLr720Q2p3DXGQYgRdkAr9A==',0),(55,12,'2026-04-29 16:43:36.000000',1,'::1','j2/3qyncCceRs6m8OtnDEKqUqH9qMUKfvbILl1U35oKnPZwrOmoHKFZeUQ/vIxFV7jMU5fkHKqSQxj1Ee4thBg==',1),(56,12,'2026-05-03 12:38:02.000000',1,'::1','vmfK3vNDuc9PYHYQNAjXccFT3WNFZ3b0FhC+oyeYikvnjUVgLgmnEGpqI0ZupJIJ2WAyz0FO3T78PeDpqR9NHg==',0),(57,14,'2026-05-05 20:04:26.000000',1,'::1','LtBjjCC4v5oYjfgLwbcNWbd5PyWZaVwQYAO7pYa3Q4ZIFc2zZDcS5vOgeQFtxQaiO5DHiTrv9N4g1kHuhBM3yg==',0),(58,14,'2026-05-05 20:04:30.000000',1,'::1','aU9ojj9Rz887l/TUzh18qKr+sh1uEPoAJZYO1uSmtid31SJvz8wkHwzyFPYQk7UFnkzIc98xw0D7myncI+O0lw==',0),(59,15,'2026-05-07 14:55:41.000000',1,'::1','PovVVYyJcmSGCr0Xzv+hSbnw8Oplepo841zyOIhrFGg/FkZ42pObP6e8otuXp101Q/YTXrKlrrwFvi2JeUK8ug==',0),(60,1,'2026-05-07 14:56:18.000000',2,'::1','O0sVNdEOElHzR3PECfOpF/XeKfqIdb2wvFgHe9ziZM/b7+qDcqixL210x4kTW+Px3PMpcWvxTMWzrp6AOTwLzQ==',0),(61,1,'2026-05-07 14:57:10.000000',2,'::1','oZlyrRTkbSt2/d5gROH5QjvzJRUbSJoCXfuEoUQ/nh3uE49wBd6EFIF1QKtxLBwiipPrvhhnYPAGoep/o1u/BA==',0),(62,1,'2026-05-07 14:58:00.000000',2,'::1','HBLIlAYu+a5D+kelpJSd989yJqylkHmawJ7fO9k5rzELVEYh8Z0u2qqahzZdyVGZSwtBIRAiqXicBFlbIfHQYg==',0),(63,1,'2026-05-07 14:58:55.000000',2,'::1','HmiDEktOB/n2HnGbQxfBsoqxRoOPtYCQ7BUrrOnAVTmRfw8/QlVBvUVhq5F/SktyA9Gn8IaVaAc8VCgJ89QJOg==',0),(64,1,'2026-05-07 15:00:40.000000',2,'::1','EHddXQ8HUzgx+q7mfvucodZe3vnCpkSruWlKqAzgpq+z3YjpqGAYvgraCukXqeS2/O+l8aVkLRHW+qWRv0wtsw==',0),(65,1,'2026-05-07 15:05:00.000000',2,'::1','qBpsjPYALh+K2759d6nk+nAf1AwAkpz/vUgqiWXMwzdPAQGN7hcz/Zd/fl1JOyjWr6BtXYTsniPBK2JkTYMknw==',0),(66,1,'2026-05-07 15:06:56.000000',2,'::1','1pxe58fyR3n/73QrWhWFnP3fBg7+gV7xLgWwizzv33y8w5KXKNPkX56ha+InK5X683iLZWHgjOPdBwnTGFpVJg==',0),(67,1,'2026-05-07 15:08:32.000000',2,'::1','2PAaCTKc1muyw+5JYI/aEWOEJlbXmeuJRzBNX/sXIqqKcBkkFKS8EDVfyNzS36BohIjAtV7Tukaiyp71J36kVA==',0),(68,1,'2026-05-07 15:41:44.000000',2,'::1','TAvGimqU2ESJyrqn/0uPBMSoGxkuvwQwidwxY7C2+HdCzTk0bhIlR7uTrkjZ9aLsodctcm8s5tWDA02UDXZNyw==',0),(69,12,'2026-05-07 15:54:08.000000',1,'::1','DmYIqiJa3YnUJSG8SmREkB2Vm9xWshcZt7pfkO0rMahWNRgYOrw7tw0aoZJDEPTR/sdAyvHa2GTJvz48XGa56A==',1),(70,12,'2026-05-07 16:42:32.000000',1,'::1','Zgloun/YfoOwCNqnyqccH42lltD4oF1f5u9caFnPeun96Cz4/wGUIecYDeP6kNGCoVTXTeAuVMpuzhloSNo0sA==',0),(71,12,'2026-05-07 19:55:04.000000',2,'127.0.0.1','ww/DKX2Npy3jwD06AT8TAipfcpvjLVkh1ktCmr06/pKodnAvc63WUCTc6GjVM0v9xxg1vaNuGTQ81aOTD7dW+A==',0),(72,12,'2026-05-07 19:58:59.000000',2,'::1','UtdsHpgbzf9xxqL0WW0c+N6Y21ndro5kP74j5xExYBfIuCnwamNiB4mYPP/38Y5IaHjTsz/tOOCDvrTcQh4y7g==',0),(73,12,'2026-05-08 11:11:59.000000',1,'::1','tkHMBjeRBWP5uqFA7RKvxmhZmmXRsHSje1eXf07dLJvUbVmjN9o/09AAJH/3/L5L2ij1g3BZu2/sN41YxQghag==',0),(74,1,'2026-05-08 11:38:25.000000',2,'::1','6wIdUjaiE7x/mMqqoDh9yUjaME5U90RV75ApHvfxycWF6pQ1M+HX5Bdl03XmZcgc0QFLCuvRFZJBj+0DjuZT5Q==',0),(75,12,'2026-05-08 11:39:45.000000',1,'::1','PL1YELIWGR8szRkJqggCBiPbFSLjUJcrA4HEQ2Oij2giGJ0PF2hyFbV9sactcV3XYqW9N/0tXxYc1LRbSpdx8g==',0),(76,12,'2026-05-08 20:30:10.000000',1,'::1','lTrRD0QHof1p37882hg4O+CViWDt2X/8OvmO7EjoI2VLypbQiHfmSGW2YlQ26Q+aGUvZfYUKsOqDOiJnOXDfDA==',0),(77,1,'2026-05-09 15:29:37.000000',2,'::1','kmQSRhC/ROnAgFS3+QWlKHF3Icx96RKWE1BQCNinZAkoF2YeFvo+i8yM6xp6xCWB55MreNdTL2KmQ0NMwIa3Uw==',0),(78,12,'2026-05-09 16:08:46.000000',1,'::1','fKN/WjJk+P6Zxf1eMzgFvj0ADqq7rXA/fTuUqbWP9B1PiTyAxSwqGzJMowKvp9j+sbQo6Nrh6SfCodB/mU27hQ==',0),(79,1,'2026-05-09 16:23:10.000000',2,'::1','ytdw50TDJIM0GcuCJqo4htUuwNMCa+IHMglXiAze+qSRs3SOYwiplfQniQjxd5pr532KfsNy+qw5JwdEJkVBRA==',0),(80,1,'2026-05-09 16:27:02.000000',2,'::1','Od9khFVgi36TQTM5TBvoBz3WpKl/aV3z13mHGrB8rwy1Q9auU58W6U+nSIotZiQEyUdSZIpIQSsYDpVlui01zA==',0),(81,1,'2026-05-09 16:32:44.000000',2,'::1','8Zg6q94QFAwDLw9V2XOzkUR+hojJ1JwLr9Y6c/TLRO8HVI3aScgI3+NSVvexx1ySDqwveBNvRIvEK98D95bU0g==',0),(82,1,'2026-05-09 16:33:53.000000',2,'::1','LV97+mg0gE0vhTMRsRTp2rCYFYQd8G6tfugjOWX9Wm7bw0Cgt5x99X+X9qYaIIAYfGz7TlxWGY5auxfkM5ylDQ==',0),(83,1,'2026-05-09 16:35:06.000000',2,'::1','qfz1GFLoTBhgq9vcU+SZfhVh1JN0aRBoakGHRtuX/2TWgxISOb1kQWm3IuTt0mNgu2nKF9gR5EvihyeJL2c4Nw==',0),(84,1,'2026-05-09 17:45:06.000000',2,'::1','nlzsV9gBxiZIFQ8OpPN3iVjgzK7+jyjGu0D0ksJDd649tpXgmJYx0UT85CjU4AeBEzxZQiW5cjpjtebWcXixew==',0),(85,1,'2026-05-09 17:48:17.000000',2,'::1','cjdwyVqMIqbemVASyBuMq9K9/YNekHJSPSIEfCsF0xsxICeiJGTAbBoXawqr5VJCkrBw6c7qvm45zU/eVhYudw==',0),(86,1,'2026-05-09 17:50:16.000000',2,'::1','4TKRlzYp2HXChVYRlbVGGPLf8X+XYgTghOehu+p4gZjfKYD7Vz2eaTYB3rozxGGaxLCR1a05cpbFT6rvkZOWxQ==',0),(87,1,'2026-05-09 17:50:45.000000',2,'::1','U2cph0HS3RNZR96XQq8aeCfKuytZ2gfzto2kWkdqHzvnVweiat7mAjoHtg+0xc0oSUo9Kva+KpWrxAneELhXrA==',0),(88,12,'2026-05-09 17:56:18.000000',1,'::1','EI4Xb6OtDkGWMSSuyUDgzBwPV7Crt28Fubj67Ul/6nPBc9HmPjjBAgTd2RxODPfwE5o+4va/1xPZqp3tA94KEw==',0),(89,1,'2026-05-09 18:24:23.000000',2,'::1','8UL9yC816hJMFRjP7BZ74VLlfWcHASFEiUSkwa5hv2jcd+61YTQJ72MF1YcnTezFKt7I8bBPc+NVbxMbCSekyg==',0),(90,1,'2026-05-09 18:34:25.000000',2,'::1','8SrxFsSv/IMwDNLutvbXI9rl4TOAknCfrdqHpwQvm7KMTe9IwrMqUPTFN36sNLQm6Yi7Mg7v7/Ed+KqTVZWjDQ==',0),(91,1,'2026-05-09 18:37:24.000000',2,'::1','Eq8COY6/e/aUf/tArh7oHniXo490KnuzHoHVVqtRr/eq9tbu2c/h1b9Wohk+xcQ5K+Lvmt2xbbEaSFalJ9lUhw==',0),(92,1,'2026-05-09 18:56:24.000000',2,'::1','/2FiYQptI0oZRvSoN9hrB3LIBCPYICxjqu9NBmLfXKSSwH1mNvG2dblq/capECbl+C9Xf1UmvGFKE1BnbU3IAQ==',0),(93,12,'2026-05-10 15:32:54.000000',1,'::1','i7F9N3kaArkNAa1NyyqFCkMOlotvIbwTsM1MJzeh1CXVhQVWnUCHopQVZacAz0gnFVZSwQ/SaADSi9AzuXKmGw==',1),(94,14,'2026-05-11 10:33:09.000000',1,'::1','7hsQ5j+S4wompzvcorkA17nSK+zmdmA+62tYaXJD/uAeU9OkaZVpQM1J9vj4atcK4Hm/4HIG3fZgWIUxesmDEw==',1),(95,14,'2026-05-11 10:35:17.000000',1,'::1','ABPHZLFeTKMz8sps7Ixx4a+8MBFcCvcyI/UTu1DHXG8PkuSScKWFyqatJsPZ32YWfnLVwMtO4KqqFFQMQQJjig==',0),(96,1,'2026-05-11 22:54:01.000000',2,'::1','fVfdLdovSNalnEIE28gUuEcHNxNay6WnGUu7l3Hpj1OsinWXSWZ74NSenfwSBKqn+ssQs1MuW4YCruSEgWGxlw==',0),(97,1,'2026-05-12 18:52:17.000000',2,'::1','d7kCtcDq6cUCxOiKfqqY9uL/uRxUeN4tKQSuUdRwcDLiNDruPpKLk3uTvzhLSU54ICy67+f021sCpPNkkRcW6w==',0),(98,12,'2026-05-25 17:58:23.000000',1,'::1','1aiKmg9tQms80nD2Sa4EJr2xtSbTrPtEHX7crQcZCJHvMIb45SbJYcLgRmVl5FNfTchWpxd8pupPxjRjTKpctg==',1),(99,10,'2026-05-26 17:30:52.000000',1,'::1','Ke1SVmYmCnDPBlJrObBQAuqNymzuQg9prUE4dvRDNM2nlX1QDTACWV/0tCUh1GbRADMn/fUIt3X+CF37LLVy+w==',0),(100,1,'2026-05-28 16:49:48.000000',2,'::1','0nNCi8Eufbm2inztnQCNo0EVLHfnjcT1AnRxsBNGrGveF88WGyETupe53+W4mhotwUkKOYWH01Cese+ntSVh/A==',0),(101,1,'2026-05-28 17:05:33.000000',2,'::1','iXgpUlSj8NvyA23+maXut+ANvBMrzrjFUkLYpwRneiztIGuZeaop4Ii5r2TUXDIc6qB8uK9vFUMCnd1Ga6/v5Q==',0),(102,1,'2026-05-28 17:22:10.000000',2,'::1','bg09wUUmDD2mngAF8snxsaV46awFXtjNAFDcYPbNmSMofMC9HdToW6M2seiwjsWlJe56fPIFDWxy2QPiKkS5bA==',0),(103,1,'2026-05-28 17:28:35.000000',2,'::1','23kZvSA9a6VIxYPwOdc1jPXTowUZkBFwbExOyJ84+rXJwzbCWriau2221YKWYNFNFcefnSf8hw39zIlZlwi4Ag==',0),(104,1,'2026-05-28 17:49:52.000000',2,'::1','7eTxtDz+4ZItIBy5hjIlO42L0iA+7Wqs3YyFdRRvXX5tZBLzVWqrKvq9jkvJg4Uo0P8awevqkPevd1wXmsyOxA==',0),(105,1,'2026-05-28 17:54:15.000000',2,'::1','mkdTBXe9uLyWNLf2p+DTAjrxWE0I3pHArmdNKbop0B/AhnHnz19GNzFvWnv3SoPH9oHYbIoLIdWzsiZCqhmoOw==',0),(106,1,'2026-05-28 18:02:55.000000',2,'::1','iV0hNWD1AMZrK9KqGUyo+zVtQ5u9jWqbxXyY/9eG0jEqIO2X0rEedNVsszvsdllmYr5KRUEz+K6lsrCi3E6GNw==',0),(107,12,'2026-05-29 14:54:38.000000',1,'::1','Kmb57+JsxkrsQob84PB39BJJEGmt0CTyc+AXoFemYjl7B0io6GmUUHIIkzIBbfVd+YebVXjY1vOqkphKh+ghzQ==',0),(108,1,'2026-05-29 15:11:17.000000',2,'::1','w0OdeXO+gNEnTQ5Wh/LH8iqKnKfUEO/5vFsfzOooabgwKHOPvAtxGWb/Rtc350eUmy9a549+vUgDKM6PZJgLLw==',0),(109,1,'2026-05-29 15:15:40.000000',2,'::1','Y7pPmBtIr7Zr/INlrAKDXGN/YLkhCI0HyM5qPiKmAnN61JVCVdwc4+z8pH4ZuUIXlN2QXzhrs19+mkQetvDs4w==',0),(110,1,'2026-05-29 15:18:16.000000',2,'::1','c4Cf4BbZYEk+1tFFgohsb9axZKBkinClkJ0UGpDzP6GZyGNutwm37jyvzhJXoVwVzIjZrJCYJe6neKv13W1Ydg==',0),(111,1,'2026-05-29 15:18:36.000000',2,'::1','zRiOLg1aHWKA0QBmh6l+xwwDXu4sY1F1VSQ5VXdAGFJR969nZb24PSj3yQW2DBNIbnUpljVmtSm/ra5W6+AA0g==',0),(112,1,'2026-05-29 15:20:12.000000',2,'::1','A+Q8/uE0ZAt1r9hVow/SvPJhbgPTzeB4SxzxzF7Wh8Z7N09scCAlwAL9uQymD6iseH/YhtS39LlbaZYFGx8Xqg==',0),(113,1,'2026-05-29 15:20:23.000000',2,'::1','TMkpHd4iCuugV/HTqU6ffUqsjSAVO+hxEPYwWvsOzMvVRH6U97EYe6FicNgL1r/xmQfAEKuEWCHoWDIkKiJXug==',0),(114,1,'2026-05-30 20:16:00.000000',2,'::1','MdujG4JAXn5N1Po2F/fTWUau/oGxnGLr+gLMAytO6e4KKQN7oR28G88oyYU4RRuECmVhVzkUILwnwy2jZ68pUA==',0),(115,1,'2026-05-30 20:22:30.000000',2,'::1','axr6J7aKJ7zpuUbnqmgGG+KsU+ln6xeMEaMXowoMVbk6+9B+sYn49YBDSDfygUowXddGWB3Oh9RB+qoUKoMFsw==',0),(116,1,'2026-05-30 20:24:00.000000',2,'::1','WK7cHlgPJSpNGNELWF+n03PciWRDRzyL/1VgeFZOE+gaCJASnYCy4uSiYtH6HsI0FpAwY0dRA2/OZlY4yIaWPg==',0),(117,1,'2026-05-30 20:28:11.000000',2,'::1','wM7ziuz4kw43i7tQXaxnn9QL8B+JamRRExmbYS8ET+xmLxT2T/PbCjnbnoJmEID86QLuF6KD9R2hFNllNBLFiw==',0),(118,1,'2026-05-30 20:28:56.000000',2,'::1','LwvWcBuN4/wo/l79xnCderhkMb7ypGuoolbz5ScxBPbavtYjFXgjMiow3TKMQ5T2jZKI4S5EKU/wvHseHYlq0g==',0),(119,1,'2026-05-30 20:30:58.000000',2,'::1','t+MCJXB55aWk+T/+m0NF6fvHad24ttwbRZkti+zKeGZJlVCjuOzVxotBbKakzA/iUoGuIqfXpjre8D6JCc6log==',0),(120,1,'2026-05-30 20:32:21.000000',2,'::1','RalZruq1jULuW8RJac/tqXgMjkF1f9VPvZmaozGD4F/hK5qnCN4uYoinCPH5p8IHbZ6/obfz4ouhrOrPMK95YQ==',0),(121,1,'2026-05-30 20:35:40.000000',2,'::1','hvN7fC1xC0m8oBxwoxoFlzzJD2GYZ3dQdFlG6EmU9H817+yC5W9IPE3Dnq+2J1jecK7QNU2tMYULT4Hn5pVgPQ==',0),(122,1,'2026-05-30 22:49:11.000000',2,'::1','b9xeEOBeH019VkLVsuDkrFEGh9MBR6v/zulAhrNxRtpCwnhl/4ESVK2Zroz4WVdeVXcNybkZNgBnaMUpko5RPQ==',0),(123,1,'2026-05-30 22:51:30.000000',2,'::1','58ho0pLUj+X8cCrmz5wTDvABH11YvQuUsekjpxJO+X0bL94hS9SS2k/zNhzrCSolcE9I8qN/kPzOX2iVd3u3Sg==',0),(124,12,'2026-05-30 22:54:46.000000',1,'::1','qjvUz/PmAK6qyuJe11K74oGLVeZPfgX7cEM5IkuMrz0mHzfw4Fod3Qfw4Pggs5LD64LKbSiiFRKAFzivaac2zQ==',1),(125,1,'2026-06-05 15:11:43.000000',2,'::1','kgTJDG64rLA3xDT1k2Awm/AukDXS3JNXQGm17dVZtWeqIy3OqLq0XoRQHrf8wOOk+yahj9UQt91JpPmZMzcyxg==',0),(126,1,'2026-06-05 15:23:45.000000',2,'::1','hHvFm/5H9/1HrOeC6tGkH49FvfnbgbIqqI/usuoDFdSHeMZTe0TkFDeDoF6l5ORQDBTe7TI6S3Uqj0Vh9VeMJg==',0),(127,1,'2026-06-05 15:25:56.000000',2,'::1','LfQoeGobnZp1A5//dy3c7qogqh2tJTQ+zglC4ChVNY6sxrgluXvUsTnGNcWYPAiUrk1/BXzDSncUIA6IMmJ82Q==',0),(128,1,'2026-06-05 15:38:03.000000',2,'::1','rO0Orb2PhH4tq5oHHTF1aStrQ34Z1tzfBRqTon7bDVEUW1q/R03QtBh3K7tXkgmIP+fLrxvtYKla3TTb6YObig==',0),(129,1,'2026-06-05 17:41:11.000000',2,'::1','lOMkFpxMP+r6eb3YCcV5j8kN9iK24HiKvzp30CiRjr992LPY0lZKIlBmL+/3uaSlDYnXZ/FgpjUhf/az4S10cg==',0),(130,1,'2026-06-05 20:06:39.000000',2,'::1','fJlOnFcP/t90oYR+c+Rljvel0hGETf8DM/cXdKV252wMVPWiyKqPs5tIV1CjZ7EzfNnvE2yod/BAuekwFASJHg==',0),(131,1,'2026-06-05 20:12:34.000000',2,'::1','X5oAmFDimUe/tCy8RdEDaIBA/rVHgq9xKInEx7nW8F7Xvqj96rNBDe0MvyfOJ9FUKs0pTpUWFKyoic1lOj1gsA==',0),(132,1,'2026-06-05 20:14:44.000000',2,'::1','BaMikRkVzI8dJdm0UdX3znKJApC7gRE/83DLIRY63d//mUX71Gp7/60CrL1UzV9G4HBvf8hTH9a2G0UepZcXEg==',0),(133,1,'2026-06-06 16:24:18.000000',2,'::1','EOKVXgmm2jQvD9B9EQ4EN9dVPg0Ijv4Ci68Zl/ai65dt4wKu7meOcE9sYXrUyIfqPqr32xX0BKLMaSC1Dt/Cbw==',0),(134,1,'2026-06-06 16:30:20.000000',2,'::1','1awzWrSHR1OhMbCJfLzOcu5EN3/oZSI1L/ruCEM+ly5W6bj/gd4bfMUBewebkELBvVpjliF6+WdEHB3JecYdOQ==',0),(135,1,'2026-06-07 22:04:10.000000',2,'::1','HygCHaU0vDwDpiB1MoaJgD2hewOgWwr2oMzcFoJ2ZTDM6+9A8CwJWtidszahhJJSEGDnS/QOCWQB9AxvI4h15g==',0),(136,1,'2026-06-07 22:53:25.000000',2,'::1','xpiT5dMJlShuSSYPc4cYcEdbXbJkD+G24/+KOQUKVMr5BZyZtsep/7Gi/XSXde+wCpQCM2+o7Oo53i1qxHDnvQ==',0),(137,1,'2026-06-07 22:54:53.000000',2,'::1','+2b5ds0PZF9RrHtNCMa8AN/hJl3VYoZCLV2EzSYAIBI1QKlEGPNZTtPC0YOvHBLm2T/xqCO3I2vSLIWX5t25HA==',0),(138,1,'2026-06-07 22:59:18.000000',2,'::1','cpCmLux24v6TGvGFJsFBZCI8fEv3wk0OVQhawd5TtdcBWAM4IE5SHUb8rSc+6s6ZEcuMKg4KuBtk5BUh0n40Zg==',0),(139,1,'2026-06-07 23:03:12.000000',2,'::1','1e67K6YnXPIXE1nLcE9y6p3Eye1YJ4Bi1c9KUHdVe7qVk6pDgpoQyrKDRZuSM9Q1vNE2aRTQUku4PiMeThSkOw==',0),(140,1,'2026-06-07 23:04:35.000000',2,'::1','jOv1hD6pca9BYLhmpkFUEfET7fQJVFzu0PJQN5q08ccsDkIU8GG62W7xoYXEmrGAsCzZmmKUj8ADGWXef2/v5A==',0),(141,1,'2026-06-07 23:06:14.000000',2,'::1','7wIltpX1KlwbF0G3AgkMbu05japS68tWT0KK9twi7OfWhW82dgQACHCxGlkMxLmVwHTeg3JX89Ur4xoZHs5khw==',0),(142,1,'2026-06-07 23:07:23.000000',2,'::1','P+GKmHU6UBAbBfUkhvNBlO4ygnaok15YFEFxmeoZM2kFekGIQV8INdbBQ835S7vv8/aqpjwotIcR+KjWU5BVCA==',0),(143,1,'2026-06-07 23:08:00.000000',2,'::1','8qnx11RbamR3qboYlErTPFQkS9sE9YbtFikco6jzfMWd7fmwvIYopAVDQSRrHRJctEpfxSKZsxhRfZAzNKnE+Q==',0),(144,1,'2026-06-08 11:33:58.000000',2,'::1','Uj2PvkKLZ/QJS5qWYFfS5goHX203NDHbvZNGVTFFpK69UVOJh14JhF2VVKfpoDsamY0gAJXF+7ecCKVt2PCdcQ==',0),(145,12,'2026-06-08 11:34:43.000000',1,'::1','gQAIKDXi2LC122Ltg5DJjVxJuU5FTMS4uTMIny8lkD+T79lT7miPC+ZDrI1DHfFv52BA1xdMAkbQwBnakdvyyw==',1),(146,1,'2026-06-08 11:36:52.000000',2,'::1','0Av9KTwhY5m3F347nuy2v5Zy145CmgXPhgl2+AxGq35HO752WYT/8SC9ZqghXfSg2AIAggbkrgzV39EhAkKiAw==',0),(147,1,'2026-06-08 11:38:50.000000',2,'::1','FytxXtATcHfH1Kcs+3Q6ssNGt3wBXvJhO6eU07vhclJ3VVfD8uyywl7FPyWkcEZDvEx8ujaVR7QgdaPdNelibg==',0),(148,1,'2026-06-08 11:42:13.000000',2,'::1','n+kheo/ll/OuSp+XnABPkaje/UVoOVrxyU7b+THjzu19ljSyfTqADDTff9KMxuSE0GYnIM3/vTNw3mrU7RpKUw==',0),(149,1,'2026-06-08 11:44:10.000000',2,'::1','9H2i3wMJICwG1wLM7KQxlCQg0TMi/jdaMRA2pKYzA/UAjxZEKdfoJY/94e5swVj6FkS1GogmuDNr9Q9dXxfeSQ==',0),(150,1,'2026-06-08 11:53:37.000000',2,'::1','GRTsyO0i75lixF59SFosEetsAbRtlUaeIKwLuKOAIoVeGhnBNXDP5hU6hWD/qJBowyqQxAdAR4H7E4HKm1QIoA==',0),(151,1,'2026-06-08 11:54:16.000000',2,'::1','hDsvoT4rCSUwST4L7lj1InrP+YoklpjmqaB2GHG4tw25sE2yGXoGUf4GWTiwK0kyNHb3LcqTRtMSknX3FcnaUQ==',0),(152,1,'2026-06-08 11:56:31.000000',2,'::1','HGD2+i/HlfUft5AmHwzDNf9EApMiPei5ntaE3r7JpMljj6Z5f8JG8qxL7oaWukxqwvGy91YrXQMwcENPZajhXQ==',0),(153,1,'2026-06-08 11:57:20.000000',2,'::1','p0wpo5eeUdLkk7lx50d4vqW5QxAjvKD/x0r7gOSXaNjkMOaOXJdD7gDVt0kdgV+Wj+mSYDx53UdwSRDAq4n8mA==',0),(154,1,'2026-06-08 11:58:36.000000',2,'::1','J4dlYS/ogVJ3Y0XdptK42ufXtomt7NOfI/eZLmvZLtcprASxnp1cec1fH0Z5h4hkFZdXLRrg+CCXZPdvqvLRUQ==',0),(155,1,'2026-06-08 15:13:53.000000',2,'::1','WBNJ7/st0HM4Dtf9zYHaWcsHKK6IVjYqZUelkIzQjDJizAaQLxhW1N7Z0zUF1xMsw5CH1MyBxD9C6LlIoMTHsA==',0),(156,1,'2026-06-08 15:17:09.000000',2,'::1','p40H7L2AfvKsShTfBCCtT+a7kKJFNUL3xLX5knH8WZJKp5mLqVO7L9iOyD5q1s4DHQgCRilJAk1lKqyUD1Jawg==',0),(157,1,'2026-06-08 15:22:24.000000',2,'::1','zdpbV+euue2oWr8k5pieXuaXA9Hy+nKM8IgiXje4iwhRkJ1/Hu7b5RXhOOucwIgXpHDufiTCVQWxFhbi0ID37Q==',0),(158,1,'2026-06-08 16:34:41.000000',2,'::1','BpdSudvIOQFc57ERUJhn2UAiBKw+FEaQy5NNjo+wR2ZdkIvDcG3R4nhb47ObeUNdcewCU89Nk+EsrqlRW1Uewg==',0),(159,12,'2026-06-08 16:42:34.000000',1,'::1','HfKh5d5fKeKgNNaDNM6LW8xlX5gD6TWMsVrgWoIIcTZ0xvcstRQkv7ZzpTFr2iBd0PCOU/q3WpdIjGeRd0/PVQ==',0),(160,12,'2026-06-09 11:55:27.543841',1,'::1','3YJe2Io2Wgy8u21fDKPoS4Vo5jC3YSjLhNLkANF57vQc48Ks38KJovv2w0EGJ/Xk3xNz89khExyGPAduhwscew==',0),(161,1,'2026-06-09 15:19:25.255219',2,'::1','0d9LPgTWhcUI681mTVJolOdpC8eUoenak45oHSo59h5YISTHUAcBskl9MWQi3H2SbhzwaHXeAl1JKhwMlbJZ4w==',0),(162,1,'2026-06-09 15:22:05.614932',2,'::1','bs2R1knheBCtmLBYQ9I53B4toARDiQBJw/SJZVItS8NgURL2CfZ39tD1g5mwYXVulNxvDOr1qpgkkdE7fSB6oA==',0),(163,1,'2026-06-09 15:23:36.988285',2,'::1','g8iGlDcqOLtmJjN4ifQmA6oWgUTPnwBDOe3g/6KuA5NPham9vTykrFXC7TMFCemOE9OHxpkVQR1xcyN3xRLQ6w==',0),(164,1,'2026-06-09 15:28:25.935094',2,'::1','RHQNTUIkxjE3eEBmEcXo1eLBCz60vd+lcqaCir8cna8yLeKPZDEuYwG/S2qITDkfrKlhBKiEnJVdygr/aBvlMQ==',0),(165,1,'2026-06-09 15:35:01.838583',2,'::1','ibuE5DU5dPlqnc1ckh4ylWRnzOHTMZHfgbJBCeINyywMbqUVjmuphdfEkant+W7RuQMI5kxiuKmhyymVnDsTyg==',0),(166,1,'2026-06-09 15:41:37.324008',2,'::1','ZgMZoSGulH4F8gYYMd2ETQKKHWIf3ratgXyEoYR1/c4ELcd5ODjxuWHBWnIOdn8OQ+GueML47wuKOfwyT1Lj6w==',0),(167,1,'2026-06-09 15:46:46.080634',2,'::1','ciMTZAKJuUgnHD57nm+RQczevRTpGuSbgsOmVLzhd8TyMlGNMZoa/ka3ZhtAEsNh+twbkoVpeqEn505+baQrWA==',0),(168,1,'2026-06-09 15:51:03.999327',2,'::1','MpPqfxtYdaqqDPgeIYWQed+Q5Z3UbDvSAYasdww82++oyi1LqL6gp1XaeW6aADeRIyHGrMg/ULtGvdhGzbCy2Q==',0),(169,1,'2026-06-09 15:52:41.458348',2,'::1','re9IZR7FmC9+xEuODmZ5KSIAQHsnryt5HR5muKwqY+Eidezld2W0bDCMqbgunN0EQR5Nvmlk6N5Fk9P0SxyBzQ==',0),(170,1,'2026-06-09 15:53:38.619946',2,'::1','pXTIGtf56bDyc7yQ5YySP9sntQRnmbQGw2ccKnTEcjM2VtsEapEZtkr4q6oyD51mlcKD8pSW+QIMdWFJmqKWkw==',0),(171,1,'2026-06-09 15:55:08.338861',2,'::1','M9iSDDQmaGZ7ruZ1Kc7S5VZ9aDndN6qie+aOPULT0b90NCwQ16BUlSh0ayts1leqRrLai3Kj5rJWO7SVbQ0RdA==',0),(172,1,'2026-06-09 16:56:41.209739',2,'::1','9+cz5N/EJ3SowX+T8remr+dwd7UmrAMsLMtX1GrOlNZIsATYxUitF7mJP2X0l9VDokG30Z6lwIUT9q4DfTR+Bw==',0),(173,1,'2026-06-09 16:59:48.088949',2,'::1','J8Jbc+KIZeaSgFjY7QkuOAkkkME4IqdW4QDmMpg/EUzI3S3+0LESa/ol+6nRO0aAa3tu+CBFHhmDGVm7gx3r/Q==',0),(174,1,'2026-06-09 17:08:50.090703',2,'::1','pJkYaiOuqtPVjZ2hm0VoF8eBuuSdFtTq6vZOBxnx2NyRrzlscdi9wZ3fiooQwuMesQKIPdsVa0le5xHItyH0pg==',0),(175,1,'2026-06-09 17:25:46.297379',2,'::1','LfNH6rKV18Sf+7yL6rhVnBpjZsRGr3lvvb9nVWbpca53yhbqnH/Ihtmznu2H6saqmqVP/jhCNji3Pgtq2wZ3YA==',0),(176,1,'2026-06-09 17:31:23.307438',2,'::1','zPGJCcuBDwWJj3w3O1bv2MabxyT8fYoLUhO4Hj9Z26cNgYMISvg5TyWM7nK2BeodiVP3EnQYWB6ijOQGdcdKPw==',0),(177,1,'2026-06-10 15:04:51.079428',2,'::1','VH+bilmOU1hlxzRHXghErGmPKeVWc6QQqImkWrsuF55CI67+ixIeiJwaQol/7sgF5p/vtIvAbcaNdRYiviYIAA==',0),(178,1,'2026-06-10 15:07:33.745037',2,'::1','xt/9qu8hw+ArwzZeTXh/xpzpA1iWYefPnF32kSIsEyVivTgoDUyIcpKuMDofQ1g9TWwhNeiSSfxkByV4gqsbjQ==',0),(179,1,'2026-06-10 15:12:11.117743',2,'::1','tmHx4OkhnkHuxqis/nI3EpjKy+nLS5jx87pm1DLpD0FhqFOYowJ18LTM/uqtvCsqmh2mJd/iHa/RS0LnuX7D9w==',0),(180,1,'2026-06-10 15:53:45.012105',2,'::1','SI10CBYWKvZR2qvXMq/oVEsuA0S+t85qe9uUM39E4lxivrQmojw0kjbuEYbRRzlc5aPA3jvKiiAGarlSzEsUDw==',0),(181,1,'2026-06-10 16:32:06.677076',2,'::1','eTpYuCCZZaNujuUPHnuZkgzGfu5+tTZBHEM2oKq4H8cQZMGQ3g8lSla3S+JNnDM4DoHj1t//mzSnheB0EcGGdw==',0),(182,1,'2026-06-10 16:54:45.348582',2,'::1','xwFn+F/xjitICrRQlLvwmIJEF7d2Lzd2XGTOk3IwvpRLPInNSvHsSLN5qiibrqQhArkoV687aMlPxx3lqR24cg==',0),(183,1,'2026-06-10 17:26:36.121783',2,'::1','vbvPaWK7qWi9irEXae39Uy++ifwkbayAkQkJBEP4GPCMrPEhTPNQC8xYrZs9+F9rrYiJuaEqtBIG22FYTkgV8w==',0),(184,1,'2026-06-10 17:33:51.508731',2,'::1','oj5X2mZVwGm6Ae2wQQFhxqsLJB2ReRh2RedKxcyXHVmGZ2ZvB1dMFSe0EaAMAl6hYnNzUSVoXOwpgo+1OoLNHw==',0),(185,1,'2026-06-10 17:36:12.292599',2,'::1','6A6Dbkw9l16QbMMmAtbI2HixxRLYFBc1JjfZ3aP4R73uR7Qx9bpOQ/RzxborBvehCHOLzGkKdR3NrBHjH5JlaQ==',0),(186,1,'2026-06-10 17:38:51.808936',2,'::1','yvSLRnePOjZ0v4TbLgR9fUoXpM320k6tpOv6HpXe4sasFxbxVVwLz3sdtXVnBvCmybxY9H7HH6IdQdjrgOLb+g==',0),(187,1,'2026-06-10 17:42:22.428749',2,'::1','Xqa49qQgjjn3XCJzrerkghxYj4JTRUKOK7NtoP1TOkQs/C70ImKQhWbLXq363p/vDb7gdnPXmUCD5EZXpZ5oUg==',0),(188,1,'2026-06-13 11:49:40.415688',2,'::1','/nc+7uSfIyRSuTYMSZeonruB1oj/PQZVUvamMcQZoZA1KI++W8NhFmymHUYc4dLUJf5y9K8NyyV7KuWZrvXzbA==',0),(189,1,'2026-06-13 18:15:45.250810',2,'::1','GCTHqbDlbsmAHPEqjJStaMPG2A3L5dCQlnSaOQcdxDLeaOVi7t7k7Jb0wi42KS/11EUnJ+8/UNKsKyOi+jN5mQ==',0),(190,1,'2026-06-13 18:17:18.893766',2,'::1','oGflUOrKDq0lA9wir9nhJDzrrbP+V500DriHRwLad3+fn8l/bMAhLaOmRKYaKBycx2MYsoLydT/dbTN4dePraA==',0),(191,1,'2026-06-13 18:22:52.574113',2,'::1','nvFZJPwIrgLPm01aO/kLdKsSGLZusqQvKKnEnCxN7WdtuL/xdwrsNDnqHLncEVdw8j4UI5wzCREjBs5cCZojAg==',0),(192,1,'2026-06-13 19:00:13.640438',2,'::1','s+uJ1J/rA54J5gGz6xwMxl+eYzG/gKv/zB4SoG3a+HAzKP+Bd5mr0MDw575EQaTYAP27EPeVJWERBxnfLP8rkg==',0),(193,1,'2026-06-13 19:35:57.857839',2,'::1','MSlEw0Py2A/LfzwEn/jvs3FIGNU2ftW4zEM3CZ7hoikukwu16iewwgyUbXLrjuXEh57Ns50XEdC7ybG/qv3w6w==',0),(194,1,'2026-06-13 19:46:45.559527',2,'::1','hlL9Wl/bwrOV3onJ+Mmo0qGTfvfj4jsZLmfWmVdre4WoC0orcCCvoOLvfK+1xkXmY58kbjoq1kXE9+IYVj995w==',0),(195,1,'2026-06-14 11:49:08.600642',2,'::1','d1BymfR2oVo1Z4oX/ocZxPcFXUH80KYb1Wfci7RzMHyNjFANqraFMqyDiwfAtjznyDJkea/b41MVRMT0AWxh9w==',0),(196,1,'2026-06-14 16:06:43.233123',2,'::1','+tAUz+b+80AgJuHqHTxg3nS0nN3iD8nCiq1WkVlWJKdjPaTMABcBWlTae0clYVkQ/hR3E/Q60BKGvYeEP7CUAw==',0),(197,1,'2026-06-14 16:59:16.853762',2,'::1','X/85glcHx0LCeV4lhQ1a0FEUwRR6OE1EqggK5wiqqNQeTGGWbxgDaVedYIgzcSjMqzf8mFoMGExMBVXwP1R+7w==',0),(198,1,'2026-06-14 17:02:50.691151',2,'::1','Jj3ZbHH1vu1MI/V0iJzW/U5noG5xR6tPn8P+sTGYyOt0NU6ghn93CgaGvHbY/ZKwIrXWV7v+Yjec5XfOzd99uQ==',0),(199,1,'2026-06-14 17:05:12.345537',2,'::1','IWSyKPBBrtCFuP4rGY6DIucPHiYrnaqa6/GjZt59sGw0Ldj5h6bwQFHIhLyZJnyugdx9i+TyhuBaYbElwldF3g==',0),(200,1,'2026-06-14 17:07:25.622730',2,'::1','oCn6ztaGHr4zGuYTvwRYDwRQpEk2XHraKBnVB0q2Mgss9KdzDAmYLe0tsjWPbf9iero2wj+QHX9+2Yevl8Hceg==',0),(201,1,'2026-06-14 20:20:52.525312',2,'::1','sVgxF/PsO/89H5d5K+/IQCEPnXcWoFU4WY25Lf6zR/TZGlrUgiSgGym6uMfrmzTOmSVjKJQPPnby51ufAxSYAg==',0),(202,1,'2026-06-14 20:33:54.817073',2,'::1','/Bv9ILfwNEyzq7fCF91f1qOWOe+VyEmQPyjhlC9MW0arsIiiUdMMU+cBl8UA+Q7Ml5EuYb8E457j4uD6G1v6KQ==',0),(203,1,'2026-06-14 21:27:43.062250',2,'::1','5Ocm6g+T7/YDI+9uA6Xf6JZAGuPUFBdDZCPptSp0p8zlA1GdV8v/S7rexZsSXPStOdp1MEfnCJOB/OzZHLm+uQ==',0);
/*!40000 ALTER TABLE `sessions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_statuses`
--

DROP TABLE IF EXISTS `user_statuses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_statuses` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_statuses`
--

LOCK TABLES `user_statuses` WRITE;
/*!40000 ALTER TABLE `user_statuses` DISABLE KEYS */;
INSERT INTO `user_statuses` VALUES (1,'Игрок'),(2,'Спонсор'),(3,'Админ'),(4,'Заблокирован');
/*!40000 ALTER TABLE `user_statuses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Email` longtext NOT NULL,
  `Password` longtext NOT NULL,
  `Name` longtext NOT NULL,
  `StatusId` int NOT NULL,
  `IsBlocked` tinyint(1) NOT NULL,
  `Coefficient` double NOT NULL,
  `Balance` double NOT NULL,
  `WinCount` int NOT NULL,
  `LossCount` int NOT NULL,
  `WinBalance` double NOT NULL,
  `LossBalance` double NOT NULL,
  `SlotsBonusCount` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Users_StatusId` (`StatusId`),
  CONSTRAINT `FK_Users_User_statuses_StatusId` FOREIGN KEY (`StatusId`) REFERENCES `user_statuses` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'admin@casino.ru','2CSU8F1pF7oC96qilonMtES7c/IDgIdssF0fN1N7eJI=','Admin',3,0,1,0,0,0,0,0,0),(7,'MegaVanya315@gmail.com','FeKw08M4keuw8e9gnsQZQgwg4yDOlMZfvIwzEkSOsiU=','MegaVanya315',2,0,1,0,0,0,0,0,0),(8,'asd@hhj.jh','FeKw08M4keuw8e9gnsQZQgwg4yDOlMZfvIwzEkSOsiU=','',4,1,1,0,0,0,0,0,0),(9,'popa@mail.dot','NkSjHwqXVdy8NrpHmCNOwqgzISgWvBHjSWyhef9yBhI=','',1,0,1,0,0,0,0,0,0),(10,'KirrilZabivnoy@pochta.tt','FeKw08M4keuw8e9gnsQZQgwg4yDOlMZfvIwzEkSOsiU=','ИгрокВКазино10',1,0,1,80,0,5,0,203,0),(11,'cemen2000@gmail.com','FeKw08M4keuw8e9gnsQZQgwg4yDOlMZfvIwzEkSOsiU=','',1,0,1,50000,0,0,0,0,0),(12,'SuperIgrok@pochta.ru','FeKw08M4keuw8e9gnsQZQgwg4yDOlMZfvIwzEkSOsiU=','Победитель67',1,0,0,14076424,96,794,14111681,4438594,0),(14,'naushniki@sSabvuferom.com','ipvPHlHoEtCvhGWo28yfdBBkvwrzs9COawJGQ3wZ9/s=','Prostofilia',4,0,1,1700,0,0,0,0,0),(15,'ppp@pp.pp','RuqkKmsKH0FKO0L7qwCc9oa3W8v40soFHUmR98BJVuI=','',1,0,1,0,0,0,0,0,0);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-06-15 15:55:38
