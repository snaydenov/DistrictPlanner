import { Component, Inject, Output, EventEmitter, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Settlement } from '../../shared/models/settlement';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Profile } from 'selenium-webdriver/firefox';
import { MatDialogRef } from '@angular/material';
import { Road } from '../../shared/models/road';
@Component({
  selector: 'road-manage-dialog',
  templateUrl: './road-manage-dialog.component.html'
})
export class RoadManageDialogComponent implements OnInit {

    @Input() settlements: Settlement[];

    @Input() road: Road;

    constructor(
        private dialogRef: MatDialogRef<RoadManageDialogComponent>) {
    }

    ngOnInit() {
        if (this.road) {
            this.createForm.setValue({
                startSettlementId: this.road.startSettlementId,
                endSettlementId: this.road.endSettlementId,
                distance: this.road.distance
            })
        }
    }

    createForm = new FormGroup({
        distance: new FormControl('', Validators.required),
        startSettlementId: new FormControl('', Validators.required),
        endSettlementId: new FormControl('', Validators.required)
    })

    onSubmit() {
        if (this.road) {
            this.road.startSettlementId = this.createForm.value.startSettlementId;
            this.road.endSettlementId = this.createForm.value.endSettlementId;
            this.road.distance = this.createForm.value.distance;
        }

        this.dialogRef.close({
            roadId: this.road ? this.road.roadId : undefined,
            startSettlementId: +this.createForm.value.startSettlementId,
            endSettlementId: +this.createForm.value.endSettlementId,
            distance: this.createForm.value.distance,
        });
    }

    onCancel() {
        this.dialogRef.close(null);
    }
}
