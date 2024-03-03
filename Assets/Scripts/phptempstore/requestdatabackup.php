<?php

// Database connection
$conn = mysqli_connect('localhost', 'root', 'root', 'unityprojdb');

// Check connection
if(mysqli_connect_errno()) {
    echo "1: Connection failed"; // Connection failed
    exit();
}

// Parameters from Unity
$username = $_POST["username"];

// Check for existing username
$namecheckquery = "SELECT username FROM users WHERE username='" . $username . "';";
$namecheck = mysqli_query($conn, $namecheckquery) or die("2: Name check query failed");

if(mysqli_num_rows($namecheck) != 1) {
    echo "5: No user found or more than one"; // No user found or more than one
    exit();
}

// Fetch user ID
$idfetchquery = "SELECT userid FROM users WHERE username='" . $username . "';";
$useridresult = mysqli_query($conn, $idfetchquery);

if($useridresult) {
    if(mysqli_num_rows($useridresult) > 0) {
        $row = mysqli_fetch_assoc($useridresult);
        $userid = $row["userid"];
    } else {
        echo "5: No user found"; // No user found with the provided username
        exit();
    }
} else {
    echo "7: Database query failed"; // Database query failed
    exit();
}

// Select data from game1_scores
$scorescheckquery_game1 = "SELECT pointsPerSession, sessionQsAnswered, sessionSuccessRate, attempts, timestamp FROM game1_scores WHERE userid='" . $userid . "'";
$scorescheck_game1 = mysqli_query($conn, $scorescheckquery_game1) or die("Error retrieving data from game1_scores");

// Select data from game2_scores
$scorescheckquery_game2 = "SELECT pointsPerSession, sessionQsAnswered, sessionSuccessRate, attempts, timestamp FROM game2_scores WHERE userid='" . $userid . "'";
$scorescheck_game2 = mysqli_query($conn, $scorescheckquery_game2) or die("Error retrieving data from game2_scores");

// Select data from game3_scores which doesn't have anything in it yet
$scorescheckquery_game3 = "SELECT pointsPerSession, sessionQsAnswered, sessionSuccessRate, attempts, timestamp FROM game3_scores WHERE userid='" . $userid . "'";
//$scorescheck_game3 = mysqli_query($conn, $scorescheckquery_game3) or die("Error retrieving data from game3_scores");

// Send data from game1_scores
echo "Game 1 Scores:<br>";
if (mysqli_num_rows($scorescheck_game1) > 0) {
    while ($row = mysqli_fetch_assoc($scorescheck_game1)) {
        echo "Points per Session: " . $row["pointsPerSession"] . ", Session Success Rate: " . $row["sessionSuccessRate"] . ", Session Questions Answered: " . $row["sessionQsAnswered"] . ", Attempts: " . $row["attempts"] . ", Timestamp: " . $row["timestamp"] . "<br>";
    }
} else {
    echo "No scores found for user with ID: $userid in Game 1<br>";
}

// Send data from game2_scores
echo "<br>Game 2 Scores:<br>";
if (mysqli_num_rows($scorescheck_game2) > 0) {
    while ($row = mysqli_fetch_assoc($scorescheck_game2)) {
        echo "Points per Session: " . $row["pointsPerSession"] . ", Session Success Rate: " . $row["sessionSuccessRate"] . ", Session Questions Answered: " . $row["sessionQsAnswered"] . ", Attempts: " . $row["attempts"] . ", Timestamp: " . $row["timestamp"] . "<br>";
    }
} else {
    echo "No scores found for user with ID: $userid in Game 2<br>";
}

?>
