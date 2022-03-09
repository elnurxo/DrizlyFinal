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
    //PRODUCTS-SHOP-SLIDER
    $('.drink-slider').slick({
        arrows: true,
        infinite: true,
		slidesToShow: 6,
        slidesToScroll: 1,
        autoplay: true,
        autoplaySpeed: 1200,
		responsive: [
            {
                breakpoint: 1400,
				settings: {
					arrows: false
				}
            },

			{
				breakpoint: 992,
				settings: {
                    arrows: false,
					slidesToShow: 4
				}
			},
            {
				breakpoint: 768,
				settings: {
                    arrows: false,
					slidesToShow: 2
				}
			},
			{
				breakpoint: 480,
				settings: {
                    arrows: false,
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
    // FILTER SIDEBAR CLICK
    $(".more-filter").click(function(){
        if($(".filter-body").css("visibility")=="hidden"){
            $(".filter-sidebar").css("transform","translate(0px, 0px)");
            $(".filter-body").css("visibility","visible");
            return;
        }
    });
    //SHOW MORE BUTTON ON DOCUMENT READY
    $(document).ready(function(){
        $(".filter-list-button").each(function(){
            let itemcount = $(this).next().find(".filter-link").length;
            let dropdownheight = itemcount * 38;
            if(itemcount<=4){
                $(this).next().find(".show-more").css("display","none");
                    $(this).find(".filter-plus-minus").text("-");
                    $(this).next().css("height",dropdownheight+"px");
                    $(this).next().find(".filters-ul").css("height","auto");
            }
        });
       
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
    //SHOW MORE CATEGORY ABOUT
    $(".category-show-more-btn").click(function(){
        if($(this).text()=="Show More"){
            $(".category-content").removeClass("show-more-less-category");
            $(this).text("Show Less");
        }
        else{
            $(".category-content").addClass("show-more-less-category");
            $(this).text("Show More");
        }
    })
     //SHOW MORE CATEGORY PAGINATED ABOUT
     $(".category-show-more-btn-pgn").click(function(){
        if($(this).text()=="Show More"){
            $(".category-content-paginated").removeClass("show-more-less-category-paginated");
            $(this).text("Show Less");
        }
        else{
            $(".category-content-paginated").addClass("show-more-less-category-paginated");
            $(this).text("Show More");
        }
    })
    //SORTING BUTTON CLICK DROPDOWN
    $(".sorting-button").click(function(){
        if($(this).next().hasClass("fa-chevron-down")){
            $(this).next().removeClass("fa-chevron-down");
            $(this).next().addClass("fa-chevron-up");
            $(".sorting-dropdown").css("display","block");
        }
        else{
            $(this).next().removeClass("fa-chevron-up");
            $(this).next().addClass("fa-chevron-down");
            $(".sorting-dropdown").css("display","none");
        }
   
    });
    //SORTING DROP-DOWN ITEM CLICK
    $(".sorting-list-item").click(function(){
        let sortingitem = $(this).text();
        let sorting = $(".sorting-button span");
        sorting.text(sortingitem);
        $(".sorting-button").next().removeClass("fa-chevron-up");
        $(".sorting-button").next().addClass("fa-chevron-down");
        $(".sorting-dropdown").css("display","none");
    });
    //MOBILE SEARCH CLICK
    $(".search-mobile").click(function(){
        let hamburger = $(".hamburger-mobile");
        if($(hamburger).children().attr("class")=="fas fa-bars"){
            $(hamburger).children().removeClass("fa-bars");
            $(hamburger).children().addClass("fa-times");
            $(".navbar-mobile-search-wrapper").css("display","block");
            return;
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
             $(".navbar-mobile-search-wrapper").css("display","none");
             return;
         }   
    })
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
        $(".navbar-mobile-search-wrapper").css("display","none");
    }
 }
 window.addEventListener("resize", onresize);
 //PROGRESS-BAR COMMENT REVIEW
 let progressContainerfive = document.querySelector('.star-five-container');
 let progressContainerfour = document.querySelector('.star-four-container');
 let progressContainerthree = document.querySelector('.star-three-container');
 let progressContainertwo = document.querySelector('.star-two-container');
 let progressContainerone = document.querySelector('.star-one-container');

 setPercentage();
 function setPercentage() {
    //Star Five
    let percentagefive = progressContainerfive.getAttribute('data-percentage') + '%';
    let progressElfive = document.querySelector('.star-five');
    progressElfive.style.width = percentagefive;
    //Star Four
    let percentagefour = progressContainerfour.getAttribute('data-percentage') + '%';
    let progressElfour = document.querySelector('.star-four');
    progressElfour.style.width = percentagefour;
    //Star Three
    let percentagethree = progressContainerthree.getAttribute('data-percentage') + '%';
    let progressElthree = document.querySelector('.star-three');
    progressElthree.style.width = percentagethree;
    //Star Two
    let percentagetwo = progressContainertwo.getAttribute('data-percentage') + '%';
    let progressEltwo = document.querySelector('.star-two');
    progressEltwo.style.width = percentagetwo;
    //Star One
    let percentageone = progressContainerone.getAttribute('data-percentage') + '%';
    let progressElone = document.querySelector('.star-one');
    progressElone.style.width = percentageone;
 }


 //PRICE RANGING FILTER

     var rangeSlider = $(".price-range"),
     amount = $("#amount"),
     amountmobile = $("#amountmobile"),
     minPrice = rangeSlider.data('min'),
     maxPrice = rangeSlider.data('max');
 rangeSlider.slider({
     range: true,
     min: minPrice,
     max: maxPrice,
     values: [minPrice, maxPrice],
     slide: function (event, ui) {
         amount.val("$" + ui.values[0] + " - $" + ui.values[1]);
         amountmobile.val(("$" + ui.values[0] + " - $" + ui.values[1]));
     }
 });
 amount.val(" $" + rangeSlider.slider("values", 0) +
     " - $" + rangeSlider.slider("values", 1));
 amountmobile.val(" $" + rangeSlider.slider("values", 0) +
 " - $" + rangeSlider.slider("values", 1));
//ABV RANGING FILTER

     var rangeSliderabv = $(".abv-range"),
     amountabv = $("#amountabv"),
     amountabvmobile = $("#amountabvmobile");
     minPriceabv = rangeSliderabv.data('min'),
     maxPriceabv = rangeSliderabv.data('max');
 rangeSliderabv.slider({
     range: true,
     min: minPriceabv,
     max: maxPriceabv,
     values: [minPriceabv, maxPriceabv],
     slide: function (event, ui) {
         amountabv.val("%" + ui.values[0] + " - %" + ui.values[1]);
         amountabvmobile.val("%" + ui.values[0] + " - %" + ui.values[1]);
     }
 });
 amountabv.val(" %" + rangeSliderabv.slider("values", 0) +
 " - %" + rangeSliderabv.slider("values", 1));
 amountabvmobile.val(" %" + rangeSliderabv.slider("values", 0) +
 " - %" + rangeSliderabv.slider("values", 1));
