$(document).ready( function() {
    $('nav.menu a').click( function() {
      $(this).parent().find('.current').removeClass('current');
      $(this).addClass('current');
    });
    $(window).scroll(function(){
      var w = $(window).width();
      if(w < 768) {
        $('#top-button').hide();
      } else {
        var e = $(window).scrollTop();
        e>150?$('#top-button').fadeIn() :$('#top-button').fadeOut();   
      }
    });
  });