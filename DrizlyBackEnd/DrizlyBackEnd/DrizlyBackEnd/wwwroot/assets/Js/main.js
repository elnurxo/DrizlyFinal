$(function () {
    //TYPE-PRODUCT FILTER
    $(document).on("change", ".typeproductfilter", function (e) {
        $("#filterForm").submit();
    })
    //BRAND FILTER
    $(document).on("change", ".brandfilter", function (e) {
        $("#filterForm").submit();
    })
    //COUNTRY FILTER
    $(document).on("change", ".countryfilter", function (e) {
        $("#filterForm").submit();
    })
    //TYPE PRODUCTS CLICK MAKE INSIDE INPUT CHECKED 
    $(".subcategory-btn").click(function () {
        console.log($(this).children().find(".typeproductfilter"));
        $(this).children().find(".typeproductfilter").attr('checked', 'checked');
        $("#filterForm").submit();
    });
    //TYPE-PRODUCT FILTER
    $(document).on("change", ".typeproductfilter1", function (e) {
        $("#filterForm1").submit();
    })
    //BRAND FILTER
    $(document).on("change", ".brandfilter1", function (e) {
        $("#filterForm1").submit();
    })
    //COUNTRY FILTER
    $(document).on("change", ".countryfilter1", function (e) {
        $("#filterForm1").submit();
    })
    //CURRENT BASKET COUNT
    let productcounter = $(".basket-counter").val();
    $(".basket-count").text(productcounter);
    //CURRENT BASKET COUNT ON BASKET VIEW

    //SET SESSION STORAGE ON AGE 21 YES CLICK
    $(".age-yes").click(function () {
        sessionStorage.setItem('agemodal', 'confirmed');
         if (sessionStorage.getItem('agemodal') !== null) {
        $(".age-gate-modal").css("display", "none");
        $(".age-yes").closest('.age-gate-modal').remove();
    }
    });
    if (sessionStorage.getItem('agemodal') !== null) {
        $(".age-gate-modal").css("display", "none");
        $(".age-yes").closest('.age-gate-modal').remove();
    }

    $("#search-desktop").on("keyup", function () {

        var searchform = document.getElementById("search-form");
        var url = searchform.action;
        var searchinput = $("#search-desktop").val().toLowerCase();

        if (searchinput.length < 3) {
            $(".navbar-search-dropdown").children().remove();
            $(".navbar-search-dropdown").css("display", "none");
            return;
        }
        if (searchinput==undefined || searchinput==null || searchinput=="") {
            $(".navbar-search-dropdown").children().remove();
            $(".navbar-search-dropdown").css("display", "none");
            return;
        }

        var formData = new FormData();
        formData.append("searchString", searchinput);

        fetch(url, {
            method: "POST",
            body: formData,
            headers: {
                'Accept': 'application/json'
            }
        })
            .then(response => {
                if (!response.ok) {
                    throw response
                }
                return response.text();
            })
            .then(data => {
                $(".navbar-search-dropdown").html(data);
                $(".navbar-search-dropdown").css("display", "block")
            })
    });

    //SEARCH INPUT ENTER CLICK
    $("#search-form").on("submit", function (e) {   
        e.preventDefault();
        $("#search-list-item-1")[0].click();
    });
    $("#entercoupon").on("keyup", function () {
        console.log($(this).val().length);
        if ($(this).val().length==36) {
            $(".apply-coupon-btn").removeAttr("disabled");
        }
        else {
            $(".apply-coupon-btn").attr("disabled", "disabled");
        }
       
    });
    $(".apply-coupon-btn").click(function () {
        $("#couponcheckbox").prop("checked", true);
        $(this).css("background-color", "#22BB33");
        $(".apply-coupon-text").text("Coupon Applied!");
        $("#couponaccordion").remove();
        $(".couponapplytext").css("display", "inline");
    });
    //HAVE A COUPON? CLICK
    $("#couponaccordion").click(function () {
        $("#couponaccordionbody").slideToggle();
    });

    //VIEW ORDER BUTTON CLICK TOGGLE
    $(".vieworderbtn").click(function () {
        $(this).parent().parent().next().slideToggle();
    });

    //SEARCH INPUT MOBILE
    $("#search-mobile").on("keyup", function () {
        var searchform = document.getElementById("search-form-mobile");
        var url = searchform.action;
        var searchinput = $("#search-mobile").val().toLowerCase();

        if (searchinput.length < 3) {
            $(".mobile-navbar-search-dropdown").children().remove();
            $(".mobile-navbar-search-dropdown").css("display", "none");
            return;
        }
        if (searchinput == undefined || searchinput == null || searchinput == "") {
            $(".mobile-navbar-search-dropdown").children().remove();
            $(".mobile-navbar-search-dropdown").css("display", "none");
            return;
        }

        var formData = new FormData();
        formData.append("searchString", searchinput);

        fetch(url, {
            method: "POST",
            body: formData,
            headers: {
                'Accept': 'application/json'
            }
        })
            .then(response => {
                if (!response.ok) {
                    throw response
                }
                return response.text();
            })
            .then(data => {
                $(".mobile-navbar-search-dropdown").html(data);
                $(".mobile-navbar-search-dropdown").css("display", "block")
            })
    });
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
            let dropdownheight = itemcount * 40;
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
    //WRITE A REVIEW BUTTON CLICK
    $(".write-review-btn").click(function(){
        $(this).css("display","none");
        $(".review-form").css("height","auto");
        $(".review-form").css("overflow","visible");
    });
    //CANCEL TO WRITE A REVIEW BUTTON CLICK
    $(".cancel-review-btn").click(function(){
        $(".write-review-btn").css("display","block");
        $(".review-form").css("height","0");
        $(".review-form").css("overflow","hidden");
    });
     //SUBMIT TO WRITE A REVIEW BUTTON CLICK
     $(".submit-review-btn").click(function(){
        $(".write-review-btn").css("display","block");
        $(".review-form").css("height","0");
        $(".review-form").css("overflow","hidden");
    });
    //AGE MODAL CLICK
    $(".age-no").click(function(){
        $(".age-no-clicked").css("display","block");
        $(".age-yes").attr("disabled", true);
        $(".age-yes").css("background", "#d27c7c");
    });
    $(".age-yes").click(function(){
        $(".age-gate-modal").css("display", "none");
        $(this).closest('.age-gate-modal').remove();
    });
    //MY ACCOUNT TAB
    $(".tab-naviagtion__item").click(function (event) {
        event.preventDefault();
        $(".myaccount-tab-menu a").removeClass("active");
        $(this).addClass("active");
        let index=$(this).attr("id");
        $(".tab-pane").removeClass("active");
        $(".tab-pane").removeClass("show");
        $(`.${index}-content`).addClass("active");
        $(`.${index}-content`).addClass("show");
    });
    //PROFILE PICTURE DELETE
    $(".remove-img-box").click(function () {
        console.log("sdjhfsjkdimage");
        $(".profile-picture").attr("src", "/uploads/users/avatar.png");
        $(".profile-pic-wrapper").remove();
        $(this).css("display", "none");
    });
    //ADD BASKET
    $(document).on("click", ".basket-add", function (e) {
        e.preventDefault();
        let url = $(this).attr("href");

        fetch(url)
            .then(function (response) { 
                if (!response.ok) {
                    alert("Error!")
                }
                return response.text();
            }).then(data => {
                console.log(data);
                $(".basket-hover").html(data)
                let productcounter = $(".basket-counter").val();
                $(".basket-count").text(productcounter);
            });
        let basketview = "https://localhost:44321/order/basket";
        if (window.location.href == basketview) {
            window.setTimeout(function () { location.reload() }, 1000)
        }
    })
    //REMOVE BASKET
    $(document).on("click", ".basket-remove", function (e) {
        e.preventDefault();
        let url = $(this).attr("href");
        fetch(url)
            .then(function (response) {
                if (!response.ok) {
                    alert("Error!")
                }
                return response.text();
            }).then(data => {
                $(".basket-hover").html(data)
                let productcounter = $(".basket-counter").val();
                $(".basket-count").text(productcounter);
            });
          let basketview = "https://localhost:44321/order/basket";
        if (window.location.href == basketview) {
            window.setTimeout(function () { location.reload() }, 1000)
        }
    })
    //BASKET NAVBAR HEIGHT
    $(".navbar-basket").hover(
        function () {
            if ($(".basket-hover").css("height") > "435px") {
                $(".basket-hover").css("height", "435px");
                $(".basket-hover").css("overflow-y", "auto");
            }
        }
    );
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
 //STAR COMMENT LOG
 $(':radio').change(function() {
    console.log('New star rating: ' + this.value);
  });
window.addEventListener("resize", onresize);
//CURRENT WINDOW LOCATION URL
let windowurl = window.location.href;
if (windowurl == "https://localhost:44321/order/basket") {
    console.log("burdayamm");
    console.log(document.getElementById("card-products-section"));
    document.getElementById("card-products-section").style.display = "block";
    //document.getElementsByClassName("basket-view").style.display = "block";
}

