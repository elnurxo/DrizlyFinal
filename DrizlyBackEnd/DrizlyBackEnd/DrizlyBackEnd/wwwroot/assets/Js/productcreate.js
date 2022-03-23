﻿$(document).ready(function () {
    //ISPACKET  INPUT CLICK
    $('#isPacketInput').click(function () {
        if ($(this).is(":checked")) {
            console.log("checked");
            let removesize = $(".productSizeContainer").hide();
            $(".productCountContainer").show();

        } else {
            console.error("UnChecked!");
            $(".productSizeContainer").show();
            $(".productCountContainer").hide();
        }
    });
    /*REMOVE THE OTHER ONE (ISPACKET) AT THE END CREATE*/
    $(".create-product-btn").click(function () {
        if ($(".productSizeContainer").css("display")=="none") {
            $(".productSizeContainer").remove();
        }
        if ($(".productCountContainer").css("display") == "none") {
            $(".productCountContainer").remove();
        }
        if ($("#sweetdrywrapper").css("display") == "none") {
            $("#sweetdrywrapper").remove();
        }
        if ($("#abvwrapper").css("display") == "none") {
            $("#abvwrapper").remove();
        }
        if ($("#liquorflavorwrapper").css("display") == "none") {
            $("#liquorflavorwrapper").remove();
        }
        if ($("#liquorcolorwrapper").css("display") == "none") {
            $("#liquorcolorwrapper").remove();
        }
        if ($("#winefoorpairingwrapper").css("display") == "none") {
            $("#winefoorpairingwrapper").remove();
        }
    });
    $("#categoryId").change(function () {
        $("#typeProductId").val('0');
        let categoryId = $(this).val();
        console.log(categoryId);
        if (categoryId == 2) {
            $("#sweetdrywrapper").show();
            $("#winefoorpairingwrapper").show();
        }
        else {
            $("#sweetdrywrapper").hide();
            $("#winefoorpairingwrapper").hide();
        }
        if (categoryId == 4) {
            $("#abvwrapper").hide();
        }
        else {
            $("#abvwrapper").show();
        }
        if (categoryId == 3) {
            $("#liquorflavorwrapper").show();
            $("#liquorcolorwrapper").show();
        }
        else {
            $("#liquorflavorwrapper").hide();
            $("#liquorcolorwrapper").hide();
        }
        let counter = 0;
        $("#typeProductId").children().each(function () {

            if (categoryId != $(this).attr("data-category")) {
                $(this).css("display", "none");
            }
            else {
                $(this).css("display", "block");          
            }
        });
        for (var i = 0; i < 1; i++) {
            $("#typeProductId").val('0');
            console.log("salam hey");
        }
    });
    $("#typeProductId").children().each(function (index) {
        let count = 0
        if ($(this).attr("data-category")!=1) {
            $(this).css("display", "none");
        }
        else {
            $(this).css("display", "block"); 
        }
    });
});