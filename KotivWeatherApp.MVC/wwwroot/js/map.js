var map;

function initializeMap(defaultLat = 42.032974, defaultLng = -93.581543) {
    const center = { lat: defaultLat, lng: defaultLng };

    const mapElement = document.getElementById("map");
    if (!mapElement) {
        console.error("Map element with id 'map' not found.");
        return;
    }

    map = new google.maps.Map(mapElement, {
        center: center,
        zoom: 7
    });

    map.addListener("click", function (e) {
        const lat = e.latLng.lat();
        const lng = e.latLng.lng();

        moveMapTo(lat, lng);
        prepareForNewForecast();

        $.get(`/Weather/FromCoords?lat=${lat}&lng=${lng}`, function (html) {
            $("#forecastArea").html(html);
            hideSpinner();
        }).fail(function () {
            hideSpinner();
            $("#forecastArea").html("<p class='text-danger'>Error loading forecast.</p>");
        });
    });
}

function moveMapTo(lat, lng) {
    if (!map) return;

    map.panTo({ lat: lat, lng: lng });
    map.setZoom(10);
}
