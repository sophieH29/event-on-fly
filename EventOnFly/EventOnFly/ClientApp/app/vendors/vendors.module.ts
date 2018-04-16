import { NgModule } from "@angular/core";
import { Route } from "@angular/router";
import { VendorRegistrationComponent } from "./registration/vendor-registration.component";
import {RegistrationApiService} from "./registration/registration.api.service";

export const vendorsRoute: Route = {
    path: "vendor",
    children: [
        { path: "registration", component: VendorRegistrationComponent },
        { path: "userstats", redirectTo: "registration" }
    ]
};

@NgModule({
    declarations: [
        VendorRegistrationComponent
    ],
    exports: [
        VendorRegistrationComponent
    ],
    providers: [
        RegistrationApiService
    ]
})
export class VendorsModule {
}