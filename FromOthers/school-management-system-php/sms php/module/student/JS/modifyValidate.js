function modifyValidate() {
    
	  var pass = document.getElementById('password').value;
	  
	  if(pass == null || pass == ""){
        alert("Password must be filled out");
        return false;
    }
	
	  else{
		    return true;
	}
}
