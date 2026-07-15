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

DROP TABLE IF EXISTS `Blackjack_cards`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Blackjack_cards` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `GameId` int NOT NULL,
  `Denomination` longtext NOT NULL,
  `Suit` varchar(1) NOT NULL,
  `InPlayerHand` tinyint(1) NOT NULL,
  `IsOpen` tinyint(1) NOT NULL,
  `BlackjackGameId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Blackjack_cards_BlackjackGameId` (`BlackjackGameId`),
  CONSTRAINT `FK_Blackjack_cards_Blackjack_Games_BlackjackGameId` FOREIGN KEY (`BlackjackGameId`) REFERENCES `Blackjack_games` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

DROP TABLE IF EXISTS `Blackjack_games`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Blackjack_games` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `Bet` double NOT NULL,
  `CanPlayerMove` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Blackjack_Games_UserId` (`UserId`),
  CONSTRAINT `FK_Blackjack_Games_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

DROP TABLE IF EXISTS `Device_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Device_types` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `Device_types` WRITE;
/*!40000 ALTER TABLE `Device_types` DISABLE KEYS */;
INSERT INTO `Device_types` VALUES (1,'Windows'),(2,'Приложение администратора'),(3,'Android'),(4,'Ios'),(5,'Браузер');
/*!40000 ALTER TABLE `Device_types` ENABLE KEYS */;
UNLOCK TABLES;

DROP TABLE IF EXISTS `Game_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Game_types` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `Game_types` WRITE;
/*!40000 ALTER TABLE `Game_types` DISABLE KEYS */;
INSERT INTO `Game_types` VALUES (1,'слоты'),(2,'блекджек'),(3,'рулетка');
/*!40000 ALTER TABLE `Game_types` ENABLE KEYS */;
UNLOCK TABLES;

DROP TABLE IF EXISTS `Games`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Games` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PlayerId` int NOT NULL,
  `GameTypeId` int NOT NULL,
  `Bet` double NOT NULL,
  `IsWin` tinyint(1) NOT NULL,
  `WinAmount` double NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Games_GameTypeId` (`GameTypeId`),
  KEY `FK_Games_Games_PlayerId_idx` (`PlayerId`),
  CONSTRAINT `FK_Games_Game_types_GameTypeId` FOREIGN KEY (`GameTypeId`) REFERENCES `Game_types` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Games_Games_PlayerId` FOREIGN KEY (`PlayerId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=389 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

DROP TABLE IF EXISTS `Promotional_codes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Promotional_codes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Code` longtext NOT NULL,
  `Use` int NOT NULL,
  `InterestBonus` int NOT NULL,
  `QuantitativeBonus` int NOT NULL,
  `FreeSpins` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

DROP TABLE IF EXISTS `Promotional_codes_activates`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Promotional_codes_activates` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PromotionalCodeId` int NOT NULL,
  `UserId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Promotional_codes_activates_PromotionalCodeId` (`PromotionalCodeId`),
  KEY `IX_Promotional_codes_activates_UserId` (`UserId`),
  CONSTRAINT `FK_Promotional_codes_activates_Promotional_codes_PromotionalCod~` FOREIGN KEY (`PromotionalCodeId`) REFERENCES `Promotional_codes` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Promotional_codes_activates_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

DROP TABLE IF EXISTS `Sessions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Sessions` (
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
  CONSTRAINT `FK_Sessions_Device_types_DeviceTypeId` FOREIGN KEY (`DeviceTypeId`) REFERENCES `Device_types` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Sessions_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=204 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

DROP TABLE IF EXISTS `User_statuses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `User_statuses` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `User_statuses` WRITE;
/*!40000 ALTER TABLE `User_statuses` DISABLE KEYS */;
INSERT INTO `User_statuses` VALUES (1,'Игрок'),(2,'Спонсор'),(3,'Админ'),(4,'Заблокирован');
/*!40000 ALTER TABLE `User_statuses` ENABLE KEYS */;
UNLOCK TABLES;

DROP TABLE IF EXISTS `Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Users` (
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
  CONSTRAINT `FK_Users_User_statuses_StatusId` FOREIGN KEY (`StatusId`) REFERENCES `User_statuses` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `Users` WRITE;
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
INSERT INTO `Users` VALUES (1,'admin@casino.ru','2CSU8F1pF7oC96qilonMtES7c/IDgIdssF0fN1N7eJI=','Admin',3,0,1,0,0,0,0,0,0);
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
