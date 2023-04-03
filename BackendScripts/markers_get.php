<?php

require_once "config.php";

if($_SERVER["REQUEST_METHOD"] == "GET"){
   	$query = "SELECT * FROM markers_1";
	$result= mysqli_query($link, $query);
	$n = mysqli_num_rows($result);
	for ($i = 0; $i < $n; $i++)
	{
		$title = mysqli_fetch_assoc($result)["title"];
		$building_location = mysqli_fetch_assoc($result)["building_location"];
		$time = mysqli_fetch_assoc($result)["time"];
		$date = mysqli_fetch_assoc($result)["date"];
		$end_date = mysqli_fetch_assoc($result)["end_date"];
		$info = mysqli_fetch_assoc($result)["info"];
		$floor = mysqli_fetch_assoc($result)["floor"];
		$is_event = mysqli_fetch_assoc($result)["is_event"];
		$is_repeating_event = mysqli_fetch_assoc($result)["is_repeating_event"];
		$last_updated = mysqli_fetch_assoc($result)["last_updated"];
		$room = mysqli_fetch_assoc($result)["room"];
		$longitude = mysqli_fetch_assoc($result)["longitude"];
		$latitude = mysqli_fetch_assoc($result)["latitude"];
		$is_service = mysqli_fetch_assoc($result)["is_service"];
		$is_res = mysqli_fetch_assoc($result)["is_res"];


		echo "Title:".$title;
		echo "building_location:".$building_location;
		echo "room:".$room;
		echo "time:".$time;
		echo "date:".$date;
		echo "end_date:".$end_date;
		echo "info:".$info;
		echo "floor:".$floor;
		echo "is_event:".$is_event;
		echo "is_repeating_event:".$is_repeating_event;
		echo "last_updated:".$last_updated;
		echo "is_service:".$is_service;
		echo "is_res:".$is_res;
		echo "longitude:".$longitude;
		echo "latitude:".$latitude;

		
	}
	
	mysqli_close($link);

}
	
?>



