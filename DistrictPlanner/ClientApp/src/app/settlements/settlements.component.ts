import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Settlement } from '../shared/models/settlement';
import { MatDialogConfig, MatDialog, MatDialogRef } from '@angular/material';
import { SettlementAddDialogComponent } from './settlement-add/settlement-add-dialog.component';
import { SettlementsService } from '../shared/services/settlements.service';

@Component({
  selector: 'settlements',
  templateUrl: './settlements.component.html'
})
export class SettlementsComponent {
    public settlements: Settlement[];

    constructor(public http: HttpClient,
        @Inject('BASE_URL') public baseUrl: string,
        public dialog: MatDialog,
        public settlementsService: SettlementsService) {
        this.settlementsService.getSettlements().subscribe(result => {
             this.settlements = result;
        });
    }

    public createSettlement() {
        this.dialog.open(SettlementAddDialogComponent).afterClosed()
            .subscribe(item => {
                if (item) {
                    let settlement: Settlement = { settlementId: undefined  , name: item, isMain:false};

                    this.settlementsService.createSettlement(settlement).subscribe(result => {
                        this.settlements.push(result as Settlement);
                    });
                }
            });
    }

    public updateSettlement(settlement: Settlement) {
        let dialogRef: MatDialogRef<SettlementAddDialogComponent> = this.dialog.open(SettlementAddDialogComponent);

        dialogRef.componentInstance.settlement = settlement;

        dialogRef.afterClosed()
            .subscribe(value => {
                console.log(value);
                if (value) {

                    this.settlementsService.updateSettlement(value).subscribe(result => {
                    });
                }
            });
    }

    public deleteSettlement(settlement:Settlement) {

        this.settlementsService.deleteSettlement(settlement).subscribe(result => {
            this.settlements = this.settlements.filter(set => set.settlementId != settlement.settlementId);
          });      
    }

    public getMainSettlement() {
        return this.settlementsService.getMainSettlement().subscribe(result => {
            if (result && !result.isMain) {
                alert('Setting main settlement to ' + result.name);

                result.isMain = true;

                let oldMainSettlement = this.settlements.find(s => s.isMain);

                if (oldMainSettlement) {
                    oldMainSettlement.isMain = false;
                    this.settlementsService.updateSettlement(oldMainSettlement).subscribe();
                }

                this.settlementsService.updateSettlement(result).subscribe(result => {
                    this.settlementsService.getSettlements().subscribe(result => {
                        this.settlements = result;
                    });
                });
            }
            else {
                alert('No changes');
            }
        });
    }
}
