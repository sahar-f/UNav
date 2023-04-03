<?php
// Initialize the session
session_start();
 
// Check if the user is logged in, if not then redirect to login page
if(!isset($_SESSION["loggedin"]) || $_SESSION["loggedin"] !== true){
    header("location: index.php");
    exit;
}
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Dashboard</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <style>
        .wrapper{
            width: 600px;
        }
        table tr td:last-child{
            width: 100px;
        }
		
		p{ font: 14px sans-serif; text-align: right; }
    </style>
    <script>
        $(document).ready(function(){
            $('[data-toggle="tooltip"]').tooltip();   
        });
    </script>
</head>
<body>
    <div class="wrapper">
		<h2 class="pl-3 pt-3">Markers Details</h2>
		<p>
			<a href="markers_create.php" class="btn btn-success"><i class="fa fa-plus"></i> Add New Marker</a>
			<a href="reset-password.php" class="btn btn-warning ml-3">Reset Your Password</a>
			<a href="logout.php" class="btn btn-danger ml-3">Sign Out of Your Account</a>
		</p>
        <div class="container-fluid no-gutters">
            <div class="row no-gutters">
                <div class="col-md-12">
                    <?php
                    // Include config file
                    require_once "config.php";
                    
                    // Attempt select query execution
                    $sql = "SELECT * FROM markers_1";
                    if($result = mysqli_query($link, $sql)){
                        if(mysqli_num_rows($result) > 0){
                            echo '<table class="table table-bordered table-striped">';
                                echo "<thead>";
                                    echo "<tr>";
                                        echo "<th>#</th>";
                                        echo "<th>Title</th>";
                                        echo "<th>Building</th>";
                                        echo "<th>Room</th>";
                                        echo "<th>Floor</th>";
										echo "<th>Time</th>";
										echo "<th>Date</th>";
										echo "<th>End Date</th>";
										echo "<th>Info</th>";
										echo "<th>Last Updated</th>";
										echo "<th>Is an Event?</th>";
										echo "<th>Is a Repeating Event?</th>";
										echo "<th>Is a Service?</th>";
										echo "<th>Is a Restaurant?</th>";
										echo "<th>Longitude</th>";
										echo "<th>Latitude</th>";
										echo "<th>Action</th>";
                                    echo "</tr>";
                                echo "</thead>";
                                echo "<tbody>";
                                while($row = mysqli_fetch_array($result)){
                                    echo "<tr>";
                                        echo "<td>" . $row['id'] . "</td>";
                                        echo "<td>" . $row['title'] . "</td>";
                                        echo "<td>" . $row['building_location'] . "</td>";
                                        echo "<td>" . $row['room'] . "</td>";
										echo "<td>" . $row['floor'] . "</td>";
										echo "<td>" . $row['time'] . "</td>";
										echo "<td>" . $row['date'] . "</td>";
										echo "<td>" . $row['end_date'] . "</td>";
										echo "<td>" . $row['info'] . "</td>";
										echo "<td>" . $row['last_updated'] . "</td>";
										echo "<td>" . $row['is_event'] . "</td>";
										echo "<td>" . $row['is_repeating_event'] . "</td>";
										echo "<td>" . $row['is_service'] . "</td>";
										echo "<td>" . $row['is_res'] . "</td>";
										echo "<td>" . $row['longitude'] . "</td>";
										echo "<td>" . $row['latitude'] . "</td>";
										echo "<td>";
                                            echo '<a href="markers_update.php?id='. $row['id'] .'" class="mr-3" title="Update Record" data-toggle="tooltip"><span class="fa fa-pencil"></span></a>';
                                            echo '<a href="markers_delete.php?id='. $row['id'] .'" title="Delete Record" data-toggle="tooltip"><span class="fa fa-trash"></span></a>';
                                        echo "</td>";
                                    echo "</tr>";
                                }
                                echo "</tbody>";                            
                            echo "</table>";
                            // Free result set
                            mysqli_free_result($result);
                        } else{
                            echo '<div class="alert alert-danger"><em>No records were found.</em></div>';
                        }
                    } else{
                        echo "Oops! Something went wrong. Please try again later.";
                    }
 
                    // Close connection
                    mysqli_close($link);
                    ?>
                </div>
            </div>        
        </div>
    </div>
</body>
</html>