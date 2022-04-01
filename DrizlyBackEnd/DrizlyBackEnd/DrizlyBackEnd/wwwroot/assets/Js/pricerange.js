$(document).ready(function () {


    ////ABV RANGING FILTER INDEX
    $("#minAbvindexInput").val("0");
    $("#maxAbvindexInput").val("100");
    var rangeSliderabvindex = $(".abv-range-index"),
        amountabvindex = $("#amountabvmobileindex"),
        minPriceabvindex = rangeSliderabvindex.data('min'),
        maxPriceabvindex = rangeSliderabvindex.data('max');
    amountabvindex.val("%" + minPriceabvindex + " - %" + maxPriceabvindex);
    rangeSliderabvindex.slider({
        range: true,
        min: minPriceabvindex,
        max: maxPriceabvindex,
        values: [minPriceabvindex, maxPriceabvindex],
        slide: function (event, ui) {
            amountabvindex.val("%" + ui.values[0] + " - %" + ui.values[1]);
        },
        change: function (event, ui) {
            $("#minAbvindexInput").val(ui.values[0]);
            $("#maxAbvindexInput").val(ui.values[1]);
        }
    });

    //ABV RANGING FILTER
    $("#minAbvInput").val("0");
    $("#maxAbvInput").val("100");
    var rangeSliderabv = $(".abv-range-shop"),
        amountabv = $("#amountmobileabv"),
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
});





