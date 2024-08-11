import { Injectable } from '@angular/core';
import { environment } from '../../environment/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  PATH_OF_API = environment.apiUrl + 'student/';

  requestHeader = new HttpHeaders({ 'No-Auth': 'True' });
  constructor(
    private httpclient: HttpClient,
    private authService: AuthService
  ) {}

  public get(): Observable<any[]> {
    return this.httpclient.get<any>(this.PATH_OF_API + 'get-all');
  }

  public getById(id: string) {
    return this.httpclient.get<any>(this.PATH_OF_API + `get-student/${id}`);
  }

  public edit(student: any) {
    return this.httpclient.put(this.PATH_OF_API + `update/${student.id}`,student)
  }

  public delete(id: string) {
    return this.httpclient.delete<any>(this.PATH_OF_API + `delete/${id}`);
  }
}
