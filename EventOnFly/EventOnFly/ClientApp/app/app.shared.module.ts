import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { HttpModule, XHRBackend } from '@angular/http';
import { RouterModule, Route } from "@angular/router";
import { AuthenticateXHRBackend } from './authenticate-xhr.backend';
import { BrowserModule } from '@angular/platform-browser';

import { routing } from './app.routing';

import { AppComponent } from "./app.component";
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';

import { AccountModule } from './vendors/account/account.module';
import { DashboardModule } from './vendors/dashboard/dashboard.module';
import { ConfigService } from './shared/utils/config.service';

@NgModule({
    declarations: [
        AppComponent,
        HeaderComponent,
        HomeComponent
    ],
    imports: [
        AccountModule,
        DashboardModule,
        BrowserModule,
        FormsModule,
        HttpModule,
        routing
    ],
    providers: [ConfigService, {
        provide: XHRBackend,
        useClass: AuthenticateXHRBackend
    }]
})

export class AppModuleShared {
}
