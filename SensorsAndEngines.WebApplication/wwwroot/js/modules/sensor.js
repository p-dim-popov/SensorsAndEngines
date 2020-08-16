import { LitElement, html } from "https://cdn.skypack.dev/lit-element@^2.3.1";

class Sensor extends LitElement {
    static get properties() {
        return {
            name: {
                type: String
            },
            currentSensorNumber: {
                type: Number
            },
            measurementUnit: {
                type: String
            },
            pin: {
                type: Number
            },
            measurementUnits: {
                type: Array
            }
        }
    }

    constructor(name) {
        super();
        this.name = name;
        this.measurementUnits = [];
    }

    changeName() {
        this.name = prompt("Enter name of sensor:", this.name) || this.name;
    }

    remove(e) {
        e.target.closest("custom-sensor").remove();
    }

    async firstUpdated() {
        this.measurementUnits = await window.measurementUnits
            .then(d => Object.entries(d)
                .map(([_, value]) => html`<option value="${value.code}">${value.code} - ${value.name}</option>`));
    }

    render() {
        return html`
${html([window.bootstrapHeaders])}
<div class="card m-2">
  <div class="card-body">
    <div class="container">
        <div class="row">
                <h5 class="card-title col">${this.name}</h5>
                <h5 @click="${this.changeName}" class="card-title p-1 col-auto border rounded">🖉</h5>
        </div>
    </div>
    <p class="card-text">Info: Add info here</p>
  </div>
  <ul class="list-group list-group-flush">
    <li class="list-group-item">
        <label for="pin">Pin</label>
        <input type="number" name="pin">${this.pin}</input>
    </li>
    <li class="list-group-item">
        <div class="input-group mb-3">
          <div class="input-group-prepend">
            <label class="input-group-text" for="inputGroupSelect01">Measurement Unit</label>
          </div>
          <select class="custom-select" id="inputGroupSelect01">
            ${this.measurementUnits}
          </select>
        </div>
    </li>
  </ul>
  <div class="card-body">
    <button @click="${this.remove}" type="button" class="btn btn-danger">Remove</button>
  </div>
</div>
`;
    }
}

class AnalogSensor extends Sensor {
    upperRange = 0;
    lowerRange = 0;

    constructor() {
        super("Analog Sensor");
    }
}
customElements.define("analog-sensor", AnalogSensor);

class DigitalSensor extends Sensor {
    constructor() {
        super("Digital Sensor");
    }
}
customElements.define("digital-sensor", DigitalSensor);