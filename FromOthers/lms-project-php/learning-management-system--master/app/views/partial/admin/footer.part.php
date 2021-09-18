
</div>
<!-- /.content-wrapper -->

<!-- Main Footer -->
<footer class="main-footer">
    <!-- To the right -->
    <div class="pull-right hidden-xs">
        Open Sorce LMS
    </div>
    <!-- Default to the left -->
    <strong>Copyright &copy; 2017 <a href="#">OSLMS</a>.</strong> All rights reserved.
</footer>
<!-- Control Sidebar -->
</div>
<!-- ./wrapper -->

<!-- REQUIRED JS SCRIPTS -->
<?php resource('js','jquery-3.1.1.min')?>
<!-- Bootstrap 3.3.5 -->
<?php resource('js','bootstrap.min')?>
<!-- AdminLTE App -->
<?php resource('js','app.min')?>
<?php resource('js','plugins/slimScroll/jquery.slimscroll.min')?>
<?php resource('js','plugins/fastclick/fastclick.min')?>
<?php resource('js','plugins/datatables/jquery.dataTables.min')?>
<?php resource('js','plugins/datatables/dataTables.bootstrap.min')?>
<!-- Optionally, you can add Slimscroll and FastClick plugins.
     Both of these plugins are recommended to enhance the
     user experience. Slimscroll is required when using the
     fixed layout. -->
<?php partial('admin/froala/scripts')?>
<script src="<?php asset('js/plugins/iCheck/icheck.min.js')?>"></script>
<script>
    $(function () {
        $('input').iCheck({
            checkboxClass: 'icheckbox_square-blue',
            radioClass: 'iradio_square-blue',
            increaseArea: '20%' // optional
        });
    });
</script>
</body>
</html>