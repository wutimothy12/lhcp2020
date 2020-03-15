 $(function() {
    $(".toggle").on("click", function() {
        if ($(".item").hasClass("active")) {
            $(".item").removeClass("active");
            $(this).find("a").html("<i class='fa fa-bars'></i> Menu");
        } else {
            $(".item").addClass("active");
            $(this).find("a").html("<i class='fa fa-times'></i> Menu");
        }
    });
});

$(document).ready(function(){
    $(".buttonOver a").click(function(){
        $(".overlay").fadeToggle(200);
       $(this).toggleClass('btn-open').toggleClass('btn-close');
	   $("ul.menu li a").toggle();
    });

$('.overlay').on('click', function(){
    $(".overlay").fadeToggle(200);   
    $(".buttonOver a").toggleClass('btn-open').toggleClass('btn-close');
	$("ul.menu li a").show();
	 open = false;
	  
});
});