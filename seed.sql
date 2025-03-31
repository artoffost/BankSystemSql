CREATE DATABASE `banksystemsql` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

CREATE TABLE `users` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(45) NOT NULL,
  `Password` longtext NOT NULL,
  `FirstName` varchar(45) NOT NULL,
  `LastName` varchar(45) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `id_UNIQUE` (`Id`) /*!80000 INVISIBLE */,
  UNIQUE KEY `username_UNIQUE` (`Username`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `balance` (
  `idbalance` int NOT NULL AUTO_INCREMENT,
  `username` varchar(45) DEFAULT NULL,
  `amount` decimal(10,2) DEFAULT '0.00',
  PRIMARY KEY (`idbalance`),
  KEY `fk_balance_user` (`username`),
  CONSTRAINT `fk_balance_user` FOREIGN KEY (`username`) REFERENCES `users` (`Username`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
