import { Component, Inject, Output, EventEmitter, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Settlement } from '../../shared/models/settlement';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Profile } from 'selenium-webdriver/firefox';
import { MatDialogRef } from '@angular/material';
@Component({
  selector: 'settlement-add-dialog',
  templateUrl: './settlement-add-dialog.component.html'
})
export class SettlementAddDialogComponent implements OnInit {

    @Input() settlement: Settlement;

    constructor(
        private dialogRef: MatDialogRef<SettlementAddDialogComponent>) {
    }

    ngOnInit() {
        if (this.settlement) {
            this.createForm.setValue({ name: this.settlement.name });
        }

    }

    createForm = new FormGroup({
        name: new FormControl('', Validators.required)
    })

    onSubmit() {
        if (this.settlement) {
            this.settlement.name = this.createForm.value.name;
            this.dialogRef.close(this.settlement);
        }
        else {
            this.dialogRef.close(this.createForm.value.name);
        }
    }

    onCancel() {
        
        this.dialogRef.close(null);
    }
}
