// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.App = window.App || (function () {

    function formatDateTime(jsonDateTime) {
        var dtObj = new Date(jsonDateTime);

        var datestring =
            dtObj.getFullYear()
            + "-" + ((dtObj.getMonth() + 1) | "00")
            + "-" + (dtObj.getDate() | "00")
            + " "
            + (dtObj.getHours() | "00")
            + ":" + (dtObj.getMinutes() | "00");

        return datestring;
    }

    return {
        formatDateTime: formatDateTime
    }
})();