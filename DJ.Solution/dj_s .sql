-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Nov 10, 2017 at 12:45 AM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dj_s`
--
CREATE DATABASE IF NOT EXISTS `dj_s` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `dj_s`;

-- --------------------------------------------------------

--
-- Table structure for table `events`
--

CREATE TABLE `events` (
  `id` int(11) NOT NULL,
  `start_time` datetime DEFAULT NULL,
  `end_time` datetime DEFAULT NULL,
  `event_name` varchar(255) DEFAULT NULL,
  `venue_name` varchar(255) DEFAULT NULL,
  `venue_address` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `events`
--

INSERT INTO `events` (`id`, `start_time`, `end_time`, `event_name`, `venue_name`, `venue_address`) VALUES
(16, '2017-11-10 18:00:00', '2017-11-10 21:00:00', 'Macy\'s Bridal Event', 'Macy\'s', '9300 SW Washington Square Rd'),
(17, '2017-11-11 11:00:00', '2017-11-11 15:00:00', '(Private Event)', '(Private Event)', '(Private Event)'),
(18, '2017-11-11 22:00:00', '2017-11-12 02:00:00', 'Hobo\'s', 'Hobo\'s', '120 NW 3rd Ave'),
(19, '2017-11-25 22:00:00', '2017-11-26 02:00:00', 'Hobo\'s', 'Hobo\'s', '120 NW 3rd Ave'),
(20, '2017-11-18 07:00:00', '2017-11-18 11:00:00', 'Doernbecher Freestyle Retail Launch', 'Nike Downtown Portland', '638 SW 5th Ave'),
(21, '2017-10-25 21:00:00', '2017-10-26 00:00:00', 'Brix', 'Brix', '1338 NW Hoyt'),
(22, '2017-10-26 18:00:00', '2017-10-26 21:30:00', 'Nike Corporate Basketball Viewing Party', 'Nike Downtown Portland', '638 SW 5th Ave'),
(23, '2017-12-16 20:00:00', '2017-12-16 23:00:00', '(Private Event)', '(Private Event)', '(Private Event)');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `events`
--
ALTER TABLE `events`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `start_time` (`start_time`),
  ADD UNIQUE KEY `end_time` (`end_time`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `events`
--
ALTER TABLE `events`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
