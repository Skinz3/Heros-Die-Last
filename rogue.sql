/*
 Navicat Premium Data Transfer

 Source Server         : local
 Source Server Type    : MySQL
 Source Server Version : 50724
 Source Host           : localhost:3306
 Source Schema         : rogue

 Target Server Type    : MySQL
 Target Server Version : 50724
 File Encoding         : 65001

 Date: 20/07/2019 19:14:42
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for accounts
-- ----------------------------
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts`  (
  `Id` int(40) NOT NULL,
  `Username` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `Password` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `CharacterName` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `Email` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `Banned` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `Iron` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `Gold` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `LeaveRatio` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `FriendList` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = MyISAM CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of accounts
-- ----------------------------
INSERT INTO `accounts` VALUES (44, 'test44', 'test', 'test44', 'test44', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (45, 'test45', 'test', 'test45', 'test45', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (46, 'test46', 'test', 'test46', 'test46', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (47, 'test47', 'test', 'test47', 'test47', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (48, 'test48', 'test', 'test48', 'test48', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (43, 'test43', 'test', 'test43', 'test43', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (42, 'test42', 'test', 'test42', 'test42', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (41, 'test41', 'test', 'test41', 'test41', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (40, 'test40', 'test', 'test40', 'test40', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (39, 'test39', 'test', 'test39', 'test39', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (38, 'test38', 'test', 'test38', 'test38', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (37, 'test37', 'test', 'test37', 'test37', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (36, 'test36', 'test', 'test36', 'test36', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (35, 'test35', 'test', 'test35', 'test35', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (34, 'test34', 'test', 'test34', 'test34', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (33, 'test33', 'test', 'test33', 'test33', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (32, 'test32', 'test', 'test32', 'test32', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (31, 'test31', 'test', 'test31', 'test31', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (30, 'test30', 'test', 'test30', 'test30', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (29, 'test29', 'test', 'test29', 'test29', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (28, 'test28', 'test', 'test28', 'test28', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (27, 'test27', 'test', 'test27', 'test27', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (26, 'test26', 'test', 'test26', 'test26', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (25, 'test25', 'test', 'test25', 'test25', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (24, 'test24', 'test', 'test24', 'test24', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (23, 'test23', 'test', 'test23', 'test23', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (22, 'test22', 'test', 'test22', 'test22', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (21, 'test21', 'test', 'test21', 'test21', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (20, 'test20', 'test', 'test20', 'test20', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (19, 'test19', 'test', 'test19', 'test19', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (18, 'test18', 'test', 'test18', 'test18', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (17, 'test17', 'test', 'test17', 'test17', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (16, 'test16', 'test', 'test16', 'test16', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (15, 'test15', 'test', 'test15', 'test15', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (14, 'test14', 'test', 'test14', 'test14', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (13, 'test13', 'test', 'test13', 'test13', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (12, 'test12', 'test', 'test12', 'test12', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (11, 'test11', 'test', 'test11', 'test11', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (10, 'test10', 'test', 'test10', 'test10', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (9, 'test9', 'test', 'test9', 'test9', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (8, 'test8', 'test', 'test8', 'test8', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (7, 'test7', 'test', 'test7', 'test7', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (6, 'test6', 'test', 'test6', 'test6', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (5, 'test5', 'test', 'test5', 'test5', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (4, 'test4', 'test', 'test4', 'test4', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (3, 'test3', 'test', 'test3', 'test3', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (2, 'test2', 'test', 'test2', 'test2', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (1, 'test1', 'test', 'test1', 'test1', 'False', '0', '0', '0', '');
INSERT INTO `accounts` VALUES (49, 'test49', 'test', 'test49', 'test49', 'False', '0', '0', '0', '');

-- ----------------------------
-- Table structure for entities
-- ----------------------------
DROP TABLE IF EXISTS `entities`;
CREATE TABLE `entities`  (
  `Name` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `Width` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `Height` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `Animations` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `IdleAnimation` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `MovementAnimation` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `DefaultSpeed` float(10, 2) NULL DEFAULT NULL,
  `DefaultLife` int(255) NULL DEFAULT NULL
) ENGINE = MyISAM CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of entities
-- ----------------------------
INSERT INTO `entities` VALUES ('Blue', '144', '144', 'blueIdle,blueMove,item282,blueIdleHolding,blueMoveHolding', 'blueIdle', 'blueMove', 3.00, 8500);
INSERT INTO `entities` VALUES ('Slime', '144', '144', 'slimeMove,slimeIdle', 'slimeIdle', 'slimeMove', 1.50, 5000);

-- ----------------------------
-- Table structure for items
-- ----------------------------
DROP TABLE IF EXISTS `items`;
CREATE TABLE `items`  (
  `Id` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `Name` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `Icon` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `Type` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `InstantUse` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `Cooldown` float(65, 2) NULL DEFAULT NULL
) ENGINE = MyISAM CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of items
-- ----------------------------
INSERT INTO `items` VALUES ('282', 'FireballDash', 'item_280', 'Weapon', 'False', 1.00);
INSERT INTO `items` VALUES ('103', 'HitscanPistol', 'item_103', 'Weapon', 'False', 0.50);
INSERT INTO `items` VALUES ('315', 'Heal', 'item_315', 'Weapon', 'False', 4.00);

-- ----------------------------
-- Table structure for mapinteractives
-- ----------------------------
DROP TABLE IF EXISTS `mapinteractives`;
CREATE TABLE `mapinteractives`  (
  `Id` int(40) NOT NULL AUTO_INCREMENT,
  `MapId` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `CellId` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `LayerEnum` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `TriggerType` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `Value1` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `Value2` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `Value3` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `Value4` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `Value5` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `Value6` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `Value7` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 6 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of mapinteractives
-- ----------------------------
INSERT INTO `mapinteractives` VALUES (1, '2', '333', 'Second', 'Locker', 'sprite_307', '496', 'Second', 'Sprite', 'sprite_177', 'False', 'False');
INSERT INTO `mapinteractives` VALUES (2, '2', '776', 'Second', 'Teleport', 'arbres', '100', NULL, NULL, NULL, NULL, NULL);
INSERT INTO `mapinteractives` VALUES (3, '2', '401', 'Second', 'Chest', 'sprite_264', NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `mapinteractives` VALUES (4, '3', '224', 'Second', 'Chest', 'sprite_264', NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `mapinteractives` VALUES (5, '3', '308', 'Second', 'Teleport', 'donjon', NULL, NULL, NULL, NULL, NULL, NULL);

-- ----------------------------
-- Table structure for maps
-- ----------------------------
DROP TABLE IF EXISTS `maps`;
CREATE TABLE `maps`  (
  `Id` int(40) NOT NULL,
  `MapName` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `Frame` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `LonlyInstance` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `SpawnPositions` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = MyISAM CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of maps
-- ----------------------------
INSERT INTO `maps` VALUES (1, 'arbres', 'HUB', 'False', '86');
INSERT INTO `maps` VALUES (3, 'Map de test 1', 'HUB', 'False', '188');
INSERT INTO `maps` VALUES (2, 'donjon', 'HUB', 'False', '402');
INSERT INTO `maps` VALUES (4, 'big', 'HUB', 'False', '601');

SET FOREIGN_KEY_CHECKS = 1;
