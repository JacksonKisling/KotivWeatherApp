function showSpinner() {
    $("#loadingSpinner").show();
}

function hideSpinner() {
    $("#loadingSpinner").hide();
}

function prepareForNewForecast() {
    $("#forecastArea").html("");
}

document.addEventListener("DOMContentLoaded", () => {

    const flyout = document.getElementById("historyFlyout");
    const openBtn = document.getElementById("openFlyout");
    const closeBtn = document.getElementById("closeFlyout");
    const content = document.getElementById("historyContent");

    openBtn.addEventListener("click", () => {
        flyout.classList.add("open");
        loadHistory();
    });

    closeBtn.addEventListener("click", () => {
        hideHistory();
    });

});

function hideHistory() {
    const flyout = document.getElementById("historyFlyout");
    flyout.classList.remove("open");
}

function loadHistory() {
    $("#historyContent").load("/History/LastFive");
}

$("#searchBox").on("keyup", function (e) {
    if (e.key === "Enter") {
        const term = $("#searchBox").val().trim();
        if (term.length > 0) {
            $("#searchButton").trigger("click");

        }
    }
});

$(document).on("click", ".recent-item", function () {

    const lat = parseFloat($(this).data("lat"));
    const lng = parseFloat($(this).data("lng"));

    if (isNaN(lat) || isNaN(lng)) {
        console.error("Invalid lat/lng on recent-item");
        return;
    }

    prepareForNewForecast();
    showSpinner();
    moveMapTo(lat, lng);

    $.get(`/Weather/FromCoords?lat=${lat}&lng=${lng}`, function (html) {
        $("#forecastArea").html(html);
        hideSpinner();
    }).fail(function () {
        hideSpinner();
        $("#forecastArea").html("<p class='text-danger'>Error loading forecast.</p>");
    });

    hideHistory();
});
