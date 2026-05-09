CREATE DATABASE  IF NOT EXISTS `gambling` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `gambling`;
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
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `email` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  `status` int DEFAULT '1',
  `isBlocked` int DEFAULT '0',
  `coefficient` double DEFAULT '1',
  `balance` double DEFAULT '0',
  `winCount` int DEFAULT '0',
  `lossCount` int DEFAULT '0',
  `winBalance` double DEFAULT NULL,
  `lossBalance` double DEFAULT NULL,
  `slotsBonusCount` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `email_UNIQUE` (`email`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'admin@casino.ru','2CSU8F1pF7oC96qilonMtES7c/IDgIdssF0fN1N7eJI=',NULL,3,0,1,0,0,0,NULL,NULL,NULL),(7,'MegaVanya315@gmail.com','FeKw08M4keuw8e9gnsQZQgwg4yDOlMZfvIwzEkSOsiU=','Vanya',2,0,1,0,0,0,NULL,NULL,NULL),(8,'asd@hhj.jh','FeKw08M4keuw8e9gnsQZQgwg4yDOlMZfvIwzEkSOsiU=',NULL,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(9,'popa@mail.dot','NkSjHwqXVdy8NrpHmCNOwqgzISgWvBHjSWyhef9yBhI=',NULL,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(10,'KirrilZabivnoy@pochta.tt','FeKw08M4keuw8e9gnsQZQgwg4yDOlMZfvIwzEkSOsiU=',NULL,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(11,'cemen2000@gmail.com','FeKw08M4keuw8e9gnsQZQgwg4yDOlMZfvIwzEkSOsiU=',NULL,1,0,1,50000,0,0,0,0,0),(12,'SuperIgrok@pochta.ru','FeKw08M4keuw8e9gnsQZQgwg4yDOlMZfvIwzEkSOsiU=',NULL,1,0,1,423467,91,772,283633,956135,1);
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

-- Dump completed on 2026-05-04 11:11:54
