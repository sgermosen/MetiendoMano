function getStaffForUpdate(str){
    var xhttp;
    if (str == "") {
        document.getElementById("updateStaffData").innerHTML = "";
        return;
    }
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            document.getElementById("updateStaffData").innerHTML = xhttp.responseText;
        }
    };
    xhttp.open("GET", "searchForUpdateStaff.php?key="+str, true);
    xhttp.send();
}
