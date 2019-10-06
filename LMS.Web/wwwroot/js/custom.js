

$(document).ready(function() {
// $('.navbar-toggler').click(function() {
//     $('.navbar-collapse').slideToggle();
//    });


 // za abauta


 
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
    //za tursachka

    //!!! ZA FLASH MSG!! --> Toastr --> trqbva ni css na CDNJS.com , kopira se css-a v nashiq layout se paste-va. Posle se slaga kopiraniq JS (stava duma samo za link)
    $("#search-button").click(function () {
        const searchText = $('#search-text').val();
        console.log(searchText);
        $.get('/home/team?name=' + searchText)
    //shte mi trqbva moi action sus parameter [FromQuery]string name 
        //i da vrushta Json (sus filtriranite danni , koito puk gi filtriram v services)
    })
    $('#search-text').on('keyup', function () {
        console.log
    })

    //Return book
    const ChangeColor = function () {
        $("#returnBook")
            .css('background-color', 'green');
    }

    setTimeout(ChangeColor, 1500);

 
});



