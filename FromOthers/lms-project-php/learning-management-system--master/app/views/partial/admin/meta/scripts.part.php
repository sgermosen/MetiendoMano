 <script>
    $(function () {
                $('#metatable').Tabledit({
                    url: 'example.php',
                    columns: {
                        identifier: [0, 'id'],
                        editable: [[1, 'key'], [2, 'value']]
                    }
                });
                $('#addnew').on('click',function(e){
                    $('#example1 tbody').append('s');

                });

            });
 </script>
 <script src="<?php asset('plugins/jquery.tabledit.js')?>"></script>
