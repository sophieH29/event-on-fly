import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { RouterModule, Route } from "@angular/router";

import { AppComponent } from "./app.component";
import { HomeComponent } from "./home/home.component";
import { VendorsModule } from "./vendors/vendors.module";
import { vendorsRoute } from "./vendors/vendors.module";
import { BaseUrlHttpInterceptor } from "./apiRelated/baseUrlHttpInterceptor";

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
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot(appRoutes),
        VendorsModule
    ],
    //providers: [
    //    { provide: HTTP_INTERCEPTORS, useClass: BaseUrlHttpInterceptor, multi: true }
    //]
})
export class AppModuleShared {
}
