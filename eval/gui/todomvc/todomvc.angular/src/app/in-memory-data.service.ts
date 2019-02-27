import { Injectable } from '@angular/core';
import { InMemoryDbService } from 'angular-in-memory-web-api';
import { TodoEntry } from './todoentry';

@Injectable({
  providedIn: 'root'
})
export class InMemoryDataService implements InMemoryDbService {
  createDb() {
    console.log("Ceating DB...");
    const todos = [
      { id: 1,
        text: "Needs to be done",
        done: false,
        archieved: false
      },
      { id: 2,
        text: "Clean the house",
        done: false,
        archieved: false
      },
      { id: 3,
        text: "Empty trash bin",
        done: true,
        archieved: false
      },
    ];
    return {todos};
  }

  getId(todoEntries: TodoEntry[]): number {
    return todoEntries.length>0 ? Math.max(...todoEntries.map(todo => todo.id))+1 : 11;
  }
  constructor() { }
}
