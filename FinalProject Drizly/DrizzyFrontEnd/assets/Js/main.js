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
    //CATEGORIES MOBILE ACCORDION
    $(".categoriesmobile").click(function(){
       if($(window).width() <= 480){
        if($(this).next().css("display")=="none"){
            console.log("block ele")
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
    console.log(width);
    if(width>480){
        $(".categoriesmobile").next().css("display","block")
    }
    else{
        $(".categoriesmobile").next().css("display","none")
    }
 }
 window.addEventListener("az", onresize);