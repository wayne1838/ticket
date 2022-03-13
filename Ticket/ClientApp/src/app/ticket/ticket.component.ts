import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';  

@Component({
  selector: 'app-ticket-component',
  templateUrl: './ticket.component.html'
})
export class TicketComponent {
  public tickets: TicketModel[];
  roleList: string[] = ['QA', 'RD', 'PM','Admin'];  
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<TicketModel[]>(baseUrl + 'api/ticket/hello').subscribe(result => {
      //this.tickets = result;
      console.log(result);
    }, error => console.error(error));
  }

  selectedRole = '';

  
  login() {
    console.log(this.selectedRole);
  }

}

interface TicketModel {
  Id: number;
  Type: number;
  Status: number;
  Summary: string;
  Desc: string;
  Serious: number;
  Priority: number;
}

