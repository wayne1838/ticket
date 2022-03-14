import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class AuthService {
  constructor(private http: HttpClient, private router: Router) {
  }

  login(username: string) {
    //return this.http.post<any>('/api/login/login', {
    //  username: username
    //}).pipe(
    //  map(result => {
    //    console.log(result);
    //    localStorage.setItem('access_token', result.token);
    //    return true;
    //  }));
    this.http.post<any>('/api/login/login', { username: username }).subscribe(data => {
      console.log(data);
      localStorage.setItem('access_token', data.token);

      this.router.navigate(['/ticket']);
    })
  }

  logout() {
    localStorage.removeItem('access_token');
  }

  public get loggedIn(): boolean {
    return (localStorage.getItem('access_token') !== null);
  }
} 
