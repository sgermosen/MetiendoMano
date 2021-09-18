function getCourse(str){
    var xhttp;
    if (str == "") {
        document.getElementById("courseList").innerHTML = "";
        return;
    }
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            document.getElementById("courseList").innerHTML = xhttp.responseText;
        }
    };
    xhttp.open("GET", "searchCourse.php?key="+str, true);
    xhttp.send();
}
