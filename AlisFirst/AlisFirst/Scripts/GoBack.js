var referrer = "";
$(document).ready(function () {
    referrer = document.referrer;
})

function goBack() {
    location.href = referrer;
}