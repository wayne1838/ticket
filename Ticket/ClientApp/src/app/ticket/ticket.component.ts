import { HttpClient } from '@angular/common/http';
import { Component, Inject, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';  
import { MatTable } from '@angular/material/table';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-ticket-component',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.css']
})
export class TicketComponent {
  public tickets: TicketModel[];
  @ViewChild(MatTable, { static: false }) table: MatTable<TicketModel>;
  displayedColumns: string[] = ['type', 'status', 'summary', 'desc'];
  role = "";
  constructor(
    private router: Router,
    private jwtHelperService: JwtHelperService,
    private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.getUserRole();
    this.getList();

    if (this.role == "QA") {
      this.displayedColumns = [...this.displayedColumns, 'edit', 'remove'];
    } else if (this.role == "RD") {
      this.displayedColumns = [...this.displayedColumns, 'solve'];
    }
  }

  getUserRole() {
    //取得使用者角色
    var accessToken = localStorage.access_token;
    //如果沒有資訊代表沒有登入 導到登入
    if (!accessToken) {
      this.router.navigate(['/login']);
    }

    //解析JWT
    var token = this.jwtHelperService.decodeToken(accessToken);
    this.role = token.roles;

   
  }

  getList() {

    this.http.post<any>('/api/ticket/list', { Type: 0, Summary: '' }).subscribe(data => {
      console.log(data);
      this.tickets = data.data;
      console.log(this.tickets);
    })
  }
  getRandom(min, max) {
    return Math.floor(Math.random() * max) + min;
  }
  addData() {
    var num :string= this.getRandom(1, 99999);
    var data: TicketModel = {
      type : TypeEnum.Error,
      status : StatusEnum.None,
      summary: "No."+num,
      desc: "測試用" + num
    };
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
  id?: number;
  type: TypeEnum;
  status: StatusEnum;
  summary: string;
  desc: string;
  serious?: number;
  priority?: number;
}

