function loginValidate() {
    var id = document.getElementById('myid').value;
	  var pass = document.getElementById('mypassword').value;
	  if ((id == null || id == "") && (pass == null || pass == "")){
        alert("ID and Pasword both must be filled out");
        return false;
    }
    else if (id == null || id == "") {
        alert("ID must be filled out");
        return false;
    }
	  else if(pass == null || pass == ""){
        alert("Password must be filled out");
        return false;
    }
	  else{
		    return true;
	}
}
