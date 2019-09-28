

$(document).ready(function() {
// $('.navbar-toggler').click(function() {
//     $('.navbar-collapse').slideToggle();
//    });


 
  $("#owl-demo").owlCarousel({
 	  navigation : true, // Show next and prev buttons
      slideSpeed : 300,
      paginationSpeed : 400,
      singleItem:true,
      items : 1,
      autoPlay: 3000,
       loop: true,
  		
 
  });
  $("#testimonal").owlCarousel({ 
  		 navigation : true, // Show next and prev buttons
      slideSpeed : 300,
      paginationSpeed : 400,
      singleItem:true,
      items : 1,
      autoPlay: 3000,
       loop: true,

  });

    //Return book
    const ChangeColor = function () {
        $("#returnBook")
            .css('background-color', 'green');
    }

    setTimeout(ChangeColor, 1500);

 
});



