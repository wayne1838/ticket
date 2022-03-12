import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-ticket-component',
  templateUrl: './ticket.component.html'
})
export class TicketComponent {
  public tickets: TicketModel[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<TicketModel[]>(baseUrl + 'api/ticket/hello').subscribe(result => {
      //this.tickets = result;
      console.log(result);
    }, error => console.error(error));
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

