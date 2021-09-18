var teaGender = '';
function newTeacherValidation(){
   var teaId = document.getElementById('teaId').value;
   var teaName = document.getElementById('teaName').value;
   var teaPassword = document.getElementById('teaPassword').value;
   var teaPhone = document.getElementById('teaPhone').value;
   var teaEmail = document.getElementById('teaEmail').value;
   var teaDOB = document.getElementById('teaDOB').value;
   var teaHireDate = document.getElementById('teaHireDate').value;
   var teaSalary = document.getElementById('teaSalary').value;
   if(!teaId){
       alert('Teacher Id Must be fild out.')
       return false;
   }
   else if(!teaName){
       alert('Teacher Name must be fild out.')
       return false;
   }
   else if(!teaPassword){
       alert('Teacher Password must be fild out.')
       return false;
   }
   else if(!teaPhone){
       alert('Teacher Phone must be fild out.')
       return false;
   }
   else if(!teaEmail){
       alert('Teacher Email must be fild out.')
       return false;
   }
   else if(!teaGender){
       alert('Teacher Gender must be fild out.')
       return false;
   }
   else if(!teaDOB){
       alert('Teacher Date Of Birth must be fild out.')
       return false;
   }
   else if(!teaParentId){
       alert('Teacher Salary must be fild out.')
       return false;
   }
   else return true;
}
