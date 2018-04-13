import { NgModule } from "@angular/core";
import { Route } from "@angular/router";
import { VendorRegistrationComponent } from "./registration/vendor-registration.component";

const vendorsRoute: Route = {
    path: "vendor",
    children: [
        { path: "registration", component: VendorRegistrationComponent },
        { path: "userstats", redirectTo: "registration" }
    ]
};
export default vendorsRoute;

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