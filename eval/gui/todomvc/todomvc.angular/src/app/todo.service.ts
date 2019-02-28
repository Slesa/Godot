import { Injectable } from '@angular/core';
import { TodoEntry } from './todoentry';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MessageService } from './message.service';
import { catchError, map, tap } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders ({ 'Content-Type': 'application/json'})
};

// https://toddmotto.com/component-events-event-emitter-output-angular-2
@Injectable({
  providedIn: 'root'
})
export class TodoService {
  private todosUrl = 'api/todos';

  constructor(
    private http: HttpClient,
    private messageService: MessageService
  ) { }

  getTodoEntries(): Observable<TodoEntry[]> {
    return this.http.get<TodoEntry[]>(this.todosUrl)
      .pipe(
        tap(_ => this.log('fetched todoEntries')),
        catchError(this.handleError(`getTodoEntries`, []))
      );
  }

  getTodoEntry(id: number): Observable<TodoEntry> {
    const url = `${this.todosUrl}/${id}`;
    return this.http.get<TodoEntry>(url)
      .pipe(
        tap(_ => this.log(`fetched todo ${id}`)),
        catchError(this.handleError<TodoEntry>(`getTodoEntry ${id}`))
      );
  }

  addTodoEntry(todoEntry: TodoEntry): Observable<TodoEntry> {
    this.log(`adding todo ${todoEntry.text}`);
    return this.http.post(this.todosUrl, todoEntry, httpOptions).pipe(
      tap(_ => this.log(`added todo ${todoEntry.id}`)),
      catchError(this.handleError<any>('addTodo'))
    );
  }

  updateTodoEntry(todoEntry: TodoEntry): Observable<TodoEntry> {
    this.log(`updating todo ${todoEntry.text}`);
    return this.http.post(this.todosUrl, todoEntry, httpOptions).pipe(
      tap(_ => this.log(`updated todo ${todoEntry.id}`)),
      catchError(this.handleError<any>('updateTodo'))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      this.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    }
  }
  private log(msg: string): void {
    this.messageService.add(`TodoService: ${msg}`);
  }
}
