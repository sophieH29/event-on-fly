import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";
import { RouterModule, Route } from "@angular/router";

import { AppComponent } from "./app.component";
import { HomeComponent } from "./home/home.component";
import { VendorsModule } from "./vendors/vendors.module";
import vendorsRoute from "./vendors/vendors.module";

let appRoutes: Route[] = [
    { path: "", redirectTo: "home", pathMatch: "full" },
    { path: "home", component: HomeComponent },
    vendorsRoute,
    { path: "**", redirectTo: "home" }
];

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot(appRoutes),
        VendorsModule
    ]
})
export class AppModuleShared {
}
