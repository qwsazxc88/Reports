$(document).ready(function () {
    setActiveMenuItem("VacationReturn");

    ConfigureFields();
});
function ConfigureFields() {
    $("#ContinueDate").datepicker();
    $("#ReturnDate").datepicker();
}
function Reject() {
    $("#IsRejected").val(true);
}