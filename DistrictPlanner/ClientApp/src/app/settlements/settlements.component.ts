import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Settlement } from '../shared/models/settlement';
import { MatDialogConfig, MatDialog, MatDialogRef } from '@angular/material';
import { SettlementAddDialogComponent } from './settlement-add/settlement-add-dialog.component';

@Component({
  selector: 'settlements',
  templateUrl: './settlements.component.html'
})
export class SettlementsComponent {
    public settlements: Settlement[];

    constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string, public dialog: MatDialog) {
        http.get<Settlement[]>(baseUrl + 'api/settlements').subscribe(result => {
             this.settlements = result;
        }, error => console.error(error));
    }

    public createSettlement() {
        this.dialog.open(SettlementAddDialogComponent).afterClosed()
            .subscribe(item => {
                if (item) {
                    let settlement: Settlement = { settlementId: undefined  , name: item };

                    const httpOptions = {
                        headers: new HttpHeaders({
                            'Content-Type': 'application/json',
                        })
                    };

                    this.http.post(this.baseUrl + 'api/settlements', settlement, httpOptions).subscribe(result => {
                        this.settlements.push(result as Settlement);
                    });
                }
            });
    }

    public updateSettlement(settlement: Settlement) {
        let dialogRef: MatDialogRef<SettlementAddDialogComponent> = this.dialog.open(SettlementAddDialogComponent);

        dialogRef.componentInstance.settlement = settlement;

        dialogRef.afterClosed()
            .subscribe(settlement => {
                if (settlement) {
                    
                    const httpOptions = {
                        headers: new HttpHeaders({
                            'Content-Type': 'application/json',
                        })
                    };

                    this.http.put(this.baseUrl + 'api/settlements/' + settlement.settlementId, settlement, httpOptions).subscribe(result => {
                    });
                }
            });
    }

    public deleteSettlement(settlement:Settlement) {

        this.http.delete(this.baseUrl + 'api/settlements/' + settlement.settlementId).subscribe(result => {
            this.settlements = this.settlements.filter(set => set.settlementId != settlement.settlementId);
          });      
    }
}
