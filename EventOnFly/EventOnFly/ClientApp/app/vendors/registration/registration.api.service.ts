import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import "rxjs/add/operator/toPromise";

@Injectable()
export class RegistrationApiService {

    constructor(private http: Http) {
    }

    public startRegistration(): Promise<StartRegistrationResult> {
        return this.http.post("/api/registration/startRegistration", {
            username: "user1",
            password: "test",
            email: "www@mm.nn",
            attachedServiceTypes: []
        })
            .toPromise()
            .then(resp => resp.json().data as StartRegistrationResult)
            .catch(err => Promise.reject(err));
    }

}

enum StartRegistrationResult {
    Success = 0,
    UserAlreadyExists = 1,
    Error = 3
}