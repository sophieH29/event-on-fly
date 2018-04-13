import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";

@Injectable()
export class RegistrationApiService {

    constructor(private http: HttpClient) {
    }

    public startRegistration(): Observable<StartRegistrationResult> {
        return this.http.post<StartRegistrationResult>("/api/registration/startRegistration", {});
    } 
    
}

enum StartRegistrationResult {
    Success = 0,
    UserAlreadyExists = 1,
    Error = 3
}