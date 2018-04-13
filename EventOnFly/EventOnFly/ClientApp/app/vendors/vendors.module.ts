import { NgModule } from "@angular/core";
import { Route } from "@angular/router";
import { VendorRegistrationComponent } from "./registration/vendor-registration.component";

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
    ]
})
export class VendorsModule {
}