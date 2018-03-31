import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html'
})
export class FetchDataComponent {
    public models: IVenue[];

    public currentModel: IVenue = {
        id: 0,
        name: "Venue1"
    }

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        http.get(baseUrl + 'api/SampleData/GetAllTestModels').subscribe(result => {
            this.models = result.json() as IVenue[];
        }, error => console.error(error));
    }

    public saveCurrentModel() {
        this.http.post(this.baseUrl + 'api/SampleData/PostTest', this.currentModel)
            .subscribe(res => { }, error => console.error(error));
    }
}

interface IVenue {
    id: number;
    name: string;
}
