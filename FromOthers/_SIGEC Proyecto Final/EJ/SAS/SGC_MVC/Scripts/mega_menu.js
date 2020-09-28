$(document).ready(function () {
    $(".hide").hide();
    $(".btnMenu").click(function () {
        var me = $(this);
        if (!me.hasClass('activeMenu')) {
            $(".activeMenu").removeClass('activeMenu');
            me.addClass("activeMenu");
            $(".hide").hide("medium");
            $("#" + "menu-" + me.attr("id").replace("btn-", "")).toggle("medium");
        }
        else {
            $(".btnMenu.activeMenu").removeClass('activeMenu');
            $(".hide").hide("medium");
        }
        return false;
    });
});