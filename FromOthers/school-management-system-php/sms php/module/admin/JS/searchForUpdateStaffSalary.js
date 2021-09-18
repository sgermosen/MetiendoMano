function getStaffForUpdateSalary(str){
    var xhttp;
    if (str == "") {
        document.getElementById("updateStaffSalary").innerHTML = "";
        return;
    }
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            document.getElementById("updateStaffSalary").innerHTML = xhttp.responseText;
        }
    };
    xhttp.open("GET", "searchForUpdateStaffSalary.php?key="+str, true);
    xhttp.send();
}
