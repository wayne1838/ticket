import { HttpClient } from '@angular/common/http';
import { Component, Inject, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';  
import { MatTable } from '@angular/material/table';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-ticket-info-component',
  templateUrl: './ticket-info.component.html'
})
export class TicketInfoComponent {
  public tickets: TicketModel[];
  @ViewChild(MatTable, { static: false }) table: MatTable<TicketModel>;
  displayedColumns: string[] = ['type', 'status', 'summary', 'desc'];
  role = "";
  ticketInfo: TicketModel =null;
  constructor(
    private router: Router,
    private jwtHelperService: JwtHelperService,
    private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  
  }


  addError() {
    
    this.http.post<any>('/api/ticket/error', this.ticketInfo)
      .subscribe({
        next: data => {
          console.log(data);
        },
        error: error => {
          alert(error.message);
          //this.errorMessage = error.message;
          console.error('There was an error!', error);
        }
      });
  }
  getTypeDesc(type:number) {
    return TypeEnumLabel.get(type);
  }
  getStatusDesc(status: number) {
    return StatusEnumLabel.get(status);
  }
}

export enum TypeEnum {
  Error=0,
  FunctionRequest=1,
  TestCase=2
}

export const TypeEnumLabel = new Map<number, string>([
  [TypeEnum.Error, '錯誤'],
  [TypeEnum.FunctionRequest, '功能請求'],
  [TypeEnum.TestCase, '測試用例']
]);

export enum StatusEnum {
  None = 0,
  Solve = 1
}

export const StatusEnumLabel = new Map<number, string>([
  [StatusEnum.None, '無'],
  [StatusEnum.Solve, '已解決']
]);
interface TicketModel {
  id?: number;
  type: TypeEnum;
  status: StatusEnum;
  summary: string;
  desc: string;
  serious?: number;
  priority?: number;
}

