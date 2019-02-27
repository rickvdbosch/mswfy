import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';

const filesUrl = `${environment.servicesBaseUrl}scale`;

@Injectable({
  providedIn: 'root'
})
export class ScaleService {

  constructor(private httpClient: HttpClient) { }

  upload(file: File): Observable<any> {
    const formData: FormData = new FormData();
    formData.append('file', file, file.name);
console.log(filesUrl);
    return this.httpClient.post(filesUrl, formData);
  }
}
