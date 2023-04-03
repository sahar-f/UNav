<?php

require_once "config.php";

$title = $building_location = $room = $time = $date = $end_date = $info = "";
$floor = $is_event = $is_repeating_event = $is_res = $is_service = $longitude = $latitude = "";
$last_updated = date('Y-m-d H:i:s');

$title_err = $building_location_err = $room_err = $time_err = $date_err = $end_date_err = $info_err = "";
$floor_err = $is_event_err = $is_repeating_event_err = $is_res_err = $is_service_err = $longitude_err = $latitude_err = "";

if($_SERVER["REQUEST_METHOD"] == "POST"){
   	
	// validate title
	$input_title = trim($_POST["title"]);
	if(empty($input_title)){
        $title_err = "Please enter a title.";
    } else{
        $title = $input_title;
    }
	
	//validate other strings 
	$input_bl = trim($_POST["building_location"]);
    if(empty($input_bl)){
        $building_location_err = "Please enter a building location.";     
    } else{
        $building_location = $input_bl;
    }
	
	$input_room = trim($_POST["room"]);
    if(empty($input_room)){
        $room_err = "Please enter a room name.";     
    } else{
        $room = $input_room;
    }
	
	$input_time = trim($_POST["time"]);
    if(empty($input_time)){
        $time_err = "Please enter a time for the event.";     
    } else{
        $time = $input_time;
    }
	
	$input_date = trim($_POST["date"]);
    if(empty($input_date)){
        $date_err = "Please enter a date for the event.";     
    } else{
        $date = $input_date;
    }
	
	$input_end_date = trim($_POST["end_date"]);
    if(empty($input_end_date)){
        $end_date_err = "Please enter an end date for the event.";     
    } else{
        $end_date = $input_end_date;
    }
	
	$input_info = trim($_POST["info"]);
    if(empty($input_info)){
        $info_err = "Please enter information for the event.";     
    } else{
        $info = $input_info;
    }
	
	//validate integers 
	$input_floor = trim($_POST["floor"]);
    if(!isset($input_floor)){
        $floor_err = "Please enter floor number.";     
    } else{
        $floor = $input_floor;
    }
	
	$input_longitude = trim($_POST["longitude"]);
    if(empty($input_longitude)){
        $longitude_err = "Please enter a longitude number.";     
    } else{
        $longitude = $input_longitude;
    }
	
	$input_latitude = trim($_POST["latitude"]);
    if(empty($input_latitude)){
        $latitude_err = "Please enter a latitude number.";     
    } else{
        $latitude = $input_latitude;
    }
	
	$input_is_event = trim($_POST["is_event"]);
    if(!isset($input_is_event)){
        $is_event_err = "Please enter is_event (0-no, 1-yes).";   
	} elseif(!ctype_digit($input_is_event)){
        $is_event_err = "Please enter a positive integer value.";		
    } else{
        $is_event = $input_is_event;
    }
	
	$input_is_service = trim($_POST["is_service"]);
    if(!isset($input_is_service)){
        $is_service_err = "Please enter is_service (0-no, 1-yes).";   
	} elseif(!ctype_digit($input_is_event)){
        $is_service_err = "Please enter a positive integer value.";		
    } else{
        $is_service = $input_is_service;
    }
	
	$input_is_res = trim($_POST["is_res"]);
    if(!isset($input_is_res)){
        $is_res_err = "Please enter is_restaurant (0-no, 1-yes).";   
	} elseif(!ctype_digit($input_is_event)){
        $is_res_err = "Please enter a positive integer value.";		
    } else{
        $is_res = $input_is_res;
    }
		
	$input_is_repeating_event = trim($_POST["is_repeating_event"]);
    if(!isset($input_is_repeating_event)){
        $is_repeating_event_err = "Please enter is_repeating_event (0-no, 1-yes).";     
	} elseif(!ctype_digit($input_is_repeating_event)){
        $is_repeating_event_err = "Please enter a positive integer value.";	
    } else{
        $is_repeating_event = $input_is_repeating_event;
    }
	
	// Check input errors before inserting in database
    if((empty($title_err) && empty($building_location_err) && empty($room_err) && empty($time_err) && empty($date_err) && empty($info_err) && empty($floor_err) && empty($is_event_err) && empty($is_repeating_event_err) && empty($end_date_err) && empty($longitude_err) && empty($latitude_err) && empty($is_res_err) && empty($is_service_err))){
        // Prepare an insert statement
        $query = "INSERT INTO markers_1 (title, building_location, room, time, date, info, floor, is_event, is_repeating_event, last_updated, end_date, longitude, latitude, is_service, is_res) VALUES ('$title', '$building_location', '$room', '$time', '$date', '$info', '$floor', '$is_event', '$is_repeating_event', '$last_updated', '$end_date', '$longitude', '$latitude', '$is_service', '$is_res');";		

		if ($result= mysqli_query($link,$query)) 
		{
			echo "/Insert Successful";
			// Records updated successfully. Redirect to landing page
            header("location: dashboard.php");
            exit();
		}
		else
		{
			echo "/Insert UnSuccessful";
		}
	}
	
	// Close connection
    mysqli_close($link);
}
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Create Record</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <style>
        .wrapper{
            width: 600px;
            margin: 0 auto;
        }
    </style>
</head>
<body>
    <div class="wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
					<div class="mt-5 mb-3 clearfix">
						<h2 class="pull-left">Create Marker</h2>
					</div>
                    <p>Please fill this form and submit to add a marker to the database.</p>
                    <form action="<?php echo htmlspecialchars($_SERVER["PHP_SELF"]); ?>" method="post">
                        <div class="form-group">
                            <label>Title</label>
                            <input type="text" name="title" class="form-control <?php echo (!empty($title_err)) ? 'is-invalid' : ''; ?>" value="<?php echo $title; ?>">
                            <span class="invalid-feedback"><?php echo $title_err;?></span>
                        </div>
			
                        <div class="form-group">
                            <label>Building or Location</label>
                            <textarea name="building_location" class="form-control <?php echo (!empty($building_location_err)) ? 'is-invalid' : ''; ?>"><?php echo $building_location; ?></textarea>
                            <span class="invalid-feedback"><?php echo $building_location_err;?></span>
                        </div>
						
						<div class="form-group">
                            <label>Room Name/Number (if outside put outside)</label>
                            <input type="text" name="room" class="form-control <?php echo (!empty($room_err)) ? 'is-invalid' : ''; ?>" value="<?php echo $room; ?>">
                            <span class="invalid-feedback"><?php echo $room_err;?></span>
                        </div>
						
						<div class="form-group">
                            <label>Time for event</label>
                            <input type="text" name="time" class="form-control <?php echo (!empty($time_err)) ? 'is-invalid' : ''; ?>" value="<?php echo $time; ?>">
                            <span class="invalid-feedback"><?php echo $time_err;?></span>
                        </div>
						
						<div class="form-group">
                            <label>Date of event</label>
                            <input type="text" name="date" class="form-control <?php echo (!empty($date_err)) ? 'is-invalid' : ''; ?>" value="<?php echo $date; ?>">
                            <span class="invalid-feedback"><?php echo $date_err;?></span>
                        </div>
						
						<div class="form-group">
                            <label>End date of marker</label>
                            <input type="text" name="end_date" class="form-control <?php echo (!empty($end_date_err)) ? 'is-invalid' : ''; ?>" value="<?php echo $end_date; ?>">
                            <span class="invalid-feedback"><?php echo $end_date_err;?></span>
                        </div>
						
						<div class="form-group">
                            <label>Information</label>
                            <textarea name="info" class="form-control <?php echo (!empty($info_err)) ? 'is-invalid' : ''; ?>"><?php echo $info; ?></textarea>
                            <span class="invalid-feedback"><?php echo $info_err;?></span>
                        </div>
							
                        <div class="form-group">
                            <label>Floor (0 if outside)</label>
                            <input type="text" name="floor" class="form-control <?php echo (!empty($floor_err)) ? 'is-invalid' : ''; ?>" value="<?php echo $floor; ?>">
                            <span class="invalid-feedback"><?php echo $floor_err;?></span>
                        </div>
						
						<div class="form-group">
                            <label>Is a event or something else? (0- no, 1- yes)</label>
                            <input type="text" name="is_event" class="form-control <?php echo (!empty($is_event_err)) ? 'is-invalid' : ''; ?>" value="<?php echo $is_event; ?>">
                            <span class="invalid-feedback"><?php echo $is_event_err;?></span>
                        </div>
						
						<div class="form-group">
                            <label>Is a is_repeating_event? (0- no, 1- yes)</label>
                            <input type="text" name="is_repeating_event" class="form-control <?php echo (!empty($is_repeating_event_err)) ? 'is-invalid' : ''; ?>" value="<?php echo $is_repeating_event; ?>">
                            <span class="invalid-feedback"><?php echo $is_repeating_event_err;?></span>
                        </div>
						
						<div class="form-group">
                            <label>Is a service or something else? (0- no, 1- yes)</label>
                            <input type="text" name="is_service" class="form-control <?php echo (!empty($is_service_err)) ? 'is-invalid' : ''; ?>" value="<?php echo $is_service; ?>">
                            <span class="invalid-feedback"><?php echo $is_service_err;?></span>
                        </div>
						
						<div class="form-group">
                            <label>Is a restaurant or something else? (0- no, 1- yes)</label>
                            <input type="text" name="is_res" class="form-control <?php echo (!empty($is_res_err)) ? 'is-invalid' : ''; ?>" value="<?php echo $is_res; ?>">
                            <span class="invalid-feedback"><?php echo $is_res_err;?></span>
                        </div>
						
						<div class="form-group">
                            <label>Longitude of marker</label>
                            <input type="text" name="longitude" class="form-control <?php echo (!empty($longitude_err)) ? 'is-invalid' : ''; ?>" value="<?php echo $longitude; ?>">
                            <span class="invalid-feedback"><?php echo $longitude_err;?></span>
                        </div>
						
						<div class="form-group">
                            <label>Latitude of marker</label>
                            <input type="text" name="latitude" class="form-control <?php echo (!empty($latitude_err)) ? 'is-invalid' : ''; ?>" value="<?php echo $latitude; ?>">
                            <span class="invalid-feedback"><?php echo $latitude_err;?></span>
                        </div>
												
                        <input type="submit" class="btn btn-primary" value="Submit">
                        <a href="dashboard.php" class="btn btn-secondary ml-2">Cancel</a>
                    </form>
                </div>
            </div>        
        </div>
    </div>
</body>
</html>


