import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Road } from '../shared/models/road';
import { Settlement } from '../shared/models/settlement';
import { RoadManageDialogComponent } from './roads-manage-dialog/road-manage-dialog.component';
import { MatDialog, MatDialogRef } from '@angular/material';
import { RoadsService } from '../shared/services/roads.service';
import { SettlementsService } from '../shared/services/settlements.service';

@Component({
  selector: 'roads',
  templateUrl: './roads.component.html'
})
export class RoadsComponent {
  public roads: Road[];
    public settlements: Settlement[];
    constructor(public http: HttpClient,
        @Inject('BASE_URL') public baseUrl: string,
        public dialog: MatDialog,
        public roadsService: RoadsService,
        public settlementsService: SettlementsService) {
        settlementsService.getSettlements().subscribe(result => {
            this.settlements = result;
        }, error => console.error(error));

        roadsService.getRoads().subscribe(result => {
            this.roads = result;
        }, error => console.error(error));
    }

    getSettlementNameById(settlementId: number) {
        return this.settlements.find(set => set.settlementId == settlementId).name;
    }

    public createRoad() {
        let dialogRef: MatDialogRef<RoadManageDialogComponent> = this.dialog.open(RoadManageDialogComponent);

        dialogRef.componentInstance.settlements = this.settlements;

        dialogRef.afterClosed().subscribe(road => {
                if (road) {
                    this.roadsService.createRoad(road).subscribe(result => {
                        this.roads.push(result as Road);
                    });
                }
            });
    }

    public updateRoad(road: Road) {
        let dialogRef: MatDialogRef<RoadManageDialogComponent> = this.dialog.open(RoadManageDialogComponent);

        dialogRef.componentInstance.settlements = this.settlements;
        dialogRef.componentInstance.road = road;

        dialogRef.afterClosed()
            .subscribe(road => {
                if (road) {

                    this.roadsService.updateRoad(road).subscribe(result => {
                    });
                }
            });
    }

    public deleteRoad(road: Road) {

        this.roadsService.deleteRoad(road).subscribe(result => {
            this.roads = this.roads.filter(r => r.roadId != road.roadId);
        });
    }
}
