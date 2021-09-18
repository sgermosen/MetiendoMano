function getPayment(str){
    var xhttp;
    if (str == "") {
        document.getElementById("paymentList").innerHTML = "";
        return;
    }
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            document.getElementById("paymentList").innerHTML = xhttp.responseText;
        }
    };
    xhttp.open("GET", "searchPayment.php?key="+str, true);
    xhttp.send();
}
