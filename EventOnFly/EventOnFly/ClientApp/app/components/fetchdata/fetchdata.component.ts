import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html'
})
export class FetchDataComponent {
    public models: ITestModel[];

    public currentModel: ITestModel = {
        id: 1,
        dateTimeColumn: new Date(),
        stringColumn: "test"
    }

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        http.get(baseUrl + 'api/SampleData/GetAllTestModels').subscribe(result => {
            this.models = result.json() as ITestModel[];
        }, error => console.error(error));
    }

    public saveCurrentModel() {
        this.http.post(this.baseUrl + 'api/SampleData/PostTest', this.currentModel)
            .subscribe(res => { }, error => console.error(error));
    }
}

interface ITestModel {
    id: number;
    dateTimeColumn: Date;
    stringColumn: string;
}
