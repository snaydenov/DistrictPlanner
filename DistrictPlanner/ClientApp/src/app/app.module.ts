import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatDialogModule } from '@angular/material';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { SettlementsComponent } from './settlements/settlements.component';
import { RoadsComponent } from './roads/roads.component';
import { SettlementAddDialogComponent } from './settlements/settlement-add/settlement-add-dialog.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RoadManageDialogComponent } from './roads/roads-manage-dialog/road-manage-dialog.component';

@NgModule({
  declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        SettlementsComponent,
        RoadsComponent,
        SettlementAddDialogComponent,
        RoadManageDialogComponent
  ],
  entryComponents: [
      SettlementAddDialogComponent,
      RoadManageDialogComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
        { path: '', component: HomeComponent, pathMatch: 'full' },
        { path: 'settlements', component: SettlementsComponent },
        { path: 'roads', component: RoadsComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
