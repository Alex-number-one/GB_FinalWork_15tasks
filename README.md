[task7.txt](https://github.com/user-attachments/files/15531417/task7.txt)# Итоговая контрольная работа
### Задание 1
![Screenshot 2024-06-01 162836](https://github.com/Alex-number-one/GB_FinalWork_15tasks/assets/136375782/d8df1225-79bd-440a-b996-81dbf5e8071c)
### Задание 2
![Screenshot 2024-06-01 162836](https://github.com/Alex-number-one/GB_FinalWork_15tasks/assets/136375782/13e19af3-4ad7-440e-a00c-8df8b6232804)
### Задание 3
wget https://dev.mysql.com/get/mysql-apt-config_0.8.26-1_all.deb

sudo dpkg -i mysql-apt-config_0.8.26-1_all.deb

sudo apt-get update

sudo apt-get install mysql-server

systemctl status mysql

![task3](https://github.com/Alex-number-one/GB_FinalWork_15tasks/assets/136375782/f9c0e49f-a126-452a-8f9b-50eaedfcceb0)
### Задание 4
![task4](https://github.com/Alex-number-one/GB_FinalWork_15tasks/assets/136375782/c331f840-994e-46cf-8436-038847dc1b3f)
![task4 2](https://github.com/Alex-number-one/GB_FinalWork_15tasks/assets/136375782/5cdac1dc-d4ef-4e49-9d92-8a3eab9b24f0)
### Задание 5
![task5](https://github.com/Alex-number-one/GB_FinalWork_15tasks/assets/136375782/d57a4854-19d0-41ba-99c2-045793c12cba)

P.S. У меня несколько раз вылетал Virtual Box в Ubuntu, так что в History осталось только несколько команд.
### Задание 6
![task6](https://github.com/Alex-number-one/GB_FinalWork_15tasks/assets/136375782/0ca5ea11-70c6-4da5-97cd-7d288cd96416)
### Задание 7
//В подключенном MySQL репозитории создать базу данных “Друзья человека”

CREATE DATABASE peopleFriends;
### Задание 8
// Создать таблицы с иерархией из диаграммы в БД
USE peopleFriends;

CREATE TABLE dog (
	id INT PRIMARY KEY AUTO_INCREMENT,
	animal_name CHAR(30),
    commands TEXT,
    date_of_birth DATE
);

CREATE TABLE cat (
	id INT PRIMARY KEY AUTO_INCREMENT,
	animal_name CHAR(30),
    commands TEXT,
    date_of_birth DATE
);

CREATE TABLE hamster (
	id INT PRIMARY KEY AUTO_INCREMENT,
	animal_name CHAR(30),
    commands TEXT,
    date_of_birth DATE
);

CREATE TABLE horse (
	id INT PRIMARY KEY AUTO_INCREMENT,
	animal_name CHAR(30),
    commands TEXT,
    date_of_birth DATE
);

CREATE TABLE camel (
	id INT PRIMARY KEY AUTO_INCREMENT,
	animal_name CHAR(30),
    commands TEXT,
    date_of_birth DATE
);

CREATE TABLE donkey (
	id INT PRIMARY KEY AUTO_INCREMENT,
	animal_name CHAR(30),
    commands TEXT,
    date_of_birth DATE
);
### Задание 9
INSERT INTO dog (animal_name, commands, date_of_birth) VALUES 
	('Oddy', 'fetch', '2017-01-06'),
	('Radar', 'stand up', '2019-11-06');

INSERT INTO cat (animal_name, commands, date_of_birth) VALUES 
	('Puff', 'voice', '2024-02-04'),
	('Garfield', 'eat', '2024-12-03');
    
INSERT INTO hamster (animal_name, commands, date_of_birth) VALUES 
	('Bro', 'eat', '2021-02-03'),
	('Cheeky', 'walk', '2024-12-10');
    
INSERT INTO horse (animal_name, commands, date_of_birth) VALUES 
	('Billy', 'eat', '2021-01-02'),
	('Chop', 'eat', '2024-12-10');
    
INSERT INTO camel (animal_name, commands, date_of_birth) VALUES 
	('Humpy', 'stop', '2021-01-01'),
	('Fott', 'eat', '2018-16-09');
   
INSERT INTO donkey (animal_name, commands, date_of_birth) VALUES 
	('Lala', 'go', '2023-01-01'),
	('Chupa', 'eat', '2013-10-08');
### Задание 10
TRUNCATE TABLE camel;

SELECT * FROM horse
UNION SELECT * FROM donkey
AS pack_animals;
### Задание 11
CREATE TABLE young_animal (
	id INT PRIMARY KEY AUTO_INCREMENT,
	animal_name CHAR(30),
    commands TEXT,
    date_of_birth DATE,
    age TEXT
);


DELIMITER $$
CREATE FUNCTION age_animal (date_b DATE)
RETURNS TEXT
DETERMINISTIC
BEGIN
    DECLARE res TEXT DEFAULT '';
	SET res = CONCAT(
            TIMESTAMPDIFF(YEAR, date_b, CURDATE()),
            ' years ',
            TIMESTAMPDIFF(MONTH, date_b, CURDATE()) % 12,
            ' month'
        );
	RETURN res;
END $$
DELIMITER ;

INSERT INTO young_animal (animal_name, commands, date_of_birth, age)
SELECT animal_name, commands, date_of_birth, age_animal(date_of_birth)
FROM cat
WHERE TIMESTAMPDIFF(YEAR, date_of_birth, CURDATE()) BETWEEN 1 AND 3
UNION ALL
SELECT animal_name, commands, date_of_birth, age_animal(date_of_birth)
FROM dog
WHERE TIMESTAMPDIFF(YEAR, date_of_birth, CURDATE()) BETWEEN 1 AND 3
UNION ALL
SELECT animal_name, commands, date_of_birth, age_animal(date_of_birth)
FROM hamster
WHERE TIMESTAMPDIFF(YEAR, date_of_birth, CURDATE()) BETWEEN 1 AND 3
UNION ALL
SELECT animal_name, commands, date_of_birth, age_animal(date_of_birth)
FROM pack_animals
WHERE TIMESTAMPDIFF(YEAR, date_of_birth, CURDATE()) BETWEEN 1 AND 3;

SET SQL_SAFE_UPDATES = 0;

DELETE FROM cat 
WHERE TIMESTAMPDIFF(YEAR, cat.date_of_birth, CURDATE()) IN (1, 2, 3);

DELETE FROM dog 
WHERE TIMESTAMPDIFF(YEAR, date_of_birth, CURDATE()) BETWEEN 1 AND 3;

DELETE FROM hamster 
WHERE TIMESTAMPDIFF(YEAR, date_of_birth, CURDATE()) BETWEEN 1 AND 3;

DELETE FROM pack_animals
WHERE TIMESTAMPDIFF(YEAR, date_of_birth, CURDATE()) BETWEEN 1 AND 3;
### Задание 12
CREATE TABLE animals (
	id INT PRIMARY KEY AUTO_INCREMENT,
	animal_name CHAR(20),
    commands TEXT,
    date_of_birth DATE,
    age TEXT,
    animal_type ENUM('cat','dog','hamster', 'pack_animals', 'young_animals') NOT NULL
);

INSERT INTO animals (animal_name, commands, date_of_birth, age, animal_type)
SELECT animal_name, commands, date_of_birth, age_animal(date_of_birth), 'cat'
FROM cat;

INSERT INTO animals (animal_name, commands, date_of_birth, age, animal_type)
SELECT animal_name, commands, date_of_birth, age_animal(date_of_birth), 'dog'
FROM dog;

INSERT INTO animals (animal_name, commands, date_of_birth, age, animal_type)
SELECT animal_name, commands, date_of_birth, age_animal(date_of_birth), 'hamster'
FROM hamster;

INSERT INTO animals (animal_name, commands, date_of_birth, age, animal_type)
SELECT animal_name, commands, date_of_birth, age_animal(date_of_birth), 'pack_animals'
FROM pack_animals;

INSERT INTO animals (animal_name, commands, date_of_birth, age, animal_type)
SELECT animal_name, commands, date_of_birth, age_animal(date_of_birth), 'young_animals'
FROM young_animal;
### Задания 13-15
