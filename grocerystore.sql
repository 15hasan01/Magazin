-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1
-- Време на генериране: 10 юни 2024 в 23:45
-- Версия на сървъра: 10.4.32-MariaDB
-- Версия на PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данни: `grocerystore`
--

-- --------------------------------------------------------

--
-- Структура на таблица `customer`
--

CREATE TABLE `customer` (
  `CustomerId` int(11) NOT NULL,
  `FirstName` varchar(255) NOT NULL,
  `LastName` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Схема на данните от таблица `customer`
--

INSERT INTO `customer` (`CustomerId`, `FirstName`, `LastName`) VALUES
(1, 'Брус', 'Лий'),
(2, 'Били', 'Хлапето'),
(3, 'Миямото', 'Мусаши'),
(4, 'Черната', 'Брада'),
(5, 'Мартин', 'Катев'),
(6, 'Snake', 'Plissken'),
(7, 'Elon', 'Musk');

-- --------------------------------------------------------

--
-- Структура на таблица `product`
--

CREATE TABLE `product` (
  `ProductId` int(11) NOT NULL,
  `Description` varchar(255) NOT NULL,
  `Category` varchar(255) NOT NULL,
  `WholesalePrice` decimal(10,2) NOT NULL,
  `RetailPrice` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Схема на данните от таблица `product`
--

INSERT INTO `product` (`ProductId`, `Description`, `Category`, `WholesalePrice`, `RetailPrice`) VALUES
(1, 'Coca-Cola', 'Напитки', 0.60, 1.00),
(2, 'Pepsi', 'Напитки', 0.58, 1.00),
(3, 'Минерална вода', 'Напитки', 0.20, 0.50),
(4, 'Сок от праскова', 'Напитки', 0.80, 1.50),
(5, 'Кисело мляко', 'Хранителни стоки', 0.90, 1.30),
(6, 'Хляб', 'Хранителни стоки', 0.50, 0.80),
(7, 'Шоколад', 'Хранителни стоки', 1.80, 2.90),
(8, 'Бисквити', 'Хранителни стоки', 1.70, 3.00),
(9, 'Чипс', 'Хранителни стоки', 0.60, 1.20),
(10, 'Цигари', 'Цигари', 2.50, 4.50),
(11, 'Пури', 'Цигари', 3.00, 5.00),
(12, 'Шампоан', 'Козметика', 1.50, 3.00),
(13, 'Паста за зъби', 'Козметика', 1.00, 2.50),
(14, 'Сапун', 'Козметика', 0.70, 1.20),
(15, 'Крем за ръце', 'Козметика', 2.00, 3.50),
(16, 'Лукчета', 'Хранителни стоки', 0.80, 1.60);

-- --------------------------------------------------------

--
-- Структура на таблица `sale`
--

CREATE TABLE `sale` (
  `SaleId` int(11) NOT NULL,
  `CustomerId` int(11) NOT NULL,
  `ProductId` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `Date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Схема на данните от таблица `sale`
--

INSERT INTO `sale` (`SaleId`, `CustomerId`, `ProductId`, `Quantity`, `Date`) VALUES
(2, 1, 5, 2, '2024-06-10 22:51:08'),
(3, 4, 3, 7, '2024-06-10 23:25:25'),
(4, 4, 3, 7, '2024-06-10 23:26:05'),
(5, 4, 7, 3, '2024-06-10 23:30:03'),
(6, 2, 5, 7, '2024-06-10 23:52:44'),
(7, 3, 3, 1, '2024-06-11 00:02:57'),
(8, 5, 2, 4, '2024-06-11 00:04:00'),
(9, 5, 5, 11, '2024-06-11 00:04:00'),
(10, 7, 5, 5, '2024-06-11 00:16:26'),
(11, 7, 6, 1, '2024-06-11 00:16:26'),
(12, 7, 1, 14, '2024-06-11 00:16:26');

--
-- Indexes for dumped tables
--

--
-- Индекси за таблица `customer`
--
ALTER TABLE `customer`
  ADD PRIMARY KEY (`CustomerId`);

--
-- Индекси за таблица `product`
--
ALTER TABLE `product`
  ADD PRIMARY KEY (`ProductId`);

--
-- Индекси за таблица `sale`
--
ALTER TABLE `sale`
  ADD PRIMARY KEY (`SaleId`),
  ADD KEY `CustomerId` (`CustomerId`),
  ADD KEY `ProductId` (`ProductId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `customer`
--
ALTER TABLE `customer`
  MODIFY `CustomerId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `product`
--
ALTER TABLE `product`
  MODIFY `ProductId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;

--
-- AUTO_INCREMENT for table `sale`
--
ALTER TABLE `sale`
  MODIFY `SaleId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- Ограничения за дъмпнати таблици
--

--
-- Ограничения за таблица `sale`
--
ALTER TABLE `sale`
  ADD CONSTRAINT `sale_ibfk_1` FOREIGN KEY (`CustomerId`) REFERENCES `customer` (`CustomerId`),
  ADD CONSTRAINT `sale_ibfk_2` FOREIGN KEY (`ProductId`) REFERENCES `product` (`ProductId`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
