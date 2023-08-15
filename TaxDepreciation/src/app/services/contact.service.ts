import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { environment } from 'src/environments/environment';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/apiresponse.model';
import { map } from "rxjs/operators";
import { Contact } from '../models/contact.model';

@Injectable({
  providedIn: 'root'
})
export class ContactService extends BaseService {
  apiUrl: string = "";
  constructor(private http: HttpClient) { 
    super();
    this.apiUrl = environment.apiUrl + "contact";
  }


  public getContacts(searchTerm: string = ""){
    return this.http.get(`${this.apiUrl}/${searchTerm}`, this.generateHeaders()).pipe(
      map((res) => res as ApiResponse));
  }

  public getContact(id: number){
    return this.http.get(this.apiUrl + "?id=" + id,this.generateHeaders()).pipe(
      map((res) => res as ApiResponse));
  }

  public saveContact(data: Contact){
    return this.http.post(this.apiUrl,data).pipe(map((res) => res as ApiResponse));
  }

}
