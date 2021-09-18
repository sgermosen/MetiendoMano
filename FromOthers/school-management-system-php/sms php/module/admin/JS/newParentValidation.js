function newParentValidation(){
   var _Id = document.getElementById('id').value;
   var _password = document.getElementById('password').value;
   var _fathername = document.getElementById('fathername').value;
   var _mothername = document.getElementById('mothername').value;
   var _fatherphone = document.getElementById('fatherphone').value;
   var _motherphone = document.getElementById('motherphone').value;
   var _address = document.getElementById('address').value;
   if(!_Id){
       alert('Parent Id Must be fild out.');
       return false;
   }
   else if(!_password){
       alert('Parent Password must be fild out.');
       return false;
   }
   else if(!_fathername){
       alert('Father Name must be fild out.');
       return false;
   }
   else if(!_mothername){
       alert('Mother Name must be fild out.');
       return false;
   }
   else if(!_fatherphone){
       alert('Father Phone must be fild out.');
       return false;
   }
   else if(!_motherphone){
       alert('Mother Phone must be fild out.');
       return false;
   }
   else if(!_address){
       alert('Address must be fild out.');
       return false;
   }
   else return true;
   alert('h');
}
