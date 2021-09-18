function getTeacherForUpdateSalary(str){
    var xhttp;
    if (str == "") {
        document.getElementById("updateTeacherSalary").innerHTML = "";
        return;
    }
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            document.getElementById("updateTeacherSalary").innerHTML = xhttp.responseText;
        }
    };
    xhttp.open("GET", "searchForUpdateTeacherSalary.php?key="+str, true);
    xhttp.send();
}
