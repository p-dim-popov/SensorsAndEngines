import { LitElement, html, css } from "https://jspm.dev/lit-element";
import { until } from "https://jspm.dev/lit-html/directives/until.js";
import { directive } from "https://jspm.dev/lit-html/lib/directive.js";

const LitSync = (baseElement) => class extends baseElement {
    constructor() {
        super();
        this.sync = directive((property, eventName) => (part) => {
            part.setValue(this[property]);

            if (!part.syncInitialized) {
                part.syncInitialized = true;

                const notifyingElement = part.committer.element;
                const notifyingProperty = part.committer.name;
                const notifyingEvent = eventName || "change";

                notifyingElement.addEventListener(notifyingEvent, (e) => {
                    const oldValue = this[property];
                    this[property] = e.target.value;
                    if (this.__lookupSetter__(property) === undefined) {
                        this.updated(new Map([[property, oldValue]]));
                    }
                });
            }
        });
    }
}

class SensorCardComponent extends LitSync(LitElement) {
    static get properties() {
        return {
            name: {
                type: String
            },
            pin: {
                type: Number
            },
            measurementUnit: {
                type: String
            },
            info: {
                type: String
            }
        }
    }

    constructor(type, outterReferenceCallback) {
        super();
        this._outterReferenceCallback = outterReferenceCallback;
        this.type = type;
        this.name = type.name;
        this.pin = 0;
        this.info = "";
        this.measurementUnit = "";
    }

    removeComponent() {
        this._outterReferenceCallback().remove();

    }

    get measurementUnits() {
        return window.measurementUnits
            .then(d => Object.entries(d)
                .map(([_, value]) => html`<option ?selected=${value.code === this.measurementUnit} .value="${value.code}">${value.code} - ${value.name}</option>`));
    }

    asSerializable() {
        let sensor = {
            name: this.name,
            info: this.info,
            pin: +this.pin,
            measurementUnit: this.measurementUnit,
            type: this.type.tagName.toLowerCase()
        }

        if (sensor.type === "analog-sensor") {
            sensor = {
                ...sensor,
                lowerRange: +this.type.lowerRange,
                upperRange: +this.type.upperRange
            }
        }

        return sensor;
    }

    static asLitElement(obj) {
        let sensorType;
        switch (obj["type"]) {
            case "analog-sensor":
                sensorType = new AnalogSensorComponent();
                sensorType.lowerRange = obj["lowerRange"];
                sensorType.upperRange = obj["upperRange"];
                break;
            case "digital-sensor":
                sensorType = new DigitalSensorComponent();
                break;
            default:
                sensorType = null;
                break;
        }

        let lit = new SensorCardComponent(sensorType, _ => lit);
        lit.measurementUnit = obj["measurementUnit"];
        lit.name = obj["name"];
        lit.info = obj["info"];
        lit.pin = obj["pin"];
        return lit;
    }

    render() {
        return html`
${html([window.bootstrapHeaders])}
<div class="card m-2">
  <div class="card-body">
    <div class="container">
        <div class="row">
            <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text">Name</div>
                </div>
                <input type="text" class="card-title col form-control" .value=${this.sync("name")}></input>
            </div>
        </div>
    </div>
    <textarea class="card-text form-control" .value=${this.sync("info")}></textarea>
  </div>
  <ul class="list-group list-group-flush">
    <li class="list-group-item">
        <div class="input-group">
            <div class="input-group-prepend">
              <div class="input-group-text">Pin</div>
            </div>
            <input type="number" name="pin" class="form-control" .value=${this.sync("pin")}></input>
        </div>
    </li>
    <li class="list-group-item">
        <div class="input-group mb-3">
          <div class="input-group-prepend">
            <label class="input-group-text">Measurement Unit</label>
          </div>
          <select .value=${this.sync("measurementUnit")} class="custom-select">
            ${until(this.measurementUnits, html`<option value="Loading...">Please wait...</option>`)}
          </select>
        </div>
    </li>
    ${this.type}
  </ul>
  <div class="card-body">
    <button @click="${this.removeComponent}" type="button" class="btn btn-danger">Remove</button>
  </div>
</div>
`;
    }
}

customElements.define("sensor-card", SensorCardComponent);

class AnalogSensorComponent extends LitSync(LitElement) {
    static get properties() {
        return {
            name: {
                type: String
            },
            lowerRange: {
                type: Number
            },
            upperRange: {
                type: Number
            }
        }
    }

    constructor() {
        super();
        this.name = "Analog Sensor";
        this.lowerRange = 0;
        this.upperRange = 0;
    }

    render() {
        return html`
${html([window.bootstrapHeaders])}
<li class="list-group-item">
    <div class="input-group">
        <div class="input-group-prepend">
          <div class="input-group-text">Lower Range</div>
        </div>
        <input type="number" name="lowerRange" class="form-control" .value="${this.sync("lowerRange", "change")}"></input>
    </div>
</li>
<li class="list-group-item">
    <div class="input-group">
        <div class="input-group-prepend">
          <div class="input-group-text">Upper Range</div>
        </div>
        <input type="number" name="upperRange" class="form-control" .value="${this.sync("upperRange", "change")}"></input>
    </div>
</li>
`;
    }
}
customElements.define("analog-sensor", AnalogSensorComponent);

class DigitalSensorComponent extends LitSync(LitElement) {
    static get properties() {
        return {
            name: {
                type: String
            }
        }
    }

    constructor() {
        super();
        this.name = "Digital Sensor";
    }
}
customElements.define("digital-sensor", DigitalSensorComponent);

class Sensors {
    constructor(htmlElement) {
        this.list = [];
        this.cards = htmlElement;
    }

    appendSensor(sensor, sensorType) {
        return () => {
            const s = sensor || new SensorCardComponent(new sensorType(), _ => s);
            this.cards.appendChild(s);
            this.list.push(s);
            return s;
        }
    }

    removeSensor(sensor) {
        this.list = this.list
            .filter(el => el !== sensor);
        sensor.removeComponent(this);
    }

    //Example usage: before serializing the list
    _removeDeadCards() {
        this.list = this.list
            .filter(el => [...this.cards.children].indexOf(el) !== -1);
    }

    asSerializable() {
        this._removeDeadCards();
        return this.list.map(s => s.asSerializable());
    }
}

export { AnalogSensorComponent, DigitalSensorComponent, SensorCardComponent, Sensors }