<title>Admin Area | Login</title>
<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="css/login.css">
<div class="modal-dialog login-center"> 
  <div class="loginmodal-container">
    <h1> Admin Login </h1>
    <br>
    <form method="post" action="adminLoginHandler.php">
      <input type="email" name="email" placeholder="Email" required>
      <input type="password" name="pass" placeholder="Password" required>
      <input type="submit" name="login" class="login loginmodal-submit navbar-inverse" value="Login">
    </form>
    <div class="login-help"> 
    </div>
  </div>
</div>