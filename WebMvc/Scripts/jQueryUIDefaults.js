$(document).ready(function () {
    $(".DatePicker").datepicker({ changeMonth: true, changeYear: true });
    $(".hasDatepicker").width("75px");
    $(':button, :input[type=button], :input[type=submit], .button').button();
    $(':input[type=text]').addClass("widget");
});