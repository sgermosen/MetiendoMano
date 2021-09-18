function getParentForUpdate(str){
    var xhttp;
    if (str == "") {
        document.getElementById("updateParentData").innerHTML = "";
        return;
    }
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            document.getElementById("updateParentData").innerHTML = xhttp.responseText;
        }
    };
    xhttp.open("GET", "searchForUpdateParent.php?key="+str, true);
    xhttp.send();
}
