$(function () {   
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