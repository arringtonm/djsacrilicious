-- ---
-- Globals
-- ---

-- SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
-- SET FOREIGN_KEY_CHECKS=0;

-- ---
-- Table 'events'
--
-- ---

DROP TABLE IF EXISTS `events`;

CREATE TABLE `events` (
  `id` INTEGER NOT NULL AUTO_INCREMENT DEFAULT NULL,
  `date` DATE NOT NULL,
  `start_time` TIME NULL DEFAULT NULL,
  `end_time` TIME NULL DEFAULT NULL,
  `event_id` INTEGER NULL DEFAULT NULL,
  `event_name` VARCHAR(255) NULL DEFAULT NULL,
  `location` VARCHAR(255) NULL DEFAULT NULL,
  `location_name` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY (`date`, `start_time`, `end_time`)
);

-- ---
-- Table 'repeat_events'
--
-- ---

DROP TABLE IF EXISTS `repeat_events`;

CREATE TABLE `repeat_events` (
  `event_id` INTEGER NOT NULL DEFAULT NULL,
  `day_id` TINYINT NOT NULL DEFAULT NULL
);

-- ---
-- Foreign Keys
-- ---


-- ---
-- Table Properties
-- ---

-- ALTER TABLE `events` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `repeat_events` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ---
-- Test Data
-- ---

-- INSERT INTO `events` (`id`,`date`,`start_time`,`end_time`,`event_id`,`event_name`,`location`,`location_name`) VALUES
-- ('','','','','','','','');
-- INSERT INTO `repeat_events` (`event_id`,`day_id`) VALUES
-- ('','');
