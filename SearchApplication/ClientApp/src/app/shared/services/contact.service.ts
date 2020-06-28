import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { IContact } from '../interfaces/i-contact';
import { Observable, throwError } from 'rxjs';
import { tap, map, catchError} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  baseUrl: string;
  constructor(private httpClient: HttpClient, @Inject('BASE_URL') url: string) {
    this.baseUrl = url;
   }

  public getContacts(name:string) : Observable<IContact[]> {
    return this.httpClient.get<IContact[]>(`/api/contact/${name}`).pipe(
      tap(response => console.log('All: ' + JSON.stringify(response))
      ), catchError(this.handleError)
    );
  }

  public addContact(contact: IContact): Observable<boolean>{
    return this.httpClient.post('/api/contact', contact).pipe(
      map((response: any) => {
        return response;
      }), catchError(this.handleError)
    );
  }
  //Error Handler
  private handleError(err: HttpErrorResponse){
    let errorMessage = '';
    if (err.error instanceof ErrorEvent){
        errorMessage = `An error Occurred: ${err.error.message}`;
    } 
    else{
        errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    return throwError(errorMessage);
}
}
