import { Component } from "@angular/core";
import {RegistrationApiService} from "./registration.api.service";

@Component({
    selector: "vendor-registration",
    templateUrl: "./vendor-registration.component.html"
})
export class VendorRegistrationComponent {
    constructor(private registrationApiService: RegistrationApiService) {
    }

    public sendStartForm() {
        this.registrationApiService.startRegistration();
    }
}