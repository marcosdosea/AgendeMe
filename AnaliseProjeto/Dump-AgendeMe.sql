CREATE DATABASE  IF NOT EXISTS `AgendeMe` /*!40100 DEFAULT CHARACTER SET utf8mb3 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `AgendeMe`;
-- MySQL dump 10.13  Distrib 8.0.30, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: agendeme
-- ------------------------------------------------------
-- Server version	8.0.30

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
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `agendadoservico`
--

DROP TABLE IF EXISTS `agendadoservico`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `agendadoservico` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `diaSemana` enum('Segunda','Terça','Quarta','Quinta','Sexta') NOT NULL,
  `horarioInicio` char(5) NOT NULL,
  `horarioFim` char(5) NOT NULL,
  `vagasAtendimento` int unsigned NOT NULL,
  `vagasRetorno` int unsigned NOT NULL,
  `idServicoPublico` int unsigned NOT NULL,
  `idProfissional` int unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_Agenda_ServicoPublico1_idx` (`idServicoPublico`),
  KEY `fk_Agenda_Cidadao1_idx` (`idProfissional`),
  CONSTRAINT `fk_Agenda_Cidadao1` FOREIGN KEY (`idProfissional`) REFERENCES `cidadao` (`id`),
  CONSTRAINT `fk_Agenda_ServicoPublico1` FOREIGN KEY (`idServicoPublico`) REFERENCES `servicopublico` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `agendadoservico`
--

LOCK TABLES `agendadoservico` WRITE;
/*!40000 ALTER TABLE `agendadoservico` DISABLE KEYS */;
INSERT INTO `agendadoservico` VALUES (1,'Segunda','08:00','12:00',10,2,1,1),(2,'Segunda','13:00','17:00',10,2,1,1),(3,'Segunda','08:00','12:00',10,2,1,1),(4,'Segunda','13:00','17:00',10,2,1,1),(5,'Terça','08:00','12:00',10,2,1,1),(6,'Terça','13:00','17:00',10,2,1,1),(7,'Quarta','08:00','12:00',10,2,1,1),(8,'Quarta','13:00','17:00',10,2,1,1),(9,'Quinta','08:00','12:00',10,2,1,1),(10,'Quinta','13:00','17:00',10,2,1,1),(11,'Sexta','08:00','12:00',10,2,1,1),(12,'Sexta','13:00','17:00',10,2,1,1);
/*!40000 ALTER TABLE `agendadoservico` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `agendamento`
--

DROP TABLE IF EXISTS `agendamento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `agendamento` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `tipo` enum('Agendamento','Retorno') NOT NULL,
  `situacao` enum('Agendado','Cancelado','Remarcado','Aguardando Atendimento','Atendido') NOT NULL,
  `dataCadastro` datetime NOT NULL,
  `idCidadao` int unsigned NOT NULL,
  `idDiaAgendamento` int unsigned NOT NULL,
  `idAtendente` int unsigned DEFAULT NULL,
  `idRetorno` int unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `situacao` (`situacao`),
  KEY `dataAtendimento` (`dataCadastro`),
  KEY `fk_Agendamento_Cidadao_idx` (`idCidadao`),
  KEY `fk_Agendamento_Atendente_idx` (`idAtendente`),
  KEY `fk_Agendamento_Agendamento1_idx` (`idRetorno`),
  KEY `fk_Agendamento_AgendamentoDia1_idx` (`idDiaAgendamento`),
  CONSTRAINT `fk_Agendamento_Agendamento1` FOREIGN KEY (`idRetorno`) REFERENCES `agendamento` (`id`),
  CONSTRAINT `fk_Agendamento_AgendamentoDia1` FOREIGN KEY (`idDiaAgendamento`) REFERENCES `diaagendamento` (`id`),
  CONSTRAINT `fk_Agendamento_Cidadao1` FOREIGN KEY (`idCidadao`) REFERENCES `cidadao` (`id`),
  CONSTRAINT `fk_Agendamento_Cidadao2` FOREIGN KEY (`idAtendente`) REFERENCES `cidadao` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `agendamento`
--

LOCK TABLES `agendamento` WRITE;
/*!40000 ALTER TABLE `agendamento` DISABLE KEYS */;
INSERT INTO `agendamento` VALUES (9,'Agendamento','Agendado','2022-10-28 15:55:53',1,1,NULL,NULL),(12,'Agendamento','Agendado','2022-10-28 15:57:08',1,1,NULL,NULL),(14,'Agendamento','Agendado','2022-10-28 15:57:32',1,1,NULL,NULL),(16,'Agendamento','Agendado','2022-10-28 15:58:11',1,1,NULL,NULL),(17,'Agendamento','Agendado','2022-10-28 16:00:40',1,1,NULL,NULL),(23,'Agendamento','Agendado','2022-10-28 16:02:04',1,1,NULL,NULL),(24,'Agendamento','Agendado','2022-10-28 16:03:57',1,1,NULL,NULL),(25,'Agendamento','Agendado','2022-10-28 16:05:30',1,1,NULL,NULL),(27,'Agendamento','Agendado','2022-10-28 16:10:11',1,1,NULL,NULL),(30,'Agendamento','Agendado','2022-10-28 16:17:43',1,1,NULL,NULL),(31,'Agendamento','Agendado','2022-10-28 16:20:52',1,1,NULL,NULL),(32,'Agendamento','Agendado','2022-11-20 07:00:55',2,1,NULL,NULL),(36,'Agendamento','Agendado','2022-11-20 16:16:12',2,1,NULL,NULL),(43,'Agendamento','Agendado','2022-11-20 16:30:48',2,1,NULL,NULL),(44,'Agendamento','Agendado','2022-11-20 16:42:53',2,1,NULL,NULL),(45,'Agendamento','Agendado','2022-11-20 17:46:29',2,1,NULL,NULL),(46,'Agendamento','Agendado','2022-11-20 17:49:08',2,1,NULL,NULL),(47,'Agendamento','Agendado','2022-11-20 17:50:41',2,1,NULL,NULL),(48,'Agendamento','Agendado','2022-11-20 17:54:54',2,1,NULL,NULL),(49,'Agendamento','Agendado','2022-11-20 17:58:32',2,1,NULL,NULL),(50,'Agendamento','Agendado','2022-11-20 17:59:38',2,1,NULL,NULL),(51,'Agendamento','Agendado','2022-11-20 18:02:17',2,2,NULL,NULL),(52,'Agendamento','Agendado','2022-11-20 18:03:32',2,2,NULL,NULL);
/*!40000 ALTER TABLE `agendamento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `areadeservico`
--

DROP TABLE IF EXISTS `areadeservico`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `areadeservico` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(70) NOT NULL,
  `idPrefeitura` int unsigned NOT NULL,
  `icone` varchar(100) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `nome` (`nome`),
  KEY `fk_Area_Prefeitura1_idx` (`idPrefeitura`),
  CONSTRAINT `fk_Area_Prefeitura1` FOREIGN KEY (`idPrefeitura`) REFERENCES `prefeitura` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `areadeservico`
--

LOCK TABLES `areadeservico` WRITE;
/*!40000 ALTER TABLE `areadeservico` DISABLE KEYS */;
INSERT INTO `areadeservico` VALUES (1,'Saúde',1,'fas fa-plus'),(2,'Transporte',1,'fas fa-bus'),(3,'Cultura',1,'fas fa-theater-masks'),(4,'Agricultura',1,'fas fa-tractor'),(5,'Esporte',1,'fas fa-futbol'),(6,'Negócio',1,'fas fa-briefcase'),(7,'Social',1,'fas fa-user-friends');
/*!40000 ALTER TABLE `areadeservico` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetRoleClaims`
--

DROP TABLE IF EXISTS `AspNetRoleClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `AspNetRoleClaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(767) NOT NULL,
  `ClaimType` text,
  `ClaimValue` text,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetRoleClaims`
--

LOCK TABLES `AspNetRoleClaims` WRITE;
/*!40000 ALTER TABLE `AspNetRoleClaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetRoleClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetRoles`
--

DROP TABLE IF EXISTS `AspNetRoles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `AspNetRoles` (
  `Id` varchar(767) NOT NULL,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(256) DEFAULT NULL,
  `ConcurrencyStamp` text,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetRoles`
--

LOCK TABLES `AspNetRoles` WRITE;
/*!40000 ALTER TABLE `AspNetRoles` DISABLE KEYS */;
INSERT INTO AspNetRoles VALUES (
'4e41763d-d54c-472a-ab46-dadabb2d8859',
'CIDADAO',
'Cidadão',
'fac5a197-97f3-47e9-b29e-479fa1e5ac80'),
(
'45e6f3fe-ec74-43c0-bacc-cda3e63c84a9',
'ATENDENTE',
'Atendente',
'fac5a197-97f3-47e9-b29e-479fa1e5ac80'),
(
'c4fce0cf-b10b-42e4-b69d-5f6a14fbe98a',
'GESTOR DO ORGAO',
'Gestor do Orgão',
'fac5a197-97f3-47e9-b29e-479fa1e5ac80'),
(
'88203b13-8185-4a5b-aaca-07284fd0bc8c',
'Gestor da Prefeitura',
'GESTOR DA PREFEITURA',
'fac5a197-97f3-47e9-b29e-479fa1e5ac80'),
(
'a7bbfaac-ccdb-4637-83c1-f85d760080a8',
'Profissional',
'PROFISSIONAL',
'fac5a197-97f3-47e9-b29e-479fa1e5ac80'),
(
'46077122-95af-4063-8570-ce2d09b7f3c0',
'Administrador do Sistema',
'ADMINISTRADOR DO SISTEMA',
'fac5a197-97f3-47e9-b29e-479fa1e5ac80');
/*!40000 ALTER TABLE `AspNetRoles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserClaims`
--

DROP TABLE IF EXISTS `AspNetUserClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `AspNetUserClaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` varchar(767) NOT NULL,
  `ClaimType` text,
  `ClaimValue` text,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserClaims`
--

LOCK TABLES `AspNetUserClaims` WRITE;
/*!40000 ALTER TABLE `AspNetUserClaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserlogins`
--

DROP TABLE IF EXISTS `AspNetUserLogins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `AspNetUserLogins` (
  `LoginProvider` varchar(128) NOT NULL,
  `ProviderKey` varchar(128) NOT NULL,
  `ProviderDisplayName` text,
  `UserId` varchar(767) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserLogins`
--

LOCK TABLES `AspNetUserLogins` WRITE;
/*!40000 ALTER TABLE `AspNetUserLogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserLogins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserRoles`
--

DROP TABLE IF EXISTS `AspNetUserRoles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `AspNetUserRoles` (
  `UserId` varchar(767) NOT NULL,
  `RoleId` varchar(767) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserRoles`
--

LOCK TABLES `AspNetUserRoles` WRITE;
/*!40000 ALTER TABLE `AspNetUserRoles` DISABLE KEYS */;
INSERT INTO AspNetUserRoles VALUES 
('f5094213-c8ef-403b-a106-7dc5c85a4c45','4e41763d-d54c-472a-ab46-dadabb2d8859'), -- Cidadao
('0f900e08-881c-41e0-8387-b2f8ea9c7eba','45e6f3fe-ec74-43c0-bacc-cda3e63c84a9'), -- Atendente
('a775fc74-4e48-47b9-9ede-340d112d3ccd','a7bbfaac-ccdb-4637-83c1-f85d760080a8'), -- Profissional
('06b90778-f588-495e-af5b-5c9477d7b0f2','c4fce0cf-b10b-42e4-b69d-5f6a14fbe98a'), -- Orgao
('45eec0fe-dd6b-448f-a8c2-ba84defaa58d','88203b13-8185-4a5b-aaca-07284fd0bc8c'), -- Prefeitura
('8e9457c5-9283-46f5-af38-298056b9f84b','46077122-95af-4063-8570-ce2d09b7f3c0'); -- Sistema
/*!40000 ALTER TABLE `AspNetUserRoles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUsers`
--

DROP TABLE IF EXISTS `AspNetUsers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `AspNetUsers` (
  `Id` varchar(767) NOT NULL,
  `UserName` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(256) DEFAULT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `EmailConfirmed` bit(1) NOT NULL,
  `PasswordHash` text,
  `SecurityStamp` text,
  `ConcurrencyStamp` text,
  `PhoneNumber` text,
  `PhoneNumberConfirmed` bit(1) NOT NULL,
  `TwoFactorEnabled` bit(1) NOT NULL,
  `LockoutEnd` timestamp NULL DEFAULT NULL,
  `LockoutEnabled` bit(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUsers`
--

LOCK TABLES `AspNetUsers` WRITE;
/*!40000 ALTER TABLE `AspNetUsers` DISABLE KEYS */;
INSERT INTO `AspNetUsers` 
VALUES 
('f5094213-c8ef-403b-a106-7dc5c85a4c45','devlauross2@gmail.com','DEVLAUROSS2@GMAIL.COM','devlauross2@gmail.com','DEVLAUROSS2@GMAIL.COM',_binary '\0','AQAAAAEAACcQAAAAEJCMEenkLDRCqgJrHFJz3cTgXPhmIfihlxHdfvzeLYS6A/czCt+UmYurKhHhAV6/YA==','IKMCPEUEGO3DFPNZ3GTZ3DVREZEMHR2I','c2ebb266-388f-469f-afac-e0ee0a6a3b9c',NULL,_binary '\0',_binary '\0',NULL,_binary '',0),
(
  '0f900e08-881c-41e0-8387-b2f8ea9c7eba',
  '222222222222222',
  '222222222222222',
  'admsistema@email.com',
  'admsistema@email.com',
  0,
  'AQAAAAEAACcQAAAAEJCMEenkLDRCqgJrHFJz3cTgXPhmIfihlxHdfvzeLYS6A/czCt+UmYurKhHhAV6/YA==',
  'U3ODHHNJ3VZU45UGMWA2AEOTBKWHODV3',
  'b3896af2-a235-48ad-b81e-4feed9c28f65',
  NULL,
  0,
  0,
  NULL,
  1,
  0
),
(
  'a775fc74-4e48-47b9-9ede-340d112d3ccd',
  '333333333333333',
  '333333333333333',
  'admsistema@email.com',
  'admsistema@email.com',
  0,
  'AQAAAAEAACcQAAAAEJCMEenkLDRCqgJrHFJz3cTgXPhmIfihlxHdfvzeLYS6A/czCt+UmYurKhHhAV6/YA==',
  'U3ODHHNJ3VZU45UGMWA2AEOTBKWHODV3',
  'b3896af2-a235-48ad-b81e-4feed9c28f65',
  NULL,
  0,
  0,
  NULL,
  1,
  0
),
(
  '06b90778-f588-495e-af5b-5c9477d7b0f2',
  '444444444444444',
  '444444444444444',
  'admsistema@email.com',
  'admsistema@email.com',
  0,
  'AQAAAAEAACcQAAAAEJCMEenkLDRCqgJrHFJz3cTgXPhmIfihlxHdfvzeLYS6A/czCt+UmYurKhHhAV6/YA==',
  'U3ODHHNJ3VZU45UGMWA2AEOTBKWHODV3',
  'b3896af2-a235-48ad-b81e-4feed9c28f65',
  NULL,
  0,
  0,
  NULL,
  1,
  0
),
(
  '45eec0fe-dd6b-448f-a8c2-ba84defaa58d',
  '555555555555555',
  '555555555555555',
  'admsistema@email.com',
  'admsistema@email.com',
  0,
  'AQAAAAEAACcQAAAAEJCMEenkLDRCqgJrHFJz3cTgXPhmIfihlxHdfvzeLYS6A/czCt+UmYurKhHhAV6/YA==',
  'U3ODHHNJ3VZU45UGMWA2AEOTBKWHODV3',
  'b3896af2-a235-48ad-b81e-4feed9c28f65',
  NULL,
  0,
  0,
  NULL,
  1,
  0
),
(
  '8e9457c5-9283-46f5-af38-298056b9f84b',
  '666666666666666',
  '666666666666666',
  'admsistema@email.com',
  'admsistema@email.com',
  0,
  'AQAAAAEAACcQAAAAEJCMEenkLDRCqgJrHFJz3cTgXPhmIfihlxHdfvzeLYS6A/czCt+UmYurKhHhAV6/YA==',
  'U3ODHHNJ3VZU45UGMWA2AEOTBKWHODV3',
  'b3896af2-a235-48ad-b81e-4feed9c28f65',
  NULL,
  0,
  0,
  NULL,
  1,
  0
);
/*!40000 ALTER TABLE `AspNetUsers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserTokens`
--

DROP TABLE IF EXISTS `AspNetUserTokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `AspNetUserTokens` (
  `UserId` varchar(767) NOT NULL,
  `LoginProvider` varchar(128) NOT NULL,
  `Name` varchar(128) NOT NULL,
  `Value` text,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserTokens`
--

LOCK TABLES `AspNetUserTokens` WRITE;
/*!40000 ALTER TABLE `AspNetUserTokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserTokens` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `atendenteorgaopublico`
--

DROP TABLE IF EXISTS `atendenteorgaopublico`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `atendenteorgaopublico` (
  `idAtendente` int unsigned NOT NULL,
  `idOrgaoPublico` int unsigned NOT NULL,
  PRIMARY KEY (`idAtendente`,`idOrgaoPublico`),
  KEY `fk_CidadaoOrgaoPublico_OrgaoPublico1_idx` (`idOrgaoPublico`),
  KEY `fk_CidadaoOrgaoPublico_Cidadao1_idx` (`idAtendente`),
  CONSTRAINT `fk_CidadaoOrgaoPublico_Cidadao1` FOREIGN KEY (`idAtendente`) REFERENCES `cidadao` (`id`),
  CONSTRAINT `fk_CidadaoOrgaoPublico_OrgaoPublico1` FOREIGN KEY (`idOrgaoPublico`) REFERENCES `orgaopublico` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `atendenteorgaopublico`
--

LOCK TABLES `atendenteorgaopublico` WRITE;
/*!40000 ALTER TABLE `atendenteorgaopublico` DISABLE KEYS */;
/*!40000 ALTER TABLE `atendenteorgaopublico` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cargo`
--

DROP TABLE IF EXISTS `cargo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cargo` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `nome` varchar(70) NOT NULL,
  `descricao` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cargo`
--

LOCK TABLES `cargo` WRITE;
/*!40000 ALTER TABLE `cargo` DISABLE KEYS */;
INSERT INTO `cargo` VALUES (1,'Clínico Geral','Clínico Geral');
/*!40000 ALTER TABLE `cargo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cargoprofissionalprefeitura`
--

DROP TABLE IF EXISTS `cargoprofissionalprefeitura`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cargoprofissionalprefeitura` (
  `idCargo` int unsigned NOT NULL,
  `idProfissional` int unsigned NOT NULL,
  `idPrefeitura` int unsigned NOT NULL,
  PRIMARY KEY (`idCargo`,`idProfissional`,`idPrefeitura`),
  KEY `fk_CargoProfissionalPrefeitura_Cargo1_idx` (`idCargo`),
  KEY `fk_CargoProfissionalPrefeitura_Cidadao1_idx` (`idProfissional`),
  KEY `fk_CargoProfissionalPrefeitura_Prefeitura1_idx` (`idPrefeitura`),
  CONSTRAINT `fk_CargoProfissionalPrefeitura_Cargo1` FOREIGN KEY (`idCargo`) REFERENCES `cargo` (`id`),
  CONSTRAINT `fk_CargoProfissionalPrefeitura_Cidadao1` FOREIGN KEY (`idProfissional`) REFERENCES `cidadao` (`id`),
  CONSTRAINT `fk_CargoProfissionalPrefeitura_Prefeitura1` FOREIGN KEY (`idPrefeitura`) REFERENCES `prefeitura` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cargoprofissionalprefeitura`
--

LOCK TABLES `cargoprofissionalprefeitura` WRITE;
/*!40000 ALTER TABLE `cargoprofissionalprefeitura` DISABLE KEYS */;
INSERT INTO `cargoprofissionalprefeitura` VALUES (1,1,1);
/*!40000 ALTER TABLE `cargoprofissionalprefeitura` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cidadao`
--

DROP TABLE IF EXISTS `cidadao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cidadao` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `nome` varchar(70) NOT NULL,
  `cpf` varchar(15) NOT NULL,
  `dataNascimento` date NOT NULL,
  `sus` varchar(20) NOT NULL,
  `telefone` varchar(20) NOT NULL,
  `email` varchar(70) DEFAULT NULL,
  `cep` varchar(10) NOT NULL,
  `estado` char(2) NOT NULL,
  `cidade` varchar(70) NOT NULL,
  `bairro` varchar(70) NOT NULL,
  `rua` varchar(70) NOT NULL,
  `numeroCasa` varchar(7) NOT NULL,
  `sexo` char(1) NOT NULL,
  `tipoCidadao` enum('Administrador','Atendente','gestorOrgao','gestorPrefeitura','Profissional','Cidadao') NOT NULL DEFAULT 'Cidadao',
  `complemento` varchar(100) DEFAULT NULL,
  `idOrgaoPublico` int unsigned DEFAULT NULL,
  `idPrefeitura` int unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `sus_UNIQUE` (`sus`),
  UNIQUE KEY `cpf_UNIQUE` (`cpf`),
  UNIQUE KEY `email_UNIQUE` (`email`),
  KEY `nome` (`nome`),
  KEY `fk_Cidadao_OrgaoPublico1_idx` (`idOrgaoPublico`),
  KEY `fk_Cidadao_Prefeitura1_idx` (`idPrefeitura`),
  CONSTRAINT `fk_Cidadao_OrgaoPublico1` FOREIGN KEY (`idOrgaoPublico`) REFERENCES `orgaopublico` (`id`),
  CONSTRAINT `fk_Cidadao_Prefeitura1` FOREIGN KEY (`idPrefeitura`) REFERENCES `prefeitura` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cidadao`
--

LOCK TABLES `cidadao` WRITE;
/*!40000 ALTER TABLE `cidadao` DISABLE KEYS */;
INSERT INTO `cidadao` 
VALUES 
(1,'Lauro Santana','111111111111111','1998-09-10','11111111111111111111','11111111111111111111','devlauross2@gmail.com','1111111111','SE','Ribeirópolis','Centro','Rua José Romualdo de Menezes','103','M','Cidadao',NULL,NULL,NULL);
/*!40000 ALTER TABLE `cidadao` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `diaagendamento`
--

DROP TABLE IF EXISTS `diaagendamento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `diaagendamento` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `data` datetime NOT NULL,
  `diaSemana` enum('Segunda','Terça','Quarta','Quinta','Sexta') NOT NULL,
  `horarioInicio` char(5) NOT NULL,
  `horarioFim` char(5) NOT NULL,
  `vagasAtendimento` int NOT NULL,
  `vagasAgendadas` int NOT NULL DEFAULT '0',
  `vagasRetorno` int NOT NULL,
  `vagasAgendadasRetorno` int NOT NULL DEFAULT '0',
  `idServicoPublico` int unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_AgendamentoDia_ServicoPublico1_idx` (`idServicoPublico`),
  CONSTRAINT `fk_AgendamentoDia_ServicoPublico1` FOREIGN KEY (`idServicoPublico`) REFERENCES `servicopublico` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `diaagendamento`
--

LOCK TABLES `diaagendamento` WRITE;
/*!40000 ALTER TABLE `diaagendamento` DISABLE KEYS */;
INSERT INTO `diaagendamento` 
VALUES 
(1,'2022-10-24 00:00:00','Segunda','08:00','12:00',10,8,2,0,1),
(2,'2022-10-24 00:00:00','Segunda','13:00','17:00',10,2,2,0,1),
(3,'2022-10-25 00:00:00','Terça','08:00','12:00',8,0,2,0,1),
(4,'2022-10-25 00:00:00','Terça','13:00','17:00',10,0,2,0,1),
(5,'2022-10-26 00:00:00','Quarta','08:00','12:00',10,0,2,0,1),
(6,'2022-10-26 00:00:00','Quarta','13:00','17:00',10,0,2,0,1),
(7,'2022-10-27 00:00:00','Quinta','08:00','12:00',10,0,2,0,1),
(8,'2022-10-27 00:00:00','Quinta','13:00','17:00',10,0,2,0,1),
(9,'2022-10-28 00:00:00','Sexta','08:00','12:00',10,0,2,0,1),
(10,'2022-10-28 00:00:00','Sexta','13:00','17:00',10,0,2,0,1),
(11,'2022-10-24 00:00:00','Segunda','08:00','12:00',10,0,2,0,2),
(12,'2022-10-24 00:00:00','Segunda','13:00','17:00',10,0,2,0,3),
(13,'2022-10-25 00:00:00','Terça','08:00','12:00',10,0,2,0,2),
(14,'2022-10-25 00:00:00','Terça','13:00','17:00',10,0,2,0,3),
(15,'2022-10-26 00:00:00','Quarta','08:00','12:00',10,0,2,0,2),
(16,'2022-10-26 00:00:00','Quarta','13:00','17:00',10,0,2,0,3),
(17,'2022-10-27 00:00:00','Quinta','08:00','12:00',10,0,2,0,2),
(18,'2022-10-27 00:00:00','Quinta','13:00','17:00',10,0,2,0,3),
(19,'2022-10-28 00:00:00','Sexta','08:00','12:00',10,0,2,0,2),
(20,'2022-10-28 00:00:00','Sexta','13:00','17:00',10,0,2,0,3),
(21,'2022-10-28 00:00:00','Sexta','07:00','09:00',10,0,2,0,12),
(22,'2022-10-28 00:00:00','Sexta','15:00','17:00',10,0,2,0,12),
(23,'2022-10-24 00:00:00','Sexta','08:00','12:00',10,0,2,0,9),
(24,'2022-10-28 00:00:00','Sexta','08:00','12:00',10,0,2,0,9),
(25,'2022-10-25 00:00:00','Sexta','08:00','12:00',10,0,2,0,10),
(26,'2022-10-27 00:00:00','Sexta','08:00','12:00',10,0,2,0,10);
/*!40000 ALTER TABLE `diaagendamento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orgaopublico`
--

DROP TABLE IF EXISTS `orgaopublico`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orgaopublico` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `nome` varchar(70) NOT NULL,
  `bairro` varchar(70) NOT NULL,
  `rua` varchar(70) NOT NULL,
  `numero` varchar(7) NOT NULL,
  `complemento` varchar(70) DEFAULT NULL,
  `cep` varchar(10) DEFAULT NULL,
  `horaAbre` char(5) NOT NULL,
  `horaFecha` char(5) NOT NULL,
  `idPrefeitura` int unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `nome` (`nome`) /*!80000 INVISIBLE */,
  KEY `numero` (`numero`),
  KEY `fk_OrgaoPublico_Prefeitura1_idx` (`idPrefeitura`),
  CONSTRAINT `fk_OrgaoPublico_Prefeitura1` FOREIGN KEY (`idPrefeitura`) REFERENCES `prefeitura` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orgaopublico`
--

LOCK TABLES `orgaopublico` WRITE;
/*!40000 ALTER TABLE `orgaopublico` DISABLE KEYS */;
INSERT INTO `orgaopublico` 
VALUES 
(1,'Clínica de Saúde da Família Vereador Vivaldo Meneses','Serrano','Rua Josué Passos','700',null,'49530-000','08:00','18:00',1),
(2,'Centro de Saúde Da Família Manoel Pereira de Andrade','Sítio Porto','Rua Principal','S/N',null,'49530-000','07:00','12:00',1),
(3,'Clinica De Fisioterapia Municipal De Itabaiana "Geraldo Teles"','Centro','Rua Clemente Alcídes Cavalcante','476',null,'49530-000','08:00','13:00',1);
/*!40000 ALTER TABLE `orgaopublico` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prefeitura`
--

DROP TABLE IF EXISTS `prefeitura`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `prefeitura` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `nome` varchar(70) NOT NULL,
  `cnpj` varchar(20) DEFAULT NULL,
  `estado` char(2) NOT NULL,
  `cidade` varchar(30) NOT NULL,
  `bairro` varchar(70) DEFAULT NULL,
  `cep` varchar(10) DEFAULT NULL,
  `rua` varchar(70) DEFAULT NULL,
  `numero` varchar(7) NOT NULL,
  `icone` varchar(100) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `cnpj_UNIQUE` (`cnpj`),
  KEY `nome` (`nome`) /*!80000 INVISIBLE */,
  KEY `numero` (`numero`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prefeitura`
--

LOCK TABLES `prefeitura` WRITE;
/*!40000 ALTER TABLE `prefeitura` DISABLE KEYS */;
INSERT INTO `prefeitura` 
VALUES (1,'Prefeitura de Itabaiana','897892344','SE','Itabaiana','Centro','2344444','Praça Fausto Cardoso','12','fas fa-university');
/*!40000 ALTER TABLE `prefeitura` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `servicopublico`
--

DROP TABLE IF EXISTS `servicopublico`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `servicopublico` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `nome` varchar(70) NOT NULL,
  `idArea` int NOT NULL,
  `idOrgaoPublico` int unsigned NOT NULL,
  `icone` varchar(100) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `nome` (`nome`),
  KEY `fk_ServicoPublico_Area1_idx` (`idArea`),
  KEY `fk_ServicoPublico_OrgaoPublico1_idx` (`idOrgaoPublico`),
  CONSTRAINT `fk_ServicoPublico_Area1` FOREIGN KEY (`idArea`) REFERENCES `areadeservico` (`id`),
  CONSTRAINT `fk_ServicoPublico_OrgaoPublico1` FOREIGN KEY (`idOrgaoPublico`) REFERENCES `orgaopublico` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `servicopublico`
--

LOCK TABLES `servicopublico` WRITE;
/*!40000 ALTER TABLE `servicopublico` DISABLE KEYS */;
INSERT INTO `servicopublico` 
VALUES 
(1,'Clínico Geral',1,1,'fas fa-user-md'),
(2,'Clínico Geral',1,2,'fas fa-user-md'),
(3,'Fisioterapia Geral',1,3,'fas fa-bone'),
(4,'Acupuntura',1,3,'fab fa-pagelines'),
(5,'Psicólogo',1,1,'fas fa-brain'),
(6,'Cardiologista',1,2,'fas fa-heartbeat'),
(7,'Cardiologista',1,1,'fas fa-heartbeat'),
(8,'Nutricionista',1,1,'fas fa-apple-alt'),
(9,'Odontologista',1,1,'fas fa-tooth'),
(10,'Odontologista',1,2,'fas fa-tooth'),
(11,'Pilates',1,3,'fas fa-hands');
/*!40000 ALTER TABLE `servicopublico` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'agendeme'
--

--
-- Dumping routines for database 'agendeme'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-11-23 19:31:03
