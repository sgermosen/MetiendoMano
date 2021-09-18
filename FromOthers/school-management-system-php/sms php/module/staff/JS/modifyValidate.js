function modifyValidate() {
    var phone = document.getElementById('phone').value;
    var email = document.getElementById('email').value;
	  var pass = document.getElementById('password').value;
	  var address = document.getElementById('address').value;
	  
	  if (phone == null || phone == "") {
        alert("Phone number must be filled out");
        return false;
    }
	  else if(email == null || email == ""){
        alert("Email address must be filled out");
        return false;
    }
	else if(pass == null || pass == ""){
        alert("Password must be filled out");
        return false;
    }
	else if(address == null || address == ""){
        alert("address must be filled out");
        return false;
    }
	
	
	  else{
		    return true;
	}
}
