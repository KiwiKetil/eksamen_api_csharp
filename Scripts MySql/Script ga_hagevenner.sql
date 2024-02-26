drop database if exists ga_hagevenner; 
create database ga_hagevenner;
use ga_hagevenner; 

#CREATE USER IF NOT EXISTS 'ga-app'@'localhost' IDENTIFIED BY 'ga-5ecret-%';
#CREATE USER IF NOT EXISTS 'ga-app'@'%' IDENTIFIED BY 'ga-5ecret-%';

GRANT ALL privileges ON ga_hagevenner.* TO 'ga-app'@'%'; 
GRANT ALL privileges ON ga_hagevenner.* TO 'ga-app'@'localhost';  

FLUSH PRIVILEGES; 