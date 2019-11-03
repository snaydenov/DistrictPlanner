import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Settlement } from '../models/settlement';

@Injectable({
    providedIn: 'root',
})
export class SettlementsService {
    constructor(public http: HttpClient,
        @Inject('BASE_URL') public baseUrl: string) {
    }

    public deleteSettlement(settlement: Settlement) {

        return this.http.delete(this.baseUrl + 'api/settlements/' + settlement.settlementId);
    }

    public createSettlement(settlement: Settlement) {
        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
            })
        };

        return this.http.post(this.baseUrl + 'api/settlements', settlement, httpOptions);
    }

    public updateSettlement(settlement: Settlement) {
        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
            })
        };

        return this.http.put(this.baseUrl + 'api/settlements/' + settlement.settlementId, settlement, httpOptions);
    }

    public getSettlements() {
        return this.http.get<Settlement[]>(this.baseUrl + 'api/settlements');
    }

    public getMainSettlement() {
        return this.http.get<Settlement>(this.baseUrl + 'api/settlements/main');
    }

}
