$("#slider").slider({
    range: true,
    min: 0,
    max: 5000,
    step: 1,
    values: [0, 5000],
    create: function (event, ui) {
        var sliderObj = $("#slider");
        sliderObj.slider("values", 0, $("#slider-min-input").val());
        sliderObj.slider("values", 1, $("#slider-max-input").val());
        $("#slider-min").html("\u20ac" + sliderObj.slider("values", 0));
        $("#slider-max").html("\u20ac" + sliderObj.slider("values", 1));
    },
    slide: function (event, ui) {
        var sliderObj = $("#slider");
        $("#slider-min").html("\u20ac" + sliderObj.slider("values", 0));
        $("#slider-max").html("\u20ac" + sliderObj.slider("values", 1));
        $("#slider-min-input").val(sliderObj.slider("values", 0));
        $("#slider-max-input").val(sliderObj.slider("values", 1));
    }
});