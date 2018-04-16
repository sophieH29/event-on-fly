import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { Observable } from "rxjs/Observable";

@Injectable()
export class RegistrationApiService {

    constructor(private http: Http) {
    }

    //public startRegistration(): Observable<StartRegistrationResult> {
    //    return this.http.post("/api/registration/startRegistration", {});
    //} 
    
}

enum StartRegistrationResult {
    Success = 0,
    UserAlreadyExists = 1,
    Error = 3
}