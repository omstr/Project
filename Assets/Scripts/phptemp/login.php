<?php

    //where to put in the url of database
    $conn = mysqli_connect('localhost', 'root', 'root', 'unityprojdb');

    //check connection
    if(mysqli_connect_errno())
    {
        echo "1: Connection failed"; //conn failed
        exit();
    }

    $username = $_POST["username"];

    //check for existing username by running a sql
                                                            //concatination horribleness
    $namecheckquery = "SELECT username FROM users WHERE username='" . $username . "';";
    $namecheck = mysqli_query($conn, $namecheckquery) or die("2: Name check query failed"); // name check failed
    if(mysqli_num_rows($namecheck) != 1)
    {
        echo "5: No user found or more than one"; //# of names matching != 1
        exit();
    }

    // Fetch the userid associated with the username
    $row = mysqli_fetch_assoc($namecheck);
    $userid = $row['userid'];
    
    //empty 2d array
    $scoresArray = array();

    //getting fields from game1_scores
    $scorescheckquery = "SELECT userid, highestScore, initialScore, attempts, timestamp FROM game1_scores WHERE userid='" . $userid . "';";
    $scorescheck = mysqli_query($conn, $scorescheckquery) or die("3: Scores check query failed"); // Scores check failed

    // Check if any scores were found for the user
    if (mysqli_num_rows($scorescheck) > 0) {
        // Scores found, fetch and process the data
        while ($row = mysqli_fetch_assoc($scorescheck)) {
            $highestScore = $row['highestScore'];
            $initialScore = $row['initialScore'];
            $attempts = $row['attempts'];
            $timestamp = $row['timestamp'];
            $scoresArray[] = $row;
            // Process scores data as needed (e.g., store in variables or echo)
            // echo "Highest Score: $highestScore, Initial Score: $initialScore, Attempts: $attempts, Timestamp: $timestamp";
        }
    } else {
        // No scores found for the user
        echo "6: No scores found for the user\t";
        //exit();
    }
    //get login info from query
    $existingusertableinfo = mysqli_fetch_assoc($namecheck);
    $existinggame1scoreinfo = mysqli_fetch_assoc($scorescheck);
    //echo "0, ";
    echo "0\t" . $existinggame1scoreinfo["highestScore"];


?>