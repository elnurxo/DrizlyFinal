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

