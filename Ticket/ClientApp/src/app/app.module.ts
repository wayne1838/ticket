import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { TicketComponent } from './ticket/ticket.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialExampleModule } from './material.module';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthService } from './auth.service';
import { AuthGuard } from './auth.guard'; 
import { AuthInterceptor } from './AuthInterceptor';
export function tokenGetter() { return localStorage.getItem('access_token'); } 

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    TicketComponent,
    FetchDataComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    MaterialExampleModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: LoginComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'ticket', component: TicketComponent, canActivate: [AuthGuard]},
      { path: 'fetch-data', component: FetchDataComponent },
    ]),
    
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        //whitelistedDomains: ['localhost:4000'],
        //blacklistedRoutes: ['localhost:4000/api/auth']
      }
    }),
    BrowserAnimationsModule
  ],
  providers: [AuthService, AuthGuard, { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }, ],
  bootstrap: [AppComponent]
})
export class AppModule { }
