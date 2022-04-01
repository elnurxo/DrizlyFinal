$(document).ready(function () {

    //ABV RANGING FILTER
    $("#minAbvInput").val("0");
    $("#maxAbvInput").val("100");
    var rangeSliderabv = $(".abv-range"),
        amountabv = $("#amountabvmobile"),
        minPriceabv = rangeSliderabv.data('min'),
        maxPriceabv = rangeSliderabv.data('max');
    amountabv.val("%" + minPriceabv + " - %" + maxPriceabv);
    rangeSliderabv.slider({
        range: true,
        min: minPriceabv,
        max: maxPriceabv,
        values: [minPriceabv, maxPriceabv],
        slide: function (event, ui) {
            amountabv.val("%" + ui.values[0] + " - %" + ui.values[1]);
        },
        change: function (event, ui) {
            $("#minAbvInput").val(ui.values[0]);
            $("#maxAbvInput").val(ui.values[1]);
        }
    });

    //ABV MOBILE RANGING FILTER
    $("#minAbvInputmobile").val("0");
    $("#maxAbvInputmobile").val("100");
    var rangeSliderabvMob = $(".abv-range-mobile"),
        amountabvmob = $("#amountabvmob"),
        minPriceabvmob = rangeSliderabvMob.data('min'),
        maxPriceabvmob = rangeSliderabvMob.data('max');
    amountabvmob.val("%" + minPriceabvmob + " - %" + maxPriceabvmob);
    rangeSliderabv.slider({
        range: true,
        min: minPriceabvmob,
        max: maxPriceabvmob,
        values: [minPriceabvmob, maxPriceabvmob],
        slide: function (event, ui) {

            amountabvmob.val("%" + ui.values[0] + " - %" + ui.values[1]);
            console.log(ui.values[0] + "-" + ui.values[1])
        },
        change: function (event, ui) {
            $("#minAbvInputmobile").val(ui.values[0]);
            $("#maxAbvInputmobile").val(ui.values[1]);
        }
    });
});





