<?php
function public_dir(){
    return "/public/";
}
function views_dir(){
    return "/app/views/";
}
function view($view, $data = [])
{
    extract($data);
    return require "app/views/{$view}.view.php";
}
function partial($part, $data = [])
{
    extract($data);
    return require "app/views/partial/{$part}.part.php";
}
function redirect($url, $data=[], $statusCode = 303)
{
    \App\Core\Session::set('response' , $data);
    header('Location: ' . $url, true, $statusCode);
    die();
}
function asset($path)
{
    echo public_dir()."$path";
}
function resource($type, $name)
{
    if ($type == 'css') {
        echo '<link rel="stylesheet" href="' . public_dir()."${type}/${name}.${type}" . '" >';
    } else if ($type == 'js') {
        echo '<script type="text/javascript" src="' . public_dir()."${type}/${name}.${type}" . '" ></script>';
    } else {
        return public_dir()."${type}/${name}.${type}";
    }
    return public_dir()."${type}/${name}.${type}";
}
function html_image($name,$option=[]){
    $options='';
    if(!empty($option)){
        $options=parseOpt($option);
    }
    echo '<img src="' . public_dir()."img/${name}" . '" '.$options.'">';
}
function uploaded_image($name,$option=[]){
    $options='';
    if(!empty($option)){
        $options=parseOpt($option);
    }
    echo '<img src="' . "/uploads/${name}" . '" '.$options.'">';
}
function html_link($url,$text,$option=[]){
    $options='';
    if(!empty($option)){
        $options=parseOpt($option);
    }
    echo '<a href="' .$url. '" '.$options.'">'.$text."</a>";
}
function method_field($method)
{
    echo '<input type="hidden" name="_method" value=' . $method . ' />';
}

function start_form($method, $action, $option = [])
{
    $options='';
    if(!empty($option)){
        $options=parseOpt($option);
    }

    if ($method === 'post' || $method === 'get') {
        echo '<form action="' . $action . '" method="' . $method . '"   enctype="multipart/form-data" >';

        csrf_field();
    } else {
        echo '<form action="' . $action . '" method="post" '.$options.'">';
        method_field($method);
        csrf_field();
    }
}
function close_form()
{
    echo '</form>';
}
function generateCSRF()
{
    if (empty($_SESSION['token'])) {
        $_SESSION['token'] = bin2hex(random_bytes(32));
    }
    return $_SESSION['token'];
}
function verifyCSRF($request)
{
    if ($request->getCSRF()) {
        if (hash_equals($_SESSION['token'], $request->getCSRF())) {
            return true;
        } else {
            return false;
        }
    }
}
function csrf_field()
{
    echo '<input type="hidden" name="_token" value=' . generateCSRF() . ' />';
}
function getErrors()
{
    $errors = false;
    if (isset($_SESSION['response'])) {
        $response = $_SESSION['response'];
        unset($_SESSION['response']);
        if (isset($response['errors'])) {
            $errors = $response['errors'];
        }
    }
    return $errors;
}
function getResponse()
{
    $response = false;
    if (isset($_SESSION['response'])) {
        $response = $_SESSION['response'];
    }
    return $response;
}
function upload_file($fieldname)
{
    try {
        $response = \FroalaEditor\Utils\DiskManagement::upload('/uploads/',['fieldname' => "$fieldname"]);
    } catch (Exception $e) {
        var_dump($e->getMessage());
    }
    return $response->link;
}
function delete_file($file)
{
    try {
        $response = \FroalaEditor\Utils\DiskManagement::delete($file);
    } catch (Exception $e) {
        var_dump($e->getMessage());
    }
}
function toJson($data)
{
    header('Access-Control-Allow-Origin: null');
    header('Content-Type: application/json');
    echo utf8_encode(json_encode($data));
}
function session_id_field(){
    echo '<input type = "hidden" name="PHPSESSID" value="'.session_id().'"/>';
 }

function parseOpt($array){
    $r =[];
    foreach ($array as $key=>$value)
        $r[]=$key."=".'"'.$value.'"';
    return implode(" ",$r);
}

function dispalyForDebug($data)
{
    if (gettype($data) == "array")
    {
        echo "<pre>".print_r($data,true)."</pre>";
    }
    else
    {
        echo "<pre>";
        var_dump($data);
        echo "</pre>";
    }
}


function uploadFile($file_name,$path,$changefilename,$plusname,$type)
{
    $filename = $_FILES[$file_name]['name'];
    $filetype = explode(".",$filename);
    $filesize = $_FILES[$file_name]['size'];
    $filetmp  = $_FILES[$file_name]['tmp_name'];
    $type     = explode("|",$type);
    if (count($filetype)>2)
    {
        $_SESSION['error']= " $filename تنبية أمنى : برجأ التأكد من أمتداد الملف المرفوع وازالة ال (.) الزائدة فى ملف ";
    }
    else
    {

        if ($changefilename=="")
        {
            $changename =$plusname.$filename;
        }
        else
        {
            $changename = $changefilename.'_'.$plusname.$filename;
        }

        $pathphender = $path.$changename;
        if (in_array($filetype[1],$type))
        {

                if (copy($filetmp,$pathphender))
                {
                    $upload_status['name'] =$changename;
                    $upload_status['url']  =$pathphender;
                    $upload_status['size'] =$filesize;
                    $upload_status['type'] =$filetype;
                    $upload_status['state']="true";
                }
                else
                {
                    echo $_SERVER["DOCUMENT_ROOT"];
                    $upload_status['name']=$changename;
                    $upload_status['url'] =$pathphender;
                    $upload_status['size']=$filesize;
                    $upload_status['type']=$filetype;
                    $upload_status['state']="false";
                    $_SESSION['error']    = $upload_status['name']."لم يتم رفع الملف بنجاح برجاء معاودة المحاولة";
                }
                return $upload_status;
        }
        else
        {
            $_SESSION['error']="تحميل ملف $filename ($filetype[1])لم يكتمل امتداد هذا الملف غير مسموح بية امتداد هذا الملف هو  ";
        }
    }
}

function getImageTypes(){
    return "gif|Gif|ico|ICO|jpg|JPG|jpeg|JPEG|BNG|png|PNG";
}

function sendMail($userEmail,$userName,$subject,$htmlBody)
{
    $mail = new PHPMailer;
    $mail->SMTPDebug = 0;                               // Enable verbose debug output
    $mail->isSMTP();                                      // Set mailer to use SMTP
    $mail->Host = 'gator4235.hostgator.com';  // Specify main and backup SMTP servers
    $mail->SMTPAuth = true;                               // Enable SMTP authentication
    $mail->Username = 'habib@itqanapp.com';                 // SMTP username
    $mail->Password = '123456789';                           // SMTP password
    $mail->SMTPSecure = 'ssl';                            // Enable TLS encryption, `ssl` also accepted
    $mail->Port = 465;                                    // TCP port to connect to

    $mail->setFrom('habib@itqanapp.com', 'LMS-OS-37');
    $mail->addAddress($userEmail, $userName);     // Add a recipient
    $mail->isHTML(true);

    $mail->Subject = $subject;
    $mail->Body    = $htmlBody;
    if(!$mail->send()) {
        echo 'Message could not be sent.';
        echo 'Mailer Error: ' . $mail->ErrorInfo;
    } else {
        echo 'Message has been sent';
    }
}

function getFacebookObj()
{
    $fb = new \Facebook\Facebook([
        'app_id' => '385244185170219',
        'app_secret' => '44ee21e007fdf0c42a7a2e6946488a17',
        'default_graph_version' => 'v2.8',
    ]);

    return $fb;
}

function getGmailObj()
{

    $client_id = '60269544916-gvohmgl6dcudacgjevh1vdffhja86usi.apps.googleusercontent.com';
    $client_secret = '5aogBhoJNqOwU_1kyuNyHYlt';
    $redirect_uri = 'https://opensourcelms.herokuapp.com/gmlogin?';
    $client = new Google_Client();
    $client->setClientId($client_id);
    $client->setClientSecret($client_secret);
    $client->setRedirectUri($redirect_uri);
    $client->addScope("email");
    $client->addScope("profile");
    return $client;
}

