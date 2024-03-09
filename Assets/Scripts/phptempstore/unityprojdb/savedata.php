<?php

     //where to put in the url of database
     
     $conn = mysqli_connect('localhost', 'root', 'root', 'unityprojdb');
     
     //check connection
     if(mysqli_connect_errno())
     {
         echo "1: Connection failed"; //conn failed
         exit();
     }
     
    //Params from Unity
    //$userid = $_POST["userid"];
    $username = $_POST["username"];
    $pointsPerSession = $_POST["pointsPerSession"];
    $sessionSuccessRate = $_POST["sessionSuccessRate"];
    $sessionQsAnswered = $_POST["sessionQsAnswered"];
    $attempts = $_POST["attempts"];
    $timestamp = $_POST["timestamp"];

    $game = $_POST["game"];
    
    
    //check for existing username by running a sql
                                                            //concatination horribleness
    $namecheckquery = "SELECT username FROM users WHERE username='" . $username . "';";
    $namecheck = mysqli_query($conn, $namecheckquery) or die("2: Name check query failed"); // name check failed
    if(mysqli_num_rows($namecheck) != 1)
    {
        echo "5: No user found or more than one"; //# of names matching != 1
        exit();
    }

    $idfetchquery = "SELECT userid FROM users WHERE username='" . $username . "';";
    $useridresult = mysqli_query($conn, $idfetchquery);

    //echo "reached userid";
    if($useridresult){
        if(mysqli_num_rows($useridresult) > 0){
            $row = mysqli_fetch_assoc($useridresult);
            $userid = $row["userid"];
        }else {
            echo "5: No user found"; // No user found with the provided username
            exit();
        }
    } else {
        echo "7: Database query failed"; // Database query failed
        exit();
    }

    //echo "reached insert query";
    // set this up to work with the different tables etc 
    //$updatequery = "UPDATE game1_scores SET initialScore = " . $initialScore . "WHERE userid = '" . $userid . "';";
    //$insertquery = "INSERT INTO game1_scores (userid, initialScore) VALUES ('" . $userid . "', " . $initialScore . ");";

    //determine the table based on the game or scene
    $tableName = "";
    switch($game) {
        case "game1":
            $tableName = "game1_scores";
            break;
        case "game2":
            $tableName = "game2_scores";
            break;
        case "game3":
            $tableName = "game3_scores";
            break;
        case "game4":
            $tableName = "game4_scores";
            break;
        //add cases for other games as they come
        default:
            echo "Invalid game"; //invalid game or scene
            exit();
    }

    $insertquery = "INSERT INTO " . $tableName . " (userid, pointsPerSession, sessionSuccessRate, sessionQsAnswered, attempts, timestamp) VALUES ('" . $userid . "', " . $pointsPerSession . ", " . $sessionSuccessRate . ", " . $sessionQsAnswered . ", " . $attempts . ", '" . $timestamp . "');";

    mysqli_query($conn, $insertquery) or die("8: saving query failed"); // updating table failed
    
    echo "0";

?>