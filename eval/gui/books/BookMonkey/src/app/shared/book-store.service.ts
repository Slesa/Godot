import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Book } from './book';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookStoreService {
  private api = 'https://api3.angular-buch.com';
  books: Book[];

  getAll(): Observable<Book[]> {
    return this.http.get<any[]>(`${this.api}/books`);
  }

  getSingle(isbn: string): Observable<Book> {
    return this.http.get<any>( `${this.api}/book/${isbn}`);
  }

  remove(isbn: string): Observable<any> {
    return this.http.delete(`${this.api}/book/${isbn}`, {responseType: 'text'});
  }

  constructor(private http: HttpClient) {

    /*
    this.books =
    this.books = [
      {
        isbn: '9783864906466',
        title: 'Angular',
        authors: ['Ferdinand Malcher', 'Johannes Hoppe', 'Danny Koppenhagen'],
        published: new Date(2019, 4, 30),
        subtitle: 'Grundlagen, fortgeschrittene Themen und Best Practices - mit NativeScript und NgRx',
        rating: 5,
        thumbnails: [{
          url: 'https://ng-buch.de/buch1.jpg',
          title: 'Buchcover'
        }],
        description: 'Die Autoren führen Sie mit einem anspruchsvollen Beispielprojekt durch die Welt von Angular...'
      },
      {
        isbn: '9783864903274',
        title: 'React',
        authors: ['Oliver Zeigermann', 'Nils Hartmann'],
        published: new Date(2016, 6, 17),
        subtitle: 'Die praktische Einführung in React, React Router und Redux',
        rating: 3,
        thumbnails: [{
          url: 'https://ng-buch.de/buch2.jpg',
          title: 'Buchcover'
        }],
        description: 'React ist ein JavaScript-Framework zur Entwicklung von Benutzeroberflächen...'
      }
    ]; */
   }
}
