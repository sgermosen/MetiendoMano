$(document).ready(function () {
    var number = parseInt($('#test').text(), 10) || 0; // Get the number from paragraph

    // Called the function in each second
    var interval = setInterval(function () {
        $('#test').text(number++); // Update the value in paragraph
        //document.getElementById('percent-box').value=number;

        if (number > 1000) {
            clearInterval(interval); // If exceeded 100, clear interval
        }
    }, 1000); // Run for each second
});
var colorInc = 100 / 3;

$(function()
{
  $("#percent-box").click(function()
  {
    $(this).select();
  });
  
  $("#percent-box").keyup(function()
  {
    var val = $(this).val();
    
    if(val != ""
      && !isNaN(val)
      && val <= 100
      && val >= 0)
    {
      console.log(val);
      
      var valOrig = val;
      val = 100 - val;
      
      if(valOrig == 0)
      {
        $("#percent-box").val(0);
        $(".progress .percent").text(0 + "%");
      }
      else $(".progress .percent").text(valOrig + "%");
      
      $(".progress").parent().removeClass();
      $(".progress .water").css("top", val + "%");
      
      if(valOrig < colorInc * 1)
        $(".progress").parent().addClass("red");
      else if(valOrig < colorInc * 2)
        $(".progress").parent().addClass("orange");
      else
        $(".progress").parent().addClass("green");
    }
    else
    {
      $(".progress").parent().removeClass();
      $(".progress").parent().addClass("green");
      $(".progress .water").css("top", 100 - 67 + "%");
      $(".progress .percent").text(67 + "%");
      $("#percent-box").val("");
    }
  });
});