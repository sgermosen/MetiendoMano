function getParent(str){
    var xhttp;
    if (str == "") {
        document.getElementById("parentList").innerHTML = "";
        return;
    }
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            document.getElementById("parentList").innerHTML = xhttp.responseText;
        }
    };
    xhttp.open("GET", "searchParent.php?key="+str, true);
    xhttp.send();
}
