import { Component, OnInit } from '@angular/core';
import { TodoEntry } from '../todoentry';
import { TodoService } from '../todo.service';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-todos',
  templateUrl: './todos.component.html',
  styleUrls: ['./todos.component.css']
})
export class TodosComponent implements OnInit {

  todoEntries: TodoEntry[];
  newTodo : string = '';

  constructor(
    private todoService: TodoService,
    private messages: MessageService) { }

  ngOnInit() {
    this.messages.add(`Todos init`);
    this.getTodoEntries();
  }

  getTodoEntries(): void {
    this.todoService.getTodoEntries()
      .subscribe(
        todos => {
          this.messages.add(`loaded ${todos.length}`);
          this.todoEntries = todos;
        });
  }

  onInput(event) {
    console.log(`Got input ${this.newTodo} / ${event}`);
    this.getTodoEntries();
  }

}
