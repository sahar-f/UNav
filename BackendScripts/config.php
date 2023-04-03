<?php

define('DB_SERVER', 'unav-db-instance.cuqmqs8n3aps.us-west-2.rds.amazonaws.com');
define('DB_USERNAME', 'capstone');
define('DB_PASSWORD', 'Captone2022$');
define('DB_DATABASE', 'UNAVDB');

/* Attempt to connect to MySQL database */
$link = mysqli_connect(DB_SERVER,'capstone','Capstone2022$',DB_DATABASE, 3306);

// Check connection
if (mysqli_connect_errno()) 
{
	echo "Failed to Connect: " . mysqli_connect_error();
}

?>
