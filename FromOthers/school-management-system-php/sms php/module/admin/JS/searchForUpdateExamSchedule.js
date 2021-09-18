function getExamScheduleForUpdate(str){
    var xhttp;
    if (str == "") {
        document.getElementById("updateExamSchedule").innerHTML = "";
        return;
    }
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            document.getElementById("updateExamSchedule").innerHTML = xhttp.responseText;
        }
    };
    xhttp.open("GET", "searchForUpdateExamSchedule.php?key="+str, true);
    xhttp.send();
}
