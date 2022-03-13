import { HttpClient } from '@angular/common/http';
import { Component, Inject, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';  
import { MatTable } from '@angular/material/table';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-ticket-component',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.css']
})
export class TicketComponent {
  public tickets: TicketModel[];
  @ViewChild(MatTable, { static: false }) table: MatTable<TicketModel>;
  displayedColumns: string[] = ['type', 'status', 'summary', 'desc', 'edit','solve','remove'];
  role = "";
  constructor(private jwtHelperService: JwtHelperService,private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.getUserRole();
    this.getList();

  }

  getUserRole() {
    //取得使用者角色

    //解析JWT
    var token = this.jwtHelperService.decodeToken(localStorage.access_token);
    this.role = token.roles;
   
  }

  getList() {

    this.http.post<any>('/api/ticket/list', { Type: 0, Summary: '' }).subscribe(data => {
      console.log(data);
      this.tickets = data.data;
      console.log(this.tickets);
    })
  }
  
  addData() {
    var data: TicketModel;
    data.type = TypeEnum.Error;
    data.status = StatusEnum.None;
    data.summary = "test";
    data.desc = "測試用";
    this.http.post<any>('/api/ticket/error', data)
      .subscribe({
        next: data => {
          console.log(data);
          this.getList();
        },
        error: error => {
          alert(error.message);
          //this.errorMessage = error.message;
          console.error('There was an error!', error);
        }
      });
  }

  solve(id) {
    this.http.put<any>('/api/ticket/status/' + id, {})
      .subscribe({
        next: data => {
          console.log(data);
          this.getList();
        },
        error: error => {
          alert(error.message);
          //this.errorMessage = error.message;
          console.error('There was an error!', error);
        }
      });
     
  }
  remove(id) {
    this.http.delete<any>('/api/ticket/' + id).subscribe({
      next: data => {
        console.log(data);
        this.getList();
      },
      error: error => {
        alert(error.message);
        //this.errorMessage = error.message;
        console.error('There was an error!', error);
      }
    });
  }
  edit(id) {
    this.http.put<any>('/api/ticket/' + id, {})
      .subscribe({
        next: data => {
          console.log(data);
          this.getList();
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
  id: number;
  type: TypeEnum;
  status: number;
  summary: string;
  desc: string;
  serious: number;
  priority: number;
}

