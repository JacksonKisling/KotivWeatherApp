var map;

window.initializeMap = function (defaultLat, defaultLng, dotNetHelper) {
    const center = { lat: defaultLat, lng: defaultLng };

    map = new google.maps.Map(document.getElementById("map"), {
        center: center,
        zoom: 7
    });

    map.addListener("click", (e) => {
        const lat = e.latLng.lat();
        const lng = e.latLng.lng();
        dotNetHelper.invokeMethodAsync("OnMapClicked", lat, lng);
    });
};

window.moveMapTo = function (lat, lng) {
    if (!map) return;

    map.panTo({ lat: lat, lng: lng });
    map.setZoom(10);
};

