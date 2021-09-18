<?php
/**
 * Created by PhpStorm.
 * User: salamaashoush
 * Date: 04/03/17
 * Time: 05:02 م
 */

namespace App\Controllers;

use App\Core\Controller;

class AjaxController extends Controller
{
    public function image_load()
    {
        try {
            $response = \FroalaEditor_Image::getList('/uploads/');
            echo stripslashes(json_encode($response));
        } catch (Exception $e) {
            http_response_code(404);
        }
    }
    public function image_upload()
    {
        $options = array(
            'resize' => array(
                // Width.
                'columns' => 300,

                // Height.
                'rows' => 300,

                // Keep aspect ratio.
                'bestfit' => true
            )
        );
        try {
            $response = \FroalaEditor_Image::upload('/uploads/');
            echo stripslashes(json_encode($response));
        } catch (Exception $e) {
            http_response_code(404);
        }
    }

    public function delete_image()
    {
        try {
            $response = \FroalaEditor_Image::delete($_POST['src']);
            echo stripslashes(json_encode('Success'));
        } catch (Exception $e) {
            http_response_code(404);
        }

    }

    public function file_upload()
    {

        try {
            $response = \FroalaEditor_File::upload('/uploads/');
            echo stripslashes(json_encode($response));
        } catch (Exception $e) {
            http_response_code(404);
        }
    }

    public function delete_file()
    {
        try {
            $response = \FroalaEditor_File::delete('/uploads/');
            echo stripslashes(json_encode($response));
        } catch (Exception $e) {
            http_response_code(404);
        }
    }

}