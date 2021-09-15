// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.App = window.App || (function () {

    function formatDateTime(jsonDateTime) {
        var dtObj = new Date(jsonDateTime);

        var datestring =
            dtObj.getFullYear()
            + "-" + zeroPad((dtObj.getMonth() + 1), 2)
            + "-" + zeroPad(dtObj.getDate(), 2)
            + " "
            + zeroPad(dtObj.getHours(), 2)
            + ":" + zeroPad(dtObj.getMinutes(), 2);

        return datestring;
    }

    const zeroPad = (num, places) => String(num).padStart(places, '0');

    return {
        formatDateTime: formatDateTime,
        zeroPad: zeroPad
    }
})();