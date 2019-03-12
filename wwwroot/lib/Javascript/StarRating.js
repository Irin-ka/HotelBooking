$(".star-container .fa").mouseover(function () {
    var rating = $(this).attr("id")[$(this).attr("id").length - 1];
    $(".star-container .fa").each(function () {
        if ($(this).attr("id")[$(this).attr("id").length - 1] <= rating) {
            $(this).addClass("fa-star");
            $(this).removeClass("fa-star-o");
        } else {
            $(this).addClass("fa-star-o");
            $(this).removeClass("fa-star");
        }
    });
    $("#star-rating-val").val(rating);
});

$(".star-container .fa").click(function () {
    var rating = $(this).attr("id")[$(this).attr("id").length - 1];
    $("#star-rating-val").val(rating);
});