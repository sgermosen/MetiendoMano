function getStaff(str){
    var xhttp;
    if (str == "") {
        document.getElementById("staffList").innerHTML = "";
        return;
    }
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            document.getElementById("staffList").innerHTML = xhttp.responseText;
        }
    };
    xhttp.open("GET", "searchStaff.php?key="+str, true);
    xhttp.send();
}
