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

    if(mysqli_num_rows($namecheck)>0)
    {
        echo "3: username already exists"; // username exists
        exit();
    }

    //add user to table
    $insertuserquery = "INSERT INTO users (username) VALUES ('" . $username . "');";
    mysqli_query($conn, $insertuserquery) or die("4: Insert user query failed"); // user query failed 

    echo ("0: Successful Registration"); //successful reg


?>
