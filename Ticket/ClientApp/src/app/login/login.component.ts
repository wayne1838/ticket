import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  public user: string;
  userList: string[] = ['QAUserName', 'RDUserName', 'PMUserName', 'AdminUserName'];
  

  selectedUser = '';
  constructor(private http: HttpClient,private auth: AuthService, private router: Router) { }

  public login() {
    this.auth.login(this.selectedUser);
    //this.router.navigate(['/ticket']);
    //this.http.post<any>('/api/login/login', { username: this.selectedUser }).subscribe(data => {
    //  console.log(data);
    //  localStorage.setItem('access_token', data);
    //})
    //this.auth.login(this.user)
    //  .pipe(first())
    //  .subscribe(
    //    result => (this.router.navigate(['todos'])),
    //    err => this.error = 'Could not authenticate'
    //  );
  }
}


