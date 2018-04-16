//import {
//    HttpInterceptor, HttpRequest, HttpHandler, HttpSentEvent,
//    HttpHeaderResponse, HttpResponse, HttpProgressEvent, HttpUserEvent
//} from "@angular/common/http";
//import { Observable } from "rxjs/Observable";
//import { Injectable, Inject } from "@angular/core";
//import { BASE_URL } from "../common/injectableVariables";

//@Injectable()
//export class BaseUrlHttpInterceptor implements HttpInterceptor {

//    constructor( @Inject(BASE_URL) private baseUrl: string) {
//    }

//    intercept(req: HttpRequest<any>, next: HttpHandler):
//        Observable<HttpSentEvent | HttpHeaderResponse | HttpProgressEvent | HttpResponse<any> | HttpUserEvent<any>> {
//        let resUrl = req.url.startsWith("/") ? this.baseUrl + req.url : req.url;
//        req = req.clone({ url: resUrl });
//        return next.handle(req);
//    }
//}