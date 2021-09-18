function getCourseNameAndId(){

    var e = document.getElementById("className");
    var classId = e.options[e.selectedIndex].value;
    //alert(classId);
    var xhttp;
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            document.getElementById("courseName").innerHTML = xhttp.responseText;
        }
    };
    xhttp.open("GET", "searchCourseName.php?key="+classId,true);
    xhttp.send();
}

function setCourseId(){
    var e = document.getElementById("courseName");
    var classId = e.options[e.selectedIndex].value;
    document.getElementById("courseId").value = classId;

    var xhttp;
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            document.getElementById("teacherId").innerHTML = xhttp.responseText;
        }
    };
    xhttp.open("GET", "getTeacherNameid.php?key="+classId,true);
    xhttp.send();
}

function getAllCourseStudentAndSubmit(){
    var e = document.getElementById("courseName");
    var courseId = e.options[e.selectedIndex].value;
    var e = document.getElementById("courseName");
    var courseName = e.options[e.selectedIndex].text;
    var e = document.getElementById("className");
    var classId = e.options[e.selectedIndex].value;
    var e = document.getElementById("teacherId");
    var teacherId = e.options[e.selectedIndex].value;

    alert(courseId+' '+courseName + ' '+classId+' '+teacherId);
    var xhttp;
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            document.getElementById("teacherId").innerHTML = xhttp.responseText;
        }
    };
    xhttp.open("GET", "submitStudentCourse.php?courseid="+courseId+"&classid="+classId+"&teacherid="+teacherId+"&coursename="+courseName,true);
    xhttp.send();
}
