$(function() {
    var availableTags = [
      "School of Business and Law", 
      "School of Environmental Design and Architecture", 
      "School of Liberal Studies and Education"
    ];
    
    $(".autocomplete").autocomplete({ 
      source: availableTags
    });
  });
  
  $(function(){
      $('#groups').on('change', function(){
          var val = $(this).val();
          var sub = $('#sub_groups');
          $('option', sub).filter(function(){
              if (
                   $(this).attr('data-group') === val 
                || $(this).attr('data-group') === 'SHOW'
              ) {
                  $(this).show();
              } else {
                  $(this).hide();
              }
          });
      });
      $('#groups').trigger('change');
  });