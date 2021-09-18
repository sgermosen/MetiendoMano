function getClassNameAndId(){
    var xhttp;
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            document.getElementById("className").innerHTML = xhttp.responseText;
        }
    };
    xhttp.open("GET", "searchClassName.php?",true);
    xhttp.send();
}
