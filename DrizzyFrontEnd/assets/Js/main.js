$(function () {  
    //NAVBAR SCROLL
    $(document).scroll(function () {
        if ($(window).width() > 992) 
        {
            if (window.scrollY > 36) 
            {
                $("#navbar").addClass("scrollactive");
            }
            else
            {
                $("#navbar").removeClass("scrollactive");
            }
        }
        else{
            $("#navbar").addClass("scrollactive"); 
        }
    });
    //BRAND SLIDER
    $('.brand-active-carousel').slick({
		arrows: false,
		slidesToShow: 4,
        autoplay: true,
        autoplaySpeed: 1000,
		responsive: [
			{
				breakpoint: 992,
				settings: {
					slidesToShow: 2
				}
			},
			{
				breakpoint: 480,
				settings: {
					slidesToShow: 1
				}
			}
		]
	});
    //CATEGORIES MOBILE ACCORDION
    $(".categoriesmobile").click(function(){
       if($(window).width() <= 480){
        if($(this).next().css("display")=="none"){
            $(this).next().css("display","block")
            $(this).prev().find("i").addClass("fa-chevron-up")
            $(this).prev().find("i").removeClass("fa-chevron-down")         
            return;
        }
        else{
            $(this).next().css("display","none")
            $(this).prev().find("i").addClass("fa-chevron-down")
            $(this).prev().find("i").removeClass("fa-chevron-up")  
            return;
        }
       }
    });
    //MOBILE NAVBAR HAMBURGER CLICK
    $(".hamburger-mobile").click(function(){
        if($(this).children().attr("class")=="fas fa-bars"){
           $(this).children().removeClass("fa-bars");
           $(this).children().addClass("fa-times");
           $(".navbar-mobile-dropdown").css("display","block");
           return;
        }
        else{
            $(this).children().removeClass("fa-times");
            $(this).children().addClass("fa-bars");
            $(".navbar-mobile-dropdown").css("display","none");
            return;
        }
        // console.log($(this).children().attr("class"));
    })
    // FILTER SIDEBAR CLICK
    $(".more-filter").click(function(){
        if($(".filter-body").css("visibility")=="hidden"){
            $(".filter-sidebar").css("transform","translate(0px, 0px)");
            $(".filter-body").css("visibility","visible");
            return;
        }
    });
    $(".filter-btn-mobile").click(function(){
        if($(".filter-body").css("visibility")=="hidden"){
            $(".filter-sidebar").css("transform","translate(0px, 0px)");
            $(".filter-body").css("visibility","visible");
            return;
        }
    });
    $(".exit-filter-sidebar").click(function(){
        $(".filter-sidebar").css("transform","translate(320px, 0px)");
        $(".filter-body").css("visibility","hidden");
        return;
    });
    //MAKE INPUT CHECKED ON CLICK OF a TAG
    $(".filter-link").click(function(){
        $(this).find("input").attr('checked','true');
    });
    //FILTER MOBILE ITEMS CLICK
    $(".filter-list-button").click(function(){
        let itemcount = $(this).next().find(".filter-link").length;
        let dropdownheight = itemcount * 38;
        if(itemcount<=4){
            $(this).next().find(".show-more").css("display","none");
            if($(this).find(".filter-plus-minus").text()=="+"){
                $(this).find(".filter-plus-minus").text("-");
                $(this).next().css("height",dropdownheight+"px");
                $(this).next().find(".filters-ul").css("height","auto");
            }
            else{
                $(this).next().css("height","0");
                $(this).find(".filter-plus-minus").text("+");
            }
        }
        else{
            $(this).next().find(".show-more").css("display","block");
            $(this).next().find(".show-more").text("More");
            if($(this).find(".filter-plus-minus").text()=="+"){
                $(this).find(".filter-plus-minus").text("-");
                $(this).next().css("height","180px");
                $(this).next().find(".filters-ul").css("height","142px");
            }
            else{
                $(this).next().css("height","0");
                $(this).find(".filter-plus-minus").text("+");
            }
        }
    });
    //SHOW MORE SIDE-BAR FILTER
    $(".show-more").click(function(){
        if($(this).text()=="More"){
            $(this).closest(".filter-side-dropdown").css("height","auto");
            $(this).prev(".filters-ul").css("height","auto");
            $(this).text("Less");
        }
        else{
            $(this).closest(".filter-side-dropdown").css("height","180px");
            $(this).prev(".filters-ul").css("height","142px");
            $(this).text("More");
        }     
    });
});
// LOADER
window.addEventListener("DOMContentLoaded", () => {
    $("#loadingwrapper").css({
      display: "block",
      visibility: "visible",
      opacity: "1",
    });
    $("body").css({
        overflow: "hidden"
    });
    setTimeout(removeLoader, 900);
  });
  function removeLoader() {
    $("#loadingwrapper").fadeOut(600, function () {
      $("#loadingwrapper").remove();
    });
    $("body").css({
        overflow: "visible"
    });
  }
  //CATEGORIES DISPLAY BLOCK/NONE ON RESIZE
  var onresize = function() {
    width = document.body.clientWidth;
    if(width>480){
        $(".categoriesmobile").next().css("display","block");
    }
    else{
        $(".categoriesmobile").next().css("display","none")
    }
    if(width>992){
        $(".navbar-mobile-dropdown").css("display","none");
    }
 }
 window.addEventListener("resize", onresize);