//==========================================================================================================================

/* Morfing Search  - Function =======================================================================================================
*  You can manage the Search
*  
*/
$(function () {
    $('.control').click(function () {
        $('body').addClass('mode-search');
        $('.input-search').focus();
    });
    $('.icon-close').click(function () {
        $('body').removeClass('mode-search');
    });
});

// Morph Search =======================================================================
$(function() {
    var morphSearch = document.getElementById( 'morphsearch' ),
        input = morphSearch.querySelector( 'input.morphsearch-input' ),
        ctrlClose = morphSearch.querySelector( 'span.morphsearch-close' ),
        isOpen = isAnimating = false,
        // show/hide search area
        toggleSearch = function(evt) {
            // return if open and the input gets focused
            if( evt.type.toLowerCase() === 'focus' && isOpen ) return false;

            var offsets = morphsearch.getBoundingClientRect();
            if( isOpen ) {
                classie.remove( morphSearch, 'open' );

                // trick to hide input text once the search overlay closes 
                // todo: hardcoded times, should be done after transition ends
                if( input.value !== '' ) {
                    setTimeout(function() {
                        classie.add( morphSearch, 'hideInput' );
                        setTimeout(function() {
                            classie.remove( morphSearch, 'hideInput' );
                            input.value = '';
                        }, 300 );
                    }, 500);
                }
                
                input.blur();
            }
            else {
                classie.add( morphSearch, 'open' );
            }
            isOpen = !isOpen;
        };

    // events
    input.addEventListener( 'focus', toggleSearch );
    ctrlClose.addEventListener( 'click', toggleSearch );
    // esc key closes search overlay
    // keyboard navigation events
    document.addEventListener( 'keydown', function( ev ) {
        var keyCode = ev.keyCode || ev.which;
        if( keyCode === 27 && isOpen ) {
            toggleSearch(ev);
        }
    } );


    /***** for demo purposes only: don't allow to submit the form *****/
    morphSearch.querySelector( 'button[type="submit"]' ).addEventListener( 'click', function(ev) { ev.preventDefault(); } );
});
// End Morph Search =======================================================================
