function getTeacherForUpdate(str){
    var xhttp;
    if (str == "") {
        document.getElementById("updateTeacherData").innerHTML = "";
        return;
    }
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            document.getElementById("updateTeacherData").innerHTML = xhttp.responseText;
        }
    };
    xhttp.open("GET", "searchForUpdateTeacher.php?key="+str, true);
    xhttp.send();
}
