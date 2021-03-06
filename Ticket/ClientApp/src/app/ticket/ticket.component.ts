import { HttpClient } from '@angular/common/http';
import { Component, Inject, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';  
import { MatTable } from '@angular/material/table';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-ticket-component',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.css']
})
export class TicketComponent {
  public tickets: TicketModel[];
  @ViewChild(MatTable, { static: false }) table: MatTable<TicketModel>;
  displayedColumns: string[] = ['info','type', 'status', 'summary', 'desc'];
  role = "";
  isOpenErrorWindow: boolean = false;
  isUpdate: boolean = false;
  isReadOnly: boolean = false;

  //取得列表enum的內容
  public typeEnums = Object.values(TypeEnum).filter(value => typeof value === 'number');
  public statusEnums = Object.values(StatusEnum).filter(value => typeof value === 'number');
  ticketInfo: TicketModel = {
    type: TypeEnum.Error,
    status: StatusEnum.None,
    summary: "",
    desc: ""
  };
  constructor(
    private router: Router,
    private jwtHelperService: JwtHelperService,
    public dialog: MatDialog,
    private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.getUserRole();
    this.getList();
    //依照權限設定表格內容
    if (this.role == "QA") {
      this.displayedColumns = ['edit', 'remove', ...this.displayedColumns];
    } else if (this.role == "RD") {
      this.displayedColumns = ['solve', ...this.displayedColumns];
    }
  }
  openErrorInfoDialog(data): void {//查看明細
    this.isUpdate = false;
    this.isReadOnly = true;
    this.ticketInfo = Object.assign({}, data);
    this.isOpenErrorWindow = true;
  }
  openErrorAddDialog(): void {//新增明細
    this.isUpdate = false;
    this.isReadOnly = false;
    this.isOpenErrorWindow = true;
    this.ticketInfo = {
      type: TypeEnum.Error,
      status: StatusEnum.UnSolve,
      summary: "",
      desc: ""
    };
  }
  openErrorEditDialog(data): void {//編輯明細
    this.isUpdate = true;
    this.isReadOnly = false;
    this.ticketInfo = Object.assign({}, data);;
    this.isOpenErrorWindow = true;
  }
  closeErrorDialog(): void {//關閉明細
    this.isOpenErrorWindow = false;
    this.isReadOnly = false;
    this.isUpdate = false;
    this.ticketInfo = {
      type: TypeEnum.Error,
      status: StatusEnum.None,
      summary: "",
      desc: ""
    };
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
    //取得列表
    this.http.post<any>('/api/ticket/list', {  Summary: '' }).subscribe(data => {
      console.log(data);
      this.tickets = data.data;
      console.log(this.tickets);
    })
  }
  
  btnAddError() {
   
    this.http.post<any>('/api/ticket/error', this.ticketInfo)
      .subscribe({
        next: data => {
          console.log(data);
          this.getList();
          this.closeErrorDialog();
        },
        error: error => {
          alert(error.message);
          //this.errorMessage = error.message;
          console.error('There was an error!', error);
        }
      });
  }
  btnAddFeatureRequest() {
   
    this.http.post<any>('/api/ticket/featurerequest', this.ticketInfo)
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
  btnAddTestCase() {
   
    this.http.post<any>('/api/ticket/testcase', this.ticketInfo)
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

  btnSolve(id) {
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
  btnRemove(id) {
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
  btnEdit(id) {
    this.isUpdate = true;
    this.isOpenErrorWindow = true;

    this.http.put<any>('/api/ticket/' + id, this.ticketInfo)
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
  FeatureRequest=1,
  TestCase=2
}

export const TypeEnumLabel = new Map<number, string>([
  [TypeEnum.Error, '錯誤'],
  [TypeEnum.FeatureRequest, '功能請求'],
  [TypeEnum.TestCase, '測試用例']
]);

export enum StatusEnum {
  None = 0,
  UnSolve = 1,
  Solve = 2
}

export const StatusEnumLabel = new Map<number, string>([
  [StatusEnum.None, '無'],
  [StatusEnum.UnSolve, '未解決'],
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

