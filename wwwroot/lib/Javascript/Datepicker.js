$("#datepicker-in").datepicker({
    minDate: 0,
    showAnim: "fade",
    dateFormat: 'dd/mm/yy',
    onSelect: function (dateText) {
        $("#datepicker-out").datepicker("option", "minDate", dateText);
        $("#datepicker-out").datepicker("setDate", dateText);
    }
});

$("#datepicker-out").datepicker({
    minDate: +1,
    showAnim: "fade",
    dateFormat: 'dd/mm/yy'
});