import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Road } from '../shared/models/road';

@Component({
  selector: 'roads',
  templateUrl: './roads.component.html'
})
export class RoadsComponent {
  public roads: Road[];

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        http.get<Road[]>(baseUrl + 'api/roads').subscribe(result => {
      this.roads = result;
    }, error => console.error(error));
  }
}
