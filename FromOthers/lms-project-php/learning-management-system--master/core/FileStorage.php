<?php
/**
 * Created by PhpStorm.
 * User: salamaashoush
 * Date: 22/02/17
 * Time: 09:39 ص
 */

namespace App\Core;


class FileStorage
{
    /**
     * Use to store fOpen connection
     * @type bool
     * @access private
     */
    private $handle;

    /**
     * To store the file URL/location
     * @type string
     * @access private
     */
    private $file;

    /**
     * Used to initialize the file
     * @access public
     * @param string $file_url
     * File location/url
     * @example 'dir/mytext.txt'
     * @return bool
     */
    public function load($file_url)
    {
        $this->file = $file_url;
        if ($this->handle = fopen($file_url, 'c+'))
        {
            return $this;
        }
    }

    /**
     * Used to write inside the file,
     * If file doesn't exists it will create it
     * @access public
     * @param string $text
     * The text which we have to write
     * @example 'My text here in the file.';
     * @return bool
     */
    public function write($text)
    {
        if (fwrite($this->handle, $text))
        {
            fclose($this->handle);
            return true;
        }
        else
        {
            fclose($this->handle);
            return false;
        }
    }

    /**
     * To read the contents of the file
     * @access public
     * @param bool $nl2br
     * By default set to false, if set to true will return
     * the contents of the file by preserving the data.
     * @example (true)
     * @return string|bool
     */
    public function read($nl2br = false)
    {
        if ($read = fread($this->handle, filesize($this->file)))
        {
            if ($nl2br == true)
            {
                fclose($this->handle);
                return nl2br($read);
            }

            fclose($this->handle);
            return $read;
        }
        else
        {
            fclose($this->handle);
            return false;
        }
    }

    /**
     * Use to delete the file
     * @access public
     * @return bool
     */
    public function delete()
    {
        fclose($this->handle);

        if (file_exists($this->file))
        {
            if (unlink($this->file))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}