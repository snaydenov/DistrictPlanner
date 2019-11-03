import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Road } from '../models/road';

@Injectable({
    providedIn: 'root',
})
export class RoadsService {
    constructor(public http: HttpClient,
        @Inject('BASE_URL') public baseUrl: string) {
    }

    public deleteRoad(road: Road) {

        return this.http.delete(this.baseUrl + 'api/roads/' + road.roadId);
    }

    public createRoad(road: Road) {
        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
            })
        };

        return this.http.post(this.baseUrl + 'api/roads', road, httpOptions);
    }

    public updateRoad(road: Road) {
        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
            })
        };

        return this.http.put(this.baseUrl + 'api/roads/' + road.roadId, road, httpOptions);
    }

    public getRoads() {
        return this.http.get<Road[]>(this.baseUrl + 'api/roads');
    }
}
