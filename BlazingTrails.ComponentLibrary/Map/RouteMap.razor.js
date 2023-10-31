export function initialize(hostElement) {
    hostElement.map = L.map(hostElement).setView([51.7002, -0.10], 3);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
        maxZoom: 18,
        opacity: .75
    }).addTo(hostElement.map);

    hostElement.waypoints = [];
    hostElement.lines = [];
    hostElement.map.on('click', function (e) {
        let waypoint = L.marker(e.latlng);
        waypoint.addTo(hostElement.map);
        hostElement.waypoints.push(waypoint);
        let line = L.polyline(hostElement.waypoints.map(m => m.getLatLng()), { color: 'var(--brand)' }).addTo(hostElement.map);
        hostElement.lines.push(line);
    });
}

export function deleteLastWayPoint(hostElement) {
    if (hostElement.waypoints.length > 0) {
        let lastWayPont = hostElement.waypoints[hostElement.waypoints.length - 1];
        hostElement.map.removeLayer(lastWayPont);
        hostElement.waypoints.pop();

        

        if (hostElement.lines.length > 0) {
            let lastLine = hostElement.lines[hostElement.lines.length - 1];
            lastLine.remove(hostElement.map);
            hostElement.lines.pop();

            console.log(`Deleted waypoint (and lines) at latitude ${lastWayPont.getLatLng().lat} longitude ${lastWayPont.getLatLng().lng}`);
            return `Deleted waypoint (and lines) at latitude ${lastWayPont.getLatLng().lat} longitude ${lastWayPont.getLatLng().lng}`;
        }
        console.log(`Deleted waypoint at latitude ${lastWayPont.getLatLng().lat} longitude ${lastWayPont.getLatLng().lng}`);
        return `Deleted waypoint at latitude ${lastWayPont.getLatLng().lat} longitude ${lastWayPont.getLatLng().lng}`;
    }
}