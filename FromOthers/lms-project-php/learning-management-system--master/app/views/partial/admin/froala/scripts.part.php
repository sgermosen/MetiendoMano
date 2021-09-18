<!-- Include Code Mirror. -->
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.3.0/codemirror.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/codemirror/5.3.0/mode/xml/xml.min.js"></script>

<!-- Include Plugins. -->

<?php resource('js','froala_editor.min')?>
<?php resource('js','plugins/align.min')?>
<?php resource('js','plugins/char_counter.min')?>
<?php resource('js','plugins/code_beautifier.min')?>
<?php resource('js','plugins/code_view.min')?>
<?php resource('js','plugins/colors.min')?>
<?php resource('js','plugins/emoticons.min')?>
<?php resource('js','plugins/entities.min')?>
<?php resource('js','plugins/file.min')?>
<?php resource('js','plugins/font_family.min')?>
<?php resource('js','plugins/font_size.min')?>
<?php resource('js','plugins/fullscreen.min')?>
<?php resource('js','plugins/image.min')?>
<?php resource('js','plugins/image_manager.min')?>
<?php resource('js','plugins/inline_style.min')?>
<?php resource('js','plugins/line_breaker.min')?>
<?php resource('js','plugins/link.min')?>
<?php resource('js','plugins/lists.min')?>
<?php resource('js','plugins/paragraph_format.min')?>
<?php resource('js','plugins/paragraph_style.min')?>
<?php resource('js','plugins/quick_insert.min')?>
<?php resource('js','plugins/quote.min')?>
<?php resource('js','plugins/table.min')?>
<?php resource('js','plugins/save.min')?>
<?php resource('js','plugins/url.min')?>
<?php resource('js','plugins/video.min')?>


    <script>
        $(function () {
            $('textarea#editor').froalaEditor({
                // Set the image upload parameter.
//                imageUploadParam: 'image',

                // Set the image upload URL.
                imageUploadURL: '/image_upload',
                fileUploadURL: '/file_upload',

                // Additional upload params.
//                imageUploadParams: {id: 'my_editor'},
////
////                // Set request type.
//                imageUploadMethod: 'POST',
//
//                // Set max image size to 5MB.
//                imageMaxSize: 5 * 1024 * 1024,
//
//                // Allow to upload PNG and JPG.
//                imageAllowedTypes: ['jpeg', 'jpg', 'png'],
//
//                // Set page size.
//                imageManagerPageSize: 20,

//                // Set a scroll offset (value in pixels).
//                imageManagerScrollOffset: 10,

                // Set the load images request URL.
                imageManagerLoadURL: "/image_load",

                // Set the load images request type.
//                imageManagerLoadMethod: "GET",


                // Set the delete image request URL.
                imageManagerDeleteURL: "/delete_image",

//                // Set the delete image request type.
//                imageManagerDeleteMethod: "POST",

            });
            // Catch the file being removed.
            $('.selector').on('froalaEditor.file.removed', function (e, editor, $img) {
                $.ajax({
                    // Request method.
                    method: 'POST',

                    // Request URL.
                    url: '/delete_file',

                    // Request params.
                    data: {
                        src: $img.attr('src')
                    }
                })
                    .done (function (data) {
                        console.log ('File was deleted');
                    })
                    .fail (function (err) {
                        console.log ('File delete problem: ' + JSON.stringify(err));
                    })
            });
            $("#indextable").DataTable();
        });
    </script>