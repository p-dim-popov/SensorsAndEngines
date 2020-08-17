// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
import { AnalogSensorComponent, DigitalSensorComponent, SensorCardComponent, Sensors } from "/js/modules/sensor.js";

main();

function main() {
    window.measurementUnits =
        window.fetch("/home/getMeasurementUnits").then(r => r.json());

    const sensors = new Sensors(document.getElementById("sensor-cards"));

    const [refreshBtn, downloadBtn, loadBtn, hiddenLoadBtn] = document
        .getElementById("pre-config-buttons").children;

    refreshBtn.addEventListener("click", homeRefreshPorts);
    downloadBtn.addEventListener("click", _ => downloadObjectAsJson(sensors.asSerializable()));
    loadBtn.addEventListener("click", _ => hiddenLoadBtn.click());
    hiddenLoadBtn.addEventListener("change",
        e => handleFileUpload(e, content => {
            sensors.list
                .forEach(s => sensors.removeSensor(s));

            [...JSON.parse(content)]
                .map(s => SensorCardComponent.asLitElement(s))
                .forEach(s => sensors.appendSensor(s, null)());
        }));

    const [addAnalogBtn, addDigitalBtn] = [document.getElementById("add-analog-btn"), document.getElementById("add-digital-btn")];
    addAnalogBtn.addEventListener("click", sensors.appendSensor(null, AnalogSensorComponent));
    addDigitalBtn.addEventListener("click", sensors.appendSensor(null, DigitalSensorComponent));
}

async function homeRefreshPorts(e) {
    e.preventDefault();
    const dropdown = document.getElementById("port-select-dropdown");
    [...dropdown.children]
        .forEach(c => c.remove());
    const response = await window.fetch(e.target.value);
    const ports = await response.json();
    [...ports]
        .forEach(p => dropdown.innerHTML += `<option value="${p}">${p}</option>`);
}

function handleFileUpload(evt, handler) {
    const files = evt.target.files;
    if (files.length < 1) {
        alert('select a file...');
        return;
    }
    const file = files[0];
    const reader = new FileReader();
    reader.onload = (e) => handler(e.target.result);

    reader.readAsText(file);
}

function downloadObjectAsJson(exportObj) {
    const dataStr = "data:text/json;charset=utf-8," + encodeURIComponent(JSON.stringify(exportObj, null, 1));
    const downloadAnchorNode = document.createElement('a');
    downloadAnchorNode.setAttribute("href", dataStr);
    downloadAnchorNode.setAttribute("download", (prompt("Enter filename:", "config") || "config") + ".json");
    document.body.appendChild(downloadAnchorNode); // required for firefox
    downloadAnchorNode.click();
    downloadAnchorNode.remove();
}