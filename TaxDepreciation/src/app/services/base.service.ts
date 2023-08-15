import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BaseService {
  public generateHeaders() {
    return {
      headers: new HttpHeaders({ "Content-Type": "application/json", "X-Api-Key": environment.APIKey}),
    };
  }
}
