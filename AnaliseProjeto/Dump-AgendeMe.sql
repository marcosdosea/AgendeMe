CREATE DATABASE  IF NOT EXISTS `agendeme` /*!40100 DEFAULT CHARACTER SET utf8mb3 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `agendeme`;
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
-- Table structure for table `aspnetroleclaims`
--

DROP TABLE IF EXISTS `aspnetroleclaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetroleclaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(767) NOT NULL,
  `ClaimType` text,
  `ClaimValue` text,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetroleclaims`
--

LOCK TABLES `aspnetroleclaims` WRITE;
/*!40000 ALTER TABLE `aspnetroleclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetroleclaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetroles`
--

DROP TABLE IF EXISTS `aspnetroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetroles` (
  `Id` varchar(767) NOT NULL,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(256) DEFAULT NULL,
  `ConcurrencyStamp` text,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetroles`
--

LOCK TABLES `aspnetroles` WRITE;
/*!40000 ALTER TABLE `aspnetroles` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserclaims`
--

DROP TABLE IF EXISTS `aspnetuserclaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserclaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` varchar(767) NOT NULL,
  `ClaimType` text,
  `ClaimValue` text,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserclaims`
--

LOCK TABLES `aspnetuserclaims` WRITE;
/*!40000 ALTER TABLE `aspnetuserclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserclaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserlogins`
--

DROP TABLE IF EXISTS `aspnetuserlogins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserlogins` (
  `LoginProvider` varchar(128) NOT NULL,
  `ProviderKey` varchar(128) NOT NULL,
  `ProviderDisplayName` text,
  `UserId` varchar(767) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserlogins`
--

LOCK TABLES `aspnetuserlogins` WRITE;
/*!40000 ALTER TABLE `aspnetuserlogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserlogins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserroles`
--

DROP TABLE IF EXISTS `aspnetuserroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserroles` (
  `UserId` varchar(767) NOT NULL,
  `RoleId` varchar(767) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserroles`
--

LOCK TABLES `aspnetuserroles` WRITE;
/*!40000 ALTER TABLE `aspnetuserroles` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetusers`
--

DROP TABLE IF EXISTS `aspnetusers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetusers` (
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
-- Dumping data for table `aspnetusers`
--

LOCK TABLES `aspnetusers` WRITE;
/*!40000 ALTER TABLE `aspnetusers` DISABLE KEYS */;
INSERT INTO `aspnetusers` VALUES ('320d61b6-1a06-49a0-bab6-8afb1973ccc7','bestlaurobr@gmail.com','BESTLAUROBR@GMAIL.COM','bestlaurobr@gmail.com','BESTLAUROBR@GMAIL.COM',_binary '\0','AQAAAAEAACcQAAAAEIlaJTk8O3pyeTXpQ6vXYAVfx874qipILvw9YzCyiwKtuzuyPFAircvFxGtNL6gJ/Q==','TQUVBNZYUEAC4FDUJYHK5PVN5JLYNDYF','0de031fd-8b34-47de-b403-33a6ebf9d1c6',NULL,_binary '\0',_binary '\0',NULL,_binary '',0);
/*!40000 ALTER TABLE `aspnetusers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetusertokens`
--

DROP TABLE IF EXISTS `aspnetusertokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetusertokens` (
  `UserId` varchar(767) NOT NULL,
  `LoginProvider` varchar(128) NOT NULL,
  `Name` varchar(128) NOT NULL,
  `Value` text,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetusertokens`
--

LOCK TABLES `aspnetusertokens` WRITE;
/*!40000 ALTER TABLE `aspnetusertokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetusertokens` ENABLE KEYS */;
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
INSERT INTO `cidadao` VALUES (1,'José da Silva','999.999.999-09','1998-10-09','888 9999 7777 6666','999998888','garelaxxx@yahool.com','49530-000','SE','Ribeirópolis','Centro','Rua Manoel','69','M','Profissional','Qualquer Complemento',NULL,NULL),(2,'Julia Almeida','888.999.999-09','2000-10-09','888 5999 7777 6666','799998888','garelaxx2x@yahool.com','49530-000','SE','Ribeirópolis','Centro','Rua Juju','65','F','Cidadao','Qualquer Complemento',NULL,NULL),(4,'José','999.999.929-09','1998-10-09','888 9999 7772 6666','999998888','garelaxxxs@yahool.com','49530-000','SE','Ribeirópolis','Centro','Rua Manoel','69','M','Cidadao','Qualquer Complemento',NULL,NULL);
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
INSERT INTO `diaagendamento` VALUES (1,'2022-10-24 00:00:00','Segunda','08:00','12:00',10,8,2,0,1),(2,'2022-10-24 00:00:00','Segunda','13:00','17:00',10,2,2,0,1),(3,'2022-10-25 00:00:00','Terça','08:00','12:00',8,0,2,0,1),(4,'2022-10-25 00:00:00','Terça','13:00','17:00',10,0,2,0,1),(5,'2022-10-26 00:00:00','Quarta','08:00','12:00',10,0,2,0,1),(6,'2022-10-26 00:00:00','Quarta','13:00','17:00',10,0,2,0,1),(7,'2022-10-27 00:00:00','Quinta','08:00','12:00',10,0,2,0,1),(8,'2022-10-27 00:00:00','Quinta','13:00','17:00',10,0,2,0,1),(9,'2022-10-28 00:00:00','Sexta','08:00','12:00',10,0,2,0,1),(10,'2022-10-28 00:00:00','Sexta','13:00','17:00',10,0,2,0,1),(11,'2022-10-24 00:00:00','Segunda','08:00','12:00',10,0,2,0,2),(12,'2022-10-24 00:00:00','Segunda','13:00','17:00',10,0,2,0,3),(13,'2022-10-25 00:00:00','Terça','08:00','12:00',10,0,2,0,2),(14,'2022-10-25 00:00:00','Terça','13:00','17:00',10,0,2,0,3),(15,'2022-10-26 00:00:00','Quarta','08:00','12:00',10,0,2,0,2),(16,'2022-10-26 00:00:00','Quarta','13:00','17:00',10,0,2,0,3),(17,'2022-10-27 00:00:00','Quinta','08:00','12:00',10,0,2,0,2),(18,'2022-10-27 00:00:00','Quinta','13:00','17:00',10,0,2,0,3),(19,'2022-10-28 00:00:00','Sexta','08:00','12:00',10,0,2,0,2),(20,'2022-10-28 00:00:00','Sexta','13:00','17:00',10,0,2,0,3),(21,'2022-10-28 00:00:00','Sexta','07:00','09:00',10,0,2,0,12),(22,'2022-10-28 00:00:00','Sexta','15:00','17:00',10,0,2,0,12),(23,'2022-10-24 00:00:00','Sexta','08:00','12:00',10,0,2,0,9),(24,'2022-10-28 00:00:00','Sexta','08:00','12:00',10,0,2,0,9),(25,'2022-10-25 00:00:00','Sexta','08:00','12:00',10,0,2,0,10),(26,'2022-10-27 00:00:00','Sexta','08:00','12:00',10,0,2,0,10);
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
INSERT INTO `orgaopublico` VALUES (1,'Clínica Dona Mininha','Centro','Rua da Clínica Dona Mininha','244','Próximo ao Simas Turbo','49530-000','08:00','18:00',1),(2,'Clínica Lá Lá','Centro','Rua da Clínica Lá Lá','36','Próximo ao México Lindo','49530-000','07:00','12:00',1),(3,'Clínica Dona Mininho','Centro','Rua da Clínica Dona Mininho','24','Próximo ao Seu Cuca é Eu?','49530-000','08:00','13:00',1),(4,'Academia de Saúde','Centro','Rua da Academia','24','Próximo ao bar do Jadeu','49530-000','08:00','13:00',1),(5,'Secretaria de Agricultura','Centro','Rua da Academia','24','Próximo ao Tilambucano','49530-000','08:00','13:00',1),(6,'Secretaria de Transporte','Centro','Rua da Academia','24','Próximo ao Arizona','49530-000','08:00','13:00',1);
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
INSERT INTO `prefeitura` VALUES (1,'Prefeitura de Ribeirópolis','897892344','SE','Ribeirópolis','Centro','2344444','Rua Teste','122','Qualquercoisaaqui');
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
INSERT INTO `servicopublico` VALUES (1,'Clínico Geral',1,1,'fas fa-user-md'),(2,'Clínico Geral',1,2,'fas fa-user-md'),(3,'Clínico Geral',1,3,'fas fa-user-md'),(4,'Psicólogo',1,3,'fas fa-brain'),(5,'Psicólogo',1,1,'fas fa-brain'),(6,'Cardiologista',1,2,'fas fa-heartbeat'),(7,'Cardiologista',1,1,'fas fa-heartbeat'),(8,'Nutricionista',1,1,'fas fa-apple-alt'),(9,'Odontologista',1,1,'fas fa-tooth'),(10,'Odontologista',1,2,'fas fa-tooth'),(11,'Odontologista',1,3,'fas fa-tooth'),(12,'Treino Terceira Idade',7,4,'fas fa-blind');
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
